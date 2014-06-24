Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Reflection

Partial Public Class UIAboutBox
    Inherits Telerik.WinControls.UI.RadForm

    Public Sub New()
        InitializeComponent()

        '  Initialize the AboutBox to display the product information from the assembly information.
        '  Change assembly information settings for your application through either:
        '  - Project->Properties->Application->Assembly Information
        '  - AssemblyInfo.cs
        Me.Text = String.Format("About {0}", AboutData.ApplicationName)
        Me.radLabelProductName.Text = AboutData.ProductName
        Me.radLabelVersion.Text = String.Format("Version {0}", AboutData.Version)
        Me.radLabelCopyright.Text = AboutData.CopyRight
        Me.radLabelCompanyName.Text = AboutData.Company
        Me.radTextBoxDescription.Text = AboutData.Description
    End Sub

    ''' <summary>
    ''' Click Handler
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub OkRadButton_Click(sender As Object, e As EventArgs) Handles okRadButton.Click
        Me.Dispose()
    End Sub

    Private Sub UIAboutBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class


Public Class AboutData
    Private Shared _ApplicationName As String
    Private Shared _Version As String
    Private Shared _Description As String
    Private Shared _CopyRight As String
    Private Shared _Company As String
    Private Shared _ProductName As String

    ''' <summary>
    ''' Gets or sets the name of the product.
    ''' </summary>
    ''' <value>The name of the product.</value>
    Public Shared Property ProductName() As String
        Get
            If String.IsNullOrWhiteSpace(_ProductName) Then
                ' Get all Product attributes on this assembly
                Dim attributes As Object() = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyProductAttribute), False)
                ' If there aren't any Product attributes, return an empty string
                If attributes.Length = 0 Then
                    Return ""
                End If
                ' If there is a Product attribute, return its value
                Return (CType(attributes(0), AssemblyProductAttribute)).Product
            End If
            Return _ProductName
        End Get
        Set(value As String)
            _ProductName = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the company.
    ''' </summary>
    ''' <value>The company.</value>
    Public Shared Property Company() As String
        Get
            If String.IsNullOrWhiteSpace(_Company) Then
                ' Get all Company attributes on this assembly
                Dim attributes As Object() = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCompanyAttribute), False)
                ' If there aren't any Company attributes, return an empty string
                If attributes.Length = 0 Then
                    Return ""
                End If
                ' If there is a Company attribute, return its value
                Return (CType(attributes(0), AssemblyCompanyAttribute)).Company
            End If
            Return _Company
        End Get
        Set(value As String)
            _Company = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the copy right.
    ''' </summary>
    ''' <value>The copy right.</value>
    Public Shared Property CopyRight() As String
        Get
            If String.IsNullOrWhiteSpace(_CopyRight) Then
                ' Get all Copyright attributes on this assembly
                Dim attributes As Object() = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCopyrightAttribute), False)
                ' If there aren't any Copyright attributes, return an empty string
                If attributes.Length = 0 Then
                    Return ""
                End If
                ' If there is a Copyright attribute, return its value
                Return (CType(attributes(0), AssemblyCopyrightAttribute)).Copyright
            End If
            Return _CopyRight
        End Get
        Set(value As String)
            _CopyRight = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the description.
    ''' </summary>
    ''' <value>The description.</value>
    Public Shared Property Description() As String
        Get
            If String.IsNullOrWhiteSpace(_Description) Then
                ' Get all Description attributes on this assembly
                Dim attributes As Object() = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyDescriptionAttribute), False)
                ' If there aren't any Description attributes, return an empty string
                If attributes.Length = 0 Then
                    Return ""
                End If
                ' If there is a Description attribute, return its value
                Return (CType(attributes(0), AssemblyDescriptionAttribute)).Description
            End If
            Return _Description
        End Get
        Set(value As String)
            _Description = Value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the version.
    ''' </summary>
    ''' <value>The version.</value>
    Public Shared Property Version() As String
        Get
            If String.IsNullOrWhiteSpace(_Version) Then
                Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
            End If
            Return _Version
        End Get
        Set(value As String)
            _Version = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the name of the application.
    ''' </summary>
    ''' <value>The name of the application.</value>
    Public Shared Property ApplicationName() As String
        Get
            If String.IsNullOrWhiteSpace(_ApplicationName) Then
                ' Get all Title attributes on this assembly
                Dim attributes As Object() = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyTitleAttribute), False)
                ' If there is at least one Title attribute
                If attributes.Length > 0 Then
                    ' Select the first one
                    Dim titleAttribute As AssemblyTitleAttribute = CType(attributes(0), AssemblyTitleAttribute)
                    ' If it is not an empty string, return it
                    If titleAttribute.Title <> "" Then
                        Return titleAttribute.Title
                    End If
                End If
                ' If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                Return System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
            End If
            Return _ApplicationName
        End Get
        Set(value As String)
            _ApplicationName = value
        End Set
    End Property

End Class
