<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmViewArPrd
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmViewArPrd))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.repBtnDel = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.repBtnView = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.GVBuyView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.btnStore = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLok = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.ToDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LokLoc = New DevExpress.XtraEditors.LookUpEdit()
        Me.FromDate = New DevExpress.XtraEditors.DateEdit()
        Me.LokExp = New DevExpress.XtraEditors.LookUpEdit()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.LblHead = New DevExpress.XtraEditors.LabelControl()
        CType(Me.repBtnDel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repBtnView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVBuyView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokExp.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSearch.Location = New System.Drawing.Point(614, 106)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(162, 56)
        Me.BtnSearch.TabIndex = 69
        Me.BtnSearch.Text = "بحث"
        '
        'BtnPrint
        '
        Me.BtnPrint.ImageOptions.Image = CType(resources.GetObject("BtnPrint.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnPrint.Location = New System.Drawing.Point(348, 106)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(162, 56)
        Me.BtnPrint.TabIndex = 67
        Me.BtnPrint.Text = "طباعة"
        '
        'repBtnDel
        '
        Me.repBtnDel.AutoHeight = False
        EditorButtonImageOptions1.Image = CType(resources.GetObject("EditorButtonImageOptions1.Image"), System.Drawing.Image)
        EditorButtonImageOptions1.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter
        Me.repBtnDel.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(EditorButtonImageOptions1, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, Nothing)})
        Me.repBtnDel.Name = "repBtnDel"
        Me.repBtnDel.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'repBtnView
        '
        Me.repBtnView.AutoHeight = False
        EditorButtonImageOptions2.Image = CType(resources.GetObject("EditorButtonImageOptions2.Image"), System.Drawing.Image)
        EditorButtonImageOptions2.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.TopCenter
        Me.repBtnView.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(EditorButtonImageOptions2, DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, Nothing)})
        Me.repBtnView.Name = "repBtnView"
        Me.repBtnView.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
        '
        'GVBuyView
        '
        Me.GVBuyView.Appearance.ColumnFilterButton.Image = CType(resources.GetObject("GVBuyView.Appearance.ColumnFilterButton.Image"), System.Drawing.Image)
        Me.GVBuyView.Appearance.ColumnFilterButton.Options.UseImage = True
        Me.GVBuyView.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVBuyView.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVBuyView.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVBuyView.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVBuyView.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVBuyView.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVBuyView.Appearance.Row.Options.UseFont = True
        Me.GVBuyView.Appearance.Row.Options.UseTextOptions = True
        Me.GVBuyView.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVBuyView.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVBuyView.GridControl = Me.GridControl1
        Me.GVBuyView.Name = "GVBuyView"
        Me.GVBuyView.OptionsFind.FindNullPrompt = ""
        Me.GVBuyView.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.GVBuyView.OptionsView.ShowGroupPanel = False
        Me.GVBuyView.OptionsView.ShowIndicator = False
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(13, 168)
        Me.GridControl1.MainView = Me.GVBuyView
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repBtnView, Me.repBtnDel})
        Me.GridControl1.Size = New System.Drawing.Size(829, 288)
        Me.GridControl1.TabIndex = 66
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVBuyView})
        '
        'btnStore
        '
        Me.btnStore.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.btnStore.Appearance.Options.UseBackColor = True
        Me.btnStore.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnStore.ImageOptions.Image = CType(resources.GetObject("btnStore.ImageOptions.Image"), System.Drawing.Image)
        Me.btnStore.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnStore.Location = New System.Drawing.Point(6, 13)
        Me.btnStore.Name = "btnStore"
        Me.btnStore.Size = New System.Drawing.Size(25, 25)
        Me.btnStore.TabIndex = 64
        '
        'btnLok
        '
        Me.btnLok.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.btnLok.Appearance.Options.UseBackColor = True
        Me.btnLok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnLok.ImageOptions.Image = CType(resources.GetObject("btnLok.ImageOptions.Image"), System.Drawing.Image)
        Me.btnLok.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnLok.Location = New System.Drawing.Point(450, 13)
        Me.btnLok.Name = "btnLok"
        Me.btnLok.Size = New System.Drawing.Size(25, 25)
        Me.btnLok.TabIndex = 63
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(209, 53)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(44, 14)
        Me.LabelControl4.TabIndex = 55
        Me.LabelControl4.Text = "الى تاريخ"
        '
        'ToDate
        '
        Me.ToDate.EditValue = Nothing
        Me.ToDate.Location = New System.Drawing.Point(34, 48)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ToDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ToDate.Size = New System.Drawing.Size(162, 20)
        Me.ToDate.TabIndex = 56
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(730, 20)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(92, 14)
        Me.LabelControl3.TabIndex = 43
        Me.LabelControl3.Text = "مشحون من محطة"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl2.Appearance.Options.UseBackColor = True
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(295, 20)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(83, 14)
        Me.LabelControl2.TabIndex = 42
        Me.LabelControl2.Text = "وصول في منطقة"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Location = New System.Drawing.Point(730, 53)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(41, 14)
        Me.LabelControl5.TabIndex = 49
        Me.LabelControl5.Text = "من تاريخ"
        '
        'BtnExit
        '
        Me.BtnExit.ImageOptions.Image = CType(resources.GetObject("BtnExit.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnExit.Location = New System.Drawing.Point(82, 106)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(162, 56)
        Me.BtnExit.TabIndex = 68
        Me.BtnExit.Text = "خروج"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.btnStore)
        Me.GroupBox1.Controls.Add(Me.btnLok)
        Me.GroupBox1.Controls.Add(Me.LabelControl4)
        Me.GroupBox1.Controls.Add(Me.ToDate)
        Me.GroupBox1.Controls.Add(Me.LabelControl3)
        Me.GroupBox1.Controls.Add(Me.LokLoc)
        Me.GroupBox1.Controls.Add(Me.LabelControl2)
        Me.GroupBox1.Controls.Add(Me.LabelControl5)
        Me.GroupBox1.Controls.Add(Me.FromDate)
        Me.GroupBox1.Controls.Add(Me.LokExp)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(13, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(829, 79)
        Me.GroupBox1.TabIndex = 65
        Me.GroupBox1.TabStop = False
        '
        'LokLoc
        '
        Me.LokLoc.EditValue = ""
        Me.LokLoc.Location = New System.Drawing.Point(478, 17)
        Me.LokLoc.Name = "LokLoc"
        Me.LokLoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokLoc.Properties.ShowFooter = False
        Me.LokLoc.Properties.ShowHeader = False
        Me.LokLoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokLoc.Size = New System.Drawing.Size(246, 20)
        Me.LokLoc.TabIndex = 44
        '
        'FromDate
        '
        Me.FromDate.EditValue = Nothing
        Me.FromDate.Location = New System.Drawing.Point(562, 51)
        Me.FromDate.Name = "FromDate"
        Me.FromDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDate.Size = New System.Drawing.Size(162, 20)
        Me.FromDate.TabIndex = 54
        '
        'LokExp
        '
        Me.LokExp.EditValue = ""
        Me.LokExp.Location = New System.Drawing.Point(34, 17)
        Me.LokExp.Name = "LokExp"
        Me.LokExp.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokExp.Properties.ShowFooter = False
        Me.LokExp.Properties.ShowHeader = False
        Me.LokExp.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokExp.Size = New System.Drawing.Size(246, 20)
        Me.LokExp.TabIndex = 53
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(821, 6)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 64
        '
        'LblHead
        '
        Me.LblHead.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblHead.Appearance.Options.UseFont = True
        Me.LblHead.Location = New System.Drawing.Point(5, 6)
        Me.LblHead.Name = "LblHead"
        Me.LblHead.Size = New System.Drawing.Size(134, 16)
        Me.LblHead.TabIndex = 76
        Me.LblHead.Text = "الوصول في منطقة الصادر"
        '
        'FrmViewArPrd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(851, 460)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.LblHead)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmViewArPrd"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmViewArPrd"
        CType(Me.repBtnDel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repBtnView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVBuyView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokExp.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents repBtnDel As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents repBtnView As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents GVBuyView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents btnStore As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btnLok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LokLoc As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents FromDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LokExp As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LblHead As DevExpress.XtraEditors.LabelControl
End Class
