Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstPeeler
    Private Sub GVPeeler_FocusedRowChanged(sender As Object, e As FocusedRowChangedEventArgs) Handles GVPeeler.FocusedRowChanged
        ID = Val(GVPeeler.GetRowCellValue(e.FocusedRowHandle, "trkPeeler"))
    End Sub

    Private Sub LstPeeler_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVPeeler.OptionsView.ShowIndicator = False

        Dim lst = (From s In db.V_Peelers Where s.delPe = False Select s).ToList
        Me.GridControl1.DataSource = lst
        GVPeeler.Columns(0).Caption = "الرقم"
        GVPeeler.Columns(1).Caption = "القشارة أو الغربال"
        GVPeeler.Columns(2).Caption = "المحطة"
        GVPeeler.Columns(3).Visible = False
        GVPeeler.Columns(4).Visible = False
        GVPeeler.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        GVPeeler.OptionsFind.FindFilterColumns = "*"
        GVPeeler.OptionsFind.ShowClearButton = False
        GVPeeler.OptionsFind.ShowFindButton = False
        GVPeeler.OptionsFind.ShowCloseButton = True
        GVPeeler.ShowFindPanel()
    End Sub
End Class