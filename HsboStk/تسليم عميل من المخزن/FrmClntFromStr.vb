
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmClntFromStr
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
#Region "Transfared Values"
    Public PLok As Integer
    Public PStore As Integer
    '*************
    Public PProd As Integer
    Public PAmt As Double
    Public PUnit As Integer
    Public PWeight As Double
    Public PClient As Integer
    Public repUnit As Repository.RepositoryItemLookUpEdit
#End Region

    Private Sub ViewDet()
        Dim i As Integer = 0
        Dim tb = (From s In db.V_RecvClnts Where s.trkClntRecv = ID And s.delClntRecv = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        TxtLok.Text = tb.arivalName
        TxtJobOrd.Text = tb.trkToPrd
        Me.DateStore.Text = tb.ClntRecvDate
        TxtStrInf.Text = tb.ClntRecvInf
        PLok = tb.trkArival
        FillStore()
        LokStore.EditValue = tb.trkAPrdStr
        PClient = tb.trkClntCrp
        TxtClient.Text = tb.clntCrpName
        BtnFind.Enabled = False
        TxtJobOrd.ReadOnly = True
        LokStore.ReadOnly = True
        btnSrchStr.Enabled = False
        Dim TbUn As New V_prdUnit
        '*******************************
        Dim lst = (From s In db.V_RecvClntDets Where s.trkClntRecv = ID Select s).ToList()
        While i < lst.Count
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVRecvDet.AddNewRow()
            '    '******************Total
            TbUn = (From s In db.V_prdUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkProd = lst.Item(i).trkProd Select s).SingleOrDefault


            '$$$$$$$$$$$$$$$$ Calculate Index
            Dim j As Integer = 0
            Dim lstInd = (From s In db.V_prdUnits Where s.trkProd = lst.Item(i).trkProd Select s).ToList()
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
            GVRecvDet.SetFocusedRowCellValue("trkRecDet", lst.Item(i).trkRecvClntDet)
            GVRecvDet.SetFocusedRowCellValue("trkPrd", lst.Item(i).prodName)
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

    Private Sub BtnFind_Click(sender As Object, e As EventArgs) Handles BtnFind.Click
        If Trim(TxtJobOrd.Text) <> "" Then
            Dim tb As New V_ToPrd
            tb = (From s In db.V_ToPrds Where s.trkToPrd = Val(TxtJobOrd.Text) And s.isLocal = False Select s).SingleOrDefault
            If Not IsNothing(tb) Then
                TxtLok.Text = tb.arivalName
                PToTrk = tb.trkToPrd
                PLok = tb.trkArival
                PClient = tb.trkClntCrp
                TxtClient.Text = tb.clntCrpName
                FillStore()
            Else
                ClearForm()
            End If
        End If
    End Sub
    Private Sub ClearForm()
        TxtLok.Text = ""
        TxtClient.Text = ""
        GridControl1.DataSource = ""
    End Sub
    Private Sub FillStore()
        'If done = True Then
        LokStore.Properties.DataSource = ""
        Dim lst = (From c In db.arivalPrdStores Where c.delAPrd = False And c.trkArival = Val(PLok) Select c).ToList
        LokStore.Properties.DataSource = lst
        LokStore.Properties.DisplayMember = "APrdStr"
        LokStore.Properties.ValueMember = "trkAPrdStr"
        LokStore.Properties.PopulateColumns()
        LokStore.Properties.Columns(0).Visible = False
        LokStore.Properties.Columns(2).Visible = False
        LokStore.Properties.Columns(3).Visible = False
        'End If
    End Sub
    Private Sub NotSaved()
        GVRecvDet.Columns.Clear()
        Row = 0
        RowInd=0
        FillGrid()
        FormatColumns()
        Dim Total As Double
        Dim i As Integer = Row
        Dim TbUn As New V_prdUnit
        Dim TheDate As DateTime
        Dim tb As New PrdClntRecvResult
        If DateStore.Text <> "" Then
            TheDate = CType(DateStore.Text, DateTime)
        End If
        Dim lst = (From s In db.PrdClntRecv(TheDate, Val(TxtJobOrd.Text), Val(PLok), Val(LokStore.EditValue)) Select s).ToList

        Dim tbPrd As New product
        While i < lst.Count
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
            tbPrd = (From s In db.products Where s.trkProd = lst.Item(i).trkProd Select s).Single
            Row = Row + 1
            RowInd = Row - 1
            GVRecvDet.AddNewRow()
            GVRecvDet.SetFocusedRowCellValue("trkPrd", tbPrd.prodName)
            If i = 0 Then
                Total = lst.Item(i).oneUnt
                Total = Math.Round(Total, 2)
                GVRecvDet.SetFocusedRowCellValue("total", Total)
                GVRecvDet.SetFocusedRowCellValue("trkUnit", lst.Item(i).oneName)
            Else
                Total = lst.Item(i).twoUnt
                Total = Math.Round(Total, 2)
                GVRecvDet.SetFocusedRowCellValue("total", Total)
                GVRecvDet.SetFocusedRowCellValue("trkUnit", lst.Item(i).twoName)
            End If
            GVRecvDet.UpdateCurrentRow()
            i = i + 1
        End While

    End Sub
    Private Function CanSaveReq() As Boolean
        CanSaveReq = False
        If DateStore.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateStore.Focus()
            Exit Function
        End If
        If CType(DateStore.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateStore.Focus()
            Exit Function
        End If
        If LokStore.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء ادخال اسم المخزن ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokStore.Focus()
            Exit Function
        End If
        If Val(TxtJobOrd.Text) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء ادخال أمر الانتاج ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            TxtJobOrd.Focus()
            Exit Function
        End If
        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New receiveClnt
        Dim theDate As DateTime
        theDate = CType(DateStore.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkClntRecv = trk
            tb.trkArival = Val(PLok)
            tb.ClntRecvDate = theDate.ToShortDateString
            tb.trkAPrdStr = Val(LokStore.EditValue)
            tb.trkPeeler = 0
            tb.trkToPrd = Val(TxtJobOrd.Text)
            tb.trkClntCrp = Val(PClient)

            tb.ClntRecvInf = TxtStrInf.Text
            tb.delClntRecv = False
            db.receiveClnts.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.receiveClnts Where s.trkClntRecv = Val(TxtRId.Text) And s.delClntRecv = False Select s).Single()
            tb.trkArival = Val(PLok)
            tb.ClntRecvDate = theDate.ToShortDateString
            tb.trkAPrdStr = Val(LokStore.EditValue)
            tb.trkPeeler = 0
            tb.trkToPrd = Val(TxtJobOrd.Text)
            tb.trkClntCrp = Val(PClient)
            tb.ClntRecvInf = TxtStrInf.Text
            tb.delClntRecv = False
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
    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub

    Function NewReqKey() As Integer
        Dim TheKey As Integer
        Dim Qrymax = (From trk In db.receiveClnts Select trk.trkClntRecv).ToList
        If Qrymax.Count = 0 Then
            TheKey = 1
        Else
            TheKey = Qrymax.Max() + 1
        End If
        Return TheKey
    End Function
    Private Function CanSave(ByVal Ind As Integer) As Boolean
        CanSave = False
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
        CanSave = True
    End Function

    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.recvClntDets Select trk.trkRecvClntDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function

    Private Sub SaveData()
        Dim TbUn As New V_prdUnit
        Dim tbPrd As New product
        Dim TheReq As Integer
        Dim i As Integer = 0
        While i < GVRecvDet.RowCount
            TheReq = Val(GVRecvDet.GetRowCellValue(i, "trkRecDet"))
            If TheReq = 0 Then
                If CanSave(i) = True Then
                    Dim tb As New recvClntDet
                    GVRecvDet.SetRowCellValue(i, "trkRecDet", NewKey())
                    tb.trkRecvClntDet = Val(GVRecvDet.GetRowCellValue(i, "trkRecDet"))
                    tb.trkClntRecv = Val(TxtRId.Text)
                    tbPrd = (From s In db.products Where s.prodName = Trim(GVRecvDet.GetRowCellValue(i, "trkPrd")) And s.delPrd = 0).SingleOrDefault()
                    TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVRecvDet.GetRowCellValue(i, "trkUnit")) _
                                                            And s.trkProd = Val(tbPrd.trkProd)
                            Select s).SingleOrDefault()
                    tb.trkUnit = TbUn.trkUnit
                    tb.weight = Val(GVRecvDet.GetRowCellValue(i, "Weight"))
                    tb.delRecDet = False
                    tb.trkProd = Val(tbPrd.trkProd)
                    tb.amount = Val(GVRecvDet.GetRowCellValue(i, "Amount"))
                    '***************************************
                    CalculatePrdUnit(TbUn.trkUnit, Val(GVRecvDet.GetRowCellValue(i, "Amount")),
                            Val(tbPrd.trkProd))
                    '***************************************
                    tb.untOne = UOne
                    tb.amtOne = AOne
                    tb.untTwo = UTwo
                    tb.amtTwo = ATwo
                    db.recvClntDets.InsertOnSubmit(tb)
                    db.SubmitChanges()
                    Progress()
                End If
            End If
            i = i + 1
        End While
    End Sub

    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New recvClntDet
        Dim tbPrd As New product
        Dim TbUn As New V_prdUnit
        Dim ThePrd As Integer
        If GVRecvDet.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVRecvDet.RowCount
                Dim TheReq As Integer
                TheReq = Val(GVRecvDet.GetRowCellValue(i, "trkRecDet"))
                If (GVRecvDet.IsRowSelected(i) = True And TheReq <> 0) Then
                    tbPrd = (From s In db.products Where s.prodName = Trim(GVRecvDet.GetRowCellValue(i, "trkPrd"))).SingleOrDefault()

                    ThePrd = Val(tbPrd.trkProd)
                    ' If CheckEditDel(ThePrd) = True Then
                    If CanSave(i) = True Then
                        tb = (From s In db.recvClntDets Where s.trkRecvClntDet = Val(TheReq) And s.trkClntRecv = Val(TxtRId.Text) Select s).SingleOrDefault()
                        tb.trkClntRecv = Val(TxtRId.Text)
                        TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVRecvDet.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkProd = Val(ThePrd)
                                Select s).SingleOrDefault()
                        tb.trkUnit = TbUn.trkUnit
                        tb.weight = Val(GVRecvDet.GetRowCellValue(i, "Weight"))
                        tb.delRecDet = False
                        tb.trkProd = Val(ThePrd)
                        tb.amount = Val(GVRecvDet.GetRowCellValue(i, "Amount"))
                        CalculatePrdUnit(TbUn.trkUnit, Val(GVRecvDet.GetRowCellValue(i, "Amount")),
                            Val(ThePrd))
                        tb.untOne = UOne
                        tb.amtOne = AOne
                        tb.untTwo = UTwo
                        tb.amtTwo = ATwo
                        GVRecvDet.UnselectRow(i)
                    End If
                    ' End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVRecvDet.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub

    Private Sub FrmReceive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        'FillLoc()
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

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVRecvDet.OptionsFind.AlwaysVisible = False
        Dim tb As New recvClntDet
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
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVRecvDet.RowCount
                    Dim TheReq As Integer
                    TheReq = Val(GVRecvDet.GetRowCellValue(i, "trkRecDet"))
                    If GVRecvDet.IsRowSelected(i) = True Then
                        ThePrd = Val(GVRecvDet.GetRowCellValue(i, "trkPrd"))
                        'If CheckEditDel(ThePrd) = True Then
                        tb = (From s In db.recvClntDets Where s.trkRecvClntDet = Val(TheReq) And s.trkClntRecv = Val(TxtRId.Text) Select s).Single()
                        tb.delRecDet = True
                        GVRecvDet.DeleteRow(i)
                        i = i - 1
                        SavedRow = Row - 1
                        Row = SavedRow
                        MemAddDet = False
                        ' End If
                    End If
                    i = i + 1
                End While
                db.SubmitChanges()
            Else
                Exit Sub
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
                MemEdit = False
                MemAdd = False
            End If
            If IsDet = False Then
                IsDet = True
                Exit Sub
            End If
        Else
            Exit Sub
        End If

        If Row > 0 Then
            'If MemAddDet Then
            MemAddDet = True
            'If CanSave(RowInd) = True Then
            SavedRow = Row
            SaveData()
            MemAddDet = False
            'End If
            'End If
            SaveEdit()
        End If

    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click

        Dim isLoc As Boolean
        Dim Clnt As Integer

        Clnt = Val(PClient)
        isLoc = False
        '*********************
        Dim rpt As New RepViewClntRcv
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
        rpt.FilterString = "[trkClntRecv]= " & Val(TxtRId.Text)
        rpt.tStr.Value = "مخزن المنتجات:"
        rpt.strName.Value = "[APrdStr]"
        rpt.tStr.Visible = False
        rpt.strName.Visible = False
        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub
    Public Sub FillGrid()
        GridControl1.RepositoryItems.Add(repTxt)
        Dim list As BindingList(Of recvDetail) = New BindingList(Of recvDetail)
        GridControl1.DataSource = list
        GVRecvDet.Columns(0).Visible = False
        GVRecvDet.Columns(3).ColumnEdit = repTxt
        GVRecvDet.Columns(5).ColumnEdit = repTxt
        GVRecvDet.OptionsSelection.MultiSelect = True
        GVRecvDet.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
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
        GVRecvDet.Columns(1).OptionsColumn.ReadOnly = True
        GVRecvDet.Columns(2).OptionsColumn.ReadOnly = True
        'GVRecvDet.Columns(4).OptionsColumn.ReadOnly = True
        GVRecvDet.Columns(2).OptionsColumn.ReadOnly = True
        GVRecvDet.Columns(2).AppearanceCell.BackColor = Color.DarkRed
        GVRecvDet.Columns(2).AppearanceCell.ForeColor = Color.White
        GVRecvDet.Columns(2).AppearanceCell.FontSizeDelta = Font.Bold
    End Sub

    'Private Function CheckEditDel(ByVal prd As Integer) As Boolean
    '    Dim TheDate As DateTime
    '    TheDate = CType(DateRecv.Text, DateTime)

    '    CheckEditDel = False
    '    '************where del=0
    '    Dim lst = (From s In db.V_expShipDets Where s.expShipDate >= TheDate.ToShortDateString And s.trkProd = prd _
    '                                         And s.trkArival = Val(PLok) And s.trkAPrdStr = Val(LokStore.EditValue)
    '               Select s).ToList()

    '    If lst.Count > 0 Then
    '        XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع الشحن من المحطات ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
    '        Exit Function
    '    End If
    '    CheckEditDel = True
    'End Function
    Private Function CalTotal(ByVal prd As Integer, ByVal UInd As Integer) As Double
        Dim tb As New PrdClntStrResult
        Dim TheDate As DateTime
        If DateStore.Text <> "" Then
            TheDate = CType(DateStore.Text, DateTime)
        End If
        tb = (From s In db.PrdClntStr(TheDate, Val(TxtJobOrd.Text), Val(PLok), Val(LokStore.EditValue), prd) Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            If UInd = 0 Then
                CalTotal = tb.oneUnt
            Else
                CalTotal = tb.twoUnt

            End If
        End If
        CalTotal = Math.Round(CalTotal, 2)
    End Function
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

    Private Sub TheStk()
        Dim count As Integer = GVRecvDet.RowCount
        Dim i As Integer = 0
        Dim UndPreShip As Decimal = 0
        Dim tbRcvClnt As New V_RecvClntDet
        Dim Total As Double
        Dim curPrd As Integer
        Dim tbPrd As New product
        '****
        Dim isLoc As Boolean
        Dim Clnt As Integer
        Dim TheDate As DateTime
        If DateStore.Text <> "" Then
            TheDate = CType(DateStore.Text, DateTime)
        End If
        While i < count
            Dim TbUn As New V_prdUnit
            tbPrd = (From s In db.products Where s.prodName = Trim(GVRecvDet.GetRowCellValue(i, "trkPrd")) And s.delPrd = 0).SingleOrDefault
            curPrd = Val(tbPrd.trkProd)

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
                    '    Clnt = Val(PClient)
                    'End If
                    isLoc = 0
                    Clnt = Val(PClient)
                    tbRcvClnt = (From s In db.V_RecvClntDets Where s.trkRecvClntDet = Val(GVRecvDet.GetRowCellValue(i, "trkRecDet")) _
                                                            And s.trkAPrdStr = Val(LokStore.EditValue) And s.trkToPrd = Val(TxtJobOrd.Text) _
                                                                 And s.trkProd = curPrd And s.ClntRecvDate <= TheDate Select s).SingleOrDefault
                    If Not IsNothing(tbRcvClnt) Then
                        UndPreShip = tbRcvClnt.amount
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

    Private Sub DateStore_TextChanged(sender As Object, e As EventArgs) Handles DateStore.TextChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub btnSrchStr_Click(sender As Object, e As EventArgs) Handles btnSrchStr.Click
        If CanSaveReq() = True Then
            NotSaved()
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

    Private Sub TxtJobOrd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJobOrd.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub

    Private Sub GVRecvDet_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GVRecvDet.CellValueChanged
        Dim prd As Integer
        Dim prdName As String = Trim(GVRecvDet.GetRowCellValue(e.RowHandle, "trkPrd"))
        Dim StrUn As String = Trim(GVRecvDet.GetRowCellValue(e.RowHandle, "trkUnit"))
        Dim TheId As Integer = GVRecvDet.GetRowCellValue(e.RowHandle, "trkRecDet")
        Dim Amt As Double = GVRecvDet.GetRowCellValue(e.RowHandle, "Amount")
        Dim Total As Double
        Dim tbRcvClnt As New V_RecvClntDet
        Dim i As Integer = 0
        Dim tbPrd As New product
        Dim TheDate As DateTime
        If DateStore.Text <> "" Then
            TheDate = CType(DateStore.Text, DateTime)
        End If
        If e.Column.Caption = "الوحدة" Then
            If StrUn <> "" And prdName <> "" Then
                tbPrd = (From s In db.products Where s.prodName = prdName And s.delPrd = 0).SingleOrDefault()
                prd = tbPrd.trkProd
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
                    tbRcvClnt = (From s In db.V_RecvClntDets Where s.trkRecvClntDet = TheId _
                                                            And s.trkAPrdStr = Val(LokStore.EditValue) And s.trkToPrd = Val(TxtJobOrd.Text) _
                                                                 And s.trkProd = prd And s.ClntRecvDate <= TheDate Select s).SingleOrDefault
                    If Not IsNothing(tbRcvClnt) Then
                        If i = 0 Then
                            Total = Total + tbRcvClnt.amtOne
                            GVRecvDet.SetFocusedRowCellValue("Amount", tbRcvClnt.amtOne)
                        Else
                            Total = Total + tbRcvClnt.amtTwo
                            GVRecvDet.SetFocusedRowCellValue("Amount", tbRcvClnt.amtTwo)
                        End If

                    End If
                End If
                GVRecvDet.SetFocusedRowCellValue("total", Total)
                StrUn = ""
                prdName = ""
            Else
                GVRecvDet.SetFocusedRowCellValue("total", 0)
            End If

        End If

    End Sub

    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub
    Private Sub GVRecvDet_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVRecvDet.CustomRowCellEditForEditing


        Dim prd As Integer
        Dim prdName As String = Trim(GVRecvDet.GetRowCellValue(e.RowHandle, "trkPrd"))
        Dim tbPrd As New product
        If e.Column.Caption = "المنتج" Then
            GVRecvDet.SetFocusedRowCellValue("trkUnit", "")
        End If
        If e.Column.Caption = "الوحدة" And prdName <> "" Then
            'prd = GVRecvDet.GetRowCellValue(e.RowHandle, "trkPrd")
            tbPrd = (From s In db.products Where s.prodName = prdName And s.delPrd = 0).SingleOrDefault()
            prd = tbPrd.trkProd
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

    Private Sub LokStore_EditValueChanged(sender As Object, e As EventArgs) Handles LokStore.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
End Class