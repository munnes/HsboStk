Imports DevExpress.Data
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Public Class LstProduct


    Private Sub LstProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
        Me.Top = Me.Height
        GVProd.OptionsView.ShowIndicator = False
        Dim lst = (From s In db.V_prdUnits Select s).ToList
        Me.GridControl1.DataSource = lst
        GVProd.Columns(0).Caption = "الرقم"
        GVProd.Columns(1).Caption = "المنتج"
        GVProd.Columns(2).Caption = "المحصول"
        GVProd.Columns(5).Caption = "وحدة القياس"
        GVProd.OptionsView.AllowCellMerge = True
        GVProd.Columns(5).OptionsColumn.AllowMerge = False

        GVProd.Columns(3).Visible = False
        GVProd.Columns(4).Visible = False
        GVProd.Columns(6).Visible = False
        GVProd.Columns(7).Visible = False
        GVProd.Columns(8).Visible = False
        GVProd.Columns(0).Width = 30

        'Dim lst = (From s In db.V_Products Where s.delPrd = False Select s).ToList
        'Me.GridControl1.DataSource = lst
        'GVProd.Columns(0).Caption = "الرقم"
        'GVProd.Columns(1).Caption = "المنتج"
        'GVProd.Columns(2).Caption = "المحصول"
        'GVProd.Columns(3).Visible = False
        'GVProd.Columns(4).Visible = False
        'GVProd.Columns(0).Width = 5
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        GVProd.OptionsFind.FindFilterColumns = "*"
        GVProd.OptionsFind.ShowClearButton = False
        GVProd.OptionsFind.ShowFindButton = False
        GVProd.OptionsFind.ShowCloseButton = True
        GVProd.ShowFindPanel()
    End Sub
    Private Sub GVProd_FocusedRowObjectChanged(sender As Object, e As FocusedRowObjectChangedEventArgs) Handles GVProd.FocusedRowObjectChanged
        ID = Val(GVProd.GetRowCellValue(e.RowHandle, "trkProd"))
    End Sub
End Class