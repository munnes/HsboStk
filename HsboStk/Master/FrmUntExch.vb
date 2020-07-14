
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Public Class FrmUntExch
    Private Sub FrmUntExch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        ClearForm()
        RestForm()
        FillFstUnit()
        ProgressBarControl1.Visible = False
    End Sub
    Private Sub ClearForm()
        TxtID.EditValue = ""
        TxtAmt.EditValue = ""
        LokFstUnt.Text = ""
        LokSecUnt.Text = ""
    End Sub
    Private Sub RestForm()
        MemAdd = False
        MemEdit = False
        MemFind = False
    End Sub
    Private Sub FillFstUnit()
        done = False
        Dim lst = (From c In db.units Where c.delUn = False Select c).ToList
        Me.LokFstUnt.Properties.DataSource = lst
        LokFstUnt.Properties.DisplayMember = "unitName"
        LokFstUnt.Properties.ValueMember = "trkUnit"
        LokFstUnt.Properties.PopulateColumns()
        LokFstUnt.Properties.Columns(0).Visible = False
        LokFstUnt.Properties.Columns(2).Visible = False
        done = True
    End Sub
    Private Sub FillSecUnit()
        If done = True And LokFstUnt.Text <> "" Then
            Dim lst = (From c In db.units Where c.delUn = False _
And c.trkUnit <> Val(LokFstUnt.EditValue)
                       Select c).ToList
            Me.LokSecUnt.Properties.DataSource = lst
            LokSecUnt.Properties.DisplayMember = "unitName"
            LokSecUnt.Properties.ValueMember = "trkUnit"
            LokSecUnt.Properties.PopulateColumns()
            LokSecUnt.Properties.Columns(0).Visible = False
            LokSecUnt.Properties.Columns(2).Visible = False
        End If

    End Sub

    Private Sub LokFstUnt_TextChanged(sender As Object, e As EventArgs) Handles LokFstUnt.TextChanged
        LokSecUnt.Text = ""
        FillSecUnit()
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        ResetAtClose()
        Me.Close()
        FrmMain.RibbonControl.Enabled = True
    End Sub

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If Not MemAdd Then
            MemAdd = True
            MemEdit = False
            ClearForm()
            TxtID.Text = NewKey()
            '   Me.TxtName.Focus()
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

        If MemEdit Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

                If Msg = MsgBoxResult.Yes Then
                    Dim tb As New unitExch
                    tb = (From s In db.unitExches Where s.trkFstUnt = Val(LokFstUnt.EditValue) _
                           And s.trkSecUnt = Val(LokSecUnt.EditValue) Select s).Single()
                    tb.delUnEx = True

                    db.SubmitChanges()
                    ClearForm()
                End If
            End If

    End Sub


    Private Function CanSave() As Boolean
        CanSave = False
        If Val(TxtID.Text) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  الرقم", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtID.Focus()
            TxtID.SelectAll()
            Exit Function
        End If
        If LokFstUnt.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال  وحدة القياس الأولى ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokFstUnt.Focus()
            LokFstUnt.SelectAll()
            Exit Function
        End If
        If Val(TxtAmt.Text) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  التحويل", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtAmt.Focus()
            TxtAmt.SelectAll()
            Exit Function
        End If
        If LokSecUnt.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال وحدة القياس الثانية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokSecUnt.Focus()
            LokSecUnt.SelectAll()
            Exit Function
        End If
        '****** chk relation avail
        Dim lst = (From s In db.unitExches Where s.trkFstUnt = Val(LokFstUnt.EditValue) And
                                               s.trkSecUnt = Val(LokSecUnt.EditValue) Select s).ToList
        If lst.Count <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بتعين العلاقة بين الوحدتين مسبقاً, يمكن البحث والتعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        Dim lstSec = (From s In db.unitExches Where s.trkFstUnt = Val(LokSecUnt.EditValue) And
                                               s.trkSecUnt = Val(LokFstUnt.EditValue) Select s).ToList
        If lstSec.Count <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لقد قمت بتعين العلاقة بين الوحدتين مسبقاً, يمكن البحث والتعديل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CanSave = True
    End Function
    Private Sub SaveData()
        Dim tb As New unitExch
        If MemAdd = True Then
            tb.trkUntEx = NewKey()
            tb.trkFstUnt = Val(LokFstUnt.EditValue)
            tb.exchgVal = Val(TxtAmt.Text)
            tb.trkSecUnt = Val(LokSecUnt.EditValue)
            tb.delUnEx = False
            db.unitExches.InsertOnSubmit(tb)
        ElseIf MemEdit = True
            tb = (From s In db.unitExches Where s.trkUntEx = Val(TxtID.Text) Select s).Single()
            tb.trkFstUnt = Val(LokFstUnt.EditValue)
            tb.exchgVal = Val(TxtAmt.Text)
            tb.trkSecUnt = Val(LokSecUnt.EditValue)
            tb.delUnEx = False
        End If
        db.SubmitChanges()
        Progress()
    End Sub
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.unitExches Select trk.trkUntEx).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Private Sub Progress()
        ProgressBarControl1.Visible = True
        Me.ProgressBarControl1.EditValue = 0
        Me.Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub

    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        RestForm()
        ClearForm()
        MemReq = trk
        Dim Mylst As New LstUntExch
        Mylst.ShowDialog()
        If ID <> 0 Then
            TxtID.Text = ID
            Dim tb As New V_unitExch
            tb = (From s In db.V_unitExches Where s.trkUntEx = Val(ID) Select s).Single
            LokFstUnt.Reset()
            LokFstUnt.EditValue = tb.trkFstUnt
            LokFstUnt.Refresh()
            LokSecUnt.Reset()
            LokSecUnt.EditValue = tb.trkSecUnt
            LokSecUnt.Refresh()
            TxtAmt.Text = tb.exchgVal
            If Not MemEdit Then
                MemEdit = True
                LokFstUnt.Focus()
            Else
                RestForm()
                ClearForm()
            End If
            ID = 0
        End If
    End Sub

    Private Sub TxtAmt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtAmt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub

    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub
End Class