Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmViewArrive
    Private col1, col2, col3, col4, col5 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private Sub FrmViewBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        FillRes()
        '  LokLoc.Properties.ShowFooter = True
    End Sub

    Private Sub FillLoc()
        Dim lst = (From c In db.buyerLocations Where c.delL = False Select c).ToList
        LokLoc.Text = ""
        Me.LokLoc.Properties.DataSource = lst
        LokLoc.Properties.DisplayMember = "buyLoc"
        LokLoc.Properties.ValueMember = "trkBuyLoc"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
    End Sub
    Private Sub FillRes()
        Dim lst = (From c In db.arivalLocs Where c.delAr = False Select c).ToList
        LokRes.Text = ""
        Me.LokRes.Properties.DataSource = lst
        LokRes.Properties.DisplayMember = "arivalName"
        LokRes.Properties.ValueMember = "trkArival"
        LokRes.Properties.PopulateColumns()
        LokRes.Properties.Columns(0).Visible = False
        LokRes.Properties.Columns(2).Visible = False
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

        Dim FDate, TDate As DateTime

        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)
        End If

        Select Case True
            Case Me.LokLoc.Text <> "" And LokRes.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0000
                Dim lst = (From s In db.V_Arrives Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkArival = Val(LokRes.EditValue) _
                                                   And s.arDate >= FDate.ToShortDateString _
                                                   And s.arDate <= TDate.ToShortDateString _
                                                   And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList

                GridControl1.DataSource = lst

            '0001
            Case Me.LokLoc.Text <> "" And LokRes.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Arrives Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkArival = Val(LokRes.EditValue) _
                                                   And s.arDate >= FDate.ToShortDateString _
                                                   And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst
            '0010
            Case Me.LokLoc.Text <> "" And LokRes.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Arrives Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkArival = Val(LokRes.EditValue) _
                                                   And s.arDate <= TDate.ToShortDateString _
                                                   And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst

            '    '0011
            Case Me.LokLoc.Text <> "" And LokRes.Text <> "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Arrives Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkArival = Val(LokRes.EditValue) _
                                                    And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst

            '    '0100
            Case Me.LokLoc.Text <> "" And LokRes.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Arrives Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.arDate >= FDate.ToShortDateString _
                                                   And s.arDate <= TDate.ToShortDateString _
                                                    And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst

            '    '0101
            Case Me.LokLoc.Text <> "" And LokRes.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Arrives Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.arDate >= FDate.ToShortDateString _
                                                    And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst

            '    '0110
            Case Me.LokLoc.Text <> "" And LokRes.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                Dim lst = (From s In db.V_Arrives Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.arDate <= TDate.ToShortDateString _
                                                            And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst

            '    '0111
            Case Me.LokLoc.Text <> "" And LokRes.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                Dim lst = (From s In db.V_Arrives Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst

            '    '*********************************
            Case Me.LokLoc.Text = "" And LokRes.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '1000
                Dim lst = (From s In db.V_Arrives Where s.trkArival = Val(LokRes.EditValue) _
                                                   And s.arDate >= FDate.ToShortDateString _
                                                   And s.arDate <= TDate.ToShortDateString _
                                                   And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList

                GridControl1.DataSource = lst

            Case Me.LokLoc.Text = "" And LokRes.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '1001
                Dim lst = (From s In db.V_Arrives Where s.trkArival = Val(LokRes.EditValue) _
                                                   And s.arDate >= FDate.ToShortDateString _
                                                   And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList

                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokRes.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '1010
                Dim lst = (From s In db.V_Arrives Where s.trkArival = Val(LokRes.EditValue) _
                                                   And s.arDate <= TDate.ToShortDateString _
                                                   And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList

                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokRes.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '1011
                Dim lst = (From s In db.V_Arrives Where s.trkArival = Val(LokRes.EditValue) _
                                                   And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text = "" And LokRes.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '    '1100
                Dim lst = (From s In db.V_Arrives Where s.arDate >= FDate.ToShortDateString _
                                                   And s.arDate <= TDate.ToShortDateString _
                                                            And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst

            '    '1101
            Case Me.LokLoc.Text = "" And LokRes.Text = "" And FromDate.Text <> "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Arrives Where s.arDate >= FDate.ToShortDateString _
                                                            And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst
            '1110
            Case Me.LokLoc.Text = "" And LokRes.Text = "" And FromDate.Text = "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_Arrives Where s.arDate <= TDate.ToShortDateString _
                                                            And s.delAr = False
                           Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
                GridControl1.DataSource = lst
            '1111
            Case Me.LokLoc.Text = "" And LokRes.Text = "" And FromDate.Text = "" And ToDate.Text = ""

                Dim lst = (From s In db.V_Arrives Where s.delAr = False Select s.trkAr, s.arivalName, s.buyLoc, s.arDate).ToList
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
        LblHead.Focus()
    End Sub

    Private Sub btnRes_Click(sender As Object, e As EventArgs) Handles btnRes.Click
        LokRes.Text = ""
        LblHead.Focus()
    End Sub

    Private Function CanSearch() As Boolean
        CanSearch = False
        If FromDate.Text <> "" And ToDate.Text <> "" Then

            If CType(FromDate.Text, DateTime) > CType(ToDate.Text, DateTime) Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً تاريخ البداية لا يمكن أن يكون أكبر من تاريخ النهاية! ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
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

            Dim rpt As New RepViewArrive
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
                Case Me.LokLoc.Text <> "" And LokRes.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة الوصول:  " & LokRes.Text & "/ منطقة الشحن: " & LokLoc.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkArival]= " & Val(LokRes.EditValue) _
                        & " and [arDate] >= #" & FDate.ToShortDateString & "# and [arDate] <= #" & TDate.ToShortDateString & "#"
                '0001
                Case Me.LokLoc.Text <> "" And LokRes.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة الوصول:  " & LokRes.Text & "/ منطقة الشحن: " & LokLoc.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkArival]= " & Val(LokRes.EditValue) _
                        & " and [arDate] >= #" & FDate.ToShortDateString & "#"
                '0010
                Case Me.LokLoc.Text <> "" And LokRes.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة الوصول:  " & LokRes.Text & "/ منطقة الشحن: " & LokLoc.Text _
                        & "؛ " & " الى تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkArival]= " & Val(LokRes.EditValue) _
                        & " and [arDate] <= #" & TDate.ToShortDateString & "#"
                '0011
                Case Me.LokLoc.Text <> "" And LokRes.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة الوصول:  " & LokRes.Text & "/ منطقة الشحن: " & LokLoc.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkArival]= " & Val(LokRes.EditValue)
                '0100
                Case Me.LokLoc.Text <> "" And LokRes.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " منطقة الشحن: " & LokLoc.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [arDate] >= #" & FDate.ToShortDateString & "# and [arDate] <= #" & TDate.ToShortDateString & "#"
                '0101
                Case Me.LokLoc.Text <> "" And LokRes.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " منطقة الشحن: " & LokLoc.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [arDate] >= #" & FDate.ToShortDateString & "# "
                '0110
                Case Me.LokLoc.Text <> "" And LokRes.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " منطقة الشحن: " & LokLoc.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [arDate] <= #" & TDate.ToShortDateString & "#"
                '0111
                Case Me.LokLoc.Text <> "" And LokRes.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة الشحن: " & LokLoc.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue)
                '1000
                Case Me.LokLoc.Text = "" And LokRes.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة الوصول:  " & LokRes.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = " [trkArival]= " & Val(LokRes.EditValue) _
                        & " and [arDate] >= #" & FDate.ToShortDateString & "# and [arDate] <= #" & TDate.ToShortDateString & "#"
                '1001
                Case Me.LokLoc.Text = "" And LokRes.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة الوصول:  " & LokRes.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = " [trkArival]= " & Val(LokRes.EditValue) _
                        & " and [arDate] >= #" & FDate.ToShortDateString & "# "
                '1010
                Case Me.LokLoc.Text = "" And LokRes.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "محطة الوصول:  " & LokRes.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [trkArival]= " & Val(LokRes.EditValue) _
                        & " and [arDate] <= #" & TDate.ToShortDateString & "#"
                '1011
                Case Me.LokLoc.Text = "" And LokRes.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "محطة الوصول:  " & LokRes.Text
                    rpt.FilterString = " [trkArival]= " & Val(LokRes.EditValue)

                '1100
                Case Me.LokLoc.Text = "" And LokRes.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[arDate] >= #" & FDate.ToShortDateString & "# And [arDate] <= #" & TDate.ToShortDateString & "#"
                '1101
                Case Me.LokLoc.Text = "" And LokRes.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = " [arDate] >= #" & FDate.ToShortDateString & "#"
                '1110
                Case Me.LokLoc.Text = "" And LokRes.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [arDate] <= #" & TDate.ToShortDateString & "#"
                Case Me.LokLoc.Text = "" And LokRes.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = " "
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
        col1.Caption = "وصول في محطة"
        col2.Caption = "مشحون من منطقة"
        col3.Caption = "تاريخ الوصول"
        col4.Caption = "عرض التفاصيل"
        col5.Caption = "حذف"
        GVBuyView.Columns(4).Width = 30
        GVBuyView.Columns(4).Visible = True
        GVBuyView.Columns(5).Width = 10
        GVBuyView.Columns(5).Visible = True
    End Sub

    Public Function CanDelete() As Boolean
        CanDelete = False
        Dim TrkAr = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkAr"))
        Dim lstBuy = (From s In db.arriveDetails Where s.trkAr = Val(TrkAr) And s.delAD = 0 Select s).ToList()
        If lstBuy.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function

    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        ID = GVBuyView.GetFocusedRowCellValue("trkAr")
        Dim tb As New buyDet
        Dim lst = (From s In db.arriveDetails Where s.trkAr = ID And s.delAD = 0 Select s).ToList
        CountView = lst.Count
        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        Dim MyFrmAddArrive As New FrmAddArrive
        'MyFrmAddBuy.MdiParent = Me
        MyFrmAddArrive.Show()
    End Sub

    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
        If CanDelete() = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If Msg = MsgBoxResult.Yes Then
                Dim tb As New arrive
                tb = (From s In db.arrives Where s.trkAr = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkAr")) Select s).Single()
                tb.delAr = True
                db.SubmitChanges()
                FillGrid()
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub
End Class