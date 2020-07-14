<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LstExport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LstExport))
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVExport = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVExport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(258, 239)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 56
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.GVExport
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(284, 231)
        Me.GridControl1.TabIndex = 55
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVExport})
        '
        'GVExport
        '
        Me.GVExport.Appearance.HeaderPanel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVExport.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVExport.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVExport.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVExport.Appearance.Row.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVExport.Appearance.Row.Options.UseFont = True
        Me.GVExport.Appearance.Row.Options.UseTextOptions = True
        Me.GVExport.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVExport.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.GVExport.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVExport.AppearancePrint.Row.Options.UseTextOptions = True
        Me.GVExport.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVExport.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GVExport.GridControl = Me.GridControl1
        Me.GVExport.Name = "GVExport"
        Me.GVExport.OptionsBehavior.KeepFocusedRowOnUpdate = False
        Me.GVExport.OptionsBehavior.ReadOnly = True
        Me.GVExport.OptionsNavigation.AutoFocusNewRow = True
        Me.GVExport.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GVExport.OptionsView.ShowGroupPanel = False
        Me.GVExport.OptionsView.ShowIndicator = False
        '
        'LstExport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.GridControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LstExport"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "LstExport"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVExport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVExport As DevExpress.XtraGrid.Views.Grid.GridView
End Class
