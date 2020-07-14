<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LstCrop
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LstCrop))
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVCrop = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVCrop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(258, 238)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 54
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.GVCrop
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(284, 231)
        Me.GridControl1.TabIndex = 53
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVCrop})
        '
        'GVCrop
        '
        Me.GVCrop.Appearance.HeaderPanel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVCrop.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVCrop.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVCrop.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVCrop.Appearance.Row.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVCrop.Appearance.Row.Options.UseFont = True
        Me.GVCrop.Appearance.Row.Options.UseTextOptions = True
        Me.GVCrop.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVCrop.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.GVCrop.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVCrop.AppearancePrint.Row.Options.UseTextOptions = True
        Me.GVCrop.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVCrop.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GVCrop.GridControl = Me.GridControl1
        Me.GVCrop.Name = "GVCrop"
        Me.GVCrop.OptionsBehavior.KeepFocusedRowOnUpdate = False
        Me.GVCrop.OptionsBehavior.ReadOnly = True
        Me.GVCrop.OptionsNavigation.AutoFocusNewRow = True
        Me.GVCrop.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GVCrop.OptionsView.ShowGroupPanel = False
        Me.GVCrop.OptionsView.ShowIndicator = False
        '
        'LstCrop
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.GridControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LstCrop"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "LstCrop"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVCrop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVCrop As DevExpress.XtraGrid.Views.Grid.GridView
End Class
