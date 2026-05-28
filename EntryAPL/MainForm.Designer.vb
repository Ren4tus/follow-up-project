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
        Me.dgvData = New System.Windows.Forms.DataGridView()
        Me.txtSearchId = New System.Windows.Forms.TextBox()
        Me.txtSearchName = New System.Windows.Forms.TextBox()
        Me.txtSearchGender = New System.Windows.Forms.TextBox()
        Me.txtSearchAge = New System.Windows.Forms.TextBox()
        Me.txtSearchProcess = New System.Windows.Forms.TextBox()
        Me.txtSearchSource = New System.Windows.Forms.TextBox()
        Me.txtIdInput = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnConfrim = New System.Windows.Forms.Button()
        Me.pnlTop1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pnlBottom1 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.panelFill = New System.Windows.Forms.Panel()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop1.SuspendLayout()
        Me.pnlBottom1.SuspendLayout()
        Me.panelFill.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvData
        '
        Me.dgvData.AllowUserToAddRows = False
        Me.dgvData.AllowUserToDeleteRows = False
        Me.dgvData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvData.Location = New System.Drawing.Point(0, 0)
        Me.dgvData.MultiSelect = False
        Me.dgvData.Name = "dgvData"
        Me.dgvData.ReadOnly = True
        Me.dgvData.RowTemplate.Height = 21
        Me.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvData.Size = New System.Drawing.Size(684, 336)
        Me.dgvData.TabIndex = 0
        '
        'txtSearchId
        '
        Me.txtSearchId.Location = New System.Drawing.Point(40, 12)
        Me.txtSearchId.Name = "txtSearchId"
        Me.txtSearchId.Size = New System.Drawing.Size(95, 19)
        Me.txtSearchId.TabIndex = 1
        '
        'txtSearchName
        '
        Me.txtSearchName.Location = New System.Drawing.Point(190, 12)
        Me.txtSearchName.Name = "txtSearchName"
        Me.txtSearchName.Size = New System.Drawing.Size(95, 19)
        Me.txtSearchName.TabIndex = 2
        '
        'txtSearchGender
        '
        Me.txtSearchGender.Location = New System.Drawing.Point(340, 12)
        Me.txtSearchGender.Name = "txtSearchGender"
        Me.txtSearchGender.Size = New System.Drawing.Size(55, 19)
        Me.txtSearchGender.TabIndex = 3
        '
        'txtSearchAge
        '
        Me.txtSearchAge.Location = New System.Drawing.Point(45, 39)
        Me.txtSearchAge.Name = "txtSearchAge"
        Me.txtSearchAge.Size = New System.Drawing.Size(90, 19)
        Me.txtSearchAge.TabIndex = 4
        '
        'txtSearchProcess
        '
        Me.txtSearchProcess.Location = New System.Drawing.Point(210, 39)
        Me.txtSearchProcess.Name = "txtSearchProcess"
        Me.txtSearchProcess.Size = New System.Drawing.Size(75, 19)
        Me.txtSearchProcess.TabIndex = 5
        '
        'txtSearchSource
        '
        Me.txtSearchSource.Location = New System.Drawing.Point(340, 39)
        Me.txtSearchSource.Name = "txtSearchSource"
        Me.txtSearchSource.Size = New System.Drawing.Size(55, 19)
        Me.txtSearchSource.TabIndex = 6
        '
        'txtIdInput
        '
        Me.txtIdInput.Location = New System.Drawing.Point(160, 12)
        Me.txtIdInput.Name = "txtIdInput"
        Me.txtIdInput.Size = New System.Drawing.Size(150, 19)
        Me.txtIdInput.TabIndex = 7
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(410, 10)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(60, 50)
        Me.btnSearch.TabIndex = 8
        Me.btnSearch.Text = "검색"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnRefresh
        '
        Me.btnRefresh.Location = New System.Drawing.Point(480, 10)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(60, 50)
        Me.btnRefresh.TabIndex = 9
        Me.btnRefresh.Text = "초기화"
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnConfrim
        '
        Me.btnConfrim.Location = New System.Drawing.Point(320, 10)
        Me.btnConfrim.Name = "btnConfrim"
        Me.btnConfrim.Size = New System.Drawing.Size(120, 25)
        Me.btnConfrim.TabIndex = 10
        Me.btnConfrim.Text = "확인 (상세보기)"
        Me.btnConfrim.UseVisualStyleBackColor = True
        '
        'pnlTop1
        '
        Me.pnlTop1.Controls.Add(Me.txtSearchSource)
        Me.pnlTop1.Controls.Add(Me.txtSearchProcess)
        Me.pnlTop1.Controls.Add(Me.txtSearchAge)
        Me.pnlTop1.Controls.Add(Me.txtSearchGender)
        Me.pnlTop1.Controls.Add(Me.txtSearchName)
        Me.pnlTop1.Controls.Add(Me.txtSearchId)
        Me.pnlTop1.Controls.Add(Me.btnRefresh)
        Me.pnlTop1.Controls.Add(Me.btnSearch)
        Me.pnlTop1.Controls.Add(Me.Label1)
        Me.pnlTop1.Controls.Add(Me.Label2)
        Me.pnlTop1.Controls.Add(Me.Label3)
        Me.pnlTop1.Controls.Add(Me.Label4)
        Me.pnlTop1.Controls.Add(Me.Label5)
        Me.pnlTop1.Controls.Add(Me.Label6)
        Me.pnlTop1.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop1.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop1.Name = "pnlTop1"
        Me.pnlTop1.Size = New System.Drawing.Size(684, 75)
        Me.pnlTop1.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(150, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 12)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "이름:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(300, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "성별:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 12)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "나이:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(150, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(43, 12)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "프로세스:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(300, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(25, 12)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "소스:"
        '
        'pnlBottom1
        '
        Me.pnlBottom1.Controls.Add(Me.Label7)
        Me.pnlBottom1.Controls.Add(Me.txtIdInput)
        Me.pnlBottom1.Controls.Add(Me.btnConfrim)
        Me.pnlBottom1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottom1.Location = New System.Drawing.Point(0, 411)
        Me.pnlBottom1.Name = "pnlBottom1"
        Me.pnlBottom1.Size = New System.Drawing.Size(684, 50)
        Me.pnlBottom1.TabIndex = 12
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(10, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(115, 12)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "선택된 ID 또는 직접 입력:"
        '
        'panelFill
        '
        Me.panelFill.Controls.Add(Me.dgvData)
        Me.panelFill.Dock = System.Windows.Forms.DockStyle.Fill
        Me.panelFill.Location = New System.Drawing.Point(0, 75)
        Me.panelFill.Name = "panelFill"
        Me.panelFill.Size = New System.Drawing.Size(684, 336)
        Me.panelFill.TabIndex = 13
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 461)
        Me.Controls.Add(Me.panelFill)
        Me.Controls.Add(Me.pnlBottom1)
        Me.Controls.Add(Me.pnlTop1)
        Me.Name = "MainForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EntryAPL - 데이터 목록"
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop1.ResumeLayout(False)
        Me.pnlTop1.PerformLayout()
        Me.pnlBottom1.ResumeLayout(False)
        Me.pnlBottom1.PerformLayout()
        Me.panelFill.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents dgvData As DataGridView
    Friend WithEvents txtSearchId As TextBox
    Friend WithEvents txtSearchName As TextBox
    Friend WithEvents txtSearchGender As TextBox
    Friend WithEvents txtSearchAge As TextBox
    Friend WithEvents txtSearchProcess As TextBox
    Friend WithEvents txtSearchSource As TextBox
    Friend WithEvents txtIdInput As TextBox
    Friend WithEvents btnSearch As Button
    Friend WithEvents btnRefresh As Button
    Friend WithEvents btnConfrim As Button
    Friend WithEvents pnlTop1 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents pnlBottom1 As Panel
    Friend WithEvents Label7 As Label
    Friend WithEvents panelFill As Panel
End Class