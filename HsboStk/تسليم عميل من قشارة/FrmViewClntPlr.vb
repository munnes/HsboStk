
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmViewClntPlr
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Private FDate, TDate As DateTime

    Private Sub FrmViewClntPlr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        LokLoc.Properties.ShowFooter = True

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

    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokPeeler.Text = ""
        LokClient.Text = ""
        FillPeeler()
        FillClient()

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
        TxtJobOrd.Text = ""
        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)

        End If

        Select Case True
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokClient.Text = ""
                '00001
                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.ClntRecvDate >= FDate.ToShortDateString _
                                                   And s.ClntRecvDate <= TDate.ToShortDateString And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList

                GridControl1.DataSource = lst

            '00011
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.ClntRecvDate >= FDate.ToShortDateString And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst
            '00101
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokClient.Text = ""

                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.ClntRecvDate <= TDate.ToShortDateString And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst

            '  00111
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst

            '    01001
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokClient.Text = ""

                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.ClntRecvDate >= FDate.ToShortDateString _
                                                   And s.ClntRecvDate <= TDate.ToShortDateString And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst

            '    '01011
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.ClntRecvDate >= FDate.ToShortDateString And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst

            '    '01101
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokClient.Text = ""
                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.ClntRecvDate <= TDate.ToShortDateString And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst

            '    '01111
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text = ""
                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst


            '    '********************************
            '    '*****************missing options due to store location relation ( 1000, 1001, 1010, 1011)
            '    '*********************************
            '    '11001
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokClient.Text = ""

                Dim lst = (From s In db.V_RecvClnts Where s.ClntRecvDate >= FDate.ToShortDateString _
                                                   And s.ClntRecvDate <= TDate.ToShortDateString And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst

            '    '11011
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_RecvClnts Where s.ClntRecvDate >= FDate.ToShortDateString And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst
            '11101
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokClient.Text = ""

                Dim lst = (From s In db.V_RecvClnts Where s.ClntRecvDate <= TDate.ToShortDateString And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst
            '11111
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_RecvClnts Where s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst


            '*****************Client
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokClient.Text <> ""
                '00000
                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                           And s.ClntRecvDate >= FDate.ToShortDateString _
                                                           And s.ClntRecvDate <= TDate.ToShortDateString _
                                                       And s.trkClntCrp = Val(LokClient.EditValue) And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokClient.Text <> ""
                '00010
                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.ClntRecvDate >= FDate.ToShortDateString _
                                                     And s.trkClntCrp = Val(LokClient.EditValue) And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst
            '00100
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokClient.Text <> ""

                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.ClntRecvDate <= TDate.ToShortDateString _
                                                    And s.trkClntCrp = Val(LokClient.EditValue) And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst
            '    '00110
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text <> ""

                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                           And s.trkClntCrp = Val(LokClient.EditValue) And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst
            '    '01000
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokClient.Text <> ""

                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.ClntRecvDate >= FDate.ToShortDateString _
                                                   And s.ClntRecvDate <= TDate.ToShortDateString _
                                                          And s.trkClntCrp = Val(LokClient.EditValue) And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst

            '    '01010
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.ClntRecvDate >= FDate.ToShortDateString _
                                                          And s.trkClntCrp = Val(LokClient.EditValue) And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst
            '    '01100
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokClient.Text <> ""
                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.ClntRecvDate <= TDate.ToShortDateString _
                                                          And s.trkClntCrp = Val(LokClient.EditValue) And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst
            '    '01110
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text <> ""
                Dim lst = (From s In db.V_RecvClnts Where s.trkArival = Val(LokLoc.EditValue) _
                                                          And s.trkClntCrp = Val(LokClient.EditValue) And s.trkAPrdStr = 0
                           Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
                GridControl1.DataSource = lst


        End Select

        ViewBtns()

    End Sub
    Private Sub ViewBtns()
        ' ********************Add repository button for details
        GVBuyView.Columns(0).Visible = False
        GVBuyView.Columns.Add()
        GVBuyView.Columns(5).ColumnEdit = repBtnView
        GVBuyView.Columns.Add()
        GVBuyView.Columns(6).ColumnEdit = repBtnDel
    End Sub
    'Public Function CalFlag() As Boolean
    '    If RdoLocal.Checked = True Then
    '        CalFlag = True
    '    ElseIf RdoClient.Checked = True
    '        CalFlag = False
    '    End If
    'End Function
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
        End If
    End Sub

    Private Sub btnLok_Click(sender As Object, e As EventArgs) Handles btnLok.Click
        LokLoc.Text = ""
        LokPeeler.Text = ""
        LokClient.Text = ""
        LblHead.Focus()
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

    Private Sub btnClnt_Click(sender As Object, e As EventArgs) Handles btnClnt.Click
        LokClient.Text = ""
    End Sub

    Private Sub BtnFind_Click(sender As Object, e As EventArgs) Handles BtnFind.Click
        ClearForm()
        LokLoc.Reset()
        LokPeeler.Reset()
        LokClient.Reset()
        Dim lst2 = (From s In db.V_RecvClnts Where s.trkToPrd = Val(TxtJobOrd.Text) And s.trkAPrdStr = 0
                    Select s).ToList
        If lst2.Count <> 0 Then
            LokPeeler.Reset()
            LokLoc.Reset()

            LokClient.Reset()
            LokLoc.EditValue = lst2.Item(0).trkArival

            LokPeeler.EditValue = lst2.Item(0).trkPeeler
            LokClient.EditValue = lst2.Item(0).trkClntCrp
            LokPeeler.Refresh()
            LokLoc.Refresh()

            LokClient.Refresh()
        Else
            clearFrm()
        End If
        Dim lst = (From s In db.V_RecvClnts Where s.trkToPrd = Val(TxtJobOrd.Text) And s.trkAPrdStr = 0
                   Select s.trkClntRecv, s.arivalName, s.peelerName, s.clntCrpName, s.ClntRecvDate).ToList
        GridControl1.DataSource = lst

        ViewBtns()
        FormatColumns()
    End Sub

    Private Sub btnPeeler_Click(sender As Object, e As EventArgs) Handles btnPeeler.Click
        LokPeeler.Text = ""
        LblHead.Focus
    End Sub
    Private Sub clearFrm()
        LokLoc.Text = ""

        LokPeeler.Text = ""
        LokClient.Text = ""
        GridControl1.DataSource = ""
    End Sub
    Private Sub ClearForm()
        LokLoc.Text = ""
        LokClient.Text = ""
        LokPeeler.Text = ""
        FromDate.Text = ""
        ToDate.Text = ""
    End Sub
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click

        '***********************

        Dim rpt As New RepViewClntRcv

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
        If Val(TxtJobOrd.Text) <> 0 Then
            rpt.TheFilter.Value = " امر انتاج: " & Val(TxtJobOrd.Text)
            rpt.FilterString = "[trkToPrd]= " & Val(TxtJobOrd.Text) & " and[trkAPrdStr]=0"
        Else
            If CanSearch() = True Then
                FillGrid()
                FormatColumns()
            End If
            Select Case True
                '00001
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokClient.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokPeeler.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [ClntRecvDate] >= #" & FDate.ToShortDateString & "# and [ClntRecvDate] <= #" & TDate.ToShortDateString & "# And [trkAPrdStr]= 0"
                '00011
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokClient.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokPeeler.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [ClntRecvDate] >= #" & FDate.ToShortDateString & "# And [trkAPrdStr]= 0"
                '00101
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokClient.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokPeeler.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [ClntRecvDate] <= #" & TDate.ToShortDateString & "# And [trkAPrdStr]= 0"
                '00111
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokPeeler.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) & " And [trkAPrdStr]= 0"
                '01001
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokClient.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & " and [ClntRecvDate] >= #" & FDate.ToShortDateString & "# and [ClntRecvDate] <= #" & TDate.ToShortDateString & "# And [trkAPrdStr]= 0"
                '01011
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokClient.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & " and [ClntRecvDate] >= #" & FDate.ToShortDateString & "# And [trkAPrdStr]= 0"
                '01101
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokClient.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [ClntRecvDate] <= #" & TDate.ToShortDateString & "# And [trkAPrdStr]= 0"
                '01111
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text = ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= 0"

                '11001
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokClient.Text = ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[ClntRecvDate] >= #" & FDate.ToShortDateString & "# And [ClntRecvDate] <= #" & TDate.ToShortDateString & "# And [trkAPrdStr]= 0"

                '    '11011
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokClient.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[ClntRecvDate] >= #" & FDate.ToShortDateString & "# And [trkAPrdStr]= 0"
                '11101
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokClient.Text = ""
                    rpt.TheFilter.Value = "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [ClntRecvDate] <= #" & TDate.ToShortDateString & "# And [trkAPrdStr]= 0"

                '11111
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = "[trkAPrdStr]= 0"

                '1100
                ''*********************client
                '00000
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokClient.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "- مخزن: " & LokPeeler.Text & "؛ " & "  - لصالح العميل: " & LokClient.Text & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue) _
                        & " and [ClntRecvDate] >= #" & FDate.ToShortDateString & "# and [ClntRecvDate] <= #" & TDate.ToShortDateString & "# And [trkAPrdStr]= 0"
                '00010
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokClient.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "- مخزن: " & LokPeeler.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [ClntRecvDate] >= #" & FDate.ToShortDateString & "# And [trkClntCrp] = " & Val(LokClient.EditValue) & " And [trkAPrdStr]= 0"
                '00100
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokClient.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "- مخزن: " & LokPeeler.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [ClntRecvDate] <= #" & TDate.ToShortDateString & "# and [trkClntCrp]= " & Val(LokClient.EditValue) & " And [trkAPrdStr]= 0"
                '00110
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "- مخزن: " & LokPeeler.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue) & " And [trkAPrdStr]= 0"
                '01000
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokClient.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue) _
                        & " and [ClntRecvDate] >= #" & FDate.ToShortDateString & "# and [ClntRecvDate] <= #" & TDate.ToShortDateString & "# And [trkAPrdStr]= 0"
                '01010
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokClient.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue) _
                        & " and [ClntRecvDate] >= #" & FDate.ToShortDateString & "# And [trkAPrdStr]= 0"
                '01100
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokClient.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [ClntRecvDate] <= #" & TDate.ToShortDateString & "# And [trkAPrdStr]= 0" _
                    & " and [trkClntCrp]= " & Val(LokClient.EditValue)
                '01110
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "  - لصالح العميل: " & LokClient.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " and [trkClntCrp]= " & Val(LokClient.EditValue) & " And [trkAPrdStr]= 0"
                '**********************************

                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokClient.Text <> ""
                    rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "  - لصالح العميل: " & LokClient.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " and [trkClntCrp]= " & Val(LokClient.EditValue) & " And [trkAPrdStr]= 0"
            End Select
        End If
        rpt.tStr.Value = "القشارة\الغربال:"
        rpt.strName.Value = "[peelerName]"
        rpt.tStr.Visible = False
        rpt.strName.Visible = False
        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False

        rpt.ShowPreview()
    End Sub

    Private Sub FormatColumns()
        col1 = GVBuyView.Columns(1)
        col2 = GVBuyView.Columns(2)
        col3 = GVBuyView.Columns(3)
        col4 = GVBuyView.Columns(4)
        col5 = GVBuyView.Columns(5)
        col6 = GVBuyView.Columns(6)

        '****************
        col1.Caption = "المحطة"
        col2.Caption = "القشارة\الغربال "
        col3.Caption = "العميل "
        col4.Caption = "تاريخ الاستلام"
        col5.Caption = "عرض التفاصيل"
        col6.Caption = "حذف"
        GVBuyView.Columns(4).Width = 100
        GVBuyView.Columns(5).Width = 100
        GVBuyView.Columns(5).Visible = True
        GVBuyView.Columns(6).Width = 40
        GVBuyView.Columns(6).Visible = True
    End Sub

    Public Function CanDelete() As Boolean
        CanDelete = False
        Dim trkClntRecv As Integer = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkClntRecv"))
        Dim lstRecv = (From s In db.recvClntDets Where s.trkClntRecv = Val(trkClntRecv) And s.delRecDet = 0 Select s).ToList()
        If lstRecv.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function

    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        ID = GVBuyView.GetFocusedRowCellValue("trkClntRecv")
        Dim tb As New receiveDet
        Dim lst = (From s In db.recvClntDets Where s.trkClntRecv = ID And s.delRecDet = 0 Select s).ToList
        CountView = lst.Count
        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        Dim MyFrmClntFromPlr As New FrmClntFromPlr
        'MyFrmAddBuy.MdiParent = Me
        MyFrmClntFromPlr.Show()
    End Sub

    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
        If CanDelete() = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If Msg = MsgBoxResult.Yes Then
                Dim tb As New receiveClnt
                tb = (From s In db.receiveClnts Where s.trkClntRecv = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkClntRecv")) Select s).Single()
                tb.delClntRecv = True
                db.SubmitChanges()
                FillGrid()
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub

    Private Sub TxtJobOrd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJobOrd.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub
End Class