<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSaleClient
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSaleClient))
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LokLoc = New DevExpress.XtraEditors.LookUpEdit()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVClient = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.CardView1 = New DevExpress.XtraGrid.Views.Card.CardView()
        Me.ProgressBarControl1 = New DevExpress.XtraEditors.ProgressBarControl()
        Me.BtnPrint = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtTel = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LblHead = New DevExpress.XtraEditors.LabelControl()
        Me.BtnAdd = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSave = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnDelete = New DevExpress.XtraEditors.SimpleButton()
        Me.TxtName = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtID = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.TxtInfo = New DevExpress.XtraEditors.MemoEdit()
        Me.BtnExit = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVClient, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CardView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtTel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtName.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtID.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtInfo.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl6
        '
        Me.LabelControl6.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl6.Appearance.Options.UseFont = True
        Me.LabelControl6.Location = New System.Drawing.Point(338, 71)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(42, 14)
        Me.LabelControl6.TabIndex = 124
        Me.LabelControl6.Text = "المنطقة:"
        '
        'LokLoc
        '
        Me.LokLoc.EditValue = ""
        Me.LokLoc.Location = New System.Drawing.Point(17, 69)
        Me.LokLoc.Name = "LokLoc"
        Me.LokLoc.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.LokLoc.Properties.ShowFooter = False
        Me.LokLoc.Properties.ShowHeader = False
        Me.LokLoc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.LokLoc.Size = New System.Drawing.Size(314, 20)
        Me.LokLoc.TabIndex = 123
        '
        'GridControl1
        '
        Me.GridControl1.Location = New System.Drawing.Point(6, 239)
        Me.GridControl1.MainView = Me.GVClient
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(760, 275)
        Me.GridControl1.TabIndex = 122
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVClient, Me.CardView1})
        '
        'GVClient
        '
        Me.GVClient.Appearance.HeaderPanel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVClient.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVClient.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVClient.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVClient.Appearance.Row.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVClient.Appearance.Row.Options.UseFont = True
        Me.GVClient.Appearance.Row.Options.UseTextOptions = True
        Me.GVClient.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near
        Me.GVClient.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.GVClient.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVClient.AppearancePrint.Row.Options.UseTextOptions = True
        Me.GVClient.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVClient.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GVClient.GridControl = Me.GridControl1
        Me.GVClient.Name = "GVClient"
        Me.GVClient.OptionsBehavior.KeepFocusedRowOnUpdate = False
        Me.GVClient.OptionsBehavior.ReadOnly = True
        Me.GVClient.OptionsFind.FindNullPrompt = ""
        Me.GVClient.OptionsNavigation.AutoFocusNewRow = True
        Me.GVClient.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GVClient.OptionsView.ShowGroupPanel = False
        Me.GVClient.OptionsView.ShowIndicator = False
        '
        'CardView1
        '
        Me.CardView1.FocusedCardTopFieldIndex = 0
        Me.CardView1.GridControl = Me.GridControl1
        Me.CardView1.Name = "CardView1"
        '
        'ProgressBarControl1
        '
        Me.ProgressBarControl1.Location = New System.Drawing.Point(79, 153)
        Me.ProgressBarControl1.Name = "ProgressBarControl1"
        Me.ProgressBarControl1.Size = New System.Drawing.Size(621, 41)
        Me.ProgressBarControl1.TabIndex = 115
        '
        'BtnPrint
        '
        Me.BtnPrint.ImageOptions.Image = CType(resources.GetObject("BtnPrint.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnPrint.Location = New System.Drawing.Point(135, 173)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(73, 56)
        Me.BtnPrint.TabIndex = 121
        Me.BtnPrint.Text = "طباعة"
        '
        'LabelControl5
        '
        Me.LabelControl5.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl5.Appearance.Options.UseFont = True
        Me.LabelControl5.Location = New System.Drawing.Point(680, 105)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(75, 14)
        Me.LabelControl5.TabIndex = 120
        Me.LabelControl5.Text = "معلومات أخرى:"
        '
        'TxtTel
        '
        Me.TxtTel.Location = New System.Drawing.Point(438, 68)
        Me.TxtTel.Name = "TxtTel"
        Me.TxtTel.Size = New System.Drawing.Size(238, 20)
        Me.TxtTel.TabIndex = 118
        '
        'LabelControl4
        '
        Me.LabelControl4.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl4.Appearance.Options.UseFont = True
        Me.LabelControl4.Location = New System.Drawing.Point(678, 71)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(56, 14)
        Me.LabelControl4.TabIndex = 117
        Me.LabelControl4.Text = "رقم الهاتف:"
        '
        'LblHead
        '
        Me.LblHead.Appearance.Font = New System.Drawing.Font("Tahoma", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.LblHead.Appearance.Options.UseFont = True
        Me.LblHead.Location = New System.Drawing.Point(7, 6)
        Me.LblHead.Name = "LblHead"
        Me.LblHead.Size = New System.Drawing.Size(59, 16)
        Me.LblHead.TabIndex = 116
        Me.LblHead.Text = "عملاء البيع"
        '
        'BtnAdd
        '
        Me.BtnAdd.ImageOptions.Image = CType(resources.GetObject("BtnAdd.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnAdd.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnAdd.Location = New System.Drawing.Point(678, 173)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(73, 56)
        Me.BtnAdd.TabIndex = 111
        Me.BtnAdd.Text = "جديد"
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSearch.Location = New System.Drawing.Point(391, 173)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(73, 56)
        Me.BtnSearch.TabIndex = 113
        Me.BtnSearch.Text = "بحث"
        '
        'BtnSave
        '
        Me.BtnSave.ImageOptions.Image = CType(resources.GetObject("BtnSave.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnSave.Location = New System.Drawing.Point(549, 173)
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(73, 56)
        Me.BtnSave.TabIndex = 114
        Me.BtnSave.Text = "حفظ"
        '
        'BtnDelete
        '
        Me.BtnDelete.ImageOptions.Image = CType(resources.GetObject("BtnDelete.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnDelete.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnDelete.Location = New System.Drawing.Point(270, 173)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(73, 56)
        Me.BtnDelete.TabIndex = 112
        Me.BtnDelete.Text = "حذف"
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(17, 35)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(314, 20)
        Me.TxtName.TabIndex = 110
        '
        'LabelControl1
        '
        Me.LabelControl1.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl1.Appearance.Options.UseFont = True
        Me.LabelControl1.Location = New System.Drawing.Point(681, 35)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(32, 14)
        Me.LabelControl1.TabIndex = 108
        Me.LabelControl1.Text = "الرقم: "
        '
        'TxtID
        '
        Me.TxtID.Location = New System.Drawing.Point(576, 35)
        Me.TxtID.Name = "TxtID"
        Me.TxtID.Properties.ReadOnly = True
        Me.TxtID.Size = New System.Drawing.Size(100, 20)
        Me.TxtID.TabIndex = 107
        '
        'LabelControl2
        '
        Me.LabelControl2.Appearance.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl2.Appearance.Options.UseFont = True
        Me.LabelControl2.Location = New System.Drawing.Point(338, 34)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(63, 14)
        Me.LabelControl2.TabIndex = 109
        Me.LabelControl2.Text = "اسم العميل:"
        '
        'TxtInfo
        '
        Me.TxtInfo.Location = New System.Drawing.Point(17, 107)
        Me.TxtInfo.Name = "TxtInfo"
        Me.TxtInfo.Size = New System.Drawing.Size(659, 51)
        Me.TxtInfo.TabIndex = 119
        '
        'BtnExit
        '
        Me.BtnExit.ImageOptions.Image = CType(resources.GetObject("BtnExit.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnExit.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.TopCenter
        Me.BtnExit.Location = New System.Drawing.Point(26, 173)
        Me.BtnExit.Name = "BtnExit"
        Me.BtnExit.Size = New System.Drawing.Size(73, 56)
        Me.BtnExit.TabIndex = 125
        Me.BtnExit.Text = "خروج"
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(742, 4)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(24, 23)
        Me.BtnMeClose.TabIndex = 126
        '
        'Timer1
        '
        Me.Timer1.Interval = 10
        '
        'FrmSaleClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(772, 520)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LokLoc)
        Me.Controls.Add(Me.GridControl1)
        Me.Controls.Add(Me.ProgressBarControl1)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.TxtTel)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LblHead)
        Me.Controls.Add(Me.BtnAdd)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.BtnSave)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.TxtID)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.TxtInfo)
        Me.Controls.Add(Me.BtnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "FrmSaleClient"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "FrmSaleClient"
        CType(Me.LokLoc.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVClient, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CardView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ProgressBarControl1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtTel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtName.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtID.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtInfo.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LokLoc As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVClient As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents CardView1 As DevExpress.XtraGrid.Views.Card.CardView
    Friend WithEvents ProgressBarControl1 As DevExpress.XtraEditors.ProgressBarControl
    Friend WithEvents BtnPrint As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtTel As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LblHead As DevExpress.XtraEditors.LabelControl
    Friend WithEvents BtnAdd As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSave As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnDelete As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents TxtName As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtID As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents TxtInfo As DevExpress.XtraEditors.MemoEdit
    Friend WithEvents BtnExit As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As Timer
End Class
