
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Data

Public Class FrmCrpInPeeler
    Private col1, col2, col3, col4 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private clnt As Integer
    Private Sub FrmInPeeler_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    Private Sub FillPeeler()
        If done = True And LokLoc.Text <> "" Then
            LokPeeler.Properties.DataSource = ""
            Dim lst = (From c In db.peelers Where c.delPe = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokPeeler.Properties.DataSource = lst
            LokPeeler.Properties.DisplayMember = "peelerName"
            LokPeeler.Properties.ValueMember = "trkPeeler"
            LokPeeler.Properties.PopulateColumns()
            LokPeeler.Properties.Columns(0).Visible = False
            LokPeeler.Properties.Columns(2).Visible = False
            LokPeeler.Properties.Columns(3).Visible = False
        End If
    End Sub

    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokPeeler.Text = ""
        LokClient.Text = ""
        FillClient()
        FillPeeler()
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

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            TheSearch()
            'Dim lst = (From s In db.V_InPeelers Where s.trkArival = Val(LokLoc.EditValue) _
            '                                     And s.trkPeeler = Val(LokPeeler.EditValue)
            '           Select s.cropName, s.oneUnt, s.twoUnt).ToList
            'GridControl1.DataSource = lst
            'FormatColumns()
        End If
    End Sub

    Private Sub TheSearch()
        GVInPeeler.Columns.Clear()
        Dim i As Integer = 0
        Dim FstAmt As Double = 0
        Dim SecAmt As Double = 0
        Dim fst As String
        Dim sec As String
        Dim store As Str
        Dim lstGrd As New List(Of Str)
        Dim lstGrdClnt As New List(Of StrClnt)

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
        If RdoLocal.Checked = True Or (RdoClient.Checked = True And LokClient.Text <> "") Then
            Dim lst = (From s In db.PeelerCrpTotal(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue), CalFlag, clnt) Select s).ToList
            GridControl1.DataSource = lst

            FormatColumns()

        ElseIf RdoClient.Checked = True And LokClient.Text = ""
            Dim lst = (From s In db.PeelerCrpNoClnt(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue)) Select s).ToList
            GridControl1.DataSource = lst

            FormatColumnsAll()
        End If
    End Sub
    Public Function CalFlag() As Boolean
        If RdoLocal.Checked = True Then
            CalFlag = True
        ElseIf RdoClient.Checked = True
            CalFlag = False
        End If
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
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click

        If CanSearch() = True Then
            TheSearch()
            ''**************************
            If RdoLocal.Checked = True Or (RdoClient.Checked = True And LokClient.Text <> "") Then
                Dim rpt As New RepCrpInPlr
                If RdoLocal.Checked = True Then
                    rpt.XrLHead.Text = LblHead.Text + " (استلام محلي)"
                Else
                    rpt.XrLHead.Text = LblHead.Text + "(استلام من عميل)"
                End If

                Dim head As String = RepHeader()
                Dim wtr As String = RepWater()

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
                rpt.SqlDataSource1.Queries.Item(0).Parameters(3).Value = Val(LokPeeler.EditValue)
                rpt.SqlDataSource1.Queries.Item(0).Parameters(4).Value = CalFlag()
                rpt.SqlDataSource1.Queries.Item(0).Parameters(5).Value = clnt
                '********************************
                rpt.prmLoc.Value = LokLoc.Text
                rpt.prmPlr.Value = LokPeeler.Text
                If LokClient.Text <> "" Then
                    rpt.prmClnt.Value = "استلام من العميل: "
                    rpt.prmClName.Value = LokClient.Text
                End If

                If ToDate.Text <> "" And FromDate.Text <> "" Then
                    rpt.TheDur.Value = "في الفترة بين: (" & FDate & " و" & TDate & ")"
                ElseIf ToDate.Text = "" And FromDate.Text <> "" Then
                    rpt.TheDur.Value = "من تاريخ: " & FDate
                ElseIf ToDate.Text <> "" And FromDate.Text = "" Then
                    rpt.TheDur.Value = "الى تاريخ: " & TDate
                End If
                '*************************
                rpt.prmLoc.Visible = False
                rpt.prmPlr.Visible = False
                rpt.TheDur.Visible = False
                rpt.prmClnt.Visible = False
                rpt.prmClName.Visible = False
                ''**************************
                rpt.RequestParameters = False
                rpt.ShowPreview()

            ElseIf RdoClient.Checked = True And LokClient.Text = ""
                Dim rpt As New RepCrpClntPlr
                rpt.XrLHead.Text = LblHead.Text
                Dim head As String = RepHeader()
                Dim wtr As String = RepWater()
                'rpt.XrLHead.Text = LblHead.Text + "(استلام من عميل)"
                rpt.XrLHead.Text = LblHead.Text + "(استلام من عميل)"
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
                rpt.SqlDataSource1.Queries.Item(0).Parameters(3).Value = Val(LokPeeler.EditValue)

                '********************************
                rpt.prmLoc.Value = LokLoc.Text
                rpt.prmStr.Value = LokPeeler.Text

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

                ''**************************
                rpt.RequestParameters = False
                rpt.ShowPreview()
            End If
        End If
    End Sub

    Private Sub btnClnt_Click(sender As Object, e As EventArgs) Handles btnClnt.Click
        LokClient.Text = ""
    End Sub

    Private Sub FormatColumns()
        col1 = GVInPeeler.Columns(0)
        col2 = GVInPeeler.Columns(1)
        col3 = GVInPeeler.Columns(2)
        '****************
        col1.Caption = "المحصول "
        col2.Caption = "الكمية"
        col3.Caption = "الكمية بمقياس آخر"
    End Sub
    Private Sub FormatColumnsAll()
        col1 = GVInPeeler.Columns(0)
        col2 = GVInPeeler.Columns(1)
        col3 = GVInPeeler.Columns(2)
        col4 = GVInPeeler.Columns(3)
        '****************
        col1.Caption = "المحصول "
        col2.Caption = "العميل "
        col3.Caption = "الكمية"
        col4.Caption = "الكمية بمقياس آخر"
    End Sub
    Private Function CanSearch() As Boolean
        CanSearch = False
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحطة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If
        If LokPeeler.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم القشارة\الغربال ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokPeeler.Focus()
            LokPeeler.SelectAll()
            Exit Function
        End If
        If RdoLocal.Checked = False And RdoClient.Checked = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء الرجاء تحديد مصدر المحاصيل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CanSearch = True
    End Function

    Private Sub RdoLocal_Click(sender As Object, e As EventArgs) Handles RdoLocal.Click
        LokClient.Text = ""
        LokClient.Enabled = False
    End Sub

    Private Sub RdoClient_Click(sender As Object, e As EventArgs) Handles RdoClient.Click
        LokClient.Enabled = True
    End Sub
End Class