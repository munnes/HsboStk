Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Data

Public Class FrmFindPrd
    Private col1, col2, col3, col4, col5 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private clnt As Integer
    '*******************************
    Private HOfGrd As Integer
    Private HOfGrp As Integer
    Private LocOfGrd As Point
    Private LocOfGrp As Point
    Private LocBtnSearch As Point
    Private LocBtnPrint As Point
    Private LocBtnExit As Point
    Private Sub FrmFindPrd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillProd()
        FillStoreType()
        RdoLocal.Checked = True
        '***************************
        'HOfGrd = GridControl1.Height
        'HOfGrp = GrpCrp.Height
        'LocOfGrp = GrpCrp.Location
        'LocOfGrd = GridControl1.Location
        'LocBtnSearch = BtnSearch.Location
        'LocBtnPrint = BtnPrint.Location
        'LocBtnExit = BtnExit.Location
        'ResetView()
    End Sub
    'Private Sub ResetView()
    '    GrpCrp.Visible = False
    '    BtnSearch.SetBounds(LocBtnSearch.X, LocOfGrp.Y + 5, BtnSearch.Width, BtnSearch.Height)
    '    BtnPrint.SetBounds(LocBtnPrint.X, LocOfGrp.Y + 5, BtnPrint.Width, BtnPrint.Height)
    '    BtnExit.SetBounds(LocBtnExit.X, LocOfGrp.Y + 5, BtnExit.Width, BtnExit.Height)
    '    GridControl1.SetBounds(LocOfGrd.X, LocBtnSearch.Y + 20, GridControl1.Width, GridControl1.Height)
    '    GridControl1.Height = HOfGrd + HOfGrp
    'End Sub
    'Private Sub SetView()
    '    GrpCrp.Visible = True
    '    GridControl1.Location = LocOfGrd
    '    GridControl1.Height = HOfGrd
    '    BtnSearch.Location = LocBtnSearch
    '    BtnPrint.Location = LocBtnPrint
    '    BtnExit.Location = LocBtnExit
    'End Sub
    Private Sub FillProd()
        done = False
        Dim lst = (From c In db.products Where c.delPrd = False Select c).ToList
        LokProd.Text = ""
        Me.LokProd.Properties.DataSource = lst
        LokProd.Properties.DisplayMember = "prodName"
        LokProd.Properties.ValueMember = "trkProd"
        LokProd.Properties.PopulateColumns()
        LokProd.Properties.Columns(0).Visible = False
        LokProd.Properties.Columns(2).Visible = False
        LokProd.Properties.Columns(3).Visible = False
        done = True
    End Sub
    Private Sub FillStoreType()
        Dim lstStrType As New List(Of String)
        lstStrType.Add("مخازن المحطات للمنتجات")
        lstStrType.Add("القشارات والغرابيل في المحطات")
        LokStrType.Properties.DataSource = lstStrType
    End Sub
    Public Function CalFlag() As Boolean
        If RdoLocal.Checked = True Then
            CalFlag = True
        ElseIf RdoClient.Checked = True
            CalFlag = False
        End If
    End Function
    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokProd.TextChanged
        LokStrType.Text = ""
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            TheSearch()
        End If
    End Sub
    Private Sub TheSearch()
        GVArStk.Columns.Clear()
        '*************
        Dim i As Integer = 0
        Dim FstAmt As Double = 0
        Dim SecAmt As Double = 0
        Dim fst As String
        Dim sec As String
        Dim store As Str
        Dim lstGrd As New List(Of Plr)
        Dim lstGrdClnt As New List(Of plrClnt)
        '***************
        If LokStrType.Text = "مخازن المحطات للمنتجات" Then
            If RdoLocal.Checked = True Then
                Dim lst = (From s In db.FindPrdArv(Val(LokProd.EditValue)) Select s).ToList
                GridControl1.DataSource = lst
                FormatColumns()
            ElseIf RdoClient.Checked = True
                Dim lst = (From s In db.FindPrdArvClnt(Val(LokProd.EditValue)) Select s).ToList
                GridControl1.DataSource = lst
                FormatColumnsAll()
            End If
        ElseIf LokStrType.Text = "القشارات والغرابيل في المحطات"

            If RdoLocal.Checked = True Then
                Dim lst = (From s In db.FindPrdPlr(Val(LokProd.EditValue)) Select s).ToList

                While i < lst.Count
                    FstAmt = lst.Item(i).oneUnt
                    If Not IsNothing(lst.Item(i).twoUnt) Then
                        SecAmt = lst.Item(i).twoUnt
                    Else
                        SecAmt = 0
                    End If
                    If FstAmt > 0 Then
                        Dim tbRecv As New FindRecvResult
                        tbRecv = (From s In db.FindRecv(Val(lst.Item(i).trkArival), Val(lst.Item(i).trkPeeler),
                                      Val(LokProd.EditValue), FstAmt, SecAmt, 1) Select s).SingleOrDefault()
                        If Not IsNothing(tbRecv) Then
                            FstAmt = tbRecv.AOne
                            SecAmt = tbRecv.ATwo
                        End If

                    End If
                    If FstAmt > 0 Then
                        Dim tbExp As New FindExpResult
                        tbExp = (From s In db.FindExp(Val(lst.Item(i).trkArival), Val(lst.Item(i).trkPeeler),
                                     Val(LokProd.EditValue), FstAmt, SecAmt) Select s).SingleOrDefault
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

                    lstGrd.Insert(i, New Plr With {.loc = lst.Item(i).arivalName,
                        .thePlr = lst.Item(i).peelerName,
                .FAmt = fst,
                .SAmt = sec})

                    i = i + 1
                End While
                GridControl1.DataSource = lstGrd
                FormatColumns()
            ElseIf RdoClient.Checked = True
                Dim lst = (From s In db.FindPrdPlrClnt(Val(LokProd.EditValue)) Select s).ToList
                While i < lst.Count
                    FstAmt = lst.Item(i).oneUnt
                    If Not IsNothing(lst.Item(i).twoUnt) Then
                        SecAmt = lst.Item(i).twoUnt
                    Else
                        SecAmt = 0
                    End If
                    If FstAmt > 0 Then
                        Dim tbRecv As New FindRecvResult
                        tbRecv = (From s In db.FindRecv(Val(lst.Item(i).trkArival), Val(lst.Item(i).trkPeeler),
                                      Val(LokProd.EditValue), FstAmt, SecAmt, 0) Select s).SingleOrDefault()
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

                    lstGrdClnt.Insert(i, New plrClnt With {.loc = lst.Item(i).arivalName, .thePlr = lst.Item(i).peelerName,
                        .clnt = lst.Item(i).clntCrpName,
                .FAmt = fst,
                .SAmt = sec})
                    i = i + 1
                End While
                GridControl1.DataSource = lstGrdClnt
                FormatColumnsAll()
            End If
            '    GVArStk.Columns(1).Caption = "قشارة\غربال"
        End If

    End Sub

    Private Sub FormatColumns()
        col1 = GVArStk.Columns(0)
        col2 = GVArStk.Columns(1)
        col3 = GVArStk.Columns(2)
        col4 = GVArStk.Columns(3)
        '****************
        col1.Caption = "المنطقة "
        col2.Caption = "المخزن "
        col3.Caption = "الكمية"
        col4.Caption = "الكمية بمقياس آخر"
    End Sub
    Private Sub FormatColumnsAll()
        col1 = GVArStk.Columns(0)
        col2 = GVArStk.Columns(1)
        col3 = GVArStk.Columns(2)
        col4 = GVArStk.Columns(3)
        col5 = GVArStk.Columns(4)
        '****************
        col1.Caption = "المنطقة "
        col2.Caption = "المخزن "
        col3.Caption = "العميل"
        col4.Caption = "الكمية"
        col5.Caption = "الكمية بمقياس آخر"
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

            If LokStrType.Text = "مخازن المحطات للمنتجات" Then
                If RdoLocal.Checked = True Then
                    Dim rpt As New RepPrdFind
                    rpt.XrLHead.Text = LblHead.Text + " في مخازن المحطات للمنتجات"
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
                    rpt.SqlDataSource1.Queries.Item(0).Parameters(0).Value = Val(LokProd.EditValue)
                    rpt.prmPrd.Value = LokProd.Text
                    rpt.prmPrd.Visible = False
                    '        '**************************
                    rpt.RequestParameters = False
                    rpt.ShowPreview()
                ElseIf RdoClient.Checked = True
                    Dim rpt As New RepPrdFindClnt
                    rpt.XrLHead.Text = LblHead.Text + " في مخازن المحطات للمنتجات"
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
                    rpt.SqlDataSource1.Queries.Item(0).Parameters(0).Value = Val(LokProd.EditValue)
                    rpt.prmPrd.Value = LokProd.Text
                    rpt.prmPrd.Visible = False
                    '        '**************************
                    rpt.RequestParameters = False
                    rpt.ShowPreview()
                End If
            ElseIf LokStrType.Text = "القشارات والغرابيل في المحطات" Then
                printPlr()

            End If

        End If
    End Sub
    Private Sub printPlr()
        If CanSearch() = True Then
            TheSearch()
            ''**************************
            Dim rpt As New RepFindPrdPlr

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

            While i < GVArStk.RowCount

                Dim CLoc, CPlr, CelF, CelS, CClnt As New XRTableCell
                Dim row As New XRTableRow
                rpt.XrTable1.BeginInit()
                rpt.XrTable1.Rows.Add(row)
                'row.InsertCell(CLoc, 3)
                'row.InsertCell(CPlr, 2)
                'row.InsertCell(CelF, 1)
                'row.InsertCell(CelS, 0)
                row.Cells.Add(CelS)
                row.Cells.Add(CelF)
                If RdoClient.Checked = True Then
                    row.Cells.Add(CClnt)
                End If
                row.Cells.Add(CPlr)
                row.Cells.Add(CLoc)

                If i = 0 Then
                    CLoc.Text = "المحطة"
                    CPlr.Text = "القشارة\الغربال"
                    CelF.Text = "الكمية"
                    CelS.Text = "الكمية بمقياس اخر"
                    CClnt.Text = "اسم العميل"
                    CLoc.Font = New System.Drawing.Font("Times New Roman", 12.0F, FontStyle.Bold)
                    CPlr.Font = New System.Drawing.Font("Times New Roman", 12.0F, FontStyle.Bold)


                    CelF.Font = New System.Drawing.Font("Times New Roman", 12.0F, FontStyle.Bold)
                    CelS.Font = New System.Drawing.Font("Times New Roman", 12.0F, FontStyle.Bold)
                    If RdoClient.Checked = True Then
                        CClnt.Font = New System.Drawing.Font("Times New Roman", 12.0F, FontStyle.Bold)
                    End If
                    row = New XRTableRow
                    rpt.XrTable1.Rows.Add(row)
                    CLoc = New XRTableCell
                    CPlr = New XRTableCell
                    CelF = New XRTableCell
                    CelS = New XRTableCell
                    CClnt = New XRTableCell
                    row.Cells.Add(CelS)
                    row.Cells.Add(CelF)
                    If RdoClient.Checked = True Then
                        row.Cells.Add(CClnt)
                    End If
                    row.Cells.Add(CPlr)
                    row.Cells.Add(CLoc)

                End If

                CLoc.Text = GVArStk.GetRowCellValue(i, "loc")

                CPlr.Text = GVArStk.GetRowCellValue(i, "thePlr")
                CelF.Text = GVArStk.GetRowCellValue(i, "FAmt")
                CelS.Text = GVArStk.GetRowCellValue(i, "SAmt")
                If RdoClient.Checked = True Then
                    CClnt.Text = GVArStk.GetRowCellValue(i, "clnt")
                End If
                rpt.XrTable1.AdjustSize()

                rpt.XrTable1.EndInit()


                i = i + 1
            End While
            rpt.XrTable1.DeleteRow(rpt.XrTableRow2)

            '********************************

            rpt.prmPrd.Value = LokProd.Text
            '   rpt.prmStr.Value = LokPeeler.Text

            rpt.prmPrd.Visible = False

            ''**************************
            '    rpt.FilterString = "[trkArival]= " & Val(LokLoc.EditValue) & " and [trkPeeler]= " & Val(LokPeeler.EditValue)

            ''**************************
            rpt.RequestParameters = False

            rpt.ShowPreview()
        End If
    End Sub
    Private Function CanSearch() As Boolean
        CanSearch = False
        If LokProd.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokProd.Focus()
            LokProd.SelectAll()
            Exit Function
        End If
        If LokStrType.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال نوع المخازن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokStrType.Focus()
            LokStrType.SelectAll()
            Exit Function
        End If
        'If RdoClient.Checked = True And LokClient.Text = "" Then
        '    XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم العميل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        '    LokClient.Focus()
        '    LokClient.SelectAll()
        '    Exit Function
        'End If

        CanSearch = True
    End Function

    'Private Sub LokStrType_TextChanged(sender As Object, e As EventArgs) Handles LokStrType.TextChanged
    '    If LokStrType.Text = "مخازن مناطق الشراء" Then
    '        ResetView()

    '    ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل" Or LokStrType.Text = "القشارات والغرابيل في المحطات"
    '        SetView()

    '    End If
    'End Sub
End Class