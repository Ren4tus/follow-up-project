<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSearchId = New System.Windows.Forms.TextBox()
        Me.txtSearchAge = New System.Windows.Forms.TextBox()
        Me.txtSearchName = New System.Windows.Forms.TextBox()
        Me.txtSearchProcess = New System.Windows.Forms.TextBox()
        Me.txtSearchGender = New System.Windows.Forms.TextBox()
        Me.txtSearchSource = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtIdInput = New System.Windows.Forms.TextBox()
        Me.btnConfirm = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.dgvData = New System.Windows.Forms.DataGridView()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(35, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(35, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "나이:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(201, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "이름:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(201, 54)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 12)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "프로세스:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(361, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(25, 12)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "성별:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(361, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(25, 12)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "소스:"
        '
        'txtSearchId
        '
        Me.txtSearchId.Location = New System.Drawing.Point(79, 20)
        Me.txtSearchId.Name = "txtSearchId"
        Me.txtSearchId.Size = New System.Drawing.Size(100, 19)
        Me.txtSearchId.TabIndex = 6
        '
        'txtSearchAge
        '
        Me.txtSearchAge.Location = New System.Drawing.Point(79, 51)
        Me.txtSearchAge.Name = "txtSearchAge"
        Me.txtSearchAge.Size = New System.Drawing.Size(100, 19)
        Me.txtSearchAge.TabIndex = 7
        '
        'txtSearchName
        '
        Me.txtSearchName.Location = New System.Drawing.Point(245, 20)
        Me.txtSearchName.Name = "txtSearchName"
        Me.txtSearchName.Size = New System.Drawing.Size(100, 19)
        Me.txtSearchName.TabIndex = 8
        '
        'txtSearchProcess
        '
        Me.txtSearchProcess.Location = New System.Drawing.Point(245, 51)
        Me.txtSearchProcess.Name = "txtSearchProcess"
        Me.txtSearchProcess.Size = New System.Drawing.Size(100, 19)
        Me.txtSearchProcess.TabIndex = 9
        '
        'txtSearchGender
        '
        Me.txtSearchGender.Location = New System.Drawing.Point(405, 20)
        Me.txtSearchGender.Name = "txtSearchGender"
        Me.txtSearchGender.Size = New System.Drawing.Size(100, 19)
        Me.txtSearchGender.TabIndex = 10
        '
        'txtSearchSource
        '
        Me.txtSearchSource.Location = New System.Drawing.Point(405, 51)
        Me.txtSearchSource.Name = "txtSearchSource"
        Me.txtSearchSource.Size = New System.Drawing.Size(100, 19)
        Me.txtSearchSource.TabIndex = 11
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(525, 34)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 12
        Me.btnSearch.Text = "검색"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(606, 34)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(75, 23)
        Me.btnRefresh.TabIndex = 13
        Me.btnRefresh.Text = "초기화"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(30, 38)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(115, 12)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "선택된 ID 또는 직접 입력:"
        '
        'txtIdInput
        '
        Me.txtIdInput.Location = New System.Drawing.Point(152, 35)
        Me.txtIdInput.Name = "txtIdInput"
        Me.txtIdInput.Size = New System.Drawing.Size(100, 19)
        Me.txtIdInput.TabIndex = 16
        '
        'btnConfirm
        '
        Me.btnConfirm.Location = New System.Drawing.Point(258, 33)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(87, 23)
        Me.btnConfirm.TabIndex = 17
        Me.btnConfirm.Text = "확인 (상세보기)"
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnSearch)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.btnRefresh)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtSearchSource)
        Me.Panel1.Controls.Add(Me.txtSearchId)
        Me.Panel1.Controls.Add(Me.txtSearchGender)
        Me.Panel1.Controls.Add(Me.txtSearchAge)
        Me.Panel1.Controls.Add(Me.txtSearchProcess)
        Me.Panel1.Controls.Add(Me.txtSearchName)
        Me.Panel1.Location = New System.Drawing.Point(1, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(742, 95)
        Me.Panel1.TabIndex = 19
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnConfirm)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.txtIdInput)
        Me.Panel2.Location = New System.Drawing.Point(1, 353)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(742, 77)
        Me.Panel2.TabIndex = 20
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.dgvData)
        Me.Panel3.Location = New System.Drawing.Point(1, 94)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(742, 262)
        Me.Panel3.TabIndex = 21
        '
        'dgvData
        '
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvData.Location = New System.Drawing.Point(0, 0)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.RowTemplate.Height = 21
        Me.dgvData.Size = New System.Drawing.Size(742, 262)
        Me.dgvData.TabIndex = 0
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 431)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "MainForm"
        Me.Text = "MainForm"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents txtSearchId As TextBox
    Friend WithEvents txtSearchAge As TextBox
    Friend WithEvents txtSearchName As TextBox
    Friend WithEvents txtSearchProcess As TextBox
    Friend WithEvents txtSearchGender As TextBox
    Friend WithEvents txtSearchSource As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents txtIdInput As TextBox
    Friend WithEvents btnConfirm As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents dgvData As DataGridView
End Class