﻿Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Public Class FrmArival
    Private Sub FrmArival_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                    Dim tb As New arivalLoc
                    tb = (From s In db.arivalLocs Where s.trkArival = Val(TxtID.Text) Select s).Single()
                    tb.delAr = True
                    db.SubmitChanges()
                    ClearForm()
                End If
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub
    Public Function CanDelete() As Boolean
        CanDelete = False

        Dim lstArStr = (From s In db.arivalStores Where s.trkArival = Val(TxtID.Text) And s.delSa = 0 Select s).ToList()
        If lstArStr.Count > 0 Then
            Exit Function
        End If
        Dim lstArPrdStr = (From s In db.arivalPrdStores Where s.trkArival = Val(TxtID.Text) And s.delAPrd = 0 Select s).ToList()
        If lstArPrdStr.Count > 0 Then
            Exit Function
        End If
        Dim lstPeeler = (From s In db.peelers Where s.trkArival = Val(TxtID.Text) And s.delPe = 0 Select s).ToList()
        If lstPeeler.Count > 0 Then
            Exit Function
        End If
        Dim lstArv = (From s In db.arrives Where s.trkArival = Val(TxtID.Text) And s.delAr = 0 Select s).ToList()
        If lstArv.Count > 0 Then
            Exit Function
        End If
        Dim lstShp = (From s In db.shipments Where s.trkArival = Val(TxtID.Text) And s.delShip = 0 Select s).ToList()
        If lstShp.Count > 0 Then
            Exit Function
        End If
        Dim lstArExp = (From s In db.arriveExps Where s.trkArival = Val(TxtID.Text) And s.delArExp = 0 Select s).ToList()
        If lstArExp.Count > 0 Then
            Exit Function
        End If

        Dim lstExShp = (From s In db.expShips Where s.trkArival = Val(TxtID.Text) And s.delExpShip = 0 Select s).ToList()
        If lstArExp.Count > 0 Then
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
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  رقم المنطقة", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtID.Focus()
            TxtID.SelectAll()
            Exit Function
        End If

        If Trim(TxtName.Text) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  اسم منطقة الوصول", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If

        If LocAvail() = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً اسم منطقة الوصول مسجل مسبقاً", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        CanSave = True
    End Function
    Private Function LocAvail() As Boolean
        TxtName.Text = CleanStr(TxtName.Text)
        LocAvail = False
        Dim tb As New arivalLoc
        tb = (From s In db.arivalLocs Where s.arivalName.Trim Like Trim(TxtName.Text) And s.delAr = False _
And s.trkArival <> Val(TxtID.Text)
              Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            LocAvail = False
        Else
            LocAvail = True
        End If
    End Function
    Private Sub SaveData()
        Dim tb As New arivalLoc
        If MemAdd = True Then
            tb.trkArival = NewKey()
            tb.arivalName = TxtName.Text
            tb.delAr = False
            db.arivalLocs.InsertOnSubmit(tb)
        ElseIf MemEdit = True
            tb = (From s In db.arivalLocs Where s.trkArival = Val(TxtID.Text) Select s).Single()
            tb.arivalName = TxtName.Text
        End If
        db.SubmitChanges()
        Progress()
    End Sub
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.arivalLocs Select trk.trkArival).ToList
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
        Dim Mylst As New LstArival
        Mylst.ShowDialog()
        If ID <> 0 Then
            TxtID.Text = ID
        Dim tb As New arivalLoc
        tb = (From s In db.arivalLocs Where s.trkArival = Val(ID) Select s).Single
        TxtName.Text = tb.arivalName

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