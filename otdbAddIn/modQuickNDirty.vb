

REM ***********************************************************************************************************************************************
REM *********** ON TRACK DATABASE TOOLING 4 EXCEL
REM *********** 
REM *********** LEGACY MODULE FUNCTIONS AND CLASSES DOC9 related 
REM ***********
REM *********** Version: X.YY
REM *********** Created: 2013-08-08
REM *********** Last Change:
REM ***********
REM *********** Change Log:
REM ***********
REM *********** (C) by Boris Schneider 2013
REM ***********************************************************************************************************************************************

Option Explicit On

Imports OnTrack.XChange
Imports OnTrack.AddIn.My
Imports OnTrack.Deliverables
Imports OnTrack.Parts
Imports OnTrack.Database

Public Module Doc9QuickNDirty

    Public Delegate Function CreateDoc18WkPkConfigDelegate() As Boolean
    Public CreateDoc18WkPkConfig As CreateDoc18WkPkConfigDelegate

    Public Delegate Function CreateDoc18ERoadmapConfigDelegate() As Boolean
    Public CreateDoc18ERoadmapConfig As CreateDoc18ERoadmapConfigDelegate


    'Private GlobalDoc9XChangeConfig As XChangeConfiguration
    ''' <summary>
    ''' creates a special XConfig for Microsoft Project Doc18 with outline
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateDoc18WkPkXConfig(Optional ByVal XCMD As otXChangeCommandType = otXChangeCommandType.Read) As XChange.XChangeConfiguration


        Try
            If Not CurrentSession.RequireAccessRight(accessRequest:=otAccessRight.ReadUpdateData) Then

                Return Nothing
            End If
            Dim aXChangeConfig As XChangeConfiguration = XChangeConfiguration.Create(MySettings.Default.DefaultDoc18MSPConfigNameDynamic)
            '* create x config
            If aXChangeConfig IsNot Nothing Then
                aXChangeConfig = XChangeConfiguration.Retrieve(MySettings.Default.DefaultDoc18MSPConfigNameDynamic)
                aXChangeConfig.Delete()
                aXChangeConfig = XChangeConfiguration.Create(MySettings.Default.DefaultDoc18MSPConfigNameDynamic)
            End If

            '* add objects
            aXChangeConfig.AddObjectByName(Deliverables.Deliverable.ConstTableID, xcmd:=XCMD)
            aXChangeConfig.AddObjectByName(Scheduling.ScheduleEdition.ConstTableID, xcmd:=XCMD)
            aXChangeConfig.AddObjectByName(Deliverables.Target.constTableID, xcmd:=XCMD)
            aXChangeConfig.AddObjectByName(Deliverables.Track.ConstTableID, xcmd:=XCMD)
            aXChangeConfig.AddObjectByName(Parts.Part.ConstTableID, xcmd:=XCMD)

            '* Add Attribute Mapping
            aXChangeConfig.AllowDynamicEntries = True


            ''''' Create the OUTLINE FOR IT
            Dim anOutline As New XOutline
            If Not anOutline.Create(id:=My.MySettings.Default.DefaultDoc18MSPConfigNameDynamic) Then
                anOutline.Inject(id:=My.MySettings.Default.DefaultDoc18MSPConfigNameDynamic)
                anOutline.Delete()
            End If
            anOutline.DynamicAddRevisions = True

            '***
            '*** create the sql query
            '***
            Dim aCommand As Database.ormSqlSelectCommand = _
                ot.GetTableStore(Deliverable.ConstTableID). _
                CreateSqlSelectCommand(id:=My.MySettings.Default.DefaultDoc18MSPConfigNameDynamic, addAllFields:=False)


            If Not aCommand.Prepared Then
                aCommand.select = Deliverable.ConstTableID & ".[" & Deliverable.constFNUid & "] ," & _
                                  Deliverable.ConstTableID & ".[" & Deliverable.constFNMatchCode & "] ," & _
                                  Parts.Part.ConstTableID & ".[" & Part.constFNRespOU & "] ," & _
                                   Parts.Part.ConstTableID & ".[" & Part.ConstFNWorkpackage & "] "
                aCommand.AddTable(tableid:=Part.ConstTableID, addAllFields:=False)
                aCommand.AddTable(tableid:=TrackItem.constTableID, addAllFields:=False)
                '" inner join " & clsOTDBTrackItem.constTableID & " on " & _
                aCommand.Where = _
                    Deliverable.ConstTableID & ".[" & Deliverable.constFNMatchCode & "] = " & TrackItem.constTableID & ".[" & TrackItem.constFNMatchCode & "]" & _
                     " AND " & _
                    Deliverable.ConstTableID & ".[" & Deliverable.constFNPartID & "] = " & Part.ConstTableID & ".[" & Part.ConstFNPartID & "]"

                aCommand.Where &= " AND " & Deliverable.ConstTableID & ".[" & Deliverable.ConstFNIsDeleted & "]=@IsDeleted and lcase(" & _
                    Deliverable.ConstTableID & ".[" & Deliverable.constFNDeliverableTypeID & "]) <> 'struktur' and " _
                    & Deliverable.ConstTableID & ".[" & Deliverable.constFNfuid & "] = 0"  ' no revision

                aCommand.AddParameter(New Database.ormSqlCommandParameter(ID:="@IsDeleted", _
                                                                                columnname:=Deliverable.ConstFNIsDeleted, _
                                                                                tablename:=Deliverable.ConstTableID))

                aCommand.OrderBy = "[" & TrackItem.constTableID & "." & TrackItem.constFNOrdinal & "] asc ," & _
                                   "[" & Part.ConstTableID & "." & Part.constFNRespOU & "] asc, " & _
                                   "[" & Part.ConstTableID & "." & Part.ConstFNWorkpackage & "] asc " & _
                                   ""
                aCommand.Prepare()
            End If

            If aCommand.Prepared Then
                aCommand.SetParameterValue(ID:="@IsDeleted", value:=False)

            End If

            '** put all deliverables to Outline 
            '**
            Dim aRecordCollection As List(Of Database.ormRecord) = aCommand.RunSelect
            Dim anordinal As Long = 10
            Dim precode As String = ""
            Dim site As String = ""
            Dim dept As String = ""
            Dim wkpk As String = ""
            Dim level As UShort = 0

            For Each aRecord In aRecordCollection

                If IsNumeric(aRecord.GetValue(1)) Then

                    '** Group on Precode
                    If precode = "" OrElse precode <> aRecord.GetValue(2) Then
                        precode = aRecord.GetValue(2)
                        level = 0
                        Dim anOIGroup As New XOutlineItem
                        anOIGroup.IsGroup = True
                        anOIGroup.IsText = True
                        anOIGroup.Text = precode
                        dept = ""
                        wkpk = ""
                        If anOIGroup.Create(ID:=anOutline.id, ordinal:=anordinal) Then
                            anOIGroup.Level = level
                            anOutline.AddOutlineItem(anOIGroup)
                        End If
                        anordinal += 10
                    End If
                    '** Group on Dept
                    If dept = "" OrElse dept <> aRecord.GetValue(3) Then
                        dept = aRecord.GetValue(3)
                        level = 1
                        Dim anOIGroup As New XOutlineItem
                        anOIGroup.IsGroup = True
                        anOIGroup.IsText = True
                        anOIGroup.Text = precode & " ( " & dept & " )"
                        wkpk = ""
                        If anOIGroup.Create(ID:=anOutline.id, ordinal:=anordinal) Then
                            anOIGroup.Level = level
                            anOutline.AddOutlineItem(anOIGroup)
                        End If
                        anordinal += 10
                    End If
                    '** Group on Workpackage
                    If wkpk = "" OrElse wkpk <> aRecord.GetValue(4) Then
                        wkpk = aRecord.GetValue(4)
                        level = 2
                        Dim anOIGroup As New XOutlineItem
                        anOIGroup.IsGroup = True
                        anOIGroup.IsText = True
                        anOIGroup.Text = precode & " ( " & dept & " / " & wkpk & " )"
                        If anOIGroup.Create(ID:=anOutline.id, ordinal:=anordinal) Then
                            anOIGroup.Level = level
                            anOutline.AddOutlineItem(anOIGroup)
                        End If
                        anordinal += 10
                    End If

                    Dim anOutlineItem As New XOutlineItem
                    If anOutlineItem.Create(ID:=anOutline.id, ordinal:=anordinal, uid:=aRecord.GetValue(1)) Then
                        anOutlineItem.Level = level + 1
                        anOutline.AddOutlineItem(anOutlineItem)
                    End If
                    anordinal += 10
                End If

            Next
            anOutline.Persist()
            aXChangeConfig.OutlineID = anOutline.id
            '*
            If aXChangeConfig.Persist() Then
                Return aXChangeConfig
            Else
                Return Nothing
            End If

        Catch ex As Exception
            CoreMessageHandler(exception:=ex, subname:="Doc9QuickNDirty.CreateDoc18XConfig")
            Return Nothing
        End Try

    End Function



    ''' <summary>
    '''  Creates a special XConfig (Dynmaic) for the Doc9 by Hand and saves it
    ''' </summary>
    ''' <param name="XCMD"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateDoc9XConfig(Optional ByVal XCMD As otXChangeCommandType = otXChangeCommandType.Read) As Boolean
        Dim aXChangeConfig As XChangeConfiguration = XChangeConfiguration.Create(MySettings.Default.DefaultDoc9ConfigNameDynamic)

        If aXChangeConfig Is Nothing Then
            aXChangeConfig = XChangeConfiguration.Retrieve(MySettings.Default.DefaultDoc9ConfigNameDynamic)
            aXChangeConfig.Delete()
        End If

        Call aXChangeConfig.AddObjectByName("tblschedules", xcmd:=XCMD)
        Call aXChangeConfig.AddObjectByName("tbldeliverabletargets", xcmd:=XCMD)
        Call aXChangeConfig.AddObjectByName("tbldeliverabletracks", xcmd:=XCMD)
        Call aXChangeConfig.AddObjectByName("tbldeliverables", xcmd:=XCMD)
        Call aXChangeConfig.AddObjectByName("tblparts", xcmd:=XCMD)
        Call aXChangeConfig.AddObjectByName("tblconfigs", xcmd:=XCMD)
        Call aXChangeConfig.AddObjectByName("ctblDeliverableObeyas", xcmd:=XCMD)
        Call aXChangeConfig.AddObjectByName("ctblDeliverableExpeditingStatus", xcmd:=XCMD)
        Call aXChangeConfig.AddObjectByName("tblDeliverableWorkstationCodes", xcmd:=XCMD)
        Call aXChangeConfig.AddObjectByName("tblxoutlineitems", xcmd:=XCMD)

        aXChangeConfig.AllowDynamicEntries = True

        CreateDoc9XConfig = aXChangeConfig.Persist()
    End Function



End Module
