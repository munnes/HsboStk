Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstArival
    Private Sub GVArival_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GVArival.FocusedRowChanged
        ID = Val(GVArival.GetRowCellValue(e.FocusedRowHandle, "trkArival"))
    End Sub

    Private Sub LstArival_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVArival.OptionsView.ShowIndicator = False

        Dim lst = (From s In db.arivalLocs Where s.delAr = False Select s).ToList
        Me.GridControl1.DataSource = lst
        GVArival.Columns(0).Caption = "الرقم"
        GVArival.Columns(1).Caption = "منطقة الوصول"
        GVArival.Columns(2).Visible = False
        GVArival.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

End Class