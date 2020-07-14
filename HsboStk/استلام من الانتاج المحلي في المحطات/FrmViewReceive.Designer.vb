<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmViewReceive
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmViewReceive))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Me.GVBuyView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.repBtnView = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.repBtnDel = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.ToDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnPeeler = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LokPeeler = New DevExpress.XtraEditors.LookUpEdit()
        Me.btnStore = New DevExpress.XtraEditors.SimpleButton()
        Me.btnLok = New DevExpress.XtraEditors.SimpleButton()
        Me.LokLoc = New DevExpress.XtraEditors.LookUpEdit()
        Me.FromDate = New DevExpress.XtraEditors.DateEdit()
        Me.LokStore = New DevExpress.XtraEditors.LookUpEdit()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.LblHead = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.GVBuyView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repBtnView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repBtnDel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.LokPeeler.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokStore.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
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
        Me.GridControl1.Location = New System.Drawing.Point(9, 196)
        Me.GridControl1.MainView = Me.GVBuyView
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repBtnView, Me.repBtnDel})
        Me.GridControl1.Size = New System.Drawing.Size(821, 316)
        Me.GridControl1.TabIndex = 66
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVBuyView})
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
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(209, 23)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(44, 14)
        Me.LabelControl4.TabIndex = 55
        Me.LabelControl4.Text = "الى تاريخ"
        '
        'ToDate
        '
        Me.ToDate.EditValue = Nothing
        Me.ToDate.Location = New System.Drawing.Point(41, 17)
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
        Me.LabelControl3.Location = New System.Drawing.Point(750, 51)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(36, 14)
        Me.LabelControl3.TabIndex = 43
        Me.LabelControl3.Text = "المحطة"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl2.Appearance.Options.UseBackColor = True
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(748, 78)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(67, 14)
        Me.LabelControl2.TabIndex = 42
        Me.LabelControl2.Text = "مخزن منتجات"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Location = New System.Drawing.Point(750, 26)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(41, 14)
        Me.LabelControl5.TabIndex = 49
        Me.LabelControl5.Text = "من تاريخ"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.btnPeeler)
        Me.GroupBox1.Controls.Add(Me.LabelControl6)
        Me.GroupBox1.Controls.Add(Me.LokPeeler)
        Me.GroupBox1.Controls.Add(Me.btnStore)
        Me.GroupBox1.Controls.Add(Me.btnLok)
        Me.GroupBox1.Controls.Add(Me.LabelControl4)
        Me.GroupBox1.Controls.Add(Me.ToDate)
        Me.GroupBox1.Controls.Add(Me.LabelControl3)
        Me.GroupBox1.Controls.Add(Me.LokLoc)
        Me.GroupBox1.Controls.Add(Me.LabelControl2)
        Me.GroupBox1.Controls.Add(Me.LabelControl5)
        Me.GroupBox1.Controls.Add(Me.FromDate)
        Me.GroupBox1.Controls.Add(Me.LokStore)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(9, 21)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(821, 105)
        Me.GroupBox1.TabIndex = 65
        Me.GroupBox1.TabStop = False
        '
        'btnPeeler
        '
        Me.btnPeeler.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.btnPeeler.Appearance.Options.UseBackColor = True
        Me.btnPeeler.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnPeeler.ImageOptions.Image = CType(resources.GetObject("btnPeeler.ImageOptions.Image"), System.Drawing.Image)
        Me.btnPeeler.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnPeeler.Location = New System.Drawing.Point(12, 41)
        Me.btnPeeler.Name = "btnPeeler"
        Me.btnPeeler.Size = New System.Drawing.Size(25, 25)
        Me.btnPeeler.TabIndex = 75
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Location = New System.Drawing.Point(293, 47)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(76, 14)
        Me.LabelControl6.TabIndex = 73
        Me.LabelControl6.Text = "القشارة\الغربال"
        '
        'LokPeeler
        '
        Me.LokPeeler.EditValue = ""
        Me.LokPeeler.Location = New System.Drawing.Point(41, 46)
        Me.LokPeeler.Name = "LokPeeler"
        Me.LokPeeler.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokPeeler.Properties.ShowFooter = False
        Me.LokPeeler.Properties.ShowHeader = False
        Me.LokPeeler.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokPeeler.Size = New System.Drawing.Size(246, 20)
        Me.LokPeeler.TabIndex = 74
        '
        'btnStore
        '
        Me.btnStore.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.btnStore.Appearance.Options.UseBackColor = True
        Me.btnStore.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnStore.ImageOptions.Image = CType(resources.GetObject("btnStore.ImageOptions.Image"), System.Drawing.Image)
        Me.btnStore.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnStore.Location = New System.Drawing.Point(461, 71)
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
        Me.btnLok.Location = New System.Drawing.Point(461, 41)
        Me.btnLok.Name = "btnLok"
        Me.btnLok.Size = New System.Drawing.Size(25, 25)
        Me.btnLok.TabIndex = 63
        '
        'LokLoc
        '
        Me.LokLoc.EditValue = ""
        Me.LokLoc.Location = New System.Drawing.Point(489, 45)
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
        Me.FromDate.Location = New System.Drawing.Point(573, 17)
        Me.FromDate.Name = "FromDate"
        Me.FromDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDate.Size = New System.Drawing.Size(162, 20)
        Me.FromDate.TabIndex = 54
        '
        'LokStore
        '
        Me.LokStore.EditValue = ""
        Me.LokStore.Location = New System.Drawing.Point(489, 75)
        Me.LokStore.Name = "LokStore"
        Me.LokStore.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokStore.Properties.ShowFooter = False
        Me.LokStore.Properties.ShowHeader = False
        Me.LokStore.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokStore.Size = New System.Drawing.Size(246, 20)
        Me.LokStore.TabIndex = 53
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSearch.Location = New System.Drawing.Point(600, 132)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(162, 56)
        Me.BtnSearch.TabIndex = 69
        Me.BtnSearch.Text = "بحث"
        '
        'BtnPrint
        '
        Me.BtnPrint.ImageOptions.Image = CType(resources.GetObject("BtnPrint.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnPrint.Location = New System.Drawing.Point(334, 132)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(162, 56)
        Me.BtnPrint.TabIndex = 67
        Me.BtnPrint.Text = "طباعة"
        '
        'BtnExit
        '
        Me.BtnExit.ImageOptions.Image = CType(resources.GetObject("BtnExit.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnExit.Location = New System.Drawing.Point(68, 132)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(162, 56)
        Me.BtnExit.TabIndex = 68
        Me.BtnExit.Text = "خروج"
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(804, 4)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 64
        '
        'LblHead
        '
        Me.LblHead.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblHead.Appearance.Options.UseFont = True
        Me.LblHead.Location = New System.Drawing.Point(10, -35)
        Me.LblHead.Name = "LblHead"
        Me.LblHead.Size = New System.Drawing.Size(169, 16)
        Me.LblHead.TabIndex = 70
        Me.LblHead.Text = "استلام من الانتاج في المحطات"
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(3, 4)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(215, 16)
        Me.LabelControl1.TabIndex = 71
        Me.LabelControl1.Text = "استلام من الانتاج المحلي في المحطات"
        '
        'FrmViewReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(838, 518)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.LblHead)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmViewReceive"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmViewReceive"
        CType(Me.GVBuyView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repBtnView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repBtnDel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.LokPeeler.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokStore.Properties, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents LokStore As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LblHead As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents btnPeeler As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LokPeeler As DevExpress.XtraEditors.LookUpEdit
End Class
