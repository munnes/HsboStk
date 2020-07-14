
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils

Public Class FrmSaleArv
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private Flag As Integer = 0

    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Public curCrop As Integer
#Region "Repository Variables"
    Public repUnit As New Repository.RepositoryItemLookUpEdit
    Public repClnt As New Repository.RepositoryItemLookUpEdit
    Public repCar As New Repository.RepositoryItemLookUpEdit
    Public repTxt As New Repository.RepositoryItemTextEdit
#End Region
#Region "Transfared Values"
    Public PLok As Integer
    Public PPeeler As Integer
    '*************
    Public PProd As Integer
    Public PAmt As Double
    Public PUnit As Integer
    Public PWeight As Double
    Public ChkSave As Integer
#End Region
    Private Sub FrmSaleArv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        '*************************
        'If ViewShip = True Then

        '    Dim tb As New V_Sale
        '    tb = (From s In db.V_Sales Where s.trkToPrd = PToTrk And s.delSale = 0 Select s).SingleOrDefault
        '    If Not IsNothing(tb) Then
        '        TxtRId.Text = Val(tb.trkSale)
        '        TxtLok.Text = tb.arivalName
        '        TxtPeeler.Text = tb.peelerName
        '        TxtSaleInf.Text = tb.saleInf
        '        DateSale.Text = tb.saleDate
        '        PLok = tb.trkArival
        '        PPeeler = tb.trkPeeler
        '        FillClnt()
        '        LokClient.EditValue = tb.trkClntAr

        '    Else
        '        ReadFromTo()
        '        FillClnt()
        '    End If
        'Else
        '    ReadFromTo()
        '    FillClnt()
        'End If

        '*****************************
        FillGrid()
        ViewDet()
        NotSaved()
        'ViewShip = False
        ProgressBarControl1.Visible = False
        FormatColumns()
        If Trim(TxtRId.Text) = "" Then
            MemAdd = True
            saved = False
        Else
            saved = True
        End If
    End Sub

    Private Sub ReadFromTo()
        Dim tb As New V_ToPrd
        tb = (From s In db.V_ToPrds Where s.trkToPrd = PToTrk Select s).SingleOrDefault
        TxtLok.Text = tb.arivalName
        TxtPeeler.Text = tb.peelerName
        DateSale.Text = tb.toPrdDate
        PLok = tb.trkArival
        PPeeler = tb.trkPeeler
        DateSale.ReadOnly = True
        'FillClients()
    End Sub
    Private Sub ViewDet()
        Dim i As Integer = 0

        Dim lst = (From s In db.V_SaleDets Where s.trkSale = Val(TxtRId.Text) And s.delSaleDet = 0
                   Select s).ToList
        While i < lst.Count
            Row = Row + 1
            RowInd = Row - 1
            GVSale.AddNewRow()
            GVSale.SetFocusedRowCellValue("trkSaleDet", lst.Item(i).trkSaleDet)
            GVSale.SetFocusedRowCellValue("trkProd", lst.Item(i).prodName)
            GVSale.SetFocusedRowCellValue("Amount", lst.Item(i).Amount)
            GVSale.SetFocusedRowCellValue("trkUnit", lst.Item(i).unitName)
            GVSale.SetFocusedRowCellValue("Weight", lst.Item(i).Weight)
            GVSale.SetFocusedRowCellValue("Price", lst.Item(i).price)
            ' GVSale.SetFocusedRowCellValue("trkClntAr", lst.Item(i).trkClntAr)
            GVSale.UpdateCurrentRow()
            '*****************************************
            PProd = lst.Item(i).trkProd
            PAmt = lst.Item(i).Amount
            PUnit = lst.Item(i).trkUnit
            PWeight = lst.Item(i).Weight
            i = i + 1
        End While

    End Sub
    Private Sub NotSaved()
        Dim i As Integer = Row
        Dim lst = (From s In db.V_toPrdDets Where s.trkToPrd = PToTrk And s.delToDet = 0 Select s).ToList
        While i < lst.Count

            Row = Row + 1
            RowInd = Row - 1
            GVSale.AddNewRow()
            GVSale.SetFocusedRowCellValue("trkProd", lst.Item(i).prodName)
            GVSale.SetFocusedRowCellValue("Amount", lst.Item(i).amount)
            GVSale.SetFocusedRowCellValue("trkUnit", lst.Item(i).unitName)
            GVSale.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVSale.UpdateCurrentRow()
            '*****************************************
            PProd = lst.Item(i).trkProd
            PAmt = lst.Item(i).amount
            PUnit = lst.Item(i).trkUnit
            PWeight = lst.Item(i).weight
            '*****************************************

            i = i + 1
        End While

    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVSale.OptionsFind.AlwaysVisible = False
        ' If CanSaveReq() = True Then

        If CanSaveReq() = True Then
            If MemAdd Or MemEdit Then
                SaveReqData()
                MemEdit = False
                MemAdd = False
            End If
        End If

        If Row > 0 Then
            SavedRow = Row
            SaveData()
            SaveEdit()
        End If

    End Sub
    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = NumOnly(e.KeyChar)
    End Sub
    'Private Function CanSaveReq() As Boolean
    '    CanSaveReq = False
    '    If LokClnt.Text = "" Then
    '        XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم منطقة الصادر ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    '        LokClnt.Focus()
    '        LokClnt.SelectAll()
    '        Exit Function
    '    End If

    '    CanSaveReq = True
    'End Function

    Private Sub SaveReqData()
        Dim tb As New sale
        Dim theDate As DateTime
        theDate = CType(DateSale.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkSale = trk
            tb.trkArival = Val(PLok)
            tb.saleDate = theDate.ToShortDateString
            'tb.trkToPrd = PToTrk
            tb.trkPeeler = Val(PPeeler)
            tb.ClntAr = Txt
            tb.delSale = False
            tb.saleInf = TxtSaleInf.Text
            db.sales.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True And saved = True Then
            tb = (From s In db.sales Where s.trkSale = Val(TxtRId.Text) And s.delSale = False Select s).Single()
            tb.delSale = False
            tb.saleInf = TxtSaleInf.Text
            tb.trkClntAr = Val(LokClient.EditValue.ToString())
            db.SubmitChanges()
            trk = Val(TxtRId.Text)
            Progress()
        End If
        TxtRId.Text = trk

    End Sub
    Private Sub Progress()
        ProgressBarControl1.Visible = True
        Me.ProgressBarControl1.EditValue = 0
        Me.Timer1.Start()
    End Sub
    Function NewReqKey() As Integer
        Dim TheKey As Integer
        Dim Qrymax = (From trk In db.sales Select trk.trkSale).ToList
        If Qrymax.Count = 0 Then
            TheKey = 1
        Else
            TheKey = Qrymax.Max() + 1
        End If
        Return TheKey
    End Function
    Private Function CanSave(ByVal Ind As Integer) As Boolean
        CanSave = False
        If Val(GVSale.GetRowCellValue(Ind, "Price")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال سعر الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVSale.SelectCell(Ind, col2)
            Exit Function
        End If

        CanSave = True
    End Function
    Private Function CanSaveReq() As Boolean
        CanSaveReq = False
        If LokClient.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المشتري ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokClient.Focus()
            LokClient.SelectAll()
            Exit Function
        End If

        CanSaveReq = True
    End Function
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.saleDets Select trk.trkSaleDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()

        Dim tbUnt As unit
        Dim tbPrd As product
        Dim TheReq As Integer
        Dim i As Integer = 0
        While i < GVSale.RowCount

            TheReq = Val(GVSale.GetRowCellValue(i, "trkSaleDet"))
            tbUnt = (From s In db.units Where s.unitName = Trim(GVSale.GetRowCellValue(i, "trkUnit")) And s.delUn = 0 Select s).Single
            tbPrd = (From s In db.products Where s.prodName = Trim(GVSale.GetRowCellValue(i, "trkProd")) And s.delPrd = 0 Select s).Single
            If TheReq = 0 Then
                If CanSave(i) = True Then
                    Dim tb As New saleDet
                    GVSale.UnselectRow(i)
                    GVSale.SetRowCellValue(i, "trkSaleDet", NewKey())
                    tb.trkSaleDet = Val(GVSale.GetRowCellValue(i, "trkSaleDet"))
                    tb.trkSale = Val(TxtRId.Text)
                    tb.trkUnit = tbUnt.trkUnit
                    tb.Weight = Val(GVSale.GetRowCellValue(i, "Weight"))
                    tb.price = Val(GVSale.GetRowCellValue(i, "Price"))

                    tb.delSaleDet = False
                    tb.trkProd = tbPrd.trkProd
                    tb.Amount = Val(GVSale.GetRowCellValue(i, "Amount"))
                    '***************************************
                    CalculatePrdUnit(tbUnt.trkUnit, Val(GVSale.GetRowCellValue(i, "Amount")), tbPrd.trkProd)
                    '***************************************
                    tb.untOne = UOne
                    tb.amtOne = AOne
                    tb.untTwo = UTwo
                    tb.amtTwo = ATwo
                    db.saleDets.InsertOnSubmit(tb)
                    db.SubmitChanges()
                    Progress()
                Else
                    GVSale.SelectRows(i, i)
                End If
            End If
            i = i + 1
        End While


    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New saleDet
        If GVSale.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVSale.RowCount
                Dim TheReq As Integer
                TheReq = Val(GVSale.GetRowCellValue(i, "trkSaleDet"))
                If (GVSale.IsRowSelected(i) = True And TheReq <> 0) Then
                    If CanSave(i) = True Then
                        tb = (From s In db.saleDets Where s.trkSaleDet = Val(TheReq) And s.trkSale = Val(TxtRId.Text) Select s).Single()
                        tb.price = Val(GVSale.GetRowCellValue(i, "Price"))


                        GVSale.UnselectRow(i)
                        db.SubmitChanges()
                        Progress()

                    End If
                End If
                i = i + 1
            End While

        End If
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVSale.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لم يتم حفظ السجلات المعدلة  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        Else
            If SaveInclose() = False Then
                saved = False
                MemEdit = False
                FrmChild = True
                Me.Close()
            Else
                BtnSave_Click(Nothing, Nothing)
            End If
        End If

    End Sub

    Public Sub FillClnt()
        Dim lst = (From c In db.clientArs Where c.delClnt = False And c.trkArival = PLok Select c).ToList

        'LokClient.Text = ""
        Me.LokClient.Properties.DataSource = lst
        LokClient.Properties.DisplayMember = "clntName"
        LokClient.Properties.ValueMember = "trkClntAr"
        LokClient.Properties.PopulateColumns()
        LokClient.Properties.Columns(0).Visible = False
        LokClient.Properties.Columns(2).Visible = False
        LokClient.Properties.Columns(3).Visible = False
        LokClient.Properties.Columns(4).Visible = False
        LokClient.Properties.Columns(5).Visible = False

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub
    Public Sub FillGrid()
        FillClnt()

        GridControl1.RepositoryItems.Add(repClnt)

        '***************** should be added her to avoid disappear when focus changed
        GridControl1.RepositoryItems.Add(repPrd)
        Dim list As BindingList(Of saleDetail) = New BindingList(Of saleDetail)

        GridControl1.DataSource = list

        GVSale.Columns(0).Visible = False

        GVSale.Columns(5).ColumnEdit = repTxt
        'GVSale.Columns(6).ColumnEdit = repClnt
        ' GVBuyDet.Columns(7).ColumnEdit = RepositoryItemCheckEdit1
        GVSale.OptionsSelection.MultiSelect = True
        GVSale.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect


    End Sub
    Private Sub FormatColumns()
        col1 = GVSale.Columns(1)
        col2 = GVSale.Columns(2)
        col3 = GVSale.Columns(3)
        col4 = GVSale.Columns(4)
        col5 = GVSale.Columns(5)
        '****************
        col1.Caption = "المنتج"
        '    col2.Caption = "المخزون"
        col2.Caption = "الكمية"
        col3.Caption = "الوحدة "
        col4.Caption = "الوزن"
        col5.Caption = "سعر الوحدة"
        GVSale.Columns(1).Width = 70
        GVSale.Columns(2).Width = 30
        GVSale.Columns(3).Width = 30
        GVSale.Columns(4).Width = 40
        GVSale.Columns(5).Width = 30
        '  GVSale.Columns(6).Width = 90

        GVSale.Columns(1).OptionsColumn.ReadOnly = True
        GVSale.Columns(2).OptionsColumn.ReadOnly = True
        GVSale.Columns(3).OptionsColumn.ReadOnly = True
        GVSale.Columns(4).OptionsColumn.ReadOnly = True
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVSale.OptionsFind.AlwaysVisible = False
        Dim tb As New saleDet
        Dim tbReq As sale
        Dim i As Integer = 0
        Dim lastRow As Integer = GVSale.RowCount - 1
        Dim lastValue As Integer = Val(GVSale.GetRowCellValue(lastRow, "trkSaleDet"))
        If lastValue = 0 And GVSale.IsRowSelected(lastRow) = True Then
            GVSale.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
        End If
        If GVSale.SelectedRowsCount <> 0 Then

            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVSale.RowCount
                    Dim TheReq As Integer
                    TheReq = Val(GVSale.GetRowCellValue(i, "trkSaleDet"))
                    If GVSale.IsRowSelected(i) = True Then
                        tb = (From s In db.saleDets Where s.trkSaleDet = Val(TheReq) And s.trkSale = Val(TxtRId.Text) And s.delSaleDet = 0 Select s).Single()
                        tb.delSaleDet = True
                        GVSale.DeleteRow(i)
                        i = i - 1
                        SavedRow = Row - 1
                        Row = SavedRow
                    End If
                    i = i + 1
                End While
                db.SubmitChanges()
            Else
                Exit Sub
            End If
        End If
        If GVSale.GetRowCellValue(0, "trkSaleDet") = 0 And saved = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف سجل الصرف المحلي؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            'If Msg = System.Windows.Forms.DialogResult.Yes Then
            '    tbReq = (From s In db.sales Where s.trkToPrd = PToTrk And s.delSale = False Select s).Single
            '    tbReq.delSale = True
            '    db.SubmitChanges()
            '    Me.TxtRId.Text = ""
            '    Me.TxtLok.Text = ""
            '    TxtPeeler.Text = ""
            '    DateSale.Text = ""
            '    TxtSaleInf.Text = ""
            '    Dim j As Integer = 0
            '    While j < GVSale.RowCount
            '        GVSale.DeleteRow(j)
            '    End While
            'End If
        End If
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        If GVSale.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لم يتم حفظ السجلات المعدلة  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        Else
            If SaveInclose() = False Then
                saved = False
                MemEdit = False
                FrmChild = True
                Me.Close()
            Else
                BtnSave_Click(Nothing, Nothing)
            End If
        End If

    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If GVSale.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVSale.OptionsFind.FindFilterColumns = "*"
            GVSale.ShowFindPanel()
            GVSale.OptionsFind.ShowClearButton = False
            GVSale.OptionsFind.ShowFindButton = False
        End If
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
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
        rpt.FilterString = " [trkSale] =" & Val(TxtRId.Text)

        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()

    End Sub


    '*****************this to edit saved data
    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub
    Public Function SaveInclose() As Boolean
        SaveInclose = False
        Dim i As Integer = 0
        While i < GVSale.RowCount
            If Val(GVSale.GetRowCellValue(i, "trkSaleDet")) = 0 Then
                GVSale.SelectRows(i, i)
            End If
            i = i + 1
        End While
        If GVSale.SelectedRowsCount <> 0 Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ التفاصيل المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                SaveInclose = True
            Else
                SaveInclose = False
                i = 0
                While i < GVSale.RowCount
                    If GVSale.IsRowSelected(i) Then
                        GVSale.UnselectRow(i)
                    End If
                    i = i + 1
                End While
            End If
        Else
            SaveInclose = False
        End If
        'If Row <> 0 Then
        '    If Val(GVExpShip.GetRowCellValue(GVExpShip.RowCount - 1, "trkSaleDet")) = 0 Then
        '        'If tb.trkBuyDet = 0 Then
        '        Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
        '        If Msg = System.Windows.Forms.DialogResult.Yes Then
        '            SaveInclose = True
        '        Else
        '            SaveInclose = False
        '        End If
        '    Else
        '        SaveInclose = False
        '    End If
        'End If
        If MemEdit = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ التعديلات الأخيرة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                SaveInclose = True
            End If
        End If
    End Function



    Private Sub TxtPInfo_TextChanged(sender As Object, e As EventArgs) Handles TxtSaleInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub LokClient_TextChanged(sender As Object, e As EventArgs) Handles LokClient.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub
End Class