﻿Imports OnTrack.UI
Imports Telerik.WinControls.UI
Imports System.Drawing
Imports System.Windows.Forms
Imports OnTrack.Database
Imports OnTrack.Commons

Namespace Global.OnTrack.UI
    Public Class UIControlDataGridView
        Inherits RadGridView

        Private WithEvents _modeltable As ormModelTable
        Private WithEvents _statuslabel As RadLabelElement
        Private WithEvents _ProgressPictureBox As System.Windows.Forms.PictureBox
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
                        alist.Add(Me.Modeltable.DataObject(aRow.Index))
                    Next
                End If
                Return aList
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

        Private Sub UIControlDataGridView_SelectionChanged(sender As Object, e As System.EventArgs) Handles Me.SelectionChanged

        End Sub

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
        ''' Event On Cell Changed from current to new cell
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub OnCellChanged(sender As Object, e As CurrentCellChangedEventArgs) Handles Me.CurrentCellChanged
            If sender.Equals(Me) Then

                'Debug.WriteLine(Me.Name & "OnCellChanged")
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
                        Dim anObjectEntry As iormObjectEntry = _modeltable.GetObjectEntry(columnname:=e.Column.Name)
                        Dim aLog As New ObjectMessageLog
                        Dim result As otValidationResultType = Global.OnTrack.Database.ObjectValidator.Validate(anObjectEntry, e.Value, aLog)
                        If result = otValidationResultType.FailedNoProceed Then e.Cancel = True ' to cancel

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
            Me.Refresh()
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
        Public Sub OnInitialize(sender As Object, e As EventArgs) Handles Me.Initialized
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
                Dim separator As RadMenuSeparatorItem = New RadMenuSeparatorItem()
                e.ContextMenu.Items.Add(separator)
                Dim menuItem1 As New RadMenuItem("Save UI Layout")
                menuItem1.ForeColor = Color.Red

                ' add to existing menu
                AddHandler menuItem1.Click, AddressOf SaveUILayout
                e.ContextMenu.Items.Add(menuItem1)
                Dim menuItem2 As New RadMenuItem("Load UI Layout")
                ' add to existing menu
                AddHandler menuItem2.Click, AddressOf LoadUILayout
                e.ContextMenu.Items.Add(menuItem2)
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
        ''' Save the Layout Status
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub SaveUILayout(sender As Object, e As EventArgs)
            Dim aXML As Xml.XmlWriter
            Dim aString As New System.Text.StringBuilder

            aXML = Xml.XmlWriter.Create(aString)
            Me.SaveLayout(aXML)
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
        End Sub
        ''' <summary>
        ''' Save the Layout Status
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Public Sub LoadUILayout(sender As Object, e As EventArgs)
            Dim aString As String
            Dim aXML As Xml.XmlReader

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

                    '' try to load the UI Layout
                    Call LoadUILayout(Me, New EventArgs())
                    '' status label
                    If _statuslabel IsNot Nothing Then
                        AddStatusMessage("finished operation in " & awatch.ElapsedMilliseconds & "ms  and " & _modeltable.Rows.Count & " rows loaded")
                    End If
                    If Me.Columns.Contains(ormModelTable.constQRYRowReference) Then
                        Me.Columns(ormModelTable.constQRYRowReference).IsVisible = False
                    End If
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

