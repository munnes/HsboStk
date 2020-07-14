<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUnit
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmUnit))
        Me.TxtName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtID = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.ProgressBarControl1 = New DevExpress.XtraEditors.ProgressBarControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.TxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(24, 70)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(321, 20)
        Me.TxtName.TabIndex = 7
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(356, 33)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(32, 14)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "الرقم: "
        '
        'TxtID
        '
        Me.TxtID.Location = New System.Drawing.Point(245, 34)
        Me.TxtID.Name = "TxtID"
        Me.TxtID.Properties.ReadOnly = True
        Me.TxtID.Size = New System.Drawing.Size(100, 20)
        Me.TxtID.TabIndex = 4
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(351, 70)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(36, 14)
        Me.LabelControl2.TabIndex = 6
        Me.LabelControl2.Text = "الوحدة:"
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(386, 3)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 52
        '
        'BtnAdd
        '
        Me.BtnAdd.ImageOptions.Image = CType(resources.GetObject("BtnAdd.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnAdd.Location = New System.Drawing.Point(331, 108)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(73, 56)
        Me.BtnAdd.TabIndex = 53
        Me.BtnAdd.Text = "جديد"
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSearch.Location = New System.Drawing.Point(115, 108)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(73, 56)
        Me.BtnSearch.TabIndex = 55
        Me.BtnSearch.Text = "بحث"
        '
        'BtnSave
        '
        Me.BtnSave.ImageOptions.Image = CType(resources.GetObject("BtnSave.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSave.Location = New System.Drawing.Point(223, 108)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(73, 56)
        Me.BtnSave.TabIndex = 56
        Me.BtnSave.Text = "حفظ"
        '
        'BtnDelete
        '
        Me.BtnDelete.ImageOptions.Image = CType(resources.GetObject("BtnDelete.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnDelete.Location = New System.Drawing.Point(7, 108)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(73, 56)
        Me.BtnDelete.TabIndex = 54
        Me.BtnDelete.Text = "حذف"
        '
        'ProgressBarControl1
        '
        Me.ProgressBarControl1.Location = New System.Drawing.Point(24, 97)
        Me.ProgressBarControl1.Name = "ProgressBarControl1"
        Me.ProgressBarControl1.Size = New System.Drawing.Size(342, 35)
        Me.ProgressBarControl1.TabIndex = 57
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(7, 6)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(78, 16)
        Me.LabelControl3.TabIndex = 58
        Me.LabelControl3.Text = "وحدات القياس"
        '
        'FrmUnit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(414, 175)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.ProgressBarControl1)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TxtID)
        Me.Controls.Add(Me.LabelControl2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmUnit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmUnit"
        CType(Me.TxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ProgressBarControl1 As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
End Class
