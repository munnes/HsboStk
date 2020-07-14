
Imports DevExpress.Data
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstArPrdStore
    Private Sub LstArPrdStore_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVArPrdStore.OptionsView.ShowIndicator = False
        Dim lst = (From s In db.V_ArivalPrdStores Where s.delAPrd = False Select s.trkAPrdStr, s.APrdStr, s.arivalName).ToList
        Me.GridControl1.DataSource = lst
        GVArPrdStore.Columns(0).Caption = "الرقم"
        GVArPrdStore.Columns(1).Caption = "المخزن"
        GVArPrdStore.Columns(2).Caption = "المحطة"
        GVArPrdStore.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        GVArPrdStore.OptionsFind.FindFilterColumns = "*"

        GVArPrdStore.OptionsFind.ShowClearButton = False
        GVArPrdStore.OptionsFind.ShowFindButton = False
        GVArPrdStore.OptionsFind.ShowCloseButton = True

        GVArPrdStore.ShowFindPanel()

    End Sub
    'this will be Ok for both search and normal close 
    Private Sub GVArivalStore_FocusedRowObjectChanged(sender As Object, e As FocusedRowObjectChangedEventArgs) Handles GVArPrdStore.FocusedRowObjectChanged
        ID = Val(GVArPrdStore.GetRowCellValue(e.RowHandle, "trkAPrdStr"))
    End Sub
End Class