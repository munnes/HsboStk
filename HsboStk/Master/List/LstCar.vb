Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid

Public Class LstCar
    Private Sub GVCar_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GVCar.FocusedRowChanged
        ID = Val(GVCar.GetRowCellValue(e.FocusedRowHandle, "trkCar"))
    End Sub

    Private Sub LstCar_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVCar.OptionsView.ShowIndicator = False

        Dim lst = (From s In db.cars Where s.delC = False Select s).ToList
        Me.GridControl1.DataSource = lst
        GVCar.Columns(0).Caption = "الرقم"
        GVCar.Columns(1).Caption = "رقم لوحة العربة"
        GVCar.Columns(2).Visible = False
        GVCar.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

End Class