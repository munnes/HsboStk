
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils
Public Class FrmArrivalProd
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
#Region "Repository Variables"
    Public repUnit As Repository.RepositoryItemLookUpEdit
    Public repTrv As New Repository.RepositoryItemTextEdit
#End Region

    Private Sub FrmAddArrive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillExp()
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
        Dim tb = (From s In db.arriveExps Where s.trkArExp = ID And s.delArExp = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokLoc.EditValue = tb.trkArival
        Me.LokExp.EditValue = tb.trkExpLoc
        Me.DateArvExp.Text = tb.arExpDate
        TxtArExpInf.Text = tb.arvExpInf
        Dim TbUn As New V_prdUnit
        '*******************************
        Dim lst = (From s In db.arriveExpDets Where s.trkArExp = ID And s.delArExDt = 0 Select s).ToList
        While i < CountView
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVArvExpDet.AddNewRow()
            TbUn = (From s In db.V_prdUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkProd = lst.Item(i).trkProd Select s).SingleOrDefault


            GVArvExpDet.SetFocusedRowCellValue("trkExShipDet", lst.Item(i).trkArExDet)
            GVArvExpDet.SetFocusedRowCellValue("trkProd", lst.Item(i).trkProd)
            GVArvExpDet.SetFocusedRowCellValue("Amount", lst.Item(i).storeAmount)
            GVArvExpDet.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVArvExpDet.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVArvExpDet.SetFocusedRowCellValue("trkDriver", lst.Item(i).Driver)
            GVArvExpDet.SetFocusedRowCellValue("trkCar", lst.Item(i).Car)
            GVArvExpDet.UpdateCurrentRow()
            i = i + 1

        End While
        trk = ID
        i = 0
        CountView = 0
        ID = 0
        saved = True
    End Sub
    Private Sub FillLoc()
        Dim lst = (From c In db.arivalLocs Where c.delAr = False Select c).ToList
        LokLoc.Text = ""
        Me.LokLoc.Properties.DataSource = lst
        LokLoc.Properties.DisplayMember = "arivalName"
        LokLoc.Properties.ValueMember = "trkArival"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
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
        GVArvExpDet.OptionsFind.FindFilterColumns = ""
        GVArvExpDet.OptionsFind.AlwaysVisible = False

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
                GVArvExpDet.AddNewRow()
            End If
        End If
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVArvExpDet.OptionsFind.AlwaysVisible = False
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
        If DateArvExp.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateArvExp.Focus()
            Exit Function
        End If
        If CType(DateArvExp.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateArvExp.Focus()
            Exit Function
        End If
        If LokExp.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً الرجاء إدخال منطقة الوصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokExp.Focus()
            LokExp.SelectAll()
            Exit Function
        End If
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال محطة الشحن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If
        If MemEdit And IsView Then
            Dim tb As New arriveExp
            Dim i As Integer = 0
            tb = (From s In db.arriveExps Where s.trkArExp = Val(TxtRId.Text) Select s).SingleOrDefault
            While i < GVArvExpDet.RowCount
                Dim lst = (From s In db.V_gdShps Where s.trkExpLok = tb.trkExpLoc _
                                                        And s.goodDate >= tb.arExpDate _
                                                          And s.trkProd = Val(GVArvExpDet.GetRowCellValue(i, "trkProd"))
                           Select s).ToList
                If lst.Count > 0 Then
                    XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بتستيف منتجات في الباخرة من نفس المنطقة في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                    Exit Function
                End If
                i = i + 1
            End While
        End If
        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New arriveExp
        Dim theDate As DateTime
        theDate = CType(DateArvExp.Text, DateTime)
        If MemAdd = True And Saved = False Then
            trk = NewReqKey()
            tb.trkArExp = trk
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.arExpDate = theDate.ToShortDateString
            tb.trkExpLoc = Val(LokExp.EditValue.ToString())
            tb.arvExpInf = TxtArExpInf.Text
            tb.delArExp = False
            db.arriveExps.InsertOnSubmit(tb)
            Saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.arriveExps Where s.trkArExp = Val(TxtRId.Text) And s.delArExp = False Select s).Single()
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.arExpDate = theDate.ToShortDateString
            tb.trkExpLoc = Val(LokExp.EditValue.ToString())
            tb.arvExpInf = TxtArExpInf.Text
            tb.delArExp = False
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
        Dim Qrymax = (From trk In db.arriveExps Select trk.trkArExp).ToList
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
        If Val(GVArvExpDet.GetRowCellValue(Ind, "trkProd")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المنتج ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArvExpDet.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVArvExpDet.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArvExpDet.SelectCell(Ind, col3)
            Exit Function
        End If

        If (GVArvExpDet.GetRowCellValue(Ind, "trkUnit")) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً الرجاء إدخال الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArvExpDet.SelectCell(Ind, col5)
            Exit Function
        End If
        If Val(GVArvExpDet.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArvExpDet.SelectCell(Ind, col4)
            Exit Function
        End If

        If GVArvExpDet.GetRowCellValue(Ind, "trkDriver") = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم السائق ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArvExpDet.SelectCell(Ind, col2)
            Exit Function
        End If
        If GVArvExpDet.GetRowCellValue(Ind, "trkCar") = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال رقم العربة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArvExpDet.SelectCell(Ind, col2)
            Exit Function
        End If

        CanSave = True
    End Function
    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVArvExpDet.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Prod As Integer = Val(GVArvExpDet.GetRowCellValue(Ind, "trkProd"))
        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVArvExpDet.GetRowCellValue(i, "trkProd")) = Prod Then
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
        Dim Qrymax = (From trk In db.arriveExpDets Select trk.trkArExDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()
        Dim tb As New arriveExpDet
        Dim TbUn As New V_prdUnit
        If MemAddDet = True Then
            GVArvExpDet.SetRowCellValue(RowInd, "trkExShipDet", NewKey())
            tb.trkArExDet = Val(GVArvExpDet.GetRowCellValue(RowInd, "trkExShipDet"))
            tb.trkArExp = Val(TxtRId.Text)
            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVArvExpDet.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkProd = Val(GVArvExpDet.GetRowCellValue(RowInd, "trkProd")) Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVArvExpDet.GetRowCellValue(RowInd, "Weight"))
            tb.Driver = GVArvExpDet.GetRowCellValue(RowInd, "trkDriver")
            tb.delArExDt = False
            tb.trkProd = Val(GVArvExpDet.GetRowCellValue(RowInd, "trkProd"))
            tb.storeAmount = Val(GVArvExpDet.GetRowCellValue(RowInd, "Amount"))
            tb.Car = GVArvExpDet.GetRowCellValue(RowInd, "trkCar")
            '***************************************
            CalculatePrdUnit(TbUn.trkUnit, Val(GVArvExpDet.GetRowCellValue(RowInd, "Amount")),
                    Val(GVArvExpDet.GetRowCellValue(RowInd, "trkProd")))
            '***************************************
            tb.untOne = UOne
            tb.amtOne = AOne
            tb.untTwo = UTwo
            tb.amtTwo = ATwo
            db.arriveExpDets.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New arriveExpDet
        Dim TbUn As New V_prdUnit
        Dim ThePrd As Integer
        If GVArvExpDet.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVArvExpDet.RowCount
                Dim TheReq As Integer
                TheReq = Val(GVArvExpDet.GetRowCellValue(i, "trkExShipDet"))
                If (GVArvExpDet.IsRowSelected(i) = True) Then
                    ThePrd = Val(GVArvExpDet.GetRowCellValue(i, "trkProd"))
                    If CheckEditDel(ThePrd) = True Then
                        If CanSave(i) = True Then
                            tb = (From s In db.arriveExpDets Where s.trkArExDet = Val(TheReq) And s.trkArExp = Val(TxtRId.Text) Select s).Single()
                            tb.trkArExp = Val(TxtRId.Text)
                            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVArvExpDet.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkProd = Val(GVArvExpDet.GetRowCellValue(i, "trkProd")) Select s).SingleOrDefault
                            tb.trkUnit = TbUn.trkUnit
                            tb.weight = Val(GVArvExpDet.GetRowCellValue(i, "Weight"))
                            tb.Driver = GVArvExpDet.GetRowCellValue(i, "trkDriver")
                            tb.Car = GVArvExpDet.GetRowCellValue(i, "trkCar")
                            tb.delArExDt = False
                            tb.trkProd = Val(GVArvExpDet.GetRowCellValue(i, "trkProd"))
                            tb.storeAmount = Val(GVArvExpDet.GetRowCellValue(i, "Amount"))
                            CalculatePrdUnit(TbUn.trkUnit, Val(GVArvExpDet.GetRowCellValue(i, "Amount")),
                            Val(GVArvExpDet.GetRowCellValue(i, "trkProd")))
                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                            GVArvExpDet.UnselectRow(i)
                        End If
                    End If
                End If
                    i = i + 1
            End While
            db.SubmitChanges()
            If GVArvExpDet.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVArvExpDet.SelectedRowsCount <> 0 Then
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

    Public Function CountDet() As Integer
        Dim lst = (From s In db.V_buyDetails Where s.delBDet = False And s.trkBuyClient = Val(TxtRId.Text) Select s).ToList
        Return lst.Count()
    End Function
    Public Sub FillGrid()
        FillProd()
        GridControl1.RepositoryItems.Add(repTxt)
        GridControl1.RepositoryItems.Add(repTrv)
        '***************** should be added her to avoid disappear when focus changed
        GridControl1.RepositoryItems.Add(repPrd)
        Dim list As BindingList(Of arrPrdDet) = New BindingList(Of arrPrdDet)

        GridControl1.DataSource = list
        GVArvExpDet.Columns(0).ColumnEdit = repTxt
        GVArvExpDet.Columns(0).Visible = False
        GVArvExpDet.Columns(1).ColumnEdit = repPrd
        GVArvExpDet.Columns(2).ColumnEdit = repTxt
        '  GVArvExpDet.Columns(3).ColumnEdit = repUnit
        GVArvExpDet.Columns(4).ColumnEdit = repTxt
        GVArvExpDet.Columns(5).ColumnEdit = repTrv
        GVArvExpDet.Columns(6).ColumnEdit = repTrv
        GVArvExpDet.OptionsSelection.MultiSelect = True
        GVArvExpDet.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub
    Private Sub FormatColumns()
        col1 = GVArvExpDet.Columns(1)
        col2 = GVArvExpDet.Columns(2)
        col3 = GVArvExpDet.Columns(3)
        col4 = GVArvExpDet.Columns(4)
        col5 = GVArvExpDet.Columns(5)
        col6 = GVArvExpDet.Columns(6)

        '****************
        col1.Caption = "المنتج"
        col2.Caption = "الكمية"
        col3.Caption = "الوحدة"
        col4.Caption = "الوزن"
        col5.Caption = "اسم السائق"
        col6.Caption = "رقم العربة"
        GVArvExpDet.Columns(1).Width = 70
        GVArvExpDet.Columns(2).Width = 30
        GVArvExpDet.Columns(3).Width = 40
        GVArvExpDet.Columns(4).Width = 40
        GVArvExpDet.Columns(5).Width = 90
        GVArvExpDet.Columns(6).Width = 60
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVArvExpDet.OptionsFind.AlwaysVisible = False
        Dim tb As New arriveExpDet
        Dim i As Integer = 0
        Dim ThePrd As Integer
        Dim lastRow As Integer = GVArvExpDet.RowCount - 1
        Dim lastValue As Integer = Val(GVArvExpDet.GetRowCellValue(lastRow, "trkExShipDet"))
        If lastValue = 0 And GVArvExpDet.IsRowSelected(lastRow) = True Then
            GVArvExpDet.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
            MemAddDet = False
        End If
        If GVArvExpDet.SelectedRowsCount <> 0 Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVArvExpDet.RowCount
                    Dim TheReq As Integer
                    TheReq = Val(GVArvExpDet.GetRowCellValue(i, "trkExShipDet"))
                    If GVArvExpDet.IsRowSelected(i) = True Then
                        ThePrd = Val(GVArvExpDet.GetRowCellValue(i, "trkProd"))
                        If CheckEditDel(ThePrd) = True Then
                            tb = (From s In db.arriveExpDets Where s.trkArExDet = Val(TheReq) And s.trkArExp = Val(TxtRId.Text) Select s).Single()
                            tb.delArExDt = True
                            GVArvExpDet.DeleteRow(i)
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
        If GVArvExpDet.SelectedRowsCount <> 0 Then
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
        If GVArvExpDet.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVArvExpDet.OptionsFind.FindFilterColumns = "*"
            GVArvExpDet.ShowFindPanel()
            GVArvExpDet.OptionsFind.ShowClearButton = False
            GVArvExpDet.OptionsFind.ShowFindButton = False
        End If
    End Sub

    Private Sub BtnPrint_Click_1(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewArvPrd
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
        rpt.FilterString = " [trkArExp] =" & Val(TxtRId.Text)
        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub

    '*****************this to edit saved data
    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        If Saved = True Then
            MemEdit = True
        End If
    End Sub
    Private Sub DateArvExp_EditValueChanged(sender As Object, e As EventArgs) Handles DateArvExp.EditValueChanged
        If Saved = True Then
            MemEdit = True
        End If
    End Sub
    Private Sub LokExp_TextChanged(sender As Object, e As EventArgs) Handles LokExp.TextChanged
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
            If Val(GVArvExpDet.GetRowCellValue(GVArvExpDet.RowCount - 1, "trkExShipDet")) = 0 Then
                'If tb.trkBuyDet = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVArvExpDet.RowCount - 1
                    GVArvExpDet.FocusedRowHandle = lastRow
                    GVArvExpDet.DeleteRow(lastRow)
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

    Private Sub TxtArExpInf_TextChanged(sender As Object, e As EventArgs) Handles TxtArExpInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub GVArvExpDet_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVArvExpDet.CustomRowCellEditForEditing
        Dim prd As Integer
        If e.Column.Caption = "المنتج" Then
            GVArvExpDet.SetFocusedRowCellValue("trkUnit", "")
        End If
        If e.Column.Caption = "الوحدة" Then
            prd = GVArvExpDet.GetRowCellValue(e.RowHandle, "trkProd")
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
    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub
    Private Function CheckEditDel(ByVal prd As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(Me.DateArvExp.Text, DateTime)
        CheckEditDel = False
        Dim lst = (From s In db.V_gdShps Where s.goodDate >= TheDate And s.trkProd = prd _
                                             And s.trkExpLok = Val(LokExp.EditValue)
                   Select s).ToList()
        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع التستيف في البواخر ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDel = True
    End Function
End Class