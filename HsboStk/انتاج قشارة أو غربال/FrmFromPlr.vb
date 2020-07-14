
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils
Public Class FrmFromPlr
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private Flag As Integer = 0
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Private MyID As Integer
    Public curCrop As Integer
    Private DoneLoc As Boolean
#Region "Repository Variables"
    Public repUnit As Repository.RepositoryItemLookUpEdit
    '  Public repTxt As New Repository.RepositoryItemTextEdit
    Public repPrd As New Repository.RepositoryItemLookUpEdit

#End Region
    Private Sub FrmFromPlr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        FillCrop()
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
    Public Sub FillGrid()
        GridControl1.RepositoryItems.Add(repTxt)
        '***************** should be added her to avoid disappear when focus changed
        GridControl1.RepositoryItems.Add(repPrd)

        Dim list As BindingList(Of tPrd) = New BindingList(Of tPrd)

        GridControl1.DataSource = list
        GVPrdTo.Columns(0).ColumnEdit = repTxt
        GVPrdTo.Columns(0).Visible = False
        GVPrdTo.Columns(1).ColumnEdit = repPrd
        GVPrdTo.Columns(2).ColumnEdit = repTxt
        ' GVPrdTo.Columns(3).ColumnEdit = repUnit
        GVPrdTo.Columns(4).ColumnEdit = repTxt

        GVPrdTo.OptionsSelection.MultiSelect = True
        GVPrdTo.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
    End Sub
    Private Sub FormatColumns()
        col1 = GVPrdTo.Columns(1)
        col2 = GVPrdTo.Columns(2)
        col3 = GVPrdTo.Columns(3)
        col4 = GVPrdTo.Columns(4)
        '****************
        col1.Caption = "المنتج"
        col2.Caption = "الكمية"
        col3.Caption = "الوحدة"
        col4.Caption = "الوزن"
    End Sub
    Private Sub MainData()
        Dim tb As New toPrd
        tb = (From s In db.toPrds Where s.trkToPrd = ID And s.delToPrd = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        Me.LokLoc.EditValue = tb.trkArival
        Me.LokPeeler.EditValue = tb.trkPeeler
        Me.DateTo.Text = tb.toPrdDate
        Me.LokCrop.EditValue = tb.trkCrop
        Me.TxtAmount.Text = tb.cropAmt
        Me.TxtTrash.Text = tb.trashAmt
        Me.TxtFinalAmt.Text = Val(TxtAmount.Text) - Val(TxtTrash.Text)
        If tb.isLocal = False Then
            RdoClient.Checked = True
            LokClient.EditValue = tb.trkClntCrp
            LokClient.Enabled = True
            RdoLocal.Enabled = False
        Else
            RdoLocal.Checked = True
            LokClient.Enabled = False
            RdoClient.Enabled = False
            LokClient.Enabled = False
        End If
        TxtToInf.Text = tb.toInfo
        SetAvail(LokUnit.ItemIndex)
        Me.TxtAvl.Text = Val(TxtAvl.Text) + Val(tb.cropAmt)

        Me.LokUnit.EditValue = tb.cropUnit
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        GVPrdTo.OptionsFind.FindFilterColumns = ""
        GVPrdTo.OptionsFind.AlwaysVisible = False
        If Val(TxtFinalAmt.Text) <> 0 Then
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
                    GVPrdTo.AddNewRow()
                End If
            End If
        Else
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يوجد محصول يصلح للانتاج ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            TxtFinalAmt.Focus()
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVPrdTo.OptionsFind.AlwaysVisible = False
        If Trim(TxtRId.Text) = "" Then
            MemAdd = True
        End If
        If CanSaveReq() = True Then
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

    Private Sub ViewDet()
        saved = True
        MainData()
        Dim i As Integer = 0
        Dim TbUn As New V_prdUnit

        '*******************************
        Dim lst = (From s In db.toPrdDets Where s.trkToPrd = ID And s.delToDet = 0 Select s).ToList
        While i < lst.Count
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVPrdTo.AddNewRow()
            TbUn = (From s In db.V_prdUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkProd = lst.Item(i).trkProd _
                    And s.trkCrop = Val(LokCrop.EditValue) Select s).SingleOrDefault

            GVPrdTo.SetFocusedRowCellValue("trkToDet", lst.Item(i).trkToDet)
            GVPrdTo.SetFocusedRowCellValue("trkPrd", lst.Item(i).trkProd)
            GVPrdTo.SetFocusedRowCellValue("Amount", lst.Item(i).amount)
            GVPrdTo.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVPrdTo.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVPrdTo.UpdateCurrentRow()
            i = i + 1
        End While
        trk = ID
        i = 0
        CountView = 0
        ID = 0

    End Sub
    Private Sub FillLoc()
        DoneLoc = False
        Dim lst = (From c In db.arivalLocs Where c.delAr = False Select c).ToList
        LokLoc.Text = ""
        Me.LokLoc.Properties.DataSource = lst
        LokLoc.Properties.DisplayMember = "arivalName"
        LokLoc.Properties.ValueMember = "trkArival"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
        DoneLoc = True
    End Sub
    Public Sub FillPeeler()
        If DoneLoc = True And LokLoc.Text <> "" Then
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

    Private Sub FillCrop()
        done = False
        Dim lst = (From c In db.crops Where c.delCrop = False Select c).ToList
        LokCrop.Text = ""
        Me.LokCrop.Properties.DataSource = lst
        LokCrop.Properties.DisplayMember = "cropName"
        LokCrop.Properties.ValueMember = "trkCrop"
        LokCrop.Properties.PopulateColumns()
        LokCrop.Properties.Columns(0).Visible = False
        LokCrop.Properties.Columns(2).Visible = False
        done = True
    End Sub
    Private Sub FillUnitCrp()
        If done = True And LokCrop.Text <> "" Then
            Dim lst = (From c In db.V_cropUnits Where c.delUn = False
                       Where c.trkCrop = Val(LokCrop.EditValue) Select c).ToList
            LokUnit.Text = ""
            Me.LokUnit.Properties.DataSource = lst
            LokUnit.Properties.DisplayMember = "unitName"
            LokUnit.Properties.ValueMember = "trkUnit"
            LokUnit.Properties.PopulateColumns()
            LokUnit.Properties.Columns(0).Visible = False
            LokUnit.Properties.Columns(1).Visible = False
            LokUnit.Properties.Columns(2).Visible = False
            LokUnit.Properties.Columns(4).Visible = False
            LokUnit.Properties.Columns(5).Visible = False
            LokUnit.Properties.Columns(6).Visible = False
            LokUnit.Properties.Columns(7).Visible = False
            '   done = False
        End If
    End Sub
    Private Sub SetAvail(ByVal UInd As Integer)
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = True
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = False
            Clnt = Val(LokClient.EditValue)
        End If
        '**********************************
        Dim Amt As Double
        Dim TheDate As DateTime
        If Me.DateTo.Text <> "" Then
            TheDate = CType(DateTo.Text, DateTime)
        End If
        TxtAvl.Text = ""
        Dim tb As New CrpPeelerResult
        tb = (From s In db.CrpPeeler(TheDate, Val(LokLoc.EditValue), Val(LokPeeler.EditValue),
                  Val(LokCrop.EditValue), isLoc, Clnt) Select s).SingleOrDefault

        If Not IsNothing(tb) Then

            If UInd = 0 Then
                Amt = tb.oneUnt
            Else
                Amt = tb.twoUnt
            End If
            TxtAvl.Text = Math.Round(Amt, 2)
        End If

    End Sub
    Private Function CanSaveReq() As Boolean
        CanSaveReq = False

        If DateTo.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateTo.Focus()
            Exit Function
        End If
        If CType(DateTo.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateTo.Focus()
            Exit Function
        End If
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحطة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If
        If LokPeeler.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم القشارة\الغربال ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokPeeler.Focus()
            LokPeeler.SelectAll()
            Exit Function
        End If

        '*********************************This part for crop
        If LokCrop.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokCrop.Focus()
            LokCrop.SelectAll()
            Exit Function
        End If
        If Val(TxtAvl.Text) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفوا لايوجد مخزون من هذا المحصول", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            TxtAvl.Focus()
            TxtAvl.SelectAll()
            Exit Function
        End If
        If Val(TxtAmount.Text) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفوا الكمية المعالجة أكبر من المتوفر", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            TxtAmount.Focus()
            TxtAmount.SelectAll()
            Exit Function
        End If
        If Val(TxtAmount.Text) > Val(TxtAvl.Text) Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفوا الكمية المعالجة أكبر من المتوفر", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            TxtAmount.Focus()
            TxtAmount.SelectAll()
            Exit Function
        End If
        If Val(TxtTrash.Text) > Val(TxtAmount.Text) Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفوا كمية فاقد معالجة أكبر من الكمية المعالجة", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            TxtTrash.Focus()
            TxtTrash.SelectAll()
            Exit Function
        End If
        If LokUnit.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال وحدة المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokUnit.Focus()
            LokUnit.SelectAll()
            Exit Function
        End If
        If RdoLocal.Checked = False And RdoClient.Checked = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء اختيار مصدر المحاصيل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        If RdoClient.Checked = True Then
            If LokClient.Text = "" Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء ادخال اسم العميل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                LokClient.Focus()
                LokClient.SelectAll()
                Exit Function
            End If
        End If
        '*************************************
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = True
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = False
            Clnt = Val(LokClient.EditValue)
        End If
        If MemAdd Then
            Dim TheDate As DateTime
            TheDate = CType(Me.DateTo.Text, DateTime)
            Dim lst = (From s In db.toPrds Where s.toPrdDate > TheDate And s.trkCrop = Val(LokCrop.EditValue) _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                And s.isLocal = isLoc And s.trkClntCrp = Clnt
                       Select s).ToList()
            If lst.Count > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن اضافة هذا المحصول ...لقد قمت باضافته في تاريخ لاحق ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                LokCrop.Focus()
                LokCrop.SelectAll()
                Exit Function
            End If

        End If
        If MemEdit And IsView Then
            Dim tb As New toPrd
            Dim i As Integer = 0
            tb = (From s In db.toPrds Where s.trkToPrd = Val(TxtRId.Text) Select s).SingleOrDefault
            If RdoLocal.Checked = True Then
                While i < GVPrdTo.RowCount
                    Dim lst = (From s In db.V_SaleDets Where s.trkPeeler = tb.trkPeeler _
                                                          And s.trkArival = tb.trkArival _
                                                        And s.saleDate >= tb.toPrdDate _
                                                          And s.trkProd = Val(GVPrdTo.GetRowCellValue(i, "trkPrd"))
                               Select s).ToList
                    If lst.Count > 0 Then
                        XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بصرف منتجات محلياً من القشارة\الغربال في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                        Exit Function
                    End If
                    i = i + 1
                End While
                i = 0
                While i < GVPrdTo.RowCount
                    Dim lst = (From s In db.V_RecvDets Where s.trkPeeler = tb.trkPeeler _
                                                          And s.trkArival = tb.trkArival _
                                                        And s.recvDate >= tb.toPrdDate _
                                                          And s.trkProd = Val(GVPrdTo.GetRowCellValue(i, "trkPrd")) _
                                                       And s.isLocal = True
                               Select s).ToList
                    If lst.Count > 0 Then
                        XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بتخزين منتجات من القشارة\الغربال في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                        Exit Function
                    End If
                    i = i + 1
                End While
                i = 0
                While i < GVPrdTo.RowCount
                    Dim lst = (From s In db.V_expShipDets Where s.trkPeeler = tb.trkPeeler _
                                                          And s.trkArival = tb.trkArival _
                                                        And s.expShipDate >= tb.toPrdDate _
                                                          And s.trkProd = Val(GVPrdTo.GetRowCellValue(i, "trkPrd"))
                               Select s).ToList
                    If lst.Count > 0 Then
                        XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بشحن منتجات من القشارة\الغربال في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                        Exit Function
                    End If
                    i = i + 1
                End While
            End If
            If RdoClient.Checked = True Then
                i = 0
                While i < GVPrdTo.RowCount
                    Dim lst = (From s In db.V_RecvDets Where s.trkToPrd = tb.trkToPrd _
                                                          And s.trkProd = Val(GVPrdTo.GetRowCellValue(i, "trkPrd"))
                               Select s).ToList
                    If lst.Count > 0 Then
                        XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بتخزين منتجات من القشارة\الغربال لهذا العميل في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                        Exit Function
                    End If
                    i = i + 1
                End While
                i = 0
                While i < GVPrdTo.RowCount
                    Dim lst = (From s In db.V_RecvClntDets Where s.trkToPrd = tb.trkToPrd _
                                                          And s.trkProd = Val(GVPrdTo.GetRowCellValue(i, "trkPrd"))
                               Select s).ToList
                    If lst.Count > 0 Then
                        XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بتسليم هذا العميل منتجات من القشارة\الغربال  في تاريخ لاحق لايمكنك التعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                        Exit Function
                    End If
                    i = i + 1
                End While
            End If
        End If

        CanSaveReq = True
    End Function
    Private Function chkInCrpAmt() As Boolean
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = 1
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = 0
            Clnt = Val(LokClient.EditValue)
        End If
        chkInCrpAmt = False
        Dim TheDate As DateTime
        TheDate = CType(Me.DateTo.Text, DateTime)
        Dim lst = (From s In db.toPrds Where s.toPrdDate >= TheDate.ToShortDateString And s.trkCrop = Val(LokCrop.EditValue) _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue) _
                                               And s.trkToPrd <> Val(TxtRId.Text) _
                                           And s.isLocal = isLoc And s.trkClntCrp = Clnt
                   Select s).ToList()

        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل كمية المحصول ...توجد سجلات اخرى ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        chkInCrpAmt = True
    End Function
    Private Sub SaveReqData()
        Dim tb As New toPrd
        Dim theDate As DateTime
        theDate = CType(DateTo.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewReqKey()
            tb.trkToPrd = trk
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.toPrdDate = theDate.ToShortDateString

            tb.trkPeeler = Val(LokPeeler.EditValue.ToString())
            tb.trkCrop = Val(LokCrop.EditValue)
            tb.cropAmt = Val(TxtAmount.Text)
            tb.trashAmt = Val(TxtTrash.Text)
            tb.cropUnit = Val(LokUnit.EditValue)
            tb.toInfo = TxtToInf.Text
            '***************************************
            CalculateUnit(Val(LokUnit.EditValue), Val(TxtAmount.Text), Val(LokCrop.EditValue))
            '***************************************
            tb.crpUntOne = UOne
            tb.crpAmtOne = AOne
            tb.crpUntTwo = UTwo
            tb.crpAmtTwo = ATwo
            '*************************
            tb.isLocal = CalLocClnt()
            If RdoClient.Checked = True Then
                tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            Else
                tb.trkClntCrp = 0
            End If
            '*************************
            tb.delToPrd = False
            db.toPrds.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.toPrds Where s.trkToPrd = Val(TxtRId.Text) And s.delToPrd = False Select s).Single()
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.toPrdDate = theDate.ToShortDateString

            tb.trkPeeler = Val(LokPeeler.EditValue.ToString())
            tb.trkCrop = Val(LokCrop.EditValue)
            tb.cropAmt = Val(TxtAmount.Text)
            tb.cropUnit = Val(LokUnit.EditValue)
            tb.trashAmt = Val(TxtTrash.Text)
            tb.toInfo = TxtToInf.Text
            '***************************************
            CalculateUnit(Val(LokUnit.EditValue), Val(TxtAmount.Text), Val(LokCrop.EditValue))
            '***************************************
            tb.crpUntOne = UOne
            tb.crpAmtOne = AOne
            tb.crpUntTwo = UTwo
            tb.crpAmtTwo = ATwo
            '*************************
            tb.isLocal = CalLocClnt()
            If RdoClient.Checked = True Then
                tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            Else
                tb.trkClntCrp = 0
            End If
            '*************************
            tb.delToPrd = False
            trk = TxtRId.Text
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

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        TheExit()
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        TheExit()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub
    Function NewReqKey() As Integer
        Dim TheKey As Integer
        Dim Qrymax = (From trk In db.toPrds Select trk.trkToPrd).ToList
        If Qrymax.Count = 0 Then
            TheKey = 1
        Else
            TheKey = Qrymax.Max() + 1
        End If
        Return TheKey
    End Function

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVPrdTo.OptionsFind.AlwaysVisible = False
        Dim tb As New toPrdDet
        Dim i As Integer = 0
        Dim prd As Integer

        Dim lastRow As Integer = GVPrdTo.RowCount - 1
        Dim lastValue As Integer = Val(GVPrdTo.GetRowCellValue(lastRow, "trkToDet"))
        If lastValue = 0 And GVPrdTo.IsRowSelected(lastRow) = True Then
            GVPrdTo.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
            MemAddDet = False
        End If


        If GVPrdTo.SelectedRowsCount <> 0 Then

            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVPrdTo.RowCount
                    Dim TheReq As Integer
                    TheReq = Val(GVPrdTo.GetRowCellValue(i, "trkToDet"))
                    prd = Val(GVPrdTo.GetRowCellValue(i, "trkPrd"))
                    If GVPrdTo.IsRowSelected(i) = True Then
                        If CheckEditDel(prd) = True Then
                            tb = (From s In db.toPrdDets Where s.trkToDet = Val(TheReq) And s.trkToPrd = Val(TxtRId.Text) Select s).Single()
                            tb.delToDet = True
                            GVPrdTo.DeleteRow(i)
                            i = i - 1
                            SavedRow = Row - 1
                            Row = SavedRow
                            MemAddDet = False
                        Else
                            GVPrdTo.UnselectRow(i)
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
    Private Function CheckEditDel(ByVal prd As Integer) As Boolean
        Dim TheDate As DateTime
        TheDate = CType(Me.DateTo.Text, DateTime)
        CheckEditDel = False

        If RdoLocal.Checked = True Then
            Dim lstSale = (From s In db.V_SaleDets Where s.saleDate >= TheDate.ToShortDateString And s.trkProd = prd _
                                                 And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s).ToList()
            If lstSale.Count > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع الصرف المحلي ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
            Dim lstExp = (From s In db.V_expShipDets Where s.expShipDate >= TheDate.ToShortDateString And s.trkProd = prd _
                                                 And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue)
                          Select s).ToList()
            If lstExp.Count > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع الشحن من القشارة\الغربال ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
            Dim lstStr = (From s In db.V_RecvDets Where s.recvDate >= TheDate.ToShortDateString And s.trkProd = prd _
                                                 And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue) _
                                               And s.isLocal = 1 Select s).ToList()
            If lstStr.Count > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع استلام من الانتاج المحلي في المحطات ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
        ElseIf RdoClient.Checked = True
            Dim lstStrClnt = (From s In db.V_RecvDets Where s.trkToPrd = Val(TxtRId.Text) And s.trkProd = prd
                              Select s).ToList()
            If lstStrClnt.Count > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع تخزين منتجات لعميل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
            Dim lstPlrClnt = (From s In db.V_RecvClntDets Where s.trkToPrd = Val(TxtRId.Text) And s.trkProd = prd
                              Select s).ToList()
            If lstPlrClnt.Count > 0 Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل المنتج لارتباطه بسجلات اخرى  راجع تخزين منتجات لعميل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Exit Function
            End If
        End If
        CheckEditDel = True
    End Function
    Private Function CanSave(ByVal Ind As Integer) As Boolean
        CanSave = False
        If IsSingleRow(Ind) = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً لقد قمت بادخال المنتج  مسبقا  يمكنك التعديل ", " تكرار في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        If LokCrop.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            'GVPrdOut.SelectCell(RowInd, col1)
            Exit Function
        End If
        If Val(TxtAmount.Text) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال كمية المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        If LokUnit.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال وحدة المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If

        If Val(GVPrdTo.GetRowCellValue(Ind, "trkPrd")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المنتج ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVPrdTo.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVPrdTo.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVPrdTo.SelectCell(Ind, col3)
            Exit Function
        End If

        If (GVPrdTo.GetRowCellValue(Ind, "trkUnit")) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً الرجاء إدخال الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVPrdTo.SelectCell(Ind, col5)
            Exit Function
        End If
        If Val(GVPrdTo.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVPrdTo.SelectCell(Ind, col4)
            Exit Function
        End If
        CanSave = True

    End Function

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If GVPrdTo.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVPrdTo.OptionsFind.FindFilterColumns = "*"
            GVPrdTo.ShowFindPanel()
            GVPrdTo.OptionsFind.ShowClearButton = False
            GVPrdTo.OptionsFind.ShowFindButton = False
        End If
    End Sub

    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVPrdTo.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0

        Dim Prd As Integer = Val(GVPrdTo.GetRowCellValue(Ind, "trkPrd"))
        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVPrdTo.GetRowCellValue(i, "trkPrd")) = Prd Then
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
        Dim Qrymax = (From trk In db.toPrdDets Select trk.trkToDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function

    Private Sub SaveData()
        Dim tb As New toPrdDet
        Dim TbUn As New V_prdUnit

        GVPrdTo.SetRowCellValue(RowInd, "trkToDet", NewKey())
        tb.trkToDet = Val(GVPrdTo.GetRowCellValue(RowInd, "trkToDet"))
        tb.trkToPrd = Val(TxtRId.Text)
        TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVPrdTo.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkProd = Val(GVPrdTo.GetRowCellValue(RowInd, "trkPrd")) _
                     And s.trkCrop = Val(LokCrop.EditValue) Select s).SingleOrDefault
        tb.trkUnit = TbUn.trkUnit
        tb.weight = Val(GVPrdTo.GetRowCellValue(RowInd, "Weight"))
        tb.delToDet = False
        tb.trkProd = Val(GVPrdTo.GetRowCellValue(RowInd, "trkPrd"))
        tb.amount = Val(GVPrdTo.GetRowCellValue(RowInd, "Amount"))

        col1 = GVPrdTo.Columns(2)
        '***************************************
        CalculatePrdUnit(TbUn.trkUnit, Val(GVPrdTo.GetRowCellValue(RowInd, "Amount")),
                    Val(GVPrdTo.GetRowCellValue(RowInd, "trkPrd")))

        '***************************************
        tb.untOne = UOne
        tb.amtOne = AOne
        tb.untTwo = UTwo
        tb.amtTwo = ATwo
        db.toPrdDets.InsertOnSubmit(tb)
        db.SubmitChanges()

        Progress()
        MemAddDet = False
    End Sub

    Public Sub SaveEdit()
        Dim i As Integer = 0
        Dim tb As New toPrdDet
        Dim TbUn As New V_prdUnit
        Dim prd As Integer
        Dim TheReq As Integer
        If GVPrdTo.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVPrdTo.RowCount
                prd = Val(GVPrdTo.GetRowCellValue(i, "trkPrd"))
                TheReq = Val(GVPrdTo.GetRowCellValue(i, "trkToDet"))
                If (GVPrdTo.IsRowSelected(i) = True) Then
                    If CheckEditDel(prd) = True Then
                        If CanSave(i) = True Then
                            tb = (From s In db.toPrdDets Where s.trkToDet = Val(TheReq) And s.trkToPrd = Val(TxtRId.Text) Select s).SingleOrDefault()
                            'tb.trkToPrd = Val(TxtRId.Text)
                            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVPrdTo.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkProd = Val(GVPrdTo.GetRowCellValue(i, "trkPrd")) _
                                 And s.trkCrop = Val(LokCrop.EditValue) Select s).SingleOrDefault
                            tb.trkUnit = TbUn.trkUnit
                            tb.weight = Val(GVPrdTo.GetRowCellValue(i, "Weight"))
                            tb.delToDet = False
                            tb.trkProd = Val(GVPrdTo.GetRowCellValue(i, "trkPrd"))
                            tb.amount = Val(GVPrdTo.GetRowCellValue(i, "Amount"))
                            CalculatePrdUnit(TbUn.trkUnit, Val(GVPrdTo.GetRowCellValue(i, "Amount")),
                            Val(GVPrdTo.GetRowCellValue(i, "trkPrd")))

                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                            GVPrdTo.UnselectRow(i)

                        End If
                    End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVPrdTo.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub
    Public Sub FillProd()
        Dim prd = (From s In db.products Where s.delPrd = False And s.trkCrop = Val(LokCrop.EditValue)
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
    Private Sub TheExit()
        If GVPrdTo.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لم يتم حفظ السجلات المعدلة  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        Else
            If SaveInclose() = False Then
                ResetAtClose()
                Me.Close()
                '***********************
            Else
                BtnSave_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    '*****************this to edit saved data
    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokClient.Text = ""
        FillClient()
        SetAvail(LokUnit.ItemIndex)
        If saved = True Then
            MemEdit = True
            WhenView()
        End If
        LokPeeler.Text = ""
        FillPeeler()
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
    Private Sub DateTo_EditValueChanged(sender As Object, e As EventArgs) Handles DateTo.EditValueChanged
        SetAvail(LokUnit.ItemIndex)
        If saved = True Then
            MemEdit = True
            WhenView()
        End If
    End Sub
    Public Function CalLocClnt() As Boolean
        If RdoLocal.Checked = True Then
            CalLocClnt = True
        ElseIf RdoClient.Checked = True
            CalLocClnt = False
        End If
    End Function
    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub
    Public Function SaveInclose() As Boolean

        If Row <> 0 Then
            If Val(GVPrdTo.GetRowCellValue(GVPrdTo.RowCount - 1, "trkToDet")) = 0 Then
                'If tb.trkBuyDet = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVPrdTo.RowCount - 1
                    GVPrdTo.FocusedRowHandle = lastRow
                    GVPrdTo.DeleteRow(lastRow)
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
            If CanEditChild(Val(TxtRId.Text)) = True Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ التعديلات الأخيرة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                End If
            End If
        End If


    End Function

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewToPrd

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
        rpt.FilterString = " [trkToPrd] =" & Val(TxtRId.Text)
        rpt.TheFilter.Visible = False
        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub

    Private Sub LokPeeler_TextChanged(sender As Object, e As EventArgs) Handles LokPeeler.TextChanged
        SetAvail(LokUnit.ItemIndex)

        If saved = True Then
            MemEdit = True
            WhenView()
        End If

    End Sub

    Private Sub TxtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtAmount.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub

    Private Sub WhenView()
        Dim tb As New toPrd
        Dim isloc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isloc = True
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isloc = False
            Clnt = Val(LokClient.EditValue)
        End If
        '**********************************
        Dim TheDate As DateTime
        If Me.DateTo.Text <> "" Then
            TheDate = CType(DateTo.Text, DateTime)
        End If
        tb = (From s In db.toPrds Where s.trkToPrd = Val(TxtRId.Text) And s.trkArival = Val(LokLoc.EditValue) _
                                      And s.trkPeeler = Val(LokPeeler.EditValue) And s.delToPrd = False _
                                      And s.isLocal = isloc And s.trkClntCrp = Clnt And s.trkCrop = Val(LokCrop.EditValue) _
                                      And s.toPrdDate <= TheDate Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            TxtAvl.Text = Val(TxtAvl.Text) + Val(TxtAmount.Text)
        End If
    End Sub
    Private Sub LokCrop_TextChanged(sender As Object, e As EventArgs) Handles LokCrop.TextChanged
        SetAvail(LokUnit.ItemIndex)

        If saved = True Then
            MemEdit = True
            WhenView()
        End If

        FillProd()
        Dim i As Integer
        Dim PrdTxt As String
        FillUnitCrp()
        If GVPrdTo.RowCount <> 0 Then
            While i < GVPrdTo.RowCount
                PrdTxt = GVPrdTo.GetRowCellDisplayText(i, "trkPrd")
                If GVPrdTo.GetRowCellValue(i, "trkToDet") <> 0 And Trim(PrdTxt) = "" Then
                    GVPrdTo.SelectRow(i)
                Else
                    GVPrdTo.UnselectRow(i)
                End If
                i = i + 1
            End While
        End If
    End Sub
    Private Sub LokUnit_TextChanged(sender As Object, e As EventArgs) Handles LokUnit.TextChanged
        SetAvail(LokUnit.ItemIndex)
        Dim amt As Double
        Dim trsh As Double
        Dim avl As Double
        If saved = True Then
            Dim tb As New toPrd
            tb = (From s In db.toPrds Where s.trkToPrd = Val(TxtRId.Text) Select s).SingleOrDefault()
            If Not IsNothing(tb) Then
                If LokUnit.ItemIndex = 0 Then
                    amt = tb.crpAmtOne
                    TxtAmount.Text = Math.Round(amt, 2)
                Else
                    amt = tb.crpAmtTwo
                    TxtAmount.Text = Math.Round(amt, 2)
                End If
                CalculateUnit(tb.cropUnit, tb.trashAmt, Val(LokCrop.EditValue))
                If LokUnit.ItemIndex = 0 Then
                    TxtTrash.Text = Math.Round(AOne, 2)

                Else
                    TxtTrash.Text = Math.Round(ATwo, 2)
                End If
            End If

            'CalculateUnit(tb.cropUnit, tb.cropAmt, Val(LokCrop.EditValue))
            'If LokUnit.ItemIndex = 0 Then
            '    TxtAmount.Text = AOne

            'Else
            '    TxtAmount.Text = ATwo
            'End If
            'CalculateUnit(tb.cropUnit, tb.trashAmt, Val(LokCrop.EditValue))
            'If LokUnit.ItemIndex = 0 Then
            '    TxtTrash.Text = AOne
            'Else
            '    TxtTrash.Text = ATwo
            'End If
            MemEdit = True
            avl = Val(TxtAvl.Text) + Val(TxtAmount.Text)
            TxtAvl.Text = Math.Round(avl, 2)

        End If
    End Sub

    Private Sub TxtAmount_TextChanged(sender As Object, e As EventArgs) Handles TxtAmount.TextChanged
        TxtFinalAmt.Text = Val(TxtAmount.Text) - Val(TxtTrash.Text)
        If saved = True Then
            MemEdit = True
        End If

    End Sub
    Private Sub TxtTrash_TextChanged(sender As Object, e As EventArgs) Handles TxtTrash.TextChanged
        TxtFinalAmt.Text = Val(TxtAmount.Text) - Val(TxtTrash.Text)
        If saved = True Then
            MemEdit = True
        End If

    End Sub
    Private Sub TxtTrash_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtTrash.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub

    Private Sub TxtToInf_TextChanged(sender As Object, e As EventArgs) Handles TxtToInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub


    Private Sub GVPrdTo_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVPrdTo.CustomRowCellEditForEditing
        Dim prd As Integer
        If e.Column.Caption = "المنتج" Then
            GVPrdTo.SetFocusedRowCellValue("trkUnit", "")
        End If
        If e.Column.Caption = "الوحدة" Then
            prd = GVPrdTo.GetRowCellValue(e.RowHandle, "trkPrd")
            If prd <> 0 Then
                FillUnit(prd)
                e.RepositoryItem = repUnit
            End If
        End If
    End Sub
    Public Sub FillUnit(ByVal prd As Integer)

        Dim un = (From s In db.V_prdUnits Where s.delPU = False And s.trkProd = prd _
                                               And s.trkCrop = Val(LokCrop.EditValue)
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

    Private Function CanAddPrev() As Boolean
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = 1
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = 0
            Clnt = Val(LokClient.EditValue)
        End If
        '**********************************
        CanAddPrev = False
        Dim TheDate As DateTime
        TheDate = CType(Me.DateTo.Text, DateTime)
        Dim lst = (From s In db.toPrds Where s.toPrdDate > TheDate.ToShortDateString And s.trkCrop = Val(LokCrop.EditValue) _
                                             And s.trkArival = Val(LokLoc.EditValue) And s.trkPeeler = Val(LokPeeler.EditValue) _
                                            And s.isLocal = isLoc And s.trkClntCrp = Clnt
                   Select s).ToList()

        If lst.Count > 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكن تعديل كمية المحصول ...توجد سجلات اخرى ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
    End Function

    Private Sub RdoLocal_Click(sender As Object, e As EventArgs) Handles RdoLocal.Click
        LokClient.Enabled = False
        LokClient.EditValue = -1
        SetAvail(LokUnit.ItemIndex)
        If saved = True Then
            MemEdit = True
            WhenView()
        End If
    End Sub

    Private Sub RdoClient_Click(sender As Object, e As EventArgs) Handles RdoClient.Click
        LokClient.Enabled = True
        SetAvail(LokUnit.ItemIndex)
        If saved = True Then
            MemEdit = True
            WhenView()
        End If
    End Sub

    Private Sub LokClient_EditValueChanged(sender As Object, e As EventArgs) Handles LokClient.EditValueChanged
        SetAvail(LokUnit.ItemIndex)
        If saved = True Then
            MemEdit = True
            WhenView()
        End If
    End Sub

End Class