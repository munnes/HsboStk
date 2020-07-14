
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils
Public Class FrmCash
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private Flag As Integer = 0
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Private MyID As Integer
    Public curCrop As Integer
    Private DoneLoc As Boolean
#Region "Repository Variables"
    Public repUnit As Repository.RepositoryItemLookUpEdit
    ' Public repTxt As New Repository.RepositoryItemTextEdit
#End Region
    Private Sub FrmCash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        FillGrid()
        FormatColumns()
        ProgressBarControl1.Visible = False
        If IsView Then
            ViewDet()
            Me.Refresh()
        End If
    End Sub
    Private Sub ViewDet()
        Dim i As Integer = 0
        Dim tb = (From s In db.sales Where s.trkSale = ID And s.delSale = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokLoc.EditValue = tb.trkArival
        'FillClient()
        FillPeeler()
        Me.LokPeeler.EditValue = tb.trkPeeler
        TxtClient.Text = tb.ClntAr
        Me.DateSale.Text = tb.saleDate
        TxtSaleInf.Text = tb.saleInf
        Dim TbUn As New V_prdUnit
        '*******************************
        Dim lst = (From s In db.saleDets Where s.trkSale = ID And s.delSaleDet = 0 Select s).ToList
        While i < lst.Count
            '******************Total
            TbUn = (From s In db.V_prdUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkProd = lst.Item(i).trkProd Select s).SingleOrDefault
            '$$$$$$$$$$$$$$$$ Calculate Index
            Dim j As Integer = 0
            Dim lstInd = (From s In db.V_prdUnits Where s.trkProd = lst.Item(i).trkProd Select s).ToList
            If lstInd.Count = 2 Then
                While j < 2
                    If lstInd.Item(j).trkUnit = lst.Item(i).trkUnit Then
                        Exit While
                    End If
                    j = j + 1
                End While
            End If
            '$$$$$$$$$$$$$

            Dim CurTotal As Integer = CalTotal(lst.Item(i).trkProd, j) + lst.Item(i).Amount
            '**********************
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVSale.AddNewRow()

            GVSale.SetFocusedRowCellValue("trkSaleDet", lst.Item(i).trkSaleDet)
            GVSale.SetFocusedRowCellValue("trkProd", lst.Item(i).trkProd)
            GVSale.SetFocusedRowCellValue("total", CurTotal)
            GVSale.SetFocusedRowCellValue("Amount", lst.Item(i).Amount)
            GVSale.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVSale.SetFocusedRowCellValue("Weight", lst.Item(i).Weight)
            GVSale.SetFocusedRowCellValue("Price", lst.Item(i).price)
            GVSale.UpdateCurrentRow()
            i = i + 1

        End While
        trk = ID
        i = 0
        CountView = 0
        ID = 0
        saved = True
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

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        GVSale.OptionsFind.FindFilterColumns = ""
        GVSale.OptionsFind.AlwaysVisible = False
        If RowInd = 0 Then
            If CanSaveReq() = False Then
                Exit Sub
            Else
                If Not MemAdd Then
                    MemAdd = True
                End If
            End If
        End If
        If Row = SavedRow Then
            If Not MemAddDet Then
                MemAddDet = True
                Row = Row + 1
                RowInd = Row - 1
                GVSale.AddNewRow()
            End If
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVSale.OptionsFind.AlwaysVisible = False
        If CanSaveReq() = True Then
            If Trim(TxtRId.Text) = "" Then
                MemAdd = True
            End If
            If MemAdd Or MemEdit Then
                SaveReqData()
                MemEdit = False
                MemAdd = False
            End If
            If IsDet = False Then
                IsDet = True
                Exit Sub
            End If
        End If

        If Row > 0 Then
            If MemAddDet Then
                If CanSave(RowInd) = True Then
                    SavedRow = Row
                    SaveData()
                    MemAddDet = False
                End If
            End If
            SaveEdit()
        End If
    End Sub
    Private Function CanSaveReq() As Boolean
        CanSaveReq = False
        If DateSale.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateSale.Focus()
            Exit Function
        End If
        If CType(DateSale.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateSale.Focus()
            Exit Function
        End If
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحطة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If
        If LokPeeler.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم القشارة\الغربال ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokPeeler.Focus()
            LokPeeler.SelectAll()
            Exit Function
        End If

        If TxtClient.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المشتري ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            TxtClient.Focus()
            TxtClient.SelectAll()
            Exit Function
        End If

        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New sale
        Dim theDate As DateTime
        theDate = CType(DateSale.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkSale = trk
            tb.trkArival = Val(LokLoc.EditValue)
            tb.saleDate = theDate.ToShortDateString
            'tb.trkToPrd = Val(TxtJobOrd.Text)
            tb.trkPeeler = Val(LokPeeler.EditValue)
            tb.ClntAr = TxtClient.Text
            tb.delSale = False
            tb.saleInf = TxtSaleInf.Text
            db.sales.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True And saved = True Then
            tb = (From s In db.sales Where s.trkSale = Val(TxtRId.Text) And s.delSale = False Select s).Single()
            tb.trkArival = Val(LokLoc.EditValue)
            tb.saleDate = theDate.ToShortDateString
            'tb.trkToPrd = Val(TxtJobOrd.Text)
            tb.trkPeeler = Val(LokPeeler.EditValue)
            tb.ClntAr = TxtClient.Text
            tb.delSale = False
            tb.saleInf = TxtSaleInf.Text
            db.SubmitChanges()
            trk = Val(TxtRId.Text)
            Progress()
        End If
        TxtRId.Text = trk

    End Sub
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
    Private Sub SaveData()

        Dim tb As New saleDet
        Dim TbUn As New V_prdUnit
        GVSale.SetRowCellValue(RowInd, "trkSaleDet", NewKey())
        tb.trkSaleDet = Val(GVSale.GetRowCellValue(RowInd, "trkSaleDet"))
        tb.trkSale = Val(TxtRId.Text)
        TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVSale.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkProd = Val(GVSale.GetRowCellValue(RowInd, "trkProd"))
                Select s).SingleOrDefault
        tb.trkUnit = TbUn.trkUnit
        tb.Weight = Val(GVSale.GetRowCellValue(RowInd, "Weight"))
        tb.delSaleDet = False
        tb.Amount = Val(GVSale.GetRowCellValue(RowInd, "Amount"))
        tb.Weight = Val(GVSale.GetRowCellValue(RowInd, "Weight"))
        tb.price = Val(GVSale.GetRowCellValue(RowInd, "Price"))
        tb.trkProd = Val(GVSale.GetRowCellValue(RowInd, "trkProd"))

        '***************************************
        CalculatePrdUnit(TbUn.trkUnit, Val(GVSale.GetRowCellValue(RowInd, "Amount")),
                    Val(GVSale.GetRowCellValue(RowInd, "trkProd")))

        '***************************************
        tb.untOne = UOne
        tb.amtOne = AOne
        tb.untTwo = UTwo
        tb.amtTwo = ATwo
        db.saleDets.InsertOnSubmit(tb)
        db.SubmitChanges()
        Progress()
        MemAddDet = False
        '****************************************************************

    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New saleDet
        Dim TbUn As New V_prdUnit
        If GVSale.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVSale.RowCount
                Dim TheReq As Integer
                TheReq = Val(GVSale.GetRowCellValue(i, "trkSaleDet"))
                If (GVSale.IsRowSelected(i) = True And TheReq <> 0) Then
                    If CanSave(i) = True Then
                        tb = (From s In db.saleDets Where s.trkSaleDet = Val(TheReq) And s.trkSale = Val(TxtRId.Text) Select s).Single()
                        tb.price = Val(GVSale.GetRowCellValue(i, "Price"))
                        '**************************************
                        TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVSale.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkProd = Val(GVSale.GetRowCellValue(i, "trkProd"))
                                Select s).SingleOrDefault
                        tb.trkUnit = TbUn.trkUnit
                        tb.Weight = Val(GVSale.GetRowCellValue(i, "Weight"))
                        tb.delSaleDet = False
                        tb.Amount = Val(GVSale.GetRowCellValue(i, "Amount"))
                        tb.Weight = Val(GVSale.GetRowCellValue(i, "Weight"))
                        tb.price = Val(GVSale.GetRowCellValue(i, "Price"))
                        tb.trkProd = Val(GVSale.GetRowCellValue(i, "trkProd"))

                        '***************************************
                        CalculatePrdUnit(TbUn.trkUnit, Val(GVSale.GetRowCellValue(i, "Amount")),
                    Val(GVSale.GetRowCellValue(i, "trkProd")))
                        '***************************************
                        tb.untOne = UOne
                        tb.amtOne = AOne
                        tb.untTwo = UTwo
                        tb.amtTwo = ATwo
                        '****************************************
                        GVSale.UnselectRow(i)
                        'db.SubmitChanges()
                        'Progress()
                    End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVSale.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub
    Private Function CanSave(ByVal Ind As Integer) As Boolean
        CanSave = False
        If IsSingleRow(Ind) = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً لقد قمت بادخال المحصول  مسبقا  يمكنك التعديل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        If GVSale.GetRowCellValue(Ind, "trkProd") = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المنتج ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVSale.SelectCell(Ind, col2)
            Exit Function
        End If
        If Val(GVSale.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVSale.SelectCell(Ind, col2)
            Exit Function
        End If
        If Val(GVSale.GetRowCellValue(Ind, "Amount")) > Val(GVSale.GetRowCellValue(Ind, "total")) Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الكمية أكثر من المخزون ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVSale.SelectCell(Ind, col3)
            Exit Function
        End If
        If GVSale.GetRowCellValue(Ind, "trkUnit") = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVSale.SelectCell(Ind, col2)
            Exit Function
        End If
        If Val(GVSale.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVSale.SelectCell(Ind, col2)
            Exit Function
        End If
        If Val(GVSale.GetRowCellValue(Ind, "Price")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال سعر الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVSale.SelectCell(Ind, col2)
            Exit Function
        End If
        Dim TheDate As DateTime
        TheDate = CType(Me.DateSale.Text, DateTime)
        If MemAddDet Then
            Dim countLst As Integer
            Dim lst = (From s In db.V_RecvDets Where s.recvDate > TheDate And s.trkProd = Val(GVSale.GetRowCellValue(Ind, "trkProd")) _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                And s.isLocal = True And s.trkClntCrp = 0
                       Select s).ToList()
            countLst = lst.Count
            If countLst > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن صرف المنتج ...تمت تخزين نفس المنتج في وقت لاحق ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If

            Dim lstSale = (From s In db.V_SaleDets Where s.saleDate > TheDate And s.trkProd = Val(GVSale.GetRowCellValue(Ind, "trkProd")) _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s).ToList()

            countLst = lstSale.Count
            If countLst > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن صرف المنتج ...تمت صرف نفس المنتج محليا في وقت لاحق ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
            Dim lstPlr = (From s In db.V_expShipDets Where s.expShipDate > TheDate And s.trkProd = Val(GVSale.GetRowCellValue(Ind, "trkProd")) _
                                           And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue)
                          Select s).ToList()

            countLst = lstPlr.Count
            If countLst > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن صرف المنتج ...تمت شحن نفس المنتج من القشارة في وقت لاحق ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
        End If
        CanSave = True
    End Function
    'Public Sub FillClient()
    '    If done = True And LokLoc.Text <> "" Then
    '        Dim lst = (From c In db.clientArs Where c.delClnt = False And c.trkArival = Val(LokLoc.EditValue) Select c).ToList
    '        Me.LokClient.Properties.DataSource = lst
    '        LokClient.Properties.DisplayMember = "clntName"
    '        LokClient.Properties.ValueMember = "trkClntAr"
    '        LokClient.Properties.PopulateColumns()
    '        LokClient.Properties.Columns(0).Visible = False
    '        LokClient.Properties.Columns(2).Visible = False
    '        LokClient.Properties.Columns(3).Visible = False
    '        LokClient.Properties.Columns(4).Visible = False
    '        LokClient.Properties.Columns(5).Visible = False
    '    End If
    'End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        If GVSale.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لم يتم حفظ السجلات المعدلة  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        Else
            If SaveInclose() = False Then
                ResetAtClose()
                Me.Close()
            Else
                BtnSave_Click(Nothing, Nothing)
            End If
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
    Private Sub LokLoc_EditValueChanged(sender As Object, e As EventArgs) Handles LokLoc.EditValueChanged
        'LokClient.Text = ""
        'FillClient()
        LokPeeler.Text = ""
        FillPeeler()
        TheStk()
        If saved = True Then
            MemEdit = True
        End If

    End Sub
    Public Sub FillProd()
        Dim prd = (From s In db.products Where s.delPrd = False
                   Select s).ToList
        repPrd.DataSource = prd
        repPrd.ValueMember = "trkProd"
        repPrd.DisplayMember = "prodName"
        repPrd.ShowHeader = False
        repPrd.PopulateColumns()
        repPrd.Columns(0).Visible = False
        repPrd.Columns(2).Visible = False
        repPrd.Columns(3).Visible = False
        repPrd.NullText = ""
    End Sub
    Public Sub FillGrid()
        '***************** should be added her to avoid disappear when focus changed
        GridControl1.RepositoryItems.Add(repPrd)
        Dim list As BindingList(Of saleDetail) = New BindingList(Of saleDetail)

        GridControl1.DataSource = list
        FillProd()
        GVSale.Columns(0).Visible = False
        GVSale.Columns(1).ColumnEdit = repPrd
        GVSale.Columns(2).ColumnEdit = repTxt
        GVSale.Columns(3).ColumnEdit = repTxt
        GVSale.Columns(5).ColumnEdit = repTxt
        GVSale.Columns(6).ColumnEdit = repTxt

        GVSale.OptionsSelection.MultiSelect = True
        GVSale.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect


    End Sub
    Private Sub FormatColumns()
        col1 = GVSale.Columns(1)
        col2 = GVSale.Columns(2)
        col3 = GVSale.Columns(3)
        col4 = GVSale.Columns(4)
        col5 = GVSale.Columns(5)
        col6 = GVSale.Columns(6)
        '****************
        col1.Caption = "المنتج"
        col2.Caption = "المخزون"
        col3.Caption = "الكمية"
        col4.Caption = "الوحدة"
        col5.Caption = "الوزن"
        col6.Caption = "سعر الوحدة"

        GVSale.Columns(2).OptionsColumn.ReadOnly = True
        GVSale.Columns(2).AppearanceCell.BackColor = Color.DarkRed
        GVSale.Columns(2).AppearanceCell.ForeColor = Color.White
        GVSale.Columns(2).AppearanceCell.FontSizeDelta = Font.Bold
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVSale.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لم يتم حفظ السجلات المعدلة  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        Else
            If SaveInclose() = False Then
                ResetAtClose()
                Me.Close()
            Else
                BtnSave_Click(Nothing, Nothing)
            End If
        End If

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

    End Sub

    Private Sub Progress()
        ProgressBarControl1.Visible = True
        Me.ProgressBarControl1.EditValue = 0
        Me.Timer1.Start()
    End Sub
    Public Function SaveInclose() As Boolean

        If GVSale.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End If
        If Row <> 0 Then
            If Val(GVSale.GetRowCellValue(GVSale.RowCount - 1, "trkSaleDet")) = 0 Then
                'If tb.trkBuyDet = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVSale.RowCount - 1
                    GVSale.FocusedRowHandle = lastRow
                    GVSale.DeleteRow(lastRow)
                    SavedRow = Row - 1
                    Row = SavedRow
                    MemAddDet = False
                    IsDet = False
                End If
            Else
                SaveInclose = False
            End If
        End If
        If MemEdit = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ التعديلات الأخيرة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                SaveInclose = True
            End If
        End If
    End Function

    Private Sub GVSale_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVSale.CustomRowCellEditForEditing
        Dim prd As Integer
        If e.Column.Caption = "المنتج" Then
            GVSale.SetFocusedRowCellValue("trkUnit", "")
        End If

        If e.Column.Caption = "الوحدة" Then
            prd = GVSale.GetRowCellValue(e.RowHandle, "trkProd")
            If prd <> 0 Then
                FillUnit(prd)
                e.RepositoryItem = repUnit

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

    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub
    Public Sub FillUnit(ByVal prd As Integer)

        Dim un = (From s In db.V_prdUnits Where s.delPU = False And s.trkProd = prd
                  Select s).ToList
        repUnit = New Repository.RepositoryItemLookUpEdit
        repUnit.DataSource = un
        repUnit.DisplayMember = "unitName"
        repUnit.ValueMember = "unitName"

        repUnit.DisplayFormat.ToString()
        repUnit.ShowHeader = False
        repUnit.PopulateColumns()

        repUnit.Columns(0).Visible = False
        repUnit.Columns(1).Visible = False
        repUnit.Columns(2).Visible = False
        repUnit.Columns(3).Visible = False
        repUnit.Columns(4).Visible = False
        repUnit.Columns(6).Visible = False
        repUnit.Columns(7).Visible = False
        repUnit.Columns(8).Visible = False
        repUnit.NullText = ""

    End Sub
    Private Function CalTotal(ByVal prd As Integer, ByVal UInd As Integer) As Double
        Dim tbExp As New ProduceExpResult
        Dim tbRcv As New ProduceRecvResult
        Dim tbSale As New ProduceSaleResult
        Dim FstAmt As Decimal = 0
        CalTotal = 0
        Dim SecAmt As Decimal
        Dim TheDate As DateTime
        If DateSale.Text <> "" Then
            TheDate = CType(DateSale.Text, DateTime)
        End If
        tbSale = (From s In db.ProduceSale(TheDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue), prd) Select s).SingleOrDefault
        If Not IsNothing(tbSale) Then
            FstAmt = tbSale.oneUnt
            If Not IsNothing(tbSale.twoUnt) Then
                SecAmt = tbSale.twoUnt
            Else
                SecAmt = 0
            End If
        End If
        If FstAmt > 0 Then
            tbRcv = (From s In db.ProduceRecv(TheDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue) _
                         , prd, FstAmt, SecAmt) Select s).SingleOrDefault
            If Not IsNothing(tbRcv) Then
                FstAmt = tbRcv.AOne
                SecAmt = tbRcv.ATwo
            End If
        End If
        If FstAmt > 0 Then
            tbExp = (From s In db.ProduceExp(TheDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue),
                         prd, FstAmt, SecAmt) Select s).SingleOrDefault
            If Not IsNothing(tbExp) Then
                FstAmt = tbExp.AOne
                SecAmt = tbExp.ATwo
            End If

        End If
        If UInd = 0 Then
            CalTotal = FstAmt
        Else
            CalTotal = SecAmt
        End If
        CalTotal = Math.Round(CalTotal, 2)

    End Function
    Private Sub GVSale_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GVSale.CellValueChanged
        Dim prd As Integer = GVSale.GetRowCellValue(e.RowHandle, "trkProd")
        Dim StrUn As String = Trim(GVSale.GetFocusedRowCellValue("trkUnit"))
        Dim TheId As Integer = GVSale.GetRowCellValue(e.RowHandle, "trkSaleDet")
        Dim Amt As Double = GVSale.GetRowCellValue(e.RowHandle, "Amount")
        Dim Total As Double
        Dim tbSale As New V_SaleDet
        Dim i As Integer = 0
        Dim TheDate As DateTime
        If DateSale.Text <> "" Then
            TheDate = CType(DateSale.Text, DateTime)
        End If
        If e.Column.Caption = "الوحدة" Then
            's.unitName = StrUn
            If StrUn <> "" And prd <> 0 Then
                Dim lst = (From s In db.V_prdUnits Where s.trkProd = prd And s.delPrd = 0 Select s).ToList
                If lst.Count = 2 Then
                    While i < 2
                        If lst.Item(i).unitName = StrUn Then
                            Exit While
                        End If
                        i = i + 1
                    End While
                End If
                Total = CalTotal(prd, i)
                If TheId <> 0 Then
                    tbSale = (From s In db.V_SaleDets Where s.trkSaleDet = TheId _
                                                           And s.trkProd = prd And s.trkArival = Val(LokLoc.EditValue) _
                                                     And s.trkPeeler = Val(LokPeeler.EditValue) And s.saleDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbSale) Then
                        If i = 0 Then
                            Total = Total + tbSale.amtOne
                            GVSale.SetFocusedRowCellValue("Amount", tbSale.amtOne)
                        Else
                            Total = Total + tbSale.amtTwo
                            GVSale.SetFocusedRowCellValue("Amount", tbSale.amtTwo)
                        End If

                    End If
                End If

                GVSale.SetFocusedRowCellValue("total", Total)
            Else
                GVSale.SetFocusedRowCellValue("total", 0)
            End If
        End If

    End Sub

    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
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

        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()

    End Sub

    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVSale.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Prod As Integer = Val(GVSale.GetRowCellValue(Ind, "trkProd"))

        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVSale.GetRowCellValue(i, "trkProd")) = Prod Then
                    IsSingleRow = False
                    Exit While
                End If
            End If
            i = i + 1
        End While
        Return IsSingleRow
    End Function

    '*********************Edit
    Private Sub TheStk()
        Dim count As Integer = GVSale.RowCount
        Dim i As Integer = 0
        Dim UndPreShip As Double = 0
        Dim tbSale As New V_SaleDet
        Dim Total As Double
        Dim curProd As Integer
        Dim TheDate As DateTime
        If DateSale.Text <> "" Then
            TheDate = CType(DateSale.Text, DateTime)
        End If
        While i < count
            Dim TbUn As New V_prdUnit
            curProd = GVSale.GetRowCellValue(i, "trkProd")

            Dim j As Integer = 0
            Dim StrUn As String = Trim(GVSale.GetRowCellValue(i, "trkUnit"))
            '$$$$$$$$$$$$$$$$$$$$$$$$
            If StrUn <> "" And curProd <> 0 Then
                Dim lst = (From s In db.V_prdUnits Where s.trkProd = curProd Select s).ToList
                If lst.Count = 2 Then
                    While j < 2
                        If lst.Item(j).unitName = StrUn Then
                            Exit While
                        End If
                        j = j + 1
                    End While
                End If

                '$$$$$$$$$$$$$$$$$$$$$$$

                If Val(GVSale.GetRowCellValue(i, "trkSaleDet")) <> 0 Then
                    tbSale = (From s In db.V_SaleDets Where s.trkSaleDet = Val(GVSale.GetRowCellValue(i, "trkSaleDet")) _
                                                           And s.trkProd = Val(GVSale.GetRowCellValue(i, "trkProd")) And s.trkArival = Val(LokLoc.EditValue) _
                                                     And s.trkPeeler = Val(LokPeeler.EditValue) And s.saleDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbSale) Then
                        UndPreShip = tbSale.Amount
                    Else
                        UndPreShip = 0
                    End If
                    GVSale.SelectRow(i)
                End If
                Total = CalTotal(curProd, j) + UndPreShip
                GVSale.SetRowCellValue(i, "total", Total)

            End If

            If i = count - 1 Then
                If Val(GVSale.GetRowCellValue(count - 1, "trkSaleDet")) = 0 Then
                    GVSale.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub

    Private Sub LokPeeler_EditValueChanged(sender As Object, e As EventArgs) Handles LokPeeler.EditValueChanged
        TheStk()
        If saved = True Then
            MemEdit = True
        End If

    End Sub

    Private Sub DateSale_EditValueChanged(sender As Object, e As EventArgs) Handles DateSale.EditValueChanged
        TheStk()
        If saved = True Then
            MemEdit = True
        End If

    End Sub

    Private Sub LokClient_EditValueChanged(sender As Object, e As EventArgs)
        If saved = True Then
            MemEdit = True
        End If

    End Sub

    Private Sub TxtSaleInf_TextChanged(sender As Object, e As EventArgs) Handles TxtSaleInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub
End Class