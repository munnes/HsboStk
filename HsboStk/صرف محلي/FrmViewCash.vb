

Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmViewCash
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private Sub FrmViewCash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()

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
    Public Sub FillPeeler()
        If done = True And LokLoc.Text <> "" Then
            LokPeeler.Properties.DataSource = ""
            Dim lst = (From c In db.peelers Where c.delPe = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokPeeler.Text = ""
            Me.LokPeeler.Properties.DataSource = lst
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
        FillPeeler()
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
        ResetAtClose()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
        ResetAtClose()
    End Sub

    Private Sub FillGrid()


        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)

        End If

        Select Case True
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0000
                Dim lst = (From s In db.V_Sales Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.saleDate >= FDate.ToShortDateString _
                                                   And s.saleDate <= TDate.ToShortDateString
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '0001
                Dim lst = (From s In db.V_Sales Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.saleDate >= FDate.ToShortDateString
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList

                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '0010
                Dim lst = (From s In db.V_Sales Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.saleDate <= TDate.ToShortDateString
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '0011
                Dim lst = (From s In db.V_Sales Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0100
                Dim lst = (From s In db.V_Sales Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.saleDate >= FDate.ToShortDateString _
                                                           And s.saleDate <= TDate.ToShortDateString
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '0101
                Dim lst = (From s In db.V_Sales Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.saleDate >= FDate.ToShortDateString
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '0110
                Dim lst = (From s In db.V_Sales Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.saleDate <= TDate.ToShortDateString
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '0111
                Dim lst = (From s In db.V_Sales Where s.trkArival = Val(LokLoc.EditValue)
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList


            ''    '********************************
            ''    '*****************missing options due to store location relation 
            '( 1000, 1001, 1010, 1011,
            ''    '*********************************

            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '1100
                Dim lst = (From s In db.V_Sales Where s.saleDate >= FDate.ToShortDateString _
                                                   And s.saleDate <= TDate.ToShortDateString
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '1101
                Dim lst = (From s In db.V_Sales Where s.saleDate >= FDate.ToShortDateString
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '1110
                Dim lst = (From s In db.V_Sales Where s.saleDate <= TDate.ToShortDateString
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '1111
                Dim lst = (From s In db.V_Sales
                           Select s.trkSale, s.arivalName, s.peelerName, s.ClntAr, s.saleDate).ToList
                GridControl1.DataSource = lst

        End Select

        ' ********************Add repository button for details
        GVShpView.Columns(0).Visible = False
        GVShpView.Columns.Add()
        GVShpView.Columns(5).ColumnEdit = repBtnView
        GVShpView.Columns.Add()
        GVShpView.Columns(6).ColumnEdit = repBtnDel

    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
        End If
    End Sub

    Private Sub btnLok_Click(sender As Object, e As EventArgs) Handles btnLok.Click
        LokLoc.Text = ""
        LokPeeler.Text = ""

        'LokPeeler.Properties.DataSource = ""
        'LokClient.Properties.DataSource = ""
        LabelControl2.Focus()
    End Sub

    Private Sub btnStore_Click(sender As Object, e As EventArgs) Handles btnStore.Click
        LokPeeler.Text = ""
        LabelControl2.Focus()
    End Sub

    Private Function CanSearch() As Boolean
        CanSearch = False
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

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
            '***********************
            Dim rpt As New RepViewSale
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

                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "- القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [saleDate] >= #" & FDate.ToShortDateString & "# and [saleDate] <= #" & TDate.ToShortDateString & "#"
                '0001
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "- القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [saleDate] >= #" & FDate.ToShortDateString & "# "
                '0010
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "- القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & "and [saleDate] <= #" & TDate.ToShortDateString & "#"
                '0011
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "- القشارة\الغربال: " & LokPeeler.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue)


                '0100
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & " And [saleDate] >= #" & FDate.ToShortDateString & "# and [saleDate] <= #" & TDate.ToShortDateString & "#"

                '    0101
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                '        & " and [saleDate] >= #" & FDate.ToShortDateString & "#"
                ''0110
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & "and [saleDate] <= #" & TDate.ToShortDateString & "#"
                '0111
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue)

                '1100
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = " [saleDate] >= #" & FDate.ToShortDateString & "# and [saleDate] <= #" & TDate.ToShortDateString & "#"

                '1101
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = " [saleDate] >= #" & FDate.ToShortDateString & "# "

                '1110
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ:" & TDate.ToShortDateString
                    rpt.FilterString = " [saleDate] <= #" & TDate.ToShortDateString & "#"
                '1111
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = ""
            End Select

            rpt.TheFilter.Visible = False
            '******************
            rpt.RequestParameters = False
            rpt.ShowPreview()
        End If
    End Sub

    Private Sub btnAr_Click(sender As Object, e As EventArgs)
        LabelControl2.Focus()
    End Sub

    Private Sub FormatColumns()
        col1 = GVShpView.Columns(1)
        col2 = GVShpView.Columns(2)
        col3 = GVShpView.Columns(3)
        col4 = GVShpView.Columns(4)
        col5 = GVShpView.Columns(5)
        col6 = GVShpView.Columns(6)
        '****************
        col1.Caption = "المحطة"
        col2.Caption = "القشارة\الغربال "
        col3.Caption = "- المشتري "
        col4.Caption = "تاريخ الصرف"
        col5.Caption = "عرض التفاصيل"
        col6.Caption = "حذف"
        GVShpView.Columns(5).Width = 40
        GVShpView.Columns(5).Visible = True
        GVShpView.Columns(6).Width = 10
        GVShpView.Columns(6).Visible = True
    End Sub


    Public Function CanDelete() As Boolean
        CanDelete = False
        Dim TrkSale = Val(GVShpView.GetRowCellValue(GVShpView.GetSelectedRows(0), "trkSale"))
        Dim lstSale = (From s In db.saleDets Where s.trkSale = Val(TrkSale) And s.delSaleDet = False Select s).ToList()
        If lstSale.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function

    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        ID = GVShpView.GetFocusedRowCellValue("trkSale")

        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        Dim MyFrmCash As New FrmCash
        'MyFrmShipProd.MdiParent = FrmMain
        MyFrmCash.Show()
    End Sub

    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
        If CanDelete() = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If Msg = MsgBoxResult.Yes Then
                Dim tb As New sale
                tb = (From s In db.sales Where s.trkSale = Val(GVShpView.GetRowCellValue(GVShpView.GetSelectedRows(0), "trkSale")) Select s).Single()
                tb.delSale = True
                db.SubmitChanges()
                FillGrid()
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub
End Class