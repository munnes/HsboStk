Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstExport
    Private Sub GVExport_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GVExport.FocusedRowChanged
        ID = Val(GVExport.GetRowCellValue(e.FocusedRowHandle, "trkExpLoc"))
    End Sub

    Private Sub LstExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVExport.OptionsView.ShowIndicator = False

        Dim lst = (From s In db.exportLocs Where s.delExp = False Select s).ToList
        Me.GridControl1.DataSource = lst
        GVExport.Columns(0).Caption = "الرقم"
        GVExport.Columns(1).Caption = "منطقة الصادر"
        GVExport.Columns(2).Visible = False
        GVExport.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub
End Class