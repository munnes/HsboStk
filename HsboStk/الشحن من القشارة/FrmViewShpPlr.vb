
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmViewShpPlr
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private Sub FrmViewBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        'LokLoc.Properties.ShowFooter = True
        FillExp()
    End Sub
    Private Sub FillLoc()
        Dim lst = (From c In db.arivalLocs Where c.delAr = False Select c).ToList
        LokLoc.Text = ""
        Me.LokLoc.Properties.DataSource = lst
        LokLoc.Properties.DisplayMember = "arivalName"
        LokLoc.Properties.ValueMember = "trkArival"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
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
    'Public Sub ResetAtClose()

    '    '************fill store ,and save req number should be rest, b/c when re-open form 
    '    done = False
    '    Me.Close()
    '    FrmMain.RibbonControl.Enabled = True
    'End Sub
    Private Sub FillGrid()


        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)

        End If

        Select Case True
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '00000
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.trkExpLoc = Val(LokExp.EditValue) _
                                                   And s.expShipDate >= FDate.ToShortDateString _
                                                   And s.expShipDate <= TDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '00001
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                     And s.trkExpLoc = Val(LokExp.EditValue) _
                                                   And s.expShipDate >= FDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList

                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '00010
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                       And s.trkExpLoc = Val(LokExp.EditValue) _
                                                   And s.expShipDate <= TDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '00011
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                    And s.trkExpLoc = Val(LokExp.EditValue) _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '00100
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.expShipDate >= FDate.ToShortDateString _
                                                   And s.expShipDate <= TDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '00101
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                   And s.expShipDate >= FDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '00110
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                           And s.expShipDate <= TDate.ToShortDateString _
                                                            And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '00111
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                            And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '01000
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkExpLoc = Val(LokExp.EditValue) _
                                                           And s.expShipDate >= FDate.ToShortDateString _
                                                           And s.expShipDate <= TDate.ToShortDateString _
                                                       And s.trkAPrdStr = 0 _
                                                            And s.delExpShip = False
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '01001
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkExpLoc = Val(LokExp.EditValue) _
                                                   And s.expShipDate >= FDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '01010
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkExpLoc = Val(LokExp.EditValue) _
                                                   And s.expShipDate <= TDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '01011
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkExpLoc = Val(LokExp.EditValue) _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '01100
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.expShipDate >= FDate.ToShortDateString _
                                                   And s.expShipDate <= TDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '01101
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.expShipDate >= FDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '01110
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.expShipDate <= TDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '01111
                Dim lst = (From s In db.V_expShips Where s.trkArival = Val(LokLoc.EditValue) _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            ''    '********************************
            ''    '*****************missing options due to store location relation ( 10000, 10001, 10010, 10011,10100,10101,10110,10111)
            ''    '*********************************
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '11000
                Dim lst = (From s In db.V_expShips Where s.trkExpLoc = Val(LokExp.EditValue) _
                                                   And s.expShipDate >= FDate.ToShortDateString _
                                                   And s.expShipDate <= TDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '11001
                Dim lst = (From s In db.V_expShips Where s.trkExpLoc = Val(LokExp.EditValue) _
                                                   And s.expShipDate >= FDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '11010
                Dim lst = (From s In db.V_expShips Where s.trkExpLoc = Val(LokExp.EditValue) _
                                                   And s.expShipDate <= TDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '11011
                Dim lst = (From s In db.V_expShips Where s.trkExpLoc = Val(LokExp.EditValue) _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '11100
                Dim lst = (From s In db.V_expShips Where s.expShipDate >= FDate.ToShortDateString _
                                                   And s.expShipDate <= TDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '11101
                Dim lst = (From s In db.V_expShips Where s.expShipDate >= FDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '11110
                Dim lst = (From s In db.V_expShips Where s.expShipDate <= TDate.ToShortDateString _
                                                    And s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '11111
                Dim lst = (From s In db.V_expShips Where s.delExpShip = False And s.trkAPrdStr = 0
                           Select s.trkExpShip, s.arivalName, s.peelerName, s.expName, s.expShipDate).ToList
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

    Private Sub LblHead_Click(sender As Object, e As EventArgs) Handles LblHead.Click

    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
            Dim rpt As New RepViewShpPeeler
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

                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & " الى منطقة: " & LokExp.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                    & " and [trkExpLoc]= " & Val(LokExp.EditValue) & " and [trkAPrdStr] = 0 " _
                        & " and [expShipDate] >= #" & FDate.ToShortDateString & "# and [expShipDate] <= #" & TDate.ToShortDateString & "#"
                '00001
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & " الى منطقة: " & LokExp.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                    & " and [trkExpLoc]= " & Val(LokExp.EditValue) & " and [trkAPrdStr] = 0 " _
                        & " and [expShipDate] >= #" & FDate.ToShortDateString & "# "
                '00010
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & " الى منطقة: " & LokExp.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                    & " and [trkExpLoc]= " & Val(LokExp.EditValue) & " and [trkAPrdStr] = 0 " _
                        & "and [expShipDate] <= #" & TDate.ToShortDateString & "#"
                '00011
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & " الى منطقة: " & LokExp.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                    & " and [trkExpLoc]= " & Val(LokExp.EditValue) & " and [trkAPrdStr] = 0 "

                '00100
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " - القشارة\الغربال: " _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) & " and [trkAPrdStr] = 0 " _
                        & " and [expShipDate] >= #" & FDate.ToShortDateString & "# and [expShipDate] <= #" & TDate.ToShortDateString & "#"
                '00101
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & " and [expShipDate] >= #" & FDate.ToShortDateString & "#" & " and [trkAPrdStr] = 0 "
                '00110
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text _
                        & "؛ " & "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                        & "and [expShipDate] <= #" & TDate.ToShortDateString & "#" & " and [trkAPrdStr] = 0 "
                '00111
                Case Me.LokLoc.Text <> "" And LokPeeler.Text <> "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkPeeler]= " & Val(LokPeeler.EditValue) _
                    & " and [trkAPrdStr] = 0 "

                '01000
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & " الى منطقة: " & LokExp.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkAPrdStr] = 0 " _
                    & " And [trkExpLoc] = " & Val(LokExp.EditValue) _
                        & " And [expShipDate] >= #" & FDate.ToShortDateString & "# and [expShipDate] <= #" & TDate.ToShortDateString & "#"

                '    01001
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " الى منطقة: " & LokExp.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkAPrdStr] = 0 " _
                    & " and [trkExpLoc]= " & Val(LokExp.EditValue) _
                        & " and [expShipDate] >= #" & FDate.ToShortDateString & "#"
                '01010
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokPeeler.Text & " الى منطقة: " & LokExp.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkAPrdStr] = 0 " _
                    & " and [trkExpLoc]= " & Val(LokExp.EditValue) _
                        & "and [expShipDate] <= #" & TDate.ToShortDateString & "#"
                '01011
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " الى منطقة: " & LokExp.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkAPrdStr] = 0 " _
                    & " and [trkExpLoc]= " & Val(LokExp.EditValue)
                '01100
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkAPrdStr] = 0 " _
                        & " and [expShipDate] >= #" & FDate.ToShortDateString & "# and [expShipDate] <= #" & TDate.ToShortDateString & "#"
                '01101
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkAPrdStr] = 0 " _
                        & " and [expShipDate] >= #" & FDate.ToShortDateString & "# "
                '01110
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkAPrdStr] = 0 " _
                        & " and [expShipDate] <= #" & TDate.ToShortDateString & "#"
                '01111
                Case Me.LokLoc.Text <> "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text
                    rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkAPrdStr] = 0 "
                '11000
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " الى منطقة: " & LokExp.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = " [trkExpLoc]= " & Val(LokExp.EditValue) & " and [trkAPrdStr] = 0 " _
                        & " and [expShipDate] >= #" & FDate.ToShortDateString & "# and [expShipDate] <= #" & TDate.ToShortDateString & "#"
                '11001
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " الى منطقة: " & LokExp.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = " [trkExpLoc] = " & Val(LokExp.EditValue) & " and [trkAPrdStr] = 0 " _
                        & " And [expShipDate] >= #" & FDate.ToShortDateString & "#"
                '11010
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " الى منطقة: " & LokExp.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [trkExpLoc]= " & Val(LokExp.EditValue) & " and [trkAPrdStr] = 0 " _
                        & " and [expShipDate] <= #" & TDate.ToShortDateString & "#"
                '11011
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " الى منطقة: " & LokExp.Text
                    rpt.FilterString = "[trkExpLoc]= " & Val(LokExp.EditValue) & " and [trkAPrdStr] = 0 "

                '11100
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = " [expShipDate] >= #" & FDate.ToShortDateString & "# and [expShipDate] <= #" & TDate.ToShortDateString & "#" _
                & " and [trkAPrdStr] = 0 "
                '11101
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = " [expShipDate] >= #" & FDate.ToShortDateString & "#  and [trkAPrdStr] = 0 "

                '11110
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ:" & TDate.ToShortDateString
                    rpt.FilterString = " [expShipDate] <= #" & TDate.ToShortDateString & "# and [trkAPrdStr] = 0 "
                '11111
                Case Me.LokLoc.Text = "" And LokPeeler.Text = "" And LokExp.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = " [trkAPrdStr] = 0 "

            End Select

            rpt.TheFilter.Visible = False
            '******************
            rpt.RequestParameters = False
            rpt.ShowPreview()
        End If
    End Sub

    Private Sub btnAr_Click(sender As Object, e As EventArgs) Handles btnAr.Click
        LokExp.Text = ""
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
        col3.Caption = "منطقة الصادر"
        col4.Caption = "تاريخ الشحن"
        col5.Caption = "عرض التفاصيل"
        col6.Caption = "حذف"
        GVShpView.Columns(5).Width = 40
        GVShpView.Columns(5).Visible = True
        GVShpView.Columns(6).Width = 10
        GVShpView.Columns(6).Visible = True
    End Sub


    Public Function CanDelete() As Boolean
        CanDelete = False
        Dim TrkShip = Val(GVShpView.GetRowCellValue(GVShpView.GetSelectedRows(0), "trkExpShip"))
        Dim lstShip = (From s In db.expShipDets Where s.trkExpShip = Val(TrkShip) And s.delExShDet = 0 Select s).ToList()
        If lstShip.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function

    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        ID = GVShpView.GetFocusedRowCellValue("trkExpShip")

        Dim lst = (From s In db.expShipDets Where s.trkExpShip = ID And s.delExShDet = False Select s).ToList
        CountView = lst.Count
        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        Dim MyFrmShpPlr As New FrmShpPlr
        'MyFrmShipProd.MdiParent = FrmMain
        MyFrmShpPlr.Show()
    End Sub

    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
        If CanDelete() = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If Msg = MsgBoxResult.Yes Then
                Dim tb As New expShip
                tb = (From s In db.expShips Where s.trkExpShip = Val(GVShpView.GetRowCellValue(GVShpView.GetSelectedRows(0), "trkExpShip")) Select s).Single()
                tb.delExpShip = True
                db.SubmitChanges()
                FillGrid()
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub
End Class