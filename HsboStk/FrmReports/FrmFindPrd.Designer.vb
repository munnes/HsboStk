<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmFindPrd
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFindPrd))
        Me.BtnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.GVArStk = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LokStrType = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LokProd = New DevExpress.XtraEditors.LookUpEdit()
        Me.GrpCrp = New System.Windows.Forms.GroupBox()
        Me.RdoClient = New System.Windows.Forms.RadioButton()
        Me.RdoLocal = New System.Windows.Forms.RadioButton()
        Me.LblHead = New DevExpress.XtraEditors.LabelControl()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GVArStk, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LokStrType.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.LokProd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpCrp.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnPrint
        '
        Me.BtnPrint.ImageOptions.Image = CType(resources.GetObject("BtnPrint.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnPrint.Location = New System.Drawing.Point(249, 113)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(99, 56)
        Me.BtnPrint.TabIndex = 92
        Me.BtnPrint.Text = "طباعة"
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSearch.Location = New System.Drawing.Point(438, 113)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(99, 56)
        Me.BtnSearch.TabIndex = 91
        Me.BtnSearch.Text = "بحث"
        '
        'GVArStk
        '
        Me.GVArStk.Appearance.HeaderPanel.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVArStk.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVArStk.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVArStk.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVArStk.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVArStk.Appearance.Row.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GVArStk.Appearance.Row.Options.UseFont = True
        Me.GVArStk.Appearance.Row.Options.UseTextOptions = True
        Me.GVArStk.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVArStk.Appearance.Row.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center
        Me.GVArStk.GridControl = Me.GridControl1
        Me.GVArStk.Name = "GVArStk"
        Me.GVArStk.OptionsBehavior.ReadOnly = True
        Me.GVArStk.OptionsFind.FindNullPrompt = ""
        Me.GVArStk.OptionsSelection.EnableAppearanceFocusedRow = False
        Me.GVArStk.OptionsView.ShowGroupPanel = False
        Me.GVArStk.OptionsView.ShowIndicator = False
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(7, 175)
        Me.GridControl1.MainView = Me.GVArStk
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(598, 233)
        Me.GridControl1.TabIndex = 87
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVArStk})
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl2.Appearance.Options.UseBackColor = True
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(236, 18)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(34, 14)
        Me.LabelControl2.TabIndex = 64
        Me.LabelControl2.Text = "المخزن"
        '
        'LokStrType
        '
        Me.LokStrType.EditValue = ""
        Me.LokStrType.Location = New System.Drawing.Point(21, 14)
        Me.LokStrType.Name = "LokStrType"
        Me.LokStrType.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokStrType.Properties.ShowFooter = False
        Me.LokStrType.Properties.ShowHeader = False
        Me.LokStrType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokStrType.Size = New System.Drawing.Size(209, 20)
        Me.LokStrType.TabIndex = 65
        '
        'LabelControl3
        '
        Me.LabelControl3.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LabelControl3.Appearance.Options.UseFont = True
        Me.LabelControl3.Location = New System.Drawing.Point(546, 18)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(29, 14)
        Me.LabelControl3.TabIndex = 63
        Me.LabelControl3.Text = "المنتج"
        '
        'BtnExit
        '
        Me.BtnExit.ImageOptions.Image = CType(resources.GetObject("BtnExit.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnExit.Location = New System.Drawing.Point(60, 113)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(99, 56)
        Me.BtnExit.TabIndex = 93
        Me.BtnExit.Text = "خروج"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LabelControl2)
        Me.GroupBox1.Controls.Add(Me.LokStrType)
        Me.GroupBox1.Controls.Add(Me.LabelControl3)
        Me.GroupBox1.Controls.Add(Me.LokProd)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 23)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(596, 43)
        Me.GroupBox1.TabIndex = 90
        Me.GroupBox1.TabStop = False
        '
        'LokProd
        '
        Me.LokProd.EditValue = ""
        Me.LokProd.Location = New System.Drawing.Point(317, 14)
        Me.LokProd.Name = "LokProd"
        Me.LokProd.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokProd.Properties.ShowFooter = False
        Me.LokProd.Properties.ShowHeader = False
        Me.LokProd.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokProd.Size = New System.Drawing.Size(223, 20)
        Me.LokProd.TabIndex = 62
        '
        'GrpCrp
        '
        Me.GrpCrp.Controls.Add(Me.RdoClient)
        Me.GrpCrp.Controls.Add(Me.RdoLocal)
        Me.GrpCrp.Location = New System.Drawing.Point(6, 67)
        Me.GrpCrp.Name = "GrpCrp"
        Me.GrpCrp.Size = New System.Drawing.Size(598, 42)
        Me.GrpCrp.TabIndex = 94
        Me.GrpCrp.TabStop = False
        Me.GrpCrp.Text = "مصدر المحاصيل"
        '
        'RdoClient
        '
        Me.RdoClient.AutoSize = True
        Me.RdoClient.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdoClient.Location = New System.Drawing.Point(161, 15)
        Me.RdoClient.Name = "RdoClient"
        Me.RdoClient.Size = New System.Drawing.Size(122, 18)
        Me.RdoClient.TabIndex = 79
        Me.RdoClient.TabStop = True
        Me.RdoClient.Text = "استلام لصالح عميل"
        Me.RdoClient.UseVisualStyleBackColor = True
        '
        'RdoLocal
        '
        Me.RdoLocal.AutoSize = True
        Me.RdoLocal.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RdoLocal.Location = New System.Drawing.Point(463, 15)
        Me.RdoLocal.Name = "RdoLocal"
        Me.RdoLocal.Size = New System.Drawing.Size(94, 18)
        Me.RdoLocal.TabIndex = 78
        Me.RdoLocal.TabStop = True
        Me.RdoLocal.Text = "استلام محلي"
        Me.RdoLocal.UseVisualStyleBackColor = True
        '
        'LblHead
        '
        Me.LblHead.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblHead.Appearance.Options.UseFont = True
        Me.LblHead.Location = New System.Drawing.Point(7, 7)
        Me.LblHead.Name = "LblHead"
        Me.LblHead.Size = New System.Drawing.Size(85, 16)
        Me.LblHead.TabIndex = 89
        Me.LblHead.Text = "البحث عن منتج"
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(579, 5)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 88
        '
        'FrmFindPrd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(611, 413)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.BtnExit)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GrpCrp)
        Me.Controls.Add(Me.LblHead)
        Me.Controls.Add(Me.BtnMeClose)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmFindPrd"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmFindPrd"
        CType(Me.GVArStk, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LokStrType.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.LokProd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpCrp.ResumeLayout(False)
        Me.GrpCrp.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GVArStk As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LokStrType As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LokProd As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents GrpCrp As GroupBox
    Friend WithEvents RdoClient As RadioButton
    Friend WithEvents RdoLocal As RadioButton
    Friend WithEvents LblHead As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
End Class
