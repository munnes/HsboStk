Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Public Class FrmAddStrArvl
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5 As Columns.GridColumn
#Region "Repository Variables"
    Public repCrop As New Repository.RepositoryItemLookUpEdit
    Public repUnit As Repository.RepositoryItemLookUpEdit
#End Region

    Private Sub FrmAddBuy_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        RdoLocal.Checked = True
        LokClient.Enabled = False
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
        Dim tb = (From s In db.aStoreReqs Where s.trkASReq = ID And s.delAS = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokLoc.EditValue = tb.trkArival
        Me.LokStore.EditValue = tb.trkAStore
        Dim CurTotal As Double = 0
        If tb.isLocal = False Then
            RdoClient.Checked = True
            LokClient.EditValue = tb.trkClntCrp
            LokClient.Enabled = True
            GVStkArDet.Columns(2).Visible = False
            RdoClient.Enabled = True
            RdoLocal.Enabled = False
            LokClient.Enabled = True
        Else
            RdoLocal.Checked = True
            LokClient.Enabled = False
            GVStkArDet.Columns(2).Visible = True
            RdoClient.Enabled = False
            RdoLocal.Enabled = True
            LokClient.Enabled = False
        End If
        Me.DateStore.Text = tb.SArDate
        TxtStrInf.Text = tb.aStrDet
        Dim TbUn As New V_cropUnit
        '*******************************
        Dim lst = (From s In db.aStoreDets Where s.trkASReq = ID And s.delASDet = 0 Select s).ToList
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
            '$$$$$$$$$$$$$$$$$$$$$$
            CurTotal = CalTotal(lst.Item(i).trkCrop, j) + lst.Item(i).aStock
            '**********************
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVStkArDet.AddNewRow()
            TbUn = (From s In db.V_cropUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkCrop = lst.Item(i).trkCrop Select s).SingleOrDefault
            GVStkArDet.SetFocusedRowCellValue("trkASDet", lst.Item(i).trkASDet)
            GVStkArDet.SetFocusedRowCellValue("trkCrop", lst.Item(i).trkCrop)
            'GVStkArDet.SetFocusedRowCellValue("total", CurTotal)
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
    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
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
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم محطة الوصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
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
            Dim tb As New aStoreReq
            Dim i As Integer = 0
            tb = (From s In db.aStoreReqs Where s.trkASReq = Val(TxtRId.Text) Select s).SingleOrDefault
            While i < GVStkArDet.RowCount
                Dim lst = (From s In db.V_OutPrdDets Where s.trkArival = tb.trkArival _
                                                        And s.oPrdDate >= tb.SArDate And s.isLocal = tb.isLocal _
                                                         And s.trkClntCrp = tb.trkClntCrp And s.trkAStore = tb.trkAStore _
                                                          And s.trkCrop = Val(GVStkArDet.GetRowCellValue(i, "trkCrop"))
                           Select s).ToList
                If lst.Count > 0 Then
                    XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بادخال محاصيل في القشارة\الغربال من نفس المخزن في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                    Exit Function
                End If
                i = i + 1
            End While
        End If

        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New aStoreReq
        Dim theDate As DateTime
        theDate = CType(DateStore.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkASReq = trk
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.SArDate = theDate.ToShortDateString
            tb.trkAStore = Val(LokStore.EditValue.ToString())
            tb.isLocal = CalFlag()
            If RdoClient.Checked = True Then
                tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            Else
                tb.trkClntCrp = 0
            End If
            tb.aStrDet = TxtStrInf.Text
            tb.trkPrs = 1
            tb.delAS = False
            db.aStoreReqs.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.aStoreReqs Where s.trkASReq = Val(TxtRId.Text) And s.delAS = False Select s).Single()
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.SArDate = theDate.ToShortDateString
            tb.trkAStore = Val(LokStore.EditValue.ToString())
            tb.aStrDet = TxtStrInf.Text
            tb.isLocal = CalFlag()
            If RdoClient.Checked = True Then
                tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            Else
                tb.trkClntCrp = 0
            End If
            tb.trkPrs = 1
            tb.delAS = False
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
        Dim Qrymax = (From trk In db.aStoreReqs Select trk.trkASReq).ToList
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
        If Val(GVStkArDet.GetRowCellValue(Ind, "trkCrop")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVStkArDet.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVStkArDet.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVStkArDet.SelectCell(Ind, col3)
            Exit Function
        End If
        If RdoLocal.Checked = True Then
            If Val(GVStkArDet.GetRowCellValue(Ind, "Amount")) > Val(GVStkArDet.GetRowCellValue(Ind, "total")) Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الكمية أكثر من المتوفر ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                GVStkArDet.SelectCell(Ind, col3)
                Exit Function
            End If
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
        Dim TheDate As DateTime
        TheDate = CType(Me.DateStore.Text, DateTime)
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
            Dim lst = (From s In db.V_ArvStrs Where s.SArDate > TheDate.ToShortDateString And s.trkCrop = Val(GVStkArDet.GetRowCellValue(Ind, "trkCrop")) _
                                             And s.trkArival = Val(LokLoc.EditValue) _
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
        Dim count As Integer = Me.GVStkArDet.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Crop As Integer = Val(GVStkArDet.GetRowCellValue(Ind, "trkCrop"))
        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVStkArDet.GetRowCellValue(i, "trkCrop")) = Crop Then
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
        Dim Qrymax = (From trk In db.aStoreDets Select trk.trkASDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()
        Dim tb As New aStoreDet
        Dim TbUn As New V_cropUnit
        If MemAddDet = True Then
            GVStkArDet.SetRowCellValue(RowInd, "trkASDet", NewKey())
            tb.trkASDet = Val(GVStkArDet.GetRowCellValue(RowInd, "trkASDet"))
            tb.trkASReq = Val(TxtRId.Text)
            TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVStkArDet.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkCrop = Val(GVStkArDet.GetRowCellValue(RowInd, "trkCrop")) Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit

            tb.weight = Val(GVStkArDet.GetRowCellValue(RowInd, "Weight"))
            tb.delASDet = False
            tb.trkCrop = Val(GVStkArDet.GetRowCellValue(RowInd, "trkCrop"))
            tb.aStock = Val(GVStkArDet.GetRowCellValue(RowInd, "Amount"))
            '***************************************
            CalculateUnit(TbUn.trkUnit, Val(GVStkArDet.GetRowCellValue(RowInd, "Amount")),
                    Val(GVStkArDet.GetRowCellValue(RowInd, "trkCrop")))

            '***************************************
            tb.untOne = UOne
            tb.amtOne = AOne
            tb.untTwo = UTwo
            tb.amtTwo = ATwo
            db.aStoreDets.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New aStoreDet
        Dim TbUn As New V_cropUnit
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        If GVStkArDet.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVStkArDet.RowCount

                If (GVStkArDet.IsRowSelected(i) = True) Then
                    TheCrp = Val(GVStkArDet.GetRowCellValue(i, "trkCrop"))
                    TheTrk = Val(GVStkArDet.GetRowCellValue(i, "trkASDet"))
                    If CheckEditDel(TheCrp) = True Then
                        If CheckEditDelSelf(TheCrp, TheTrk) Then

                            If CanSave(i) = True Then
                                tb = (From s In db.aStoreDets Where s.trkASDet = Val(TheTrk) And s.trkASReq = Val(TxtRId.Text) Select s).Single()
                                tb.trkASReq = Val(TxtRId.Text)
                                TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVStkArDet.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkCrop = Val(GVStkArDet.GetRowCellValue(i, "trkCrop")) Select s).SingleOrDefault
                                tb.trkUnit = TbUn.trkUnit
                                tb.weight = Val(GVStkArDet.GetRowCellValue(i, "Weight"))
                                tb.delASDet = False
                                tb.trkCrop = Val(GVStkArDet.GetRowCellValue(i, "trkCrop"))
                                tb.aStock = Val(GVStkArDet.GetRowCellValue(i, "Amount"))
                                CalculateUnit(TbUn.trkUnit, Val(GVStkArDet.GetRowCellValue(i, "Amount")),
                                  Val(GVStkArDet.GetRowCellValue(i, "trkCrop")))

                                tb.untOne = UOne
                                tb.amtOne = AOne
                                tb.untTwo = UTwo
                                tb.amtTwo = ATwo
                                GVStkArDet.UnselectRow(i)
                            End If
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

    'Public Function CountDet() As Integer
    '    Dim lst = (From s In db.V_buyDetails Where s.delBDet = False And s.trkBuyClient = Val(TxtRId.Text) Select s).ToList
    '    Return lst.Count()
    'End Function
    Public Sub FillGrid()
        FillCrop()
        '  FillUnit()
        GridControl1.RepositoryItems.Add(repUnit)
        GridControl1.RepositoryItems.Add(repCrop)
        GridControl1.RepositoryItems.Add(repTxt)
        Dim list As BindingList(Of aStDet) = New BindingList(Of aStDet)
        GridControl1.DataSource = list
        GVStkArDet.Columns(0).ColumnEdit = repTxt
        GVStkArDet.Columns(0).Visible = False
        GVStkArDet.Columns(1).ColumnEdit = repCrop
        GVStkArDet.Columns(2).ColumnEdit = repTxt
        GVStkArDet.Columns(3).ColumnEdit = repTxt
        '  GVStkArDet.Columns(3).ColumnEdit = repUnit
        GVStkArDet.Columns(5).ColumnEdit = repTxt
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
        col1.Caption = "المحصول"
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
        Dim tb As New aStoreDet
        Dim i As Integer = 0
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        Dim lastRow As Integer = GVStkArDet.RowCount - 1
        Dim lastValue As Integer = Val(GVStkArDet.GetRowCellValue(lastRow, "trkASDet"))
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

                    If GVStkArDet.IsRowSelected(i) = True Then
                        TheCrp = Val(GVStkArDet.GetRowCellValue(i, "trkCrop"))
                        TheTrk = Val(GVStkArDet.GetRowCellValue(i, "trkASDet"))
                        If CheckEditDel(TheCrp) = True Then
                            If CheckEditDelSelf(TheCrp, TheTrk) Then
                                tb = (From s In db.aStoreDets Where s.trkASDet = Val(TheTrk) And s.trkASReq = Val(TxtRId.Text) Select s).Single()
                                tb.delASDet = True
                                GVStkArDet.DeleteRow(i)
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
    Private Sub LokStore_EditValueChanged(sender As Object, e As EventArgs) Handles LokStore.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub


    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokStore.Text = ""
        LokClient.Text = ""
        FillStore()
        FillClient()
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
    Private Sub DateStore_EditValueChanged(sender As Object, e As EventArgs) Handles DateStore.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewArStr

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
        rpt.FilterString = " [trkASReq] =" & Val(TxtRId.Text)

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
            If Val(GVStkArDet.GetRowCellValue(GVStkArDet.RowCount - 1, "trkASDet")) = 0 Then
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

    Private Sub TxtStrInf_TextChanged(sender As Object, e As EventArgs) Handles TxtStrInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub
    Private Sub GVStkArDet_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVStkArDet.CustomRowCellEditForEditing
        Dim crp As Integer
        If e.Column.Caption = "المحصول" Then
            GVStkArDet.SetFocusedRowCellValue("trkUnit", "")
        End If
        If e.Column.Caption = "الوحدة" Then
            crp = GVStkArDet.GetRowCellValue(e.RowHandle, "trkCrop")
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
    Private Function CalTotal(ByVal crp As Integer, ByVal UInd As Integer) As Double
        Dim TheDate As DateTime

        Dim tb As New CrpStationResult
        If Me.DateStore.Text <> "" Then
            TheDate = CType(DateStore.Text, DateTime)
        End If
        tb = (From s In db.CrpStation(Val(LokLoc.EditValue), crp, TheDate) Select s).SingleOrDefault

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
        Dim tbArvStr As New V_ArvStr
        Dim Total As Double
        Dim StrUn As String
        Dim curCrop As Integer
        Dim TheDate As DateTime
        If DateStore.Text <> "" Then
            TheDate = CType(DateStore.Text, DateTime)
        End If
        While i < count
            Dim TbUn As New V_cropUnit

            Dim j As Integer = 0
            StrUn = Trim(GVStkArDet.GetRowCellValue(i, "trkUnit"))
            curCrop = Val(GVStkArDet.GetRowCellValue(i, "trkCrop"))
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

                If Val(GVStkArDet.GetRowCellValue(i, "trkASDet")) <> 0 Then
                    tbArvStr = (From s In db.V_ArvStrs Where s.trkASDet = Val(GVStkArDet.GetRowCellValue(i, "trkASDet")) _
                                                          And s.trkCrop = curCrop And s.trkArival = Val(LokLoc.EditValue) _
                                                        And s.isLocal = True _
                                                           And s.SArDate <= TheDate
                                Select s).SingleOrDefault()
                    If Not IsNothing(tbArvStr) Then
                        UndPreStk = tbArvStr.aStock
                    Else
                        UndPreStk = 0
                    End If
                    GVStkArDet.SelectRow(i)
                End If

                Total = CalTotal(curCrop, j) + UndPreStk
                GVStkArDet.SetRowCellValue(i, "total", Total)
            End If

            If i = count - 1 Then
                If Val(GVStkArDet.GetRowCellValue(count - 1, "trkASDet")) = 0 Then
                    GVStkArDet.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub

    Private Sub GVStkArDet_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GVStkArDet.CellValueChanged
        If GVStkArDet.Columns(2).Visible = True Then
            Dim crp As Integer = GVStkArDet.GetRowCellValue(e.RowHandle, "trkCrop")
            Dim StrUn As String = Trim(GVStkArDet.GetFocusedRowCellValue("trkUnit"))
            Dim i As Integer = 0
            Dim TheId As Integer = GVStkArDet.GetRowCellValue(e.RowHandle, "trkASDet")
            Dim Amt As Double = GVStkArDet.GetRowCellValue(e.RowHandle, "Amount")
            Dim tb As New aStoreReq
            Dim tbArvStr As New V_ArvStr
            Dim Total As Double
            Dim TheDate As DateTime
            If DateStore.Text <> "" Then
                TheDate = CType(DateStore.Text, DateTime)
            End If

            If e.Column.Caption = "الوحدة" Then
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

                        tbArvStr = (From s In db.V_ArvStrs Where s.trkASDet = TheId _
                                                          And s.trkCrop = crp And s.trkArival = Val(LokLoc.EditValue) _
                                                         And s.isLocal = True _
                                                           And s.SArDate <= TheDate
                                    Select s).SingleOrDefault()

                        If Not IsNothing(tbArvStr) Then
                            If i = 0 Then
                                Total = Total + tbArvStr.amtOne
                                GVStkArDet.SetFocusedRowCellValue("Amount", tbArvStr.amtOne)
                            Else
                                Total = Total + tbArvStr.amtTwo
                                GVStkArDet.SetFocusedRowCellValue("Amount", tbArvStr.amtTwo)
                            End If

                        End If
                    End If

                    GVStkArDet.SetFocusedRowCellValue("total", Total)
                Else
                    GVStkArDet.SetFocusedRowCellValue("total", 0)
                End If
            End If
        End If
    End Sub
    Private Function CheckEditDel(ByVal crp As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(Me.DateStore.Text, DateTime)
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = 1
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = 0
            Clnt = Val(LokClient.EditValue)
        End If
        CheckEditDel = False
        '**********set where del=0 in view itself
        Dim lst = (From s In db.V_OutPrdDets Where s.oPrdDate >= TheDate.ToShortDateString And s.trkCrop = crp _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkAStore = Val(LokStore.EditValue) _
   And s.isLocal = isLoc And s.trkClntCrp = Clnt _
        Select  s).ToList()
        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع دخول انتاج في القشارة\الغربال ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDel = True
    End Function



    Private Function CheckEditDelSelf(ByVal crp As Integer, ByVal trk As Integer) As Boolean
        Dim TheDate As DateTime
        Dim countLst As Integer = 0
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = 1
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = 0
            Clnt = Val(LokClient.EditValue)
        End If
        TheDate = CType(Me.DateStore.Text, DateTime)
        CheckEditDelSelf = False
        Dim lst = (From s In db.V_ArvStrs Where s.SArDate >= TheDate.ToShortDateString And s.trkCrop = crp _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkASDet <> trk _
                                                And s.isLocal = isLoc And s.trkClntCrp = Clnt
                   Select s).ToList()
        countLst = lst.Count
        If countLst > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لايمكنك التعديل أو الحذف...لقد قمت باضافة المحصول بنفس التاريخ أو تاريخ لا حق  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDelSelf = True
    End Function

    Private Sub RdoLocal_Click(sender As Object, e As EventArgs) Handles RdoLocal.Click
        GVStkArDet.SetFocusedRowCellValue("total", 0)
        LokClient.Enabled = False
        GVStkArDet.Columns(2).Visible = True
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub RdoClient_Click(sender As Object, e As EventArgs) Handles RdoClient.Click
        LokClient.Enabled = True
        GVStkArDet.Columns(2).Visible = False
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub LokClient_TextChanged(sender As Object, e As EventArgs) Handles LokClient.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub
End Class