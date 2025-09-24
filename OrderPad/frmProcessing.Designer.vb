<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProcessing
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProcessing))
        Me.UploadingNotify = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.NotifyMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CancelUploadingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblProcessing = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.NotifyMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'UploadingNotify
        '
        Me.UploadingNotify.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.UploadingNotify.BalloonTipTitle = "OrderPad - Upload Files"
        Me.UploadingNotify.ContextMenuStrip = Me.NotifyMenu
        Me.UploadingNotify.Icon = CType(resources.GetObject("UploadingNotify.Icon"), System.Drawing.Icon)
        Me.UploadingNotify.Visible = True
        '
        'NotifyMenu
        '
        Me.NotifyMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CancelUploadingToolStripMenuItem})
        Me.NotifyMenu.Name = "NotifyMenu"
        Me.NotifyMenu.Size = New System.Drawing.Size(169, 26)
        '
        'CancelUploadingToolStripMenuItem
        '
        Me.CancelUploadingToolStripMenuItem.Name = "CancelUploadingToolStripMenuItem"
        Me.CancelUploadingToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.CancelUploadingToolStripMenuItem.Text = "Cancel Uploading"
        '
        'lblProcessing
        '
        Me.lblProcessing.AutoSize = True
        Me.lblProcessing.Location = New System.Drawing.Point(22, 13)
        Me.lblProcessing.Name = "lblProcessing"
        Me.lblProcessing.Size = New System.Drawing.Size(69, 13)
        Me.lblProcessing.TabIndex = 1
        Me.lblProcessing.Text = "lblProcessing"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(25, 30)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(364, 23)
        Me.ProgressBar1.TabIndex = 2
        '
        'txtLog
        '
        Me.txtLog.BackColor = System.Drawing.SystemColors.Control
        Me.txtLog.Location = New System.Drawing.Point(25, 60)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLog.Size = New System.Drawing.Size(364, 100)
        Me.txtLog.TabIndex = 3
        '
        'frmProcessing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 172)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lblProcessing)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmProcessing"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OrderPad"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.NotifyMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UploadingNotify As System.Windows.Forms.NotifyIcon
    Friend WithEvents NotifyMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CancelUploadingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblProcessing As Label
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents txtLog As TextBox
End Class
