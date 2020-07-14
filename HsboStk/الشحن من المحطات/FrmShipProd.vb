
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils
Public Class FrmShipProd
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private Flag As Integer = 0
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Public curProd As Integer
#Region "Repository Variables"
    Public repUnit As Repository.RepositoryItemLookUpEdit
    Public repTrv As New Repository.RepositoryItemTextEdit
#End Region

    Private Sub FrmShipProd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
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
        Dim tb = (From s In db.expShips Where s.trkExpShip = ID And s.delExpShip = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokLoc.EditValue = tb.trkArival
        Me.LokStore.EditValue = tb.trkAPrdStr
        Me.LokExp.EditValue = tb.trkExpLoc
        Me.DateExpShip.Text = tb.expShipDate
        TxtShpInf.Text = tb.expShpInf
        Dim TbUn As New V_prdUnit
        '*******************************

        Dim lst = (From s In db.expShipDets Where s.trkExpShip = ID And s.delExShDet = 0 Select s).ToList
        While i < CountView
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

            Dim CurTotal As Integer = CalTotal(lst.Item(i).trkProd, j) + lst.Item(i).storeAmount
            '**********************
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVExpShip.AddNewRow()

            GVExpShip.SetFocusedRowCellValue("trkExShipDet", lst.Item(i).trkExpShipDet)
            GVExpShip.SetFocusedRowCellValue("trkProd", lst.Item(i).trkProd)
            GVExpShip.SetFocusedRowCellValue("total", CurTotal)
            GVExpShip.SetFocusedRowCellValue("Amount", lst.Item(i).storeAmount)
            GVExpShip.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVExpShip.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVExpShip.SetFocusedRowCellValue("trkDriver", lst.Item(i).Driver)
            GVExpShip.SetFocusedRowCellValue("trkCar", lst.Item(i).Car)
            GVExpShip.UpdateCurrentRow()
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
    Private Sub FillStore()
        If done = True And LokLoc.Text <> "" Then
            LokStore.Properties.DataSource = Nothing
            '   LokStore.Properties.DataSource = ""
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
        GVExpShip.OptionsFind.FindFilterColumns = ""
        GVExpShip.OptionsFind.AlwaysVisible = False

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
                GVExpShip.AddNewRow()
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVExpShip.OptionsFind.AlwaysVisible = False
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
        If DateExpShip.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateExpShip.Focus()
            Exit Function
        End If
        If CType(DateExpShip.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateExpShip.Focus()
            Exit Function
        End If

        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحطة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If

        If LokStore.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم مخزن المنتجات ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokStore.Focus()
            LokStore.SelectAll()
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
        Dim tb As New expShip
        Dim theDate As DateTime
        theDate = CType(DateExpShip.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkExpShip = trk
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.expShipDate = theDate.ToShortDateString
            tb.trkAPrdStr = Val(LokStore.EditValue.ToString())
            tb.trkExpLoc = Val(LokExp.EditValue.ToString())
            tb.expShpInf = TxtShpInf.Text
            tb.trkPeeler = 0
            tb.delExpShip = False
            db.expShips.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.expShips Where s.trkExpShip = Val(TxtRId.Text) And s.delExpShip = False Select s).Single()
            tb.trkExpLoc = Val(LokLoc.EditValue.ToString())
            tb.expShipDate = theDate.ToShortDateString
            tb.expShpInf = TxtShpInf.Text
            tb.trkAPrdStr = Val(LokStore.EditValue.ToString())
            tb.trkArival = Val(LokExp.EditValue.ToString())
            tb.trkPeeler = 0
            tb.delExpShip = False
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
        If IsSingleRow(Ind) = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً لقد قمت بادخال المحصول  مسبقا  يمكنك التعديل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        If Val(GVExpShip.GetRowCellValue(Ind, "trkProd")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المنتج ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVExpShip.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVExpShip.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVExpShip.SelectCell(Ind, col3)
            Exit Function
        End If

        If Val(GVExpShip.GetRowCellValue(Ind, "Amount")) > Val(GVExpShip.GetRowCellValue(Ind, "total")) Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الكمية أكثر من المخزون ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVExpShip.SelectCell(Ind, col3)
            Exit Function
        End If
        If (GVExpShip.GetRowCellValue(Ind, "trkUnit")) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً الرجاء إدخال الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVExpShip.SelectCell(Ind, col5)
            Exit Function
        End If
        If Val(GVExpShip.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVExpShip.SelectCell(Ind, col4)
            Exit Function
        End If

        If GVExpShip.GetRowCellValue(Ind, "trkDriver") = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم السائق ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVExpShip.SelectCell(Ind, col2)
            Exit Function
        End If
        If GVExpShip.GetRowCellValue(Ind, "trkCar") = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال رقم العربة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVExpShip.SelectCell(Ind, col2)
            Exit Function
        End If
        If MemAddDet Then
            Dim TheDate As DateTime
            TheDate = CType(Me.DateExpShip.Text, DateTime)
            Dim countLst As Integer
            Dim lst = (From s In db.V_expShipDets Where s.expShipDate >= TheDate.ToShortDateString And s.trkProd = Val(GVExpShip.GetRowCellValue(Ind, "trkPrd")) _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkAPrdStr = Val(LokStore.EditValue)
                       Select s).ToList()
            countLst = lst.Count
            If countLst > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن اضافة المنتج ...تمت اضافة نفس المنتج في وقت لاحق ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
        End If

        CanSave = True
    End Function
    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVExpShip.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Prod As Integer = Val(GVExpShip.GetRowCellValue(Ind, "trkProd"))

        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVExpShip.GetRowCellValue(i, "trkProd")) = Prod Then
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
        Dim Qrymax = (From trk In db.expShipDets Select trk.trkExpShipDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()
        Dim tb As New expShipDet
        Dim TbUn As New V_prdUnit
        If MemAddDet = True Then
            GVExpShip.SetRowCellValue(RowInd, "trkExShipDet", NewKey())
            tb.trkExpShipDet = Val(GVExpShip.GetRowCellValue(RowInd, "trkExShipDet"))
            tb.trkExpShip = Val(TxtRId.Text)
            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVExpShip.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkProd = Val(GVExpShip.GetRowCellValue(RowInd, "trkProd"))
                    Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVExpShip.GetRowCellValue(RowInd, "Weight"))
            tb.Driver = GVExpShip.GetRowCellValue(RowInd, "trkDriver")
            tb.Car = GVExpShip.GetRowCellValue(RowInd, "trkCar")
            tb.delExShDet = False
            tb.trkProd = Val(GVExpShip.GetRowCellValue(RowInd, "trkProd"))
            tb.storeAmount = Val(GVExpShip.GetRowCellValue(RowInd, "Amount"))
            '***************************************
            CalculatePrdUnit(TbUn.trkUnit, Val(GVExpShip.GetRowCellValue(RowInd, "Amount")),
                    Val(GVExpShip.GetRowCellValue(RowInd, "trkProd")))

            '***************************************
            tb.untOne = UOne
            tb.amtOne = AOne
            tb.untTwo = UTwo
            tb.amtTwo = ATwo
            db.expShipDets.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim ThePrd As Integer
        Dim TheTrk As Integer
        Dim tb As New expShipDet
        Dim TbUn As New V_prdUnit
        If GVExpShip.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVExpShip.RowCount

                If (GVExpShip.IsRowSelected(i) = True) Then
                    ThePrd = Val(GVExpShip.GetRowCellValue(i, "trkProd"))
                    TheTrk = Val(GVExpShip.GetRowCellValue(i, "trkExShipDet"))
                    If CheckEditDel(ThePrd, TheTrk) = True Then
                        If CanSave(i) = True Then
                            tb = (From s In db.expShipDets Where s.trkExpShipDet = Val(TheTrk) And s.trkExpShip = Val(TxtRId.Text) Select s).Single()
                            tb.trkExpShip = Val(TxtRId.Text)
                            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVExpShip.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkProd = Val(GVExpShip.GetRowCellValue(i, "trkProd"))
                                    Select s).SingleOrDefault
                            tb.trkUnit = TbUn.trkUnit
                            tb.weight = Val(GVExpShip.GetRowCellValue(i, "Weight"))
                            tb.Driver = Val(GVExpShip.GetRowCellValue(i, "trkDriver"))
                            tb.Car = GVExpShip.GetRowCellValue(i, "trkCar")
                            tb.delExShDet = False
                            tb.trkProd = Val(GVExpShip.GetRowCellValue(i, "trkProd"))
                            tb.storeAmount = Val(GVExpShip.GetRowCellValue(i, "Amount"))
                            CalculatePrdUnit(TbUn.trkUnit, Val(GVExpShip.GetRowCellValue(i, "Amount")),
                          Val(GVExpShip.GetRowCellValue(i, "trkProd")))

                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                            GVExpShip.UnselectRow(i)
                        End If
                    End If
                End If
                    i = i + 1
            End While
            db.SubmitChanges()
            If GVExpShip.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVExpShip.SelectedRowsCount <> 0 Then
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

        GridControl1.RepositoryItems.Add(repTxt)

        GridControl1.RepositoryItems.Add(repTrv)
        '***************** should be added her to avoid disappear when focus changed
        GridControl1.RepositoryItems.Add(repPrd)
        Dim list As BindingList(Of expoProd) = New BindingList(Of expoProd)

        GridControl1.DataSource = list
        GVExpShip.Columns(0).ColumnEdit = repTxt
        GVExpShip.Columns(0).Visible = False
        GVExpShip.Columns(1).ColumnEdit = repPrd
        GVExpShip.Columns(2).ColumnEdit = repTxt
        GVExpShip.Columns(3).ColumnEdit = repTxt
        ' GVExpShip.Columns(4).ColumnEdit = repUnit
        GVExpShip.Columns(5).ColumnEdit = repTxt
        GVExpShip.Columns(6).ColumnEdit = repTrv
        GVExpShip.Columns(7).ColumnEdit = repTrv

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
        col2.Caption = "المخزون"
        col3.Caption = "الكمية"
        col4.Caption = "الوحدة"
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
        GVExpShip.Columns(2).OptionsColumn.ReadOnly = True
        GVExpShip.Columns(2).AppearanceCell.BackColor = Color.DarkRed
        GVExpShip.Columns(2).AppearanceCell.ForeColor = Color.White
        GVExpShip.Columns(2).AppearanceCell.FontSizeDelta = Font.Bold
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVExpShip.OptionsFind.AlwaysVisible = False
        Dim tb As New expShipDet
        Dim i As Integer = 0
        Dim ThePrd As Integer
        Dim TheTrk As Integer
        Dim lastRow As Integer = GVExpShip.RowCount - 1
        Dim lastValue As Integer = Val(GVExpShip.GetRowCellValue(lastRow, "trkExShipDet"))
        If lastValue = 0 And GVExpShip.IsRowSelected(lastRow) = True Then
            GVExpShip.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
            MemAddDet = False
        End If
        If GVExpShip.SelectedRowsCount <> 0 Then

            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVExpShip.RowCount
                    If GVExpShip.IsRowSelected(i) = True Then
                        ThePrd = Val(GVExpShip.GetRowCellValue(i, "trkProd"))
                        TheTrk = Val(GVExpShip.GetRowCellValue(i, "trkExShipDet"))
                        If CheckEditDel(ThePrd, TheTrk) = True Then
                            tb = (From s In db.expShipDets Where s.trkExpShipDet = Val(TheTrk) And s.trkExpShip = Val(TxtRId.Text) Select s).Single()
                            tb.delExShDet = True
                            GVExpShip.DeleteRow(i)
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
        If GVExpShip.SelectedRowsCount <> 0 Then
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
        If GVExpShip.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVExpShip.OptionsFind.FindFilterColumns = "*"
            GVExpShip.ShowFindPanel()
            GVExpShip.OptionsFind.ShowClearButton = False
            GVExpShip.OptionsFind.ShowFindButton = False
        End If
    End Sub
    '*****************this to edit saved data

    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokStore.Text = ""
        FillStore()
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
    Private Sub TheStk()
        Dim count As Integer = GVExpShip.RowCount
        Dim i As Integer = 0
        Dim UndPreShip As Double = 0
        Dim tbShip As New V_expShipDet
        Dim Total As Double
        Dim TheDate As DateTime
        If Me.DateExpShip.Text <> "" Then
            TheDate = CType(DateExpShip.Text, DateTime)
        End If
        While i < count
            Dim TbUn As New V_prdUnit
            curProd = GVExpShip.GetRowCellValue(i, "trkProd")

            Dim j As Integer = 0
            Dim StrUn As String = Trim(GVExpShip.GetRowCellValue(i, "trkUnit"))
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

                If Val(GVExpShip.GetRowCellValue(i, "trkExShipDet")) <> 0 Then
                    tbShip = (From s In db.V_expShipDets Where s.trkExpShipDet = Val(GVExpShip.GetRowCellValue(i, "trkExShipDet")) _
                                                     And s.trkProd = Val(GVExpShip.GetRowCellValue(i, "trkProd")) And s.trkArival = Val(LokLoc.EditValue) _
                                                     And s.trkAPrdStr = Val(LokStore.EditValue) And s.expShipDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbShip) Then
                        UndPreShip = tbShip.storeAmount
                    Else
                        UndPreShip = 0
                    End If
                    GVExpShip.SelectRow(i)
                End If
                Total = CalTotal(curProd, j) + UndPreShip
                GVExpShip.SetRowCellValue(i, "total", Total)

            End If

            If i = count - 1 Then
                If Val(GVExpShip.GetRowCellValue(count - 1, "trkExShipDet")) = 0 Then
                    GVExpShip.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub

    Private Sub BtnPrint_Click_1(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewShpPrd

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
        rpt.FilterString = " [trkExpShip] =" & Val(TxtRId.Text)
        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()

    End Sub

    Private Function CalTotal(ByVal prd As Integer, ByVal UInd As Integer) As Double
        Dim TheDate As DateTime
        If Me.DateExpShip.Text <> "" Then
            TheDate = CType(DateExpShip.Text, DateTime)
        End If
        Dim tb As New PrdArvResult
        tb = (From s In db.PrdArv(TheDate, Val(LokLoc.EditValue), Val(LokStore.EditValue), prd) Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            If UInd = 0 Then
                CalTotal = tb.oneUnt
            Else
                CalTotal = tb.twoUnt

            End If
        End If
        CalTotal = Math.Round(CalTotal, 2)
    End Function

    Private Sub Datebuy_EditValueChanged(sender As Object, e As EventArgs) Handles DateExpShip.EditValueChanged
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

        If GVExpShip.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End If
        If Row <> 0 Then
            If Val(GVExpShip.GetRowCellValue(GVExpShip.RowCount - 1, "trkExShipDet")) = 0 Then
                'If tb.trkBuyDet = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVExpShip.RowCount - 1
                    GVExpShip.FocusedRowHandle = lastRow
                    GVExpShip.DeleteRow(lastRow)
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

    Private Sub TxtShpInf_TextChanged(sender As Object, e As EventArgs) Handles TxtShpInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub GVExpShip_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVExpShip.CustomRowCellEditForEditing
        Dim prd As Integer
        If e.Column.Caption = "المنتج" Then
            GVExpShip.SetFocusedRowCellValue("trkUnit", "")
        End If
        If e.Column.Caption = "الوحدة" Then
            prd = GVExpShip.GetRowCellValue(e.RowHandle, "trkProd")
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

    Private Sub GVExpShip_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GVExpShip.CellValueChanged
        Dim prd As Integer = GVExpShip.GetRowCellValue(e.RowHandle, "trkProd")
        Dim StrUn As String = Trim(GVExpShip.GetFocusedRowCellValue("trkUnit"))
        Dim TheId As Integer = GVExpShip.GetRowCellValue(e.RowHandle, "trkExShipDet")
        Dim Amt As Double = GVExpShip.GetRowCellValue(e.RowHandle, "Amount")
        Dim Total As Double
        Dim tbShip As New V_expShipDet
        Dim TbUn As New V_prdUnit
        Dim i As Integer = 0
        Dim TheDate As DateTime
        If Me.DateExpShip.Text <> "" Then
            TheDate = CType(DateExpShip.Text, DateTime)
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
                    tbShip = (From s In db.V_expShipDets Where s.trkExpShipDet = Val(TheId) _
                                                     And s.trkProd = Val(prd) And s.trkArival = Val(LokLoc.EditValue) _
                                                     And s.trkAPrdStr = Val(LokStore.EditValue) And s.expShipDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbShip) Then
                        If i = 0 Then
                            Total = Total + tbShip.amtOne
                            GVExpShip.SetFocusedRowCellValue("Amount", tbShip.amtOne)
                        Else
                            Total = Total + tbShip.amtTwo
                        GVExpShip.SetFocusedRowCellValue("Amount", tbShip.amtTwo)
                    End If

                    End If
                End If
                GVExpShip.SetFocusedRowCellValue("total", Total)
            Else
                GVExpShip.SetFocusedRowCellValue("total", 0)
            End If
        End If
    End Sub

    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub
    Private Function CheckEditDel(ByVal prd As Integer, ByVal trk As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(Me.DateExpShip.Text, DateTime)

        CheckEditDel = False
        '************where del=0
        Dim lst = (From s In db.V_expShipDets Where s.expShipDate >= TheDate.ToShortDateString And s.trkProd = prd _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkAPrdStr = Val(LokStore.EditValue) _
                                               And s.trkExpShipDet <> trk
                   Select s).ToList()

        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج ...سيأثر على شحنات لاحقة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDel = True
    End Function

    Private Sub LokStore_EditValueChanged(sender As Object, e As EventArgs) Handles LokStore.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
End Class