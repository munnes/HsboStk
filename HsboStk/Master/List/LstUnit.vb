Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class LstUnit
    Private Sub GVUnit_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GVUnit.FocusedRowChanged
        ID = Val(GVUnit.GetRowCellValue(e.FocusedRowHandle, "trkUnit"))
    End Sub

    Private Sub lstUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVUnit.OptionsView.ShowIndicator = False

        Dim lst = (From s In db.units Where s.delUn = False Select s).ToList
        Me.GridControl1.DataSource = lst
        GVUnit.Columns(0).Caption = "الرقم"
        GVUnit.Columns(1).Caption = "الوحدة"
        GVUnit.Columns(2).Visible = False
        GVUnit.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub


End Class