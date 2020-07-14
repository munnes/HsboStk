Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils
Public Class FrmAddArrive
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Public curCrop As Integer
#Region "Repository Variables"
    Public repUnit As Repository.RepositoryItemLookUpEdit

    Public repTrv As New Repository.RepositoryItemTextEdit
#End Region

    Private Sub FrmAddArrive_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillRes()
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
        Dim tb = (From s In db.arrives Where s.trkAr = ID And s.delAr = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokLoc.EditValue = tb.trkBuyLoc
        Me.LokRes.EditValue = tb.trkArival
        Me.DateArrive.Text = tb.arDate
        TxtArrInf.Text = tb.arInfo
        Dim TbUn As New V_cropUnit
        '*******************************
        Dim lst = (From s In db.arriveDetails Where s.trkAr = ID And s.delAD = 0 Select s).ToList
        While i < CountView
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVArriveDet.AddNewRow()
            TbUn = (From s In db.V_cropUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkCrop = lst.Item(i).trkCrop Select s).SingleOrDefault
            GVArriveDet.SetFocusedRowCellValue("trkArDet", lst.Item(i).trkArDet)
            GVArriveDet.SetFocusedRowCellValue("trkCrop", lst.Item(i).trkCrop)
            GVArriveDet.SetFocusedRowCellValue("Amount", lst.Item(i).storeAmount)
            GVArriveDet.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVArriveDet.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVArriveDet.SetFocusedRowCellValue("trkDriver", lst.Item(i).Driver)
            GVArriveDet.SetFocusedRowCellValue("trkCar", lst.Item(i).Car)
            GVArriveDet.UpdateCurrentRow()
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
        GVArriveDet.OptionsFind.FindFilterColumns = ""
        GVArriveDet.OptionsFind.AlwaysVisible = False

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
                GVArriveDet.AddNewRow()
            End If
        End If
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVArriveDet.OptionsFind.AlwaysVisible = False
        If CanSaveReq() = True Then
            If Trim(TxtRId.Text) = "" Then
                MemAdd = True
            End If
            If MemAdd Or MemEdit Then
                SaveReqData()
                'MemEdit = False
                'MemAdd = False
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
        If DateArrive.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateArrive.Focus()
            Exit Function
        End If
        If CType(DateArrive.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateArrive.Focus()
            Exit Function
        End If
        If LokRes.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً الرجاء إدخال محطة الوصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokRes.Focus()
            LokRes.SelectAll()
            Exit Function
        End If
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال منطقة الشحن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If
        If MemEdit And IsView Then
            Dim tb As New arrive
            Dim i As Integer = 0
            tb = (From s In db.arrives Where s.trkAr = Val(TxtRId.Text) Select s).SingleOrDefault
            While i < GVArriveDet.RowCount
                Dim lst = (From s In db.V_ArvStrs Where s.trkArival = tb.trkArival _
                                                        And s.SArDate >= tb.arDate And s.isLocal = True _
                                                          And s.trkCrop = Val(GVArriveDet.GetRowCellValue(i, "trkCrop"))
                           Select s).ToList
                If lst.Count > 0 Then
                    XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بالتخزين من نفس المحطة في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                    Exit Function
                End If
                i = i + 1
            End While
        End If
        CanSaveReq = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New arrive
        Dim theDate As DateTime
        theDate = CType(DateArrive.Text, DateTime)
        If MemAdd = True And Saved = False Then
            trk = NewReqKey()
            tb.trkAr = trk
            tb.trkBuyLoc = Val(LokLoc.EditValue.ToString())
            tb.arDate = theDate.ToShortDateString
            tb.trkArival = Val(LokRes.EditValue.ToString())
            tb.arInfo = TxtArrInf.Text
            tb.delAr = False
            db.arrives.InsertOnSubmit(tb)
            Saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.arrives Where s.trkAr = Val(TxtRId.Text) And s.delAr = False Select s).Single()
            tb.trkBuyLoc = Val(LokLoc.EditValue.ToString())
            tb.arDate = theDate.ToShortDateString
            tb.trkArival = Val(LokRes.EditValue.ToString())
            tb.arInfo = TxtArrInf.Text
            tb.delAr = False
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
        Dim Qrymax = (From trk In db.arrives Select trk.trkAr).ToList
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
        If Val(GVArriveDet.GetRowCellValue(Ind, "trkCrop")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArriveDet.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVArriveDet.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArriveDet.SelectCell(Ind, col3)
            Exit Function
        End If

        If (GVArriveDet.GetRowCellValue(Ind, "trkUnit")) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً الرجاء إدخال الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArriveDet.SelectCell(Ind, col5)
            Exit Function
        End If
        If Val(GVArriveDet.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArriveDet.SelectCell(Ind, col4)
            Exit Function
        End If

        If GVArriveDet.GetRowCellValue(Ind, "trkDriver") = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم السائق ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArriveDet.SelectCell(Ind, col2)
            Exit Function
        End If
        If GVArriveDet.GetRowCellValue(Ind, "trkCar") = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال رقم العربة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVArriveDet.SelectCell(Ind, col2)
            Exit Function
        End If

        CanSave = True
    End Function
    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVArriveDet.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Crop As Integer = Val(GVArriveDet.GetRowCellValue(Ind, "trkCrop"))

        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVArriveDet.GetRowCellValue(i, "trkCrop")) = Crop Then
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
        Dim Qrymax = (From trk In db.arriveDetails Select trk.trkArDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub SaveData()
        Dim tb As New arriveDetail
        Dim TbUn As New V_cropUnit
        If MemAddDet = True Then
            GVArriveDet.SetRowCellValue(RowInd, "trkArDet", NewKey())
            tb.trkArDet = Val(GVArriveDet.GetRowCellValue(RowInd, "trkArDet"))
            tb.trkAr = Val(TxtRId.Text)
            TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVArriveDet.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkCrop = Val(GVArriveDet.GetRowCellValue(RowInd, "trkCrop")) Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVArriveDet.GetRowCellValue(RowInd, "Weight"))
            tb.Driver = GVArriveDet.GetRowCellValue(RowInd, "trkDriver")
            tb.delAD = False
            tb.trkCrop = Val(GVArriveDet.GetRowCellValue(RowInd, "trkCrop"))
            tb.storeAmount = Val(GVArriveDet.GetRowCellValue(RowInd, "Amount"))
            tb.Car = GVArriveDet.GetRowCellValue(RowInd, "trkCar")
            '***************************************
            CalculateUnit(TbUn.trkUnit, Val(GVArriveDet.GetRowCellValue(RowInd, "Amount")),
                    Val(GVArriveDet.GetRowCellValue(RowInd, "trkCrop")))

            '***************************************
            tb.untOne = UOne
            tb.amtOne = AOne
            tb.untTwo = UTwo
            tb.amtTwo = ATwo
            db.arriveDetails.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New arriveDetail
        Dim TbUn As New V_cropUnit
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        If GVArriveDet.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVArriveDet.RowCount
                If (GVArriveDet.IsRowSelected(i) = True) Then
                    TheCrp = Val(GVArriveDet.GetRowCellValue(i, "trkCrop"))
                    TheTrk = Val(GVArriveDet.GetRowCellValue(i, "trkArDet"))
                    If CheckEditDel(TheCrp) = True Then
                        If CanSave(i) = True Then
                            tb = (From s In db.arriveDetails Where s.trkArDet = Val(TheTrk) And s.trkAr = Val(TxtRId.Text) Select s).Single()
                            tb.trkAr = Val(TxtRId.Text)
                            TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVArriveDet.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkCrop = Val(GVArriveDet.GetRowCellValue(i, "trkCrop")) Select s).SingleOrDefault
                            tb.trkUnit = TbUn.trkUnit
                            tb.weight = Val(GVArriveDet.GetRowCellValue(i, "Weight"))
                            tb.Driver = GVArriveDet.GetRowCellValue(i, "trkDriver")
                            tb.Car = GVArriveDet.GetRowCellValue(i, "trkCar")
                            tb.delAD = False
                            tb.trkCrop = Val(GVArriveDet.GetRowCellValue(i, "trkCrop"))
                            tb.storeAmount = Val(GVArriveDet.GetRowCellValue(i, "Amount"))
                            '$$$$$$$$$$$$$$$$$$$$$$$$$$$
                            CalculateUnit(TbUn.trkUnit, Val(GVArriveDet.GetRowCellValue(i, "Amount")),
                            Val(GVArriveDet.GetRowCellValue(i, "trkCrop")))

                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                            GVArriveDet.UnselectRow(i)
                        End If
                    End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVArriveDet.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVArriveDet.SelectedRowsCount <> 0 Then
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
        FillCrop()
        GridControl1.RepositoryItems.Add(repUnit)
        GridControl1.RepositoryItems.Add(repTxt)

        GridControl1.RepositoryItems.Add(repTrv)
        '***************** should be added her to avoid disappear when focus changed
        GridControl1.RepositoryItems.Add(repCrop)
        Dim list As BindingList(Of arrDet) = New BindingList(Of arrDet)

        GridControl1.DataSource = list
        GVArriveDet.Columns(0).ColumnEdit = repTxt
        GVArriveDet.Columns(0).Visible = False
        GVArriveDet.Columns(1).ColumnEdit = repCrop
        GVArriveDet.Columns(2).ColumnEdit = repTxt
        '   GVArriveDet.Columns(3).ColumnEdit = repUnit
        GVArriveDet.Columns(4).ColumnEdit = repTxt
        GVArriveDet.Columns(5).ColumnEdit = repTrv
        GVArriveDet.Columns(6).ColumnEdit = repTrv
        GVArriveDet.OptionsSelection.MultiSelect = True
        GVArriveDet.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub
    Private Sub FormatColumns()
        col1 = GVArriveDet.Columns(1)
        col2 = GVArriveDet.Columns(2)
        col3 = GVArriveDet.Columns(3)
        col4 = GVArriveDet.Columns(4)
        col5 = GVArriveDet.Columns(5)
        col6 = GVArriveDet.Columns(6)

        '****************
        col1.Caption = "المحصول"
        col2.Caption = "الكمية"
        col3.Caption = "الوحدة"
        col4.Caption = "الوزن"
        col5.Caption = "اسم السائق"
        col6.Caption = "رقم العربة"
        GVArriveDet.Columns(1).Width = 70
        GVArriveDet.Columns(2).Width = 30
        GVArriveDet.Columns(3).Width = 40
        GVArriveDet.Columns(4).Width = 40
        GVArriveDet.Columns(5).Width = 90
        GVArriveDet.Columns(6).Width = 60
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVArriveDet.OptionsFind.AlwaysVisible = False
        Dim tb As New arriveDetail
        Dim i As Integer = 0
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        Dim lastRow As Integer = GVArriveDet.RowCount - 1
        Dim lastValue As Integer = Val(GVArriveDet.GetRowCellValue(lastRow, "trkArDet"))
        If lastValue = 0 And GVArriveDet.IsRowSelected(lastRow) = True Then
            GVArriveDet.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
            MemAddDet = False
        End If
        If GVArriveDet.SelectedRowsCount <> 0 Then
            'E
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVArriveDet.RowCount

                    If GVArriveDet.IsRowSelected(i) = True Then
                        TheCrp = Val(GVArriveDet.GetRowCellValue(i, "trkCrop"))
                        TheTrk = Val(GVArriveDet.GetRowCellValue(i, "trkArDet"))
                        If CheckEditDel(TheCrp) = True Then
                            tb = (From s In db.arriveDetails Where s.trkArDet = Val(TheTrk) And s.trkAr = Val(TxtRId.Text) Select s).Single()
                            tb.delAD = True
                            GVArriveDet.DeleteRow(i)
                            SavedRow = Row - 1
                            i = i - 1
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
        If GVArriveDet.SelectedRowsCount <> 0 Then
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
        If GVArriveDet.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVArriveDet.OptionsFind.FindFilterColumns = "*"
            GVArriveDet.ShowFindPanel()
            GVArriveDet.OptionsFind.ShowClearButton = False
            GVArriveDet.OptionsFind.ShowFindButton = False
        End If
    End Sub


    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewArrive

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
        rpt.FilterString = " [trkAr] =" & Val(TxtRId.Text)
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
    Private Sub DateArrive_EditValueChanged(sender As Object, e As EventArgs) Handles DateArrive.EditValueChanged
        If Saved = True Then
            MemEdit = True
        End If
    End Sub
    Private Sub LokRes_TextChanged(sender As Object, e As EventArgs) Handles LokRes.TextChanged
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
            If Val(GVArriveDet.GetRowCellValue(GVArriveDet.RowCount - 1, "trkArDet")) = 0 Then
                'If tb.trkBuyDet = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVArriveDet.RowCount - 1
                    GVArriveDet.FocusedRowHandle = lastRow
                    GVArriveDet.DeleteRow(lastRow)
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

    Private Sub TxtArrInf_TextChanged(sender As Object, e As EventArgs) Handles TxtArrInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub GVArriveDet_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVArriveDet.CustomRowCellEditForEditing
        Dim crp As Integer
        If e.Column.Caption = "المحصول" Then
            GVArriveDet.SetFocusedRowCellValue("trkUnit", "")
        End If
        If e.Column.Caption = "الوحدة" Then
            crp = GVArriveDet.GetRowCellValue(e.RowHandle, "trkCrop")
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

    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub
    Private Function CheckEditDel(ByVal crp As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(Me.DateArrive.Text, DateTime)
        CheckEditDel = False
        Dim lst = (From s In db.V_ArvStrs Where s.SArDate >= TheDate.ToShortDateString And s.trkCrop = crp _
                                             And s.trkArival = Val(LokRes.EditValue)
                   Select s).ToList()
        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع التخزين في المحطات ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CheckEditDel = True
    End Function
End Class