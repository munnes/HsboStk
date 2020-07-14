<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LstUnit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LstUnit))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVUnit = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVUnit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.GVUnit
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(284, 231)
        Me.GridControl1.TabIndex = 2
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVUnit})
        '
        'GVUnit
        '
        Me.GVUnit.Appearance.HeaderPanel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVUnit.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVUnit.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVUnit.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVUnit.Appearance.Row.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVUnit.Appearance.Row.Options.UseFont = True
        Me.GVUnit.Appearance.Row.Options.UseTextOptions = True
        Me.GVUnit.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVUnit.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.GVUnit.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVUnit.AppearancePrint.Row.Options.UseTextOptions = True
        Me.GVUnit.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVUnit.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GVUnit.GridControl = Me.GridControl1
        Me.GVUnit.Name = "GVUnit"
        Me.GVUnit.OptionsBehavior.KeepFocusedRowOnUpdate = False
        Me.GVUnit.OptionsBehavior.ReadOnly = True
        Me.GVUnit.OptionsNavigation.AutoFocusNewRow = True
        Me.GVUnit.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GVUnit.OptionsView.ShowGroupPanel = False
        Me.GVUnit.OptionsView.ShowIndicator = False
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(258, 237)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 52
        '
        'LstUnit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.GridControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LstUnit"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "LstUnit"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVUnit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVUnit As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
End Class
