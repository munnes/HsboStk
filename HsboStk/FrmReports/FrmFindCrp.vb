
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Data
Public Class FrmFindCrp
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
    Private Sub FrmArvStk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillCrop()
        FillStoreType()
        RdoLocal.Checked = True
        '***************************

        HOfGrd = GridControl1.Height
        HOfGrp = GrpCrp.Height
        LocOfGrp = GrpCrp.Location
        LocOfGrd = GridControl1.Location
        LocBtnSearch = BtnSearch.Location
        LocBtnPrint = BtnPrint.Location
        LocBtnExit = BtnExit.Location
        ResetView()
    End Sub
    Private Sub ResetView()
        GrpCrp.Visible = False
        BtnSearch.SetBounds(LocBtnSearch.X, LocOfGrp.Y + 5, BtnSearch.Width, BtnSearch.Height)
        BtnPrint.SetBounds(LocBtnPrint.X, LocOfGrp.Y + 5, BtnPrint.Width, BtnPrint.Height)
        BtnExit.SetBounds(LocBtnExit.X, LocOfGrp.Y + 5, BtnExit.Width, BtnExit.Height)
        GridControl1.SetBounds(LocOfGrd.X, LocBtnSearch.Y + 20, GridControl1.Width, GridControl1.Height)
        GridControl1.Height = HOfGrd + HOfGrp
    End Sub
    Private Sub SetView()
        GrpCrp.Visible = True
        GridControl1.Location = LocOfGrd
        GridControl1.Height = HOfGrd
        BtnSearch.Location = LocBtnSearch
        BtnPrint.Location = LocBtnPrint
        BtnExit.Location = LocBtnExit
    End Sub
    Private Sub FillCrop()
        done = False
        Dim lst = (From c In db.crops Where c.delCrop = False Select c).ToList
        LokCrop.Text = ""
        Me.LokCrop.Properties.DataSource = lst
        LokCrop.Properties.DisplayMember = "cropName"
        LokCrop.Properties.ValueMember = "trkCrop"
        LokCrop.Properties.PopulateColumns()
        LokCrop.Properties.Columns(0).Visible = False
        LokCrop.Properties.Columns(2).Visible = False
        done = True
    End Sub
    Private Sub FillStoreType()
        Dim lstStrType As New List(Of String)
        lstStrType.Add("مخازن مناطق الشراء")
        lstStrType.Add("مخازن المحطات للمحاصيل")
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
    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokCrop.TextChanged
        LokStrType.Text = ""

    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            TheSearch()
        End If
    End Sub
    Private Sub TheSearch()
        GVArStk.Columns.Clear()

        If LokStrType.Text = "مخازن مناطق الشراء" Then
            Dim lst = (From s In db.FindCrpBuy(Val(LokCrop.EditValue)) Select s).ToList
            GridControl1.DataSource = lst
            FormatColumns()
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل" Then
            If RdoLocal.Checked = True Then
                Dim lst = (From s In db.FindCrpArv(Val(LokCrop.EditValue)) Select s).ToList
                GridControl1.DataSource = lst
                FormatColumns()
            ElseIf RdoClient.Checked = True
                Dim lst = (From s In db.FindCrpClnt(Val(LokCrop.EditValue)) Select s).ToList
                GridControl1.DataSource = lst
                FormatColumnsAll()
            End If
        ElseIf LokStrType.Text = "القشارات والغرابيل في المحطات"

            If RdoLocal.Checked = True Then
                Dim lst = (From s In db.PlrFindCrp(Val(LokCrop.EditValue)) Select s).ToList
                GridControl1.DataSource = lst
                FormatColumns()
            ElseIf RdoClient.Checked = True
                Dim lst = (From s In db.PlrFindCrpClnt(Val(LokCrop.EditValue)) Select s).ToList
                GridControl1.DataSource = lst
                FormatColumnsAll()
            End If
            GVArStk.Columns(1).Caption = "قشارة\غربال"
        End If

    End Sub
    'Private Sub Plr()
    '    Dim i As Integer = 0
    '    Dim FstAmt As Double = 0
    '    Dim SecAmt As Double = 0
    '    Dim fst As String
    '    Dim sec As String
    '    Dim store As Str
    '    Dim lstGrd As New List(Of Str)
    '    Dim lstGrdClnt As New List(Of StrClnt)


    '    If RdoLocal.Checked = True Then
    '        Dim lst = (From s In db.PeelerCrpTotal(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue), CalFlag, clnt) Select s).ToList

    '        While i < lst.Count
    '            FstAmt = lst.Item(i).oneUnt
    '            If Not IsNothing(lst.Item(i).twoUnt) Then
    '                SecAmt = lst.Item(i).twoUnt
    '            Else
    '                SecAmt = 0
    '            End If

    '            fst = CType(FstAmt, String) + " " + lst.Item(i).oneName
    '            If SecAmt > 0 Then
    '                sec = CType(SecAmt, String) + " " + lst.Item(i).twoName
    '            Else
    '                sec = ""
    '            End If

    '            lstGrd.Insert(i, New Str With {.crp = lst.Item(i).cropName,
    '            .FAmt = fst,
    '            .SAmt = sec})

    '            i = i + 1
    '        End While
    '        GridControl1.DataSource = lstGrd
    '        FormatColumns()

    '    ElseIf RdoClient.Checked = True
    '        Dim lst = (From s In db.PeelerCrpNoClnt(TDate, FDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue)) Select s).ToList

    '        While i < lst.Count
    '            FstAmt = lst.Item(i).oneUnt
    '            If Not IsNothing(lst.Item(i).twoUnt) Then
    '                SecAmt = lst.Item(i).twoUnt
    '            Else
    '                SecAmt = 0
    '            End If


    '            fst = CType(FstAmt, String) + " " + lst.Item(i).oneName
    '            If SecAmt > 0 Then
    '                sec = CType(SecAmt, String) + " " + lst.Item(i).twoName
    '            Else
    '                sec = ""
    '            End If

    '            lstGrdClnt.Insert(i, New StrClnt With {.crp = lst.Item(i).cropName, .clnt = lst.Item(i).clntCrpName,
    '            .FAmt = fst,
    '            .SAmt = sec})
    '            i = i + 1
    '        End While
    '        GridControl1.DataSource = lstGrdClnt
    '        FormatColumnsAll()
    '    End If
    'End Sub
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
            If LokStrType.Text = "مخازن مناطق الشراء" Then
                Dim rpt As New RepCrpFind
                rpt.XrLHead.Text = LblHead.Text + " في مخازن مناطق الشراء"
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
                rpt.SqlDataSource1.Queries.Item(0).Parameters(0).Value = Val(LokCrop.EditValue)
                rpt.prmCrp.Value = LokCrop.Text
                rpt.prmCrp.Visible = False
                '        '**************************
                rpt.RequestParameters = False
                rpt.ShowPreview()
            ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل" Then
                If RdoLocal.Checked = True Then
                    Dim rpt As New RepArvFind
                    rpt.XrLHead.Text = LblHead.Text + " في مخازن المحطات للمحاصيل"
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
                    rpt.SqlDataSource1.Queries.Item(0).Parameters(0).Value = Val(LokCrop.EditValue)
                    rpt.prmCrp.Value = LokCrop.Text
                    rpt.prmCrp.Visible = False
                    '        '**************************
                    rpt.RequestParameters = False
                    rpt.ShowPreview()
                ElseIf RdoClient.Checked = True
                    Dim rpt As New RepArvClnt
                    rpt.XrLHead.Text = LblHead.Text + " في مخازن المحطات للمحاصيل"
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
                    rpt.SqlDataSource1.Queries.Item(0).Parameters(0).Value = Val(LokCrop.EditValue)
                    rpt.prmCrp.Value = LokCrop.Text
                    rpt.prmCrp.Visible = False
                    '        '**************************
                    rpt.RequestParameters = False
                    rpt.ShowPreview()
                End If
            ElseIf LokStrType.Text = "القشارات والغرابيل في المحطات" Then
                If RdoLocal.Checked = True Then
                    Dim rpt As New RepPlrFind
                    rpt.XrLHead.Text = LblHead.Text + " في القشارات والغرابيل في المحطات"
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
                    rpt.SqlDataSource1.Queries.Item(0).Parameters(0).Value = Val(LokCrop.EditValue)
                    rpt.prmCrp.Value = LokCrop.Text
                    rpt.prmCrp.Visible = False
                    '        '**************************
                    rpt.RequestParameters = False
                    rpt.ShowPreview()
                ElseIf RdoClient.Checked = True
                    Dim rpt As New RepPlrClntFind
                    rpt.XrLHead.Text = LblHead.Text + " في القشارات والغرابيل في المحطات"
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
                    rpt.SqlDataSource1.Queries.Item(0).Parameters(0).Value = Val(LokCrop.EditValue)
                    rpt.prmCrp.Value = LokCrop.Text
                    rpt.prmCrp.Visible = False
                    '        '**************************
                    rpt.RequestParameters = False
                    rpt.ShowPreview()
                End If
            End If

        End If
    End Sub

    Private Function CanSearch() As Boolean
        CanSearch = False
        If LokCrop.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokCrop.Focus()
            LokCrop.SelectAll()
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

    Private Sub LokStrType_TextChanged(sender As Object, e As EventArgs) Handles LokStrType.TextChanged
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            ResetView()

        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل" Or LokStrType.Text = "القشارات والغرابيل في المحطات"
            SetView()

        End If
    End Sub
End Class