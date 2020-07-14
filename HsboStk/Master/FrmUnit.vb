Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Public Class FrmUnit
    Private Sub FrmUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                If CanDelete() = True Then
                    Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                    If Msg = MsgBoxResult.Yes Then
                        Dim tb As New unit
                        tb = (From s In db.units Where s.trkUnit = Val(TxtID.Text) Select s).Single()
                        tb.delUn = True
                        db.SubmitChanges()
                        ClearForm()
                    End If
                Else
                    Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End If
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub
    Public Function CanDelete() As Boolean
        CanDelete = False

        Dim lstFst = (From s In db.unitExches Where s.trkFstUnt = Val(TxtID.Text) _
                      And s.delUnEx = 0 Select s).ToList()
        If lstFst.Count > 0 Then
            Exit Function
        End If
        Dim lstSec = (From s In db.unitExches Where s.trkSecUnt = Val(TxtID.Text) _
                      And s.delUnEx = 0 Select s).ToList()
        If lstSec.Count > 0 Then
            Exit Function
        End If
        Dim lstCrp = (From s In db.cropUnits Where s.trkUnit = Val(TxtID.Text) _
                      And s.delCU = 0 Select s).ToList()
        If lstCrp.Count > 0 Then
            Exit Function
        End If
        Dim lstPrd = (From s In db.prodUnits Where s.trkUnit = Val(TxtID.Text) _
                      And s.delPU = 0 Select s).ToList()
        If lstPrd.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
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
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  رقم الوحدة", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtID.Focus()
            TxtID.SelectAll()
            Exit Function
        End If

        If Trim(TxtName.Text) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  اسم الوحدة", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        If LocAvail() = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً اسم وحدة القياس مسجل مسبقاً", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        CanSave = True
    End Function
    Private Function LocAvail() As Boolean
        TxtName.Text = CleanStr(TxtName.Text)
        LocAvail = False
        Dim tb As New unit
        tb = (From s In db.units Where s.unitName.Trim Like Trim(TxtName.Text) And s.delUn = False _
        And s.trkUnit <> Val(TxtID.Text)
              Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            LocAvail = False
        Else
            LocAvail = True
        End If
    End Function
    Private Sub SaveData()
        Dim tb As New unit
        If MemAdd = True Then
            tb.trkUnit = NewKey()
            tb.unitName = TxtName.Text
            tb.delUn = False
            db.units.InsertOnSubmit(tb)
        ElseIf MemEdit = True
            tb = (From s In db.units Where s.trkUnit = Val(TxtID.Text) Select s).Single()
            tb.unitName = TxtName.Text
        End If
        db.SubmitChanges()
        Progress()
    End Sub
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.units Select trk.trkUnit).ToList
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
        Dim Mylst As New LstUnit
        Mylst.ShowDialog()
        If ID <> 0 Then
            TxtID.Text = ID
            Dim tb As New unit
            tb = (From s In db.units Where s.trkUnit = Val(ID) Select s).Single
            TxtName.Text = tb.unitName

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