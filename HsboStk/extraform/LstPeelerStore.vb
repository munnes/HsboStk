
Imports DevExpress.Data
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstPeelerStore
    Private Sub LstPeelerStore_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVPeelerStore.OptionsView.ShowIndicator = False
        Dim lst = (From s In db.V_PeelerStores Where s.delSp = False Select s.trkPStore, s.pStore, s.peelerName).ToList
        Me.GridControl1.DataSource = lst
        GVPeelerStore.Columns(0).Caption = "الرقم"
        GVPeelerStore.Columns(1).Caption = "المخزن"
        GVPeelerStore.Columns(2).Caption = "القشارة\الغربال"
        GVPeelerStore.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        GVPeelerStore.OptionsFind.FindFilterColumns = "*"

        GVPeelerStore.OptionsFind.ShowClearButton = False
        GVPeelerStore.OptionsFind.ShowFindButton = False
        GVPeelerStore.OptionsFind.ShowCloseButton = True

        GVPeelerStore.ShowFindPanel()

    End Sub
    'this will be Ok for both search and normal close 
    Private Sub GVArivalStore_FocusedRowObjectChanged(sender As Object, e As FocusedRowObjectChangedEventArgs) Handles GVPeelerStore.FocusedRowObjectChanged
        ID = Val(GVPeelerStore.GetRowCellValue(e.RowHandle, "trkPStore"))
    End Sub
End Class