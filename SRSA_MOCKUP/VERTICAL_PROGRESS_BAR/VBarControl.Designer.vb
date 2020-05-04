<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VBarControl
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
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
        Me.Vbar = New VERTICAL_PROGRESS_BAR.VBAR()
        Me.SuspendLayout()
        '
        'Vbar
        '
        Me.Vbar.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Vbar.BackColor = System.Drawing.SystemColors.Control
        Me.Vbar.Location = New System.Drawing.Point(3, 3)
        Me.Vbar.Name = "Vbar"
        Me.Vbar.Size = New System.Drawing.Size(19, 125)
        Me.Vbar.TabIndex = 0
        Me.Vbar.Value = 50
        '
        'VBarControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Vbar)
        Me.Name = "VBarControl"
        Me.Size = New System.Drawing.Size(25, 135)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Vbar As VERTICAL_PROGRESS_BAR.VBAR

End Class
