<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LstStoreLoc
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LstStoreLoc))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVBuyStore = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVBuyStore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.GVBuyStore
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(346, 231)
        Me.GridControl1.TabIndex = 56
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVBuyStore})
        '
        'GVBuyStore
        '
        Me.GVBuyStore.Appearance.HeaderPanel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVBuyStore.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVBuyStore.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVBuyStore.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVBuyStore.Appearance.Row.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVBuyStore.Appearance.Row.Options.UseFont = True
        Me.GVBuyStore.Appearance.Row.Options.UseTextOptions = True
        Me.GVBuyStore.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVBuyStore.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.GVBuyStore.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVBuyStore.AppearancePrint.Row.Options.UseTextOptions = True
        Me.GVBuyStore.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVBuyStore.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GVBuyStore.GridControl = Me.GridControl1
        Me.GVBuyStore.Name = "GVBuyStore"
        Me.GVBuyStore.OptionsBehavior.KeepFocusedRowOnUpdate = False
        Me.GVBuyStore.OptionsBehavior.ReadOnly = True
        Me.GVBuyStore.OptionsFind.FindNullPrompt = ""
        Me.GVBuyStore.OptionsNavigation.AutoFocusNewRow = True
        Me.GVBuyStore.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GVBuyStore.OptionsView.ShowGroupPanel = False
        Me.GVBuyStore.OptionsView.ShowIndicator = False


        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(312, 236)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 57
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("SimpleButton1.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.Location = New System.Drawing.Point(150, 235)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 58
        Me.BtnSearch.Text = "بحث"
        '
        'LstStoreLoc
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(346, 262)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.GridControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LstStoreLoc"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "LstStoreLoc"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVBuyStore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVBuyStore As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
End Class
