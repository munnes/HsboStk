Imports DevExpress.Data
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstArivalStore
    Private Sub LstArivalStore_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVArivalStore.OptionsView.ShowIndicator = False
        Dim lst = (From s In db.V_ArivalStores Where s.delSa = False Select s.trkAStore, s.AStore, s.arivalName).ToList
        Me.GridControl1.DataSource = lst
        GVArivalStore.Columns(0).Caption = "الرقم"
        GVArivalStore.Columns(1).Caption = "المخزن"
        GVArivalStore.Columns(2).Caption = "منطقة الوصول"
        GVArivalStore.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        GVArivalStore.OptionsFind.FindFilterColumns = "*"

        GVArivalStore.OptionsFind.ShowClearButton = False
        GVArivalStore.OptionsFind.ShowFindButton = False
        GVArivalStore.OptionsFind.ShowCloseButton = True
        GVArivalStore.ShowFindPanel()

    End Sub
    'this will be Ok for both search and normal close 
    Private Sub GVArivalStore_FocusedRowObjectChanged(sender As Object, e As FocusedRowObjectChangedEventArgs) Handles GVArivalStore.FocusedRowObjectChanged
        ID = Val(GVArivalStore.GetRowCellValue(e.RowHandle, "trkAStore"))
    End Sub
End Class