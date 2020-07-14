Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstBuyLoc
    Private Sub GVBuyLoc_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GVBuyLoc.FocusedRowChanged
        ID = Val(GVBuyLoc.GetRowCellValue(e.FocusedRowHandle, "trkBuyLoc"))
    End Sub

    Private Sub LstBuyLoc_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVBuyLoc.OptionsView.ShowIndicator = False

        Dim lst = (From s In db.buyerLocations Where s.delL = False Select s).ToList
        Me.GridControl1.DataSource = lst
        GVBuyLoc.Columns(0).Caption = "الرقم"
        GVBuyLoc.Columns(1).Caption = "منطقة الشراء"
        GVBuyLoc.Columns(2).Visible = False
        GVBuyLoc.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

End Class