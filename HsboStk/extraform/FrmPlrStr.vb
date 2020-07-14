Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Public Class FrmPlrStr
    Private Sub FrmPlrStr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        ClearForm()
        FillPeeler()
        RestForm()
        ProgressBarControl1.Visible = False
    End Sub
    Private Sub FillPeeler()
        Dim lst = (From c In db.peelers Where c.delPe = False Select c).ToList
        LokPeeler.Text = ""
        Me.LokPeeler.Properties.DataSource = lst
        LokPeeler.Properties.DisplayMember = "peelerName"
        LokPeeler.Properties.ValueMember = "trkPeeler"
        LokPeeler.Properties.PopulateColumns()
        LokPeeler.Properties.Columns(0).Visible = False
        LokPeeler.Properties.Columns(2).Visible = False
        LokPeeler.Properties.Columns(3).Visible = False
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
                    Dim tb As New peelerStore
                    tb = (From s In db.peelerStores Where s.trkPStore = Val(TxtID.Text) Select s).Single()
                    tb.delSp = True
                    db.SubmitChanges()
                    ClearForm()
                End If
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End If
    End Sub
    Public Function CanDelete() As Boolean
        'CanDelete = False
        'Dim lstTo = (From s In db.toPrds Where s.trkPStore = Val(TxtID.Text) _
        '            And s.delToPrd = 0 Select s).ToList()
        'If lstTo.Count > 0 Then
        '    Exit Function
        'End If
        'CanDelete = True
    End Function
    Private Sub ClearForm()
        TxtID.EditValue = ""
        TxtName.EditValue = ""
        LokPeeler.Text = ""
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
        If LokPeeler.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المنطقة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokPeeler.Focus()
            LokPeeler.SelectAll()
            Exit Function
        End If
        If Trim(TxtName.Text) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  اسم القشارة أو الغربال", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        If LocAvail() = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً اسم المخزن في القشارة مسجل مسبقاً", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        CanSave = True
    End Function
    Private Function LocAvail() As Boolean
        TxtName.Text = CleanStr(TxtName.Text)
        LocAvail = False
        Dim tb As New peelerStore
        tb = (From s In db.peelerStores Where s.pStore.Trim Like Trim(TxtName.Text) And s.delSp = False _
        And s.trkPStore <> Val(TxtID.Text) And s.trkPeeler = Val(LokPeeler.EditValue)
              Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            LocAvail = False
        Else
            LocAvail = True
        End If
    End Function
    Private Sub SaveData()
        Dim tb As New peelerStore
        If MemAdd = True Then
            tb.trkPStore = NewKey()
            tb.pStore = TxtName.Text
            tb.trkPeeler = Val(LokPeeler.EditValue)
            tb.delSp = False
            db.peelerStores.InsertOnSubmit(tb)
        ElseIf MemEdit = True
            tb = (From s In db.peelerStores Where s.trkPStore = Val(TxtID.Text) Select s).Single()
            tb.pStore = TxtName.Text
            tb.trkPeeler = Val(LokPeeler.EditValue)
        End If
        db.SubmitChanges()
        Progress()
    End Sub
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.peelerStores Select trk.trkPStore).ToList
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
        Dim Mylst As New LstPeelerStore
        Mylst.ShowDialog()
        If ID <> 0 Then
            TxtID.Text = ID
            Dim tb As New V_PeelerStore
            tb = (From s In db.V_PeelerStores Where s.trkPStore = Val(ID) Select s).SingleOrDefault
            TxtName.Text = tb.pStore
            LokPeeler.Reset()
            LokPeeler.EditValue = tb.trkPeeler
            LokPeeler.Refresh()
            LokPeeler.Text = tb.peelerName
            End If
            If Not MemEdit Then
                MemEdit = True
                TxtName.Focus()
            Else
                RestForm()
            ClearForm()
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