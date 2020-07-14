

Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Data

Public Class FrmInPeeler
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
        If RdoLocal.Checked = True Then
            Dim lst = (From s In db.PeelerTotal(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue)) Select s).ToList

            While i < lst.Count
                FstAmt = lst.Item(i).oneUnt
                If Not IsNothing(lst.Item(i).twoUnt) Then
                    SecAmt = lst.Item(i).twoUnt
                Else
                    SecAmt = 0
                End If
                If FstAmt > 0 Then
                    Dim tbRecv As New RecvTotalResult
                    tbRecv = (From s In db.RecvTotal(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue) _
                                       , lst.Item(i).trkProd, FstAmt, SecAmt, 1) Select s).SingleOrDefault
                    If Not IsNothing(tbRecv) Then
                        FstAmt = tbRecv.AOne
                        SecAmt = tbRecv.ATwo
                    End If

                End If
                If FstAmt > 0 Then
                    Dim tbExp As New ExpTotalResult
                    tbExp = (From s In db.ExpTotal(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue) _
                                       , lst.Item(i).trkProd, FstAmt, SecAmt) Select s).SingleOrDefault
                    If Not IsNothing(tbExp) Then
                        FstAmt = tbExp.AOne
                        SecAmt = tbExp.ATwo
                    End If
                End If

                fst = CType(FstAmt, String) + " " + lst.Item(i).oneName
                If SecAmt > 0 Then
                    sec = CType(SecAmt, String) + " " + lst.Item(i).twoName
                Else
                    sec = ""
                End If

                lstGrd.Insert(i, New Str With {.crp = lst.Item(i).prodName,
                .FAmt = fst,
                .SAmt = sec})

                i = i + 1
            End While
            GridControl1.DataSource = lstGrd
            FormatColumns()
        ElseIf RdoClient.Checked = True And LokClient.Text <> ""
            Dim lst = (From s In db.PeelerClntTotal(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue), Val(LokClient.EditValue)) Select s).ToList

            While i < lst.Count
                FstAmt = lst.Item(i).oneUnt
                If Not IsNothing(lst.Item(i).twoUnt) Then
                    SecAmt = lst.Item(i).twoUnt
                Else
                    SecAmt = 0
                End If
                If FstAmt > 0 Then
                    Dim tbRecv As New RecvTotalClntResult
                    tbRecv = (From s In db.RecvTotalClnt(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue) _
                                       , lst.Item(i).trkProd, FstAmt, SecAmt, Val(LokClient.EditValue)) Select s).SingleOrDefault
                    If Not IsNothing(tbRecv) Then
                        FstAmt = tbRecv.AOne
                        SecAmt = tbRecv.ATwo
                    End If

                End If

                fst = CType(FstAmt, String) + " " + lst.Item(i).oneName
                If SecAmt > 0 Then
                    sec = CType(SecAmt, String) + " " + lst.Item(i).twoName
                Else
                    sec = ""
                End If
                lstGrd.Insert(i, New Str With {.crp = lst.Item(i).prodName,
                .FAmt = fst,
                .SAmt = sec})
                'Next
                i = i + 1
            End While
            GridControl1.DataSource = lstGrd
            FormatColumns()
        ElseIf RdoClient.Checked = True And LokClient.Text = ""
            Dim lst = (From s In db.PlrNoClntTotal(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue)) Select s).ToList

            While i < lst.Count
                FstAmt = lst.Item(i).oneUnt
                If Not IsNothing(lst.Item(i).twoUnt) Then
                    SecAmt = lst.Item(i).twoUnt
                Else
                    SecAmt = 0
                End If
                If FstAmt > 0 Then
                    Dim tbRecv As New RecvTotalResult
                    tbRecv = (From s In db.RecvTotal(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue) _
                                       , lst.Item(i).trkProd, FstAmt, SecAmt, 0) Select s).SingleOrDefault
                    If Not IsNothing(tbRecv) Then
                        FstAmt = tbRecv.AOne
                        SecAmt = tbRecv.ATwo
                    End If

                End If

                fst = CType(FstAmt, String) + " " + lst.Item(i).oneName
                If SecAmt > 0 Then
                    sec = CType(SecAmt, String) + " " + lst.Item(i).twoName
                Else
                    sec = ""
                End If

                lstGrdClnt.Insert(i, New StrClnt With {.crp = lst.Item(i).prodName, .clnt = lst.Item(i).clntCrpName,
                .FAmt = fst,
                .SAmt = sec})
                i = i + 1
            End While
            GridControl1.DataSource = lstGrdClnt
            FormatColumnsAll()
        End If
    End Sub
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
            Dim rpt As New RepPeelerPrd

            Dim head As String = RepHeader()
            Dim wtr As String = RepWater()
            'rpt.XrLHead.Text = LblHead.Text + " (استلام محلي)"
            If RdoLocal.Checked = True Then
                rpt.XrLHead.Text = LblHead.Text + " (محلي)"
            Else
                rpt.XrLHead.Text = LblHead.Text + "(لصالح عميل)"
            End If
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
            Dim i As Integer = 0

            While i < GVInPeeler.RowCount

                Dim cCrp, CelF, CelS, CClnt As New XRTableCell
                Dim row As New XRTableRow
                rpt.XrTable1.BeginInit()
                rpt.XrTable1.Rows.Add(row)

                row.Cells.Add(CelS)
                row.Cells.Add(CelF)
                If LokClient.Text = "" And RdoClient.Checked = True Then
                    row.Cells.Add(CClnt)
                End If
                row.Cells.Add(cCrp)
                If i = 0 Then
                    cCrp.Text = "المنتج"
                    CelF.Text = "الكمية"
                    CelS.Text = "الكمية بمقياس اخر"
                    CClnt.Text = "اسم العميل"
                    cCrp.Font = New System.Drawing.Font("Times New Roman", 12.0F, FontStyle.Bold)
                    CelF.Font = New System.Drawing.Font("Times New Roman", 12.0F, FontStyle.Bold)
                    CelS.Font = New System.Drawing.Font("Times New Roman", 12.0F, FontStyle.Bold)
                    If LokClient.Text = "" And RdoClient.Checked = True Then
                        CClnt.Font = New System.Drawing.Font("Times New Roman", 12.0F, FontStyle.Bold)
                    End If
                    row = New XRTableRow
                        rpt.XrTable1.Rows.Add(row)
                        cCrp = New XRTableCell
                        CelF = New XRTableCell
                    CelS = New XRTableCell
                    CClnt = New XRTableCell
                    row.Cells.Add(CelS)
                    row.Cells.Add(CelF)
                    If LokClient.Text = "" And RdoClient.Checked = True Then
                        row.Cells.Add(CClnt)
                    End If
                    row.Cells.Add(cCrp)

                End If

                    cCrp.Text = GVInPeeler.GetRowCellValue(i, "crp")
                    CelF.Text = GVInPeeler.GetRowCellValue(i, "FAmt")
                CelS.Text = GVInPeeler.GetRowCellValue(i, "SAmt")
                If LokClient.Text = "" And RdoClient.Checked = True Then
                    CClnt.Text = GVInPeeler.GetRowCellValue(i, "clnt")
                End If
                rpt.XrTable1.AdjustSize()
                rpt.XrTable1.EndInit()
                i = i + 1
            End While
            rpt.XrTable1.DeleteRow(rpt.XrTableRow3)

            '********************************
            If ToDate.Text <> "" And FromDate.Text <> "" Then
                rpt.TheDur.Value = "في الفترة بين: (" & FDate & " و" & TDate & ")"
            ElseIf ToDate.Text = "" And FromDate.Text <> "" Then
                rpt.TheDur.Value = "من تاريخ: " & FDate
            ElseIf ToDate.Text <> "" And FromDate.Text = "" Then
                rpt.TheDur.Value = "الى تاريخ: " & TDate
            End If
            If LokClient.Text <> "" Then
                rpt.prmClnt.Value = "اسم العميل:"
                rpt.prmClntName.Value = LokClient.Text
            End If
            rpt.prmLoc.Value = LokLoc.Text
            rpt.prmStr.Value = LokPeeler.Text
            rpt.prmClnt.Visible = False
            rpt.prmClntName.Visible = False
            rpt.TheDur.Visible = False
                rpt.prmLoc.Visible = False
                rpt.prmStr.Visible = False
                ''**************************
                '    rpt.FilterString = "[trkArival]= " & Val(LokLoc.EditValue) & " and [trkPeeler]= " & Val(LokPeeler.EditValue)

                ''**************************
                rpt.RequestParameters = False

                rpt.ShowPreview()
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
        col1.Caption = "المنتج "
        col2.Caption = "الكمية"
        col3.Caption = "الكمية بمقياس آخر"
    End Sub
    Private Sub FormatColumnsAll()
        col1 = GVInPeeler.Columns(0)
        col2 = GVInPeeler.Columns(1)
        col3 = GVInPeeler.Columns(2)
        col4 = GVInPeeler.Columns(3)
        '****************
        col1.Caption = "المنتج "
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

    Private Sub LokLoc_Click(sender As Object, e As EventArgs) Handles LokLoc.Click

    End Sub

    Private Sub RdoClient_Click(sender As Object, e As EventArgs) Handles RdoClient.Click
        LokClient.Enabled = True
    End Sub
End Class