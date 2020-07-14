<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LstPrdStr
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LstPrdStr))
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVPrdStr = New DevExpress.XtraGrid.Views.Grid.GridView()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVPrdStr, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(255, 238)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 24)
        Me.BtnMeClose.TabIndex = 56
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.GVPrdStr
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(284, 231)
        Me.GridControl1.TabIndex = 55
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVPrdStr})
        '
        'GVPrdStr
        '
        Me.GVPrdStr.Appearance.HeaderPanel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVPrdStr.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVPrdStr.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVPrdStr.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVPrdStr.Appearance.Row.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVPrdStr.Appearance.Row.Options.UseFont = True
        Me.GVPrdStr.Appearance.Row.Options.UseTextOptions = True
        Me.GVPrdStr.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVPrdStr.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.GVPrdStr.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVPrdStr.AppearancePrint.Row.Options.UseTextOptions = True
        Me.GVPrdStr.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVPrdStr.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GVPrdStr.GridControl = Me.GridControl1
        Me.GVPrdStr.Name = "GVPrdStr"
        Me.GVPrdStr.OptionsBehavior.KeepFocusedRowOnUpdate = False
        Me.GVPrdStr.OptionsBehavior.ReadOnly = True
        Me.GVPrdStr.OptionsNavigation.AutoFocusNewRow = True
        Me.GVPrdStr.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GVPrdStr.OptionsView.ShowGroupPanel = False
        Me.GVPrdStr.OptionsView.ShowIndicator = False
        '
        'LstPrdStr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.GridControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LstPrdStr"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "LstPrdStr"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVPrdStr, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVPrdStr As DevExpress.XtraGrid.Views.Grid.GridView
End Class
