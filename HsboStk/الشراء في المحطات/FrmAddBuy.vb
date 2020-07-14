Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmAddBuy
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
    Dim SaveView As Boolean = False
#Region "Repository Variables"
    Public repCrop As New Repository.RepositoryItemLookUpEdit
    Public repUnit As Repository.RepositoryItemLookUpEdit
    Public repClient As New Repository.RepositoryItemLookUpEdit
#End Region

    Private Sub FrmAddBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        Dim tb = (From s In db.buys Where s.trkBuyClient = ID And s.delBuy = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokLoc.EditValue = tb.trkBuyLoc
        Me.LokStore.EditValue = tb.trkBStore
        Me.Datebuy.Text = tb.buyDate
        buyInf.Text = tb.buyInfo
        Dim TbUn As New V_cropUnit
        '*******************************
        Dim lst = (From s In db.buyDetails Where s.trkBuyClient = ID And s.delBDet = 0 Select s).ToList

        While i < lst.Count
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVBuyDet.AddNewRow()
            TbUn = (From s In db.V_cropUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkCrop = lst.Item(i).trkCrop Select s).SingleOrDefault


            GVBuyDet.SetFocusedRowCellValue("trkDet", lst.Item(i).trkBuyDet)
            GVBuyDet.SetFocusedRowCellValue("trkCrop", lst.Item(i).trkCrop)
            GVBuyDet.SetFocusedRowCellValue("Amount", lst.Item(i).storeAmount)
            GVBuyDet.SetFocusedRowCellValue("Price", lst.Item(i).cropPrice)
            GVBuyDet.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVBuyDet.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVBuyDet.SetFocusedRowCellValue("trkClient", lst.Item(i).trkClient)
            GVBuyDet.UpdateCurrentRow()
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

    Public Sub FillCrop()
        done = False
        Dim crp = (From s In db.crops Where s.delCrop = False
                   Select s).ToList
        repCrop.DataSource = crp
        repCrop.ValueMember = "trkCrop"
        repCrop.DisplayMember = "cropName"
        repCrop.ShowHeader = False
        repCrop.PopulateColumns()
        repCrop.Columns(0).Visible = False
        repCrop.Columns(2).Visible = False
        repCrop.NullText = ""
        done = True
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        GVBuyDet.OptionsFind.FindFilterColumns = ""
        GVBuyDet.OptionsFind.AlwaysVisible = False

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
                GVBuyDet.AddNewRow()
            End If
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVBuyDet.OptionsFind.AlwaysVisible = False
        If CanSaveReq() = True Then
            If Trim(TxtRId.Text) = "" Then
                MemAdd = True
            End If
            If MemAdd Or MemEdit Then
                SaveReqData()
                'MemEdit = False
                'MemAdd = False
                'MemInf = False
            End If
            'If IsDet = False Then
            '    IsDet = True
            '    Exit Sub
            'End If
        End If
        MemEdit = False
        MemAdd = False
        MemInf = False
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
        If Datebuy.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Datebuy.Focus()
            Exit Function
        End If
        If CType(Datebuy.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Datebuy.Focus()
            Exit Function
        End If
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المنطقة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If
        If LokStore.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المخزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokStore.Focus()
            LokStore.SelectAll()
            Exit Function
        End If

        If MemEdit = True And IsView = True Then

            Dim tb As New buy
            Dim tbShp As New V_shipDetail
            Dim i As Integer = 0
            tb = (From s In db.buys Where s.trkBuyClient = Val(TxtRId.Text) Select s).SingleOrDefault
            While i < GVBuyDet.RowCount

                Dim lst = (From s In db.V_shipDetails Where s.trkBStore = tb.trkBStore _
                                                      And s.trkBuyLoc = tb.trkBuyLoc _
                                                    And s.shipDate >= tb.buyDate _
                                                      And s.trkCrop = Val(GVBuyDet.GetRowCellValue(i, "trkCrop"))
                           Select s).ToList
                If lst.Count > 0 Then
                    XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بالشحن من المخزن في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                    Exit Function
                End If
                i = i + 1
            End While
        End If

        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New buy
        Dim theDate As DateTime
        theDate = CType(Datebuy.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkBuyClient = trk
            tb.trkBuyLoc = Val(LokLoc.EditValue.ToString())
            tb.buyDate = theDate.ToShortDateString
            tb.trkBStore = Val(LokStore.EditValue.ToString())
            tb.buyInfo = buyInf.Text
            tb.trkPrs = 1
            tb.delBuy = False
            db.buys.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.buys Where s.trkBuyClient = Val(TxtRId.Text) And s.delBuy = False Select s).Single()
            tb.trkBuyLoc = Val(LokLoc.EditValue.ToString())
            tb.buyDate = theDate.ToShortDateString
            tb.trkBStore = Val(LokStore.EditValue.ToString())
            tb.trkPrs = 1
            tb.delBuy = False
            tb.buyInfo = buyInf.Text
            trk = Val(TxtRId.Text)
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
        Dim Qrymax = (From trk In db.buys Select trk.trkBuyClient).ToList
        If Qrymax.Count = 0 Then
            TheKey = 1
        Else
            TheKey = Qrymax.Max() + 1
        End If
        Return TheKey
    End Function
    Private Function CanSave(ByVal Ind As Integer) As Boolean
        CanSave = False
        If Val(GVBuyDet.GetRowCellValue(Ind, "trkCrop")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVBuyDet.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVBuyDet.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVBuyDet.SelectCell(Ind, col3)
            Exit Function
        End If
        If Val(GVBuyDet.GetRowCellValue(Ind, "Price")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال سعر الشراء ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVBuyDet.SelectCell(Ind, col6)
            Exit Function
        End If
        If (GVBuyDet.GetRowCellValue(Ind, "trkUnit")) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVBuyDet.SelectCell(Ind, col5)
            Exit Function
        End If
        If Val(GVBuyDet.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVBuyDet.SelectCell(Ind, col4)
            Exit Function
        End If
        If Val(GVBuyDet.GetRowCellValue(Ind, "trkClient")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم العميل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVBuyDet.SelectCell(Ind, col2)
            Exit Function
        End If
        If IsSingleRow(Ind) = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً لقد قمت بادخال المحصول من نفس العميل مسبقا  يمكنك التعديل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CanSave = True
    End Function
    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVBuyDet.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Crop As Integer = Val(GVBuyDet.GetRowCellValue(Ind, "trkCrop"))
        Dim Client As Integer = Val(GVBuyDet.GetRowCellValue(Ind, "trkClient"))
        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVBuyDet.GetRowCellValue(i, "trkCrop")) = Crop And Val(GVBuyDet.GetRowCellValue(i, "trkClient")) = Client Then
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
        Dim Qrymax = (From trk In db.buyDetails Select trk.trkBuyDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()

        Dim tb As New buyDetail
        Dim TbUn As New V_cropUnit
        If MemAddDet = True Then
            GVBuyDet.SetRowCellValue(RowInd, "trkDet", NewKey())
            tb.trkBuyDet = Val(GVBuyDet.GetRowCellValue(RowInd, "trkDet"))
            tb.trkBuyClient = Val(TxtRId.Text)
            tb.cropPrice = Val(GVBuyDet.GetRowCellValue(RowInd, "Price"))
            TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVBuyDet.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkCrop = Val(GVBuyDet.GetRowCellValue(RowInd, "trkCrop")) Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVBuyDet.GetRowCellValue(RowInd, "Weight"))
            tb.trkClient = Val(GVBuyDet.GetRowCellValue(RowInd, "trkClient"))
            tb.delBDet = False

            tb.trkCrop = Val(GVBuyDet.GetRowCellValue(RowInd, "trkCrop"))
            tb.storeAmount = Val(GVBuyDet.GetRowCellValue(RowInd, "Amount"))
            '***************************************
            CalculateUnit(TbUn.trkUnit, Val(GVBuyDet.GetRowCellValue(RowInd, "Amount")),
                    Val(GVBuyDet.GetRowCellValue(RowInd, "trkCrop")))
            '***************************************
            tb.untOne = UOne
            tb.amtOne = AOne
            tb.untTwo = UTwo
            tb.amtTwo = ATwo
            db.buyDetails.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()

        End If
    End Sub

    Public Sub SaveEdit()

        Dim i As Integer = 0
        Dim tb As New buyDetail
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        Dim TbUn As New V_cropUnit
        If GVBuyDet.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVBuyDet.RowCount

                If (GVBuyDet.IsRowSelected(i) = True) Then
                    TheCrp = Val(GVBuyDet.GetRowCellValue(i, "trkCrop"))
                    TheTrk = Val(GVBuyDet.GetRowCellValue(i, "trkDet"))
                    If CheckEditDel(TheCrp) = True Then
                        If CanSave(i) = True Then

                            tb = (From s In db.buyDetails Where s.trkBuyDet = Val(TheTrk) And s.trkBuyClient = Val(TxtRId.Text) Select s).Single()
                            tb.trkBuyClient = Val(TxtRId.Text)
                            tb.cropPrice = Val(GVBuyDet.GetRowCellValue(i, "Price"))
                            '  tb.trkUnit = Val(GVBuyDet.GetRowCellValue(i, "trkUnit"))
                            TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVBuyDet.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkCrop = Val(GVBuyDet.GetRowCellValue(i, "trkCrop")) Select s).SingleOrDefault
                            tb.trkUnit = TbUn.trkUnit
                            tb.weight = Val(GVBuyDet.GetRowCellValue(i, "Weight"))
                            tb.trkClient = Val(GVBuyDet.GetRowCellValue(i, "trkClient"))
                            tb.delBDet = False

                            tb.trkCrop = Val(GVBuyDet.GetRowCellValue(i, "trkCrop"))
                            tb.storeAmount = Val(GVBuyDet.GetRowCellValue(i, "Amount"))
                            CalculateUnit(TbUn.trkUnit, Val(GVBuyDet.GetRowCellValue(i, "Amount")),
                            Val(GVBuyDet.GetRowCellValue(i, "trkCrop")))

                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                            GVBuyDet.UnselectRow(i)
                        End If
                    End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVBuyDet.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub
    Private Function CheckEditDel(ByVal crp As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(Datebuy.Text, DateTime)
        CheckEditDel = False
        Dim lst = (From s In db.V_shipDetails Where s.shipDate >= TheDate.ToShortDateString And s.trkCrop = crp _
                                             And s.trkBuyLoc = Val(LokLoc.EditValue) And s.trkBStore = Val(LokStore.EditValue)
                   Select s).ToList()
        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع الشحن في المحطات ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDel = True
    End Function


    Public Sub FillClient()
        Dim cl = (From s In db.clients Where s.delClient = False
                  Select s).ToList
        repClient.DataSource = cl
        repClient.ValueMember = "trkClient"
        repClient.DisplayMember = "clientName"
        repClient.ShowHeader = False
        repClient.PopulateColumns()
        repClient.Columns(0).Visible = False
        repClient.Columns(2).Visible = False
        repClient.Columns(3).Visible = False
        repClient.Columns(4).Visible = False
        repClient.Columns(5).Visible = False
        repClient.NullText = ""
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub

    Public Function CountDet() As Integer
        Dim lst = (From s In db.V_buyDetails Where s.trkBuyClient = Val(TxtRId.Text) Select s).ToList
        Return lst.Count()
    End Function
    Public Sub FillGrid()
        FillCrop()
        FillClient()
        'GridControl1.RepositoryItems.Add(repUnit)
        GridControl1.RepositoryItems.Add(repCrop)
        GridControl1.RepositoryItems.Add(repTxt)
        GridControl1.RepositoryItems.Add(repClient)

        Dim list As BindingList(Of buyDet) = New BindingList(Of buyDet)
        GridControl1.DataSource = list
        GVBuyDet.Columns(0).ColumnEdit = repTxt
        GVBuyDet.Columns(0).Visible = False
        GVBuyDet.Columns(1).ColumnEdit = repCrop
        GVBuyDet.Columns(2).ColumnEdit = repTxt
        GVBuyDet.Columns(3).ColumnEdit = repTxt
        'GVBuyDet.Columns(4).ColumnEdit = repUnit
        GVBuyDet.Columns(5).ColumnEdit = repTxt
        GVBuyDet.Columns(6).ColumnEdit = repClient
        ' GVBuyDet.Columns(7).ColumnEdit = RepositoryItemCheckEdit1
        GVBuyDet.OptionsSelection.MultiSelect = True
        GVBuyDet.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub
    Private Sub FormatColumns()
        col1 = GVBuyDet.Columns(1)
        col2 = GVBuyDet.Columns(2)
        col3 = GVBuyDet.Columns(3)
        col4 = GVBuyDet.Columns(4)
        col5 = GVBuyDet.Columns(5)
        col6 = GVBuyDet.Columns(6)
        '****************
        col1.Caption = "المحصول"
        col2.Caption = "الكمية "
        col3.Caption = "سعر وحدة الشراء"
        col4.Caption = "الوحدة"

        col5.Caption = "الوزن"
        col6.Caption = " اسم العميل"
        GVBuyDet.Columns(1).Width = 40
        GVBuyDet.Columns(2).Width = 30
        GVBuyDet.Columns(3).Width = 30
        GVBuyDet.Columns(4).Width = 30
        GVBuyDet.Columns(5).Width = 30

    End Sub
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVBuyDet.OptionsFind.AlwaysVisible = False
        Dim tb As New buyDetail
        Dim i As Integer = 0
        Dim lastRow As Integer = GVBuyDet.RowCount - 1
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        Dim lastValue As Integer = Val(GVBuyDet.GetRowCellValue(lastRow, "trkDet"))
        If lastValue = 0 And GVBuyDet.IsRowSelected(lastRow) = True Then
            GVBuyDet.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
            MemAddDet = False
        End If
        If GVBuyDet.SelectedRowsCount <> 0 Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVBuyDet.RowCount

                    If GVBuyDet.IsRowSelected(i) = True Then
                        TheCrp = Val(GVBuyDet.GetRowCellValue(i, "trkCrop"))
                        TheTrk = Val(GVBuyDet.GetRowCellValue(i, "trkDet"))
                        If CheckEditDel(TheCrp) = True Then
                            Dim TheReq As Integer
                            TheReq = Val(GVBuyDet.GetRowCellValue(i, "trkDet"))
                            tb = (From s In db.buyDetails Where s.trkBuyDet = Val(TheTrk) And s.trkBuyClient = Val(TxtRId.Text) Select s).Single()
                            tb.delBDet = True
                            GVBuyDet.DeleteRow(i)
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
        If GVBuyDet.SelectedRowsCount <> 0 Then
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
        If GVBuyDet.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVBuyDet.OptionsFind.FindFilterColumns = "*"
            GVBuyDet.ShowFindPanel()
            GVBuyDet.OptionsFind.ShowClearButton = False
            GVBuyDet.OptionsFind.ShowFindButton = False
        End If
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewBuy

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
        rpt.FilterString = " [trkBuyClient] =" & Val(TxtRId.Text)
        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()

    End Sub

    Private Sub BtnMeClose_Click_1(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVBuyDet.SelectedRowsCount <> 0 Then
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


    '*****************this to edit saved data
    Private Sub LokStore_EditValueChanged(sender As Object, e As EventArgs) Handles LokStore.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub Datebuy_EditValueChanged(sender As Object, e As EventArgs) Handles Datebuy.EditValueChanged
        If Saved = True Then
            MemEdit = True
        End If
    End Sub
    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub
    Public Function SaveInclose() As Boolean
        If Row <> 0 Then
            If Val(GVBuyDet.GetRowCellValue(GVBuyDet.RowCount - 1, "trkDet")) = 0 Then
                'If tb.trkBuyDet = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVBuyDet.RowCount - 1
                    GVBuyDet.FocusedRowHandle = lastRow
                    GVBuyDet.DeleteRow(lastRow)
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

    Private Sub buyInf_TextChanged(sender As Object, e As EventArgs) Handles buyInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokStore.Text = ""
        FillStore()
        If saved = True Then
            MemEdit = True
        End If
    End Sub


    Private Sub GVBuyDet_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVBuyDet.CustomRowCellEditForEditing
        Dim crp As Integer
        If e.Column.Caption = "المحصول" Then
            GVBuyDet.SetFocusedRowCellValue("trkUnit", "")
        End If
        If e.Column.Caption = "الوحدة" Then
            crp = GVBuyDet.GetRowCellValue(e.RowHandle, "trkCrop")
            If crp <> 0 Then
                FillUnit(crp)
                e.RepositoryItem = repUnit
            End If
        End If

    End Sub
    Public Sub FillUnit(ByVal crp As Integer)

        Dim un = (From s In db.V_cropUnits Where s.delCU = False And s.trkCrop = crp
                  Select s Order By s.trkUnit).ToList
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
        repUnit.Columns(4).Visible = False
        repUnit.Columns(5).Visible = False
        repUnit.Columns(6).Visible = False
        repUnit.Columns(7).Visible = False
        repUnit.NullText = ""
        '  rep.Add(repUnit)
    End Sub

End Class