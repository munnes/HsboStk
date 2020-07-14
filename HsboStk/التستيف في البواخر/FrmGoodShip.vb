
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmGoodShip
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Public curProd As Integer
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
#Region "Repository Variables"
    ' Public repPrd As New Repository.RepositoryItemLookUpEdit
    Public repUnit As Repository.RepositoryItemLookUpEdit
    Public repMem As New Repository.RepositoryItemMemoEdit
#End Region

    Private Sub FrmAddBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillExp()
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
        Dim tb = (From s In db.goodShps Where s.trkGood = ID And s.delGood = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokExp.EditValue = tb.trkExpLok
        Me.MemShipDet.Text = tb.ship
        Me.DateGood.Text = tb.goodDate
        TxtGoodInf.Text = tb.goodInf
        Dim TbUn As New V_prdUnit
        '*******************************
        Dim lst = (From s In db.goodShpDets Where s.trkGood = ID And s.delGoodDet = 0 Select s).ToList
        While i < lst.Count
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVStkArDet.AddNewRow()
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

            Dim CurTotal As Integer = CalTotal(lst.Item(i).trkProd, j) + lst.Item(i).aStock
            '**********************
            GVStkArDet.SetFocusedRowCellValue("trkGoodDet", lst.Item(i).trkGoodDet)
            GVStkArDet.SetFocusedRowCellValue("trkProd", lst.Item(i).trkProd)
            GVStkArDet.SetFocusedRowCellValue("total", CurTotal)
            GVStkArDet.SetFocusedRowCellValue("Amount", lst.Item(i).aStock)
            GVStkArDet.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVStkArDet.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVStkArDet.SetFocusedRowCellValue("ShipDet", lst.Item(i).shipDet)
            GVStkArDet.UpdateCurrentRow()
            i = i + 1

        End While
        trk = ID
        i = 0
        CountView = 0
        ID = 0
        saved = True
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

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        GVStkArDet.OptionsFind.FindFilterColumns = ""
        GVStkArDet.OptionsFind.AlwaysVisible = False

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
                GVStkArDet.AddNewRow()
            End If
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVStkArDet.OptionsFind.AlwaysVisible = False
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
        If DateGood.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateGood.Focus()
            Exit Function
        End If
        If CType(DateGood.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateGood.Focus()
            Exit Function
        End If
        If LokExp.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم منطقة الصادر ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokExp.Focus()
            LokExp.SelectAll()
            Exit Function
        End If

        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New goodShp
        Dim theDate As DateTime
        theDate = CType(DateGood.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkGood = trk
            tb.trkExpLok = Val(LokExp.EditValue.ToString())
            tb.goodDate = theDate.ToShortDateString
            tb.ship = MemShipDet.Text
            tb.goodInf = TxtGoodInf.Text
            tb.delGood = False
            db.goodShps.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.goodShps Where s.trkGood = Val(TxtRId.Text) And s.delGood = False Select s).Single()
            tb.trkExpLok = Val(LokExp.EditValue.ToString())
            tb.goodDate = theDate.ToShortDateString
            tb.goodInf = TxtGoodInf.Text
            tb.delGood = False
            tb.ship = MemShipDet.Text
            db.SubmitChanges()
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
        Dim Qrymax = (From trk In db.goodShps Select trk.trkGood).ToList
        If Qrymax.Count = 0 Then
            TheKey = 1
        Else
            TheKey = Qrymax.Max() + 1
        End If
        Return TheKey
    End Function
    Private Function CanSave(ByVal Ind As Integer) As Boolean
        CanSave = False
        If IsSingleRow(Ind) = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً لقد قمت بادخال المحصول  مسبقا  يمكنك التعديل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        If Val(GVStkArDet.GetRowCellValue(Ind, "trkProd")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المنتج ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVStkArDet.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVStkArDet.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVStkArDet.SelectCell(Ind, col3)
            Exit Function
        End If

        If Val(GVStkArDet.GetRowCellValue(Ind, "Amount")) > Val(GVStkArDet.GetRowCellValue(Ind, "total")) Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الكمية أكثر من المخزون ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVStkArDet.SelectCell(Ind, col3)
            Exit Function
        End If
        If (GVStkArDet.GetRowCellValue(Ind, "trkUnit")) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVStkArDet.SelectCell(Ind, col5)
            Exit Function
        End If
        If Val(GVStkArDet.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVStkArDet.SelectCell(Ind, col4)
            Exit Function
        End If

        If MemAddDet Then
            Dim countLst As Integer
            Dim TheDate As DateTime
            TheDate = CType(Me.DateGood.Text, DateTime)
            Dim lst = (From s In db.V_gdShps Where s.goodDate > TheDate.ToShortDateString And s.trkProd = Val(GVStkArDet.GetRowCellValue(Ind, "trkProd")) _
                                             And s.trkExpLok = Val(LokExp.EditValue)
                       Select s).ToList()
            countLst = lst.Count
            If countLst > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن اضافة المنتج ...تمت اضافة نفس المنتج في وقت لاحق ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
        End If
        CanSave = True
    End Function
    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVStkArDet.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Prod As Integer = Val(GVStkArDet.GetRowCellValue(Ind, "trkProd"))
        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVStkArDet.GetRowCellValue(i, "trkProd")) = Prod Then
                    IsSingleRow = False
                    Exit While
                End If
            End If
            i = i + 1
        End While
        Return IsSingleRow
    End Function
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.goodShpDets Select trk.trkGoodDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()
        Dim tb As New goodShpDet
        Dim TbUn As New V_prdUnit
        If MemAddDet = True Then
            GVStkArDet.SetRowCellValue(RowInd, "trkGoodDet", NewKey())
            tb.trkGoodDet = Val(GVStkArDet.GetRowCellValue(RowInd, "trkGoodDet"))
            tb.trkGood = Val(TxtRId.Text)
            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVStkArDet.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkProd = Val(GVStkArDet.GetRowCellValue(RowInd, "trkProd")) Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVStkArDet.GetRowCellValue(RowInd, "Weight"))
            tb.delGoodDet = False
            tb.trkProd = Val(GVStkArDet.GetRowCellValue(RowInd, "trkProd"))
            tb.aStock = Val(GVStkArDet.GetRowCellValue(RowInd, "Amount"))
            tb.shipDet = GVStkArDet.GetRowCellValue(RowInd, "ShipDet")
            '***************************************
            CalculatePrdUnit(TbUn.trkUnit, Val(GVStkArDet.GetRowCellValue(RowInd, "Amount")),
                    Val(GVStkArDet.GetRowCellValue(RowInd, "trkProd")))
            '***************************************
            tb.untOne = UOne
            tb.amtOne = AOne
            tb.untTwo = UTwo
            tb.amtTwo = ATwo
            db.goodShpDets.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New goodShpDet
        Dim Thetrk As Integer
        Dim TbUn As New V_prdUnit
        If GVStkArDet.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVStkArDet.RowCount
                Dim TheReq As Integer
                TheReq = Val(GVStkArDet.GetRowCellValue(i, "trkGoodDet"))
                If (GVStkArDet.IsRowSelected(i) = True) Then
                    Thetrk = Val(GVStkArDet.GetRowCellValue(i, "trkProd"))
                    If CheckEditDel(Thetrk, Thetrk) = True Then
                        If CanSave(i) = True Then
                            tb = (From s In db.goodShpDets Where s.trkGoodDet = Val(TheReq) And s.trkGood = Val(TxtRId.Text) Select s).Single()
                            tb.trkGood = Val(TxtRId.Text)
                            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVStkArDet.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkProd = Val(GVStkArDet.GetRowCellValue(i, "trkProd")) Select s).SingleOrDefault
                            tb.trkUnit = TbUn.trkUnit
                            tb.weight = Val(GVStkArDet.GetRowCellValue(i, "Weight"))
                            tb.delGoodDet = False
                            tb.trkProd = Val(GVStkArDet.GetRowCellValue(i, "trkProd"))
                            tb.aStock = Val(GVStkArDet.GetRowCellValue(i, "Amount"))
                            tb.shipDet = GVStkArDet.GetRowCellValue(i, "ShipDet")
                            CalculatePrdUnit(TbUn.trkUnit, Val(GVStkArDet.GetRowCellValue(i, "Amount")),
                            Val(GVStkArDet.GetRowCellValue(i, "trkProd")))
                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                            GVStkArDet.UnselectRow(i)
                        End If
                    End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVStkArDet.SelectedRowsCount = 0 Then
                Progress()
            End If

        End If
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVStkArDet.SelectedRowsCount <> 0 Then
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub
    Public Sub FillGrid()
        FillProd()
        ' FillUnit()
        ' GridControl1.RepositoryItems.Add(repUnit)
        GridControl1.RepositoryItems.Add(repPrd)
        GridControl1.RepositoryItems.Add(repTxt)
        Dim list As BindingList(Of goodDet) = New BindingList(Of goodDet)
        GridControl1.DataSource = list
        GVStkArDet.Columns(0).ColumnEdit = repTxt
        GVStkArDet.Columns(0).Visible = False
        GVStkArDet.Columns(1).ColumnEdit = repPrd
        GVStkArDet.Columns(2).ColumnEdit = repTxt
        GVStkArDet.Columns(3).ColumnEdit = repTxt
        ' GVStkArDet.Columns(4).ColumnEdit = repUnit
        GVStkArDet.Columns(5).ColumnEdit = repTxt
        GVStkArDet.Columns(6).ColumnEdit = repMem
        GVStkArDet.OptionsSelection.MultiSelect = True
        GVStkArDet.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub
    Private Sub FormatColumns()
        col1 = GVStkArDet.Columns(1)
        col2 = GVStkArDet.Columns(2)
        col3 = GVStkArDet.Columns(3)
        col4 = GVStkArDet.Columns(4)
        col5 = GVStkArDet.Columns(5)
        col6 = GVStkArDet.Columns(6)
        '****************
        col1.Caption = "المنتج"
        col2.Caption = "المخزون"
        col3.Caption = "الكمية "
        col4.Caption = "الوحدة"
        col5.Caption = "الوزن"
        col6.Caption = "تفاصيل الشحن"
        GVStkArDet.Columns(1).Width = 50
        GVStkArDet.Columns(2).Width = 30
        GVStkArDet.Columns(3).Width = 30
        GVStkArDet.Columns(4).Width = 30
        GVStkArDet.Columns(5).Width = 30
        GVStkArDet.Columns(2).OptionsColumn.ReadOnly = True
        GVStkArDet.Columns(2).AppearanceCell.BackColor = Color.DarkRed
        GVStkArDet.Columns(2).AppearanceCell.ForeColor = Color.White
        GVStkArDet.Columns(2).AppearanceCell.FontSizeDelta = Font.Bold
    End Sub
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVStkArDet.OptionsFind.AlwaysVisible = False
        Dim tb As New goodShpDet
        Dim i As Integer = 0
        Dim Thetrk As Integer
        Dim trk As Integer
        Dim lastRow As Integer = GVStkArDet.RowCount - 1
        Dim lastValue As Integer = Val(GVStkArDet.GetRowCellValue(lastRow, "trkGoodDet"))
        If lastValue = 0 And GVStkArDet.IsRowSelected(lastRow) = True Then
            GVStkArDet.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
            MemAddDet = False
        End If
        If GVStkArDet.SelectedRowsCount <> 0 Then
            'E
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVStkArDet.RowCount
                    Dim TheReq As Integer
                    TheReq = Val(GVStkArDet.GetRowCellValue(i, "trkGoodDet"))
                    If GVStkArDet.IsRowSelected(i) = True Then
                        Thetrk = Val(GVStkArDet.GetRowCellValue(i, "trkProd"))
                        If CheckEditDel(TheReq, Thetrk) = True Then
                            tb = (From s In db.goodShpDets Where s.trkGoodDet = Val(TheReq) And s.trkGood = Val(TxtRId.Text) Select s).Single()
                            tb.delGoodDet = True
                            GVStkArDet.DeleteRow(i)
                            i = i - 1
                            SavedRow = Row - 1
                            Row = SavedRow
                            MemAddDet = False
                        End If
                    End If
                        i = i + 1
                End While
                db.SubmitChanges()
            Else
                Exit Sub
            End If
        End If

    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        If GVStkArDet.SelectedRowsCount <> 0 Then
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

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If GVStkArDet.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVStkArDet.OptionsFind.FindFilterColumns = "*"
            GVStkArDet.ShowFindPanel()
            GVStkArDet.OptionsFind.ShowClearButton = False
            GVStkArDet.OptionsFind.ShowFindButton = False
        End If
    End Sub

    '*****************this to edit saved data
    Private Sub LokExp_TextChanged(sender As Object, e As EventArgs) Handles LokExp.TextChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub DateGood_EditValueChanged(sender As Object, e As EventArgs) Handles DateGood.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub
    Public Function SaveInclose() As Boolean
        If Row <> 0 Then
            If Val(GVStkArDet.GetRowCellValue(GVStkArDet.RowCount - 1, "trkGoodDet")) = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVStkArDet.RowCount - 1
                    GVStkArDet.FocusedRowHandle = lastRow
                    GVStkArDet.DeleteRow(lastRow)
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

    Private Sub MemShipDet_TextChanged(sender As Object, e As EventArgs) Handles MemShipDet.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub TxtGoodInf_TextChanged(sender As Object, e As EventArgs) Handles TxtGoodInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
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
        rpt.TheFilter.Value = ""
        rpt.FilterString = " [trkGood] =" & Val(TxtRId.Text)
        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub

    Private Sub TheStk()
        Dim count As Integer = GVStkArDet.RowCount
        Dim i As Integer = 0
        Dim UndPreShip As Double = 0
        Dim Total As Double
        Dim tbGood As New V_gdShp
        Dim TheDate As DateTime
        If Me.DateGood.Text <> "" Then
            TheDate = CType(DateGood.Text, DateTime)
        End If
        While i < count
            Dim TbUn As New V_prdUnit
            curProd = GVStkArDet.GetRowCellValue(i, "trkProd")
            Dim j As Integer = 0
            Dim StrUn As String = Trim(GVStkArDet.GetRowCellValue(i, "trkUnit"))
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

                '    GVStkArDet.SetFocusedRowCellValue("total", CalTotal(curProd, j))
                'Else
                '    GVStkArDet.SetFocusedRowCellValue("total", 0)
                'End If
                '$$$$$$$$$$$$$$$$$$$$$$$
                If Val(GVStkArDet.GetRowCellValue(i, "trkGoodDet")) <> 0 Then
                    tbGood = (From s In db.V_gdShps Where s.trkGoodDet = Val(GVStkArDet.GetRowCellValue(i, "trkGoodDet")) _
                                                     And s.trkProd = curProd And s.trkExpLok = Val(LokExp.EditValue) _
                                                    And s.goodDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbGood) Then
                        UndPreShip = tbGood.aStock
                    Else
                        UndPreShip = 0
                    End If
                    GVStkArDet.SelectRow(i)
                End If
                Total = CalTotal(curProd, j) + UndPreShip
                GVStkArDet.SetRowCellValue(i, "total", Total)
            End If

            If i = count - 1 Then
                If Val(GVStkArDet.GetRowCellValue(count - 1, "trkGoodDet")) = 0 Then
                    GVStkArDet.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub
    Private Function CalTotal(ByVal prd As Integer, ByVal UInd As Integer) As Double
        Dim TheDate As DateTime
        If Me.DateGood.Text <> "" Then
            TheDate = CType(DateGood.Text, DateTime)
        End If
        Dim tb As New PrdGoodResult
        tb = (From s In db.PrdGood(TheDate, Val(LokExp.EditValue), prd) Select s).SingleOrDefault

        If Not IsNothing(tb) Then
            If UInd = 0 Then
                CalTotal = tb.oneUnt
            Else
                CalTotal = tb.twoUnt
            End If
        End If
        CalTotal = Math.Round(CalTotal, 2)
    End Function


    Private Sub LokStore_TextChanged(sender As Object, e As EventArgs)
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    'Private Sub repPrd_CloseUp(sender As Object, e As CloseUpEventArgs) Handles repPrd.CloseUp
    '    '    GVStkArDet.OptionsView.RowAutoHeight = False
    '    If Not IsNothing(e.Value) Then
    '        curProd = e.Value
    '        GVStkArDet.SetFocusedRowCellValue("total", CalTotal(curProd))

    '    Else
    '        GVStkArDet.SetFocusedRowCellValue("total", 0)
    '    End If
    '    GVStkArDet.SetFocusedRowCellValue("trkProd", curProd)
    'End Sub

    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub

    Private Sub GVStkArDet_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVStkArDet.CustomRowCellEditForEditing
        Dim prd As Integer
        If e.Column.Caption = "المنتج" Then
            GVStkArDet.SetFocusedRowCellValue("trkUnit", "")
        End If
        If e.Column.Caption = "الوحدة" Then
            prd = GVStkArDet.GetRowCellValue(e.RowHandle, "trkProd")
            If prd <> 0 Then
                FillUnit(prd)
                e.RepositoryItem = repUnit
            End If
        End If
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

    Private Sub GVStkArDet_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GVStkArDet.CellValueChanged
        Dim prd As Integer = GVStkArDet.GetRowCellValue(e.RowHandle, "trkProd")
        Dim StrUn As String = Trim(GVStkArDet.GetFocusedRowCellValue("trkUnit"))
        Dim TheId As Integer = GVStkArDet.GetRowCellValue(e.RowHandle, "trkGoodDet")
        Dim Amt As Double = GVStkArDet.GetRowCellValue(e.RowHandle, "Amount")
        Dim tb As New expStock
        Dim Total As Double
        Dim tbGood As New V_gdShp
        Dim TbUn As New V_prdUnit
        Dim i As Integer = 0
        Dim TheDate As DateTime
        If Me.DateGood.Text <> "" Then
            TheDate = CType(DateGood.Text, DateTime)
        End If
        If e.Column.Caption = "الوحدة" Then
            If StrUn <> "" And prd <> 0 Then
                Dim lst = (From s In db.V_prdUnits Where s.trkProd = prd And s.delPU = 0 Select s).ToList
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
                    tbGood = (From s In db.V_gdShps Where s.trkGoodDet = Val(TheId) _
                                                     And s.trkProd = prd And s.trkExpLok = Val(LokExp.EditValue) _
                                                    And s.goodDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbGood) Then
                        If i = 0 Then
                            Total = Total + tbGood.amtOne
                            GVStkArDet.SetFocusedRowCellValue("Amount", tbGood.amtOne)
                        Else
                            Total = Total + tbGood.amtTwo
                            GVStkArDet.SetFocusedRowCellValue("Amount", tbGood.amtTwo)
                        End If
                    End If
                End If
                GVStkArDet.SetFocusedRowCellValue("total", Total)
            Else
                GVStkArDet.SetFocusedRowCellValue("total", 0)
            End If
        End If
    End Sub
    Private Function CheckEditDel(ByVal prd As Integer, ByVal trk As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(Me.DateGood.Text, DateTime)
        CheckEditDel = False
        Dim lst = (From s In db.V_gdShps Where s.goodDate >= TheDate.ToShortDateString And s.trkProd = prd _
                                             And s.trkExpLok = Val(LokExp.EditValue) _
                                                  And s.trkGoodDet = trk
                   Select s).ToList()
        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDel = True
    End Function
End Class