
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmViewGoodShp
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private Sub FrmViewBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillExp()
        '  LokExp.Properties.ShowFooter = True

    End Sub
    Private Sub FillExp()
        done = False
        Dim lst = (From c In db.exportLocs Where c.delExp = False Select c).ToList
        LokExp.Text = ""
        Me.LokExp.Properties.DataSource = lst
        LokExp.Properties.DisplayMember = "expName"
        LokExp.Properties.ValueMember = "trkExpLoc"
        LokExp.Properties.PopulateColumns()
        LokExp.Properties.Columns(0).Visible = False
        LokExp.Properties.Columns(2).Visible = False
        done = True
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
            Case Me.LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '000
                Dim lst = (From s In db.V_goodShps Where s.trkExpLok = Val(LokExp.EditValue) _
                                                   And s.goodDate >= FDate.ToShortDateString _
                                                   And s.goodDate <= TDate.ToShortDateString _
                                                   And s.delGood = False
                           Select s.trkGood, s.expName, s.goodDate, s.ship).ToList

                GridControl1.DataSource = lst

            '001
            Case Me.LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_goodShps Where s.trkExpLok = Val(LokExp.EditValue) _
                                                   And s.goodDate >= FDate.ToShortDateString _
                                                   And s.delGood = False
                           Select s.trkGood, s.expName, s.goodDate, s.ship).ToList
                GridControl1.DataSource = lst
            '010
            Case Me.LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_goodShps Where s.trkExpLok = Val(LokExp.EditValue) _
                                                   And s.goodDate <= TDate.ToShortDateString _
                                                   And s.delGood = False
                           Select s.trkGood, s.expName, s.goodDate, s.ship).ToList
                GridControl1.DataSource = lst

            '   '011
            Case Me.LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_goodShps Where s.trkExpLok = Val(LokExp.EditValue) _
                                                    And s.delGood = False
                           Select s.trkGood, s.expName, s.goodDate, s.ship).ToList
                GridControl1.DataSource = lst

            '100
            Case Me.LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_goodShps Where s.goodDate >= FDate.ToShortDateString _
                                                   And s.goodDate <= TDate.ToShortDateString _
                                                            And s.delGood = False
                           Select s.trkGood, s.expName, s.goodDate, s.ship).ToList
                GridControl1.DataSource = lst

            '    '101
            Case Me.LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_goodShps Where s.goodDate >= FDate.ToShortDateString _
                                                            And s.delGood = False
                           Select s.trkGood, s.expName, s.goodDate, s.ship).ToList
                GridControl1.DataSource = lst
            '110
            Case Me.LokExp.Text = "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_goodShps Where s.goodDate <= TDate.ToShortDateString _
                                                            And s.delGood = False
                           Select s.trkGood, s.expName, s.goodDate, s.ship).ToList
                GridControl1.DataSource = lst
            '111
            Case Me.LokExp.Text = "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_goodShps Where s.delGood = False Select s.trkGood, s.expName, s.goodDate, s.ship).ToList
                GridControl1.DataSource = lst
        End Select

        ' ********************Add repository button for details
        GVGoodView.Columns(0).Visible = False
        GVGoodView.Columns.Add()
        GVGoodView.Columns(4).ColumnEdit = repBtnView
        GVGoodView.Columns.Add()
        GVGoodView.Columns(5).ColumnEdit = repBtnDel

    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
        End If
    End Sub

    Private Sub btnLok_Click(sender As Object, e As EventArgs) Handles btnLok.Click
        LokExp.Text = ""
        LabelControl3.Focus()
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
    Private Sub FormatColumns()
        col1 = GVGoodView.Columns(1)
        col2 = GVGoodView.Columns(2)
        col3 = GVGoodView.Columns(3)
        col4 = GVGoodView.Columns(4)
        col5 = GVGoodView.Columns(5)
        '****************
        col1.Caption = "المنطقة"
        col2.Caption = "التاريخ "
        col3.Caption = "بيانات الباخرة والشحن"
        col4.Caption = "عرض التفاصيل"
        col5.Caption = "حذف"

        GVGoodView.Columns(4).Width = 50
        GVGoodView.Columns(4).Visible = True
        GVGoodView.Columns(5).Width = 10
        GVGoodView.Columns(5).Visible = True
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
        End If
        Dim rpt As New RepViewGood
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
            '000
            Case Me.LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                rpt.TheFilter.Value = "منطقة الصادر:  " & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                rpt.FilterString = "[trkExpLok]= " & Val(Me.LokExp.EditValue) _
                    & " and [goodDate] >= #" & FDate.ToShortDateString & "# and [goodDate] <= #" & TDate.ToShortDateString & "#"
            '001
            Case Me.LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                rpt.TheFilter.Value = "منطقة الصادر:  " & LokExp.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                rpt.FilterString = "[trkExpLok]= " & Val(Me.LokExp.EditValue) &
                    " and [goodDate] >= #" & FDate.ToShortDateString & "#"
            '010
            Case Me.LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                rpt.TheFilter.Value = "منطقة الصادر:  " & LokExp.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                rpt.FilterString = "[trkExpLok]= " & Val(Me.LokExp.EditValue) _
                    & " and [goodDate] <= #" & TDate.ToShortDateString & "#"
            '011
            Case Me.LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                rpt.TheFilter.Value = "منطقة الصادر:  " & LokExp.Text
                rpt.FilterString = "[trkExpLok]= " & Val(Me.LokExp.EditValue)

            '100
            Case Me.LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                rpt.FilterString = "[goodDate] >= #" & FDate.ToShortDateString & "# And [goodDate] <= #" & TDate.ToShortDateString & "#"

            '101
            Case Me.LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                rpt.FilterString = "[goodDate] >= #" & FDate.ToShortDateString & "#"
            '110
            Case Me.LokExp.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                rpt.TheFilter.Value = "الى تاريخ: " & TDate.ToShortDateString
                rpt.FilterString = " [goodDate] <= #" & TDate.ToShortDateString & "#"

            '111
            Case Me.LokExp.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                rpt.TheFilter.Value = ""
                rpt.FilterString = ""
        End Select

        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False

        rpt.ShowPreview()
    End Sub


    Public Function CanDelete() As Boolean
        CanDelete = False
        Dim TrkGood As Integer = Val(GVGoodView.GetRowCellValue(GVGoodView.GetSelectedRows(0), "trkGood"))
        Dim lstGood = (From s In db.goodShpDets Where s.trkGood = Val(TrkGood) And s.delGoodDet = 0 Select s).ToList()
        If lstGood.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function


    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        ID = GVGoodView.GetFocusedRowCellValue("trkGood")

        Dim tb As New buyDet
        Dim lst = (From s In db.goodShpDets Where s.trkGood = ID And s.delGoodDet = 0 Select s).ToList
        CountView = lst.Count
        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        Dim MyFrmGoodShip As New FrmGoodShip
        'MyFrmAddBuy.MdiParent = Me
        MyFrmGoodShip.Show()
    End Sub

    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
        If CanDelete() = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If Msg = MsgBoxResult.Yes Then
                Dim tb As New goodShp
                tb = (From s In db.goodShps Where s.trkGood = Val(GVGoodView.GetRowCellValue(GVGoodView.GetSelectedRows(0), "trkGood")) Select s).Single()
                tb.delGood = True
                db.SubmitChanges()
                FillGrid()
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub


End Class