<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LstPeeler
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LstPeeler))
        Me.BtnMeClose = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.GVPeeler = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.BtnSearch = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GVPeeler, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnMeClose
        '
        Me.BtnMeClose.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat
        Me.BtnMeClose.ImageOptions.Image = CType(resources.GetObject("BtnMeClose.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnMeClose.Location = New System.Drawing.Point(320, 237)
        Me.BtnMeClose.Name = "BtnMeClose"
        Me.BtnMeClose.Size = New System.Drawing.Size(26, 23)
        Me.BtnMeClose.TabIndex = 54
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.GVPeeler
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(346, 231)
        Me.GridControl1.TabIndex = 53
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GVPeeler})
        '
        'GVPeeler
        '
        Me.GVPeeler.Appearance.HeaderPanel.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVPeeler.Appearance.HeaderPanel.Options.UseFont = True
        Me.GVPeeler.Appearance.HeaderPanel.Options.UseTextOptions = True
        Me.GVPeeler.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVPeeler.Appearance.Row.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(178, Byte))
        Me.GVPeeler.Appearance.Row.Options.UseFont = True
        Me.GVPeeler.Appearance.Row.Options.UseTextOptions = True
        Me.GVPeeler.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVPeeler.AppearancePrint.HeaderPanel.Options.UseTextOptions = True
        Me.GVPeeler.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVPeeler.AppearancePrint.Row.Options.UseTextOptions = True
        Me.GVPeeler.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
        Me.GVPeeler.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None
        Me.GVPeeler.GridControl = Me.GridControl1
        Me.GVPeeler.Name = "GVPeeler"
        Me.GVPeeler.OptionsBehavior.KeepFocusedRowOnUpdate = False
        Me.GVPeeler.OptionsBehavior.ReadOnly = True
        Me.GVPeeler.OptionsFind.FindNullPrompt = ""
        Me.GVPeeler.OptionsNavigation.AutoFocusNewRow = True
        Me.GVPeeler.OptionsSelection.EnableAppearanceFocusedCell = False
        Me.GVPeeler.OptionsView.ShowGroupPanel = False
        Me.GVPeeler.OptionsView.ShowIndicator = False
        '
        'BtnSearch
        '
        Me.BtnSearch.ImageOptions.Image = CType(resources.GetObject("BtnSearch.ImageOptions.Image"), System.Drawing.Image)
        Me.BtnSearch.Location = New System.Drawing.Point(131, 236)
        Me.BtnSearch.Name = "BtnSearch"
        Me.BtnSearch.Size = New System.Drawing.Size(75, 23)
        Me.BtnSearch.TabIndex = 60
        Me.BtnSearch.Text = "بحث"
        '
        'LstPeeler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(346, 262)
        Me.Controls.Add(Me.BtnSearch)
        Me.Controls.Add(Me.BtnMeClose)
        Me.Controls.Add(Me.GridControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "LstPeeler"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Text = "LstPeeler"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GVPeeler, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnMeClose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GVPeeler As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents BtnSearch As DevExpress.XtraEditors.SimpleButton
End Class
