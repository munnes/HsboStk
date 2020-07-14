
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Data
Public Class FrmArvPrdStk
    Private col1, col2, col3, col4 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private clnt As Integer
    Private Sub FrmArvPrdStk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        RdoLocal.Checked = True
        LokClient.Enabled = False
    End Sub
    Private Sub FillLoc()
        done = False
        Dim lst = (From c In db.arivalLocs Where c.delAr = False Select c).ToList
        LokLoc.Text = ""
        Me.LokLoc.Properties.DataSource = lst
        LokLoc.Properties.DisplayMember = "arivalName"
        LokLoc.Properties.ValueMember = "trkArival"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
        done = True
    End Sub
    Private Sub FillStore()
        If done = True And LokLoc.Text <> "" Then
            LokStore.Properties.DataSource = ""
            Dim lst = (From c In db.arivalPrdStores Where c.delAPrd = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokStore.Properties.DataSource = lst
            LokStore.Properties.DisplayMember = "APrdStr"
            LokStore.Properties.ValueMember = "trkAPrdStr"
            LokStore.Properties.PopulateColumns()
            LokStore.Properties.Columns(0).Visible = False
            LokStore.Properties.Columns(2).Visible = False
            LokStore.Properties.Columns(3).Visible = False
        End If
    End Sub

    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokStore.Text = ""
        LokClient.Text = ""
        FillStore()
        FillClient()
    End Sub
    Private Sub TheSearch()
        GVBuyDet.Columns.Clear()

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

        If LokClient.Text <> "" And RdoClient.Checked = True Then
            clnt = Val(LokClient.EditValue)
        Else
            clnt = 0
        End If
        If RdoLocal.Checked = True Then
            Dim lst = (From s In db.ArrivalPrdTotal(TDate, FDate, Val(LokLoc.EditValue), Val(LokStore.EditValue)) Select s).ToList
            GridControl1.DataSource = lst
            FormatColumns()
        ElseIf RdoClient.Checked = True And LokClient.Text <> ""
            Dim lst = (From s In db.ArvlPrdTotalClnt(Val(LokLoc.EditValue), Val(LokStore.EditValue), Val(LokClient.EditValue), TDate, FDate) Select s).ToList
            GridControl1.DataSource = lst
            FormatColumns()
        ElseIf RdoClient.Checked = True And LokClient.Text = ""
            Dim lst = (From s In db.ArvlPrdAllClnt(Val(LokLoc.EditValue), Val(LokStore.EditValue), TDate, FDate) Select s).ToList
            GridControl1.DataSource = lst
            FormatColumnsAll()
        End If
    End Sub
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            TheSearch()
        End If
    End Sub
    Public Function CalFlag() As Boolean
        If RdoLocal.Checked = True Then
            CalFlag = True
        ElseIf RdoClient.Checked = True
            CalFlag = False
        End If
    End Function
    Private Sub FormatColumns()
        col1 = GVBuyDet.Columns(0)
        col2 = GVBuyDet.Columns(1)
        col3 = GVBuyDet.Columns(2)
        '****************
        col1.Caption = "المحصول "
        col2.Caption = "الكمية"
        col3.Caption = "الكمية بمقياس آخر"
    End Sub
    Private Sub FormatColumnsAll()
        col1 = GVBuyDet.Columns(0)
        col2 = GVBuyDet.Columns(1)
        col3 = GVBuyDet.Columns(2)
        col4 = GVBuyDet.Columns(3)
        '****************
        col1.Caption = "المحصول "
        col2.Caption = "العميل "
        col3.Caption = "الكمية"
        col4.Caption = "الكمية بمقياس آخر"
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
            If RdoLocal.Checked = True Then
                ''**************************
                Replocal()
            ElseIf RdoClient.Checked = True And LokClient.Text <> ""
                RepClntPrd()
            ElseIf RdoClient.Checked = True And LokClient.Text = ""
                RepAllClntPrd()
            End If

        End If
    End Sub
    Private Sub Replocal()
        Dim rpt As New RepPrdStk

        Dim head As String = RepHeader()
        Dim wtr As String = RepWater()
        rpt.XrLHead.Text = LblHead.Text + " (استلام محلي)"
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
        rpt.prmLoc.Value = LokLoc.Text
        rpt.prmStr.Value = LokStore.Text

        If ToDate.Text <> "" And FromDate.Text <> "" Then
            rpt.TheDur.Value = "في الفترة بين: (" & FDate & " و" & TDate & ")"
        ElseIf ToDate.Text = "" And FromDate.Text <> "" Then
            rpt.TheDur.Value = "من تاريخ: " & FDate
        ElseIf ToDate.Text <> "" And FromDate.Text = "" Then
            rpt.TheDur.Value = "الى تاريخ: " & TDate
        End If
        '*************************
        rpt.prmLoc.Visible = False
        rpt.prmStr.Visible = False
        rpt.TheDur.Visible = False

        rpt.RequestParameters = False

        rpt.ShowPreview()

    End Sub
    Private Sub RepClntPrd()
        Dim rpt As New RepPrdClnt

        Dim head As String = RepHeader()
        Dim wtr As String = RepWater()
        rpt.XrLHead.Text = LblHead.Text + " (لصالح عميل)"

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
        rpt.SqlDataSource1.Queries.Item(0).Parameters(0).Value = Val(LokLoc.EditValue)
        rpt.SqlDataSource1.Queries.Item(0).Parameters(1).Value = Val(LokStore.EditValue)
        rpt.SqlDataSource1.Queries.Item(0).Parameters(2).Value = Val(LokClient.EditValue)
        rpt.SqlDataSource1.Queries.Item(0).Parameters(3).Value = TDate
        rpt.SqlDataSource1.Queries.Item(0).Parameters(4).Value = FDate


        '********************************
        rpt.prmLoc.Value = LokLoc.Text
        rpt.prmStr.Value = LokStore.Text
        rpt.prmClnt.Value = LokClient.Text
        If ToDate.Text <> "" And FromDate.Text <> "" Then
            rpt.TheDur.Value = "في الفترة بين: (" & FDate & " و" & TDate & ")"
        ElseIf ToDate.Text = "" And FromDate.Text <> "" Then
            rpt.TheDur.Value = "من تاريخ: " & FDate
        ElseIf ToDate.Text <> "" And FromDate.Text = "" Then
            rpt.TheDur.Value = "الى تاريخ: " & TDate
        End If
        '*************************
        rpt.prmLoc.Visible = False
        rpt.prmStr.Visible = False
        rpt.TheDur.Visible = False
        rpt.prmClnt.Visible = False
        rpt.RequestParameters = False

        rpt.ShowPreview()

    End Sub

    Private Sub RepAllClntPrd()
        Dim rpt As New RepAllClnt
        Dim head As String = RepHeader()
        Dim wtr As String = RepWater()
        rpt.XrLHead.Text = LblHead.Text + " (لصالح عميل)"
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
        rpt.SqlDataSource1.Queries.Item(0).Parameters(0).Value = Val(LokLoc.EditValue)
        rpt.SqlDataSource1.Queries.Item(0).Parameters(1).Value = Val(LokStore.EditValue)
        rpt.SqlDataSource1.Queries.Item(0).Parameters(2).Value = TDate
        rpt.SqlDataSource1.Queries.Item(0).Parameters(3).Value = FDate


        '********************************
        rpt.prdLoc.Value = LokLoc.Text
        rpt.prdStr.Value = LokStore.Text

        If ToDate.Text <> "" And FromDate.Text <> "" Then
            rpt.TheDur.Value = "في الفترة بين: (" & FDate & " و" & TDate & ")"
        ElseIf ToDate.Text = "" And FromDate.Text <> "" Then
            rpt.TheDur.Value = "من تاريخ: " & FDate
        ElseIf ToDate.Text <> "" And FromDate.Text = "" Then
            rpt.TheDur.Value = "الى تاريخ: " & TDate
        End If
        '*************************
        rpt.prdLoc.Visible = False
        rpt.prdStr.Visible = False
        rpt.TheDur.Visible = False

        rpt.RequestParameters = False

        rpt.ShowPreview()

    End Sub

    Private Sub btnClnt_Click(sender As Object, e As EventArgs) Handles btnClnt.Click
        LokClient.Text = ""
    End Sub

    Private Function CanSearch() As Boolean
        CanSearch = False
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحطة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
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
        'If RdoClient.Checked = True And LokClient.Text = "" Then
        '    XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم العميل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        '    LokClient.Focus()
        '    LokClient.SelectAll()
        '    Exit Function
        'End If
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
    Private Sub FillClient()
        If done = True And LokLoc.Text <> "" Then
            LokClient.Properties.DataSource = ""
            Dim lst = (From c In db.clientCrps Where c.delClntCrp = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokClient.Properties.DataSource = lst
            LokClient.Properties.DisplayMember = "clntCrpName"
            LokClient.Properties.ValueMember = "trkClntCrp"
            LokClient.Properties.PopulateColumns()
            LokClient.Properties.Columns(0).Visible = False
            LokClient.Properties.Columns(2).Visible = False
            LokClient.Properties.Columns(3).Visible = False
            LokClient.Properties.Columns(4).Visible = False
            LokClient.Properties.Columns(5).Visible = False

        End If
    End Sub

    Private Sub RdoClient_Click(sender As Object, e As EventArgs) Handles RdoClient.Click
        LokClient.Enabled = True
    End Sub

    Private Sub RdoLocal_Click(sender As Object, e As EventArgs) Handles RdoLocal.Click
        LokClient.Text = ""
        LokClient.Enabled = False
    End Sub
End Class