﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmViewToPrd
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmViewToPrd))
        Dim EditorButtonImageOptions1 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Dim EditorButtonImageOptions2 As DevExpress.XtraEditors.Controls.EditorButtonImageOptions = New DevExpress.XtraEditors.Controls.EditorButtonImageOptions()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.ToDate = New DevExpress.XtraEditors.DateEdit()
        Me.btnStore = New DevExpress.XtraEditors.SimpleButton()
        Me.FromDate = New DevExpress.XtraEditors.DateEdit()
        Me.LokPeeler = New DevExpress.XtraEditors.LookUpEdit()
        Me.btnLok = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LokLoc = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVShpView = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.repBtnView = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.repBtnDel = New DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit()
        Me.BtnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.LblHead = New DevExpress.XtraEditors.LabelControl()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RdoClient = New System.Windows.Forms.RadioButton()
        Me.RdoLocal = New System.Windows.Forms.RadioButton()
        CType(Me.ToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokPeeler.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVShpView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repBtnView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.repBtnDel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(810, 4)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 71
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(221, 20)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(44, 14)
        Me.LabelControl4.TabIndex = 55
        Me.LabelControl4.Text = "الى تاريخ"
        '
        'ToDate
        '
        Me.ToDate.EditValue = Nothing
        Me.ToDate.Location = New System.Drawing.Point(53, 18)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ToDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ToDate.Size = New System.Drawing.Size(162, 20)
        Me.ToDate.TabIndex = 56
        '
        'btnStore
        '
        Me.btnStore.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.btnStore.Appearance.Options.UseBackColor = True
        Me.btnStore.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnStore.ImageOptions.Image = CType(resources.GetObject("btnStore.ImageOptions.Image"), System.Drawing.Image)
        Me.btnStore.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnStore.Location = New System.Drawing.Point(27, 50)
        Me.btnStore.Name = "btnStore"
        Me.btnStore.Size = New System.Drawing.Size(25, 25)
        Me.btnStore.TabIndex = 64
        '
        'FromDate
        '
        Me.FromDate.EditValue = Nothing
        Me.FromDate.Location = New System.Drawing.Point(564, 18)
        Me.FromDate.Name = "FromDate"
        Me.FromDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDate.Size = New System.Drawing.Size(162, 20)
        Me.FromDate.TabIndex = 54
        '
        'LokPeeler
        '
        Me.LokPeeler.EditValue = ""
        Me.LokPeeler.Location = New System.Drawing.Point(53, 54)
        Me.LokPeeler.Name = "LokPeeler"
        Me.LokPeeler.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokPeeler.Properties.ShowFooter = False
        Me.LokPeeler.Properties.ShowHeader = False
        Me.LokPeeler.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokPeeler.Size = New System.Drawing.Size(246, 20)
        Me.LokPeeler.TabIndex = 53
        '
        'btnLok
        '
        Me.btnLok.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.btnLok.Appearance.Options.UseBackColor = True
        Me.btnLok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnLok.ImageOptions.Image = CType(resources.GetObject("btnLok.ImageOptions.Image"), System.Drawing.Image)
        Me.btnLok.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnLok.Location = New System.Drawing.Point(452, 51)
        Me.btnLok.Name = "btnLok"
        Me.btnLok.Size = New System.Drawing.Size(25, 25)
        Me.btnLok.TabIndex = 63
        '
        'BtnExit
        '
        Me.BtnExit.ImageOptions.Image = CType(resources.GetObject("BtnExit.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnExit.Location = New System.Drawing.Point(75, 155)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(162, 56)
        Me.BtnExit.TabIndex = 75
        Me.BtnExit.Text = "خروج"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.LabelControl4)
        Me.GroupBox1.Controls.Add(Me.ToDate)
        Me.GroupBox1.Controls.Add(Me.LabelControl5)
        Me.GroupBox1.Controls.Add(Me.btnStore)
        Me.GroupBox1.Controls.Add(Me.FromDate)
        Me.GroupBox1.Controls.Add(Me.LokPeeler)
        Me.GroupBox1.Controls.Add(Me.btnLok)
        Me.GroupBox1.Controls.Add(Me.LabelControl2)
        Me.GroupBox1.Controls.Add(Me.LokLoc)
        Me.GroupBox1.Controls.Add(Me.LabelControl3)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(8, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(820, 81)
        Me.GroupBox1.TabIndex = 72
        Me.GroupBox1.TabStop = False
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Location = New System.Drawing.Point(742, 24)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(41, 14)
        Me.LabelControl5.TabIndex = 49
        Me.LabelControl5.Text = "من تاريخ"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl2.Appearance.Options.UseBackColor = True
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(305, 56)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(76, 14)
        Me.LabelControl2.TabIndex = 42
        Me.LabelControl2.Text = "القشارة\الغربال"
        '
        'LokLoc
        '
        Me.LokLoc.EditValue = ""
        Me.LokLoc.Location = New System.Drawing.Point(480, 54)
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
        Me.LabelControl3.Location = New System.Drawing.Point(747, 57)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(36, 14)
        Me.LabelControl3.TabIndex = 43
        Me.LabelControl3.Text = "المحطة"
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(9, 215)
        Me.GridControl1.MainView = Me.GVShpView
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.RepositoryItems.AddRange(New DevExpress.XtraEditors.Repository.RepositoryItem() {Me.repBtnView, Me.repBtnDel})
        Me.GridControl1.Size = New System.Drawing.Size(820, 251)
        Me.GridControl1.TabIndex = 73
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVShpView})
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
        'BtnPrint
        '
        Me.BtnPrint.ImageOptions.Image = CType(resources.GetObject("BtnPrint.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnPrint.Location = New System.Drawing.Point(341, 155)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(162, 56)
        Me.BtnPrint.TabIndex = 74
        Me.BtnPrint.Text = "طباعة"
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSearch.Location = New System.Drawing.Point(607, 155)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(162, 56)
        Me.BtnSearch.TabIndex = 76
        Me.BtnSearch.Text = "بحث"
        '
        'LblHead
        '
        Me.LblHead.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblHead.Appearance.Options.UseFont = True
        Me.LblHead.Location = New System.Drawing.Point(12, 6)
        Me.LblHead.Name = "LblHead"
        Me.LblHead.Size = New System.Drawing.Size(232, 16)
        Me.LblHead.TabIndex = 89
        Me.LblHead.Text = "الصرف في المحطات ( انتاج قشارة أو غربال)"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RdoClient)
        Me.GroupBox2.Controls.Add(Me.RdoLocal)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 109)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(820, 42)
        Me.GroupBox2.TabIndex = 82
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "مصدر المحاصيل "
        '
        'RdoClient
        '
        Me.RdoClient.AutoSize = True
        Me.RdoClient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdoClient.Location = New System.Drawing.Point(272, 18)
        Me.RdoClient.Name = "RdoClient"
        Me.RdoClient.Size = New System.Drawing.Size(110, 18)
        Me.RdoClient.TabIndex = 79
        Me.RdoClient.TabStop = True
        Me.RdoClient.Text = "استلام من عميل"
        Me.RdoClient.UseVisualStyleBackColor = True
        '
        'RdoLocal
        '
        Me.RdoLocal.AutoSize = True
        Me.RdoLocal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdoLocal.Location = New System.Drawing.Point(690, 18)
        Me.RdoLocal.Name = "RdoLocal"
        Me.RdoLocal.Size = New System.Drawing.Size(94, 18)
        Me.RdoLocal.TabIndex = 78
        Me.RdoLocal.TabStop = True
        Me.RdoLocal.Text = "استلام محلي"
        Me.RdoLocal.UseVisualStyleBackColor = True
        '
        'FrmViewToPrd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(838, 470)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.LblHead)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.BtnSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmViewToPrd"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmViewToPrd"
        CType(Me.ToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokPeeler.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVShpView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repBtnView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.repBtnDel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents btnStore As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents FromDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents LokPeeler As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents btnLok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LokLoc As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVShpView As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents repBtnView As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents repBtnDel As DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit
    Friend WithEvents BtnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LblHead As DevExpress.XtraEditors.LabelControl
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RdoClient As RadioButton
    Friend WithEvents RdoLocal As RadioButton
End Class
