﻿
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmViewBuy
    Private col1, col2, col3, col4, col5 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private Sub FrmViewBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        'LokLoc.Properties.ShowFooter = True

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

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        ResetAtClose()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        ResetAtClose()
    End Sub
    Public Sub ResetAtClose()
        '************fill store ,and save req number should be rest, b/c when re-open form 
        done = False
        Me.Close()
        FrmMain.RibbonControl.Enabled = True
    End Sub
    Private Sub FillGrid()
        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)
        End If

        Select Case True
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0000
                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                   And s.trkPrs = 1 _
                                                   And s.delBuy = False
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList

                GridControl1.DataSource = lst

            '0001
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                   And s.delBuy = False _
                                                     And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst
            '0010
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                   And s.delBuy = False And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst

            '    '0011
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                    And s.delBuy = False And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst

            '    '0100
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                    And s.delBuy = False And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst

            '    '0101
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.buyDate >= FDate.ToShortDateString _
                                                    And s.delBuy = False And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst

            '    '0110
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst

            '    '0111
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                Dim lst = (From s In db.V_buys Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.delBuy = False And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst


            '    '********************************
            '    '*****************missing options due to store location relation ( 1000, 1001, 1010, 1011)
            '    '*********************************
            '    '1100
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.buyDate >= FDate.ToShortDateString _
                                                   And s.buyDate <= TDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst

            '    '1101
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.buyDate >= FDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst
            '1110
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buys Where s.buyDate <= TDate.ToShortDateString _
                                                            And s.delBuy = False And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst
            '1111
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_buys Where s.delBuy = False And s.trkPrs = 1
                           Select s.trkBuyClient, s.buyLoc, s.bStore, s.buyDate).ToList
                GridControl1.DataSource = lst
        End Select

        ' ********************Add repository button for details
        GVBuyView.Columns(0).Visible = False
        GVBuyView.Columns.Add()
        GVBuyView.Columns(4).ColumnEdit = repBtnView
        GVBuyView.Columns.Add()
        GVBuyView.Columns(5).ColumnEdit = repBtnDel

    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
        End If
    End Sub

    Private Sub btnLok_Click(sender As Object, e As EventArgs) Handles btnLok.Click
        LokLoc.Text = ""
        LokStore.Text = ""
        LblHead.Focus()
    End Sub

    Private Sub btnStore_Click(sender As Object, e As EventArgs) Handles btnStore.Click
        LokStore.Text = ""
        LblHead.Focus()
    End Sub

    Private Function CanSearch() As Boolean
        CanSearch = False
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

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()

            Dim rpt As New RepViewBuy
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
            '**************************
            If FromDate.Text <> "" Then
                FDate = CType(FromDate.Text, DateTime)
            End If
            If ToDate.Text <> "" Then
                TDate = CType(ToDate.Text, DateTime)
            End If
            '**************************

            Select Case True
                '0000
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "# and [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= 1"
                '0001
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "#" & " And [trkPrs]= 1"
                '0010
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= 1"
                '0011
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) & " And [trkPrs]= 1"
                '0100
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "# and [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= 1"
                '0101
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [buyDate] >= #" & FDate.ToShortDateString & "#" & " And [trkPrs]= 1"
                '0110
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " and [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= 1"
                '0111
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkPrs]= 1"

                '1100
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[buyDate] >= #" & FDate.ToShortDateString & "# And [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= 1"

                '    '1101
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[buyDate] >= #" & FDate.ToShortDateString & "#" & " And [trkPrs]= 1"
                '1110
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [buyDate] <= #" & TDate.ToShortDateString & "#" & " And [trkPrs]= 1"

                '1111
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = " [trkPrs]= 1"
            End Select

            rpt.TheFilter.Visible = False
            '******************
            rpt.RequestParameters = False

            rpt.ShowPreview()
        End If
    End Sub

    Private Sub FormatColumns()
        col1 = GVBuyView.Columns(1)
        col2 = GVBuyView.Columns(2)
        col3 = GVBuyView.Columns(3)
        col4 = GVBuyView.Columns(4)
        col5 = GVBuyView.Columns(5)
        '****************
        col1.Caption = "المنطقة"
        col2.Caption = "المخزن "
        col3.Caption = "تاريخ الشراء"
        col4.Caption = "عرض التفاصيل"
        col5.Caption = "حذف"
        GVBuyView.Columns(4).Width = 30
        GVBuyView.Columns(4).Visible = True
        GVBuyView.Columns(5).Width = 10
        GVBuyView.Columns(5).Visible = True
    End Sub


    Public Function CanDelete() As Boolean
        CanDelete = False
        Dim TrkBuy = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkBuyClient"))
        Dim lstBuy = (From s In db.buyDetails Where s.trkBuyClient = Val(TrkBuy) And s.delBDet = 0 Select s).ToList()
        If lstBuy.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function

    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        ID = GVBuyView.GetFocusedRowCellValue("trkBuyClient")
        Dim tb As New buyDet
        Dim lst = (From s In db.buyDetails Where s.trkBuyClient = ID And s.delBDet = 0 Select s).ToList
        CountView = lst.Count
        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        Dim MyFrmAddBuy As New FrmAddBuy
        'MyFrmAddBuy.MdiParent = Me
        MyFrmAddBuy.Show()
    End Sub

    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
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
    End Sub
End Class