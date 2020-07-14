<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LstArPrdStore
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LstArPrdStore))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVArPrdStore = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVArPrdStore, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.GVArPrdStore
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(346, 231)
        Me.GridControl1.TabIndex = 60
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVArPrdStore})
        '
        'GVArPrdStore
        '
        Me.GVArPrdStore.Appearance.HeaderPanel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVArPrdStore.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVArPrdStore.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVArPrdStore.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVArPrdStore.Appearance.Row.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVArPrdStore.Appearance.Row.Options.UseFont = True
        Me.GVArPrdStore.Appearance.Row.Options.UseTextOptions = True
        Me.GVArPrdStore.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVArPrdStore.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.GVArPrdStore.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVArPrdStore.AppearancePrint.Row.Options.UseTextOptions = True
        Me.GVArPrdStore.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVArPrdStore.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GVArPrdStore.GridControl = Me.GridControl1
        Me.GVArPrdStore.Name = "GVArPrdStore"
        Me.GVArPrdStore.OptionsBehavior.KeepFocusedRowOnUpdate = False
        Me.GVArPrdStore.OptionsBehavior.ReadOnly = True
        Me.GVArPrdStore.OptionsFind.FindNullPrompt = ""
        Me.GVArPrdStore.OptionsNavigation.AutoFocusNewRow = True
        Me.GVArPrdStore.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GVArPrdStore.OptionsView.ShowGroupPanel = False
        Me.GVArPrdStore.OptionsView.ShowIndicator = False
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.Location = New System.Drawing.Point(154, 235)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 63
        Me.BtnSearch.Text = "بحث"
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(316, 236)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 62
        '
        'LstArPrdStore
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(346, 262)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.GridControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LstArPrdStore"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "LstArPrdStore"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVArPrdStore, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVArPrdStore As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
End Class
