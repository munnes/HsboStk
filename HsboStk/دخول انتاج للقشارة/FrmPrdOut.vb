Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils
Public Class FrmPrdOut
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Public curCrop As Integer
#Region "Repository Variables"
    Public repUnit As Repository.RepositoryItemLookUpEdit
    '  Public repTxt As New Repository.RepositoryItemTextEdit

#End Region
    Private Sub FrmPrdOut_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        RdoLocal.Checked = True
        LokClient.Enabled = False
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
        Dim tb = (From s In db.outPrds Where s.trkOutPrd = ID And s.delOPrd = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokLoc.EditValue = tb.trkArival
        Me.LokStore.EditValue = tb.trkAStore
        Me.LokPeeler.EditValue = tb.trkPeeler
        If tb.isLocal = False Then
            RdoClient.Checked = True
            LokClient.EditValue = tb.trkClntCrp
            LokClient.Enabled = True
            RdoLocal.Enabled = False
            RdoClient.Enabled = True
        Else
            RdoLocal.Checked = True
            LokClient.Enabled = False
            RdoClient.Enabled = False
        End If
        Me.DateOut.Text = tb.oPrdDate
        TxtOutInf.Text = tb.outInfo
        Dim TbUn As New V_cropUnit
        'Dim tbStk As New V_FinalOutStk
        '*******************************
        Dim lst = (From s In db.outPrdDets Where s.trkOutPrd = ID And s.delPrd = 0 Select s).ToList
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
            Dim CurTotal As Integer = CalTotal(lst.Item(i).trkCrop, j) + lst.Item(i).amount
            '**********************
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVPrdOut.AddNewRow()

            GVPrdOut.SetFocusedRowCellValue("trkPrdDet", lst.Item(i).trkPrdDet)
            GVPrdOut.SetFocusedRowCellValue("trkCrop", lst.Item(i).trkCrop)
            GVPrdOut.SetFocusedRowCellValue("total", CurTotal)
            GVPrdOut.SetFocusedRowCellValue("Amount", lst.Item(i).amount)
            GVPrdOut.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVPrdOut.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVPrdOut.UpdateCurrentRow()
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
            Dim lst = (From c In db.arivalStores Where c.delSa = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokStore.Properties.DataSource = lst
            LokStore.Properties.DisplayMember = "AStore"
            LokStore.Properties.ValueMember = "trkAStore"
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


    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        GVPrdOut.OptionsFind.FindFilterColumns = ""
        GVPrdOut.OptionsFind.AlwaysVisible = False

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
                GVPrdOut.AddNewRow()
            End If
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVPrdOut.OptionsFind.AlwaysVisible = False
        If CanSaveReq() = True Then
            If Trim(TxtRId.Text) = "" Then
                MemAdd = True
            End If
            If MemAdd Or MemEdit Then
                SaveReqData()

            End If
            If IsDet = False Then
                IsDet = True
                Exit Sub
            End If
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
        If DateOut.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateOut.Focus()
            Exit Function
        End If
        If CType(DateOut.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateOut.Focus()
            Exit Function
        End If
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحطة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
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
        If LokPeeler.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم القشارة\الغربال ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokPeeler.Focus()
            LokPeeler.SelectAll()
            Exit Function
        End If
        If RdoLocal.Checked = False And RdoClient.Checked = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء اختيار مصدر المحاصيل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        If RdoClient.Checked = True Then
            If LokClient.Text = "" Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء ادخال اسم العميل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
        End If
        If MemEdit And IsView Then
            Dim tb As New outPrd
            Dim tbShp As New V_shipDetail
            Dim i As Integer = 0
            tb = (From s In db.outPrds Where s.trkOutPrd = Val(TxtRId.Text) Select s).SingleOrDefault
            While i < GVPrdOut.RowCount
                Dim lst = (From s In db.V_ToPrds Where s.trkPeeler = tb.trkPeeler _
                                                          And s.trkArival = tb.trkArival _
                                                        And s.toPrdDate >= tb.oPrdDate _
                                                          And s.trkCrop = Val(GVPrdOut.GetRowCellValue(i, "trkCrop")) _
                                                     And s.isLocal = tb.isLocal And s.trkClntCrp = tb.trkClntCrp
                           Select s).ToList
                If lst.Count > 0 Then
                    XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بمعالجة محاصيل في القشارة\الغربال في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                    Exit Function
                End If
                i = i + 1
            End While
        End If
        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New outPrd
        Dim theDate As DateTime
        theDate = CType(DateOut.Text, DateTime)
        If MemAdd = True And Saved = False Then
            trk = NewReqKey()
            tb.trkOutPrd = trk
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.oPrdDate = theDate.ToShortDateString
            tb.trkAStore = Val(LokStore.EditValue.ToString())
            tb.trkPeeler = Val(LokPeeler.EditValue.ToString())
            tb.outInfo = TxtOutInf.Text
            tb.delOPrd = False
            tb.isLocal = CalFlag()
            If RdoClient.Checked = True Then
                tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            Else
                tb.trkClntCrp = 0
            End If
            db.outPrds.InsertOnSubmit(tb)
            Saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.outPrds Where s.trkOutPrd = Val(TxtRId.Text) And s.delOPrd = False Select s).Single()
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.oPrdDate = theDate.ToShortDateString
            tb.trkAStore = Val(LokStore.EditValue.ToString())
            tb.trkPeeler = Val(LokPeeler.EditValue.ToString())
            tb.outInfo = TxtOutInf.Text
            tb.isLocal = CalFlag()
            If RdoClient.Checked = True Then
                tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            Else
                tb.trkClntCrp = 0
            End If
            tb.delOPrd = False
            db.SubmitChanges()
            Progress()
        End If
        TxtRId.Text = trk
    End Sub
    Public Function CalFlag() As Boolean
        If RdoLocal.Checked = True Then
            CalFlag = True
        ElseIf RdoClient.Checked = True
            CalFlag = False
        End If
    End Function
    Private Sub Progress()
        ProgressBarControl1.Visible = True
        Me.ProgressBarControl1.EditValue = 0
        Me.Timer1.Start()
    End Sub
    Function NewReqKey() As Integer
        Dim TheKey As Integer
        Dim Qrymax = (From trk In db.outPrds Select trk.trkOutPrd).ToList
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
        If Val(GVPrdOut.GetRowCellValue(Ind, "trkCrop")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVPrdOut.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVPrdOut.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVPrdOut.SelectCell(Ind, col3)
            Exit Function
        End If

        If Val(GVPrdOut.GetRowCellValue(Ind, "Amount")) > Val(GVPrdOut.GetRowCellValue(Ind, "total")) Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الكمية أكثر من المخزون ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVPrdOut.SelectCell(Ind, col3)
            Exit Function
        End If
        If (GVPrdOut.GetRowCellValue(Ind, "trkUnit")) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً الرجاء إدخال الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVPrdOut.SelectCell(Ind, col5)
            Exit Function
        End If
        If Val(GVPrdOut.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVPrdOut.SelectCell(Ind, col4)
            Exit Function
        End If

        Dim TheDate As DateTime
        TheDate = CType(Me.DateOut.Text, DateTime)
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = 1
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = 0
            Clnt = Val(LokClient.EditValue)
        End If
        If MemAddDet Then
            Dim countLst As Integer
            Dim lst = (From s In db.V_OutPrdDets Where s.oPrdDate > TheDate.ToShortDateString And s.trkCrop = Val(GVPrdOut.GetRowCellValue(Ind, "trkCrop")) _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkAStore = Val(LokStore.EditValue) _
                                                And s.isLocal = isLoc And s.trkClntCrp = Clnt
                       Select s).ToList()
            countLst = lst.Count
            If countLst > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن اضافة المحصول ...تمت اضافة نفس المحصول في وقت لاحق ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
        End If

        CanSave = True
    End Function
    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVPrdOut.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Crop As Integer = Val(GVPrdOut.GetRowCellValue(Ind, "trkCrop"))

        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVPrdOut.GetRowCellValue(i, "trkCrop")) = Crop Then
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
        Dim Qrymax = (From trk In db.outPrdDets Select trk.trkPrdDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()
        Dim tb As New outPrdDet
        Dim TbUn As New V_cropUnit
        If MemAddDet = True Then
            GVPrdOut.SetRowCellValue(RowInd, "trkPrdDet", NewKey())
            tb.trkPrdDet = Val(GVPrdOut.GetRowCellValue(RowInd, "trkPrdDet"))
            tb.trkOutPrd = Val(TxtRId.Text)
            TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVPrdOut.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkCrop = Val(GVPrdOut.GetRowCellValue(RowInd, "trkCrop")) Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVPrdOut.GetRowCellValue(RowInd, "Weight"))
            tb.delPrd = False
            tb.trkCrop = Val(GVPrdOut.GetRowCellValue(RowInd, "trkCrop"))
            tb.amount = Val(GVPrdOut.GetRowCellValue(RowInd, "Amount"))
            '***************************************
            CalculateUnit(TbUn.trkUnit, Val(GVPrdOut.GetRowCellValue(RowInd, "Amount")),
                    Val(GVPrdOut.GetRowCellValue(RowInd, "trkCrop")))

            '***************************************
            tb.untOne = UOne
            tb.amtOne = AOne
            tb.untTwo = UTwo
            tb.amtTwo = ATwo
            db.outPrdDets.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New outPrdDet
        Dim TbUn As New V_cropUnit
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        If GVPrdOut.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVPrdOut.RowCount

                If (GVPrdOut.IsRowSelected(i) = True) Then
                    TheCrp = Val(GVPrdOut.GetRowCellValue(i, "trkCrop"))
                    TheTrk = Val(GVPrdOut.GetRowCellValue(i, "trkPrdDet"))
                    If CheckEditDel(TheCrp) = True Then
                        If CheckEditDelSelf(TheCrp, TheTrk) Then
                            If CanSave(i) = True Then
                                tb = (From s In db.outPrdDets Where s.trkPrdDet = Val(TheTrk) And s.trkOutPrd = Val(TxtRId.Text) Select s).Single()
                                tb.trkOutPrd = Val(TxtRId.Text)
                                TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVPrdOut.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkCrop = Val(GVPrdOut.GetRowCellValue(i, "trkCrop")) Select s).SingleOrDefault
                                tb.trkUnit = TbUn.trkUnit
                                tb.weight = Val(GVPrdOut.GetRowCellValue(i, "Weight"))
                                tb.delPrd = False
                                tb.trkCrop = Val(GVPrdOut.GetRowCellValue(i, "trkCrop"))
                                tb.amount = Val(GVPrdOut.GetRowCellValue(i, "Amount"))
                                CalculateUnit(TbUn.trkUnit, Val(GVPrdOut.GetRowCellValue(i, "Amount")),
                            Val(GVPrdOut.GetRowCellValue(i, "trkCrop")))

                                tb.untOne = UOne
                                tb.amtOne = AOne
                                tb.untTwo = UTwo
                                tb.amtTwo = ATwo
                                GVPrdOut.UnselectRow(i)
                            End If
                        End If
                    End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVPrdOut.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVPrdOut.SelectedRowsCount <> 0 Then
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

    Public Function CountDet() As Integer
        Dim lst = (From s In db.V_buyDetails Where s.delBDet = False And s.trkBuyClient = Val(TxtRId.Text) Select s).ToList
        Return lst.Count()
    End Function
    Public Sub FillGrid()
        FillCrop()
        '   FillUnit()

        GridControl1.RepositoryItems.Add(repUnit)
        GridControl1.RepositoryItems.Add(repTxt)
        '***************** should be added her to avoid disappear when focus changed
        GridControl1.RepositoryItems.Add(repCrop)

        Dim list As BindingList(Of outProduct) = New BindingList(Of outProduct)

        GridControl1.DataSource = list
        GVPrdOut.Columns(0).ColumnEdit = repTxt
        GVPrdOut.Columns(0).Visible = False
        GVPrdOut.Columns(1).ColumnEdit = repCrop
        GVPrdOut.Columns(2).ColumnEdit = repTxt
        GVPrdOut.Columns(3).ColumnEdit = repTxt
        '  GVPrdOut.Columns(4).ColumnEdit = repUnit
        GVPrdOut.Columns(5).ColumnEdit = repTxt

        GVPrdOut.OptionsSelection.MultiSelect = True
        GVPrdOut.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub
    Private Sub FormatColumns()
        col1 = GVPrdOut.Columns(1)
        col2 = GVPrdOut.Columns(2)
        col3 = GVPrdOut.Columns(3)
        col4 = GVPrdOut.Columns(4)
        col5 = GVPrdOut.Columns(5)

        '****************
        col1.Caption = "المحصول"
        col2.Caption = "المخزون"
        col3.Caption = "الكمية"
        col4.Caption = "الوحدة"
        col5.Caption = "الوزن"

        GVPrdOut.Columns(2).OptionsColumn.ReadOnly = True
        GVPrdOut.Columns(2).AppearanceCell.BackColor = Color.DarkRed
        GVPrdOut.Columns(2).AppearanceCell.ForeColor = Color.White
        GVPrdOut.Columns(2).AppearanceCell.FontSizeDelta = Font.Bold
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVPrdOut.OptionsFind.AlwaysVisible = False
        Dim tb As New outPrdDet
        Dim i As Integer = 0
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        Dim lastRow As Integer = GVPrdOut.RowCount - 1
        Dim lastValue As Integer = Val(GVPrdOut.GetRowCellValue(lastRow, "trkPrdDet"))
        If lastValue = 0 And GVPrdOut.IsRowSelected(lastRow) = True Then
            GVPrdOut.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
            MemAddDet = False
        End If
        If GVPrdOut.SelectedRowsCount <> 0 Then
            'E
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVPrdOut.RowCount

                    If GVPrdOut.IsRowSelected(i) = True Then
                        TheCrp = Val(GVPrdOut.GetRowCellValue(i, "trkCrop"))
                        TheTrk = Val(GVPrdOut.GetRowCellValue(i, "trkPrdDet"))
                        If CheckEditDel(TheCrp) = True Then
                            If CheckEditDelSelf(TheCrp, TheTrk) Then
                                tb = (From s In db.outPrdDets Where s.trkPrdDet = Val(TheTrk) And s.trkOutPrd = Val(TxtRId.Text) Select s).Single()
                                tb.delPrd = True
                                GVPrdOut.DeleteRow(i)
                                i = i - 1
                                SavedRow = Row - 1
                                Row = SavedRow
                                MemAddDet = False
                            End If
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
        If GVPrdOut.SelectedRowsCount <> 0 Then
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
        If GVPrdOut.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVPrdOut.OptionsFind.FindFilterColumns = "*"
            GVPrdOut.ShowFindPanel()
            GVPrdOut.OptionsFind.ShowClearButton = False
            GVPrdOut.OptionsFind.ShowFindButton = False
        End If
    End Sub


    '*****************this to edit saved data
    Private Sub LokStore_EditValueChanged(sender As Object, e As EventArgs) Handles LokStore.EditValueChanged
        If Saved = True Then
            MemEdit = True
        End If
        '************************************
        TheStk()
    End Sub

    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokStore.Text = ""
        LokPeeler.Text = ""
        LokClient.Text = ""
        FillStore()
        FillPeeler()
        FillClient()
        If Saved = True Then
            MemEdit = True
        End If
        '**********************************
        TheStk()
    End Sub
    Private Sub FillClient()
        If done = True And LokLoc.Text <> "" Then
            LokClient.Properties.DataSource = ""
            Dim lst = (From c In db.clientCrps Where c.delClntCrp = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokClient.Properties.DataSource = lst
            LokClient.Properties.DisplayMember = "clntCrpName"
            LokClient.Properties.ValueMember = "trkClntCrp"
            LokClient.Properties.PopulateColumns()
            LokClient.Properties.Columns(0).Visible = False
            LokClient.Properties.Columns(2).Visible = False
            LokClient.Properties.Columns(3).Visible = False
            LokClient.Properties.Columns(4).Visible = False
            LokClient.Properties.Columns(5).Visible = False
        End If
    End Sub
    Private Sub TheStk()
        Dim count As Integer = GVPrdOut.RowCount
        Dim i As Integer = 0
        Dim UndPreShip As Double = 0
        Dim tbOutPrd As New V_OutPrdDet
        Dim Total As Double
        '****
        Dim isLoc As Boolean
        Dim Clnt As Integer
        '*********************
        Dim TheDate As DateTime
        If DateOut.Text <> "" Then
            TheDate = CType(DateOut.Text, DateTime)
        End If
        While i < count
            Dim TbUn As New V_cropUnit
            Dim j As Integer = 0
            Dim StrUn As String = Trim(GVPrdOut.GetRowCellValue(i, "trkUnit"))
            curCrop = GVPrdOut.GetRowCellValue(i, "trkCrop")
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

                If Val(GVPrdOut.GetRowCellValue(i, "trkPrdDet")) <> 0 Then

                    If RdoLocal.Checked = True Then
                        isLoc = True
                        Clnt = 0
                    ElseIf RdoClient.Checked = True
                        isLoc = False
                        Clnt = Val(LokClient.EditValue)
                    End If
                    tbOutPrd = (From s In db.V_OutPrdDets Where s.trkPrdDet = Val(GVPrdOut.GetRowCellValue(i, "trkPrdDet")) _
                                                          And s.trkCrop = curCrop And s.trkArival = Val(LokLoc.EditValue) _
                                                         And s.trkAStore = Val(LokStore.EditValue) _
                                                              And s.isLocal = isLoc And s.trkClntCrp = Clnt _
                                                              And s.oPrdDate <= TheDate
                                Select s).SingleOrDefault()

                    If Not IsNothing(tbOutPrd) Then
                        UndPreShip = tbOutPrd.amount
                    Else
                        UndPreShip = 0
                    End If
                    GVPrdOut.SelectRow(i)
                End If
                Total = CalTotal(curCrop, j) + UndPreShip
                GVPrdOut.SetRowCellValue(i, "total", Total)
            End If

            If i = count - 1 Then
                If Val(GVPrdOut.GetRowCellValue(count - 1, "trkPrdDet")) = 0 Then
                    GVPrdOut.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub

    Private Sub DateOut_EditValueChanged(sender As Object, e As EventArgs) Handles DateOut.EditValueChanged
        If Saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
    Private Sub LokPeeler_TextChanged(sender As Object, e As EventArgs) Handles LokPeeler.TextChanged
        If Saved = True Then
            MemEdit = True
        End If

    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewPrdOut

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
        rpt.FilterString = " [trkOutPrd] =" & Val(TxtRId.Text)
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

        If GVPrdOut.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لم يتم حفظ السجلات المعدلة  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        End If
        If Row <> 0 Then
            If Val(GVPrdOut.GetRowCellValue(GVPrdOut.RowCount - 1, "trkPrdDet")) = 0 Then
                'If tb.trkBuyDet = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVPrdOut.RowCount - 1
                    GVPrdOut.FocusedRowHandle = lastRow
                    GVPrdOut.DeleteRow(lastRow)
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
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = 1
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = 0
            Clnt = Val(LokClient.EditValue)
        End If
        Dim TheDate As DateTime

        If DateOut.Text <> "" Then
            TheDate = CType(DateOut.Text, DateTime)
        End If
        Dim tb As New CrpArvResult
        tb = (From s In db.CrpArv(TheDate, Val(LokLoc.EditValue), Val(LokStore.EditValue), crp, isLoc, Clnt) Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            If UInd = 0 Then
                CalTotal = tb.oneUnt
            Else
                CalTotal = tb.twoUnt

            End If
        End If
        CalTotal = Math.Round(CalTotal, 2)
    End Function


    Private Sub TxtOutInf_TextChanged(sender As Object, e As EventArgs) Handles TxtOutInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If

    End Sub
    Private Function CheckEditDel(ByVal crp As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(DateOut.Text, DateTime)
        CheckEditDel = False
        Dim lst = (From s In db.toPrds Where s.toPrdDate >= TheDate.ToShortDateString And s.trkCrop = crp _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue) _
                                            And s.delToPrd = 0
                   Select s).ToList()
        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع انتاج قشارة أو غربال ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDel = True
    End Function
    Private Function CheckEditDelSelf(ByVal crp As Integer, ByVal trk As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(Me.DateOut.Text, DateTime)
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = 1
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = 0
            Clnt = Val(LokClient.EditValue)
        End If
        CheckEditDelSelf = False
        Dim lst = (From s In db.V_OutPrdDets Where s.oPrdDate >= TheDate.ToShortDateString And s.trkCrop = crp _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkPrdDet <> trk _
                                                 And s.trkAStore = Val(LokStore.EditValue) _
                                                 And s.isLocal = isLoc And s.trkClntCrp = Clnt
                   Select s).ToList()
        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لايمكنك التعديل أو الحذف...لقد قمت باضافة المحصول بنفس التاريخ أو تاريخ لاحق  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDelSelf = True
    End Function
    Private Sub GVPrdOut_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVPrdOut.CustomRowCellEditForEditing
        Dim crp As Integer
        If e.Column.Caption = "المحصول" Then
            GVPrdOut.SetFocusedRowCellValue("trkUnit", "")
        End If
        If e.Column.Caption = "الوحدة" Then
            crp = GVPrdOut.GetRowCellValue(e.RowHandle, "trkCrop")
            If crp <> 0 Then
                FillUnit(crp)
                e.RepositoryItem = repUnit
            End If
        End If
    End Sub
    Public Sub FillUnit(ByVal crp As Integer)

        Dim un = (From s In db.V_cropUnits Where s.delCU = False And s.trkCrop = crp
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
        repUnit.Columns(4).Visible = False
        repUnit.Columns(5).Visible = False
        repUnit.Columns(6).Visible = False
        repUnit.Columns(7).Visible = False
        repUnit.NullText = ""
        '  rep.Add(repUnit)
    End Sub

    Private Sub GVPrdOut_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GVPrdOut.CellValueChanged
        Dim crp As Integer = GVPrdOut.GetRowCellValue(e.RowHandle, "trkCrop")
        Dim StrUn As String = Trim(GVPrdOut.GetFocusedRowCellValue("trkUnit"))
        Dim TheId As Integer = GVPrdOut.GetRowCellValue(e.RowHandle, "trkPrdDet")
        Dim Amt As Double = GVPrdOut.GetRowCellValue(e.RowHandle, "Amount")
        Dim Total As Double
        Dim tbOutPrd As New V_OutPrdDet
        Dim i As Integer = 0
        'tbOutPrd = (From s In db.V_OutPrdDets Where s.trkPrdDet = TheId Select s).SingleOrDefault
        Dim TheDate As DateTime
        If DateOut.Text <> "" Then
            TheDate = CType(DateOut.Text, DateTime)
        End If
        '************************
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = True
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = False
            Clnt = Val(LokClient.EditValue)
        End If

        '***************************
        If e.Column.Caption = "الوحدة"  Then

            If StrUn <> "" And crp <> 0 Then
                Dim lst = (From s In db.V_cropUnits Where s.trkCrop = crp Select s).ToList
                If lst.Count = 2 Then
                    While i < 2
                        If lst.Item(i).unitName = StrUn Then
                            Exit While
                        End If
                        i = i + 1
                    End While
                End If
                Total = CalTotal(crp, i)

                If TheId <> 0 Then
                    tbOutPrd = (From s In db.V_OutPrdDets Where s.trkPrdDet = TheId _
                                                          And s.trkCrop = crp And s.trkArival = Val(LokLoc.EditValue) _
                                                         And s.trkAStore = Val(LokStore.EditValue) _
                                                              And s.isLocal = isLoc And s.trkClntCrp = Clnt _
                                                              And s.oPrdDate <= TheDate
                                Select s).SingleOrDefault()
                    If Not IsNothing(tbOutPrd) Then
                        If i = 0 Then
                            Total = Total + tbOutPrd.amtOne
                            GVPrdOut.SetFocusedRowCellValue("Amount", tbOutPrd.amtOne)
                        Else
                            Total = Total + tbOutPrd.amtTwo
                            GVPrdOut.SetFocusedRowCellValue("Amount", tbOutPrd.amtTwo)
                        End If

                    End If
                End If

                GVPrdOut.SetFocusedRowCellValue("total", Total)
            Else
                GVPrdOut.SetFocusedRowCellValue("total", 0)
            End If
        End If

    End Sub
    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub

    Private Sub RdoLocal_Click(sender As Object, e As EventArgs) Handles RdoLocal.Click
        LokClient.Enabled = False
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub RdoClient_Click(sender As Object, e As EventArgs) Handles RdoClient.Click
        LokClient.Enabled = True
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
    Private Sub LokClient_EditValueChanged(sender As Object, e As EventArgs) Handles LokClient.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
End Class