<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UIControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.RadRichTextBox1 = New Telerik.WinControls.RichTextBox.RadRichTextBox()
        CType(Me.RadRichTextBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadRichTextBox1
        '
        Me.RadRichTextBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RadRichTextBox1.HyperlinkToolTipFormatString = Nothing
        Me.RadRichTextBox1.Location = New System.Drawing.Point(0, 0)
        Me.RadRichTextBox1.Name = "RadRichTextBox1"
        Me.RadRichTextBox1.Size = New System.Drawing.Size(150, 150)
        Me.RadRichTextBox1.TabIndex = 0
        Me.RadRichTextBox1.Text = "RadRichTextBox1"
        '
        'UserControl1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.RadRichTextBox1)
        Me.Name = "UserControl1"
        CType(Me.RadRichTextBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RadRichTextBox1 As Telerik.WinControls.RichTextBox.RadRichTextBox

End Class
