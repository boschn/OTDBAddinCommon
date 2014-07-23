Imports OnTrack.UI
Imports Telerik.WinControls.UI
Imports System.Drawing
Imports System.Windows.Forms
Imports OnTrack.Database
Imports OnTrack.Commons
Imports System.ComponentModel

Namespace Global.OnTrack.UI
    Public Class UIControlDataGridViewEventArgs
        Inherits System.EventArgs

        Private _msgtext As String

        Public Sub New(messagetext As String)
            _msgtext = messagetext
        End Sub
        ''' <summary>
        ''' Gets or sets the msgtext.
        ''' </summary>
        ''' <value>The msgtext.</value>
        Public Property Msgtext() As String
            Get
                Return Me._msgtext
            End Get
            Set(value As String)
                Me._msgtext = value
            End Set
        End Property

    End Class
    ''' <summary>
    ''' DataGridView Control for Objects
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UIControlDataGridView
        Inherits RadGridView



        Private WithEvents _modeltable As ormModelTable
        Private WithEvents _statuslabel As RadLabelElement
        Private WithEvents _ProgressPictureBox As System.Windows.Forms.PictureBox
        Private _pwfieldname As String = ""

        Public Event OnStatusTextChanged As EventHandler(Of UIControlDataGridViewEventArgs)

        ''' <summary>
        ''' Gets or sets the progress picture box.
        ''' </summary>
        ''' <value>The progress picture box.</value>
        Public ReadOnly Property ProgressPictureBox() As PictureBox
            Get
                If _ProgressPictureBox Is Nothing Then
                    '
                    'ProgressPictureBox
                    '
                    _ProgressPictureBox = New System.Windows.Forms.PictureBox
                    CType(Me._ProgressPictureBox, System.ComponentModel.ISupportInitialize).BeginInit()
                    With _ProgressPictureBox

                        .Enabled = False
                        .Dock = System.Windows.Forms.DockStyle.Fill
                        .Image = Global.OnTrack.AddIn.My.Resources.Resources.progress_radar
                        '.Location = New System.Drawing.Point(0, 0)
                        .Name = "ProgressPictureBox"
                        .Size = Me.Size
                        .SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
                        .TabIndex = 0
                        .TabStop = False
                    End With
                    CType(Me._ProgressPictureBox, System.ComponentModel.ISupportInitialize).EndInit()
                End If
                If Me.Controls.Count = 0 Then Me.Controls.Add(_ProgressPictureBox)
                Return Me._ProgressPictureBox
            End Get

        End Property

        ''' <summary>
        ''' Gets or sets the selected data object.
        ''' </summary>
        ''' <value>The selected data object.</value>
        Public ReadOnly Property SelectedDataObjects As IList(Of ormDataObject)
            Get
                Dim alist As New List(Of ormDataObject)
                If Me.SelectionMode = GridViewSelectionMode.FullRowSelect Then
                    For Each aRow In Me.SelectedRows
                        Dim aDataObject As ormDataObject = Me.Modeltable.DataObject(aRow.Index)
                        If aDataObject IsNot Nothing Then alist.Add(aDataObject)
                    Next
                End If
                Return alist
            End Get

        End Property

        ''' <summary>
        ''' Gets or sets the status.
        ''' </summary>
        ''' <value>The status.</value>
        Public Property Status() As RadLabelElement
            Get
                Return Me._statuslabel
            End Get
            Set(value As RadLabelElement)
                Me._statuslabel = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the modeltable.
        ''' </summary>
        ''' <value>The modeltable.</value>
        Public Property Modeltable() As ormModelTable
            Get
                Return Me._modeltable
            End Get
            Set(value As ormModelTable)
                Me._modeltable = value
            End Set
        End Property
        ''' <summary>
        ''' Event On Row Adding
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnRowAdding(sender As Object, e As GridViewRowCancelEventArgs) Handles Me.UserAddingRow
            If sender.Equals(Me) Then
                If Not CurrentSession.RequestUserAccess(accessRequest:=otAccessRight.ReadUpdateData, loginOnFailed:=True, _
                                                        objecttransactions:={Me._modeltable.ObjectID & "." & ormDataObject.ConstOPCreate}) Then
                    AddStatusMessage("no right to add a object of type " & _modeltable.ObjectID)
                    e.Cancel = True
                End If
            End If
        End Sub
        ''' <summary>
        ''' Event On Row Adding
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnRowAdded(sender As Object, e As GridViewRowEventArgs) Handles Me.UserAddedRow
            If sender.Equals(Me) Then
                Debug.WriteLine(Me.Name & "OnRowAdded")
            End If

        End Sub

        ''' <summary>
        ''' Event On Cell Validating
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub UIControlDataGridView_OnCellValidating(sender As Object, e As CellValidatingEventArgs) Handles Me.CellValidating
            If sender.Equals(Me) Then
                If e.ColumnIndex > 0 Then
                    If (e.OldValue IsNot Nothing AndAlso e.Value IsNot Nothing AndAlso Not e.Value.Equals(e.OldValue)) _
                        OrElse (e.OldValue Is Nothing AndAlso e.Value IsNot Nothing) _
                        OrElse (e.OldValue IsNot Nothing AndAlso e.Value Is Nothing) Then


                        Debug.WriteLine(Me.Name & " OnCellValidating :" & _modeltable.Columns(e.ColumnIndex).ColumnName & " value:" & e.Value & " oldvalue:" & e.OldValue)
                        Dim anObjectEntry As iormObjectEntry = Me.Modeltable.GetObjectEntry(columnname:=e.Column.Name)
                        Dim result As otValidationResultType
                        Dim msglog As New ObjectMessageLog
                        ''' skip if not an objectentry
                        ''' 
                        If anObjectEntry Is Nothing Then Return
                        ''' get the object or create one
                        Dim anDataobject As iormPersistable
                        If e.RowIndex >= 0 Then
                            anDataobject = Me.Modeltable.DataObject(e.RowIndex)
                        Else
                            Dim anObjectDefinition As ObjectDefinition = CurrentSession.Objects.GetObject(objectid:=anObjectEntry.Objectname)
                            anDataobject = ot.CreateDataObjectInstance(type:=anObjectDefinition.ObjectType)
                        End If

                        If anDataobject Is Nothing Then
                            Me.AddStatusMessage("row not a dataobject -refresh view")
                            result = otValidationResultType.FailedNoProceed

                        Else
                            ''' 
                            ''' APPLY THE ENTRY PROPERTIES AND TRANSFORM THE VALUE REQUESTED
                            ''' 
                            Dim outvalue As Object = e.Value ' copy over
                            If Not TryCast(anDataobject, iormInfusable).Normalizevalue(anObjectEntry.Entryname, outvalue) Then
                                CoreMessageHandler(message:="Warning ! Could not normalize value", arg1:=outvalue, objectname:=anObjectEntry.Objectname, _
                                                    entryname:=anObjectEntry.Entryname, subname:="UIConTrolDataGridView.CellValidating")

                            End If
                            ''''
                            '''' validate the value
                            ''''
                            result = TryCast(anDataobject, iormValidatable).Validate(anObjectEntry.Entryname, outvalue, msglog)
                        End If

                        ''' result
                        If result = otValidationResultType.FailedNoProceed Then
                            e.Cancel = True ' to cancel
                            If msglog.Count > 0 Then AddStatusMessage(msglog.MessageText)
                            Me.CurrentCell.BorderBoxStyle = Telerik.WinControls.BorderBoxStyle.SingleBorder
                            Me.CurrentCell.BorderColor = Color.Red
                            Me.CurrentCell.BorderDashStyle = Drawing2D.DashStyle.Solid
                            Me.CurrentCell.BorderThickness = New Padding(0)
                            'Me.CurrentRow.ErrorText = msglog.MessageText

                        Else
                            Me.CurrentCell.BorderBoxStyle = Telerik.WinControls.BorderBoxStyle.SingleBorder
                            Me.CurrentCell.BorderColor = Color.LightGreen
                            Me.CurrentCell.BorderDashStyle = Drawing2D.DashStyle.Solid
                            Me.CurrentCell.BorderThickness = New Padding(1)
                        End If

                    End If

                End If

            End If

        End Sub
        ''' <summary>
        ''' Event On Control validated (post validation)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub UIControlDataGridView_OnCellValidated(sender As Object, e As CellValidatedEventArgs) Handles Me.CellValidated
            If sender.Equals(Me) Then
                ' Debug.WriteLine(Me.Name & " OnCellValidated :" & _modeltable.Columns(e.ColumnIndex).ColumnName & " value:" & e.Value)
            End If

        End Sub
        ''' <summary>
        ''' Event On Control validated
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub UIControlDataGridView_OnRowValidating(sender As Object, e As RowValidatingEventArgs) Handles Me.RowValidating
            If sender.Equals(Me) Then
                If e.Row IsNot Nothing AndAlso e.Row.IsModified Then
                    Debug.WriteLine(Me.Name & "OnRowValidating")
                    e.Cancel = False
                End If

            End If

        End Sub
        ''' <summary>
        ''' Event On row validated (post validation)
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub UIControlDataGridView_OnRowValidated(sender As Object, e As RowValidatedEventArgs) Handles Me.RowValidated
            If sender.Equals(Me) Then
                If e.Row IsNot Nothing AndAlso e.Row.IsModified Then
                    Debug.WriteLine(Me.Name & "OnRowValidated")
                End If

            End If

        End Sub
        ''' <summary>
        ''' Event On Row position changed
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnRowChanged(sender As Object, e As CurrentRowChangedEventArgs) Handles Me.CurrentRowChanged
            If sender.Equals(Me) Then
                If e.OldRow IsNot Nothing AndAlso e.OldRow.IsModified Then
                    'Debug.WriteLine(Me.Name & "OnRowChanged")
                End If
            End If

        End Sub
        ''' <summary>
        ''' Event On default values for a new row
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnDefaultValueNeeded(sender As Object, e As GridViewRowEventArgs) Handles Me.DefaultValuesNeeded
            For Each anEntry In _modeltable.GetObjectEntries
                ''' set the current domain in domain fields
                If anEntry.Entryname = Domain.ConstFNDomainID Then
                    If e.Row.Cells.Item(anEntry.Entryname) IsNot Nothing Then e.Row.Cells.Item(anEntry.Entryname).Value = CurrentSession.CurrentDomainID
                Else
                    If e.Row.Cells.Item(anEntry.Entryname) IsNot Nothing Then e.Row.Cells.Item(anEntry.Entryname).Value = anEntry.DefaultValue
                End If
            Next

        End Sub

        ''' <summary>
        ''' Add a Message to the connected status message
        ''' </summary>
        ''' <param name="message"></param>
        ''' <remarks></remarks>
        Private Sub AddStatusMessage(message As String)

            If _statuslabel Is Nothing Then Exit Sub

            _statuslabel.Text = Date.Now & " : " & message

            RaiseEvent OnStatusTextChanged(Me, New UIControlDataGridViewEventArgs(Date.Now & " : " & message))
            Me.Refresh()
        End Sub

        ''' <summary>
        ''' Create failed
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Modeltable_ObjectOpFailed(sender As Object, e As ormModelTable.EventArgs) Handles _modeltable.ObjectCreateFailed, _modeltable.ObjectDeleteFailed
            If Not String.IsNullOrWhiteSpace(e.Message) Then Me.AddStatusMessage(e.Message)
        End Sub
        ''' <summary>
        ''' Update failed
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub Modeltable_ObjectUpdateFailed(sender As Object, e As ormModelTable.EventArgs) Handles _modeltable.ObjectUpdateFailed, _modeltable.ObjectPersistFailed
            If e.Msglog.Count > 0 Then Me.AddStatusMessage(e.Msglog.MessageText)
        End Sub
        ''' <summary>
        ''' Event handler for operation messages
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnMessageFromTable(sender As Object, e As ormModelTable.EventArgs) Handles _modeltable.OperationMessage
            If _statuslabel IsNot Nothing AndAlso e.Message IsNot Nothing Then
                AddStatusMessage(e.Message)
            End If
        End Sub
        ''' <summary>
        ''' 
        ''' <summary>
        ''' Event On Row Deleting
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnRowDeleting(sender As Object, e As GridViewRowCancelEventArgs) Handles Me.UserDeletingRow
            If Not CurrentSession.RequestUserAccess(accessRequest:=otAccessRight.ReadUpdateData, loginOnFailed:=True, _
                                                       objecttransactions:={Me._modeltable.ObjectID & "." & ormDataObject.ConstOPDelete}) Then
                AddStatusMessage("no right to delete a object of type " & _modeltable.ObjectID)
                e.Cancel = True
            End If
        End Sub


        ''' <summary>
        ''' Handles the Initialize Event
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnInitialize(sender As Object, e As System.EventArgs) Handles Me.Initialized
            Me.EnableCustomFiltering = False
            Me.EnableCustomGrouping = False
            Me.EnableCustomSorting = False
            Me.EnableHotTracking = False

            Me.MasterTemplate.ShowHeaderCellButtons = True
            Me.MasterTemplate.ShowFilteringRow = False
            Me.MasterTemplate.EnableHierarchyFiltering = True
            Me.MasterTemplate.EnableCustomFiltering = False


            Me.EnableFiltering = True
            Me.MasterTemplate.EnableFiltering = True
            Me.EnableGrouping = False
            Me.EnableSorting = True


        End Sub
        ''' <summary>
        ''' Handles the OnContextMenuOpening Event to add or delete context sensitive Events
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub UIControlDataGridView_OnContextMenuOpening(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ContextMenuOpeningEventArgs) Handles MyBase.ContextMenuOpening

            ''' Add the Layout Save if we are connected
            ''' 
            If CurrentSession.IsRunning Then

                ''' *** save the Format of the data grid
                ''' 
                If e.ContextMenu.Items.Where(Function(x) x.Name = "Save UI Layout").FirstOrDefault Is Nothing Then
                    Dim separator As RadMenuSeparatorItem = New RadMenuSeparatorItem()
                    e.ContextMenu.Items.Add(separator)

                    Dim menuItemCS As New RadMenuItem("Adjust Current Column Size")
                    AddHandler menuItemCS.Click, AddressOf OnMenuItemClick_AdjustColumnSize
                    e.ContextMenu.Items.Add(menuItemCS)


                    Dim menuItemCSa As New RadMenuItem("Adjust All Columns Size")
                    AddHandler menuItemCSa.Click, AddressOf OnMenuItemClick_AdjustAllColumnSize
                    e.ContextMenu.Items.Add(menuItemCSa)

                    Dim menuItem1 As New RadMenuItem("Save UI Layout")
                    menuItem1.ForeColor = Color.Red
                    AddHandler menuItem1.Click, AddressOf OnMenuItemClick_SaveUILayout
                    e.ContextMenu.Items.Add(menuItem1)

                    Dim menuItem2 As New RadMenuItem("Load UI Layout")
                    AddHandler menuItem2.Click, AddressOf OnMenuItemClick_LoadUILayout
                    e.ContextMenu.Items.Add(menuItem2)
                End If

            Else
                Dim element1 = e.ContextMenu.Items.Select(Function(x) x.Text = "Save UI Layout")
                If element1 IsNot Nothing Then
                    TryCast(element1, RadMenuItem).Enabled = False
                End If
                Dim element2 = e.ContextMenu.Items.Select(Function(x) x.Text = "Load UI Layout")
                If element2 IsNot Nothing Then
                    TryCast(element2, RadMenuItem).Enabled = False
                End If
            End If
        End Sub


        ''' <summary>
        ''' MenuItemClick Event handler
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnMenuItemClick_AdjustAllColumnSize(sender As Object, e As System.EventArgs)
            Me.MasterGridViewTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.None
            Me.MasterGridViewTemplate.BestFitColumns(BestFitColumnMode.AllCells)
        End Sub

        ''' <summary>
        ''' MenuItemClick Event handler
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnMenuItemClick_AdjustColumnSize(sender As Object, e As System.EventArgs)
            Me.CurrentColumn.BestFit()
        End Sub

        ''' <summary>
        ''' MenuItemClick Event handler
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnMenuItemClick_SaveUILayout(sender As Object, e As System.EventArgs)
            Call StoreGridViewLayout()
        End Sub

        ''' <summary>
        ''' MenuItemClick Event handler
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnMenuItemClick_LoadUILayout(sender As Object, e As System.EventArgs)
            Call RetrieveGridViewLayout()
        End Sub
        ''' <summary>
        ''' Save the Layout Status
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub StoreGridViewLayout()
            Dim aXML As Xml.XmlWriter
            Dim aString As New System.Text.StringBuilder
            Try
                Me.XmlSerializationInfo.DisregardOriginalSerializationVisibility = True
                Me.XmlSerializationInfo.SerializationMetadata.Clear()
                Me.XmlSerializationInfo.SerializationMetadata.Add(GetType(RadGridView), "MasterTemplate", DesignerSerializationVisibilityAttribute.Content)
                Me.XmlSerializationInfo.SerializationMetadata.Add(GetType(GridViewTemplate), "Columns", DesignerSerializationVisibilityAttribute.Content)
                Me.XmlSerializationInfo.SerializationMetadata.Add(GetType(GridViewDataColumn), "UniqueName", DesignerSerializationVisibilityAttribute.Visible)
                Me.XmlSerializationInfo.SerializationMetadata.Add(GetType(GridViewDataColumn), "Width", DesignerSerializationVisibilityAttribute.Visible)

                aXML = Xml.XmlWriter.Create(aString)
                MyBase.SaveLayout(aXML)
                aXML.Close()

                If CurrentSession.IsRunning Then
                    If CurrentSession.RequireAccessRight(otAccessRight.ReadUpdateData) Then
                        CurrentSession.CurrentDomain.SetSetting(id:="UI." & Me.Name & "." & _modeltable.Id, datatype:=otDataType.Text, _
                                                                description:="Setting for the UI ControlDataGridView Element", value:=aString.ToString)
                        If CurrentSession.CurrentDomain.Persist() Then
                            AddStatusMessage("layout format saved in database domain (" & CurrentSession.CurrentDomainID & ") setting")
                        Else
                            AddStatusMessage("unable to save layout format - see session log")
                        End If
                    Else
                        AddStatusMessage("unable to save layout format - OnTrack has not granted update rights")
                    End If
                Else
                    AddStatusMessage("unable to save layout format - OnTrack session not running")
                End If
            Catch ex As Exception
                CoreMessageHandler(exception:=ex, subname:="UIControlDataGridView.StoreGridViewLayout")
            End Try

        End Sub
        ''' <summary>
        ''' Save the Layout Status
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub RetrieveGridViewLayout()
            Dim aString As String
            Dim aXML As Xml.XmlReader

            Try

                If CurrentSession.IsRunning Then
                    Dim aDomainSetting As DomainSetting = CurrentSession.CurrentDomain.GetSetting(id:="UI." & Me.Name & "." & _modeltable.Id)
                    If aDomainSetting IsNot Nothing Then
                        aString = aDomainSetting.value.ToString
                        aXML = Xml.XmlReader.Create(New IO.StringReader(aString))
                        Me.LoadLayout(aXML)
                        aXML.Close()
                        AddStatusMessage("layout loaded from domain setting")
                    Else
                        AddStatusMessage("no layout defined in domain setting")
                    End If

                Else
                    AddStatusMessage("unable to load layout format - OnTrack session not running")
                End If

            Catch ex As Exception
                CoreMessageHandler(exception:=ex, subname:="UIControlDataGridView.RestoreGridViewLayout")
            End Try


        End Sub
        ''' <summary>
        ''' Handles the DataBinding Complete Event to setup different Models
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub UIConTrolDataGrid_OnDatabindingComplete(sender As Object, e As GridViewBindingCompleteEventArgs) Handles Me.DataBindingComplete
            If Me.DataSource IsNot Nothing Then
                If Me.DataSource.GetType.Equals(GetType(ormModelTable)) Then
                    Me.ProgressPictureBox.Enabled = True

                    Me.Refresh()

                    _modeltable = TryCast(Me.DataSource, ormModelTable)
                    Dim awatch As New Stopwatch
                    awatch.Start()
                    _modeltable.Load()
                    awatch.Stop()

                    Me.Controls.Clear()
                    Me.Refresh()

                    For Each aColumn In Me.Columns.ToList
                        Dim anEntry As iormObjectEntry = _modeltable.GetObjectEntry(aColumn.FieldName)
                        If anEntry IsNot Nothing Then
                            ''' hack: if enrypted then show masked
                            If anEntry.Properties.Where(Function(x) x.Enum = otObjectEntryProperty.Encrypted).FirstOrDefault IsNot Nothing Then
                                _pwfieldname = anEntry.Entryname
                            End If
                        End If
                    Next

                    '' try to load the UI Layout
                    Call RetrieveGridViewLayout()
              
                    '' switch off editing of primary keys
                    For Each aRow In Me.Rows
                        For Each aCell As GridViewCellInfo In aRow.Cells
                            Dim anEntry As iormObjectEntry = _modeltable.GetObjectEntry(aCell.ColumnInfo.FieldName)
                            If anEntry IsNot Nothing Then
                                Dim anobjectdefinition As ObjectDefinition = anEntry.GetObjectDefinition
                                Dim aKeyNameList = anobjectdefinition.GetKeyNames

                                If aKeyNameList.Contains(anEntry.Entryname) Then
                                    aCell.ReadOnly = True
                                End If

                            End If
                        Next
                    Next

                    '' status label
                    If _statuslabel IsNot Nothing Then
                        AddStatusMessage("finished operation in " & awatch.ElapsedMilliseconds & "ms  and " & _modeltable.Rows.Count & " rows loaded")
                    End If

                    ''' invisible reference to modeltable objects
                    If Me.Columns.Contains(ormModelTable.constQRYRowReference) Then
                        Me.Columns(ormModelTable.constQRYRowReference).IsVisible = False
                    End If
                End If
            End If
        End Sub

        ''' <summary>
        ''' Cell Editor initialized
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub radGridView_CellEditorInitialized(sender As Object, e As GridViewCellEventArgs) Handles Me.CellEditorInitialized
            Dim dataColumn As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)

            If dataColumn IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(_pwfieldname) AndAlso dataColumn.FieldName = _pwfieldname Then
                Dim textBoxEditor As RadTextBoxEditor = TryCast(Me.ActiveEditor, RadTextBoxEditor)

                If textBoxEditor IsNot Nothing Then
                    Dim editorElement As RadTextBoxEditorElement = TryCast(textBoxEditor.EditorElement, RadTextBoxEditorElement)
                    editorElement.PasswordChar = "#"c
                End If
            End If
        End Sub
        ''' <summary>
        ''' CellFormatting Handler
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub radGridView_CellFormatting(sender As Object, e As CellFormattingEventArgs) Handles Me.CellFormatting
            Dim dataColumn As GridViewDataColumn = TryCast(e.CellElement.ColumnInfo, GridViewDataColumn)

            If dataColumn IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(_pwfieldname) AndAlso dataColumn.FieldName = _pwfieldname Then
                If e.CellElement.RowInfo.Cells(dataColumn.Name) IsNot Nothing Then
                    Dim value As Object = e.CellElement.RowInfo.Cells(dataColumn.Name).Value
                    Dim text As String = [String].Empty
                    If value IsNot Nothing Then
                        Dim passwordLen As Integer = Convert.ToString(value).Length
                        text = [String].Join("#", New String(passwordLen - 1) {})
                    End If

                    e.CellElement.Text = text
                End If

            End If
        End Sub
        ''' <summary>
        ''' EdtorRequired Event Handler
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub UICOntrolDataGridView_EditorRequired(sender As Object, e As EditorRequiredEventArgs) Handles Me.EditorRequired

            Dim anObjectEntry = Me.Modeltable.GetObjectEntry(Me.CurrentColumn.FieldName)
            'Dim [date] As DateTime
            'If DateTime.TryParse(Me.CurrentCell.Value.ToString(), [date]) Then
            '    e.EditorType = GetType(RadDateTimeEditor)
            '    Return
            'End If

            'Dim i As Integer = 0
            'If Integer.TryParse(Me.CurrentCell.Value.ToString(), i) Then
            '    e.EditorType = GetType(GridSpinEditor)
            '    Return
            'End If

            'If TypeOf Me.CurrentCell.Value Is String Then
            '    e.EditorType = GetType(RadTextBoxEditor)
            '    Return
            'End If
        End Sub
        ''' <summary>
        ''' event handler to check on Right to Change Object
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub UIControlDataGridView_ValueChanging(sender As Object, e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles Me.CellBeginEdit

            If Not CurrentSession.RequestUserAccess(accessRequest:=otAccessRight.ReadUpdateData, loginOnFailed:=True, _
                                                      objecttransactions:={Me._modeltable.ObjectID & "." & ormDataObject.ConstOPPersist}) Then
                AddStatusMessage("no right to change a object of type " & _modeltable.ObjectID)
                e.Cancel = True
            End If
        End Sub


    End Class
End Namespace

