Imports DevExpress.Data
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstStoreLoc

    Private Sub LstBuyLoc_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVBuyStore.OptionsView.ShowIndicator = False
        Dim lst = (From s In db.V_BuyerStores Where s.delSL = False Select s.trkBStore, s.bStore, s.buyLoc).ToList
        Me.GridControl1.DataSource = lst
        GVBuyStore.Columns(0).Caption = "الرقم"
        GVBuyStore.Columns(1).Caption = "المخزن"
        GVBuyStore.Columns(2).Caption = "منطقة الشراء"
        GVBuyStore.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        GVBuyStore.OptionsFind.FindFilterColumns = "*"

        GVBuyStore.OptionsFind.ShowClearButton = False
        GVBuyStore.OptionsFind.ShowFindButton = False
        GVBuyStore.OptionsFind.ShowCloseButton = True

        GVBuyStore.ShowFindPanel()

    End Sub
    'this will be Ok for both search and normal close 
    Private Sub GVBuyStore_FocusedRowObjectChanged(sender As Object, e As FocusedRowObjectChangedEventArgs) Handles GVBuyStore.FocusedRowObjectChanged
        ID = Val(GVBuyStore.GetRowCellValue(e.RowHandle, "trkBStore"))
    End Sub
End Class