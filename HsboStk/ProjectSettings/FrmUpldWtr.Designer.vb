<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmUpldWtr
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmUpldWtr))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.LblHead = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtExt = New DevExpress.XtraEditors.TextEdit()
        Me.Pic = New DevExpress.XtraEditors.PictureEdit()
        Me.BtnUpload = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVHead = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.repBtnView = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.repBtnDel = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.repchk = New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
        Me.GridView2 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnAssign = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.ProgressBarControl1 = New DevExpress.XtraEditors.ProgressBarControl()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.BtnDelete = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.TxtExt.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Pic.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVHead, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repBtnView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repBtnDel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repchk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(499, 1)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 73
        '
        'LblHead
        '
        Me.LblHead.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblHead.Appearance.Options.UseFont = True
        Me.LblHead.Location = New System.Drawing.Point(9, 7)
        Me.LblHead.Name = "LblHead"
        Me.LblHead.Size = New System.Drawing.Size(149, 16)
        Me.LblHead.TabIndex = 91
        Me.LblHead.Text = "تحديد العلامة المائية للتقرير"
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(448, 20)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(52, 14)
        Me.LabelControl3.TabIndex = 99
        Me.LabelControl3.Text = "رابط الصورة"
        '
        'TxtExt
        '
        Me.TxtExt.Location = New System.Drawing.Point(110, 16)
        Me.TxtExt.Name = "TxtExt"
        Me.TxtExt.Size = New System.Drawing.Size(332, 20)
        Me.TxtExt.TabIndex = 98
        '
        'Pic
        '
        Me.Pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Pic.Cursor = System.Windows.Forms.Cursors.Default
        Me.Pic.Location = New System.Drawing.Point(21, 92)
        Me.Pic.Name = "Pic"
        Me.Pic.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.[Auto]
        Me.Pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch
        Me.Pic.Properties.ZoomAccelerationFactor = 1.0R
        Me.Pic.Size = New System.Drawing.Size(168, 168)
        Me.Pic.TabIndex = 94
        '
        'BtnUpload
        '
        Me.BtnUpload.ImageOptions.Image = CType(resources.GetObject("BtnUpload.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnUpload.Location = New System.Drawing.Point(8, 14)
        Me.BtnUpload.Name = "BtnUpload"
        Me.BtnUpload.Size = New System.Drawing.Size(96, 23)
        Me.BtnUpload.TabIndex = 92
        Me.BtnUpload.Text = "تحميل الصورة"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.BtnUpload)
        Me.GroupBox1.Controls.Add(Me.LabelControl3)
        Me.GroupBox1.Controls.Add(Me.TxtExt)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(507, 48)
        Me.GroupBox1.TabIndex = 100
        Me.GroupBox1.TabStop = False
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(197, 92)
        Me.GridControl1.MainView = Me.GVHead
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repBtnView, Me.repBtnDel, Me.repchk})
        Me.GridControl1.Size = New System.Drawing.Size(315, 168)
        Me.GridControl1.TabIndex = 103
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVHead, Me.GridView2, Me.GridView1})
        '
        'GVHead
        '
        Me.GVHead.Appearance.ColumnFilterButton.Image = CType(resources.GetObject("GVHead.Appearance.ColumnFilterButton.Image"), System.Drawing.Image)
        Me.GVHead.Appearance.ColumnFilterButton.Options.UseImage = True
        Me.GVHead.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.GVHead.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GVHead.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVHead.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVHead.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVHead.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVHead.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVHead.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVHead.Appearance.Row.Options.UseFont = True
        Me.GVHead.Appearance.Row.Options.UseTextOptions = True
        Me.GVHead.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVHead.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVHead.GridControl = Me.GridControl1
        Me.GVHead.Name = "GVHead"
        Me.GVHead.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = False
        Me.GVHead.OptionsFind.FindNullPrompt = ""
        Me.GVHead.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GVHead.OptionsView.ShowGroupPanel = False
        Me.GVHead.OptionsView.ShowIndicator = False
        '
        'repBtnView
        '
        Me.repBtnView.AutoHeight = False
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        EditorButtonImageOptions1.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter
        Me.repBtnView.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(EditorButtonImageOptions1, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, Nothing)})
        Me.repBtnView.Name = "repBtnView"
        Me.repBtnView.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'repBtnDel
        '
        Me.repBtnDel.AutoHeight = False
        EditorButtonImageOptions2.Image = CType(resources.GetObject("EditorButtonImageOptions2.Image"), System.Drawing.Image)
        EditorButtonImageOptions2.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter
        Me.repBtnDel.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(EditorButtonImageOptions2, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, Nothing)})
        Me.repBtnDel.Name = "repBtnDel"
        Me.repBtnDel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'repchk
        '
        Me.repchk.AutoHeight = False
        Me.repchk.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
        Me.repchk.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered
        Me.repchk.Name = "repchk"
        '
        'GridView2
        '
        Me.GridView2.GridControl = Me.GridControl1
        Me.GridView2.Name = "GridView2"
        '
        'GridView1
        '
        Me.GridView1.GridControl = Me.GridControl1
        Me.GridView1.Name = "GridView1"
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(12, 75)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(510, 195)
        Me.GroupBox2.TabIndex = 104
        Me.GroupBox2.TabStop = False
        '
        'BtnAssign
        '
        Me.BtnAssign.ImageOptions.Image = CType(resources.GetObject("BtnAssign.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnAssign.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnAssign.Location = New System.Drawing.Point(291, 275)
        Me.BtnAssign.Name = "BtnAssign"
        Me.BtnAssign.Size = New System.Drawing.Size(73, 56)
        Me.BtnAssign.TabIndex = 107
        Me.BtnAssign.Text = "تحديد الصورة"
        '
        'BtnExit
        '
        Me.BtnExit.ImageOptions.Image = CType(resources.GetObject("BtnExit.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnExit.Location = New System.Drawing.Point(43, 275)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(73, 56)
        Me.BtnExit.TabIndex = 106
        Me.BtnExit.Text = "خروج"
        '
        'BtnSave
        '
        Me.BtnSave.ImageOptions.Image = CType(resources.GetObject("BtnSave.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSave.Location = New System.Drawing.Point(415, 275)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(73, 56)
        Me.BtnSave.TabIndex = 105
        Me.BtnSave.Text = "حفظ"
        '
        'ProgressBarControl1
        '
        Me.ProgressBarControl1.Location = New System.Drawing.Point(39, 249)
        Me.ProgressBarControl1.Name = "ProgressBarControl1"
        Me.ProgressBarControl1.Size = New System.Drawing.Size(437, 41)
        Me.ProgressBarControl1.TabIndex = 108
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'BtnDelete
        '
        Me.BtnDelete.ImageOptions.Image = CType(resources.GetObject("BtnDelete.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnDelete.Location = New System.Drawing.Point(167, 275)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(73, 56)
        Me.BtnDelete.TabIndex = 109
        Me.BtnDelete.Text = "ازالة الصورة"
        '
        'FrmUpldWtr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(528, 340)
        Me.Controls.Add(Me.ProgressBarControl1)
        Me.Controls.Add(Me.BtnAssign)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.Pic)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.LblHead)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.BtnDelete)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmUpldWtr"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmUpldWtr"
        CType(Me.TxtExt.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Pic.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVHead, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repBtnView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repBtnDel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repchk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LblHead As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtExt As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Pic As DevExpress.XtraEditors.PictureEdit
    Friend WithEvents BtnUpload As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVHead As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents repBtnView As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents repBtnDel As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents repchk As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit
    Friend WithEvents GridView2 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridView1 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents BtnAssign As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents ProgressBarControl1 As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents Timer1 As Timer
    Friend WithEvents BtnDelete As DevExpress.XtraEditors.SimpleButton
End Class
