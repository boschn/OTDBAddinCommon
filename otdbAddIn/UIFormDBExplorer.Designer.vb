Imports OnTrack.UI

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
        Me.DataGrid = New UIControlDataGridView()
        Me.RadMenu1 = New Telerik.WinControls.UI.RadMenu()
        Me.RefreshTableButton = New Telerik.WinControls.UI.RadMenuItem()
        Me.Menu = New Telerik.WinControls.UI.RadMenu()
        Me.RefreshTreeButton = New Telerik.WinControls.UI.RadMenuItem()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        CType(Me.StatusStrip, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadSplitContainer1.SuspendLayout()
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel1.SuspendLayout()
        CType(Me.ObjectTree, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitPanel2.SuspendLayout()
        CType(Me.RadPageView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPageView.SuspendLayout()
        Me.PageData.SuspendLayout()
        CType(Me.DataGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGrid.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Menu, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        '
        'StatusLabel
        '
        Me.StatusLabel.Name = "StatusLabel"
        Me.StatusStrip.SetSpring(Me.StatusLabel, True)
        Me.StatusLabel.Text = ""
        Me.StatusLabel.TextWrap = True
        Me.StatusLabel.Visibility = Telerik.WinControls.ElementVisibility.Visible
        '
        'RadCloseButton
        '
        Me.RadCloseButton.AccessibleDescription = "Close"
        Me.RadCloseButton.AccessibleName = "Close"
        Me.RadCloseButton.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadCloseButton.Name = "RadCloseButton"
        Me.StatusStrip.SetSpring(Me.RadCloseButton, False)
        Me.RadCloseButton.Text = "Close"
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
        Me.RadSplitContainer1.TabStop = False
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
        Me.SplitPanel1.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(-0.2290168!, 0.0!)
        Me.SplitPanel1.SizeInfo.SplitterCorrection = New System.Drawing.Size(-191, 0)
        Me.SplitPanel1.TabIndex = 0
        Me.SplitPanel1.TabStop = False
        Me.SplitPanel1.ThemeName = "TelerikMetroBlue"
        '
        'ObjectTree
        '
        Me.ObjectTree.AllowArbitraryItemHeight = True
        Me.ObjectTree.AllowPlusMinusAnimation = True
        Me.ObjectTree.BackColor = System.Drawing.Color.White
        Me.ObjectTree.Cursor = System.Windows.Forms.Cursors.Default
        Me.ObjectTree.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ObjectTree.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.ObjectTree.ForeColor = System.Drawing.Color.Black
        Me.ObjectTree.LineColor = System.Drawing.Color.FromArgb(CType(CType(214, Byte), Integer), CType(CType(21, Byte), Integer), CType(CType(21, Byte), Integer))
        Me.ObjectTree.Location = New System.Drawing.Point(0, 0)
        Me.ObjectTree.Name = "ObjectTree"
        Me.ObjectTree.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ObjectTree.ShowLines = True
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
        Me.SplitPanel2.SizeInfo.AutoSizeScale = New System.Drawing.SizeF(0.2290168!, 0.0!)
        Me.SplitPanel2.SizeInfo.SplitterCorrection = New System.Drawing.Size(191, 0)
        Me.SplitPanel2.TabIndex = 1
        Me.SplitPanel2.TabStop = False
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
        Me.PageObjectProperties.Enabled = False
        Me.PageObjectProperties.Location = New System.Drawing.Point(5, 31)
        Me.PageObjectProperties.Name = "PageObjectProperties"
        Me.PageObjectProperties.Size = New System.Drawing.Size(632, 281)
        Me.PageObjectProperties.Text = "Properties"
        Me.PageObjectProperties.Title = "Object Properties"
        '
        'PageData
        '
        Me.PageData.Controls.Add(Me.DataGrid)
        Me.PageData.Enabled = False
        Me.PageData.Location = New System.Drawing.Point(5, 31)
        Me.PageData.Name = "PageData"
        Me.PageData.Size = New System.Drawing.Size(632, 305)
        Me.PageData.Text = "Data"
        '
        'DataGrid
        '
        Me.DataGrid.AutoSizeRows = True
        Me.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGrid.Location = New System.Drawing.Point(0, 0)
        '
        'DataGrid
        '
        Me.DataGrid.MasterTemplate.EnableFiltering = True
        Me.DataGrid.Modeltable = Nothing
        Me.DataGrid.Name = "DataGrid"
        Me.DataGrid.Size = New System.Drawing.Size(632, 305)
        Me.DataGrid.Status = Nothing
        Me.DataGrid.TabIndex = 0
        Me.DataGrid.Text = "DataGrid"
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
        Me.Menu.Items.AddRange(New Telerik.WinControls.RadItem() {Me.RefreshTreeButton})
        Me.Menu.Location = New System.Drawing.Point(0, 0)
        Me.Menu.Name = "Menu"
        Me.Menu.Padding = New System.Windows.Forms.Padding(2, 2, 0, 0)
        Me.Menu.Size = New System.Drawing.Size(883, 36)
        Me.Menu.TabIndex = 0
        Me.Menu.ThemeName = "TelerikMetroBlue"
        '
        'RefreshTreeButton
        '
        Me.RefreshTreeButton.AccessibleDescription = "Refresh"
        Me.RefreshTreeButton.AccessibleName = "Refresh"
        Me.RefreshTreeButton.Image = Global.OnTrack.AddIn.My.Resources.Resources.playback_reload
        Me.RefreshTreeButton.Name = "RefreshTreeButton"
        Me.RefreshTreeButton.Text = "Refresh"
        Me.RefreshTreeButton.Visibility = Telerik.WinControls.ElementVisibility.Visible
        CType(Me.RefreshTreeButton.GetChildAt(2), Telerik.WinControls.UI.RadMenuItemLayout).ScaleTransform = New System.Drawing.SizeF(1.0!, 1.0!)
        CType(Me.RefreshTreeButton.GetChildAt(2).GetChildAt(0), Telerik.WinControls.UI.MenuImageAndTextLayout).ScaleTransform = New System.Drawing.SizeF(0.5!, 0.5!)
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'UIFormDBExplorer
        '
        Me.AcceptButton = Me.RadCloseButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.RadCloseButton
        Me.ClientSize = New System.Drawing.Size(883, 409)
        Me.Controls.Add(Me.RadSplitContainer1)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.Menu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(400, 200)
        Me.Name = "UIFormDBExplorer"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.Text = "OnTrack Database Explorer"
        Me.ThemeName = "TelerikMetroBlue"
        CType(Me.StatusStrip, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadSplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadSplitContainer1.ResumeLayout(False)
        CType(Me.SplitPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel1.ResumeLayout(False)
        CType(Me.ObjectTree, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitPanel2.ResumeLayout(False)
        CType(Me.RadPageView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPageView.ResumeLayout(False)
        Me.PageData.ResumeLayout(False)
        CType(Me.DataGrid.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadMenu1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Menu, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TelerikMetroBlueTheme1 As Telerik.WinControls.Themes.TelerikMetroBlueTheme
    Friend WithEvents StatusStrip As Telerik.WinControls.UI.RadStatusStrip
    Friend WithEvents RadCloseButton As Telerik.WinControls.UI.RadButtonElement
    Friend WithEvents StatusLabel As Telerik.WinControls.UI.RadLabelElement
    Friend WithEvents RadSplitContainer1 As Telerik.WinControls.UI.RadSplitContainer
    Friend WithEvents SplitPanel1 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents SplitPanel2 As Telerik.WinControls.UI.SplitPanel
    Friend WithEvents ObjectTree As Telerik.WinControls.UI.RadTreeView
    Friend WithEvents RefreshTreeButton As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents Menu As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RadPageView As Telerik.WinControls.UI.RadPageView
    Friend WithEvents PageObjectProperties As Telerik.WinControls.UI.RadPageViewPage
    Friend WithEvents PageData As Telerik.WinControls.UI.RadPageViewPage
    'Friend WithEvents GridView As Telerik.WinControls.UI.RadGridView

    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RadMenu1 As Telerik.WinControls.UI.RadMenu
    Friend WithEvents RefreshTableButton As Telerik.WinControls.UI.RadMenuItem
    Friend WithEvents DataGrid As Global.OnTrack.UI.UIControlDataGridView
End Class

