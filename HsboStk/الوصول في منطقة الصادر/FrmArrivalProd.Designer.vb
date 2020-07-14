<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmArrivalProd
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
        Dim GridLevelNode2 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmArrivalProd))
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.ProgressBarControl1 = New DevExpress.XtraEditors.ProgressBarControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVArvExpDet = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.repPrd = New DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit()
        Me.Txt = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.repTxt = New DevExpress.XtraEditors.Repository.RepositoryItemTextEdit()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LokExp = New DevExpress.XtraEditors.LookUpEdit()
        Me.TxtRId = New DevExpress.XtraEditors.TextEdit()
        Me.DateArvExp = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.LokLoc = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LblHead = New DevExpress.XtraEditors.LabelControl()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupControl2 = New DevExpress.XtraEditors.GroupControl()
        Me.TxtArExpInf = New DevExpress.XtraEditors.MemoEdit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVArvExpDet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repPrd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Txt, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repTxt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.LokExp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtRId.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateArvExp.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DateArvExp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl2.SuspendLayout()
        CType(Me.TxtArExpInf.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupControl1
        '
        Me.GroupControl1.Appearance.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GroupControl1.Appearance.Options.UseFont = True
        Me.GroupControl1.Controls.Add(Me.ProgressBarControl1)
        Me.GroupControl1.Controls.Add(Me.GridControl1)
        Me.GroupControl1.Location = New System.Drawing.Point(10, 107)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(875, 239)
        Me.GroupControl1.TabIndex = 77
        Me.GroupControl1.Text = "تفاصيل الوصول"
        '
        'ProgressBarControl1
        '
        Me.ProgressBarControl1.Location = New System.Drawing.Point(213, 146)
        Me.ProgressBarControl1.Name = "ProgressBarControl1"
        Me.ProgressBarControl1.Size = New System.Drawing.Size(445, 62)
        Me.ProgressBarControl1.TabIndex = 51
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        GridLevelNode2.RelationName = "Level1"
        Me.GridControl1.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode2})
        Me.GridControl1.Location = New System.Drawing.Point(2, 20)
        Me.GridControl1.MainView = Me.GVArvExpDet
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repPrd, Me.Txt, Me.repTxt})
        Me.GridControl1.Size = New System.Drawing.Size(871, 217)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVArvExpDet})
        '
        'GVArvExpDet
        '
        Me.GVArvExpDet.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVArvExpDet.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVArvExpDet.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVArvExpDet.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVArvExpDet.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVArvExpDet.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVArvExpDet.Appearance.Row.Options.UseFont = True
        Me.GVArvExpDet.Appearance.Row.Options.UseTextOptions = True
        Me.GVArvExpDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVArvExpDet.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVArvExpDet.GridControl = Me.GridControl1
        Me.GVArvExpDet.Name = "GVArvExpDet"
        Me.GVArvExpDet.OptionsFind.FindNullPrompt = ""
        Me.GVArvExpDet.OptionsView.ShowGroupPanel = False
        Me.GVArvExpDet.OptionsView.ShowIndicator = False
        '
        'repPrd
        '
        Me.repPrd.AutoHeight = False
        Me.repPrd.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.repPrd.Name = "repPrd"
        '
        'Txt
        '
        Me.Txt.AutoHeight = False
        Me.Txt.Name = "Txt"
        '
        'repTxt
        '
        Me.repTxt.AutoHeight = False
        Me.repTxt.Name = "repTxt"
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(870, 3)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 76
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.LabelControl6)
        Me.GroupBox1.Controls.Add(Me.LokExp)
        Me.GroupBox1.Controls.Add(Me.TxtRId)
        Me.GroupBox1.Controls.Add(Me.DateArvExp)
        Me.GroupBox1.Controls.Add(Me.LabelControl7)
        Me.GroupBox1.Controls.Add(Me.LokLoc)
        Me.GroupBox1.Controls.Add(Me.LabelControl4)
        Me.GroupBox1.Controls.Add(Me.LabelControl5)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(10, 20)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(876, 80)
        Me.GroupBox1.TabIndex = 74
        Me.GroupBox1.TabStop = False
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Location = New System.Drawing.Point(780, 55)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(83, 14)
        Me.LabelControl6.TabIndex = 69
        Me.LabelControl6.Text = "وصول في منطقة"
        '
        'LokExp
        '
        Me.LokExp.EditValue = ""
        Me.LokExp.Location = New System.Drawing.Point(487, 49)
        Me.LokExp.Name = "LokExp"
        Me.LokExp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokExp.Properties.ShowFooter = False
        Me.LokExp.Properties.ShowHeader = False
        Me.LokExp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokExp.Size = New System.Drawing.Size(287, 20)
        Me.LokExp.TabIndex = 70
        '
        'TxtRId
        '
        Me.TxtRId.Location = New System.Drawing.Point(674, 20)
        Me.TxtRId.Name = "TxtRId"
        Me.TxtRId.Properties.ReadOnly = True
        Me.TxtRId.Size = New System.Drawing.Size(100, 20)
        Me.TxtRId.TabIndex = 47
        '
        'DateArvExp
        '
        Me.DateArvExp.EditValue = Nothing
        Me.DateArvExp.Location = New System.Drawing.Point(20, 20)
        Me.DateArvExp.Name = "DateArvExp"
        Me.DateArvExp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateArvExp.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.DateArvExp.Size = New System.Drawing.Size(175, 20)
        Me.DateArvExp.TabIndex = 68
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl7.Appearance.Options.UseFont = True
        Me.LabelControl7.Location = New System.Drawing.Point(313, 55)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(92, 14)
        Me.LabelControl7.TabIndex = 43
        Me.LabelControl7.Text = "مشحون من محطة"
        '
        'LokLoc
        '
        Me.LokLoc.EditValue = ""
        Me.LokLoc.Location = New System.Drawing.Point(20, 53)
        Me.LokLoc.Name = "LokLoc"
        Me.LokLoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokLoc.Properties.ShowFooter = False
        Me.LokLoc.Properties.ShowHeader = False
        Me.LokLoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokLoc.Size = New System.Drawing.Size(287, 20)
        Me.LokLoc.TabIndex = 44
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(787, 26)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(24, 14)
        Me.LabelControl4.TabIndex = 48
        Me.LabelControl4.Text = "الرقم"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Location = New System.Drawing.Point(201, 26)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(59, 14)
        Me.LabelControl5.TabIndex = 49
        Me.LabelControl5.Text = "تاريخ الوصول"
        '
        'LblHead
        '
        Me.LblHead.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblHead.Appearance.Options.UseFont = True
        Me.LblHead.Location = New System.Drawing.Point(12, 3)
        Me.LblHead.Name = "LblHead"
        Me.LblHead.Size = New System.Drawing.Size(134, 16)
        Me.LblHead.TabIndex = 75
        Me.LblHead.Text = "الوصول في منطقة الصادر"
        '
        'BtnExit
        '
        Me.BtnExit.ImageOptions.Image = CType(resources.GetObject("BtnExit.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnExit.Location = New System.Drawing.Point(30, 449)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(73, 56)
        Me.BtnExit.TabIndex = 83
        Me.BtnExit.Text = "خروج"
        '
        'BtnPrint
        '
        Me.BtnPrint.ImageOptions.Image = CType(resources.GetObject("BtnPrint.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnPrint.Location = New System.Drawing.Point(194, 449)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(73, 56)
        Me.BtnPrint.TabIndex = 82
        Me.BtnPrint.Text = "طباعة"
        '
        'BtnDelete
        '
        Me.BtnDelete.ImageOptions.Image = CType(resources.GetObject("BtnDelete.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnDelete.Location = New System.Drawing.Point(429, 449)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(73, 56)
        Me.BtnDelete.TabIndex = 81
        Me.BtnDelete.Text = "حذف"
        '
        'BtnAdd
        '
        Me.BtnAdd.ImageOptions.Image = CType(resources.GetObject("BtnAdd.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnAdd.Location = New System.Drawing.Point(797, 449)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(73, 56)
        Me.BtnAdd.TabIndex = 78
        Me.BtnAdd.Text = "جديد"
        '
        'BtnSave
        '
        Me.BtnSave.ImageOptions.Image = CType(resources.GetObject("BtnSave.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSave.Location = New System.Drawing.Point(679, 449)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(73, 56)
        Me.BtnSave.TabIndex = 79
        Me.BtnSave.Text = "حفظ"
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSearch.Location = New System.Drawing.Point(553, 449)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(73, 56)
        Me.BtnSearch.TabIndex = 80
        Me.BtnSearch.Text = "بحث"
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'GroupControl2
        '
        Me.GroupControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupControl2.Appearance.Options.UseFont = True
        Me.GroupControl2.Controls.Add(Me.TxtArExpInf)
        Me.GroupControl2.Location = New System.Drawing.Point(12, 352)
        Me.GroupControl2.Name = "GroupControl2"
        Me.GroupControl2.Size = New System.Drawing.Size(871, 93)
        Me.GroupControl2.TabIndex = 84
        Me.GroupControl2.Text = "بيان"
        '
        'TxtArExpInf
        '
        Me.TxtArExpInf.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtArExpInf.Location = New System.Drawing.Point(2, 20)
        Me.TxtArExpInf.Name = "TxtArExpInf"
        Me.TxtArExpInf.Size = New System.Drawing.Size(867, 71)
        Me.TxtArExpInf.TabIndex = 62
        '
        'FrmArrivalProd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(896, 510)
        Me.Controls.Add(Me.LblHead)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.GroupControl2)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmArrivalProd"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmArrivalProd"
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVArvExpDet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repPrd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Txt, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repTxt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.LokExp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtRId.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateArvExp.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DateArvExp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl2.ResumeLayout(False)
        CType(Me.TxtArExpInf.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents ProgressBarControl1 As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVArvExpDet As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents repPrd As DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit
    Friend WithEvents Txt As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LokExp As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents TxtRId As DevExpress.XtraEditors.TextEdit
    Friend WithEvents DateArvExp As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LokLoc As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblHead As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As Timer
    Friend WithEvents GroupControl2 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents TxtArExpInf As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents repTxt As DevExpress.XtraEditors.Repository.RepositoryItemTextEdit
End Class
