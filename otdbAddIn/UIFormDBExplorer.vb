Imports Telerik.WinControls.UI
Imports System.Drawing
Imports System.Windows.Forms
Imports OnTrack.UI
Imports OnTrack.database


''' <summary>
''' Object Explorer - explors the Object Data and Structure and its setting in the OnTrack Enviormennt
''' </summary>
''' <remarks></remarks>
Public Class UIFormDBExplorer

    ''' <summary>
    ''' ModelClass for the TreeView
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ObjectStructureItem


        Public Enum type
            CacheManager
            Database = 1
            Table
            ObjectRepository
            [Module]
            [Object]
            ObjectEntry
            DbParameter
        End Enum

        Private _ID As String = ""
        Private _Nodetype As [type] = type.Module
        Private _Description As String = ""

        Private _Members As New List(Of ObjectStructureItem)
        Private _DataItem As Object

        ''' <summary>
        ''' Gets or sets the members.
        ''' </summary>
        ''' <value>The members.</value>
        Public Property Members() As List(Of ObjectStructureItem)
            Get
                Return Me._Members
            End Get
            Set(value As List(Of ObjectStructureItem))
                Me._Members = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the description.
        ''' </summary>
        ''' <value>The description.</value>
        Public Property Description() As String
            Get
                Return Me._Description
            End Get
            Set(value As String)
                Me._Description = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the nodetype.
        ''' </summary>
        ''' <value>The nodetype.</value>
        Public Property Nodetype() As type
            Get
                Return Me._Nodetype
            End Get
            Set(value As type)
                Me._Nodetype = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the ID.
        ''' </summary>
        ''' <value>The ID.</value>
        Public Property ID() As String
            Get
                Return Me._ID
            End Get
            Set(value As String)
                Me._ID = value
            End Set
        End Property
        ''' <summary>
        ''' Gets or sets the data item.
        ''' </summary>
        ''' <value>The data item.</value>
        Public Property DataItem() As Object
            Get
                Return Me._DataItem
            End Get
            Set(value As Object)
                Me._DataItem = Value
            End Set
        End Property

    End Class

    Private _topItems As New List(Of ObjectStructureItem)
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' nothing on the shadow

    End Sub

    ''' <summary>
    ''' Build the Object Tree
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub BuildTree()
        '''
        ''' build the cached Entries
        Dim cacheItem As New ObjectStructureItem With {.ID = "Cache Manager", .Description = "Cache", .Nodetype = ObjectStructureItem.type.CacheManager}
        _topItems.Add(cacheItem)

        ' Add Objects
        Dim repositoryCacheItem As New ObjectStructureItem With {.ID = "ObjectCache", .Description = "Cached Objects", .Nodetype = ObjectStructureItem.type.ObjectRepository}
        cacheItem.Members.Add(repositoryCacheItem)
        For Each anObjectDefinition In CurrentSession.Objects.ObjectDefinitions
            Dim newItem As New ObjectStructureItem With {.ID = anObjectDefinition.ID, .Description = anObjectDefinition.Description, .Nodetype = ObjectStructureItem.type.Object, _
                                                         .DataItem = anObjectDefinition}
            If anObjectDefinition.UseCache Then repositoryCacheItem.Members.Add(newItem)
        Next
        ' Add Tables
        Dim dbCacheItem As New ObjectStructureItem With {.ID = "TableCache", .Description = "Cached Tables", .Nodetype = ObjectStructureItem.type.Database}
        cacheItem.Members.Add(dbCacheItem)
        For Each aTable In CurrentSession.Objects.TableDefinitions
            Dim newItem As New ObjectStructureItem With {.ID = aTable.Name, .Description = aTable.Description, .Nodetype = ObjectStructureItem.type.Table, .DataItem = aTable}
            If aTable.UseCache Then dbCacheItem.Members.Add(newItem)
        Next

        '''
        ''' build the Database and Table entries
        Dim databaseItem As New ObjectStructureItem With {.ID = CurrentConfigSetName, .Description = "Database", .Nodetype = ObjectStructureItem.type.Database}
        _topItems.Add(databaseItem)
        Dim dbparameterITem As New ObjectStructureItem With {.ID = "Parameters", .Description = "Db parameters", .Nodetype = ObjectStructureItem.type.DbParameter}
        databaseItem.Members.Add(dbparameterITem)
        For Each aTable In TableDefinition.All
            Dim newItem As New ObjectStructureItem With {.ID = aTable.Name, .Description = aTable.Description, .Nodetype = ObjectStructureItem.type.Table, .DataItem = aTable}
            databaseItem.Members.Add(newItem)
        Next

        ''' 
        ''' build the Business Objects and Modules entries
        Dim repositoryItem As New ObjectStructureItem With {.ID = "Objects Dictionary", .Description = "all object instances per object definition", .Nodetype = ObjectStructureItem.type.ObjectRepository}
        _topItems.Add(repositoryItem)
        For Each aName In ot.InstalledModules
            Dim ModuleItem As New ObjectStructureItem With {.ID = aName, .Description = "Module", .Nodetype = ObjectStructureItem.type.Module}
            repositoryItem.Members.Add(ModuleItem)

            For Each aDescription In ot.GetObjectClassDescriptionsForModule(modulename:=aName)

                Dim anObjectDefinition = CurrentSession.Objects.GetObject(objectid:=aDescription.ID)
                If anObjectDefinition Is Nothing Then
                    CoreMessageHandler(message:="Object definition could not be retrieved", objectname:=aDescription.ID, arg1:=aName, subname:="UIFormDBExplorer.BuildTree", messagetype:=otCoreMessageType.InternalError)
                End If
                Dim anObjectItem As New ObjectStructureItem With {.ID = aDescription.ObjectAttribute.ID, .Nodetype = ObjectStructureItem.type.Object, _
                                                                  .DataItem = anObjectDefinition}
                With anObjectItem
                    If aDescription.ObjectAttribute.HasValueDescription Then .Description = aDescription.ObjectAttribute.Description
                End With

                ModuleItem.Members.Add(anObjectItem)
            Next
        Next
    End Sub
    ''' <summary>
    ''' OnLoad Event Handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Overloads Sub OnLoad(sender As Object, e As EventArgs) Handles Me.Load

        If ot.RequireAccess(accessRequest:=otAccessRight.ReadOnly) Then
            BuildTree()

            With ObjectTree
                .DataSource = _topItems
                .DisplayMember = "ID\ID\ID\ID"
                .ExpandAll()
                .ChildMember = "ObjectStructureItem\Members\Members\Members"
                .Enabled = True

            End With
        Else
            ObjectTree.Text = "Unable to resolve database information"
        End If

    End Sub

    ''' <summary>
    ''' Handler for Screen Tip
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ObjecTree_ScreenTipNeeded(ByVal sender As Object, ByVal e As Telerik.WinControls.ScreenTipNeededEventArgs) Handles ObjectTree.ScreenTipNeeded
        Dim node As TreeNodeElement = TryCast(e.Item, TreeNodeElement)
        Dim screentip As New RadOffice2007ScreenTipElement
        Dim size As New Size(120, 70)
        Dim pad As New Padding(2)

        If node IsNot Nothing Then
            'screentip.MainTextLabel.Image = node.ImageElement.Image
            'screentip.MainTextLabel.TextImageRelation = TextImageRelation.ImageBeforeText
            'screentip.MainTextLabel.Padding = pad
            Dim objectEntryItem = TryCast(node.Data.DataBoundItem, ObjectStructureItem)

            screentip.MainTextLabel.Text = "Object Element:" & objectEntryItem.ID.ToString
            screentip.MainTextLabel.Margin = New System.Windows.Forms.Padding(10)
            screentip.CaptionLabel.Padding = pad
            screentip.CaptionLabel.Text = objectEntryItem.Description

            screentip.EnableCustomSize = False
            screentip.AutoSize = True
            screentip.Size = size
            node.ScreenTip = screentip
        End If
    End Sub

    '#Region dataBoundItem
    Private Sub ObjectTree_SelectedNodeChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.RadTreeViewEventArgs) Handles ObjectTree.SelectedNodeChanged
        Dim nodeitem As ObjectStructureItem = TryCast(e.Node.DataBoundItem, ObjectStructureItem)
        If nodeitem IsNot Nothing Then
            Debug.WriteLine("Product: " & e.Node.Text & ", Item ID: " & nodeitem.ID)
            Select Case nodeitem.Nodetype

                Case ObjectStructureItem.type.Object
                    Me.PageData.Enabled = True
                    Me.PageObjectProperties.Enabled = True
                    Me.PageData.Item.Visibility = Telerik.WinControls.ElementVisibility.Visible
                    Me.PageObjectProperties.Item.Visibility = Telerik.WinControls.ElementVisibility.Visible
                    Dim aObjectdefinition As ObjectDefinition = TryCast(nodeitem.DataItem, ObjectDefinition)
                    Dim aqry As Global.OnTrack.database.iormQueriedEnumeration = aObjectdefinition.GetQuery(ormDataObject.constQRYAll)
                    Dim aModeltable As ormModelTable = New ormModelTable(aqry)
                  
                    With TryCast(Me.DataGrid, UIControlDataGridView)
                        .DataSource = aModeltable
                        .ThemeName = "TelerikMetroBlue"
                        .AutoSizeColumnsMode = True
                        .Status = Me.StatusLabel
                    End With
                Case Else
                    Me.PageData.Enabled = False
                    Me.PageData.Item.Visibility = Telerik.WinControls.ElementVisibility.Hidden
                    Me.PageObjectProperties.Enabled = False
                    Me.PageObjectProperties.Item.Visibility = Telerik.WinControls.ElementVisibility.Hidden
            End Select
            Me.Refresh()
        End If
    End Sub
    '#endregion


    ''' <summary>
    ''' NodeFormatting
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub ObjectTree_NodeFormatting(ByVal sender As Object, ByVal e As TreeNodeFormattingEventArgs) Handles ObjectTree.NodeFormatting
        Dim nodeitem As ObjectStructureItem = TryCast(e.Node.DataBoundItem, ObjectStructureItem)

        If nodeitem IsNot Nothing Then
            ''' format the icon
            ''' 
            Select Case nodeitem.Nodetype
                Case ObjectStructureItem.type.CacheManager
                    e.NodeElement.ImageElement.Image = New Bitmap(My.Resources.library, New Size(24, 24))
                    '''
                    e.NodeElement.ItemHeight = 30
                    e.NodeElement.Font = New Font(e.NodeElement.Font, FontStyle.Bold)
                Case ObjectStructureItem.type.Database
                    e.NodeElement.ImageElement.Image = New Bitmap(My.Resources.db, New Size(24, 24))
                    '''
                    e.NodeElement.ItemHeight = 30
                    e.NodeElement.Font = New Font(e.NodeElement.Font, FontStyle.Bold)

                Case ObjectStructureItem.type.Table
                    e.NodeElement.ImageElement.Image = New Bitmap(My.Resources.table, New Size(12, 12))
                    '''
                    e.NodeElement.ItemHeight = 15
                    e.NodeElement.Font = New Font(e.NodeElement.Font, FontStyle.Regular)

                Case ObjectStructureItem.type.ObjectRepository
                    e.NodeElement.ImageElement.Image = New Bitmap(My.Resources.business, New Size(24, 24))
                    '''
                    e.NodeElement.ItemHeight = 30
                    e.NodeElement.Font = New Font(e.NodeElement.Font, FontStyle.Bold)

                Case ObjectStructureItem.type.Module
                    e.NodeElement.ImageElement.Image = New Bitmap(My.Resources.library, New Size(24, 24))
                    '''
                    e.NodeElement.ItemHeight = 30
                    e.NodeElement.Font = New Font(e.NodeElement.Font, FontStyle.Bold)

                Case ObjectStructureItem.type.Object
                    e.NodeElement.ImageElement.Image = New Bitmap(My.Resources.business_contact, New Size(16, 16))
                    '''
                    e.NodeElement.ItemHeight = 18
                    e.NodeElement.Font = New Font(e.NodeElement.Font, FontStyle.Regular)
                Case ObjectStructureItem.type.DbParameter
                    e.NodeElement.ImageElement.Image = New Bitmap(My.Resources.list_bullets, New Size(12, 12))
                    '''
                    e.NodeElement.ItemHeight = 15
                    e.NodeElement.Font = New Font(e.NodeElement.Font, FontStyle.Regular)
                Case ObjectStructureItem.type.ObjectEntry
                Case Else

            End Select


        End If
    End Sub


    Private Sub RadCloseButton_Click(sender As Object, e As EventArgs) Handles RadCloseButton.Click
        Me.Close()
    End Sub
End Class
