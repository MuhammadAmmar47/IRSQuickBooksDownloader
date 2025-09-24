<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.btnCustomersDownload = New System.Windows.Forms.Button
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.txtError = New System.Windows.Forms.TextBox
        Me.btnStop = New System.Windows.Forms.Button
        Me.lblProgress = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.btnRevertLastDownload = New System.Windows.Forms.LinkLabel
        Me.btnRefreshBatchNo = New System.Windows.Forms.LinkLabel
        Me.txtLastBatchNo = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.btnUpdateInvNo = New System.Windows.Forms.LinkLabel
        Me.txtStartInvoice = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtLogInvoices = New System.Windows.Forms.TextBox
        Me.dtTo = New System.Windows.Forms.DateTimePicker
        Me.dtFrom = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblInvoicesInfo = New System.Windows.Forms.Label
        Me.pgInvoices = New System.Windows.Forms.ProgressBar
        Me.btnCancelInvoicesDownload = New System.Windows.Forms.Button
        Me.btnInvoicesDownload = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnTestAuth = New System.Windows.Forms.Button
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCustomersDownload
        '
        Me.btnCustomersDownload.Location = New System.Drawing.Point(116, 80)
        Me.btnCustomersDownload.Name = "btnCustomersDownload"
        Me.btnCustomersDownload.Size = New System.Drawing.Size(150, 23)
        Me.btnCustomersDownload.TabIndex = 0
        Me.btnCustomersDownload.Text = "Start Downloading"
        Me.btnCustomersDownload.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(18, 40)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(583, 23)
        Me.ProgressBar1.TabIndex = 0
        '
        'txtError
        '
        Me.txtError.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtError.Location = New System.Drawing.Point(6, 138)
        Me.txtError.Multiline = True
        Me.txtError.Name = "txtError"
        Me.txtError.ReadOnly = True
        Me.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtError.Size = New System.Drawing.Size(768, 298)
        Me.txtError.TabIndex = 3
        '
        'btnStop
        '
        Me.btnStop.Location = New System.Drawing.Point(273, 80)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(154, 23)
        Me.btnStop.TabIndex = 1
        Me.btnStop.Text = "Stop Downloading"
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Location = New System.Drawing.Point(18, 21)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(33, 13)
        Me.lblProgress.TabIndex = 5
        Me.lblProgress.Text = "Label"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(3, 85)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(790, 482)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtError)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.lblProgress)
        Me.TabPage1.Controls.Add(Me.btnCustomersDownload)
        Me.TabPage1.Controls.Add(Me.btnStop)
        Me.TabPage1.Controls.Add(Me.ProgressBar1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(782, 456)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Customers Download"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 122)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 5
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnRevertLastDownload)
        Me.TabPage2.Controls.Add(Me.btnRefreshBatchNo)
        Me.TabPage2.Controls.Add(Me.txtLastBatchNo)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.btnUpdateInvNo)
        Me.TabPage2.Controls.Add(Me.txtStartInvoice)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.txtLogInvoices)
        Me.TabPage2.Controls.Add(Me.dtTo)
        Me.TabPage2.Controls.Add(Me.dtFrom)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.lblInvoicesInfo)
        Me.TabPage2.Controls.Add(Me.pgInvoices)
        Me.TabPage2.Controls.Add(Me.btnCancelInvoicesDownload)
        Me.TabPage2.Controls.Add(Me.btnInvoicesDownload)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(782, 456)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Invoices Download"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnRevertLastDownload
        '
        Me.btnRevertLastDownload.AutoSize = True
        Me.btnRevertLastDownload.Location = New System.Drawing.Point(292, 23)
        Me.btnRevertLastDownload.Name = "btnRevertLastDownload"
        Me.btnRevertLastDownload.Size = New System.Drawing.Size(113, 13)
        Me.btnRevertLastDownload.TabIndex = 0
        Me.btnRevertLastDownload.TabStop = True
        Me.btnRevertLastDownload.Text = "Revert Last Download"
        '
        'btnRefreshBatchNo
        '
        Me.btnRefreshBatchNo.AutoSize = True
        Me.btnRefreshBatchNo.Location = New System.Drawing.Point(241, 23)
        Me.btnRefreshBatchNo.Name = "btnRefreshBatchNo"
        Me.btnRefreshBatchNo.Size = New System.Drawing.Size(44, 13)
        Me.btnRefreshBatchNo.TabIndex = 11
        Me.btnRefreshBatchNo.TabStop = True
        Me.btnRefreshBatchNo.Text = "Refresh"
        '
        'txtLastBatchNo
        '
        Me.txtLastBatchNo.Location = New System.Drawing.Point(166, 19)
        Me.txtLastBatchNo.Name = "txtLastBatchNo"
        Me.txtLastBatchNo.ReadOnly = True
        Me.txtLastBatchNo.Size = New System.Drawing.Size(68, 20)
        Me.txtLastBatchNo.TabIndex = 10
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(93, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 13)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Last Batch #:"
        '
        'btnUpdateInvNo
        '
        Me.btnUpdateInvNo.AutoSize = True
        Me.btnUpdateInvNo.Location = New System.Drawing.Point(639, 22)
        Me.btnUpdateInvNo.Name = "btnUpdateInvNo"
        Me.btnUpdateInvNo.Size = New System.Drawing.Size(42, 13)
        Me.btnUpdateInvNo.TabIndex = 8
        Me.btnUpdateInvNo.TabStop = True
        Me.btnUpdateInvNo.Text = "Update"
        '
        'txtStartInvoice
        '
        Me.txtStartInvoice.Location = New System.Drawing.Point(571, 19)
        Me.txtStartInvoice.MaxLength = 7
        Me.txtStartInvoice.Name = "txtStartInvoice"
        Me.txtStartInvoice.Size = New System.Drawing.Size(62, 20)
        Me.txtStartInvoice.TabIndex = 1
        Me.txtStartInvoice.Text = "1100"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(546, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(22, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "07-"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(455, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Invoice Counter:"
        '
        'txtLogInvoices
        '
        Me.txtLogInvoices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtLogInvoices.Location = New System.Drawing.Point(12, 176)
        Me.txtLogInvoices.Multiline = True
        Me.txtLogInvoices.Name = "txtLogInvoices"
        Me.txtLogInvoices.ReadOnly = True
        Me.txtLogInvoices.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtLogInvoices.Size = New System.Drawing.Size(691, 205)
        Me.txtLogInvoices.TabIndex = 6
        '
        'dtTo
        '
        Me.dtTo.Location = New System.Drawing.Point(335, 103)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(200, 20)
        Me.dtTo.TabIndex = 3
        '
        'dtFrom
        '
        Me.dtFrom.Location = New System.Drawing.Point(93, 103)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(200, 20)
        Me.dtFrom.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(299, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(23, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "To:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(48, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "From:"
        '
        'lblInvoicesInfo
        '
        Me.lblInvoicesInfo.AutoSize = True
        Me.lblInvoicesInfo.Location = New System.Drawing.Point(9, 50)
        Me.lblInvoicesInfo.Name = "lblInvoicesInfo"
        Me.lblInvoicesInfo.Size = New System.Drawing.Size(39, 13)
        Me.lblInvoicesInfo.TabIndex = 2
        Me.lblInvoicesInfo.Text = "Label2"
        '
        'pgInvoices
        '
        Me.pgInvoices.Location = New System.Drawing.Point(8, 69)
        Me.pgInvoices.Name = "pgInvoices"
        Me.pgInvoices.Size = New System.Drawing.Size(673, 23)
        Me.pgInvoices.TabIndex = 1
        '
        'btnCancelInvoicesDownload
        '
        Me.btnCancelInvoicesDownload.Location = New System.Drawing.Point(301, 134)
        Me.btnCancelInvoicesDownload.Name = "btnCancelInvoicesDownload"
        Me.btnCancelInvoicesDownload.Size = New System.Drawing.Size(188, 23)
        Me.btnCancelInvoicesDownload.TabIndex = 5
        Me.btnCancelInvoicesDownload.Text = "Cancel Download"
        Me.btnCancelInvoicesDownload.UseVisualStyleBackColor = True
        '
        'btnInvoicesDownload
        '
        Me.btnInvoicesDownload.Location = New System.Drawing.Point(107, 134)
        Me.btnInvoicesDownload.Name = "btnInvoicesDownload"
        Me.btnInvoicesDownload.Size = New System.Drawing.Size(188, 23)
        Me.btnInvoicesDownload.TabIndex = 4
        Me.btnInvoicesDownload.Text = "Invoices Download"
        Me.btnInvoicesDownload.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnTestAuth)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(419, 74)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Authentication"
        '
        'btnTestAuth
        '
        Me.btnTestAuth.Location = New System.Drawing.Point(295, 22)
        Me.btnTestAuth.Name = "btnTestAuth"
        Me.btnTestAuth.Size = New System.Drawing.Size(104, 37)
        Me.btnTestAuth.TabIndex = 2
        Me.btnTestAuth.Text = "Authenticate"
        Me.btnTestAuth.UseVisualStyleBackColor = True
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(67, 45)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(219, 23)
        Me.txtPassword.TabIndex = 1
        '
        'txtUserName
        '
        Me.txtUserName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.Location = New System.Drawing.Point(67, 17)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(219, 23)
        Me.txtUserName.TabIndex = 0
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(7, 50)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(56, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Password:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(7, 22)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(63, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "User Name:"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(796, 571)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(735, 451)
        Me.Name = "Form1"
        Me.Text = "IRS Tax Records [v3.0]"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCustomersDownload As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents txtError As System.Windows.Forms.TextBox
    Friend WithEvents btnStop As System.Windows.Forms.Button
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents btnInvoicesDownload As System.Windows.Forms.Button
    Friend WithEvents pgInvoices As System.Windows.Forms.ProgressBar
    Friend WithEvents lblInvoicesInfo As System.Windows.Forms.Label
    Friend WithEvents btnCancelInvoicesDownload As System.Windows.Forms.Button
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLogInvoices As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtStartInvoice As System.Windows.Forms.TextBox
    Friend WithEvents btnUpdateInvNo As System.Windows.Forms.LinkLabel
    Friend WithEvents txtLastBatchNo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnRefreshBatchNo As System.Windows.Forms.LinkLabel
    Friend WithEvents btnRevertLastDownload As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTestAuth As System.Windows.Forms.Button
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label

End Class
