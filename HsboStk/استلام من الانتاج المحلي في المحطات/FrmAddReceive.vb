
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmAddReceive
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
#Region "Repository Variables"
    Public repPrd As New Repository.RepositoryItemLookUpEdit
    Public repUnit As Repository.RepositoryItemLookUpEdit
#End Region

    Private Sub FrmAddReceive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        'RdoLocal.Checked = True
        'LokClient.Enabled = False
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
        Dim tb = (From s In db.receives Where s.trkRecv = ID And s.delRecv = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokLoc.EditValue = tb.trkArival
        Me.LokStore.EditValue = tb.trkAPrdStr
        LokPeeler.EditValue = tb.trkPeeler
        Me.DateRecv.Text = tb.recvDate
        TxtRecvInfo.Text = tb.recvInf
        'If tb.isLocal = False Then
        '    RdoClient.Checked = True
        '    LokClient.EditValue = tb.trkClntCrp
        '    LokClient.Enabled = True
        'Else
        '    RdoLocal.Checked = True
        '    LokClient.Enabled = False
        'End If
        Dim TbUn As New V_prdUnit
        '*******************************
        Dim lst = (From s In db.receiveDets Where s.trkRecv = ID And s.delRecDet = 0 Select s).ToList
        While i < CountView
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVRecvDet.AddNewRow()
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
            Dim CurTotal As Double = CalTotal(lst.Item(i).trkProd, j) + lst.Item(i).amount

            '**********************
            GVRecvDet.SetFocusedRowCellValue("trkRecDet", lst.Item(i).trkRecvDet)
            GVRecvDet.SetFocusedRowCellValue("trkPrd", lst.Item(i).trkProd)
            GVRecvDet.SetFocusedRowCellValue("total", CurTotal)
          
            GVRecvDet.SetFocusedRowCellValue("Amount", lst.Item(i).amount)
            GVRecvDet.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVRecvDet.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVRecvDet.UpdateCurrentRow()

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
    Private Sub FillStore()
        If done = True And LokLoc.Text <> "" Then
            LokStore.Properties.DataSource = ""
            Dim lst = (From c In db.arivalPrdStores Where c.delAPrd = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokStore.Properties.DataSource = lst
            LokStore.Properties.DisplayMember = "APrdStr"
            LokStore.Properties.ValueMember = "trkAPrdStr"
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
        GVRecvDet.OptionsFind.FindFilterColumns = ""
        GVRecvDet.OptionsFind.AlwaysVisible = False

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
                GVRecvDet.AddNewRow()
            End If
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVRecvDet.OptionsFind.AlwaysVisible = False
        If CanSaveReq() = True Then

            If Trim(TxtRId.Text) = "" Then
                MemAdd = True
            End If
            If MemAdd Or MemEdit Then
                SaveReqData()

            End If
            'If IsDet = False Then
            '    IsDet = True
            '    Exit Sub
            'End If
        End If
        MemEdit = False
        MemAdd = False
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
        If DateRecv.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateRecv.Focus()
            Exit Function
        End If
        If CType(DateRecv.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateRecv.Focus()
            Exit Function
        End If
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحطة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If
        If LokPeeler.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم القشارة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokPeeler.Focus()
            LokPeeler.SelectAll()
            Exit Function
        End If
        If LokStore.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المخزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokStore.Focus()
            LokStore.SelectAll()
            Exit Function
        End If
        If MemEdit And IsView Then
            Dim tb As New receive
            Dim i As Integer = 0
            tb = (From s In db.receives Where s.trkRecv = Val(TxtRId.Text) Select s).SingleOrDefault
            While i < GVRecvDet.RowCount
                Dim lst = (From s In db.V_expShipDets Where s.trkArival = tb.trkArival _
                                                        And s.expShipDate >= tb.recvDate _
                                                          And s.trkAPrdStr = tb.trkAPrdStr _
                                                          And s.trkProd = Val(GVRecvDet.GetRowCellValue(i, "trkPrd"))
                           Select s).ToList
                If lst.Count > 0 Then
                    XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بشحن منتجات من نفس المخزن في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                    Exit Function
                End If
                i = i + 1
            End While
        End If
        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New receive
        Dim theDate As DateTime
        theDate = CType(DateRecv.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkRecv = trk
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.recvDate = theDate.ToShortDateString
            tb.trkAPrdStr = Val(LokStore.EditValue.ToString())
            tb.trkPeeler = Val(LokPeeler.EditValue.ToString)
            tb.isLocal = 1
            tb.trkClntCrp = 0
            'If RdoClient.Checked = True Then
            '    tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            'Else
            '    tb.trkClntCrp = 0
            'End If
            tb.trkPrs = 1
            tb.recvInf = TxtRecvInfo.Text
            tb.delRecv = False
            db.receives.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.receives Where s.trkRecv = Val(TxtRId.Text) And s.delRecv = False Select s).Single()
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.recvDate = theDate.ToShortDateString
            tb.trkAPrdStr = Val(LokStore.EditValue.ToString())
            tb.trkPeeler = Val(LokPeeler.EditValue.ToString)
            tb.isLocal = 1
            tb.trkClntCrp = 0
            tb.trkPrs = 1
            'If RdoClient.Checked = True Then
            '    tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            'Else
            '    tb.trkClntCrp = 0
            'End If
            tb.recvInf = TxtRecvInfo.Text
            tb.delRecv = False
            db.SubmitChanges()
            Progress()
        End If

        TxtRId.Text = trk
    End Sub
    'Public Function CalFlag() As Boolean
    '    If RdoLocal.Checked = True Then
    '        CalFlag = True
    '    ElseIf RdoClient.Checked = True
    '        CalFlag = False
    '    End If
    'End Function
    Private Sub Progress()
        ProgressBarControl1.Visible = True
        Me.ProgressBarControl1.EditValue = 0
        Me.Timer1.Start()
    End Sub
    Function NewReqKey() As Integer
        Dim TheKey As Integer
        Dim Qrymax = (From trk In db.receives Select trk.trkRecv).ToList
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
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً لقد قمت بادخال المنتج مسبقا  يمكنك التعديل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        If Val(GVRecvDet.GetRowCellValue(Ind, "trkPrd")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVRecvDet.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVRecvDet.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVRecvDet.SelectCell(Ind, col3)
            Exit Function
        End If
        If Val(GVRecvDet.GetRowCellValue(Ind, "Amount")) > Val(GVRecvDet.GetRowCellValue(Ind, "total")) Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الكمية أكثر من المخزون ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVRecvDet.SelectCell(Ind, col3)
            Exit Function
        End If

        If (GVRecvDet.GetRowCellValue(Ind, "trkUnit")) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVRecvDet.SelectCell(Ind, col5)
            Exit Function
        End If
        If Val(GVRecvDet.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVRecvDet.SelectCell(Ind, col4)
            Exit Function
        End If
        Dim TheDate As DateTime
        TheDate = CType(Me.DateRecv.Text, DateTime)
        'Dim isLoc As Boolean
        'Dim Clnt As Integer
        'If RdoLocal.Checked = True Then
        '    isLoc = 1
        '    Clnt = 0
        'ElseIf RdoClient.Checked = True
        '    isLoc = 0
        '    Clnt = Val(LokClient.EditValue)
        'End If
        If MemAddDet Then
            Dim countLst As Integer
            Dim lst = (From s In db.V_RecvDets Where s.recvDate > TheDate And s.trkProd = Val(GVRecvDet.GetRowCellValue(Ind, "trkPrd")) _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                And s.isLocal = True And s.trkClntCrp = 0
                       Select s).ToList()
            countLst = lst.Count
            If countLst > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن صرف المنتج ...تمت تخزين نفس المنتج في وقت لاحق ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If

            Dim lstSale = (From s In db.V_SaleDets Where s.saleDate > TheDate And s.trkProd = Val(GVRecvDet.GetRowCellValue(Ind, "trkPrd")) _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s).ToList()

            countLst = lstSale.Count
            If countLst > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن صرف المنتج ...تمت صرف نفس المنتج محليا في وقت لاحق ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
            Dim lstPlr = (From s In db.V_expShipDets Where s.expShipDate > TheDate And s.trkProd = Val(GVRecvDet.GetRowCellValue(Ind, "trkPrd")) _
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

    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVRecvDet.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Prod As Integer = Val(GVRecvDet.GetRowCellValue(Ind, "trkPrd"))
        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVRecvDet.GetRowCellValue(i, "trkPrd")) = Prod Then
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
        Dim Qrymax = (From trk In db.receiveDets Select trk.trkRecvDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()
        Dim tb As New receiveDet
        Dim TbUn As New V_prdUnit
        If MemAddDet = True Then
            GVRecvDet.SetRowCellValue(RowInd, "trkRecDet", NewKey())
            tb.trkRecvDet = Val(GVRecvDet.GetRowCellValue(RowInd, "trkRecDet"))
            tb.trkRecv = Val(TxtRId.Text)

            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVRecvDet.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkProd = Val(GVRecvDet.GetRowCellValue(RowInd, "trkPrd"))
                    Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVRecvDet.GetRowCellValue(RowInd, "Weight"))
            tb.delRecDet = False
            tb.trkProd = Val(GVRecvDet.GetRowCellValue(RowInd, "trkPrd"))
            tb.amount = Val(GVRecvDet.GetRowCellValue(RowInd, "Amount"))
            '***************************************
            CalculatePrdUnit(TbUn.trkUnit, Val(GVRecvDet.GetRowCellValue(RowInd, "Amount")),
                    Val(GVRecvDet.GetRowCellValue(RowInd, "trkPrd")))
            '***************************************
            tb.untOne = UOne
            tb.amtOne = AOne
            tb.untTwo = UTwo
            tb.amtTwo = ATwo
            db.receiveDets.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New receiveDet
        Dim TbUn As New V_prdUnit
        Dim ThePrd As Integer
        If GVRecvDet.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVRecvDet.RowCount
                Dim TheReq As Integer
                TheReq = Val(GVRecvDet.GetRowCellValue(i, "trkRecDet"))
                If (GVRecvDet.IsRowSelected(i) = True) Then
                    ThePrd = Val(GVRecvDet.GetRowCellValue(i, "trkPrd"))
                    If CheckEditDel(ThePrd) = True Then
                        If CanSave(i) = True Then
                            tb = (From s In db.receiveDets Where s.trkRecvDet = Val(TheReq) And s.trkRecv = Val(TxtRId.Text) Select s).Single()
                            tb.trkRecv = Val(TxtRId.Text)
                            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVRecvDet.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkProd = Val(GVRecvDet.GetRowCellValue(i, "trkPrd"))
                                    Select s).SingleOrDefault
                            tb.trkUnit = TbUn.trkUnit
                            tb.weight = Val(GVRecvDet.GetRowCellValue(i, "Weight"))
                            tb.delRecDet = False
                            tb.trkProd = Val(GVRecvDet.GetRowCellValue(i, "trkPrd"))
                            tb.amount = Val(GVRecvDet.GetRowCellValue(i, "Amount"))
                            CalculatePrdUnit(TbUn.trkUnit, Val(GVRecvDet.GetRowCellValue(i, "Amount")),
                            Val(GVRecvDet.GetRowCellValue(i, "trkPrd")))
                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                            GVRecvDet.UnselectRow(i)
                        End If
                    End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVRecvDet.SelectedRowsCount = 0 Then
                Progress()
            End If

        End If
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVRecvDet.SelectedRowsCount <> 0 Then
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


    'Public Function CountDet() As Integer
    '    Dim lst = (From s In db.V_buyDetails Where s.delBDet = False And s.trkBuyClient = Val(TxtRId.Text) Select s).ToList
    '    Return lst.Count()
    'End Function
    Public Sub FillGrid()
        FillProd()
        GridControl1.RepositoryItems.Add(repPrd)
        GridControl1.RepositoryItems.Add(repTxt)
        Dim list As BindingList(Of recvDetail) = New BindingList(Of recvDetail)
        GridControl1.DataSource = list
        GVRecvDet.Columns(0).ColumnEdit = repTxt
        GVRecvDet.Columns(0).Visible = False
        GVRecvDet.Columns(1).ColumnEdit = repPrd
        GVRecvDet.Columns(2).ColumnEdit = repTxt
        GVRecvDet.Columns(3).ColumnEdit = repTxt
        GVRecvDet.Columns(5).ColumnEdit = repTxt

        GVRecvDet.OptionsSelection.MultiSelect = True
        GVRecvDet.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub
    Private Sub FormatColumns()
        col1 = GVRecvDet.Columns(1)
        col2 = GVRecvDet.Columns(2)
        col3 = GVRecvDet.Columns(3)
        col4 = GVRecvDet.Columns(4)
        col5 = GVRecvDet.Columns(5)
        '****************
        col1.Caption = "المنتج"
        col2.Caption = "المخزون "
        col3.Caption = "الكمية "
        col4.Caption = "الوحدة"
        col5.Caption = "الوزن"
        GVRecvDet.Columns(2).OptionsColumn.ReadOnly = True
        GVRecvDet.Columns(2).AppearanceCell.BackColor = Color.DarkRed
        GVRecvDet.Columns(2).AppearanceCell.ForeColor = Color.White
        GVRecvDet.Columns(2).AppearanceCell.FontSizeDelta = Font.Bold
    End Sub
    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVRecvDet.OptionsFind.AlwaysVisible = False
        Dim tb As New receiveDet
        Dim i As Integer = 0
        Dim ThePrd As Integer
        Dim lastRow As Integer = GVRecvDet.RowCount - 1
        Dim lastValue As Integer = Val(GVRecvDet.GetRowCellValue(lastRow, "trkRecDet"))
        If lastValue = 0 And GVRecvDet.IsRowSelected(lastRow) = True Then
            GVRecvDet.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
            MemAddDet = False
        End If
        If GVRecvDet.SelectedRowsCount <> 0 Then
            'E
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVRecvDet.RowCount
                    Dim TheReq As Integer
                    TheReq = Val(GVRecvDet.GetRowCellValue(i, "trkRecDet"))
                    If GVRecvDet.IsRowSelected(i) = True Then
                        ThePrd = Val(GVRecvDet.GetRowCellValue(i, "trkPrd"))
                        If CheckEditDel(ThePrd) = True Then
                            tb = (From s In db.receiveDets Where s.trkRecvDet = Val(TheReq) And s.trkRecv = Val(TxtRId.Text) Select s).Single()
                            tb.delRecDet = True
                            GVRecvDet.DeleteRow(i)
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
        If GVRecvDet.SelectedRowsCount <> 0 Then
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
        If GVRecvDet.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVRecvDet.OptionsFind.FindFilterColumns = "*"
            GVRecvDet.ShowFindPanel()
            GVRecvDet.OptionsFind.ShowClearButton = False
            GVRecvDet.OptionsFind.ShowFindButton = False
        End If
    End Sub



    '*****************this to edit saved data
    Private Sub LokStore_EditValueChanged(sender As Object, e As EventArgs) Handles LokStore.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub


    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokStore.Text = ""
        'LokClient.Text = ""
        FillStore()
        FillPeeler()
        '   FillClient()
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
    'Private Sub FillClient()
    '    If done = True And LokLoc.Text <> "" Then
    '        LokClient.Properties.DataSource = ""
    '        Dim lst = (From c In db.clientCrps Where c.delClntCrp = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
    '        LokClient.Properties.DataSource = lst
    '        LokClient.Properties.DisplayMember = "clntCrpName"
    '        LokClient.Properties.ValueMember = "trkClntCrp"
    '        LokClient.Properties.PopulateColumns()
    '        LokClient.Properties.Columns(0).Visible = False
    '        LokClient.Properties.Columns(2).Visible = False
    '        LokClient.Properties.Columns(3).Visible = False
    '        LokClient.Properties.Columns(4).Visible = False
    '        LokClient.Properties.Columns(5).Visible = False
    '    End If
    'End Sub
    Private Sub Datebuy_EditValueChanged(sender As Object, e As EventArgs) Handles DateRecv.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewRecv
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
        rpt.FilterString = " [trkRecv] =" & Val(TxtRId.Text)
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
            If Val(GVRecvDet.GetRowCellValue(GVRecvDet.RowCount - 1, "trkRecDet")) = 0 Then
                'If tb.trkBuyDet = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVRecvDet.RowCount - 1
                    GVRecvDet.FocusedRowHandle = lastRow
                    GVRecvDet.DeleteRow(lastRow)
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

    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub

    Private Sub TxtRecvInfo_TextChanged(sender As Object, e As EventArgs) Handles TxtRecvInfo.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub
    Private Function CheckEditDel(ByVal prd As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(DateRecv.Text, DateTime)

        CheckEditDel = False
        '************where del=0
        Dim lst = (From s In db.V_expShipDets Where s.expShipDate >= TheDate.ToShortDateString And s.trkProd = prd _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkAPrdStr = Val(LokStore.EditValue)
                   Select s).ToList()

        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع الشحن من المحطات ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDel = True
    End Function

    Private Sub GVRecvDet_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVRecvDet.CustomRowCellEditForEditing
        Dim prd As Integer
        If e.Column.Caption = "المنتج" Then
            GVRecvDet.SetFocusedRowCellValue("trkUnit", "")
        End If
        If e.Column.Caption = "الوحدة" Then
            prd = GVRecvDet.GetRowCellValue(e.RowHandle, "trkPrd")
            If prd <> 0 Then
                FillUnit(prd)
                e.RepositoryItem = repUnit
            End If
        End If
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

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

        Dim FstAmt As Double = 0
        CalTotal = 0
        Dim SecAmt As Double
        Dim TheDate As DateTime
        If DateRecv.Text <> "" Then
            TheDate = CType(DateRecv.Text, DateTime)
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
    'Private Function CalTotal(ByVal prd As Integer, ByVal un As Integer, ByVal UInd As Integer) As Double
    '    Dim isLoc As Boolean
    '    'Dim Clnt As Integer
    '    'If RdoLocal.Checked = True Then
    '    '    isLoc = 1
    '    '    Clnt = 0
    '    'ElseIf RdoClient.Checked = True
    '    '    isLoc = 0
    '    '    Clnt = Val(LokClient.EditValue)
    '    'End If
    '    Dim TheDate As DateTime
    '    Dim TotalStk As Double
    '    If Me.DateRecv.Text <> "" Then
    '        TheDate = CType(DateRecv.Text, DateTime)
    '    End If
    '    If UInd = 0 Then
    '        '*********************stock in recv
    '        Dim lst = (From s In db.toPrds Join d In db.toPrdDets On s.trkToPrd Equals d.trkToPrd
    '                   Where s.delToPrd = 0 And d.delToDet = 0 And s.toPrdDate <= TheDate.ToShortDateString _
    '                        And d.trkProd = prd And s.trkArival = Val(LokLoc.EditValue) _
    '                       And s.trkPeeler = Val(LokPeeler.EditValue) _
    '                       And d.untOne = un And s.isLocal = 1 And s.trkClntCrp = 0
    '                   Group s, d By s.trkArival, s.trkPeeler, d.trkProd, d.untOne Into Sum(d.amtOne)).ToList
    '        Dim totalBuy As Double
    '        If lst.Count = 0 Then
    '            totalBuy = 0
    '        Else
    '            totalBuy = lst.Item(0).Sum
    '        End If

    '        '******************shiped crp
    '        Dim lstShp = (From s In db.receives Join d In db.receiveDets On s.trkRecv Equals d.trkRecv
    '                      Where s.delRecv = 0 And d.delRecDet = 0 And s.recvDate <= TheDate.ToShortDateString _
    '                        And d.trkProd = prd And s.trkArival = Val(LokLoc.EditValue) _
    '                             And d.untOne = un And s.isLocal = isLoc And s.trkClntCrp = Clnt _
    '                       And s.trkPeeler = Val(LokPeeler.EditValue)
    '                      Group s, d By s.trkArival, s.trkPeeler, d.trkProd, d.untOne Into Sum(d.amtOne)).ToList
    '        Dim totalShp As Double
    '        If lstShp.Count = 0 Then
    '            totalShp = 0
    '        Else
    '            totalShp = lstShp.Item(0).Sum
    '        End If
    '        CalTotal = totalBuy - totalShp

    '    Else
    '        '$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$ index 1

    '        '*********************stock in recv
    '        Dim lst = (From s In db.toPrds Join d In db.toPrdDets On s.trkToPrd Equals d.trkToPrd
    '                   Where s.delToPrd = 0 And d.delToDet = 0 And s.toPrdDate <= TheDate.ToShortDateString _
    '                        And d.trkProd = prd And s.trkArival = Val(LokLoc.EditValue) _
    '                       And s.trkPeeler = Val(LokPeeler.EditValue) _
    '                       And d.untTwo = un And s.isLocal = isLoc And s.trkClntCrp = Clnt
    '                   Group s, d By s.trkArival, s.trkPeeler, d.trkProd, d.untTwo Into Sum(d.amtTwo)).ToList
    '        Dim totalBuy As Double
    '        If lst.Count = 0 Then
    '            totalBuy = 0
    '        Else
    '            totalBuy = lst.Item(0).Sum
    '        End If

    '        '******************shiped crp
    '        Dim lstShp = (From s In db.receives Join d In db.receiveDets On s.trkRecv Equals d.trkRecv
    '                      Where s.delRecv = 0 And d.delRecDet = 0 And s.recvDate <= TheDate.ToShortDateString _
    '                        And d.trkProd = prd And s.trkArival = Val(LokLoc.EditValue) _
    '                             And d.untTwo = un And s.isLocal = isLoc And s.trkClntCrp = Clnt _
    '                       And s.trkPeeler = Val(LokPeeler.EditValue)
    '                      Group s, d By s.trkArival, s.trkPeeler, d.trkProd, d.untTwo Into Sum(d.amtTwo)).ToList
    '        Dim totalShp As Double
    '        If lstShp.Count = 0 Then
    '            totalShp = 0
    '        Else
    '            totalShp = lstShp.Item(0).Sum
    '        End If
    '        CalTotal = totalBuy - totalShp

    '    End If
    'End Function
    Private Sub TheStk()
        Dim count As Integer = GVRecvDet.RowCount
        Dim i As Integer = 0
        Dim UndPreShip As Double = 0
        Dim tbShip As New V_RecvDet
        Dim Total As Double
        Dim curPrd As Integer
        '****
        Dim isLoc As Boolean
        Dim Clnt As Integer
        Dim TheDate As DateTime
        If DateRecv.Text <> "" Then
            TheDate = CType(DateRecv.Text, DateTime)
        End If
        While i < count
            Dim TbUn As New V_prdUnit
            curPrd = GVRecvDet.GetRowCellValue(i, "trkPrd")

            Dim j As Integer = 0
            Dim StrUn As String = Trim(GVRecvDet.GetRowCellValue(i, "trkUnit"))
            '$$$$$$$$$$$$$$$$$$$$$$$$
            If StrUn <> "" And curPrd <> 0 Then
                Dim lst = (From s In db.V_prdUnits Where s.trkProd = curPrd Select s).ToList
                If lst.Count = 2 Then
                    While j < 2
                        If lst.Item(j).unitName = StrUn Then
                            Exit While
                        End If
                        j = j + 1
                    End While
                End If
                '$$$$$$$$$$$$$$$$$$$$$$$
                If Val(GVRecvDet.GetRowCellValue(i, "trkRecDet")) <> 0 Then
                    'If RdoLocal.Checked = True Then
                    '    isLoc = 1
                    '    Clnt = 0
                    'ElseIf RdoClient.Checked = True
                    '    isLoc = 0
                    '    Clnt = Val(LokClient.EditValue)
                    'End If

                    tbShip = (From s In db.V_RecvDets Where s.trkRecvDet = Val(GVRecvDet.GetRowCellValue(i, "trkRecDet")) _
                                                         And s.trkProd = curPrd And s.trkArival = Val(LokLoc.EditValue) _
                                                         And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                            And s.isLocal = True And s.trkClntCrp = 0 And s.recvDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbShip) Then
                        UndPreShip = tbShip.amount
                    Else
                        UndPreShip = 0
                    End If
                    GVRecvDet.SelectRow(i)
                End If
                Total = CalTotal(curPrd, j) + UndPreShip
                GVRecvDet.SetRowCellValue(i, "total", Total)
            End If

            If i = count - 1 Then
                If Val(GVRecvDet.GetRowCellValue(count - 1, "trkRecDet")) = 0 Then
                    GVRecvDet.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub

    Private Sub LokPeeler_TextChanged(sender As Object, e As EventArgs) Handles LokPeeler.TextChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub GVRecvDet_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GVRecvDet.CellValueChanged
        Dim prd As Integer = GVRecvDet.GetRowCellValue(e.RowHandle, "trkPrd")
        Dim StrUn As String = Trim(GVRecvDet.GetFocusedRowCellValue("trkUnit"))
        Dim TheId As Integer = GVRecvDet.GetRowCellValue(e.RowHandle, "trkRecDet")
        Dim Amt As Double = GVRecvDet.GetRowCellValue(e.RowHandle, "Amount")
        Dim Total As Double
        Dim tbShip As New V_RecvDet
        Dim i As Integer = 0
        Dim TheDate As DateTime
        If DateRecv.Text <> "" Then
            TheDate = CType(DateRecv.Text, DateTime)
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
                    tbShip = (From s In db.V_RecvDets Where s.trkRecvDet = TheId _
                                                         And s.trkProd = prd And s.trkArival = Val(LokLoc.EditValue) _
                                                         And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                            And s.isLocal = True And s.trkClntCrp = 0 And s.recvDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbShip) Then
                        If i = 0 Then
                            Total = Total + tbShip.amtOne
                            GVRecvDet.SetFocusedRowCellValue("Amount", tbShip.amtOne)
                        Else
                            Total = Total + tbShip.amtTwo
                            GVRecvDet.SetFocusedRowCellValue("Amount", tbShip.amtTwo)
                        End If
                    End If
                End If
            GVRecvDet.SetFocusedRowCellValue("total", Total)
            Else
                GVRecvDet.SetFocusedRowCellValue("total", 0)
            End If
        End If
    End Sub

    'Private Sub RdoLocal_Click(sender As Object, e As EventArgs)
    '    LokClient.Enabled = False
    '    If saved = True Then
    '        MemEdit = True
    '    End If
    'End Sub

    'Private Sub RdoClient_Click(sender As Object, e As EventArgs)
    '    LokClient.Enabled = True
    '    If saved = True Then
    '        MemEdit = True
    '    End If
    'End Sub
End Class