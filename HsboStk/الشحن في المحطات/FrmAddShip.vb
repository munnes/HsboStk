Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils

Public Class FrmAddShip
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Public curCrop As Integer
#Region "Repository Variables"
    Public repUnit As Repository.RepositoryItemLookUpEdit
    'Public repDriver As New Repository.RepositoryItemLookUpEdit
    'Public repCar As New Repository.RepositoryItemLookUpEdit
    Public repTrv As New Repository.RepositoryItemTextEdit
#End Region

    Private Sub FrmAddShip_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillRes()
        FillLoc()
        FillGrid()
        FormatColumns()
        ProgressBarControl1.Visible = False

        If IsView Then
            ViewDet()
            ' Me.Refresh()
        End If

    End Sub
    Private Sub ViewDet()
        '***************************
        Dim tb = (From s In db.shipments Where s.trkShip = ID And s.delShip = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokLoc.EditValue = tb.trkBuyLoc
        Me.LokStore.EditValue = tb.trkBStore
        Me.LokRes.EditValue = tb.trkArival
        Me.DateShip.Text = tb.shipDate
        TxtShpInf.Text = tb.shpInfo
        '******************************
        Dim i As Integer = 0
        Dim TbUn As New V_cropUnit
        Dim tbStk As New V_FinalStock
        '*******************************
        Dim lst = (From s In db.shipDetails Where s.trkShip = ID And s.delSD = 0 Select s).ToList
        While i < CountView
            '******************Total
            TbUn = (From s In db.V_cropUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkCrop = lst.Item(i).trkCrop Select s).SingleOrDefault
            '$$$$$$$$$$$$$$$$ Calculate Index
            Dim j As Integer = 0
            Dim lstInd = (From s In db.V_cropUnits Where s.trkCrop = lst.Item(i).trkCrop Select s).ToList
            If lstInd.Count = 2 Then
                While j < 2
                    If lstInd.Item(j).trkUnit = lst.Item(i).trkUnit Then
                        Exit While
                    End If
                    j = j + 1
                End While
            End If
            '$$$$$$$$$$$$$
            '  Dim CurTotal As Double = CalTotal(lst.Item(i).trkCrop, j) + lst.Item(i).storeAmount

            '**********************
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVShipDet.AddNewRow()

            GVShipDet.SetFocusedRowCellValue("trkShipDet", lst.Item(i).trkShipDet)
            GVShipDet.SetFocusedRowCellValue("trkCrop", lst.Item(i).trkCrop)
            'GVShipDet.SetFocusedRowCellValue("total", CurTotal)
            GVShipDet.SetFocusedRowCellValue("Amount", lst.Item(i).storeAmount)
            GVShipDet.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVShipDet.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVShipDet.SetFocusedRowCellValue("trkDriver", lst.Item(i).Driver)
            GVShipDet.SetFocusedRowCellValue("trkCar", lst.Item(i).Car)
            GVShipDet.UpdateCurrentRow()
            i = i + 1
        End While
        trk = ID
        i = 0
        CountView = 0
        ID = 0
        saved = True
    End Sub
    Private Sub FillRes()
        Dim lst = (From c In db.arivalLocs Where c.delAr = False Select c).ToList
        LokRes.Text = ""
        Me.LokRes.Properties.DataSource = lst
        LokRes.Properties.DisplayMember = "arivalName"
        LokRes.Properties.ValueMember = "trkArival"
        LokRes.Properties.PopulateColumns()
        LokRes.Properties.Columns(0).Visible = False
        LokRes.Properties.Columns(2).Visible = False
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
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        GVShipDet.OptionsFind.FindFilterColumns = ""
        GVShipDet.OptionsFind.AlwaysVisible = False
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
                GVShipDet.AddNewRow()
            End If
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVShipDet.OptionsFind.AlwaysVisible = False
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
        If DateShip.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateShip.Focus()
            Exit Function
        End If
        If CType(DateShip.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateShip.Focus()
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
        If LokRes.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم محطة الوصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokRes.Focus()
            LokRes.SelectAll()
            Exit Function
        End If

        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New shipment
        Dim theDate As DateTime
        theDate = CType(DateShip.Text, DateTime)
        If MemAdd = True And Saved = False Then
            trk = NewReqKey()
            tb.trkShip = trk
            tb.trkBuyLoc = Val(LokLoc.EditValue.ToString())
            tb.shipDate = theDate.ToShortDateString
            tb.trkBStore = Val(LokStore.EditValue.ToString())
            tb.trkArival = Val(LokRes.EditValue.ToString())
            tb.shpInfo = TxtShpInf.Text
            tb.delShip = False
            db.shipments.InsertOnSubmit(tb)
            Saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.shipments Where s.trkShip = Val(TxtRId.Text) And s.delShip = False Select s).Single()
            tb.trkBuyLoc = Val(LokLoc.EditValue.ToString())
            tb.shipDate = theDate.ToShortDateString
            tb.trkBStore = Val(LokStore.EditValue.ToString())
            tb.trkArival = Val(LokRes.EditValue.ToString())
            tb.shpInfo = TxtShpInf.Text
            tb.delShip = False
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
        Dim Qrymax = (From trk In db.shipments Select trk.trkShip).ToList
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
        If Val(GVShipDet.GetRowCellValue(Ind, "trkCrop")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVShipDet.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVShipDet.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVShipDet.SelectCell(Ind, col3)
            Exit Function
        End If

        If Val(GVShipDet.GetRowCellValue(Ind, "Amount")) > Val(GVShipDet.GetRowCellValue(Ind, "total")) Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الكمية أكثر من المخزون ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVShipDet.SelectCell(Ind, col3)
            Exit Function
        End If
        If (GVShipDet.GetRowCellValue(Ind, "trkUnit")) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً الرجاء إدخال الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVShipDet.SelectCell(Ind, col5)
            Exit Function
        End If
        If Val(GVShipDet.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVShipDet.SelectCell(Ind, col4)
            Exit Function
        End If

        If GVShipDet.GetRowCellValue(Ind, "trkDriver") = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم السائق ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVShipDet.SelectCell(Ind, col2)
            Exit Function
        End If
        If GVShipDet.GetRowCellValue(Ind, "trkCar") = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال رقم العربة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVShipDet.SelectCell(Ind, col2)
            Exit Function
        End If
        Dim TheDate As DateTime
        TheDate = CType(Me.DateShip.Text, DateTime)
        If MemAddDet Then
            Dim lst = (From s In db.V_shipDetails Where s.shipDate > TheDate.ToShortDateString And s.trkCrop = Val(GVShipDet.GetRowCellValue(Ind, "trkCrop")) _
                                             And s.trkBuyLoc = Val(LokLoc.EditValue) And s.trkBStore = Val(LokStore.EditValue) _
                                                  And s.delShip = 0 And s.delSD = 0
                       Select s).ToList()
            If lst.Count > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن اضافة المحصول ...تمت اضافة نفس المحصول في وقت لاحق ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
        End If
        CanSave = True
    End Function
    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVShipDet.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Crop As Integer = Val(GVShipDet.GetRowCellValue(Ind, "trkCrop"))

        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVShipDet.GetRowCellValue(i, "trkCrop")) = Crop Then
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
        Dim Qrymax = (From trk In db.shipDetails Select trk.trkShipDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()
        Dim tb As New shipDetail
        Dim TbUn As New V_cropUnit
        If MemAddDet = True Then
            GVShipDet.SetRowCellValue(RowInd, "trkShipDet", NewKey())
            tb.trkShipDet = Val(GVShipDet.GetRowCellValue(RowInd, "trkShipDet"))
            tb.trkShip = Val(TxtRId.Text)
            TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVShipDet.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkCrop = Val(GVShipDet.GetRowCellValue(RowInd, "trkCrop")) Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVShipDet.GetRowCellValue(RowInd, "Weight"))
            tb.Driver = GVShipDet.GetRowCellValue(RowInd, "trkDriver")
            tb.Car = GVShipDet.GetRowCellValue(RowInd, "trkCar")
            tb.delSD = False
            tb.trkCrop = Val(GVShipDet.GetRowCellValue(RowInd, "trkCrop"))
            tb.storeAmount = Val(GVShipDet.GetRowCellValue(RowInd, "Amount"))
            '***************************************
            CalculateUnit(TbUn.trkUnit, Val(GVShipDet.GetRowCellValue(RowInd, "Amount")),
                    Val(GVShipDet.GetRowCellValue(RowInd, "trkCrop")))

            '***************************************
            tb.untOne = UOne
            tb.amtOne = AOne
            tb.untTwo = UTwo
            tb.amtTwo = ATwo
            db.shipDetails.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New shipDetail
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        Dim TbUn As New V_cropUnit
        If GVShipDet.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVShipDet.RowCount

                If (GVShipDet.IsRowSelected(i) = True) Then
                    TheCrp = Val(GVShipDet.GetRowCellValue(i, "trkCrop"))
                    TheTrk = Val(GVShipDet.GetRowCellValue(i, "trkShipDet"))
                    If CheckEditDel(TheCrp, TheTrk) = True Then
                        If CanSave(i) = True Then
                            tb = (From s In db.shipDetails Where s.trkShipDet = Val(TheTrk) And s.trkShip = Val(TxtRId.Text) Select s).Single()
                            tb.trkShip = Val(TxtRId.Text)
                            TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVShipDet.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkCrop = Val(GVShipDet.GetRowCellValue(i, "trkCrop")) Select s).SingleOrDefault
                            tb.trkUnit = TbUn.trkUnit
                            tb.weight = Val(GVShipDet.GetRowCellValue(i, "Weight"))
                            tb.Driver = GVShipDet.GetRowCellValue(i, "trkDriver")
                            tb.Car = GVShipDet.GetRowCellValue(i, "trkCar")
                            tb.delSD = False
                            tb.trkCrop = Val(GVShipDet.GetRowCellValue(i, "trkCrop"))
                            tb.storeAmount = Val(GVShipDet.GetRowCellValue(i, "Amount"))
                            CalculateUnit(TbUn.trkUnit, Val(GVShipDet.GetRowCellValue(i, "Amount")),
                            Val(GVShipDet.GetRowCellValue(i, "trkCrop")))

                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                            GVShipDet.UnselectRow(i)
                        End If
                    End If
                End If
                    i = i + 1
            End While
            db.SubmitChanges()
            If GVShipDet.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVShipDet.SelectedRowsCount <> 0 Then
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

    'Public Sub FillDriver()
    '    Dim cl = (From s In db.drivers Where s.delD = False
    '              Select s).ToList
    '    repDriver.DataSource = cl
    '    repDriver.ValueMember = "trkDriver"
    '    repDriver.DisplayMember = "driverName"
    '    repDriver.ShowHeader = False
    '    repDriver.PopulateColumns()
    '    repDriver.Columns(0).Visible = False
    '    repDriver.Columns(2).Visible = False
    '    repDriver.Columns(3).Visible = False
    '    repDriver.Columns(4).Visible = False
    '    repDriver.NullText = ""
    'End Sub
    'Public Sub FillCar()
    '    Dim cl = (From s In db.cars Where s.delC = False
    '              Select s).ToList
    '    repCar.DataSource = cl
    '    repCar.ValueMember = "trkCar"
    '    repCar.DisplayMember = "carNo"
    '    repCar.ShowHeader = False
    '    repCar.PopulateColumns()
    '    repCar.Columns(0).Visible = False
    '    repCar.Columns(2).Visible = False
    '    repCar.NullText = ""
    'End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub

    Public Sub FillGrid()
        FillCrop()
        'FillDriver()
        'FillCar()
        GridControl1.RepositoryItems.Add(repUnit)
        GridControl1.RepositoryItems.Add(repTxt)
        GridControl1.RepositoryItems.Add(repTrv)

        '***************** should be added her to avoid disappear when focus changed
        GridControl1.RepositoryItems.Add(repCrop)
        Dim list As BindingList(Of shipDet) = New BindingList(Of shipDet)

        GridControl1.DataSource = list
        GVShipDet.Columns(0).ColumnEdit = repTxt
        GVShipDet.Columns(0).Visible = False
        GVShipDet.Columns(1).ColumnEdit = repCrop
        GVShipDet.Columns(2).ColumnEdit = repTxt
        GVShipDet.Columns(3).ColumnEdit = repTxt
        '  GVShipDet.Columns(4).ColumnEdit = repUnit
        GVShipDet.Columns(5).ColumnEdit = repTxt
        GVShipDet.Columns(6).ColumnEdit = repTrv
        GVShipDet.Columns(7).ColumnEdit = repTrv

        ' GVBuyDet.Columns(7).ColumnEdit = RepositoryItemCheckEdit1
        GVShipDet.OptionsSelection.MultiSelect = True
        GVShipDet.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub
    Private Sub FormatColumns()
        col1 = GVShipDet.Columns(1)
        col2 = GVShipDet.Columns(2)
        col3 = GVShipDet.Columns(3)
        col4 = GVShipDet.Columns(4)
        col5 = GVShipDet.Columns(5)
        col6 = GVShipDet.Columns(6)
        col7 = GVShipDet.Columns(7)
        '****************
        col1.Caption = "المحصول"
        col2.Caption = "المخزون"
        col3.Caption = "الكمية"
        col4.Caption = "الوحدة"
        col5.Caption = "الوزن"
        col6.Caption = "اسم السائق"
        col7.Caption = "رقم العربة"
        GVShipDet.Columns(1).Width = 70
        GVShipDet.Columns(2).Width = 30
        GVShipDet.Columns(3).Width = 30
        GVShipDet.Columns(4).Width = 40
        GVShipDet.Columns(5).Width = 30
        GVShipDet.Columns(6).Width = 90
        GVShipDet.Columns(7).Width = 50
        GVShipDet.Columns(2).OptionsColumn.ReadOnly = True
        GVShipDet.Columns(2).AppearanceCell.BackColor = Color.DarkRed
        GVShipDet.Columns(2).AppearanceCell.ForeColor = Color.White
        GVShipDet.Columns(2).AppearanceCell.FontSizeDelta = Font.Bold
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVShipDet.OptionsFind.AlwaysVisible = False
        Dim tb As New shipDetail
        Dim i As Integer = 0
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        Dim lastRow As Integer = GVShipDet.RowCount - 1
        Dim lastValue As Integer = Val(GVShipDet.GetRowCellValue(lastRow, "trkShipDet"))
        If lastValue = 0 And GVShipDet.IsRowSelected(lastRow) = True Then
            GVShipDet.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
            MemAddDet = False
        End If
        If GVShipDet.SelectedRowsCount <> 0 Then
            'E
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVShipDet.RowCount

                    If GVShipDet.IsRowSelected(i) = True Then
                        TheCrp = Val(GVShipDet.GetRowCellValue(i, "trkCrop"))
                        TheTrk = Val(GVShipDet.GetRowCellValue(i, "trkShipDet"))
                        If CheckEditDel(TheCrp, TheTrk) = True Then
                            Dim TheReq As Integer
                            TheReq = Val(GVShipDet.GetRowCellValue(i, "trkShipDet"))
                            tb = (From s In db.shipDetails Where s.trkShipDet = Val(TheReq) And s.trkShip = Val(TxtRId.Text) Select s).Single()
                            tb.delSD = True
                            GVShipDet.DeleteRow(i)
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
        If GVShipDet.SelectedRowsCount <> 0 Then
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
        If GVShipDet.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVShipDet.OptionsFind.FindFilterColumns = "*"
            GVShipDet.ShowFindPanel()
            GVShipDet.OptionsFind.ShowClearButton = False
            GVShipDet.OptionsFind.ShowFindButton = False
        End If
    End Sub



    '*****************this to edit saved data
    Private Sub LokStore_EditValueChanged(sender As Object, e As EventArgs) Handles LokStore.EditValueChanged
        If Saved = True Then
            MemEdit = True
        End If
        TheStk()
        'TheStock()

    End Sub


    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokStore.Text = ""
        FillStore()
        If Saved = True Then
            MemEdit = True
        End If
        TheStk()

    End Sub
    Private Sub TheStk()
        Dim count As Integer = GVShipDet.RowCount
        Dim i As Integer = 0
        Dim UndPreShip As Decimal = 0
        Dim tbShip As New V_shipDetail
        Dim Total As Decimal
        Dim TheDate As DateTime
        If DateShip.Text <> "" Then
            TheDate = CType(DateShip.Text, DateTime)
        End If
        While i < count
            Dim TbUn As New V_cropUnit
            curCrop = GVShipDet.GetRowCellValue(i, "trkCrop")

            Dim j As Integer = 0
            Dim StrUn As String = Trim(GVShipDet.GetRowCellValue(i, "trkUnit"))
            '$$$$$$$$$$$$$$$$$$$$$$$$
            If StrUn <> "" And curCrop <> 0 Then
                Dim lst = (From s In db.V_cropUnits Where s.trkCrop = curCrop Select s).ToList
                If lst.Count = 2 Then
                    While j < 2
                        If lst.Item(j).unitName = StrUn Then
                            Exit While
                        End If
                        j = j + 1
                    End While
                End If

                '$$$$$$$$$$$$$$$$$$$$$$$
                If Val(GVShipDet.GetRowCellValue(i, "trkShipDet")) <> 0 Then
                    tbShip = (From s In db.V_shipDetails Where s.trkShipDet = Val(GVShipDet.GetRowCellValue(i, "trkShipDet")) _
                                                         And s.trkCrop = curCrop And s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                         And s.trkBStore = Val(LokStore.EditValue) And s.shipDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbShip) Then
                        If j = 0 Then

                            UndPreShip = tbShip.amtOne

                        Else
                            UndPreShip = tbShip.amtTwo
                        End If

                    Else
                        UndPreShip = 0
                    End If
                    GVShipDet.SelectRow(i)
                End If
                Total = CalTotal(curCrop, j) + UndPreShip
                GVShipDet.SetRowCellValue(i, "total", Total)
                GVShipDet.SetRowCellValue(i, "Amount", UndPreShip)
            End If

            If i = count - 1 Then
                If Val(GVShipDet.GetRowCellValue(count - 1, "trkShipDet")) = 0 Then
                    GVShipDet.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewShip

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
        rpt.FilterString = " [trkShip] =" & Val(TxtRId.Text)
        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()

    End Sub

    Private Sub DateShip_EditValueChanged(sender As Object, e As EventArgs) Handles DateShip.EditValueChanged
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

        If GVShipDet.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End If
        If Row <> 0 Then
            If Val(GVShipDet.GetRowCellValue(GVShipDet.RowCount - 1, "trkShipDet")) = 0 Then
                'If tb.trkBuyDet = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVShipDet.RowCount - 1
                    GVShipDet.FocusedRowHandle = lastRow
                    GVShipDet.DeleteRow(lastRow)
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


    Private Function CalTotal(ByVal crp As Integer, ByVal UInd As Integer) As Double
        Dim TheDate As DateTime
        Dim tb As New CrpBuyResult
        If DateShip.Text <> "" Then
            TheDate = CType(DateShip.Text, DateTime)
        End If
        tb = (From s In db.CrpBuy(TheDate, crp, Val(LokLoc.EditValue), Val(LokStore.EditValue)) Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            If UInd = 0 Then
                CalTotal = tb.oneUnt
            Else
                CalTotal = tb.twoUnt

            End If
        End If
        CalTotal = Math.Round(CalTotal, 2)

    End Function

    Private Sub TxtShpInf_TextChanged(sender As Object, e As EventArgs) Handles TxtShpInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub GVShipDet_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVShipDet.CustomRowCellEditForEditing
        Dim crp As Integer
        If e.Column.Caption = "المحصول" Then
            GVShipDet.SetFocusedRowCellValue("trkUnit", "")
        End If

        If e.Column.Caption = "الوحدة" Then
            crp = GVShipDet.GetRowCellValue(e.RowHandle, "trkCrop")
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

        repUnit.NullText = ""
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

    Private Sub GVShipDet_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GVShipDet.CellValueChanged
        Dim crp As Integer = GVShipDet.GetRowCellValue(e.RowHandle, "trkCrop")
        Dim StrUn As String = Trim(GVShipDet.GetFocusedRowCellValue("trkUnit"))
        Dim TheId As Integer = GVShipDet.GetRowCellValue(e.RowHandle, "trkShipDet")
        Dim Amt As Double = GVShipDet.GetRowCellValue(e.RowHandle, "Amount")
        Dim tbShip As New V_shipDetail

        Dim Total As Double
        '  Dim TbUn As New V_cropUnit
        Dim i As Integer = 0
        If e.Column.Caption = "الوحدة" Then
            's.unitName = StrUn
            If StrUn <> "" And crp <> 0 Then
                Dim lst = (From s In db.V_cropUnits Where s.trkCrop = crp And s.delCU = 0 Select s).ToList
                If lst.Count = 2 Then
                    While i < 2
                        If lst.Item(i).unitName = StrUn Then
                            Exit While
                        End If
                        i = i + 1
                    End While
                End If

                Total = CalTotal(crp, i)
                '***************
                Dim TheDate As DateTime
                If DateShip.Text <> "" Then
                    TheDate = CType(DateShip.Text, DateTime)
                End If      '**************

                If TheId <> 0 Then
                    tbShip = (From s In db.V_shipDetails Where s.trkShipDet = TheId _
                                                         And s.trkCrop = crp And s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                         And s.trkBStore = Val(LokStore.EditValue) And s.shipDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbShip) Then

                        If i = 0 Then
                            Total = Total + tbShip.amtOne
                            GVShipDet.SetFocusedRowCellValue("Amount", tbShip.amtOne)
                        Else
                            Total = Total + tbShip.amtTwo
                            GVShipDet.SetFocusedRowCellValue("Amount", tbShip.amtTwo)
                        End If
                    End If
                End If

                GVShipDet.SetFocusedRowCellValue("total", Total)
            Else
                GVShipDet.SetFocusedRowCellValue("total", 0)
            End If
        End If

    End Sub

    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub
    Private Function CheckEditDel(ByVal crp As Integer, ByVal trk As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(Me.DateShip.Text, DateTime)
        CheckEditDel = False
        Dim lst = (From s In db.V_shipDetails Where s.shipDate >= TheDate.ToShortDateString And s.trkCrop = crp _
                                             And s.trkBuyLoc = Val(LokLoc.EditValue) And s.trkBStore = Val(LokStore.EditValue) _
                                                  And s.delShip = 0 And s.delSD = 0 And s.trkShipDet <> trk
                   Select s).ToList()
        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج ...لقد قمت بشحنة من هذا المنتج من نفس المخزن لاحقا ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDel = True
    End Function

    Private Sub GVShipDet_ColumnChanged(sender As Object, e As EventArgs) Handles GVShipDet.ColumnChanged

    End Sub
End Class


