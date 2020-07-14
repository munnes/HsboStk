
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmStoreProd
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5 As Columns.GridColumn
#Region "Repository Variables"
    Public repPrd As New Repository.RepositoryItemLookUpEdit
    Public repUnit As Repository.RepositoryItemLookUpEdit
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
        Dim tb = (From s In db.expStocks Where s.trkExpStk = ID And s.delExpStk = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokExp.EditValue = tb.trkExpLok
        Me.LokStore.EditValue = tb.trkExpStore
        Me.DateStrPrd.Text = tb.exStkDate
        TxtStrPrdInf.Text = tb.expStkInf
        Dim CurTotal As Double = 0
        Dim TbUn As New V_prdUnit
        '*******************************
        Dim lst = (From s In db.expStockDets Where s.trkExpStk = ID And s.delExStkDet = 0 Select s).ToList
        While i < CountView
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVStkArDet.AddNewRow()
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
            '$$$$$$$$$$$$$$$$$$$$$$
            CurTotal = CalTotal(lst.Item(i).trkProd, j) + lst.Item(i).aStock
            '**********************
            GVStkArDet.SetFocusedRowCellValue("trkExpStkDet", lst.Item(i).trkExpStkDet)
            GVStkArDet.SetFocusedRowCellValue("trkProd", lst.Item(i).trkProd)
            GVStkArDet.SetFocusedRowCellValue("total", CurTotal)
            GVStkArDet.SetFocusedRowCellValue("Amount", lst.Item(i).aStock)
            GVStkArDet.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVStkArDet.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
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
    Private Sub FillStore()
        If done = True And LokExp.Text <> "" Then
            LokStore.Properties.DataSource = ""
            Dim lst = (From c In db.exportStores Where c.delSExp = False And c.trkExpLoc = Val(LokExp.EditValue.ToString()) Select c).ToList
            LokStore.Properties.DataSource = lst
            LokStore.Properties.DisplayMember = "expStore"
            LokStore.Properties.ValueMember = "trkExpStore"
            LokStore.Properties.PopulateColumns()
            LokStore.Properties.Columns(0).Visible = False
            LokStore.Properties.Columns(2).Visible = False
            LokStore.Properties.Columns(3).Visible = False
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
    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub
    Private Function CanSaveReq() As Boolean
        CanSaveReq = False
        If DateStrPrd.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateStrPrd.Focus()
            Exit Function
        End If
        If CType(DateStrPrd.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateStrPrd.Focus()
            Exit Function
        End If
        If LokExp.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم منطقة الصادر ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokExp.Focus()
            LokExp.SelectAll()
            Exit Function
        End If
        If LokStore.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المخزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokStore.Focus()
            LokStore.SelectAll()
            Exit Function
        End If
        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New expStock
        Dim theDate As DateTime
        theDate = CType(DateStrPrd.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkExpStk = trk
            tb.trkExpLok = Val(LokExp.EditValue.ToString())
            tb.exStkDate = theDate.ToShortDateString
            tb.trkExpStore = Val(LokStore.EditValue.ToString())
            tb.delExpStk = False
            tb.trkPrs = 1
            tb.expStkInf = TxtStrPrdInf.Text
            db.expStocks.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.expStocks Where s.trkExpStk = Val(TxtRId.Text) And s.delExpStk = False Select s).Single()
            tb.trkExpLok = Val(LokExp.EditValue.ToString())
            tb.exStkDate = theDate.ToShortDateString
            tb.trkExpStore = Val(LokStore.EditValue.ToString())
            tb.expStkInf = TxtStrPrdInf.Text
            tb.trkPrs = 1
            tb.delExpStk = False
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
        Dim Qrymax = (From trk In db.expStocks Select trk.trkExpStk).ToList
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
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الكمية أكثر من المتوفر في منطقة الصادر ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
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
            TheDate = CType(Me.DateStrPrd.Text, DateTime)
            Dim lst = (From s In db.V_ExpStkDets Where s.exStkDate > TheDate.ToShortDateString And s.trkProd = Val(GVStkArDet.GetRowCellValue(Ind, "trkProd")) _
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
        Dim Qrymax = (From trk In db.expStockDets Select trk.trkExpStkDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()
        Dim tb As New expStockDet
        Dim TbUn As New V_prdUnit
        If MemAddDet = True Then
            GVStkArDet.SetRowCellValue(RowInd, "trkExpStkDet", NewKey())
            tb.trkExpStkDet = Val(GVStkArDet.GetRowCellValue(RowInd, "trkExpStkDet"))
            tb.trkExpStk = Val(TxtRId.Text)
            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVStkArDet.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkProd = Val(GVStkArDet.GetRowCellValue(RowInd, "trkProd")) Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVStkArDet.GetRowCellValue(RowInd, "Weight"))
            tb.delExStkDet = False
            tb.trkProd = Val(GVStkArDet.GetRowCellValue(RowInd, "trkProd"))
            tb.aStock = Val(GVStkArDet.GetRowCellValue(RowInd, "Amount"))
            '***************************************
            CalculatePrdUnit(TbUn.trkUnit, Val(GVStkArDet.GetRowCellValue(RowInd, "Amount")),
                    Val(GVStkArDet.GetRowCellValue(RowInd, "trkProd")))
            '***************************************
            tb.untOne = UOne
            tb.amtOne = AOne
            tb.untTwo = UTwo
            tb.amtTwo = ATwo
            db.expStockDets.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New expStockDet
        Dim ThePrd As Integer
        Dim TbUn As New V_prdUnit
        If GVStkArDet.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVStkArDet.RowCount
                Dim TheReq As Integer
                TheReq = Val(GVStkArDet.GetRowCellValue(i, "trkExpStkDet"))
                If (GVStkArDet.IsRowSelected(i) = True) Then
                    ThePrd = Val(GVStkArDet.GetRowCellValue(i, "trkProd"))
                    If CheckEditDel(ThePrd) = True Then
                        If CanSave(i) = True Then
                            tb = (From s In db.expStockDets Where s.trkExpStkDet = Val(TheReq) And s.trkExpStk = Val(TxtRId.Text) Select s).Single()
                            tb.trkExpStk = Val(TxtRId.Text)
                            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVStkArDet.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkProd = Val(GVStkArDet.GetRowCellValue(i, "trkProd")) Select s).SingleOrDefault
                            tb.trkUnit = TbUn.trkUnit
                            tb.weight = Val(GVStkArDet.GetRowCellValue(i, "Weight"))
                            tb.delExStkDet = False
                            tb.trkProd = Val(GVStkArDet.GetRowCellValue(i, "trkProd"))
                            tb.aStock = Val(GVStkArDet.GetRowCellValue(i, "Amount"))
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
        '  FillUnit()
        ' GridControl1.RepositoryItems.Add(repUnit)
        GridControl1.RepositoryItems.Add(repPrd)
        GridControl1.RepositoryItems.Add(repTxt)
        Dim list As BindingList(Of expStDet) = New BindingList(Of expStDet)
        GridControl1.DataSource = list
        GVStkArDet.Columns(0).ColumnEdit = repTxt
        GVStkArDet.Columns(0).Visible = False
        GVStkArDet.Columns(1).ColumnEdit = repPrd
        GVStkArDet.Columns(2).ColumnEdit = repTxt
        ' GVStkArDet.Columns(3).ColumnEdit = repUnit
        GVStkArDet.Columns(4).ColumnEdit = repTxt
        GVStkArDet.OptionsSelection.MultiSelect = True
        GVStkArDet.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub
    Private Sub FormatColumns()
        col1 = GVStkArDet.Columns(1)
        col2 = GVStkArDet.Columns(2)
        col3 = GVStkArDet.Columns(3)
        col4 = GVStkArDet.Columns(4)
        col5 = GVStkArDet.Columns(5)
        '****************
        col1.Caption = "المنتج"
        col2.Caption = "المتوفر في المحطة"
        col3.Caption = "الكمية "
        col4.Caption = "الوحدة"
        col5.Caption = "الوزن"
        GVStkArDet.Columns(2).OptionsColumn.ReadOnly = True
        GVStkArDet.Columns(2).AppearanceCell.BackColor = Color.DarkRed
        GVStkArDet.Columns(2).AppearanceCell.ForeColor = Color.White
        GVStkArDet.Columns(2).AppearanceCell.FontSizeDelta = Font.Bold
    End Sub
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVStkArDet.OptionsFind.AlwaysVisible = False
        Dim tb As New expStockDet
        Dim i As Integer = 0
        Dim ThePrd As Integer
        Dim lastRow As Integer = GVStkArDet.RowCount - 1
        Dim lastValue As Integer = Val(GVStkArDet.GetRowCellValue(lastRow, "trkExpStkDet"))
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
                    TheReq = Val(GVStkArDet.GetRowCellValue(i, "trkExpStkDet"))
                    If GVStkArDet.IsRowSelected(i) = True Then
                        ThePrd = Val(GVStkArDet.GetRowCellValue(i, "trkProd"))
                        If CheckEditDel(ThePrd) = True Then
                            tb = (From s In db.expStockDets Where s.trkExpStkDet = Val(TheReq) And s.trkExpStk = Val(TxtRId.Text) Select s).Single()
                            tb.delExStkDet = True
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

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs)

    End Sub

    '*****************this to edit saved data
    Private Sub LokStore_EditValueChanged(sender As Object, e As EventArgs) Handles LokStore.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub


    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokExp.TextChanged
        LokStore.Text = ""
        FillStore()
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
    Private Sub Datebuy_EditValueChanged(sender As Object, e As EventArgs) Handles DateStrPrd.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub BtnPrint_Click_1(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewStrPrd

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
        rpt.FilterString = " [trkExpStk] =" & Val(TxtRId.Text)
        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub

    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub
    Public Function SaveInclose() As Boolean
        If Row <> 0 Then
            If Val(GVStkArDet.GetRowCellValue(GVStkArDet.RowCount - 1, "trkExpStkDet")) = 0 Then
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

    Private Sub TxtStrPrdInf_TextChanged(sender As Object, e As EventArgs) Handles TxtStrPrdInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub
    Private Function CheckEditDel(ByVal prd As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(Me.DateStrPrd.Text, DateTime)
        CheckEditDel = False
        Dim lst = (From s In db.V_gdShps Where s.goodDate >= TheDate.ToShortDateString And s.trkProd = prd _
                                             And s.trkExpLok = Val(LokExp.EditValue)
                   Select s).ToList()
        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع التستيف في البواخر ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDel = True
    End Function

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

    Private Function CalTotal(ByVal prd As Integer, ByVal UInd As Integer) As Double
        Dim TheDate As DateTime
        Dim TotalStk As Double
        If Me.DateStrPrd.Text <> "" Then
            TheDate = CType(DateStrPrd.Text, DateTime)
        End If
        Dim tb As New PrdStationResult
        tb = (From s In db.PrdStation(Val(LokExp.EditValue), prd, TheDate) Select s).SingleOrDefault

        If Not IsNothing(tb) Then
            If UInd = 0 Then
                CalTotal = tb.oneUnt
            Else
                CalTotal = tb.twoUnt

            End If
        End If
        CalTotal = Math.Round(CalTotal, 2)

    End Function
    Private Sub TheStk()

        Dim count As Integer = GVStkArDet.RowCount
        Dim i As Integer = 0
        Dim UndPreStk As Double = 0
        Dim tbArvStr As New V_ExpStkDet
        Dim Total As Double
        Dim StrUn As String
        Dim curProd As Integer
        While i < count
            Dim TbUn As New V_cropUnit
            Dim j As Integer = 0
            StrUn = Trim(GVStkArDet.GetRowCellValue(i, "trkUnit"))
            curProd = Val(GVStkArDet.GetRowCellValue(i, "trkProd"))
            '$$$$$$$$$$$$$$$$$$$$$$$$
            If StrUn <> "" And curProd <> 0 Then
                Dim lst = (From s In db.V_prdUnits Where s.trkCrop = curProd Select s).ToList
                If lst.Count = 2 Then
                    While j < 2
                        If lst.Item(j).unitName = StrUn Then
                            Exit While
                        End If
                        j = j + 1
                    End While
                End If

                If Val(GVStkArDet.GetRowCellValue(i, "trkExpStkDet")) <> 0 Then
                    tbArvStr = (From s In db.V_ExpStkDets Where s.trkExpStkDet = Val(GVStkArDet.GetRowCellValue(i, "trkExpStkDet")) _
                                                          And s.trkProd = curProd And s.trkExpLok = Val(LokExp.EditValue) _
                                    And s.trkExpStore = Val(LokStore.EditValue)
                                Select s).SingleOrDefault()
                    If Not IsNothing(tbArvStr) Then
                        UndPreStk = tbArvStr.aStock
                    Else
                        UndPreStk = 0
                    End If
                End If

                Total = CalTotal(curProd, j) + UndPreStk
                GVStkArDet.SetRowCellValue(i, "total", Total)
            End If
            GVStkArDet.SelectRow(i)
            If i = count - 1 Then
                If Val(GVStkArDet.GetRowCellValue(count - 1, "trkExpStkDet")) = 0 Then
                    GVStkArDet.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub

    Private Sub GVStkArDet_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GVStkArDet.CellValueChanged
        If GVStkArDet.Columns(2).Visible = True Then
            Dim prd As Integer = GVStkArDet.GetRowCellValue(e.RowHandle, "trkProd")
            Dim StrUn As String = Trim(GVStkArDet.GetFocusedRowCellValue("trkUnit"))
            Dim i As Integer = 0
            Dim TheId As Integer = GVStkArDet.GetRowCellValue(e.RowHandle, "trkExpStkDet")
            Dim Amt As Double = GVStkArDet.GetRowCellValue(e.RowHandle, "Amount")
            Dim tb As New expStock
            Dim tbExpStk As expStockDet
            Dim Total As Double
            '  Dim UndPreStk As Double = 0
            '  Dim tbArvStr As New V_ArvStr
            If e.Column.Caption = "الوحدة" Then
                If StrUn <> "" And prd <> 0 Then
                    Dim lst = (From s In db.V_prdUnits Where s.trkProd = prd Select s).ToList
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
                        tbExpStk = (From s In db.expStockDets Where s.trkExpStkDet = Val(TxtRId.Text) Select s).SingleOrDefault
                        If i = 0 Then
                            Total = Total + tbExpStk.amtOne
                            GVStkArDet.SetFocusedRowCellValue("Amount", tbExpStk.amtOne)
                        Else
                            Total = Total + tbExpStk.amtTwo
                            GVStkArDet.SetFocusedRowCellValue("Amount", tbExpStk.amtTwo)
                        End If

                    End If

                    GVStkArDet.SetFocusedRowCellValue("total", Total)
                Else
                    GVStkArDet.SetFocusedRowCellValue("total", 0)
                End If
            End If
        End If
    End Sub
End Class