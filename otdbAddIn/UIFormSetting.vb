Imports System.ComponentModel
Imports OnTrack.Database
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.Data
Imports OnTrack
Imports OnTrack.AddIn

Imports System
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.Windows.Forms

Public Class UIFormSetting
    Private Const constConfigurationName As String = "1ConfigurationName"
    Private Const constConfigDescription As String = "2description"
    Private Const constConfigFileName As String = "3ConfigFileName"
    Private Const constConfigFileLocation As String = "4ConfigFileLocation"

    Private WithEvents _propertyStore As RadPropertyStore
    Private isChanged As Boolean = False
    Public Delegate Function SetHostProperty(ByVal name As String, _
                                          ByVal value As Object, _
                                          ByRef host As Object, _
                                          silent As Boolean) As Boolean
    Private SetHostPropertyDelegate As SetHostProperty
    Private ConfigSetnames As New Dictionary(Of String, ConfigSetModel)


    ''' <summary>
    ''' ModelConverter for the ConfigSet 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ConfigSetModelConverter
        Inherits ExpandableObjectConverter

        Public Overrides Function ConvertTo(context As ITypeDescriptorContext, culture As Globalization.CultureInfo, value As Object, destinationType As Type) As Object
            Return MyBase.ConvertTo(context, culture, value, destinationType)
            If (destinationType = GetType(String)) And TypeOf value Is ConfigSetModel Then
                Dim aValue As ConfigSetModel = TryCast(value, ConfigSetModel)
                If aValue IsNot Nothing Then
                    Return String.Format("[{0}]:{1}", aValue.Database, aValue.DBPath)
                Else
                    Return ""
                End If
            Else
                Return MyBase.ConvertTo(context, culture, value, destinationType)
            End If
        End Function

        Public Overrides Function CanConvertFrom(context As ITypeDescriptorContext, sourceType As Type) As Boolean

            Return False

            If sourceType = GetType(String) Then
                Return True
            End If
            Return MyBase.CanConvertFrom(context, sourceType)
        End Function

        Public Overrides Function ConvertFrom(context As ITypeDescriptorContext, culture As Globalization.CultureInfo, value As Object) As Object
            If TypeOf value Is String Then
                Debug.Assert(False)

            End If

            Return MyBase.ConvertFrom(context, culture, value)

        End Function
        Public Overrides Function GetProperties(context As ITypeDescriptorContext, value As Object, attributes() As Attribute) As PropertyDescriptorCollection
            'Return MyBase.GetProperties(context, value, attributes)
            'Return TypeDescriptor.GetProperties(GetType(ConfigSetModel), attributes).Sort({"Name", "DBType", "ConfigSetname", "Sequence", "Path", "DbUser", "DbPassword"})
            Dim aList As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(ConfigSetModel), attributes).Sort()
            Return aList

        End Function

        Public Overrides Function GetPropertiesSupported(context As ITypeDescriptorContext) As Boolean
            Return True
        End Function
    End Class

    ''' <summary>
    ''' ConfigSetModel for nested Properties per ConfigSetModel
    ''' </summary>
    ''' <remarks></remarks>
    <TypeConverter(GetType(ConfigSetModelConverter))> Public Class ConfigSetModel


        Private _configsetName As String = ""
        Private _type As Database.otDBServerType = 0
        Private _name As String = ""
        Private _path As String = ""
        Private _dbuser As String = ""
        Private _dbpassword As String = ""
        Private _sequence As ComplexPropertyStore.Sequence = ComplexPropertyStore.Sequence.primary
        Private _description As String = ""
        Private _driver As Database.otDbDriverType = 0
        Private _ConnectionString As String = ""
        Private _logagent As Boolean = False

        ''' <summary>
        ''' Gets or sets the logagent.
        ''' </summary>
        ''' <value>The logagent.</value>
        <DisplayName("Start LogAgent")> _
        <Category("Database")> <Browsable(True)> _
     <Description("start loggin agent to submit log messages asynchronous to database")> _
        Public Property Logagent() As Boolean
            Get
                Return Me._logagent
            End Get
            Set(value As Boolean)
                Me._logagent = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the connection string.
        ''' </summary>
        ''' <value>The connection string.</value>

        <DisplayName("Connection String")> _
        <Category("Database")> <Browsable(True)> _
     <Description("connection string to be used to connect to database - will be build if empty")> _
        Public Property ConnectionString() As String
            Get
                Return Me._ConnectionString
            End Get
            Set(value As String)
                Me._ConnectionString = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the driver.
        ''' </summary>
        ''' <value>The driver.</value>

        <DisplayName("Database Driver Name")> _
        <Category("Database")> <Browsable(True)> _
     <Description("name of the internal driver")> _
        Public Property DatabaseDriver() As otDbDriverType
            Get
                Return Me._driver
            End Get
            Set(value As otDbDriverType)
                Me._driver = value
            End Set
        End Property

        <DisplayName("Config Set Name")> _
        <Category("Database")> <Browsable(True)> _
     <Description("name of the configuration set")> _
        Public Property ConfigSetname As String
            Set(value As String)
                If value Is Nothing Then value = ""
                _configsetName = value
            End Set
            Get
                Return _configsetName
            End Get
        End Property
        <DisplayName("Config Set Description")> _
 <Category("Database")> <Browsable(True)> _
 <Description("description of the configuration set")> _
        Public Property ConfigSetNDescription As String
            Set(value As String)
                If value Is Nothing Then value = ""
                _description = value
            End Set
            Get
                Return _description
            End Get
        End Property
        <DisplayName("Config Set Sequence")> _
        <Category("Database")> <Browsable(True)> _
        <Description("sequence of the configuration")> _
        Public Property Sequence As ComplexPropertyStore.Sequence
            Set(value As ComplexPropertyStore.Sequence)
                _sequence = value
            End Set
            Get
                Return _sequence
            End Get
        End Property

        <DisplayName("Database Type")> _
       <Category("Database")> _
       <Browsable(True)> _
       <Description("type of the database")> _
        Public Property DatabaseType As Database.otDBServerType
            Set(value As Database.otDBServerType)
                'If value Is Nothing Then value = 0
                _type = value
            End Set
            Get
                Return _type
            End Get
        End Property

        <DisplayName("Database Name")> _
        <Category("Database")> <Browsable(True)> _
        <Description("name of the database in connection string")> _
        Public Property Database As String
            Set(value As String)
                If value Is Nothing Then value = ""
                _name = value
            End Set
            Get
                Return _name
            End Get
        End Property


        <DisplayName("Database Path")> _
       <Category("Database")> <Browsable(True)> _
       <Description("name of the database path or host address in connection string")> _
        Public Property DBPath As String
            Set(value As String)
                If value Is Nothing Then value = ""
                _path = value
            End Set
            Get
                Return _path
            End Get
        End Property
        <DisplayName("Db User")> _
        <Category("Database")> <Browsable(True)> _
               <Description("name of the database user in connection string")> _
        Public Property DbUser As String
            Set(value As String)
                If value Is Nothing Then value = ""
                _dbuser = value
            End Set
            Get
                Return _dbuser
            End Get
        End Property
        <DisplayName("Db User Password")> _
        <Category("Database")> _
        <Browsable(True)> _
        <PasswordPropertyText(True)> _
        <Description("password of the database user in connection string")> _
        Public Property DbUserPassword As String
            Set(value As String)
                If value Is Nothing Then value = ""
                _dbpassword = value
            End Set
            Get
                Return _dbpassword
            End Get
        End Property

        Public Overrides Function toString() As String
            Return String.Format("{0}:({1}/{2},{3},{4},{5})", _configsetName, _type.ToString, _name, _path, _dbuser, _configsetName, _sequence.ToString)
        End Function
    End Class

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()


    End Sub
    ''' <summary>
    ''' Register the SetHostPropertyFunction
    ''' </summary>
    ''' <param name="delegate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function RegisterSetHost(ByVal [delegate] As SetHostProperty) As Boolean
        Me.SetHostPropertyDelegate = [delegate]
    End Function

    ''' <summary>
    ''' Change Handler for the Properties
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub OnChange(sender As Object, e As PropertyStoreItemValueChangedEventArgs) Handles _propertyStore.ItemValueChanged
        isChanged = True

        If e.Item.PropertyName = constConfigFileLocation Then
            '** TODO: change the name of the SetModel
            Dim path As String = CStr(e.Item.Value)
            Dim configfilename As String = _propertyStore.Item(constConfigFileName).Value.ToString
            Dim changed As Boolean = False

            '** path has a filename
            If IO.File.Exists(path) Then
                configfilename = IO.Path.GetFileName(path)
                changed = True
            ElseIf Not IO.Directory.Exists(path) Then
                Me.StatusLabel.Text = "no directory exists"
                Exit Sub
            End If

            If Mid(path, Len(path), 1) <> "\" Then path = path & "\"
            If File.Exists(path & configfilename) Then
                'reload
                ot.AddConfigFilePath(path)
                If changed Then
                    ot.CurrentConfigFileName = configfilename
                End If

                '* reinitialize OTDB
                ot.Initialize(force:=True)
                Me.UpdatePropertyStore(_propertyStore)
                CoreMessageHandler(message:="configuration file in " & path & configfilename & " found and added to OnTrack Configuration", _
                                   subname:="UIFormSetting.PropertyStore.ItemValueChanged", messagetype:=otCoreMessageType.ApplicationInfo)
                Me.StatusLabel.Text = "configuration file found and added to OnTrack Configuration"
                Me.Refresh()
            Else
                Me.StatusLabel.Text = "no configuration file found "
            End If
        ElseIf e.Item.PropertyName = constConfigFileName Then
            '** TODO: change the name of the SetModel
            Dim configfilename As String = CStr(e.Item.Value)
            Dim path As String = _propertyStore.Item(constConfigFileLocation).Value.ToString
            If Mid(path, Len(path), 1) <> "\" Then path = path & "\"

            '** path has a filename
            If Not IO.Directory.Exists(path) Then
                Me.StatusLabel.Text = "no directory found "
            ElseIf Not IO.File.Exists(path & configfilename) Then
                Me.StatusLabel.Text = "no file with that filename found "
            End If

        End If
        'SetConfigProperty(e.Item.PropertyName, weight:=50, value:=e.Item.Value)

    End Sub

    Private Sub radPropertyGrid_EditorRequired(ByVal sender As Object, ByVal e As PropertyGridEditorRequiredEventArgs) Handles RadPropertyGrid.EditorRequired
        If e.Item.Name = constConfigurationName Then
            e.EditorType = GetType(PropertyGridDropDownListEditor)
        ElseIf e.Item.Name = constConfigFileLocation Then
            e.EditorType = GetType(PropertyGridBrowseEditor)
        End If
    End Sub

    Private Sub FileEditor_Changed(ByVal sender As Object, ByVal e As System.EventArgs)

        Debug.Print(DirectCast(sender, RadBrowseEditorElement).Value)

        Dim configfilename As String = ""
        Dim configfilelocation As String = ""
        Dim newValue As String = DirectCast(sender, RadBrowseEditorElement).Value.ToString
        Dim configfile As PropertyStoreItem = _propertyStore.Item(constConfigFileName)
        If configfile IsNot Nothing Then
            configfilename = configfile.Value.ToString
        End If
        Dim configlocation As PropertyStoreItem = _propertyStore.Item(constConfigFileLocation)
        If configlocation IsNot Nothing Then
            configfilelocation = configlocation.Value.ToString
        End If

        If IO.Path.GetFileName(newValue) <> "" Then
            _propertyStore.Item(constConfigFileName).Value = IO.Path.GetFileName(newValue)
        End If
        If IO.Path.GetDirectoryName(newValue) <> "" Then
            _propertyStore.Item(constConfigFileLocation).Value = IO.Path.GetDirectoryName(newValue)
        End If

        Me.Refresh()
    End Sub
    Private Sub radPropertyGrid_EditorInitalized(ByVal sender As Object, ByVal e As PropertyGridItemEditorInitializedEventArgs) Handles RadPropertyGrid.EditorInitialized

        If e.Item.Name = constConfigurationName Then
            Dim editor As PropertyGridDropDownListEditor = DirectCast(e.Editor, PropertyGridDropDownListEditor)
            Dim editorElement As BaseDropDownListEditorElement = DirectCast(editor.EditorElement, BaseDropDownListEditorElement)
            'editorElement.DropDownStyle = RadDropDownStyle.DropDown

            editorElement.DataSource = ot.ConfigSetNames.FindAll(Function(x) x <> ConstGlobalConfigSetName)
            editorElement.SelectedValue = ot.CurrentConfigSetName
        ElseIf e.Item.Name = constConfigFileLocation Then

            Dim editor As PropertyGridBrowseEditor = TryCast(e.Editor, PropertyGridBrowseEditor) 'New PropertyGridBrowseEditor()
            Dim element As RadBrowseEditorElement = TryCast(editor.EditorElement, RadBrowseEditorElement)
            element.DialogType = BrowseEditorDialogType.OpenFileDialog
            'e.Editor = editor

            Dim openDialog As OpenFileDialog = DirectCast(element.Dialog, OpenFileDialog)
            'openDialog.Filter = "OnTrack Config Files (*.ini)"
            openDialog.CheckFileExists = True
            openDialog.CheckPathExists = True
            openDialog.Multiselect = False
            openDialog.Title = "select OnTrack configuration file"

            AddHandler element.ValueChanged, AddressOf FileEditor_Changed
        End If
    End Sub
    ''' <summary>
    ''' creates the Property Store for the configuration ITems
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePropertyStore(Optional store As RadPropertyStore = Nothing) As RadPropertyStore
        Dim myStore As RadPropertyStore = New RadPropertyStore()
        Dim aPropertyName As String = ""
        If store IsNot Nothing Then
            myStore = store
        End If

        '** reread
        ot.RetrieveConfigProperties(force:=False)
        '** check
        Dim currentconfig As PropertyStoreItem = myStore.Item(constConfigurationName)
        If currentconfig Is Nothing Then
            currentconfig = New PropertyStoreItem(GetType(String), constConfigurationName, ot.CurrentConfigSetName)
            myStore.Add(currentconfig)
        End If
        If currentconfig.Value IsNot Nothing AndAlso currentconfig.Value <> ot.CurrentConfigSetName Then
            currentconfig.Value = ot.CurrentConfigSetName
        End If
        currentconfig.Value = ot.CurrentConfigSetName
        currentconfig.Description = "current configuration set to be used"
        currentconfig.Label = "current configuration set"

        If ot.CurrentSession.IsRunning Then currentconfig.ReadOnly = True

        Dim description As PropertyStoreItem = myStore.Item(constConfigDescription)
        If description Is Nothing Then
            description = New PropertyStoreItem(GetType(String), constConfigDescription, GetConfigProperty(ConstCPNDescription))
            myStore.Add(description)
        End If
        If description.Value IsNot Nothing AndAlso description.Value <> GetConfigProperty(ConstCPNDescription) Then
            description.Value = GetConfigProperty(ConstCPNDescription)
        End If
        description.Value = GetConfigProperty(ConstCPNDescription)
        description.Description = "description of the current configuration set"
        description.Label = "current configuration description"
        If ot.CurrentSession.IsRunning Then description.ReadOnly = True

        Dim configfilename As PropertyStoreItem = myStore.Item(constConfigFileName)
        If configfilename Is Nothing Then
            configfilename = New PropertyStoreItem(GetType(String), constConfigFileName, GetConfigProperty(ConstCPNConfigFileName))
            myStore.Add(configfilename)
        End If
        If configfilename.Value IsNot Nothing AndAlso configfilename.Value <> GetConfigProperty(ConstCPNConfigFileName) Then
            configfilename.Value = GetConfigProperty(ConstCPNConfigFileName)
        End If
        configfilename.Description = "name of the current configuration file"
        configfilename.Label = "configuration file name"
        If ot.CurrentSession.IsRunning Then configfilename.ReadOnly = True

        Dim configlocation As PropertyStoreItem = myStore.Item(constConfigFileLocation)
        If configlocation Is Nothing Then
            configlocation = New PropertyStoreItem(GetType(String), constConfigFileLocation, ot.UsedConfigFileLocation)
            myStore.Add(configlocation)
        End If
        If configlocation.Value IsNot Nothing AndAlso configlocation.Value <> ot.UsedConfigFileLocation Then
            configlocation.Value = ot.UsedConfigFileLocation
        End If
        configlocation.Description = "location of the current configuration file"
        configlocation.Label = "configuration file location"
        If ot.CurrentSession.IsRunning Then configlocation.ReadOnly = True

        '*** remove all config sets
        Dim aList As New List(Of String)
        ConfigSetnames.Clear()
        For Each aProperty In myStore
            If aProperty.PropertyName Like "&*" Then
                aList.Add(aProperty.PropertyName)
            End If
        Next
        For Each aName In aList
            myStore.Remove(aName)
            'Dim aValue As String = aProperty.PropertyName
            'If aValue.Contains(ConstDelimiter) Then
            '    Dim aName As String = aValue.Split(ConstDelimiter).ElementAt(1)
            '    If Not ot.ConfigSetNames.Contains(aName) Then
            '        myStore.Remove(aName)
            '    ElseIf Not ot.HasConfigSetProperty(ConstCPNDBType, configsetname:=aName, sequence:=ComplexPropertyStore.Sequence.primary) Then
            '        myStore.Remove(aName)
            '    ElseIf Not ot.HasConfigSetProperty(ConstCPNDBType, configsetname:=aName, sequence:=ComplexPropertyStore.Sequence.secondary) Then
            '        myStore.Remove(aName)
            '    End If
            'End If
        Next

        '*** add configsets
        Dim i As UShort = 1
        For Each aConfigSetName In ot.ConfigSetNamesToSelect
            For Each aSequence As ComplexPropertyStore.Sequence In [Enum].GetValues(GetType(ComplexPropertyStore.Sequence))
                If ot.HasConfigSetName(configsetname:=aConfigSetName, sequence:=aSequence) Then
                    Dim aConfigSetModel As New ConfigSetModel
                    With aConfigSetModel
                        .ConfigSetname = aConfigSetName
                        .Sequence = aSequence
                        .DatabaseType = GetConfigProperty(ConstCPNDBType, configsetname:=aConfigSetName, sequence:=aSequence)
                        .DbUser = GetConfigProperty(ConstCPNDBUser, configsetname:=aConfigSetName, sequence:=aSequence)
                        .DbUserPassword = GetConfigProperty(ConstCPNDBPassword, configsetname:=aConfigSetName, sequence:=aSequence)
                        .Database = GetConfigProperty(ConstCPNDBName, configsetname:=aConfigSetName, sequence:=aSequence)
                        .DBPath = GetConfigProperty(ConstCPNDBPath, configsetname:=aConfigSetName, sequence:=aSequence)
                        .ConfigSetNDescription = GetConfigProperty(ConstCPNDescription, configsetname:=aConfigSetName, sequence:=aSequence)
                        .ConnectionString = GetConfigProperty(ConstCPNDBConnection, configsetname:=aConfigSetName, sequence:=aSequence)
                        .DatabaseDriver = GetConfigProperty(ConstCPNDriverName, configsetname:=aConfigSetName, sequence:=aSequence)
                        .Logagent = GetConfigProperty(constCPNUseLogAgent, configsetname:=aConfigSetName, sequence:=aSequence)
                    End With

                    aPropertyName = "&" & i & ConstDelimiter & "ConfigurationSet" & ConstDelimiter & aSequence.ToString
                    If ConfigSetnames.ContainsKey(aPropertyName) Then
                        ConfigSetnames.Remove(aPropertyName)
                    End If
                    ConfigSetnames.Add(key:=aPropertyName, value:=aConfigSetModel)

                    Dim configset As PropertyStoreItem = myStore.Item(aPropertyName)
                    If configset Is Nothing Then
                        configset = New PropertyStoreItem(GetType(UIFormSetting.ConfigSetModel), aPropertyName, aConfigSetModel)
                        '** add only if there is a sequence
                        If aConfigSetModel.DatabaseType <> 0 And aConfigSetModel.Database <> "" Then
                            configset.Label = "DB Configuration #" & i & ":" & aSequence.ToString
                            myStore.Add(configset)
                        End If
                    End If
                    configset.Value = aConfigSetModel
                    configset.Description = GetConfigProperty(ConstCPNDescription, configsetname:=aConfigSetName)
                    configset.Label = "DB Configuration #" & i & ":" & aSequence.ToString



                End If
            Next
            i += 1 ' next configuration (primary & secondary belong together)
        Next

        Return myStore
    End Function

    '''
    ''' <summary>
    ''' Cancel Button Handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click, Me.FormClosing
        Dim ds As Windows.Forms.DialogResult
        If isChanged Then
            RadMessageBox.SetThemeName(Me.ThemeName)
            ds = RadMessageBox.Show(Me, "Are you sure?", "Cancel", Windows.Forms.MessageBoxButtons.YesNo, RadMessageIcon.Question)
        Else
            ds = Windows.Forms.DialogResult.Yes
        End If
        Dim formClosingArgs As System.Windows.Forms.FormClosingEventArgs = _
               TryCast(e, System.Windows.Forms.FormClosingEventArgs)

        If formClosingArgs Is Nothing Then
            If ds = Windows.Forms.DialogResult.Yes Then
                Me.Hide()
            End If
        Else
            If ds = Windows.Forms.DialogResult.Yes Then
                formClosingArgs.Cancel = True
                Me.Hide()
            End If
        End If


    End Sub

    ''' <summary>
    ''' Create Schema Handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>


    Private Sub ButtonCreateSchema_Click(sender As Object, e As EventArgs) Handles ButtonCreateSchema.Click

        If ot.RequireAccess(otAccessRight.AlterSchema) Then
            Global.OnTrack.Database.createDatabase.Run(ot.InstalledModules)
        Else
            ot.CoreMessageHandler(message:="couldn't acquire the necessary rights to continue this operation", _
                                         messagetype:=otCoreMessageType.ApplicationError, subname:="UIFormSetting.CreateSchemaButton")

        End If
    End Sub
    ''' <summary>
    ''' Onload of Form handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UIFormSetting_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

        Me.RadPropertyGrid.SelectedObject = Me._propertyStore
        isChanged = False
    End Sub

    ''' <summary>
    ''' Event handler for save in Document
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SaveInDocument_Click(sender As Object, e As EventArgs) Handles SaveDocumentMenuButton.Click
        Try
            Dim configsetname As PropertyStoreItem = _propertyStore.Item(constConfigurationName)
            If configsetname IsNot Nothing AndAlso configsetname.Value IsNot Nothing AndAlso LCase(CStr(configsetname.Value)) <> LCase(ot.CurrentConfigSetName) Then
                ot.CurrentConfigSetName = configsetname.Value.ToString
                SetHostPropertyDelegate(name:=ConstCPNUseConfigSetName, value:=configsetname.Value, host:=Nothing, silent:=False)
                ot.CoreMessageHandler(message:="Current database configuration changed to " & CStr(configsetname.Value), _
                                      messagetype:=otCoreMessageType.ApplicationInfo, subname:="UIFormSetting.SaveInDocument")
            End If

            Dim description As PropertyStoreItem = _propertyStore.Item(constConfigDescription)
            If description IsNot Nothing Then
                SetConfigProperty(ot.ConstCPNDescription, weight:=50, value:=description.Value)
                SetHostPropertyDelegate(name:=ot.ConstCPNDescription, value:=description.Value, host:=Nothing, silent:=False)
            End If

            Dim configfilename As PropertyStoreItem = _propertyStore.Item(ConstCPNConfigFileName)
            If configfilename IsNot Nothing Then
                SetConfigProperty(ot.ConstCPNConfigFileName, weight:=50, value:=configfilename.Value)
                SetHostPropertyDelegate(name:=ot.ConstCPNConfigFileName, value:=configfilename.Value, host:=Nothing, silent:=False)
            End If

            Dim configfilelocation As PropertyStoreItem = _propertyStore.Item(constConfigFileLocation)
            If configfilelocation IsNot Nothing Then
                ot.AddConfigFilePath(configfilelocation.Value)
                SetHostPropertyDelegate(name:=ot.ConstCPNConfigFileLocation, value:=configfilelocation.Value, host:=Nothing, silent:=False)
            End If

            For Each aProperty In _propertyStore
                Dim aValue As String = aProperty.PropertyName

                If aValue.Split(ConstDelimiter).Length >= 2 Then
                    Dim aName As String = aValue.Split(ConstDelimiter).ElementAt(1)
                    If LCase(aName) = "configurationset" Then
                        Dim aConfigSetModel As ConfigSetModel = TryCast(aProperty.Value, ConfigSetModel)
                        If aConfigSetModel IsNot Nothing _
                        AndAlso aConfigSetModel.ConfigSetname = configsetname.Value.ToString _
                        And aConfigSetModel.Sequence = ComplexPropertyStore.Sequence.Primary Then
                            With aConfigSetModel
                                If .Database <> "" Then
                                    SetHostPropertyDelegate(name:=ConstCPNDBName, value:=.Database, host:=Nothing, silent:=False)
                                    SetConfigProperty(ConstCPNDBName, weight:=50, value:=.Database, configsetname:=configsetname.Value.ToString, sequence:=ComplexPropertyStore.Sequence.Primary)
                                End If

                                If .DBPath <> "" Then
                                    SetHostPropertyDelegate(name:=ConstCPNDBPath, value:=.DBPath, host:=Nothing, silent:=False)
                                    SetConfigProperty(ConstCPNDBPath, weight:=50, value:=.DBPath, configsetname:=configsetname.Value.ToString, sequence:=ComplexPropertyStore.Sequence.Primary)
                                End If

                                If .DatabaseType <> 0 Then
                                    SetHostPropertyDelegate(name:=ConstCPNDBType, value:=.DatabaseType, host:=Nothing, silent:=False)
                                    SetConfigProperty(ConstCPNDBType, weight:=50, value:=.DatabaseType, configsetname:=configsetname.Value.ToString, sequence:=ComplexPropertyStore.Sequence.Primary)
                                End If

                                If .DbUser <> "" Then
                                    SetHostPropertyDelegate(name:=ConstCPNDBUser, value:=.DbUser, host:=Nothing, silent:=False)
                                    SetConfigProperty(ConstCPNDBUser, weight:=50, value:=.DbUser, configsetname:=configsetname.Value.ToString, sequence:=ComplexPropertyStore.Sequence.Primary)
                                End If

                                If .DbUserPassword <> "" Then
                                    SetHostPropertyDelegate(name:=ConstCPNDBPassword, value:=.DbUserPassword, host:=Nothing, silent:=False)
                                    SetConfigProperty(ConstCPNDBPassword, weight:=50, value:=.DbUserPassword, configsetname:=configsetname.Value.ToString, sequence:=ComplexPropertyStore.Sequence.Primary)
                                End If

                                If .ConfigSetNDescription <> "" Then
                                    SetHostPropertyDelegate(name:=ConstCPNDescription, value:=.ConfigSetNDescription, host:=Nothing, silent:=False)
                                    SetConfigProperty(name:=ConstCPNDescription, weight:=50, value:=.ConfigSetNDescription, configsetname:=configsetname.Value.ToString, sequence:=ComplexPropertyStore.Sequence.Primary)
                                End If

                                If .DatabaseDriver <> 0 Then
                                    SetHostPropertyDelegate(name:=ConstCPNDriverName, value:=.DatabaseDriver, host:=Nothing, silent:=False)
                                    SetConfigProperty(name:=ConstCPNDriverName, weight:=50, value:=.DatabaseDriver, configsetname:=configsetname.Value.ToString, sequence:=ComplexPropertyStore.Sequence.Primary)
                                End If

                                If .ConnectionString <> "" Then
                                    SetHostPropertyDelegate(name:=ConstCPNDBConnection, value:=.ConnectionString, host:=Nothing, silent:=False)
                                    SetConfigProperty(name:=ConstCPNDBConnection, weight:=50, value:=.ConnectionString, configsetname:=configsetname.Value.ToString, sequence:=ComplexPropertyStore.Sequence.Primary)
                                End If

                                SetHostPropertyDelegate(name:=constCPNUseLogAgent, value:=.Logagent, host:=Nothing, silent:=False)
                                SetConfigProperty(name:=constCPNUseLogAgent, weight:=50, value:=.Logagent, configsetname:=configsetname.Value.ToString, sequence:=ComplexPropertyStore.Sequence.Primary)

                            End With
                        End If
                    End If
                End If
            Next


            ot.CoreMessageHandler(message:="OnTrack configuration properties saved in document properties", messagetype:=otCoreMessageType.ApplicationInfo, _
                                   subname:="UIFormSetting.saveInDocument")
            isChanged = False
            Me.Refresh()
            Me.Hide()
        Catch ex As Exception
            ot.CoreMessageHandler(exception:=ex, subname:="UIFormSetting.SaveInDocument", showmsgbox:=True)
        End Try

    End Sub
    ''' <summary>
    ''' handles the inSession save
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SaveInSessionButton_Click(sender As Object, e As EventArgs) Handles SaveInSessionMenuButton.Click
        Try
            If ot.CurrentSession.IsRunning Then
                ot.CoreMessageHandler(showmsgbox:=True, message:="Current database configuration cannot be changed." & vbLf & _
                                      "since a session is running. Please disconnect from session and try again", _
                                       messagetype:=otCoreMessageType.ApplicationError, subname:="UIFormSetting.SaveInSession")
                Return
            End If


            '** change the config set name
            Dim configsetname As PropertyStoreItem = _propertyStore.Item(constConfigurationName)
            If configsetname IsNot Nothing AndAlso configsetname.Value IsNot Nothing AndAlso LCase(CStr(configsetname.Value)) <> LCase(ot.CurrentConfigSetName) Then
                ot.CurrentConfigSetName = configsetname.Value
                ot.CoreMessageHandler(message:="Current database configuration changed to " & CStr(configsetname.Value), _
                                      messagetype:=otCoreMessageType.ApplicationInfo, subname:="UIFormSetting.SaveInSession")
            End If

            Dim description As PropertyStoreItem = _propertyStore.Item(constConfigDescription)
            If description IsNot Nothing Then SetConfigProperty(ot.ConstCPNDescription, weight:=50, value:=description.Value)

            Dim configfilename As PropertyStoreItem = _propertyStore.Item(ConstCPNConfigFileName)
            If configfilename IsNot Nothing Then ot.CurrentConfigFileName = configfilename.Value

            Dim configfilelocation As PropertyStoreItem = _propertyStore.Item(constConfigFileLocation)
            If configfilelocation IsNot Nothing Then ot.AddConfigFilePath(configfilelocation.Value)

            '** set the current configuration set
            Dim aSequence As ComplexPropertyStore.Sequence
            For Each aProperty In _propertyStore
                Dim aValue As String = aProperty.PropertyName
                If aValue.Split(ConstDelimiter).Length >= 2 Then
                    Dim aName As String = aValue.Split(ConstDelimiter).ElementAt(1)
                    If LCase(aName) = "configurationset" Then
                        Dim aConfigSetModel As ConfigSetModel = TryCast(aProperty.Value, ConfigSetModel)
                        If aConfigSetModel IsNot Nothing _
                            AndAlso aConfigSetModel.ConfigSetname = configsetname.Value.ToString Then
                            aSequence = aConfigSetModel.Sequence
                            With aConfigSetModel
                                If .Database <> "" Then SetConfigProperty(ConstCPNDBName, weight:=50, value:=.Database, configsetname:=configsetname.Value.ToString, sequence:=aSequence)
                                If .DBPath <> "" Then SetConfigProperty(ConstCPNDBPath, weight:=50, value:=.DBPath, configsetname:=configsetname.Value.ToString, sequence:=aSequence)
                                If .DatabaseType <> 0 Then SetConfigProperty(ConstCPNDBType, weight:=50, value:=.DatabaseType, configsetname:=configsetname.Value.ToString, sequence:=aSequence)
                                If .DbUser <> "" Then SetConfigProperty(ConstCPNDBUser, weight:=50, value:=.DbUser, configsetname:=configsetname.Value.ToString, sequence:=aSequence)
                                If .DbUserPassword <> "" Then SetConfigProperty(ConstCPNDBPassword, weight:=50, value:=.DbUserPassword, configsetname:=configsetname.Value.ToString, sequence:=aSequence)
                                If .ConfigSetNDescription <> "" Then SetConfigProperty(name:=ConstCPNDescription, weight:=50, value:=.ConfigSetNDescription, configsetname:=configsetname.Value.ToString, sequence:=aSequence)
                                If .DatabaseDriver <> 0 Then SetConfigProperty(name:=ConstCPNDriverName, weight:=50, value:=.DatabaseDriver, configsetname:=configsetname.Value.ToString, sequence:=aSequence)
                                If .ConnectionString <> "" Then SetConfigProperty(name:=ConstCPNDBConnection, weight:=50, value:=.ConnectionString, configsetname:=configsetname.Value.ToString, sequence:=aSequence)
                                SetConfigProperty(name:=constCPNUseLogAgent, weight:=50, value:=.Logagent, configsetname:=configsetname.Value.ToString, sequence:=aSequence)
                            End With
                        End If

                    End If
                End If
            Next


            ot.CoreMessageHandler(message:="OnTrack configuration properties saved in session", messagetype:=otCoreMessageType.ApplicationInfo, _
                                   subname:="UIFormSetting.SaveInSession")
            isChanged = False
            Me.Refresh()
            Me.Close()
        Catch ex As Exception
            ot.CoreMessageHandler(exception:=ex, subname:="UIFormSetting.SaveInSession", showmsgbox:=True)
        End Try
    End Sub
    ''' <summary>
    ''' handles the InConfigFileSave
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub SaveInConfigFileButton_Click(sender As Object, e As EventArgs) Handles SaveConfigFileMenuButton.Click

    End Sub
    ''' <summary>
    ''' handles the Form OnLoad Event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UIFormSetting_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add any initialization after the InitializeComponent() call.
        _propertyStore = Me.UpdatePropertyStore()
    End Sub

    ''' <summary>
    ''' Click the button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CreateData_Click(sender As Object, e As EventArgs) Handles CreateData.Click
        If createDatabase.InitializeTestData() Then
            Me.StatusLabel.Text = "test data initialized"
        End If
    End Sub
End Class
