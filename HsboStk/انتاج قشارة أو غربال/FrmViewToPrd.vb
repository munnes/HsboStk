
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmViewToPrd
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private Sub FrmViewToPrd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        LokLoc.Properties.ShowFooter = True
        RdoLocal.Checked = True
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
                Dim lst = (From s In db.V_ToPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.toPrdDate >= FDate.ToShortDateString _
                                                   And s.toPrdDate <= TDate.ToShortDateString _
                                                    And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '0001
                Dim lst = (From s In db.V_ToPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                  And s.toPrdDate >= FDate.ToShortDateString _
                                                    And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList

                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '0010
                Dim lst = (From s In db.V_ToPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.toPrdDate <= TDate.ToShortDateString _
                                                    And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '0011
                Dim lst = (From s In db.V_ToPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                    And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '0100
                Dim lst = (From s In db.V_ToPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                                And s.toPrdDate >= FDate.ToShortDateString _
                                                           And s.toPrdDate <= TDate.ToShortDateString _
                                                            And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '0101
                Dim lst = (From s In db.V_ToPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.toPrdDate >= FDate.ToShortDateString _
                                                    And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '0110
                Dim lst = (From s In db.V_ToPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.toPrdDate <= TDate.ToShortDateString _
                                                    And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '0111
                Dim lst = (From s In db.V_ToPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                    And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst

            ''    '********************************
            ''    '*****************missing options due to store location relation ( 1000, 1001, 1010, 1011)
            ''    '*********************************
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '1100
                Dim lst = (From s In db.V_ToPrds Where s.toPrdDate >= FDate.ToShortDateString _
                                                   And s.toPrdDate <= TDate.ToShortDateString _
                                                    And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '1101
                Dim lst = (From s In db.V_ToPrds Where s.toPrdDate >= FDate.ToShortDateString _
                                                    And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '1110
                Dim lst = (From s In db.V_ToPrds Where s.toPrdDate <= TDate.ToShortDateString _
                                                    And s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '1111
                Dim lst = (From s In db.V_ToPrds Where s.delToPrd = False And s.isLocal = CalFlag()
                           Select s.trkToPrd, s.arivalName, s.peelerName, s.toPrdDate).ToList
                GridControl1.DataSource = lst

        End Select

        ' ********************Add repository button for details
        GVShpView.Columns(0).Visible = False
        GVShpView.Columns.Add()
        GVShpView.Columns(4).ColumnEdit = repBtnView
        GVShpView.Columns.Add()
        GVShpView.Columns(5).ColumnEdit = repBtnDel

    End Sub
    Public Function CalFlag() As Boolean
        If RdoLocal.Checked = True Then
            CalFlag = True
        ElseIf RdoClient.Checked = True
            CalFlag = False
        End If
    End Function

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
        End If
    End Sub

    Private Sub btnLok_Click(sender As Object, e As EventArgs) Handles btnLok.Click
        LokLoc.Text = ""
        LokPeeler.Properties.DataSource = ""
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
        Dim isLoc As Boolean
        If RdoLocal.Checked = True Then
            isLoc = True
        ElseIf RdoClient.Checked = True
            isLoc = False
        End If
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()

            Dim rpt As New RepViewToPrd
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
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text _
                    & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                                      & " and [toPrdDate] >= #" & FDate.ToShortDateString & "# and [toPrdDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & isLoc

                '0001
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text &
                      "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                & " and [toPrdDate] >= #" & FDate.ToShortDateString & "# and  [isLocal]= " & isLoc
                '0010
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [toPrdDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & isLoc
                '0011
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) & " and  [isLocal]= " & isLoc

                '0100
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text _
                    & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & " and [toPrdDate] >= #" & FDate.ToShortDateString & "# and [toPrdDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & isLoc
                '0101
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text _
                    & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & " and [toPrdDate] >= #" & FDate.ToShortDateString & "# and  [isLocal]= " & isLoc
                '0110
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text _
                    & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                                      & " and [toPrdDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & isLoc
                '0111
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " and  [isLocal]= " & isLoc
                '0100
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [toPrdDate] >= #" & FDate.ToShortDateString & "# And [toPrdDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & isLoc

                '0101
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & "and[toPrdDate] >= #" & FDate.ToShortDateString & "# and  [isLocal]= " & isLoc
                '0110
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text & "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [toPrdDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & isLoc

                '0111
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "منطقة:  " & LokLoc.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and  [isLocal]= " & isLoc
                '1100
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = " [toPrdDate] >= #" & FDate.ToShortDateString & "# and [toPrdDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & isLoc
                '1101
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = " [toPrdDate] >= #" & FDate.ToShortDateString & "# and  [isLocal]= " & isLoc
                '1110
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = " [toPrdDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & isLoc

                '11111
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = "[isLocal]= " & isLoc
            End Select


            rpt.TheFilter.Visible = False
            '******************
            rpt.RequestParameters = False

            rpt.ShowPreview()
        End If
    End Sub



    Private Sub FormatColumns()
        col1 = GVShpView.Columns(1)
        col2 = GVShpView.Columns(2)
        col3 = GVShpView.Columns(3)
        col4 = GVShpView.Columns(4)
        col5 = GVShpView.Columns(5)
        '****************
        col1.Caption = "المحطة"
        col2.Caption = "القشارة\الغربال "
        col3.Caption = "تاريخ الصرف"
        col4.Caption = "عرض التفاصيل"
        col5.Caption = "حذف"

        GVShpView.Columns(4).Width = 40
        GVShpView.Columns(4).Visible = True
        GVShpView.Columns(5).Width = 10
        GVShpView.Columns(5).Visible = True
    End Sub

    Public Function CanDelete() As Boolean
        CanDelete = False
        Dim TrkToPrd = Val(GVShpView.GetRowCellValue(GVShpView.GetSelectedRows(0), "trkToPrd"))
        Dim lstToPrd = (From s In db.toPrdDets Where s.trkToPrd = Val(TrkToPrd) And s.delToDet = False Select s).ToList()
        If lstToPrd.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function

    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        ID = GVShpView.GetFocusedRowCellValue("trkToPrd")

        Dim lst = (From s In db.toPrdDets Where s.trkToPrd = ID And s.delToDet = False Select s).ToList
        CountView = lst.Count
        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        Dim MyFrmFromPlr As New FrmFromPlr
        '  MyFrmAddShip.MdiParent = FrmMain
        MyFrmFromPlr.Show()
    End Sub

    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
        If CanDelete() = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If Msg = MsgBoxResult.Yes Then
                Dim tb As New toPrd
                tb = (From s In db.toPrds Where s.trkToPrd = Val(GVShpView.GetRowCellValue(GVShpView.GetSelectedRows(0), "trkToPrd")) Select s).Single()
                tb.delToPrd = True
                db.SubmitChanges()
                FillGrid()
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub


End Class