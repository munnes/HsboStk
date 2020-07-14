<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmViewShpPlr
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmViewShpPlr))
        Dim EditorButtonImageOptions3 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim EditorButtonImageOptions4 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.LblHead = New DevExpress.XtraEditors.LabelControl()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.repBtnDel = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.repBtnView = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.GVShpView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.btnAr = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.btnLok = New DevExpress.XtraEditors.SimpleButton()
        Me.LokLoc = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.ToDate = New DevExpress.XtraEditors.DateEdit()
        Me.LokExp = New DevExpress.XtraEditors.LookUpEdit()
        Me.btnStore = New DevExpress.XtraEditors.SimpleButton()
        Me.FromDate = New DevExpress.XtraEditors.DateEdit()
        Me.LokPeeler = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.repBtnDel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repBtnView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVShpView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokExp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokPeeler.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(803, 4)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 78
        '
        'LblHead
        '
        Me.LblHead.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblHead.Appearance.Options.UseFont = True
        Me.LblHead.Location = New System.Drawing.Point(4, 8)
        Me.LblHead.Name = "LblHead"
        Me.LblHead.Size = New System.Drawing.Size(147, 16)
        Me.LblHead.TabIndex = 84
        Me.LblHead.Text = "الشحن من القشارة\الغربال"
        '
        'BtnExit
        '
        Me.BtnExit.ImageOptions.Image = CType(resources.GetObject("BtnExit.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnExit.Location = New System.Drawing.Point(65, 156)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(162, 56)
        Me.BtnExit.TabIndex = 82
        Me.BtnExit.Text = "خروج"
        '
        'BtnPrint
        '
        Me.BtnPrint.ImageOptions.Image = CType(resources.GetObject("BtnPrint.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnPrint.Location = New System.Drawing.Point(331, 156)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(162, 56)
        Me.BtnPrint.TabIndex = 81
        Me.BtnPrint.Text = "طباعة"
        '
        'repBtnDel
        '
        Me.repBtnDel.AutoHeight = False
        EditorButtonImageOptions3.Image = CType(resources.GetObject("EditorButtonImageOptions3.Image"), System.Drawing.Image)
        EditorButtonImageOptions3.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter
        Me.repBtnDel.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(EditorButtonImageOptions3, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, Nothing)})
        Me.repBtnDel.Name = "repBtnDel"
        Me.repBtnDel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'repBtnView
        '
        Me.repBtnView.AutoHeight = False
        EditorButtonImageOptions4.Image = CType(resources.GetObject("EditorButtonImageOptions4.Image"), System.Drawing.Image)
        EditorButtonImageOptions4.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter
        Me.repBtnView.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(EditorButtonImageOptions4, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, Nothing)})
        Me.repBtnView.Name = "repBtnView"
        Me.repBtnView.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'GVShpView
        '
        Me.GVShpView.Appearance.ColumnFilterButton.Image = CType(resources.GetObject("GVShpView.Appearance.ColumnFilterButton.Image"), System.Drawing.Image)
        Me.GVShpView.Appearance.ColumnFilterButton.Options.UseImage = True
        Me.GVShpView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVShpView.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVShpView.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVShpView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVShpView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVShpView.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVShpView.Appearance.Row.Options.UseFont = True
        Me.GVShpView.Appearance.Row.Options.UseTextOptions = True
        Me.GVShpView.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVShpView.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVShpView.GridControl = Me.GridControl1
        Me.GVShpView.Name = "GVShpView"
        Me.GVShpView.OptionsFind.FindNullPrompt = ""
        Me.GVShpView.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.GVShpView.OptionsView.ShowGroupPanel = False
        Me.GVShpView.OptionsView.ShowIndicator = False
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(6, 217)
        Me.GridControl1.MainView = Me.GVShpView
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repBtnView, Me.repBtnDel})
        Me.GridControl1.Size = New System.Drawing.Size(817, 256)
        Me.GridControl1.TabIndex = 80
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVShpView})
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSearch.Location = New System.Drawing.Point(597, 156)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(162, 56)
        Me.BtnSearch.TabIndex = 83
        Me.BtnSearch.Text = "بحث"
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Location = New System.Drawing.Point(714, 93)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(97, 14)
        Me.LabelControl6.TabIndex = 67
        Me.LabelControl6.Text = "مشحون الى منطقة"
        '
        'btnAr
        '
        Me.btnAr.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.btnAr.Appearance.Options.UseBackColor = True
        Me.btnAr.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnAr.ImageOptions.Image = CType(resources.GetObject("btnAr.ImageOptions.Image"), System.Drawing.Image)
        Me.btnAr.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnAr.Location = New System.Drawing.Point(435, 87)
        Me.btnAr.Name = "btnAr"
        Me.btnAr.Size = New System.Drawing.Size(25, 25)
        Me.btnAr.TabIndex = 66
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Location = New System.Drawing.Point(736, 24)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(41, 14)
        Me.LabelControl5.TabIndex = 49
        Me.LabelControl5.Text = "من تاريخ"
        '
        'btnLok
        '
        Me.btnLok.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.btnLok.Appearance.Options.UseBackColor = True
        Me.btnLok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnLok.ImageOptions.Image = CType(resources.GetObject("btnLok.ImageOptions.Image"), System.Drawing.Image)
        Me.btnLok.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnLok.Location = New System.Drawing.Point(435, 54)
        Me.btnLok.Name = "btnLok"
        Me.btnLok.Size = New System.Drawing.Size(25, 25)
        Me.btnLok.TabIndex = 63
        '
        'LokLoc
        '
        Me.LokLoc.EditValue = ""
        Me.LokLoc.Location = New System.Drawing.Point(463, 57)
        Me.LokLoc.Name = "LokLoc"
        Me.LokLoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokLoc.Properties.ShowFooter = False
        Me.LokLoc.Properties.ShowHeader = False
        Me.LokLoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokLoc.Size = New System.Drawing.Size(246, 20)
        Me.LokLoc.TabIndex = 44
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(719, 60)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(92, 14)
        Me.LabelControl3.TabIndex = 43
        Me.LabelControl3.Text = "مشحون من محطة"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.LabelControl6)
        Me.GroupBox1.Controls.Add(Me.btnAr)
        Me.GroupBox1.Controls.Add(Me.LabelControl4)
        Me.GroupBox1.Controls.Add(Me.ToDate)
        Me.GroupBox1.Controls.Add(Me.LokExp)
        Me.GroupBox1.Controls.Add(Me.LabelControl5)
        Me.GroupBox1.Controls.Add(Me.btnStore)
        Me.GroupBox1.Controls.Add(Me.FromDate)
        Me.GroupBox1.Controls.Add(Me.LokPeeler)
        Me.GroupBox1.Controls.Add(Me.btnLok)
        Me.GroupBox1.Controls.Add(Me.LabelControl2)
        Me.GroupBox1.Controls.Add(Me.LokLoc)
        Me.GroupBox1.Controls.Add(Me.LabelControl3)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(4, 22)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(819, 126)
        Me.GroupBox1.TabIndex = 79
        Me.GroupBox1.TabStop = False
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(204, 24)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(44, 14)
        Me.LabelControl4.TabIndex = 55
        Me.LabelControl4.Text = "الى تاريخ"
        '
        'ToDate
        '
        Me.ToDate.EditValue = Nothing
        Me.ToDate.Location = New System.Drawing.Point(36, 22)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ToDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ToDate.Size = New System.Drawing.Size(162, 20)
        Me.ToDate.TabIndex = 56
        '
        'LokExp
        '
        Me.LokExp.EditValue = ""
        Me.LokExp.Location = New System.Drawing.Point(463, 91)
        Me.LokExp.Name = "LokExp"
        Me.LokExp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokExp.Properties.ShowFooter = False
        Me.LokExp.Properties.ShowHeader = False
        Me.LokExp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokExp.Size = New System.Drawing.Size(246, 20)
        Me.LokExp.TabIndex = 65
        '
        'btnStore
        '
        Me.btnStore.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.btnStore.Appearance.Options.UseBackColor = True
        Me.btnStore.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnStore.ImageOptions.Image = CType(resources.GetObject("btnStore.ImageOptions.Image"), System.Drawing.Image)
        Me.btnStore.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnStore.Location = New System.Drawing.Point(10, 54)
        Me.btnStore.Name = "btnStore"
        Me.btnStore.Size = New System.Drawing.Size(25, 25)
        Me.btnStore.TabIndex = 64
        '
        'FromDate
        '
        Me.FromDate.EditValue = Nothing
        Me.FromDate.Location = New System.Drawing.Point(547, 21)
        Me.FromDate.Name = "FromDate"
        Me.FromDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDate.Size = New System.Drawing.Size(162, 20)
        Me.FromDate.TabIndex = 54
        '
        'LokPeeler
        '
        Me.LokPeeler.EditValue = ""
        Me.LokPeeler.Location = New System.Drawing.Point(36, 57)
        Me.LokPeeler.Name = "LokPeeler"
        Me.LokPeeler.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokPeeler.Properties.ShowFooter = False
        Me.LokPeeler.Properties.ShowHeader = False
        Me.LokPeeler.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokPeeler.Size = New System.Drawing.Size(246, 20)
        Me.LokPeeler.TabIndex = 53
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl2.Appearance.Options.UseBackColor = True
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(288, 59)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(76, 14)
        Me.LabelControl2.TabIndex = 42
        Me.LabelControl2.Text = "القشارة\الغربال"
        '
        'FrmViewShpPlr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(833, 477)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.LblHead)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmViewShpPlr"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmViewShpPlr"
        CType(Me.repBtnDel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repBtnView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVShpView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokExp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokPeeler.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LblHead As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents repBtnDel As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents repBtnView As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents GVShpView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnAr As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnLok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LokLoc As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LokExp As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents btnStore As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FromDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LokPeeler As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
End Class
