Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Public Class FrmCar
    Private Sub FrmCar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        ClearForm()
        RestForm()
        ProgressBarControl1.Visible = False
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
                    Dim tb As New car
                    tb = (From s In db.cars Where s.trkCar = Val(TxtID.Text) Select s).Single()
                    tb.delC = True
                    db.SubmitChanges()
                    ClearForm()
                End If
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub
    Public Function CanDelete() As Boolean
        '    CanDelete = False
        '    Dim lstShp = (From s In db.shipDetails Where s.trkCar = Val(TxtID.Text) And s.delSD = 0 Select s).ToList()
        '    If lstShp.Count > 0 Then
        '        Exit Function
        '    End If

        '    Dim lstExpShp = (From s In db.expShipDets Where s.trkCar = Val(TxtID.Text) And s.delExShDet = 0 Select s).ToList()
        '    If lstExpShp.Count > 0 Then
        '        Exit Function
        '    End If
        '    Dim lstArDet = (From s In db.arriveDetails Where s.trkCar = Val(TxtID.Text) And s.delAD = 0 Select s).ToList()
        '    If lstArDet.Count > 0 Then
        '        Exit Function
        '    End If
        '    Dim lstAr = (From s In db.arriveExpDets Where s.trkCar = Val(TxtID.Text) And s.delArExDt = 0 Select s).ToList()
        '    If lstAr.Count > 0 Then
        '        Exit Function
        '    End If

        '    CanDelete = True
    End Function
    Private Sub ClearForm()
        TxtID.EditValue = ""
        TxtName.EditValue = ""
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
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  رقم لوحة العربية", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        If LocAvail() = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً رقم العربية مسجل مسبقاً", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        CanSave = True
    End Function
    Private Function LocAvail() As Boolean
        TxtName.Text = CleanStr(TxtName.Text)
        LocAvail = False
        Dim tb As New car
        tb = (From s In db.cars Where s.carNo.Trim Like Trim(TxtName.Text) And s.delC = False _
                                    And s.trkCar <> Val(TxtID.Text)
              Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            LocAvail = False
        Else
            LocAvail = True
        End If
    End Function
    Private Sub SaveData()
        Dim tb As New car
        If MemAdd = True Then
            tb.trkCar = NewKey()
            tb.carNo = TxtName.Text
            tb.delC = False
            db.cars.InsertOnSubmit(tb)
        ElseIf MemEdit = True
            tb = (From s In db.cars Where s.trkCar = Val(TxtID.Text) Select s).Single()
            tb.carNo = TxtName.Text
        End If
        db.SubmitChanges()
        Progress()
    End Sub
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.cars Select trk.trkCar).ToList
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
        MemReq = trk
        Dim Mylst As New LstCar
        Mylst.ShowDialog()
        If ID <> 0 Then
            TxtID.Text = ID
            Dim tb As New car
            tb = (From s In db.cars Where s.trkCar = Val(ID) Select s).Single
            TxtName.Text = tb.carNo

            If Not MemEdit Then
                MemEdit = True
                TxtName.Focus()
            Else
                RestForm()
                ClearForm()
            End If
            ID = 0
        End If
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


End Class