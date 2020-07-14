Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Data

Public Class FrmBuyCrp
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Private Total, Quantity, Average As Double
    Private FDate, TDate As DateTime
    Private Sub FrmBuyCrp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        FillCrop()

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

    Private Sub btnCrp_Click(sender As Object, e As EventArgs) Handles btnCrp.Click
        LokCrp.Text = ""
        LokUnit.Text = ""
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click

        '************************
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()

            Dim rpt As New RepBuyCrp
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
            ''**************************
            If FromDate.Text <> "" Then
                FDate = CType(FromDate.Text, DateTime)
            End If
            If ToDate.Text <> "" Then
                TDate = CType(ToDate.Text, DateTime)
            End If
            ''**************************
            Select Case True
                Case LokLoc.Text <> ""
                    rpt.TheFilter.Value = "من منطقة:  " & LokLoc.Text _
                        & " -  في الفترة بين: (" & FDate.ToShortDateString & " و " & TDate.ToShortDateString & ")"
                    rpt.FilterString = "[trkCrop]= " & Val(LokCrp.EditValue) & " and [trkUnit]= " & Val(LokUnit.EditValue) &
                         " and [trkBuyLoc] = " & Val(LokLoc.EditValue) &
                         "And [buyDate] >= #" & FDate.ToShortDateString & "# And [buyDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]=1"

                Case LokLoc.Text = ""
                    rpt.TheFilter.Value = "في الفترة بين: (" & FDate.ToShortDateString & " و " & TDate.ToShortDateString & ")"
                    rpt.FilterString = "[trkCrop]= " & Val(LokCrp.EditValue) & " And [trkUnit]= " & Val(LokUnit.EditValue) &
                                          " And [buyDate] >= #" & FDate.ToShortDateString & "# And [buyDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]=1"

            End Select
            '******************
            rpt.Amt.Value = Quantity
            rpt.TotalPrice.Value = Total
            rpt.Avg.Value = Average
            rpt.crp.Value = LokCrp.Text
            rpt.unt.Value = LokUnit.Text
            '******************
            rpt.crp.Visible = False
            rpt.unt.Visible = False
            rpt.Amt.Visible = False
            rpt.TotalPrice.Visible = False
            rpt.Avg.Visible = False
            rpt.TheFilter.Visible = False
            rpt.RequestParameters = False

            rpt.ShowPreview()
        End If

    End Sub

    Private Sub btnLoc_Click(sender As Object, e As EventArgs) Handles btnLoc.Click
        LokLoc.Text = ""
    End Sub

    Private Sub FillCrop()
        done = False
        Dim lst = (From c In db.crops Where c.delCrop = False Select c).ToList
        LokCrp.Text = ""
        Me.LokCrp.Properties.DataSource = lst
        LokCrp.Properties.DisplayMember = "cropName"
        LokCrp.Properties.ValueMember = "trkCrop"
        LokCrp.Properties.PopulateColumns()
        LokCrp.Properties.Columns(0).Visible = False
        LokCrp.Properties.Columns(2).Visible = False
        done = True
    End Sub

    Private Sub btnUnt_Click(sender As Object, e As EventArgs) Handles btnUnt.Click
        LokUnit.Text = ""
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
        End If

    End Sub

    Private Function CanSearch() As Boolean
        CanSearch = False
        If LokCrp.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokCrp.Focus()
            LokCrp.SelectAll()
            Exit Function
        End If
        If LokUnit.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال وحدة القياس ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokUnit.Focus()
            LokUnit.SelectAll()
            Exit Function
        End If
        If FromDate.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            FromDate.Focus()
            Exit Function
        End If
        If ToDate.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            ToDate.Focus()
            Exit Function
        End If
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
    Private Sub FillGrid()
        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)
        End If

        Select Case True
            Case Me.LokLoc.Text <> "" And LokUnit.Text <> "" And LokCrp.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""

                Dim lst = (From s In db.V_buyDetails
                           Where s.buyDate >= FDate.ToShortDateString _
  And s.buyDate <= TDate.ToShortDateString _
   And s.trkCrop = Val(LokCrp.EditValue) _
     And s.trkUnit = Val(LokUnit.EditValue) _
And s.trkBuyLoc = Val(LokLoc.EditValue) _
                               And s.trkPrs = 1
                           Select s.storeAmount, s.cropPrice, TheTotal = Math.Round((s.storeAmount * s.cropPrice), 2, MidpointRounding.AwayFromZero), s.clientName, s.buyDate).ToList
                GridControl1.DataSource = lst
            '    '*********************************
            Case Me.LokLoc.Text = "" And LokCrp.Text <> "" And FromDate.Text <> "" And ToDate.Text <> ""
                '1000
                Dim lst = (From s In db.V_buyDetails
                           Where s.buyDate >= FDate.ToShortDateString _
  And s.buyDate <= TDate.ToShortDateString _
   And s.trkCrop = Val(LokCrp.EditValue) _
     And s.trkUnit = Val(LokUnit.EditValue) _
                               And s.trkPrs = 1
                           Select s.storeAmount, s.cropPrice, TheTotal = Math.Round((s.storeAmount * s.cropPrice), 2, MidpointRounding.AwayFromZero), s.clientName, s.buyDate).ToList
                GridControl1.DataSource = lst

        End Select

    End Sub
    Private Sub FormatColumns()
        col1 = GVBuyView.Columns(0)
        col2 = GVBuyView.Columns(1)
        col3 = GVBuyView.Columns(2)
        col4 = GVBuyView.Columns(3)
        col5 = GVBuyView.Columns(4)
        'col6 = GVBuyView.Columns(5)
        '****************
        col1.Caption = "الكمية "
        col2.Caption = "سعر وحدة الشراء"
        col3.Caption = "السعر الكلي"
        'col4.Caption = "الوحدة"
        col4.Caption = " اسم العميل"
        col5.Caption = " التاريخ"

        GVBuyView.Columns(0).Width = 150
        GVBuyView.Columns(1).Width = 150
        GVBuyView.Columns(2).Width = 150
        GVBuyView.Columns(3).Width = 300
        GVBuyView.Columns(4).Width = 80
        '  GVBuyView.Columns(5).Width = 80
        col1.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        col1.SummaryItem.DisplayFormat = "الكمية الكلية:" + "{0}"
        col3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
        col3.SummaryItem.DisplayFormat = "السعرالكلي:" + "{0:n2}"
        '   Dim x As Integer = col3.SummaryItem.SummaryType / col1.SummaryItem.DisplayFormat
        col4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom
        col4.SummaryItem.DisplayFormat = "متوسط سعر الوحدة:" + "{0:n2}"
        GVBuyView.OptionsView.ShowFooter = True
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        done = False
        FrmMain.RibbonControl.Enabled = True
        Me.Close()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        done = False
        FrmMain.RibbonControl.Enabled = True
        Me.Close()
    End Sub

    Private Sub GVBuyView_CustomSummaryCalculate(sender As Object, e As CustomSummaryEventArgs) Handles GVBuyView.CustomSummaryCalculate
        Total = col3.SummaryItem.SummaryValue
        Quantity = col1.SummaryItem.SummaryValue
        If Quantity <> 0 Then
            Average = Total / Quantity
        Else
            Average = 0
        End If
        e.TotalValue = Average

    End Sub
    Private Sub FillUnit()
        If done = True And LokCrp.Text <> "" Then
            LokUnit.Properties.DataSource = ""
            Dim lst = (From c In db.V_cropUnits Where c.delCU = False And c.trkCrop = Val(LokCrp.EditValue.ToString()) Select c).ToList
            LokUnit.Properties.DataSource = lst
            LokUnit.Properties.DisplayMember = "unitName"
            LokUnit.Properties.ValueMember = "trkUnit"
            LokUnit.Properties.PopulateColumns()
            LokUnit.Properties.Columns(0).Visible = False
            LokUnit.Properties.Columns(1).Visible = False
            LokUnit.Properties.Columns(2).Visible = False
            LokUnit.Properties.Columns(4).Visible = False
            LokUnit.Properties.Columns(5).Visible = False
            LokUnit.Properties.Columns(6).Visible = False
            LokUnit.Properties.Columns(7).Visible = False

        End If
    End Sub

    Private Sub LokCrp_TextChanged(sender As Object, e As EventArgs) Handles LokCrp.TextChanged
        LokUnit.Text = ""
        FillUnit()
    End Sub
End Class