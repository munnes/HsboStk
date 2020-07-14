Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstCrop
    Private Sub GVcrop_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GVCrop.FocusedRowChanged

        ID = Val(GVCrop.GetRowCellValue(e.FocusedRowHandle, "trkCrop"))
    End Sub

    Private Sub LstCrop_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVCrop.OptionsView.ShowIndicator = False
        Dim lst = (From s In db.V_cropUnits Select s).ToList
        Me.GridControl1.DataSource = lst
        GVCrop.Columns(0).Caption = "الرقم"
        GVCrop.Columns(1).Caption = "المحصول"
        GVCrop.Columns(3).Caption = "وحدة القياس"

        GVCrop.Columns(3).OptionsColumn.AllowMerge = False
        GVCrop.OptionsView.AllowCellMerge = True
        GVCrop.Columns(4).Visible = False
        GVCrop.Columns(2).Visible = False
        GVCrop.Columns(5).Visible = False
        GVCrop.Columns(6).Visible = False
        GVCrop.Columns(7).Visible = False
        GVCrop.Columns(0).Width = 5
        'Dim lst = (From s In db.crops Where s.delCrop = False Select s).ToList
        'Me.GridControl1.DataSource = lst
        'GVCrop.Columns(0).Caption = "الرقم"
        'GVCrop.Columns(1).Caption = "المحصول"
        'GVCrop.Columns(2).Visible = False
        'GVCrop.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

    Private Sub GVCrop_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GVCrop.RowCellStyle
        e.Appearance.TextOptions.HAlignment = e.Column.AppearanceCell.HAlignment
    End Sub


End Class