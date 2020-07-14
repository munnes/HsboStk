Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Data

Public Class FrmBuyStk
    Private col1, col2, col3 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private Sub FrmBuyStk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
    End Sub
    Private Sub FillLoc()
        done = False
        Dim lst = (From c In db.buyerLocations Where c.delL = False Select c).ToList
        LokLoc.Text = ""
        Me.LokLoc.Properties.DataSource = lst
        LokLoc.Properties.DisplayMember = "buyLoc"
        LokLoc.Properties.ValueMember = "trkBuyLoc"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
        done = True
    End Sub
    Private Sub FillStore()
        If done = True And LokLoc.Text <> "" Then
            LokStore.Properties.DataSource = ""
            Dim lst = (From c In db.buyerStores Where c.delSL = False And c.trkBuyLoc = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokStore.Properties.DataSource = lst
            LokStore.Properties.DisplayMember = "bStore"
            LokStore.Properties.ValueMember = "trkBStore"
            LokStore.Properties.PopulateColumns()
            LokStore.Properties.Columns(0).Visible = False
            LokStore.Properties.Columns(2).Visible = False
            LokStore.Properties.Columns(3).Visible = False
        End If
    End Sub

    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokStore.Text = ""
        FillStore()
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            TheSearch()
        End If
    End Sub

    Private Sub TheSearch()

        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        Else
            FDate = CType("1 / 1 / 2010", DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)
        Else
            TDate = CType(Today, DateTime)
        End If
        Dim lst = (From s In db.BuyTotal(TDate, FDate, Val(LokLoc.EditValue), Val(LokStore.EditValue)) Select s).ToList
        GridControl1.DataSource = lst
        FormatColumns()
    End Sub
    Private Sub FormatColumns()
        col1 = GVBuyDet.Columns(0)
        col2 = GVBuyDet.Columns(1)
        col3 = GVBuyDet.Columns(2)
        '****************
        col1.Caption = "المحصول "
        col2.Caption = "الكمية"
        col3.Caption = "الكمية بمقياس آخر"
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        FrmMain.RibbonControl.Enabled = True
        Me.Close()
        done = False
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        FrmMain.RibbonControl.Enabled = True
        done = False
        Me.Close()
    End Sub


    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        If CanSearch() = True Then
            TheSearch()
            ''**************************
            Dim rpt As New RepBuyStk
            rpt.XrLHead.Text = LblHead.Text
            Dim head As String = RepHeader()
            Dim wtr As String = RepWater()
            rpt.XrLHead.Text = LblHead.Text
            If IsHeader = True Then
                rpt.XrPic.ImageUrl = head
                rpt.XrPic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
            End If
            If IsWater Then
                Dim imgWtr As Image = Image.FromFile(wtr)
                rpt.Watermark.Image = imgWtr
                rpt.Watermark.ImageTransparency = 240
            End If
            ''**************************
            rpt.SqlDataSource1.Queries.Item(0).Parameters(0).Value = TDate
            rpt.SqlDataSource1.Queries.Item(0).Parameters(1).Value = FDate
            rpt.SqlDataSource1.Queries.Item(0).Parameters(2).Value = Val(LokLoc.EditValue)
            rpt.SqlDataSource1.Queries.Item(0).Parameters(3).Value = Val(LokStore.EditValue)
            '********************************
            If ToDate.Text <> "" And FromDate.Text <> "" Then
                rpt.TheDur.Value = "في الفترة بين: (" & FDate & " و" & TDate & ")"
            ElseIf ToDate.Text = "" And FromDate.Text <> "" Then
                rpt.TheDur.Value = "من تاريخ: " & FDate
            ElseIf ToDate.Text <> "" And FromDate.Text = "" Then
                rpt.TheDur.Value = "الى تاريخ: " & TDate
            End If
            rpt.prmLoc.Value = LokLoc.Text
            rpt.prmStr.Value = LokStore.Text
            rpt.TheDur.Visible = False
            rpt.prmLoc.Visible = False
            rpt.prmStr.Visible = False
            ''**************************
            rpt.RequestParameters = False

            rpt.ShowPreview()
        End If
    End Sub

    Private Function CanSearch() As Boolean
        CanSearch = False
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المنطقة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If
        If LokStore.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokStore.Focus()
            LokStore.SelectAll()
            Exit Function
        End If
        If FromDate.Text <> "" And ToDate.Text <> "" Then
            If CType(FromDate.Text, DateTime) > CType(ToDate.Text, DateTime) Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء تاريخ البداية لا يمكن أن يكون أكبر من تاريخ النهاية! ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                FromDate.Focus()
                Exit Function
            End If
        End If
        If FromDate.Text <> "" Then
            If CType(FromDate.Text, DateTime) > Today Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل! ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                FromDate.Focus()
                Exit Function
            End If
        End If
        If ToDate.Text <> "" Then
            If CType(ToDate.Text, DateTime) > Today Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل! ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                ToDate.Focus()
                Exit Function
            End If
        End If
        CanSearch = True
    End Function
End Class