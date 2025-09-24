<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogin))
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.lblExit = New System.Windows.Forms.Label()
        Me.lblLogin = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtUsername
        '
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.txtUsername.Location = New System.Drawing.Point(350, 250)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(356, 27)
        Me.txtUsername.TabIndex = 0
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.txtPassword.Location = New System.Drawing.Point(350, 295)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(355, 27)
        Me.txtPassword.TabIndex = 1
        '
        'lblExit
        '
        Me.lblExit.BackColor = System.Drawing.Color.Transparent
        Me.lblExit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblExit.Image = CType(resources.GetObject("lblExit.Image"), System.Drawing.Image)
        Me.lblExit.Location = New System.Drawing.Point(535, 338)
        Me.lblExit.Name = "lblExit"
        Me.lblExit.Size = New System.Drawing.Size(69, 26)
        Me.lblExit.TabIndex = 6
        '
        'lblLogin
        '
        Me.lblLogin.BackColor = System.Drawing.Color.Transparent
        Me.lblLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblLogin.Image = CType(resources.GetObject("lblLogin.Image"), System.Drawing.Image)
        Me.lblLogin.Location = New System.Drawing.Point(631, 340)
        Me.lblLogin.Name = "lblLogin"
        Me.lblLogin.Size = New System.Drawing.Size(70, 26)
        Me.lblLogin.TabIndex = 7
        '
        'frmLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(911, 453)
        Me.Controls.Add(Me.lblLogin)
        Me.Controls.Add(Me.lblExit)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUsername)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IRS Order Pad - Login"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Private WithEvents lblExit As System.Windows.Forms.Label
    Private WithEvents lblLogin As System.Windows.Forms.Label
End Class
