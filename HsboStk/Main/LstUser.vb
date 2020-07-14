
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstUser
    Private Sub GVUsr_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GVUsr.FocusedRowChanged
        ID = Val(GVUsr.GetRowCellValue(e.FocusedRowHandle, "uTrk"))
    End Sub

    Private Sub LstUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVUsr.OptionsView.ShowIndicator = False

        Dim lst = (From s In db.users Where s.uDel = False And s.uTrk <> 1 Select s).ToList
        Me.GridControl1.DataSource = lst
        GVUsr.Columns(0).Caption = "الرقم"
        GVUsr.Columns(1).Caption = "الاسم"
        GVUsr.Columns(2).Caption = "كلمة المرور"
        GVUsr.Columns(3).Visible = False
        GVUsr.Columns(4).Visible = False
        GVUsr.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub
End Class