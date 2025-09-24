<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.btnQueryInvoices = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtInvoiceDetails = New System.Windows.Forms.TextBox()
        Me.btnExportInvoice = New System.Windows.Forms.Button()
        Me.lstInvoices = New System.Windows.Forms.ListBox()
        Me.dlgSave = New System.Windows.Forms.SaveFileDialog()
        Me.lblInvoiceExorted = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkConsiderDuplicatebySSN = New System.Windows.Forms.CheckBox()
        Me.chkShowXMLOnly = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnTestAuth = New System.Windows.Forms.Button()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.chkIgnoreDuplicateInvoices = New System.Windows.Forms.CheckBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(17, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(138, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Invoice Reference Number:"
        '
        'txtRefNo
        '
        Me.txtRefNo.Location = New System.Drawing.Point(161, 106)
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.Size = New System.Drawing.Size(182, 20)
        Me.txtRefNo.TabIndex = 1
        Me.txtRefNo.Text = "07-9901" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'btnQueryInvoices
        '
        Me.btnQueryInvoices.AutoSize = True
        Me.btnQueryInvoices.Location = New System.Drawing.Point(350, 108)
        Me.btnQueryInvoices.Name = "btnQueryInvoices"
        Me.btnQueryInvoices.Size = New System.Drawing.Size(78, 13)
        Me.btnQueryInvoices.TabIndex = 2
        Me.btnQueryInvoices.TabStop = True
        Me.btnQueryInvoices.Text = "Query Invoices"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(23, 157)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Invoices List"
        '
        'txtInvoiceDetails
        '
        Me.txtInvoiceDetails.Location = New System.Drawing.Point(353, 174)
        Me.txtInvoiceDetails.Multiline = True
        Me.txtInvoiceDetails.Name = "txtInvoiceDetails"
        Me.txtInvoiceDetails.ReadOnly = True
        Me.txtInvoiceDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtInvoiceDetails.Size = New System.Drawing.Size(439, 302)
        Me.txtInvoiceDetails.TabIndex = 6
        '
        'btnExportInvoice
        '
        Me.btnExportInvoice.Location = New System.Drawing.Point(648, 482)
        Me.btnExportInvoice.Name = "btnExportInvoice"
        Me.btnExportInvoice.Size = New System.Drawing.Size(144, 23)
        Me.btnExportInvoice.TabIndex = 7
        Me.btnExportInvoice.Text = "Export Invoice"
        Me.btnExportInvoice.UseVisualStyleBackColor = True
        '
        'lstInvoices
        '
        Me.lstInvoices.FormattingEnabled = True
        Me.lstInvoices.Location = New System.Drawing.Point(24, 174)
        Me.lstInvoices.Name = "lstInvoices"
        Me.lstInvoices.Size = New System.Drawing.Size(318, 303)
        Me.lstInvoices.TabIndex = 5
        '
        'lblInvoiceExorted
        '
        Me.lblInvoiceExorted.AutoSize = True
        Me.lblInvoiceExorted.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInvoiceExorted.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblInvoiceExorted.Location = New System.Drawing.Point(423, 485)
        Me.lblInvoiceExorted.Name = "lblInvoiceExorted"
        Me.lblInvoiceExorted.Size = New System.Drawing.Size(223, 17)
        Me.lblInvoiceExorted.TabIndex = 8
        Me.lblInvoiceExorted.Text = "Invoice Exported Successfully"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(351, 157)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Invoice Details"
        '
        'chkConsiderDuplicatebySSN
        '
        Me.chkConsiderDuplicatebySSN.AutoSize = True
        Me.chkConsiderDuplicatebySSN.Checked = True
        Me.chkConsiderDuplicatebySSN.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkConsiderDuplicatebySSN.Location = New System.Drawing.Point(161, 133)
        Me.chkConsiderDuplicatebySSN.Name = "chkConsiderDuplicatebySSN"
        Me.chkConsiderDuplicatebySSN.Size = New System.Drawing.Size(180, 17)
        Me.chkConsiderDuplicatebySSN.TabIndex = 3
        Me.chkConsiderDuplicatebySSN.Text = "Check duplicates based on SSN"
        Me.chkConsiderDuplicatebySSN.UseVisualStyleBackColor = True
        '
        'chkShowXMLOnly
        '
        Me.chkShowXMLOnly.AutoSize = True
        Me.chkShowXMLOnly.Location = New System.Drawing.Point(658, 151)
        Me.chkShowXMLOnly.Name = "chkShowXMLOnly"
        Me.chkShowXMLOnly.Size = New System.Drawing.Size(135, 17)
        Me.chkShowXMLOnly.TabIndex = 4
        Me.chkShowXMLOnly.Text = "Show Output XML only"
        Me.chkShowXMLOnly.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnTestAuth)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(27, 12)
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
        Me.btnTestAuth.TabIndex = 3
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
        Me.txtPassword.TabIndex = 2
        '
        'txtUserName
        '
        Me.txtUserName.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUserName.Location = New System.Drawing.Point(67, 17)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(219, 23)
        Me.txtUserName.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(7, 50)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Password:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 22)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "User Name:"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(22, 507)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(321, 29)
        Me.ProgressBar1.TabIndex = 9
        '
        'lblInfo
        '
        Me.lblInfo.AutoSize = True
        Me.lblInfo.Location = New System.Drawing.Point(27, 491)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(25, 13)
        Me.lblInfo.TabIndex = 10
        Me.lblInfo.Text = "Info"
        '
        'chkIgnoreDuplicateInvoices
        '
        Me.chkIgnoreDuplicateInvoices.AutoSize = True
        Me.chkIgnoreDuplicateInvoices.Checked = True
        Me.chkIgnoreDuplicateInvoices.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIgnoreDuplicateInvoices.Location = New System.Drawing.Point(347, 133)
        Me.chkIgnoreDuplicateInvoices.Name = "chkIgnoreDuplicateInvoices"
        Me.chkIgnoreDuplicateInvoices.Size = New System.Drawing.Size(147, 17)
        Me.chkIgnoreDuplicateInvoices.TabIndex = 3
        Me.chkIgnoreDuplicateInvoices.Text = "Ignore Duplicate Invoices"
        Me.chkIgnoreDuplicateInvoices.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AcceptButton = Me.btnQueryInvoices
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(805, 548)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.chkShowXMLOnly)
        Me.Controls.Add(Me.chkIgnoreDuplicateInvoices)
        Me.Controls.Add(Me.chkConsiderDuplicatebySSN)
        Me.Controls.Add(Me.lblInvoiceExorted)
        Me.Controls.Add(Me.lstInvoices)
        Me.Controls.Add(Me.btnExportInvoice)
        Me.Controls.Add(Me.txtInvoiceDetails)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnQueryInvoices)
        Me.Controls.Add(Me.txtRefNo)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmMain"
        Me.Text = "Quick Books Invoice Export"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRefNo As System.Windows.Forms.TextBox
    Friend WithEvents btnQueryInvoices As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceDetails As System.Windows.Forms.TextBox
    Friend WithEvents btnExportInvoice As System.Windows.Forms.Button
    Friend WithEvents lstInvoices As System.Windows.Forms.ListBox
    Friend WithEvents dlgSave As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lblInvoiceExorted As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkConsiderDuplicatebySSN As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowXMLOnly As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnTestAuth As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents chkIgnoreDuplicateInvoices As CheckBox
End Class
