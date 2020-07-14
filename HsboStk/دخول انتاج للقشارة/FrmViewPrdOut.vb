Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmViewPrdOut
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private Sub FrmViewBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FillStore()
        If done = True And LokLoc.Text <> "" Then
            LokStore.Properties.DataSource = ""
            Dim lst = (From c In db.arivalStores Where c.delSa = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokStore.Properties.DataSource = lst
            LokStore.Properties.DisplayMember = "AStore"
            LokStore.Properties.ValueMember = "trkAStore"
            LokStore.Properties.PopulateColumns()
            LokStore.Properties.Columns(0).Visible = False
            LokStore.Properties.Columns(2).Visible = False
            LokStore.Properties.Columns(3).Visible = False
        End If
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
        LokStore.Text = ""
        LokPeeler.Text = ""
        FillStore()
        FillPeeler()
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
    Public Function CalFlag() As Boolean
        If RdoLocal.Checked = True Then
            CalFlag = True
        ElseIf RdoClient.Checked = True
            CalFlag = False
        End If
    End Function
    Private Sub FillGrid()
        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)

        End If

        Select Case True
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '00000
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.oPrdDate >= FDate.ToShortDateString _
                                                   And s.oPrdDate <= TDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '00001
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                     And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.oPrdDate >= FDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList

                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '00010
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                       And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.oPrdDate <= TDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '00011
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                    And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '00100
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                   And s.oPrdDate >= FDate.ToShortDateString _
                                                   And s.oPrdDate <= TDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '00101
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAStore = Val(LokStore.EditValue) _
                                                   And s.oPrdDate >= FDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '00110
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkAStore = Val(LokStore.EditValue) _
                                                           And s.oPrdDate <= TDate.ToShortDateString _
                                                            And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '00111
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkAStore = Val(LokStore.EditValue) _
                                                            And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '01000
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                           And s.oPrdDate >= FDate.ToShortDateString _
                                                           And s.oPrdDate <= TDate.ToShortDateString _
                                                            And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '01001
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.oPrdDate >= FDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '01010
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.oPrdDate <= TDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '01011
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '01100
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.oPrdDate >= FDate.ToShortDateString _
                                                   And s.oPrdDate <= TDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '01101
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.oPrdDate >= FDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '01110
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.oPrdDate <= TDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '01111
                Dim lst = (From s In db.V_OutPrds Where s.trkArival = Val(LokLoc.EditValue) _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            ''    '********************************
            ''    '*****************missing options due to store location relation ( 10000, 10001, 10010, 10011,10100,10101,10110,10111)
            '************************************(11000,11001, 11010,11011) missing for peeler
            ''    '*********************************
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '11100
                Dim lst = (From s In db.V_OutPrds Where s.oPrdDate >= FDate.ToShortDateString _
                                                   And s.oPrdDate <= TDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '11101
                Dim lst = (From s In db.V_OutPrds Where s.oPrdDate >= FDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '11110
                Dim lst = (From s In db.V_OutPrds Where s.oPrdDate <= TDate.ToShortDateString _
                                                    And s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '11111
                Dim lst = (From s In db.V_OutPrds Where s.delOPrd = False And s.isLocal = CalFlag()
                           Select s.trkOutPrd, s.arivalName, s.AStore, s.peelerName, s.oPrdDate).ToList
                GridControl1.DataSource = lst

        End Select

        ' ********************Add repository button for details
        GVPrdOutView.Columns(0).Visible = False
        GVPrdOutView.Columns.Add()
        GVPrdOutView.Columns(5).ColumnEdit = repBtnView
        GVPrdOutView.Columns.Add()
        GVPrdOutView.Columns(6).ColumnEdit = repBtnDel

    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
        End If
    End Sub

    Private Sub btnLok_Click(sender As Object, e As EventArgs) Handles btnLok.Click
        LokLoc.Text = ""
        LokStore.Properties.DataSource = ""
        LokPeeler.Properties.DataSource = ""
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

            Dim rpt As New RepViewPrdOut
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
                '00000

                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & " -  القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                    & " and [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [oPrdDate] >= #" & FDate.ToShortDateString & "# and [oPrdDate] <= #" & TDate.ToShortDateString & "# and  [isLocal]= " & isLoc
                '00001
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & " -  القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                    & " and [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [oPrdDate] >= #" & FDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '00010
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & " -  القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                    & " and [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & "and [oPrdDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '00011
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & " -  القشارة\الغربال: " & LokPeeler.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                    & " and [trkPeeler]= " & Val(LokPeeler.EditValue) & " and [isLocal]= " & isLoc

                '00100
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "/ مخزن: " _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                        & " and [oPrdDate] >= #" & FDate.ToShortDateString & "# and [oPrdDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '00101
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                        & " and [oPrdDate] >= #" & FDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '00110
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text _
                        & "؛ " & "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) _
                        & "and [oPrdDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '00111
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAStore]= " & Val(LokStore.EditValue) & " and [isLocal]= " & isLoc

                '01000
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & " -  القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " And [trkPeeler] = " & Val(LokPeeler.EditValue) _
                        & " And [oPrdDate] >= #" & FDate.ToShortDateString & "# and [oPrdDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & isLoc

                '    01001
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & " -  القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " and [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [oPrdDate] >= #" & FDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '01010
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokStore.Text & " -  القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " and [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & "and [oPrdDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '01011
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text & " -  القشارة\الغربال: " & LokPeeler.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                    & " and [trkPeeler]= " & Val(LokPeeler.EditValue) & " and [isLocal]= " & isLoc
                '01100
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & " and [oPrdDate] >= #" & FDate.ToShortDateString & "# and [oPrdDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '01101
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & " and [oPrdDate] >= #" & FDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '01110
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "المحطة:  " & LokLoc.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                        & " and [oPrdDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '01111
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [isLocal]= " & isLoc
                ''11000
                'Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '    rpt.TheFilter.Value = " الى محطة: " & LokArrive.Text _
                '        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                '    rpt.FilterString = " [trkArival]= " & Val(LokArrive.EditValue) _
                '        & " and [buyDate] >= #" & FDate.ToShortDateString & "# and [buyDate] <= #" & TDate.ToShortDateString & "#"
                ''11001
                'Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '    rpt.TheFilter.Value = " الى محطة: " & LokArrive.Text _
                '        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                '    rpt.FilterString = " [trkArival] = " & Val(LokArrive.EditValue) _
                '        & " And [buyDate] >= #" & FDate.ToShortDateString & "#"
                ''11010
                'Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '    rpt.TheFilter.Value = " الى محطة: " & LokArrive.Text _
                '        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                '    rpt.FilterString = " [trkArival]= " & Val(LokArrive.EditValue) _
                '        & " and [buyDate] <= #" & TDate.ToShortDateString & "#"
                ''11011
                'Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '    rpt.TheFilter.Value = " الى محطة: " & LokArrive.Text
                '    rpt.FilterString = "[trkArival]= " & Val(LokArrive.EditValue)

                '11100
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = " [oPrdDate] >= #" & FDate.ToShortDateString & "# and [oPrdDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & isLoc

                '11101
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = " [oPrdDate] >= #" & FDate.ToShortDateString & "# and [isLocal]= " & isLoc

                '11110
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ:" & TDate.ToShortDateString
                    rpt.FilterString = " [oPrdDate] <= #" & TDate.ToShortDateString & "# and [isLocal]= " & isLoc
                '11111
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokPeeler.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = " [isLocal]= " & isLoc

            End Select

            rpt.TheFilter.Visible = False

            '******************
            rpt.RequestParameters = False

            rpt.ShowPreview()
        End If
    End Sub

    Private Sub btnPel_Click(sender As Object, e As EventArgs) Handles btnPel.Click
        LokPeeler.Text = ""
        LblHead.Focus()
    End Sub

    Private Sub FormatColumns()
        col1 = GVPrdOutView.Columns(1)
        col2 = GVPrdOutView.Columns(2)
        col3 = GVPrdOutView.Columns(3)
        col4 = GVPrdOutView.Columns(4)
        col5 = GVPrdOutView.Columns(5)
        col6 = GVPrdOutView.Columns(6)
        '****************
        col1.Caption = "المحطة"
        col2.Caption = " المخزن في المحطة"
        col3.Caption = "القشارة\الغربال"
        col4.Caption = "التاريخ"
        col5.Caption = "عرض التفاصيل"
        col6.Caption = "حذف"
        GVPrdOutView.Columns(5).Width = 40
        GVPrdOutView.Columns(5).Visible = True
        GVPrdOutView.Columns(6).Width = 10
        GVPrdOutView.Columns(6).Visible = True
    End Sub

    Public Function CanDelete() As Boolean
        CanDelete = False
        Dim TrkOPrd = Val(GVPrdOutView.GetRowCellValue(GVPrdOutView.GetSelectedRows(0), "trkOutPrd"))
        Dim lstShip = (From s In db.outPrdDets Where s.trkOutPrd = Val(TrkOPrd) And s.delPrd = False Select s).ToList()
        If lstShip.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function

    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        ID = GVPrdOutView.GetFocusedRowCellValue("trkOutPrd")

        Dim lst = (From s In db.outPrdDets Where s.trkOutPrd = ID And s.delPrd = 0 Select s).ToList
        CountView = lst.Count
        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        Dim MyFrmPrdOut As New FrmPrdOut
        'MyFrmPrdOut.MdiParent = FrmMain
        MyFrmPrdOut.Show()
    End Sub

    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
        If CanDelete() = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If Msg = MsgBoxResult.Yes Then
                Dim tb As New outPrd
                tb = (From s In db.outPrds Where s.trkOutPrd = Val(GVPrdOutView.GetRowCellValue(GVPrdOutView.GetSelectedRows(0), "trkOutPrd")) Select s).Single()
                tb.delOPrd = True
                db.SubmitChanges()
                FillGrid()
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub
End Class