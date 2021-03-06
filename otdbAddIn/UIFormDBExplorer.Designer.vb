﻿Imports OnTrack.UI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UIFormDBExplorer
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UIFormDBExplorer))
        Me.TelerikMetroBlueTheme1 = New Telerik.WinControls.Themes.TelerikMetroBlueTheme()
        Me.StatusStrip = New Telerik.WinControls.UI.RadStatusStrip()
        Me.StatusLabel = New Telerik.WinControls.UI.RadLabelElement()
        Me.RadCloseButton = New Telerik.WinControls.UI.RadButtonElement()
        Me.RadSplitContainer1 = New Telerik.WinControls.UI.RadSplitContainer()
        Me.SplitPanel1 = New Telerik.WinControls.UI.SplitPanel()
        Me.ObjectTree = New Telerik.WinControls.UI.RadTreeView()
        Me.SplitPanel2 = New Telerik.WinControls.UI.SplitPanel()
        Me.RadPageView = New Telerik.WinControls.UI.RadPageView()
        Me.PageObjectProperties = New Telerik.WinControls.UI.RadPageViewPage()
        Me.PageData = New Telerik.WinControls.UI.RadPageViewPage()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RefreshTableButton = New Telerik.WinControls.UI.RadMenuItem()
        Me.Menu = New Telerik.WinControls.UI.RadMenu()
        Me.DomainComboMenu = New Telerik.WinControls.UI.RadMenuComboItem()
        Me.RefreshMenu = New Telerik.WinControls.UI.RadMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RadMenuItem1 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadMenuItem2 = New Telerik.WinControls.UI.RadMenuItem()
        Me.RadOffice2007ScreenTipElement1 = New Telerik.WinControls.UI.RadOffice2007ScreenTipElement()
        CType(Me.StatusStrip,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.RadSplitContainer1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.RadSplitContainer1.SuspendLayout
        CType(Me.SplitPanel1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitPanel1.SuspendLayout
        CType(Me.ObjectTree,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.SplitPanel2,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitPanel2.SuspendLayout
        CType(Me.RadPageView,System.ComponentModel.ISupportInitialize).BeginInit
        Me.RadPageView.SuspendLayout
        CType(Me.RadMenu1,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.Menu,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.DomainComboMenu.ComboBoxElement,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New Telerik.WinControls.RadItem() {Me.StatusLabel, Me.RadCloseButton})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 377)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(883, 32)
        Me.StatusStrip.TabIndex = 1
        Me.StatusStrip.Text = "RadStatusStrip"
        Me.StatusStrip.ThemeName = "TelerikMetroBlue"
        Me.StatusStrip.AutoSize = True
        '
        'StatusLabel
        '
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusStrip.SetSpring(Me.StatusLabel, true)
        Me.StatusLabel.Text = ""
        Me.StatusLabel.TextWrap = False
        Me.StatusLabel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadCloseButton
        '
        Me.RadCloseButton.AccessibleDescription = "Close"
        Me.RadCloseButton.AccessibleName = "Close"
        Me.RadCloseButton.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.RadCloseButton.Name = "RadCloseButton"
        Me.StatusStrip.SetSpring(Me.RadCloseButton, false)
        Me.RadCloseButton.Text = "Close"
        Me.RadCloseButton.UseDefaultDisabledPaint = true
        Me.RadCloseButton.Visibility = Telerik.WinControls.ElementVisibility.Visible

        '
        'RadSplitContainer1
        '
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel1)
        Me.RadSplitContainer1.Controls.Add(Me.SplitPanel2)
        Me.RadSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadSplitContainer1.Location = New System.Drawing.Point(0, 36)
        Me.RadSplitContainer1.Name = "RadSplitContainer1"
        '
        '
        '
        Me.RadSplitContainer1.RootElement.MinSize = New System.Drawing.Size(25, 25)
        Me.RadSplitContainer1.Size = New System.Drawing.Size(883, 341)
        Me.RadSplitContainer1.TabIndex = 2
        Me.RadSplitContainer1.TabStop = false
        Me.RadSplitContainer1.Text = "RadSplitContainer1"
        Me.RadSplitContainer1.ThemeName = "TelerikMetroBlue"
        '
        'SplitPanel1
        '
        Me.SplitPanel1.Controls.Add(Me.ObjectTree)
        Me.SplitPanel1.Location = New System.Drawing.Point(0, 0)
        Me.SplitPanel1.Name = "SplitPanel1"
        '
        '
        '
        Me.SplitPanel1.RootElement.MinSize = New System.Drawing.Size(25, 25)
        Me.SplitPanel1.Size = New System.Drawing.Size(238, 341)
        Me.SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(-0.2290168!, 0!)
        Me.SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(-191, 0)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = false
        Me.SplitPanel1.ThemeName = "TelerikMetroBlue"
        '
        'ObjectTree
        '
        Me.ObjectTree.AllowArbitraryItemHeight = true
        Me.ObjectTree.AllowPlusMinusAnimation = true
        Me.ObjectTree.BackColor = System.Drawing.Color.White
        Me.ObjectTree.Cursor = System.Windows.Forms.Cursors.Default
        Me.ObjectTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ObjectTree.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.ObjectTree.ForeColor = System.Drawing.Color.Black
        Me.ObjectTree.LineColor = System.Drawing.Color.FromArgb(CType(CType(214,Byte),Integer), CType(CType(21,Byte),Integer), CType(CType(21,Byte),Integer))
        Me.ObjectTree.Location = New System.Drawing.Point(0, 0)
        Me.ObjectTree.Name = "ObjectTree"
        Me.ObjectTree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ObjectTree.ShowLines = true
        Me.ObjectTree.Size = New System.Drawing.Size(238, 341)
        Me.ObjectTree.SpacingBetweenNodes = -1
        Me.ObjectTree.TabIndex = 0
        Me.ObjectTree.Text = "Objectsstructure"
        Me.ObjectTree.ThemeName = "TelerikMetroBlue"
        '
        'SplitPanel2
        '
        Me.SplitPanel2.Controls.Add(Me.RadPageView)
        Me.SplitPanel2.Location = New System.Drawing.Point(241, 0)
        Me.SplitPanel2.Name = "SplitPanel2"
        '
        '
        '
        Me.SplitPanel2.RootElement.MinSize = New System.Drawing.Size(25, 25)
        Me.SplitPanel2.Size = New System.Drawing.Size(642, 341)
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0.2290168!, 0!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(191, 0)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = false
        Me.SplitPanel2.ThemeName = "TelerikMetroBlue"
        '
        'RadPageView
        '
        Me.RadPageView.Controls.Add(Me.PageObjectProperties)
        Me.RadPageView.Controls.Add(Me.PageData)
        Me.RadPageView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadPageView.Location = New System.Drawing.Point(0, 0)
        Me.RadPageView.Name = "RadPageView"
        Me.RadPageView.SelectedPage = Me.PageData
        Me.RadPageView.Size = New System.Drawing.Size(642, 341)
        Me.RadPageView.TabIndex = 0
        Me.RadPageView.Text = "RadPageView1"
        Me.RadPageView.ThemeName = "TelerikMetroBlue"
        '
        'PageObjectProperties
        '
        Me.PageObjectProperties.Description = Nothing
        Me.PageObjectProperties.Enabled = false
        Me.PageObjectProperties.Location = New System.Drawing.Point(5, 31)
        Me.PageObjectProperties.Name = "PageObjectProperties"
        Me.PageObjectProperties.Size = New System.Drawing.Size(632, 281)
        Me.PageObjectProperties.Text = "Properties"
        Me.PageObjectProperties.Title = "Object Properties"
        '
        'PageData
        '
        Me.PageData.Enabled = false
        Me.PageData.Location = New System.Drawing.Point(5, 31)
        Me.PageData.Name = "PageData"
        Me.PageData.Size = New System.Drawing.Size(632, 305)
        Me.PageData.Text = "Data"
        '
        'RadMenu1
        '
        Me.RadMenu1.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RefreshTableButton})
        Me.RadMenu1.Location = New System.Drawing.Point(0, 0)
        Me.RadMenu1.Name = "RadMenu1"
        Me.RadMenu1.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.RadMenu1.Size = New System.Drawing.Size(632, 60)
        Me.RadMenu1.TabIndex = 1
        Me.RadMenu1.Text = "RadMenu1"
        Me.RadMenu1.ThemeName = "TelerikMetroBlue"
        '
        'RefreshTableButton
        '
        Me.RefreshTableButton.AccessibleDescription = "Refresh"
        Me.RefreshTableButton.AccessibleName = "Refresh"
        Me.RefreshTableButton.Image = Global.OnTrack.AddIn.My.Resources.Resources.playback_reload
        Me.RefreshTableButton.Name = "RefreshTableButton"
        Me.RefreshTableButton.Text = "Refresh"
        Me.RefreshTableButton.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'Menu
        '
        Me.Menu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.DomainComboMenu, Me.RefreshMenu})
        Me.Menu.Location = New System.Drawing.Point(0, 0)
        Me.Menu.Name = "Menu"
        Me.Menu.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.Menu.Size = New System.Drawing.Size(883, 36)
        Me.Menu.TabIndex = 0
        Me.Menu.ThemeName = "TelerikMetroBlue"
        '
        'DomainComboMenu
        '
        Me.DomainComboMenu.AccessibleDescription = "Domain"
        Me.DomainComboMenu.AccessibleName = "Domain"
        Me.DomainComboMenu.AutoToolTip = true
        '
        '
        '
        Me.DomainComboMenu.ComboBoxElement.AutoCompleteAppend = Nothing
        Me.DomainComboMenu.ComboBoxElement.AutoCompleteDataSource = Nothing
        Me.DomainComboMenu.ComboBoxElement.AutoCompleteDisplayMember = Nothing
        Me.DomainComboMenu.ComboBoxElement.AutoCompleteSuggest = Nothing
        Me.DomainComboMenu.ComboBoxElement.AutoCompleteValueMember = Nothing
        Me.DomainComboMenu.ComboBoxElement.DataMember = ""
        Me.DomainComboMenu.ComboBoxElement.DataSource = Nothing
        Me.DomainComboMenu.ComboBoxElement.DefaultItemsCountInDropDown = 6
        Me.DomainComboMenu.ComboBoxElement.DefaultValue = Nothing
        Me.DomainComboMenu.ComboBoxElement.DisplayMember = ""
        Me.DomainComboMenu.ComboBoxElement.DropDownAnimationEasing = Telerik.WinControls.RadEasingType.InQuad
        Me.DomainComboMenu.ComboBoxElement.DropDownAnimationEnabled = true
        Me.DomainComboMenu.ComboBoxElement.EditableElementText = ""
        Me.DomainComboMenu.ComboBoxElement.EditorElement = Me.DomainComboMenu.ComboBoxElement
        Me.DomainComboMenu.ComboBoxElement.EditorManager = Nothing
        Me.DomainComboMenu.ComboBoxElement.Filter = Nothing
        Me.DomainComboMenu.ComboBoxElement.FilterExpression = ""
        Me.DomainComboMenu.ComboBoxElement.Focusable = true
        Me.DomainComboMenu.ComboBoxElement.FormatString = ""
        Me.DomainComboMenu.ComboBoxElement.FormattingEnabled = true
        Me.DomainComboMenu.ComboBoxElement.ItemHeight = 18
        Me.DomainComboMenu.ComboBoxElement.MaxDropDownItems = 0
        Me.DomainComboMenu.ComboBoxElement.MaxLength = 32767
        Me.DomainComboMenu.ComboBoxElement.MaxValue = Nothing
        Me.DomainComboMenu.ComboBoxElement.MinValue = Nothing
        Me.DomainComboMenu.ComboBoxElement.NullValue = Nothing
        Me.DomainComboMenu.ComboBoxElement.Owner = Nothing
        Me.DomainComboMenu.ComboBoxElement.OwnerOffset = 0
        Me.DomainComboMenu.ComboBoxElement.ShowImageInEditorArea = true
        Me.DomainComboMenu.ComboBoxElement.SortStyle = Telerik.WinControls.Enumerations.SortStyle.None
        Me.DomainComboMenu.ComboBoxElement.Value = Nothing
        Me.DomainComboMenu.ComboBoxElement.ValueMember = ""
        Me.DomainComboMenu.DisplayStyle = Telerik.WinControls.DisplayStyle.Text
        Me.DomainComboMenu.Name = "DomainComboMenu"
        Me.DomainComboMenu.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None
        Me.DomainComboMenu.Text = ""
        Me.DomainComboMenu.ToolTipText = "Set the Current Domain"
        Me.DomainComboMenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RefreshMenu
        '
        Me.RefreshMenu.AccessibleDescription = "Refresh"
        Me.RefreshMenu.AccessibleName = "Refresh"
        Me.RefreshMenu.Image = Global.OnTrack.AddIn.My.Resources.Resources.playback_reload
        Me.RefreshMenu.Name = "RefreshMenu"
        Me.RefreshMenu.Text = "Refresh"
        Me.RefreshMenu.Visibility = Telerik.WinControls.ElementVisibility.Visible
        CType(Me.RefreshMenu.GetChildAt(2),Telerik.WinControls.UI.RadMenuItemLayout).ScaleTransform = New System.Drawing.SizeF(1!, 1!)
        CType(Me.RefreshMenu.GetChildAt(2).GetChildAt(0),Telerik.WinControls.UI.MenuImageAndTextLayout).ScaleTransform = New System.Drawing.SizeF(0.5!, 0.5!)
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'RadMenuItem1
        '
        Me.RadMenuItem1.AccessibleDescription = "RadMenuItem1"
        Me.RadMenuItem1.AccessibleName = "RadMenuItem1"
        Me.RadMenuItem1.Name = "RadMenuItem1"
        Me.RadMenuItem1.Text = "RadMenuItem1"
        Me.RadMenuItem1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadMenuItem2
        '
        Me.RadMenuItem2.AccessibleDescription = "RadMenuItem2"
        Me.RadMenuItem2.AccessibleName = "RadMenuItem2"
        Me.RadMenuItem2.Name = "RadMenuItem2"
        Me.RadMenuItem2.Text = "RadMenuItem2"
        Me.RadMenuItem2.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadOffice2007ScreenTipElement1
        '
        Me.RadOffice2007ScreenTipElement1.Description = "Override this property and provide custom screentip template description in Desig"& _ 
    "nTime."
        Me.RadOffice2007ScreenTipElement1.Name = "RadOffice2007ScreenTipElement1"
        Me.RadOffice2007ScreenTipElement1.TemplateType = Nothing
        Me.RadOffice2007ScreenTipElement1.TipSize = New System.Drawing.Size(210, 50)
        Me.RadOffice2007ScreenTipElement1.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'UIFormDBExplorer
        '
        Me.AcceptButton = Me.RadCloseButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.RadCloseButton
        Me.ClientSize = New System.Drawing.Size(883, 409)
        Me.Controls.Add(Me.RadSplitContainer1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.Menu)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(400, 200)
        Me.Name = "UIFormDBExplorer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = true
        Me.Text = "OnTrack Database Explorer"
        Me.ThemeName = "TelerikMetroBlue"
        CType(Me.StatusStrip,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.RadSplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.RadSplitContainer1.ResumeLayout(false)
        CType(Me.SplitPanel1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitPanel1.ResumeLayout(false)
        CType(Me.ObjectTree,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.SplitPanel2,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitPanel2.ResumeLayout(false)
        CType(Me.RadPageView,System.ComponentModel.ISupportInitialize).EndInit
        Me.RadPageView.ResumeLayout(false)
        CType(Me.RadMenu1,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.Menu,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DomainComboMenu.ComboBoxElement,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents TelerikMetroBlueTheme1 As Telerik.WinControls.Themes.TelerikMetroBlueTheme
    Friend WithEvents StatusStrip As Telerik.WinControls.UI.RadStatusStrip
    Friend WithEvents RadCloseButton As Telerik.WinControls.UI.RadButtonElement
    Friend WithEvents StatusLabel As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents RadSplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel1 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel2 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents ObjectTree As Telerik.WinControls.UI.RadTreeView
    Friend WithEvents RefreshMenu As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Menu As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadPageView As Telerik.WinControls.UI.RadPageView
    Friend WithEvents PageObjectProperties As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents PageData As Telerik.WinControls.UI.RadPageViewPage
    'Friend WithEvents GridView As Telerik.WinControls.UI.RadGridView

    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RefreshTableButton As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents DataGrid As Global.OnTrack.UI.UIControlDataGridView
    Friend WithEvents DomainComboMenu As Telerik.WinControls.UI.RadMenuComboItem
    Friend WithEvents RadMenuItem1 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadMenuItem2 As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents RadOffice2007ScreenTipElement1 As Telerik.WinControls.UI.RadOffice2007ScreenTipElement
End Class

