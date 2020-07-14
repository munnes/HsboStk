Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data
Imports DevExpress.XtraReports.UI

Public Class FrmClient

    Private Sub FrmClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        ProgressBarControl1.Visible = False
        FillGrid()
        GVClient.ClearSelection()
        FillLoc()
        ClearForm()

    End Sub

    Private Sub FillGrid()
        Dim lst = (From s In db.V_ClientLocs Where s.delClient = False Select s.trkClient, s.clientName, s.buyLoc, s.clientTel, s.clientInfo, s.trkBuyLoc).ToList
        GridControl1.DataSource = lst
        GVClient.Columns(0).Caption = "الرقم"
        GVClient.Columns(1).Caption = "اسم العميل"
        GVClient.Columns(2).Caption = "اسم المنطقة"
        GVClient.Columns(3).Caption = "رقم الهاتف"
        GVClient.Columns(4).Caption = "معلومات أخرى"
        GVClient.Columns(0).Width = 2
        GVClient.Columns(3).Width = 30

        GVClient.Columns(5).Visible = False
        GVClient.OptionsCustomization.AllowColumnResizing = False
        GVClient.OptionsCustomization.AllowColumnMoving = False

    End Sub
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If Not MemAdd Then
            MemAdd = True
            MemEdit = False
            ClearForm()
            TxtID.Text = NewKey()
            Me.TxtName.Focus()
        Else
            ClearForm()
            RestForm()
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If MemAdd Or MemEdit Then
            If CanSave() = True Then
                SaveData()
                RestForm()
            End If
        End If
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If CanDelete() = True Then
            If MemEdit Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If Msg = MsgBoxResult.Yes Then
                    Dim tb As New client
                    tb = (From s In db.clients Where s.trkClient = Val(TxtID.Text) Select s).Single()
                    tb.delClient = True
                    db.SubmitChanges()
                    FillGrid()
                    GVClient.UnselectRow(0)
                    ClearForm()
                End If
            End If
        Else
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

            End If
    End Sub

    Private Sub ClearForm()
        TxtID.EditValue = ""
        TxtName.EditValue = ""
        TxtTel.EditValue = ""
        TxtInfo.EditValue = ""
        LokLoc.Text = ""
        GVClient.OptionsSelection.MultiSelect = True
        GVClient.FocusedRowHandle = GridControl1.InvalidRowHandle
        GVClient.ClearSelection()

    End Sub
    Private Sub RestForm()
        MemAdd = False
        MemEdit = False
        MemFind = False
    End Sub
    Private Function CanSave() As Boolean
        CanSave = False
        If Val(TxtID.Text) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  الرقم", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtID.Focus()
            TxtID.SelectAll()
            Exit Function
        End If

        If Trim(TxtName.Text) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  اسم العميل", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المنطقة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If
        If LocAvail() = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً اسم العميل في منطقة الشراء مسجل مسبقاً", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        CanSave = True
    End Function
    Private Function LocAvail() As Boolean
        TxtName.Text = CleanStr(TxtName.Text)
        LocAvail = False
        Dim tb As New client
        tb = (From s In db.clients Where s.clientName.Trim Like Trim(TxtName.Text) And s.delClient = False _
        And s.trkClient <> Val(TxtID.Text) And s.trkBuyLoc = Val(LokLoc.EditValue)
              Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            LocAvail = False
        Else
            LocAvail = True
        End If
    End Function
    Private Sub SaveData()
        Dim tb As New client
        If MemAdd = True Then
            tb.trkClient = NewKey()
            tb.clientName = TxtName.Text
            tb.clientTel = Val(TxtTel.Text)
            tb.trkBuyLoc = Val(LokLoc.EditValue)
            tb.clientInfo = TxtInfo.Text
            tb.delClient = False
            db.clients.InsertOnSubmit(tb)
        ElseIf MemEdit = True
            tb = (From s In db.clients Where s.trkClient = Val(TxtID.Text) Select s).Single()
            tb.clientName = TxtName.Text
            tb.clientTel = Val(TxtTel.Text)
            tb.trkBuyLoc = Val(LokLoc.EditValue)
            tb.clientInfo = TxtInfo.Text
            tb.delClient = False
        End If
        db.SubmitChanges()
        Progress()
        If MemEdit = True Then
            If GVClient.SelectedRowsCount <> 0 Then
                Dim currentRow As Integer() = GVClient.GetSelectedRows()
                FillGrid()
                GVClient.ClearSelection()
                GVClient.SelectRow(currentRow(0))
                GVClient.UnselectRow(0)
            End If
        End If
        If MemAdd = True Then
            FillGrid()
            GVClient.SelectRow(GVClient.RowCount - 1)
            GVClient.UnselectRow(0)
        End If

    End Sub
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.clients Select trk.trkClient).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        RestForm()
        ClearForm()
        GVClient.OptionsFind.FindFilterColumns = "*"
        GVClient.OptionsFind.ShowClearButton = False
        GVClient.OptionsFind.ShowFindButton = False
        GVClient.OptionsFind.ShowCloseButton = True
        GVClient.ShowFindPanel()

    End Sub

    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        ResetAtClose()
        Me.Close()
        FrmMain.RibbonControl.Enabled = True
    End Sub
    Private Sub Progress()
        ProgressBarControl1.Visible = True
        Me.ProgressBarControl1.EditValue = 0
        Me.Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub

    Private Sub GVDriver_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles GVClient.SelectionChanged
        Dim countSelect As Integer = GVClient.SelectedRowsCount
        If countSelect <> 0 Then
            Dim RowInd As Integer() = GVClient.GetSelectedRows()
            TxtID.Text = Val(GVClient.GetRowCellValue(RowInd(0), "trkClient"))
            TxtName.Text = GVClient.GetRowCellValue(RowInd(0), "clientName")
            TxtTel.Text = GVClient.GetRowCellValue(RowInd(0), "clientTel")
            TxtInfo.Text = GVClient.GetRowCellValue(RowInd(0), "clientInfo")
            LokLoc.EditValue = GVClient.GetRowCellValue(RowInd(0), "trkBuyLoc")
            MemEdit = True
            'If Not MemEdit Then
            '    MemEdit = True
            'Else
            '    RestForm()
            'End If
        End If
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        ResetAtClose()
        Me.Close()
        FrmMain.RibbonControl.Enabled = True
    End Sub

    Private Sub TxtTel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtTel.KeyPress
        e.Handled = NumOnly(e.KeyChar)
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
    Public Function CanDelete() As Boolean
        CanDelete = False
        'buyDetail
        Dim lst = (From s In db.buyDetails Where s.trkClient = Val(TxtID.Text) _
                                               And s.delBDet = 0 Select s).ToList()
        If lst.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewClient

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


        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub
End Class