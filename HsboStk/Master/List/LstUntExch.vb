
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstUntExch
    Private Sub GVPrdStr_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GVUntEx.FocusedRowChanged
        ID = Val(GVUntEx.GetRowCellValue(e.FocusedRowHandle, "trkUntEx"))

    End Sub

    Private Sub LstUntExch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVUntEx.OptionsView.ShowIndicator = False

        Dim lst = (From s In db.V_unitExches Where s.delUnEx = False Select s).ToList
        Me.GridControl1.DataSource = lst
        GVUntEx.Columns(0).Caption = "الرقم"
        GVUntEx.Columns(5).Caption = "الوحدة"
        GVUntEx.Columns(7).Caption = "التحويل"
        GVUntEx.Columns(1).Visible = False
        GVUntEx.Columns(2).Visible = False
        GVUntEx.Columns(3).Visible = False
        GVUntEx.Columns(4).Visible = False
        GVUntEx.Columns(6).Visible = False
        GVUntEx.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub
End Class