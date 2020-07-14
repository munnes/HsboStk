Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstPrdStr
    Private Sub GVPrdStr_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GVPrdStr.FocusedRowChanged
        ID = Val(GVPrdStr.GetRowCellValue(e.FocusedRowHandle, "trkPStr"))
    End Sub

    Private Sub LstPrdStr_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVPrdStr.OptionsView.ShowIndicator = False

        Dim lst = (From s In db.prdStores Where s.delPrdStr = False Select s).ToList
        Me.GridControl1.DataSource = lst
        GVPrdStr.Columns(0).Caption = "الرقم"
        GVPrdStr.Columns(1).Caption = "المخزن"
        GVPrdStr.Columns(2).Visible = False
        GVPrdStr.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

End Class