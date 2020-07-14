<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInPeeler
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInPeeler))
        Me.LblHead = New DevExpress.XtraEditors.LabelControl()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.ToDate = New DevExpress.XtraEditors.DateEdit()
        Me.LokPeeler = New DevExpress.XtraEditors.LookUpEdit()
        Me.FromDate = New DevExpress.XtraEditors.DateEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LokLoc = New DevExpress.XtraEditors.LookUpEdit()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVInPeeler = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GroupControl1 = New DevExpress.XtraEditors.GroupControl()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnClnt = New DevExpress.XtraEditors.SimpleButton()
        Me.LokClient = New DevExpress.XtraEditors.LookUpEdit()
        Me.RdoClient = New System.Windows.Forms.RadioButton()
        Me.RdoLocal = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        CType(Me.ToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ToDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokPeeler.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FromDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVInPeeler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupControl1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.LokClient.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LblHead
        '
        Me.LblHead.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblHead.Appearance.Options.UseFont = True
        Me.LblHead.Location = New System.Drawing.Point(7, 5)
        Me.LblHead.Name = "LblHead"
        Me.LblHead.Size = New System.Drawing.Size(278, 16)
        Me.LblHead.TabIndex = 69
        Me.LblHead.Text = "منتجات متوفرة في القشارة أو الغربال(لم يتم صرفها)"
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(579, 3)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 68
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LabelControl4)
        Me.GroupBox1.Controls.Add(Me.LabelControl5)
        Me.GroupBox1.Controls.Add(Me.LabelControl2)
        Me.GroupBox1.Controls.Add(Me.ToDate)
        Me.GroupBox1.Controls.Add(Me.LokPeeler)
        Me.GroupBox1.Controls.Add(Me.FromDate)
        Me.GroupBox1.Controls.Add(Me.LabelControl3)
        Me.GroupBox1.Controls.Add(Me.LokLoc)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 22)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(596, 75)
        Me.GroupBox1.TabIndex = 70
        Me.GroupBox1.TabStop = False
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(245, 51)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(44, 14)
        Me.LabelControl4.TabIndex = 92
        Me.LabelControl4.Text = "الى تاريخ"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Location = New System.Drawing.Point(547, 50)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(41, 14)
        Me.LabelControl5.TabIndex = 90
        Me.LabelControl5.Text = "من تاريخ"
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl2.Appearance.Options.UseBackColor = True
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(245, 22)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(39, 14)
        Me.LabelControl2.TabIndex = 64
        Me.LabelControl2.Text = "القشارة"
        '
        'ToDate
        '
        Me.ToDate.EditValue = Nothing
        Me.ToDate.Location = New System.Drawing.Point(77, 48)
        Me.ToDate.Name = "ToDate"
        Me.ToDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ToDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ToDate.Size = New System.Drawing.Size(162, 20)
        Me.ToDate.TabIndex = 93
        '
        'LokPeeler
        '
        Me.LokPeeler.EditValue = ""
        Me.LokPeeler.Location = New System.Drawing.Point(30, 19)
        Me.LokPeeler.Name = "LokPeeler"
        Me.LokPeeler.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokPeeler.Properties.ShowFooter = False
        Me.LokPeeler.Properties.ShowHeader = False
        Me.LokPeeler.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokPeeler.Size = New System.Drawing.Size(209, 20)
        Me.LokPeeler.TabIndex = 65
        '
        'FromDate
        '
        Me.FromDate.EditValue = Nothing
        Me.FromDate.Location = New System.Drawing.Point(378, 47)
        Me.FromDate.Name = "FromDate"
        Me.FromDate.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDate.Properties.CalendarTimeProperties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.FromDate.Size = New System.Drawing.Size(162, 20)
        Me.FromDate.TabIndex = 91
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(547, 22)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(36, 14)
        Me.LabelControl3.TabIndex = 63
        Me.LabelControl3.Text = "المحطة"
        '
        'LokLoc
        '
        Me.LokLoc.EditValue = ""
        Me.LokLoc.Location = New System.Drawing.Point(318, 19)
        Me.LokLoc.Name = "LokLoc"
        Me.LokLoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokLoc.Properties.ShowFooter = False
        Me.LokLoc.Properties.ShowHeader = False
        Me.LokLoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokLoc.Size = New System.Drawing.Size(223, 20)
        Me.LokLoc.TabIndex = 62
        '
        'BtnExit
        '
        Me.BtnExit.ImageOptions.Image = CType(resources.GetObject("BtnExit.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnExit.Location = New System.Drawing.Point(64, 147)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(99, 56)
        Me.BtnExit.TabIndex = 75
        Me.BtnExit.Text = "خروج"
        '
        'BtnPrint
        '
        Me.BtnPrint.ImageOptions.Image = CType(resources.GetObject("BtnPrint.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnPrint.Location = New System.Drawing.Point(254, 146)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(99, 56)
        Me.BtnPrint.TabIndex = 74
        Me.BtnPrint.Text = "طباعة"
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSearch.Location = New System.Drawing.Point(442, 147)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(99, 56)
        Me.BtnSearch.TabIndex = 73
        Me.BtnSearch.Text = "بحث"
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridControl1.Location = New System.Drawing.Point(2, 20)
        Me.GridControl1.MainView = Me.GVInPeeler
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(592, 222)
        Me.GridControl1.TabIndex = 1
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVInPeeler})
        '
        'GVInPeeler
        '
        Me.GVInPeeler.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVInPeeler.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVInPeeler.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVInPeeler.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVInPeeler.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVInPeeler.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVInPeeler.Appearance.Row.Options.UseFont = True
        Me.GVInPeeler.Appearance.Row.Options.UseTextOptions = True
        Me.GVInPeeler.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVInPeeler.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVInPeeler.GridControl = Me.GridControl1
        Me.GVInPeeler.Name = "GVInPeeler"
        Me.GVInPeeler.OptionsBehavior.ReadOnly = True
        Me.GVInPeeler.OptionsFind.FindNullPrompt = ""
        Me.GVInPeeler.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.GVInPeeler.OptionsView.ShowGroupPanel = False
        Me.GVInPeeler.OptionsView.ShowIndicator = False
        '
        'GroupControl1
        '
        Me.GroupControl1.Controls.Add(Me.GridControl1)
        Me.GroupControl1.Location = New System.Drawing.Point(6, 208)
        Me.GroupControl1.Name = "GroupControl1"
        Me.GroupControl1.Size = New System.Drawing.Size(596, 244)
        Me.GroupControl1.TabIndex = 76
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnClnt)
        Me.GroupBox2.Controls.Add(Me.LokClient)
        Me.GroupBox2.Controls.Add(Me.RdoClient)
        Me.GroupBox2.Controls.Add(Me.RdoLocal)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 99)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(596, 42)
        Me.GroupBox2.TabIndex = 83
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "مصدر المحاصيل"
        '
        'btnClnt
        '
        Me.btnClnt.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.btnClnt.Appearance.Options.UseBackColor = True
        Me.btnClnt.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.btnClnt.ImageOptions.Image = CType(resources.GetObject("btnClnt.ImageOptions.Image"), System.Drawing.Image)
        Me.btnClnt.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.btnClnt.Location = New System.Drawing.Point(4, 11)
        Me.btnClnt.Name = "btnClnt"
        Me.btnClnt.Size = New System.Drawing.Size(25, 25)
        Me.btnClnt.TabIndex = 83
        '
        'LokClient
        '
        Me.LokClient.EditValue = ""
        Me.LokClient.Location = New System.Drawing.Point(30, 13)
        Me.LokClient.Name = "LokClient"
        Me.LokClient.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokClient.Properties.ShowFooter = False
        Me.LokClient.Properties.ShowHeader = False
        Me.LokClient.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokClient.Size = New System.Drawing.Size(209, 20)
        Me.LokClient.TabIndex = 73
        '
        'RdoClient
        '
        Me.RdoClient.AutoSize = True
        Me.RdoClient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdoClient.Location = New System.Drawing.Point(245, 15)
        Me.RdoClient.Name = "RdoClient"
        Me.RdoClient.Size = New System.Drawing.Size(83, 18)
        Me.RdoClient.TabIndex = 79
        Me.RdoClient.TabStop = True
        Me.RdoClient.Text = "لصالح عميل"
        Me.RdoClient.UseVisualStyleBackColor = True
        '
        'RdoLocal
        '
        Me.RdoLocal.AutoSize = True
        Me.RdoLocal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdoLocal.Location = New System.Drawing.Point(496, 15)
        Me.RdoLocal.Name = "RdoLocal"
        Me.RdoLocal.Size = New System.Drawing.Size(94, 18)
        Me.RdoLocal.TabIndex = 78
        Me.RdoLocal.TabStop = True
        Me.RdoLocal.Text = "استلام محلي"
        Me.RdoLocal.UseVisualStyleBackColor = True
        '
        'FrmInPeeler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 457)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupControl1)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.LblHead)
        Me.Controls.Add(Me.BtnMeClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmInPeeler"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmInPeeler"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.ToDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ToDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokPeeler.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FromDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVInPeeler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GroupControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupControl1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.LokClient.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LblHead As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LokPeeler As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LokLoc As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents BtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVInPeeler As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GroupControl1 As DevExpress.XtraEditors.GroupControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents ToDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents FromDate As DevExpress.XtraEditors.DateEdit
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents btnClnt As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LokClient As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents RdoClient As RadioButton
    Friend WithEvents RdoLocal As RadioButton
End Class
