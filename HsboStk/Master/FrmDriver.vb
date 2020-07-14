Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Data
Imports DevExpress.XtraReports.UI
Public Class FrmDriver

    Private Sub FrmDriver_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        ProgressBarControl1.Visible = False
        FillGrid()
        GVDriver.ClearSelection()
        ClearForm()

    End Sub

    Private Sub FillGrid()
        Dim lst = (From s In db.drivers Where s.delD = False Select s).ToList
        GridControl1.DataSource = lst
        GVDriver.Columns(0).Caption = "الرقم"
        GVDriver.Columns(1).Caption = "اسم السائق"
        GVDriver.Columns(2).Caption = "رقم الهاتف"
        GVDriver.Columns(3).Caption = "معلومات أخرى"
        GVDriver.Columns(0).Width = 2
        GVDriver.Columns(1).Width = 40
        GVDriver.Columns(2).Width = 15
        GVDriver.Columns(4).Visible = False
        GVDriver.OptionsCustomization.AllowColumnResizing = False
        GVDriver.OptionsCustomization.AllowColumnMoving = False

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
                    Dim tb As New driver
                    tb = (From s In db.drivers Where s.trkDriver = Val(TxtID.Text) Select s).Single()
                    tb.delD = True
                    db.SubmitChanges()
                    FillGrid()
                    GVDriver.UnselectRow(0)
                    ClearForm()
                End If
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub
    Public Function CanDelete() As Boolean
        CanDelete = False
        'Dim lstShp = (From s In db.shipDetails Where s.trkDriver = Val(TxtID.Text) And s.delSD = 0 Select s).ToList()
        'If lstShp.Count > 0 Then
        '    Exit Function
        'End If
        'Dim lstExpShp = (From s In db.expShipDets Where s.trkDriver = Val(TxtID.Text) And s.delExShDet = 0 Select s).ToList()
        'If lstExpShp.Count > 0 Then
        '    Exit Function
        'End If
        'Dim lstArDet = (From s In db.arriveDetails Where s.trkDriver = Val(TxtID.Text) And s.delAD = 0 Select s).ToList()
        'If lstArDet.Count > 0 Then
        '    Exit Function
        'End If
        'Dim lstAr = (From s In db.arriveExpDets Where s.trkDriver = Val(TxtID.Text) And s.delArExDt = 0 Select s).ToList()
        'If lstAr.Count > 0 Then
        '    Exit Function
        'End If
        'CanDelete = True
    End Function
    Private Sub ClearForm()
        TxtID.EditValue = ""
        TxtName.EditValue = ""
        TxtTel.EditValue = ""
        TxtInfo.EditValue = ""
        GVDriver.OptionsSelection.MultiSelect = True
        GVDriver.FocusedRowHandle = GridControl1.InvalidRowHandle
        GVDriver.ClearSelection()

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
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  اسم السائق", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        If LocAvail() = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً اسم السائق مسجل مسبقاً", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        CanSave = True
    End Function

    Private Function LocAvail() As Boolean
        TxtName.Text = CleanStr(TxtName.Text)
        LocAvail = False
        Dim tb As New driver
        tb = (From s In db.drivers Where s.driverName.Trim Like Trim(TxtName.Text) And s.delD = False _
        And s.trkDriver <> Val(TxtID.Text)
              Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            LocAvail = False
        Else
            LocAvail = True
        End If
    End Function

    Private Sub SaveData()
        Dim tb As New driver
        If MemAdd = True Then
            tb.trkDriver = NewKey()
            tb.driverName = TxtName.Text
            tb.driverTel = Val(TxtTel.Text)
            tb.driverInfo = TxtInfo.Text
            tb.delD = False
            db.drivers.InsertOnSubmit(tb)
        ElseIf MemEdit = True
            tb = (From s In db.drivers Where s.trkDriver = Val(TxtID.Text) Select s).Single()
            tb.driverName = TxtName.Text
            tb.driverTel = Val(TxtTel.Text)
            tb.driverInfo = TxtInfo.Text
            tb.delD = False
        End If
        db.SubmitChanges()
        Progress()
        If MemEdit = True Then
            If GVDriver.SelectedRowsCount <> 0 Then
                Dim currentRow As Integer() = GVDriver.GetSelectedRows()
                FillGrid()
                GVDriver.ClearSelection()
                GVDriver.SelectRow(currentRow(0))
                GVDriver.UnselectRow(0)
            End If
        End If
        If MemAdd = True Then
            FillGrid()
            GVDriver.SelectRow(GVDriver.RowCount - 1)
            GVDriver.UnselectRow(0)
        End If

    End Sub
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.drivers Select trk.trkDriver).ToList
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
        GVDriver.OptionsFind.FindFilterColumns = "*"
        GVDriver.OptionsFind.ShowClearButton = False
        GVDriver.OptionsFind.ShowFindButton = False
        GVDriver.OptionsFind.ShowCloseButton = True
        GVDriver.ShowFindPanel()

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

    Private Sub GVDriver_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles GVDriver.SelectionChanged
        Dim countSelect As Integer = GVDriver.SelectedRowsCount
        If countSelect <> 0 Then
            Dim RowInd As Integer() = GVDriver.GetSelectedRows()
            TxtID.Text = Val(GVDriver.GetRowCellValue(RowInd(0), "trkDriver"))
            TxtName.Text = GVDriver.GetRowCellValue(RowInd(0), "driverName")
            TxtTel.Text = GVDriver.GetRowCellValue(RowInd(0), "driverTel")
            TxtInfo.Text = GVDriver.GetRowCellValue(RowInd(0), "driverInfo")
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

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim rpt As New RepViewDriver

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
        rpt.FilterString = " [delD] = False"

        '******************
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub
End Class