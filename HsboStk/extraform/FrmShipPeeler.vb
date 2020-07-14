
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils
Public Class FrmShipPeeler
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private Flag As Integer = 0
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Public curCrop As Integer
#Region "Repository Variables"
    Public repUnit As New Repository.RepositoryItemLookUpEdit
    Public repDriver As New Repository.RepositoryItemLookUpEdit
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
    Private Sub FrmShipProd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        '*************************
        If ViewShip = True Then

            Dim tb As New V_expShip
            tb = (From s In db.V_expShips Where s.trkToPrd = PToTrk And s.delExpShip = 0 Select s).SingleOrDefault
            If Not IsNothing(tb) Then
                TxtRId.Text = Val(tb.trkExpShip)
                TxtLok.Text = tb.arivalName
                TxtPeeler.Text = tb.peelerName
                TxtPInfo.Text = tb.expShpInf
                DateExpShip.Text = tb.expShipDate
                FillExp()
                LokExp.EditValue = tb.trkExpLoc
                PLok = tb.trkArival
                PPeeler = tb.trkPeeler
            Else
                ReadFromTo()
            End If
        Else
            ReadFromTo()
        End If

        '*****************************
        FillGrid()
        ViewDet()
        NotSaved()
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
        DateExpShip.Text = tb.toPrdDate
        PLok = tb.trkArival
        PPeeler = tb.trkPeeler
        DateExpShip.ReadOnly = True
        FillExp()
    End Sub
    Private Sub ViewDet()
        Dim i As Integer = 0

        Dim lst = (From s In db.V_expDetOfShps Where s.trkExpShip = Val(TxtRId.Text) And s.delExShDet = 0
                   Select s).ToList
        While i < lst.Count
                    Row = Row + 1
                    RowInd = Row - 1
                    GVExpShip.AddNewRow()
                    GVExpShip.SetFocusedRowCellValue("trkExShipDet", lst.Item(i).trkExpShipDet)
                    GVExpShip.SetFocusedRowCellValue("trkProd", lst.Item(i).prodName)
                    GVExpShip.SetFocusedRowCellValue("Amount", lst.Item(i).storeAmount)
                    GVExpShip.SetFocusedRowCellValue("trkUnit", lst.Item(i).unitName)
                    GVExpShip.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
                    GVExpShip.SetFocusedRowCellValue("trkDriver", lst.Item(i).trkDriver)
                    GVExpShip.SetFocusedRowCellValue("trkCar", lst.Item(i).trkCar)
                    GVExpShip.UpdateCurrentRow()
                    '*****************************************
                    PProd = lst.Item(i).trkProd
                    PAmt = lst.Item(i).storeAmount
                    PUnit = lst.Item(i).trkUnit
                    PWeight = lst.Item(i).weight
                    i = i + 1
            End While

    End Sub
    Private Sub NotSaved()
        Dim i As Integer = Row
        Dim lst = (From s In db.V_toPrdDets Where s.trkToPrd = PToTrk And s.delToDet = 0 Select s).ToList
        While i < lst.Count
            Row = Row + 1
            RowInd = Row - 1
            GVExpShip.AddNewRow()
            GVExpShip.SetFocusedRowCellValue("trkProd", lst.Item(i).prodName)
            GVExpShip.SetFocusedRowCellValue("Amount", lst.Item(i).amount)
            GVExpShip.SetFocusedRowCellValue("trkUnit", lst.Item(i).unitName)
            GVExpShip.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVExpShip.UpdateCurrentRow()
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


        GVExpShip.OptionsFind.AlwaysVisible = False
        ' If CanSaveReq() = True Then

        If CanSaveReq() = True Then
            If MemAdd Or MemEdit Then
                SaveReqData()
                MemEdit = False
                MemAdd = False
            End If


            If Row > 0 Then
                SavedRow = Row
                SaveData()
                SaveEdit()
            End If
        End If

    End Sub
    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs)
        e.Handled = NumOnly(e.KeyChar)
    End Sub
    Private Function CanSaveReq() As Boolean
        CanSaveReq = False
        If LokExp.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم منطقة الصادر ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokExp.Focus()
            LokExp.SelectAll()
            Exit Function
        End If

        CanSaveReq = True
    End Function

    Private Sub SaveReqData()
        Dim tb As New expShip
        Dim theDate As DateTime
        theDate = CType(DateExpShip.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkExpShip = trk
            tb.trkArival = Val(PLok)
            tb.expShipDate = theDate.ToShortDateString
            tb.trkToPrd = PToTrk
            tb.trkPeeler = Val(PPeeler)
            tb.trkExpLoc = Val(LokExp.EditValue.ToString())
            tb.delExpShip = False
            tb.expShpInf = TxtPInfo.Text
            db.expShips.InsertOnSubmit(tb)
            saved = True

            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.expShips Where s.trkExpShip = Val(TxtRId.Text) And s.delExpShip = False Select s).Single()
            tb.trkExpLoc = Val(LokExp.EditValue.ToString())
            tb.delExpShip = False
            tb.expShpInf = TxtPInfo.Text
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
        Dim Qrymax = (From trk In db.expShips Select trk.trkExpShip).ToList
        If Qrymax.Count = 0 Then
            TheKey = 1
        Else
            TheKey = Qrymax.Max() + 1
        End If
        Return TheKey
    End Function
    Private Function CanSave(ByVal Ind As Integer) As Boolean
        CanSave = False

        If Val(GVExpShip.GetRowCellValue(Ind, "trkDriver")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم السائق ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVExpShip.SelectCell(Ind, col2)
            Exit Function
        End If
        If Val(GVExpShip.GetRowCellValue(Ind, "trkCar")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال رقم العربة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVExpShip.SelectCell(Ind, col2)
            Exit Function
        End If

        CanSave = True
    End Function

    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.expShipDets Select trk.trkExpShipDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()
        Dim TheReq As Integer
        Dim tbUnt As unit
        Dim i As Integer = 0
        While i < GVExpShip.RowCount
            TheReq = Val(GVExpShip.GetRowCellValue(i, "trkExShipDet"))
            tbUnt = (From s In db.units Where s.unitName = Trim(GVExpShip.GetRowCellValue(i, "trkUnit")) And s.delUn = 0 Select s).Single
            If TheReq = 0 Then
                If CanSave(i) = True Then
                    Dim tb As New expShipDet
                    GVExpShip.SetRowCellValue(i, "trkExShipDet", NewKey())
                    tb.trkExpShipDet = Val(GVExpShip.GetRowCellValue(i, "trkExShipDet"))
                    tb.trkExpShip = Val(TxtRId.Text)
                    tb.trkUnit = tbUnt.trkUnit
                    tb.weight = Val(GVExpShip.GetRowCellValue(i, "Weight"))
                    tb.Driver = Val(GVExpShip.GetRowCellValue(i, "trkDriver"))
                    tb.Car = Val(GVExpShip.GetRowCellValue(i, "trkCar"))
                    tb.delExShDet = False
                    tb.trkProd = Val(GVExpShip.GetRowCellValue(i, "trkProd"))
                    tb.storeAmount = Val(GVExpShip.GetRowCellValue(i, "Amount"))

                    '***************************************
                    CalculatePrdUnit(tbUnt.trkUnit, Val(GVExpShip.GetRowCellValue(i, "Amount")),
                                    Val(GVExpShip.GetRowCellValue(i, "trkProd")))

                    '***************************************
                    tb.untOne = UOne
                    tb.amtOne = AOne
                    tb.untTwo = UTwo
                    tb.amtTwo = ATwo
                    GVExpShip.UnselectRow(i)
                    db.expShipDets.InsertOnSubmit(tb)
                    db.SubmitChanges()
                    Progress()
                Else
                    GVExpShip.SelectRows(i, i)
                End If
            End If
            i = i + 1
        End While


    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New expShipDet
        If GVExpShip.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVExpShip.RowCount
                Dim TheReq As Integer
                TheReq = Val(GVExpShip.GetRowCellValue(i, "trkExShipDet"))
                If (GVExpShip.IsRowSelected(i) = True And TheReq <> 0) Then
                    If CanSave(i) = True Then
                        tb = (From s In db.expShipDets Where s.trkExpShipDet = Val(TheReq) And s.trkExpShip = Val(TxtRId.Text) Select s).Single()
                        tb.Driver = Val(GVExpShip.GetRowCellValue(i, "trkDriver"))
                        tb.Car = Val(GVExpShip.GetRowCellValue(i, "trkCar"))
                        GVExpShip.UnselectRow(i)
                        db.SubmitChanges()
                        Progress()
                    End If
                End If
                i = i + 1
            End While
        End If
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVExpShip.SelectedRowsCount <> 0 Then
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

    Public Sub FillDriver()
        Dim cl = (From s In db.drivers Where s.delD = False
                  Select s).ToList
        repDriver.DataSource = cl
        repDriver.ValueMember = "trkDriver"
        repDriver.DisplayMember = "driverName"
        repDriver.ShowHeader = False
        repDriver.PopulateColumns()
        repDriver.Columns(0).Visible = False
        repDriver.Columns(2).Visible = False
        repDriver.Columns(3).Visible = False
        repDriver.Columns(4).Visible = False
        repDriver.NullText = ""
    End Sub
    Public Sub FillCar()
        Dim cl = (From s In db.cars Where s.delC = False
                  Select s).ToList
        repCar.DataSource = cl
        repCar.ValueMember = "trkCar"
        repCar.DisplayMember = "carNo"
        repCar.ShowHeader = False
        repCar.PopulateColumns()
        repCar.Columns(0).Visible = False
        repCar.Columns(2).Visible = False
        repCar.NullText = ""
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub
    Public Sub FillGrid()
        FillDriver()
        FillCar()
        GridControl1.RepositoryItems.Add(repDriver)
        GridControl1.RepositoryItems.Add(repCar)
        '***************** should be added her to avoid disappear when focus changed
        GridControl1.RepositoryItems.Add(repPrd)
        Dim list As BindingList(Of expoProd) = New BindingList(Of expoProd)

        GridControl1.DataSource = list
        'GVExpShip.Columns(0).ColumnEdit = repTxt
        GVExpShip.Columns(0).Visible = False
        'GVExpShip.Columns(1).ColumnEdit = repPrd
        'GVExpShip.Columns(2).ColumnEdit = repTxt
        GVExpShip.Columns(2).Visible = False
        'GVExpShip.Columns(3).ColumnEdit = repTxt
        'GVExpShip.Columns(4).ColumnEdit = repUnit
        'GVExpShip.Columns(5).ColumnEdit = repTxt
        GVExpShip.Columns(6).ColumnEdit = repDriver
        GVExpShip.Columns(7).ColumnEdit = repCar

        ' GVBuyDet.Columns(7).ColumnEdit = RepositoryItemCheckEdit1
        GVExpShip.OptionsSelection.MultiSelect = True
        GVExpShip.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub
    Private Sub FormatColumns()
        col1 = GVExpShip.Columns(1)
        col2 = GVExpShip.Columns(2)
        col3 = GVExpShip.Columns(3)
        col4 = GVExpShip.Columns(4)
        col5 = GVExpShip.Columns(5)
        col6 = GVExpShip.Columns(6)
        col7 = GVExpShip.Columns(7)
        '****************
        col1.Caption = "المنتج"
        '    col2.Caption = "المخزون"
        col3.Caption = "الكمية"
        col4.Caption = "الوحدة "
        col5.Caption = "الوزن"
        col6.Caption = "اسم السائق"
        col7.Caption = "رقم العربة"
        GVExpShip.Columns(1).Width = 70
        GVExpShip.Columns(2).Width = 30
        GVExpShip.Columns(3).Width = 30
        GVExpShip.Columns(4).Width = 40
        GVExpShip.Columns(5).Width = 30
        GVExpShip.Columns(6).Width = 90
        GVExpShip.Columns(7).Width = 50
        GVExpShip.Columns(1).OptionsColumn.ReadOnly = True
        GVExpShip.Columns(3).OptionsColumn.ReadOnly = True
        GVExpShip.Columns(4).OptionsColumn.ReadOnly = True
        GVExpShip.Columns(5).OptionsColumn.ReadOnly = True
    End Sub

    'Private Sub BtnDelete_Click(sender As Object, e As EventArgs)
    '    GVExpShip.OptionsFind.AlwaysVisible = False
    '    Dim tb As New expShipDet
    '    Dim i As Integer = 0
    '    Dim lastRow As Integer = GVExpShip.RowCount - 1
    '    Dim lastValue As Integer = Val(GVExpShip.GetRowCellValue(lastRow, "trkExShipDet"))
    '    If lastValue = 0 And GVExpShip.IsRowSelected(lastRow) = True Then
    '        GVExpShip.DeleteRow(lastRow)
    '        SavedRow = Row - 1
    '        Row = SavedRow
    '        MemAddDet = False
    '    End If
    '    If GVExpShip.SelectedRowsCount <> 0 Then

    '        Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
    '        If Msg = System.Windows.Forms.DialogResult.Yes Then
    '            While i < GVExpShip.RowCount
    '                Dim TheReq As Integer
    '                TheReq = Val(GVExpShip.GetRowCellValue(i, "trkExShipDet"))
    '                If GVExpShip.IsRowSelected(i) = True Then
    '                    tb = (From s In db.expShipDets Where s.trkExpShipDet = Val(TheReq) And s.trkExpShip = Val(TxtRId.Text) Select s).Single()
    '                    tb.delExShDet = True
    '                    GVExpShip.DeleteRow(i)
    '                    SavedRow = Row - 1
    '                    Row = SavedRow
    '                    MemAddDet = False
    '                End If
    '                i = i + 1
    '            End While
    '            db.SubmitChanges()
    '        Else
    '            Exit Sub
    '        End If
    '    End If

    'End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        If GVExpShip.SelectedRowsCount <> 0 Then
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
        If GVExpShip.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVExpShip.OptionsFind.FindFilterColumns = "*"
            GVExpShip.ShowFindPanel()
            GVExpShip.OptionsFind.ShowClearButton = False
            GVExpShip.OptionsFind.ShowFindButton = False
        End If
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVExpShip.OptionsFind.AlwaysVisible = False
        Dim tb As New expShipDet
        Dim tbReq As expShip
        Dim i As Integer = 0
        Dim lastRow As Integer = GVExpShip.RowCount - 1
        Dim lastValue As Integer = Val(GVExpShip.GetRowCellValue(lastRow, "trkExShipDet"))
        If lastValue = 0 And GVExpShip.IsRowSelected(lastRow) = True Then
            GVExpShip.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
        End If
        If GVExpShip.SelectedRowsCount <> 0 Then

            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVExpShip.RowCount
                    Dim TheReq As Integer
                    TheReq = Val(GVExpShip.GetRowCellValue(i, "trkExShipDet"))
                    If GVExpShip.IsRowSelected(i) = True Then
                        tb = (From s In db.expShipDets Where s.trkExpShipDet = Val(TheReq) And s.trkExpShip = Val(TxtRId.Text) And s.delExShDet = 0 Select s).Single()
                        tb.delExShDet = True
                        GVExpShip.DeleteRow(i)
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
        If GVExpShip.GetRowCellValue(0, "trkExpShip") = 0 And saved = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف سجل الشحن؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                tbReq = (From s In db.expShips Where s.trkToPrd = PToTrk And s.delExpShip = 0 Select s).Single
                tbReq.delExpShip = True
                db.SubmitChanges()
                Me.TxtRId.Text = ""
                Me.TxtLok.Text = ""
                TxtPeeler.Text = ""
                DateExpShip.Text = ""
                TxtPInfo.Text = ""
                Dim j As Integer = 0
                While j < GVExpShip.RowCount
                    GVExpShip.DeleteRow(j)
                End While
            End If
        End If
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
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
        rpt.FilterString = " [trkExpShip] =" & Val(TxtRId.Text)

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
        While i < GVExpShip.RowCount
            If Val(GVExpShip.GetRowCellValue(i, "trkExShipDet")) = 0 Then
                GVExpShip.SelectRows(i, i)
            End If
            i = i + 1
        End While
        If GVExpShip.SelectedRowsCount <> 0 Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ التفاصيل المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                SaveInclose = True
            Else
                SaveInclose = False
                i = 0
                While i < GVExpShip.RowCount
                    If GVExpShip.IsRowSelected(i) Then
                        GVExpShip.UnselectRow(i)
                    End If
                    i = i + 1
                End While
            End If
        Else
            SaveInclose = False
        End If
        'If Row <> 0 Then
        '    If Val(GVExpShip.GetRowCellValue(GVExpShip.RowCount - 1, "trkExShipDet")) = 0 Then
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

    Private Sub LokExp_TextChanged(sender As Object, e As EventArgs) Handles LokExp.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub
    Private Sub FillExp()
        ' done = False
        Dim lst = (From c In db.exportLocs Where c.delExp = False Select c).ToList
        LokExp.Text = ""
        Me.LokExp.Properties.DataSource = lst
        LokExp.Properties.DisplayMember = "expName"
        LokExp.Properties.ValueMember = "trkExpLoc"
        LokExp.Properties.PopulateColumns()
        LokExp.Properties.Columns(0).Visible = False
        LokExp.Properties.Columns(2).Visible = False
        ' done = True
    End Sub

    Private Sub TxtPInfo_TextChanged(sender As Object, e As EventArgs) Handles TxtPInfo.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub
End Class