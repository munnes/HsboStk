﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBuyStore
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBuyStore))
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.ProgressBarControl1 = New DevExpress.XtraEditors.ProgressBarControl()
        Me.BtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtID = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LokLoc = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(4, 4)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(113, 16)
        Me.LabelControl3.TabIndex = 80
        Me.LabelControl3.Text = "مخازن مناطق الشراء"
        '
        'ProgressBarControl1
        '
        Me.ProgressBarControl1.Location = New System.Drawing.Point(22, 133)
        Me.ProgressBarControl1.Name = "ProgressBarControl1"
        Me.ProgressBarControl1.Size = New System.Drawing.Size(342, 35)
        Me.ProgressBarControl1.TabIndex = 79
        '
        'BtnAdd
        '
        Me.BtnAdd.ImageOptions.Image = CType(resources.GetObject("BtnAdd.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnAdd.Location = New System.Drawing.Point(333, 152)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(73, 56)
        Me.BtnAdd.TabIndex = 75
        Me.BtnAdd.Text = "جديد"
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSearch.Location = New System.Drawing.Point(117, 152)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(73, 56)
        Me.BtnSearch.TabIndex = 77
        Me.BtnSearch.Text = "بحث"
        '
        'BtnSave
        '
        Me.BtnSave.ImageOptions.Image = CType(resources.GetObject("BtnSave.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSave.Location = New System.Drawing.Point(225, 152)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(73, 56)
        Me.BtnSave.TabIndex = 78
        Me.BtnSave.Text = "حفظ"
        '
        'BtnDelete
        '
        Me.BtnDelete.ImageOptions.Image = CType(resources.GetObject("BtnDelete.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnDelete.Location = New System.Drawing.Point(9, 152)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(73, 56)
        Me.BtnDelete.TabIndex = 76
        Me.BtnDelete.Text = "حذف"
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(386, 4)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 74
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(9, 108)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(318, 20)
        Me.TxtName.TabIndex = 73
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(338, 33)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(32, 14)
        Me.LabelControl1.TabIndex = 71
        Me.LabelControl1.Text = "الرقم: "
        '
        'TxtID
        '
        Me.TxtID.Location = New System.Drawing.Point(227, 34)
        Me.TxtID.Name = "TxtID"
        Me.TxtID.Properties.ReadOnly = True
        Me.TxtID.Size = New System.Drawing.Size(100, 20)
        Me.TxtID.TabIndex = 70
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(331, 70)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(71, 14)
        Me.LabelControl2.TabIndex = 72
        Me.LabelControl2.Text = "منطقة الشراء:"
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(331, 109)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(38, 14)
        Me.LabelControl4.TabIndex = 82
        Me.LabelControl4.Text = "المخزن:"
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'LokLoc
        '
        Me.LokLoc.EditValue = ""
        Me.LokLoc.Location = New System.Drawing.Point(9, 71)
        Me.LokLoc.Name = "LokLoc"
        Me.LokLoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokLoc.Properties.ShowFooter = False
        Me.LokLoc.Properties.ShowHeader = False
        Me.LokLoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokLoc.Size = New System.Drawing.Size(318, 20)
        Me.LokLoc.TabIndex = 83
        '
        'FrmBuyStore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 220)
        Me.Controls.Add(Me.LokLoc)
        Me.Controls.Add(Me.LabelControl4)
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
        Me.Name = "FrmBuyStore"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmBuyStore"
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ProgressBarControl1 As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents BtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents LokLoc As DevExpress.XtraEditors.LookUpEdit
End Class
