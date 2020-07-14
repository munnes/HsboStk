
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Imports DevExpress.XtraEditors.Controls
Public Class FrmUsers
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If Not MemAdd Then
            MemAdd = True
            MemEdit = False
            ClearForm()
            TxtTrk.Text = NewKey()
            Me.TxtName.Focus()
        Else
            ClearForm()
            RestForm()
        End If
    End Sub
    Private Sub ClearForm()
        TxtTrk.Text = ""
        TxtName.Text = ""
        TxtId.Text = ""
        TxtP1.Text = ""
        TxtP2.Text = ""
    End Sub

    Private Sub FrmUsers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        ClearForm()

        RestForm()
        ProgressBarControl1.Visible = False
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If MemAdd Or MemEdit Then
            If CanSave() = True Then
                SaveData()
                RestForm()
            End If
        End If
    End Sub
    Private Sub RestForm()
        MemAdd = False
        MemEdit = False
        MemFind = False
    End Sub
    Private Function CanSave() As Boolean

        If Val(TxtTrk.Text) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  رقم المستخدم", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtId.Focus()
            TxtId.SelectAll()
            Exit Function
        End If

        If Trim(TxtName.Text) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  اسم المحصول", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If

        If Trim(TxtId.Text) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  كلمة المرور", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtId.Focus()
            TxtId.SelectAll()
            Exit Function
        End If
        If UAvail() = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً كلمة المرور مسجل مسبقاً", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtId.Focus()
            TxtId.SelectAll()
            Exit Function
        End If
        If Trim(TxtP1.Text) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  كلمة السر", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtP1.Focus()
            TxtP1.SelectAll()
            Exit Function
        End If
        If Trim(TxtP2.Text) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال تأكيد كلمة السر", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtP2.Focus()
            TxtP2.SelectAll()
            Exit Function
        End If
        If Trim(TxtP1.Text) <> Trim(TxtP2.Text) Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً  كلمة السر غير متطابقة", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtP2.Focus()
            TxtP2.SelectAll()
            Exit Function
        End If


        CanSave = True
    End Function
    Private Function UAvail() As Boolean
        TxtName.Text = CleanStr(TxtName.Text)
        UAvail = False
        Dim tb As New user
        tb = (From s In db.users Where s.uId.Trim Like Trim(TxtId.Text) And s.uDel = False _
        And s.uTrk <> Val(TxtTrk.Text)
              Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            UAvail = False
        Else
            UAvail = True
        End If
    End Function
    Private Sub SaveData()
        Dim tb As New user

        If MemAdd = True Then
            tb.uTrk = NewKey()
            tb.uName = TxtName.Text
            tb.uId = TxtId.Text
            tb.uPass = TxtP1.Text
            tb.uDel = False
            db.users.InsertOnSubmit(tb)

            '******************Unit
        ElseIf MemEdit = True
            tb = (From s In db.users Where s.uTrk = Val(TxtTrk.Text) Select s).Single()
            tb.uName = TxtName.Text
            tb.uId = TxtId.Text
            tb.uPass = TxtP1.Text
            tb.uDel = False
        End If
        db.SubmitChanges()
        Progress()

    End Sub
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.users Select trk.uTrk).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub

    Private Sub Progress()
        ProgressBarControl1.Visible = True
        Me.ProgressBarControl1.EditValue = 0
        Me.Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs)
        ResetAtClose()
        Me.Close()
        FrmMain.RibbonControl.Enabled = True
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        ResetAtClose()
        Me.Close()
        FrmMain.RibbonControl.Enabled = True
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        RestForm()
        ClearForm()
        MemReq = trk
        Dim Mylst As New LstUser
        Mylst.ShowDialog()
        If ID <> 0 Then
            TxtTrk.Text = ID
            Dim tb As New user
            tb = (From s In db.users Where s.uTrk = Val(ID) Select s).Single
            TxtName.Text = tb.uName
            TxtId.Text = tb.uId
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

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        If MemEdit Then

            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If Msg = MsgBoxResult.Yes Then
                Dim tb As New user
                tb = (From s In db.users Where s.uTrk = Val(TxtTrk.Text) Select s).Single()
                tb.uDel = True
                db.SubmitChanges()
                    ClearForm()
                End If

        End If
    End Sub
End Class