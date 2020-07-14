Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmViewShip
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
    Private FDate, TDate As DateTime
    Private Sub FrmViewBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        LokLoc.Properties.ShowFooter = True
        FillArrive()
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
    Private Sub FillArrive()
        Dim lst = (From c In db.arivalLocs Where c.delAr = False Select c).ToList
        LokArrive.Text = ""
        Me.LokArrive.Properties.DataSource = lst
        LokArrive.Properties.DisplayMember = "arivalName"
        LokArrive.Properties.ValueMember = "trkArival"
        LokArrive.Properties.PopulateColumns()
        LokArrive.Properties.Columns(0).Visible = False
        LokArrive.Properties.Columns(2).Visible = False

    End Sub
    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokStore.Text = ""
        FillStore()
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

        Dim FDate, TDate As DateTime
        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)

        End If

        Select Case True
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '00000
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.trkArival = Val(LokArrive.EditValue) _
                                                   And s.shipDate >= FDate.ToShortDateString _
                                                   And s.shipDate <= TDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '00001
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                     And s.trkArival = Val(LokArrive.EditValue) _
                                                   And s.shipDate >= FDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList

                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '00010
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                       And s.trkArival = Val(LokArrive.EditValue) _
                                                   And s.shipDate <= TDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '00011
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                    And s.trkArival = Val(LokArrive.EditValue) _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '00100
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.shipDate >= FDate.ToShortDateString _
                                                   And s.shipDate <= TDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '00101
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkBStore = Val(LokStore.EditValue) _
                                                   And s.shipDate >= FDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '00110
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                           And s.trkBStore = Val(LokStore.EditValue) _
                                                           And s.shipDate <= TDate.ToShortDateString _
                                                            And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '00111
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                           And s.trkBStore = Val(LokStore.EditValue) _
                                                            And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst

            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '01000
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                           And s.trkArival = Val(LokArrive.EditValue) _
                                                           And s.shipDate >= FDate.ToShortDateString _
                                                           And s.shipDate <= TDate.ToShortDateString _
                                                            And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '01001
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkArival = Val(LokArrive.EditValue) _
                                                   And s.shipDate >= FDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '01010
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkArival = Val(LokArrive.EditValue) _
                                                   And s.shipDate <= TDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '01011
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.trkArival = Val(LokArrive.EditValue) _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '01100
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.shipDate >= FDate.ToShortDateString _
                                                   And s.shipDate <= TDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '01101
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.shipDate >= FDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '01110
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                   And s.shipDate <= TDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '01111
                Dim lst = (From s In db.V_Ships Where s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            ''    '********************************
            ''    '*****************missing options due to store location relation ( 10000, 10001, 10010, 10011,10100,10101,10110,10111)
            ''    '*********************************
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '11000
                Dim lst = (From s In db.V_Ships Where s.trkArival = Val(LokArrive.EditValue) _
                                                   And s.shipDate >= FDate.ToShortDateString _
                                                   And s.shipDate <= TDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                '11001
                Dim lst = (From s In db.V_Ships Where s.trkArival = Val(LokArrive.EditValue) _
                                                   And s.shipDate >= FDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                '11010
                Dim lst = (From s In db.V_Ships Where s.trkArival = Val(LokArrive.EditValue) _
                                                   And s.shipDate <= TDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                '11011
                Dim lst = (From s In db.V_Ships Where s.trkArival = Val(LokArrive.EditValue) _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                '11100
                Dim lst = (From s In db.V_Ships Where s.shipDate >= FDate.ToShortDateString _
                                                   And s.shipDate <= TDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                '11101
                Dim lst = (From s In db.V_Ships Where s.shipDate >= FDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                '11110
                Dim lst = (From s In db.V_Ships Where s.shipDate <= TDate.ToShortDateString _
                                                    And s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                '11111
                Dim lst = (From s In db.V_Ships Where s.delShip = False
                           Select s.trkShip, s.buyLoc, s.bStore, s.arivalName, s.shipDate).ToList
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

            Dim rpt As New RepViewShip
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

                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & " الى محطة: " & LokArrive.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                    & " and [trkArival]= " & Val(LokArrive.EditValue) _
                        & " and [shipDate] >= #" & FDate.ToShortDateString & "# and [shipDate] <= #" & TDate.ToShortDateString & "#"
                '00001
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & " الى محطة: " & LokArrive.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                    & " and [trkArival]= " & Val(LokArrive.EditValue) _
                        & " and [shipDate] >= #" & FDate.ToShortDateString & "# "
                '00010
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & " الى محطة: " & LokArrive.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                    & " and [trkArival]= " & Val(LokArrive.EditValue) _
                        & "and [shipDate] <= #" & TDate.ToShortDateString & "#"
                '00011
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & " الى محطة: " & LokArrive.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                    & " and [trkArival]= " & Val(LokArrive.EditValue)

                '00100
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [shipDate] >= #" & FDate.ToShortDateString & "# and [shipDate] <= #" & TDate.ToShortDateString & "#"
                '00101
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & " and [shipDate] >= #" & FDate.ToShortDateString & "#"
                '00110
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text _
                        & "؛ " & "الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue) _
                        & "and [shipDate] <= #" & TDate.ToShortDateString & "#"
                '00111
                Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) & " And [trkBStore]= " & Val(LokStore.EditValue)

                '01000
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & " الى محطة: " & LokArrive.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                    & " And [trkArival] = " & Val(LokArrive.EditValue) _
                        & " And [shipDate] >= #" & FDate.ToShortDateString & "# and [shipDate] <= #" & TDate.ToShortDateString & "#"

                '    01001
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " الى محطة: " & LokArrive.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                    & " and [trkArival]= " & Val(LokArrive.EditValue) _
                        & " and [shipDate] >= #" & FDate.ToShortDateString & "#"
                '01010
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokStore.Text & " الى محطة: " & LokArrive.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                    & " and [trkArival]= " & Val(LokArrive.EditValue) _
                        & "and [shipDate] <= #" & TDate.ToShortDateString & "#"
                '01011
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text & " الى محطة: " & LokArrive.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                    & " and [trkArival]= " & Val(LokArrive.EditValue)
                '01100
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [shipDate] >= #" & FDate.ToShortDateString & "# and [shipDate] <= #" & TDate.ToShortDateString & "#"
                '01101
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [shipDate] >= #" & FDate.ToShortDateString & "# "
                '01110
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue) _
                        & " and [shipDate] <= #" & TDate.ToShortDateString & "#"
                '01111
                Case Me.LokLoc.Text <> "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = "مشحون من:  " & LokLoc.Text
                    rpt.FilterString = "[trkBuyLoc]= " & Val(Me.LokLoc.EditValue)
                '11000
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " الى محطة: " & LokArrive.Text _
                        & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = " [trkArival]= " & Val(LokArrive.EditValue) _
                        & " and [shipDate] >= #" & FDate.ToShortDateString & "# and [shipDate] <= #" & TDate.ToShortDateString & "#"
                '11001
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " الى محطة: " & LokArrive.Text _
                        & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = " [trkArival] = " & Val(LokArrive.EditValue) _
                        & " And [shipDate] >= #" & FDate.ToShortDateString & "#"
                '11010
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " الى محطة: " & LokArrive.Text _
                        & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                    rpt.FilterString = " [trkArival]= " & Val(LokArrive.EditValue) _
                        & " and [shipDate] <= #" & TDate.ToShortDateString & "#"
                '11011
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " الى محطة: " & LokArrive.Text
                    rpt.FilterString = "[trkArival]= " & Val(LokArrive.EditValue)

                '11100
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                    rpt.FilterString = " [shipDate] >= #" & FDate.ToShortDateString & "# and [shipDate] <= #" & TDate.ToShortDateString & "#"

                '11101
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text <> "" And ToDate.Text = ""
                    rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                    rpt.FilterString = " [shipDate] >= #" & FDate.ToShortDateString & "# "

                '11110
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text <> ""
                    rpt.TheFilter.Value = "الى تاريخ:" & TDate.ToShortDateString
                    rpt.FilterString = " [shipDate] <= #" & TDate.ToShortDateString & "#"
                '11111
                Case Me.LokLoc.Text = "" And LokStore.Text = "" And LokArrive.Text = "" And FromDate.Text = "" And ToDate.Text = ""
                    rpt.TheFilter.Value = ""
                    rpt.FilterString = ""

            End Select

            rpt.TheFilter.Visible = False

            '******************
            rpt.RequestParameters = False

            rpt.ShowPreview()
        End If
    End Sub

    Private Sub btnAr_Click(sender As Object, e As EventArgs) Handles btnAr.Click
        LokArrive.Text = ""
        LblHead.Focus()
    End Sub

    Private Sub FormatColumns()
        col1 = GVShpView.Columns(1)
        col2 = GVShpView.Columns(2)
        col3 = GVShpView.Columns(3)
        col4 = GVShpView.Columns(4)
        col5 = GVShpView.Columns(5)
        col6 = GVShpView.Columns(6)
        '****************
        col1.Caption = "منطقة الشحن"
        col2.Caption = "المخزن "
        col3.Caption = "منطقة الوصول"
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
        Dim TrkShip = Val(GVShpView.GetRowCellValue(GVShpView.GetSelectedRows(0), "trkShip"))
        Dim lstShip = (From s In db.shipDetails Where s.trkShip = Val(TrkShip) And s.delSD = 0 Select s).ToList()
        If lstShip.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function

    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        ID = GVShpView.GetFocusedRowCellValue("trkShip")

        Dim lst = (From s In db.shipDetails Where s.trkShip = ID And s.delSD = False Select s).ToList
        CountView = lst.Count
        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        Dim MyFrmAddShip As New FrmAddShip
        'MyFrmAddShip.MdiParent = FrmMain
        MyFrmAddShip.Show()
    End Sub

    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
        If CanDelete() = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If Msg = MsgBoxResult.Yes Then
                Dim tb As New shipment
                tb = (From s In db.shipments Where s.trkShip = Val(GVShpView.GetRowCellValue(GVShpView.GetSelectedRows(0), "trkShip")) Select s).Single()
                tb.delShip = True
                db.SubmitChanges()
                FillGrid()
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub
End Class