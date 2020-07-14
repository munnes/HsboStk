
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel

Public Class FrmStoreProcess
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Private FDate, TDate As DateTime

    Public repUnit As Repository.RepositoryItemLookUpEdit
    Private HOfGrd As Integer
    Private HOfGrp As Integer
    Private LocOfGrd As Point
    Private LocOfGrp As Point
    Private LocBtnSearch As Point
    Private LocBtnPrint As Point
    Private LocBtnExit As Point
    Private Sub FrmStoreProcess_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        'Dim lstPros As New List(Of String)
        'lstPros.Add("ضبط المخزن خروج")
        'lstPros.Add("ضبط المخزن دخول")
        'LokProcess.Properties.DataSource = lstPros
        '**********************
        Dim lstStrType As New List(Of String)
        lstStrType.Add("مخازن مناطق الشراء")
        lstStrType.Add("مخازن المحطات للمحاصيل")
        lstStrType.Add("مخازن المحطات للمنتجات")

        LokStrType.Properties.DataSource = lstStrType

        '********************
        RdoLocal.Checked = True
        '  LokClient.Enabled = False
        '*******************
        HOfGrd = GridControl1.Height
        HOfGrp = GrpCrp.Height
        LocOfGrp = GrpCrp.Location
        LocOfGrd = GridControl1.Location
        LocBtnSearch = BtnSearch.Location
        LocBtnPrint = BtnPrint.Location
        LocBtnExit = BtnExit.Location
        ResetView()
        'GrpCrp.Visible = False
        'BtnSearch.SetBounds(LocBtnSearch.X, LocOfGrp.Y, BtnSearch.Width, BtnSearch.Height)
        'BtnPrint.SetBounds(LocBtnPrint.X, LocOfGrp.Y, BtnPrint.Width, BtnPrint.Height)
        'BtnExit.SetBounds(LocBtnExit.X, LocOfGrp.Y, BtnExit.Width, BtnExit.Height)

        'GridControl1.SetBounds(LocOfGrd.X, LocBtnSearch.Y + 10, GridControl1.Width, GridControl1.Height)
        'GridControl1.Height = HOfGrd + HOfGrp + 10
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            GVBuyView.Columns.Clear()
            FillGrid()
            FormatColumns()
        End If
    End Sub
    Public Sub ResetAtClose()
        '************fill store ,and save req number should be rest, b/c when re-open form 
        done = False
        Me.Close()
        FrmMain.RibbonControl.Enabled = True
    End Sub
    Private Function CanSearch() As Boolean
        CanSearch = False
        If LokStrType.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً  الرجاء تحديد نوع المخزن! ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            LokStrType.Focus()
            Exit Function
        End If
        If FromDate.Text <> "" And ToDate.Text <> "" Then

            If CType(FromDate.Text, DateTime) > CType(ToDate.Text, DateTime) Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً  تاريخ البداية لا يمكن أن يكون أكبر من تاريخ النهاية! ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
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

    Public Function CalFlag() As Boolean
        If RdoLocal.Checked = True Then
            CalFlag = True
        ElseIf RdoClient.Checked = True
            CalFlag = False
        End If
    End Function
#Region "Grid"
    Private Sub FillGrid()
        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)
        End If
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            If LokProcess.Text = "" Then
                FillGridBuy()
            Else
                FillGridBuyPrs(Val(LokProcess.EditValue))
            End If
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            If LokProcess.Text = "" Then
                FillGridArv()
            Else
                FillGridArvPrs(Val(LokProcess.EditValue))
            End If
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            If LokProcess.Text = "" Then
                FillGridArvPrd()
            Else
                FillGridArvPrdPrs(Val(LokProcess.EditValue))
            End If
        End If
        ' ********************Add repository button for details
        GVBuyView.Columns(0).Visible = False
        GVBuyView.Columns.Add()
        GVBuyView.Columns(5).ColumnEdit = repBtnView
        GVBuyView.Columns.Add()
        GVBuyView.Columns(6).ColumnEdit = repBtnDel

    End Sub
    Private Sub FillGridBuy()
        Select Case True
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0000
                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                   And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList

                GridControl1.DataSource = lst

            '0001
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                   And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '0010
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                   And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0011
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                    And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0100
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                    And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0101
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                    And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0110
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0111
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst


            '    '********************************
            '    '*****************missing options due to store location relation ( 1000, 1001, 1010, 1011)
            '    '*********************************
            '    '1100
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.buyDate >= FDate.ToShortDateString _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '1101
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.buyDate >= FDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1110
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.buyDate <= TDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1111
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.delBuy = False And s.trkPrs <> 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst
        End Select
    End Sub
    Private Sub FillGridBuyPrs(ByVal ThePrs As Integer)
        Select Case True
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0000
                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                   And s.trkPrs = ThePrs _
                                                   And s.delBuy = False
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList

                GridControl1.DataSource = lst

            '0001
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                   And s.delBuy = False _
                                                     And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '0010
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                   And s.delBuy = False And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0011
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                    And s.delBuy = False And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0100
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                    And s.delBuy = False And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0101
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                    And s.delBuy = False And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0110
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0111
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.delBuy = False And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst


            '    '********************************
            '    '*****************missing options due to store location relation ( 1000, 1001, 1010, 1011)
            '    '*********************************
            '    '1100
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.buyDate >= FDate.ToShortDateString _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '1101
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.buyDate >= FDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1110
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.buyDate <= TDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1111
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.delBuy = False And s.trkPrs = ThePrs
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate, s.prsName).ToList
                GridControl1.DataSource = lst
        End Select
    End Sub
    '**************************************************************************************************
    Private Sub FillGridArv()

        Select Case True
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0000
                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                   And s.SArDate >= FDate.ToShortDateString _
                                                   And s.SArDate <= TDate.ToShortDateString _
                                                   And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList

                GridControl1.DataSource = lst

            '0001
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                   And s.SArDate >= FDate.ToShortDateString _
                                                   And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '0010
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                   And s.SArDate <= TDate.ToShortDateString _
                                                   And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0011
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                    And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0100
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.SArDate >= FDate.ToShortDateString _
                                                   And s.SArDate <= TDate.ToShortDateString _
                                                    And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0101
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.SArDate >= FDate.ToShortDateString _
                                                    And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0110
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.SArDate <= TDate.ToShortDateString _
                                                            And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0111
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst


            '    '********************************
            '    '*****************missing options due to store location relation ( 1000, 1001, 1010, 1011)
            '    '*********************************
            '    '1100
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_aStoreReqs Where s.SArDate >= FDate.ToShortDateString _
                                                   And s.SArDate <= TDate.ToShortDateString _
                                                            And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '1101
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_aStoreReqs Where s.SArDate >= FDate.ToShortDateString _
                                                            And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1110
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_aStoreReqs Where s.SArDate <= TDate.ToShortDateString _
                                                            And s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1111
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_aStoreReqs Where s.delAS = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst
        End Select

    End Sub

    Private Sub FillGridArvPrs(ByVal ThePrs As Integer)
        Select Case True
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0000
                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                   And s.SArDate >= FDate.ToShortDateString _
                                                   And s.SArDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                   And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList

                GridControl1.DataSource = lst

            '0001
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                   And s.SArDate >= FDate.ToShortDateString And s.trkPrs = ThePrs _
                                                   And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '0010
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                   And s.SArDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                   And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0011
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) And s.trkPrs = ThePrs _
                                                    And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0100
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.SArDate >= FDate.ToShortDateString _
                                                   And s.SArDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                    And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0101
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.SArDate >= FDate.ToShortDateString And s.trkPrs = ThePrs _
                                                    And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0110
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.SArDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                            And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0111
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                Dim lst = (From s In db.V_aStoreReqs Where s.trkArival = Val(LokLoc.EditValue) And s.trkPrs = ThePrs _
                                                   And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst


            '    '********************************
            '    '*****************missing options due to store location relation ( 1000, 1001, 1010, 1011)
            '    '*********************************
            '    '1100
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_aStoreReqs Where s.SArDate >= FDate.ToShortDateString _
                                                   And s.SArDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                            And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '1101
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_aStoreReqs Where s.SArDate >= FDate.ToShortDateString And s.trkPrs = ThePrs _
                                                            And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1110
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_aStoreReqs Where s.SArDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                            And s.delAS = False And s.isLocal = CalFlag()
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1111
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_aStoreReqs Where s.delAS = False And s.isLocal = CalFlag() And s.trkPrs = ThePrs
                           Select s.trkASReq, s.arivalName, s.AStore, s.SArDate, s.prsName).ToList
                GridControl1.DataSource = lst
        End Select

    End Sub
    '******************************************************************************************************
    Private Sub FillGridArvPrd()
        Select Case True
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0000
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString _
                                                   And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList

                GridControl1.DataSource = lst

            '0001
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString _
                                                   And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '0010
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate <= TDate.ToShortDateString _
                                                   And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0011
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                    And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0100
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString _
                                                    And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0101
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString _
                                                    And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0110
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate <= TDate.ToShortDateString _
                                                            And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0111
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst


            '    '********************************
            '    '*****************missing options due to store location relation ( 1000, 1001, 1010, 1011)
            '    '*********************************
            '    '1100
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString _
                                                            And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '1101
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Receives Where s.recvDate >= FDate.ToShortDateString _
                                                            And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1110
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.recvDate <= TDate.ToShortDateString _
                                                            And s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1111
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Receives Where s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs <> 1
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst
        End Select
    End Sub

    Private Sub FillGridArvPrdPrs(ByVal ThePrs As Integer)
        Select Case True
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0000
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                   And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList

                GridControl1.DataSource = lst

            '0001
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString And s.trkPrs = ThePrs _
                                                   And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '0010
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                   And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0011
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) And s.trkPrs = ThePrs _
                                                    And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0100
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                    And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0101
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString And s.trkPrs = ThePrs _
                                                    And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0110
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                            And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '0111
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) And s.trkPrs = ThePrs _
                                                   And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst


            '    '********************************
            '    '*****************missing options due to store location relation ( 1000, 1001, 1010, 1011)
            '    '*********************************
            '    '1100
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                            And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst

            '    '1101
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Receives Where s.recvDate >= FDate.ToShortDateString And s.trkPrs = ThePrs _
                                                            And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1110
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.recvDate <= TDate.ToShortDateString And s.trkPrs = ThePrs _
                                                            And s.delRecv = False And s.isLocal = CalFlag()
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst
            '1111
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Receives Where s.delRecv = False And s.isLocal = CalFlag() And s.trkPrs = ThePrs
                           Select s.trkRecv, s.arivalName, s.APrdStr, s.recvDate, s.prsName).ToList
                GridControl1.DataSource = lst
        End Select
    End Sub

    '******************************************************************************************************
#End Region
#Region "FillProcess"
    Private Sub FillPrsBuy()
        Dim lst = (From s In db.processBuys Where s.trkPrs <> 1 Select s)
        LokProcess.Properties.DataSource = lst
        LokProcess.Properties.ValueMember = "trkPrs"
        LokProcess.Properties.DisplayMember = "prsName"
        LokProcess.Properties.PopulateColumns()
        LokProcess.Properties.Columns(0).Visible = False
    End Sub
    Private Sub FillPrsArv()
        Dim lst = (From s In db.processArvs Where s.trkPrs <> 1 Select s)
        LokProcess.Properties.DataSource = lst
        LokProcess.Properties.ValueMember = "trkPrs"
        LokProcess.Properties.DisplayMember = "prsName"
        LokProcess.Properties.PopulateColumns()
        LokProcess.Properties.Columns(0).Visible = False
    End Sub
    Private Sub FillPrsRcv()
        Dim lst = (From s In db.processRcvs Where s.trkPrs <> 1 Select s)
        LokProcess.Properties.DataSource = lst
        LokProcess.Properties.ValueMember = "trkPrs"
        LokProcess.Properties.DisplayMember = "prsName"
        LokProcess.Properties.PopulateColumns()
        LokProcess.Properties.Columns(0).Visible = False
    End Sub

#End Region
    Private Sub FormatColumns()
        col1 = GVBuyView.Columns(1)
        col2 = GVBuyView.Columns(2)
        col3 = GVBuyView.Columns(3)
        col4 = GVBuyView.Columns(4)
        col5 = GVBuyView.Columns(5)
        col6 = GVBuyView.Columns(6)
        '****************
        col1.Caption = "المنطقة"
        col2.Caption = "المخزن "
        col3.Caption = "التاريخ"
        col4.Caption = "نوع العملية"
        col5.Caption = "عرض التفاصيل"
        col6.Caption = "حذف"
        GVBuyView.Columns(5).Width = 40
        GVBuyView.Columns(5).Visible = True
        GVBuyView.Columns(6).Width = 10
        GVBuyView.Columns(6).Visible = True
    End Sub

    Private Sub ResetView()
        GrpCrp.Visible = False
        BtnSearch.SetBounds(LocBtnSearch.X, LocOfGrp.Y, BtnSearch.Width, BtnSearch.Height)
        BtnPrint.SetBounds(LocBtnPrint.X, LocOfGrp.Y, BtnPrint.Width, BtnPrint.Height)
        BtnExit.SetBounds(LocBtnExit.X, LocOfGrp.Y, BtnExit.Width, BtnExit.Height)
        GridControl1.SetBounds(LocOfGrd.X, LocBtnSearch.Y + 10, GridControl1.Width, GridControl1.Height)
        GridControl1.Height = HOfGrd + HOfGrp + 10
    End Sub
    Private Sub SetView()
        GrpCrp.Visible = True
        GridControl1.Location = LocOfGrd
        GridControl1.Height = HOfGrd
        BtnSearch.Location = LocBtnSearch
        BtnPrint.Location = LocBtnPrint
        BtnExit.Location = LocBtnExit
    End Sub

    Private Sub LokStrType_TextChanged(sender As Object, e As EventArgs) Handles LokStrType.TextChanged
        done = False
        ClearForm()
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            FillPrsBuy()
            FillBuyLoc()
            '******************
            ResetView()
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            FillPrsArv()
            FillArivalLoc()
            '******************
            SetView()
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            FillPrsRcv()
            FillArivalLoc()
            '******************
            SetView()
        End If
        done = True
    End Sub

#Region "Fill"
    Private Sub FillBuyLoc()
        Dim lst = (From s In db.buyerLocations Where s.delL = 0 Select s).ToList
        LokLoc.Properties.DataSource = lst
        LokLoc.Properties.ValueMember = "trkBuyLoc"
        LokLoc.Properties.DisplayMember = "buyLoc"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
    End Sub
    Private Sub FillArivalLoc()
        Dim lst = (From c In db.arivalLocs Where c.delAr = False Select c).ToList
        Me.LokLoc.Properties.DataSource = lst
        LokLoc.Properties.DisplayMember = "arivalName"
        LokLoc.Properties.ValueMember = "trkArival"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        ResetAtClose()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        ResetAtClose()
    End Sub
#Region "Report"
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If CanSearch() = True And LokStrType.Text <> "" Then
            GVBuyView.Columns.Clear()
            FillGrid()
            FormatColumns()

            If RdoLocal.Checked = True Then
                isLoc = 1
                Clnt = 0
            ElseIf RdoClient.Checked = True
                isLoc = 0
                'Clnt = Val(LokClient.EditValue)
            End If
            If LokStrType.Text = "مخازن مناطق الشراء" Then
                RepBuy(Val(LokProcess.EditValue))
            ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
                RepArrive(isLoc, Clnt, Val(LokProcess.EditValue))
            ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
                RepArvPrd(isLoc, Clnt, Val(LokProcess.EditValue))
            End If
        End If
    End Sub
    Private Sub RepBuy(ByVal ThePrs As Integer)
        Dim rpt As New RepStkBuy
        rpt.XrLHead.Text = "ضبط المخزن (مخازن مناطق الشراء)"

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
        '******************
        If ThePrs <> 0 Then

            Select Case True
                '0000
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "# and [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= " & ThePrs
                '0001
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "#" & " And [trkPrs]= " & ThePrs
                '0010
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= " & ThePrs
                '0011
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) & " And [trkPrs]= " & ThePrs
                '0100
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "# and [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= " & ThePrs
                '0101
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "#" & " And [trkPrs]= " & ThePrs
                '0110
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " and [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= " & ThePrs
                '0111
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkPrs]= " & ThePrs

                '1100
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[buyDate] >= #" & FDate.ToShortDateString & "# And [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= " & ThePrs

                '    '1101
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[buyDate] >= #" & FDate.ToShortDateString & "#" & " And [trkPrs]= " & ThePrs
                '1110
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= " & ThePrs

                '1111
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = " [trkPrs]= " & ThePrs
            End Select
        Else

            '***************************
            Select Case True
                '0000
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "# and [buyDate] <= #" & TDate.ToShortDateString & "#"
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "# And [trkPrs] <> 1"
                '0010
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [buyDate] <= #" & TDate.ToShortDateString & "# And [trkPrs] <> 1"
                '0011
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) & " And [trkPrs] <> 1"
                '0100
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "# and [buyDate] <= #" & TDate.ToShortDateString & "# And [trkPrs] <> 1"
                '0101
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "# And [trkPrs] <> 1"
                '0110
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " and [buyDate] <= #" & TDate.ToShortDateString & "# And [trkPrs] <> 1"
                '0111
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkPrs] <> 1"

                '1100
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[buyDate] >= #" & FDate.ToShortDateString & "# And [buyDate] <= #" & TDate.ToShortDateString & "# And [trkPrs] <> 1"

                '    '1101
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[buyDate] >= #" & FDate.ToShortDateString & "# And [trkPrs] <> 1"
                '1110
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [buyDate] <= #" & TDate.ToShortDateString & "# And [trkPrs] <> 1"

                '1111
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = "[trkPrs] <> 1"
            End Select
        End If
        rpt.TheFilter.Visible = False
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub

    Private Sub RepArrive(ByVal IsLoc As Boolean, ByVal clnt As Integer, ByVal ThePrs As Integer)
        Dim rpt As New RepStkArv
        rpt.XrLHead.Text = "ضبط المخزن (مخازن المحطات للمحاصيل)"
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
        '******************
        If ThePrs <> 0 Then
            Select Case True
                '0000
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                        & " and [SArDate] >= #" & FDate.ToShortDateString & "# and [SArDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & "and [isLocal]= " & IsLoc
                '0001
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                        & " and [SArDate] >= #" & FDate.ToShortDateString & "# and [trkPrs] " & ThePrs & "and [isLocal]= " & IsLoc
                '0010
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                        & " and [SArDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]=  " & ThePrs & "and  [isLocal]= " & IsLoc
                '0011
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) & " and [trkPrs]=  " & ThePrs & "and  [isLocal]= " & IsLoc
                '0100
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & " and [SArDate] >= #" & FDate.ToShortDateString & "# and [SArDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & "and [isLocal]= " & IsLoc
                '0101
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & " and [SArDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]=  " & ThePrs & "and [isLocal]= " & IsLoc
                '0110
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [SArDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]=  " & ThePrs & "and  [isLocal]= " & IsLoc
                '0111
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkPrs]=  " & ThePrs & " and [isLocal]= " & IsLoc

                '1100
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[SArDate] >= #" & FDate.ToShortDateString & "# And [SArDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]=  " & ThePrs & "and  [isLocal]= " & IsLoc

                '    '1101
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[SArDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & " and  [isLocal]= " & IsLoc
                '1110
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [SArDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]=  " & ThePrs & " and  [isLocal]= " & IsLoc

                '1111
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = "[trkPrs]=  " & ThePrs & "and  [isLocal]= " & IsLoc
            End Select
        Else

            Select Case True
                '0000
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                            & " and [SArDate] >= #" & FDate.ToShortDateString & "# and [SArDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0001
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                            & " and [SArDate] >= #" & FDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0010
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                            & " and [SArDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0011
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) & " and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0100
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                            & " and [SArDate] >= #" & FDate.ToShortDateString & "# and [SArDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1 "
                '0101
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                            & " and [SArDate] >= #" & FDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0110
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [SArDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0111
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"

                '1100
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[SArDate] >= #" & FDate.ToShortDateString & "# And [SArDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"

                '    '1101
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[SArDate] >= #" & FDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '1110
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [SArDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"

                '1111
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = "[isLocal]= " & IsLoc & " And [trkPrs] <> 1"
            End Select
        End If
        rpt.TheFilter.Visible = False
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub

    Private Sub RepArvPrd(ByVal IsLoc As Boolean, ByVal clnt As Integer, ByVal ThePrs As Integer)
        Dim rpt As New RepStkArvPrd
        rpt.XrLHead.Text = "ضبط المخزن (مخازن المحطات للمنتجات)"
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
        If ThePrs <> 0 Then

            Select Case True
                '0000
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                    & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & " and [isLocal]= " & IsLoc
                '0001
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                    & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & " and [isLocal]= " & IsLoc
                '0010
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                    & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & " and  [isLocal]= " & IsLoc
                '0011
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) & " and [trkPrs]= " & ThePrs & " and  [isLocal]= " & IsLoc
                '0100
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & " and  [isLocal]= " & IsLoc
                '0101
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & " and  [isLocal]= " & IsLoc
                '0110
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & " and  [isLocal]= " & IsLoc
                '0111
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkPrs]= " & ThePrs & " and [isLocal]= " & IsLoc

                '1100
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[recvDate] >= #" & FDate.ToShortDateString & "# And [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & " and  [isLocal]= " & IsLoc

                '    '1101
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & " and  [isLocal]= " & IsLoc
                '1110
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= " & ThePrs & " and  [isLocal]= " & IsLoc

                '1111
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = "[trkPrs] = " & ThePrs & " and [isLocal]= " & IsLoc
            End Select
        Else
            Select Case True
                '0000
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                    & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0001
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                    & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0010
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                    & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0011
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) & " and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0100
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0101
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " and [recvDate] >= #" & FDate.ToShortDateString & "#  and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0110
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [recvDate] <= #" & TDate.ToShortDateString & "#  and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '0111
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and  [isLocal]= " & IsLoc & " And [trkPrs] <> 1"

                '1100
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[recvDate] >= #" & FDate.ToShortDateString & "# And [recvDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"

                '    '1101
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[recvDate] >= #" & FDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"
                '1110
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [recvDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & IsLoc & " And [trkPrs] <> 1"

                '1111
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = "[isLocal]= " & IsLoc & " And [trkPrs] <> 1"
            End Select

        End If

        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub

    Private Sub btnLok_Click(sender As Object, e As EventArgs) Handles btnLok.Click
        LokLoc.Text = ""
        LokStore.Properties.DataSource = ""
        LblHead.Focus()
    End Sub

    Private Sub btnPrs_Click(sender As Object, e As EventArgs) Handles btnPrs.Click
        LokProcess.Text = ""
        LblHead.Focus()
    End Sub

    Private Sub btnStr_Click(sender As Object, e As EventArgs) Handles btnStr.Click
        LokStore.Text = ""
        LblHead.Focus()
    End Sub

    Private Sub BtnStkType_Click(sender As Object, e As EventArgs) Handles BtnStkType.Click
        LokStrType.Text = ""
        LokLoc.Properties.DataSource = ""
        LokStore.Properties.DataSource = ""
        LokProcess.Properties.DataSource = ""
        LblHead.Focus()
        ResetView()
    End Sub
#End Region
    Private Sub FillBuyStore()
        Dim lst = (From c In db.buyerStores Where c.delSL = False And c.trkBuyLoc = Val(LokLoc.EditValue.ToString()) Select c).ToList
        LokStore.Properties.DataSource = lst
        LokStore.Properties.DisplayMember = "bStore"
        LokStore.Properties.ValueMember = "trkBStore"
        LokStore.Properties.PopulateColumns()
        LokStore.Properties.Columns(0).Visible = False
        LokStore.Properties.Columns(2).Visible = False
        LokStore.Properties.Columns(3).Visible = False
    End Sub

    Private Sub FillArriveCrpStore()
        Dim lst = (From c In db.arivalStores Where c.delSa = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
        LokStore.Properties.DataSource = lst
        LokStore.Properties.DisplayMember = "AStore"
        LokStore.Properties.ValueMember = "trkAStore"
        LokStore.Properties.PopulateColumns()
        LokStore.Properties.Columns(0).Visible = False
        LokStore.Properties.Columns(2).Visible = False
        LokStore.Properties.Columns(3).Visible = False
    End Sub

    Private Sub FillArrivePrdStore()
        Dim lst = (From c In db.arivalPrdStores Where c.delAPrd = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
        LokStore.Properties.DataSource = lst
        LokStore.Properties.DisplayMember = "APrdStr"
        LokStore.Properties.ValueMember = "trkAPrdStr"
        LokStore.Properties.PopulateColumns()
        LokStore.Properties.Columns(0).Visible = False
        LokStore.Properties.Columns(2).Visible = False
        LokStore.Properties.Columns(3).Visible = False
    End Sub
#End Region

    Private Sub ClearForm()

        LokLoc.Properties.Columns.Clear()
        LokLoc.Properties.NullText = ""
        LokStore.Properties.Columns.Clear()
        LokStore.Properties.NullText = ""
        LokProcess.Properties.Columns.Clear()
        LokProcess.Properties.NullText = ""
        '**************************************************
        LokLoc.Text = ""
        LokStore.Text = ""
        LokProcess.Text = ""
    End Sub

    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        If done = True And LokLoc.Text <> "" Then
            If LokStrType.Text = "مخازن مناطق الشراء" Then
                FillBuyStore()
            ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
                FillArriveCrpStore()
            ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
                FillArrivePrdStore()
            End If
        End If
    End Sub

    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            ID = GVBuyView.GetFocusedRowCellValue("trkBuyClient")
            TheStrType = 1
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            ID = GVBuyView.GetFocusedRowCellValue("trkASReq")
            TheStrType = 2
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            ID = GVBuyView.GetFocusedRowCellValue("trkRecv")
            TheStrType = 3
        End If
        Dim MyFrmStoreMgmt As New FrmStoreMgmt
        MyFrmStoreMgmt.Show()
    End Sub
    Public Function CanDelete() As Boolean
        CanDelete = False
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            Dim TrkBuy = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkBuyClient"))
            Dim lstBuy = (From s In db.buyDetails Where s.trkBuyClient = Val(TrkBuy) And s.delBDet = 0 Select s).ToList()
            If lstBuy.Count > 0 Then
                Exit Function
            End If
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            Dim TrkStr = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkASReq"))
            Dim lstBuy = (From s In db.aStoreDets Where s.trkASReq = Val(TrkStr) And s.delASDet = 0 Select s).ToList()
            If lstBuy.Count > 0 Then
                Exit Function
            End If
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            Dim TrkRecv As Integer = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkRecv"))
            Dim lstRecv = (From s In db.receiveDets Where s.trkRecv = Val(TrkRecv) And s.delRecDet = 0 Select s).ToList()
            If lstRecv.Count > 0 Then
                Exit Function
            End If
        End If
        CanDelete = True
    End Function
    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            If CanDelete() = True Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If Msg = MsgBoxResult.Yes Then
                    Dim tb As New buy
                    tb = (From s In db.buys Where s.trkBuyClient = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkBuyClient")) Select s).Single()
                    tb.delBuy = True
                    db.SubmitChanges()
                    FillGrid()
                End If
            Else
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If

        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            If CanDelete() = True Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If Msg = MsgBoxResult.Yes Then
                    Dim tb As New aStoreReq
                    tb = (From s In db.aStoreReqs Where s.trkASReq = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkASReq")) Select s).Single()
                    tb.delAS = True
                    db.SubmitChanges()
                    FillGrid()
                End If
            Else
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

            End If
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            If CanDelete() = True Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If Msg = MsgBoxResult.Yes Then
                    Dim tb As New receive
                    tb = (From s In db.receives Where s.trkRecv = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkRecv")) Select s).Single()
                    tb.delRecv = True
                    db.SubmitChanges()
                    FillGrid()
                End If
            Else
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If
        End If
    End Sub
End Class