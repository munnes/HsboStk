Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Imports DevExpress.XtraEditors.Controls

Public Class FrmCrop
    Private Sub FrmCrop_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        ClearForm()
        FillUnit()
        RestForm()
        ProgressBarControl1.Visible = False
    End Sub

    Private Sub FillUnit()
        Dim lst = (From c In db.units Where c.delUn = False Select c).ToList
        LstUnt.DataSource = lst
        LstUnt.DisplayMember = "unitName"
        LstUnt.ValueMember = "trkUnit"

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
                    Dim tb As New crop
                    tb = (From s In db.crops Where s.trkCrop = Val(TxtID.Text) Select s).Single()
                    tb.delCrop = True
                    Dim i As Integer = 0
                    Dim lst = (From s In db.cropUnits Where s.trkCrop = Val(TxtID.Text) And s.delCU = False Select s).ToList
                    While i < lst.Count
                        Dim tbCU As New cropUnit
                        tbCU = (From s In db.cropUnits Where s.trkCrop = Val(TxtID.Text) And s.trkUnit = lst.Item(i).trkUnit
                                Select s).Single()
                        tbCU.delCU = True
                        i = i + 1
                    End While
                    db.SubmitChanges()
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
        LstUnt.UnCheckAll()
    End Sub
    Private Sub RestForm()
        MemAdd = False
        MemEdit = False
        MemFind = False
    End Sub
    Private Function CanSave() As Boolean

        If Val(TxtID.Text) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  رقم المحصول", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtID.Focus()
            TxtID.SelectAll()
            Exit Function
        End If

        If Trim(TxtName.Text) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  اسم المحصول", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        If LocAvail() = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً اسم المحصول مسجل مسبقاً", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        If LstUnt.CheckedItemsCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء تحديد وحدات القياس", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Exit Function
        End If
        If LstUnt.CheckedItemsCount > 2 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً لا يمكنك تعين أكثر من وحدتين للمحصول الواحد", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Exit Function

        End If
        CanSave = True
    End Function
    Private Function LocAvail() As Boolean
        TxtName.Text = CleanStr(TxtName.Text)
        LocAvail = False
        Dim tb As New crop
        tb = (From s In db.crops Where s.cropName.Trim Like Trim(TxtName.Text) And s.delCrop = False _
        And s.trkCrop <> Val(TxtID.Text)
              Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            LocAvail = False
        Else
            LocAvail = True
        End If
    End Function
    Private Sub SaveData()
        Dim tb As New crop

        If MemAdd = True Then
            tb.trkCrop = NewKey()
            tb.cropName = TxtName.Text
            tb.delCrop = False
            db.crops.InsertOnSubmit(tb)
            db.SubmitChanges()
            '******************Unit
            Dim i As Integer = 0
            Dim Unt = (From s In LstUnt.CheckedItems Select s.trkUnit).ToList
            While i < Unt.Count
                Dim tbRel As New cropUnit
                tbRel.trk = NewKeyRel()
                tbRel.trkCrop = Val(TxtID.Text)
                tbRel.trkUnit = Unt.Item(i)
                tbRel.delCU = False
                db.cropUnits.InsertOnSubmit(tbRel)
                db.SubmitChanges()
                i = i + 1
            End While
            '******************
        ElseIf MemEdit = True
            tb = (From s In db.crops Where s.trkCrop = Val(TxtID.Text) Select s).Single()
            tb.cropName = TxtName.Text
            db.SubmitChanges()
            '**********************
            Dim Unt = (From s In LstUnt.CheckedItems Select s.trkUnit).ToList
            Dim i As Integer = 0
            While i < Unt.Count
                Dim tbRel As New cropUnit
                Dim TheUnt As Integer
                TheUnt = Unt.Item(i)
                tbRel = (From s In db.cropUnits Where s.trkCrop = Val(TxtID.Text) And s.trkUnit = TheUnt Select s).SingleOrDefault


                If Not IsNothing(tbRel) Then

                    tbRel = (From s In db.cropUnits Where s.trkCrop = Val(TxtID.Text) And s.trkUnit = TheUnt And s.delCU = True Select s).SingleOrDefault
                    If Not IsNothing(tbRel) Then
                        tbRel.delCU = 0
                        db.SubmitChanges()
                    End If
                Else
                    Dim tbRelSave As New cropUnit
                    tbRelSave.trk = NewKeyRel()
                    tbRelSave.trkCrop = Val(TxtID.Text)
                    tbRelSave.trkUnit = Unt.Item(i)
                    tbRelSave.delCU = False
                    db.cropUnits.InsertOnSubmit(tbRelSave)
                    db.SubmitChanges()
                End If

                i = i + 1
            End While
            '*******************************************

            Dim lst = (From s In db.cropUnits Where s.trkCrop = Val(TxtID.Text) And s.delCU = False Select s).ToList
            i = 0
            Dim j As Integer = 0
            Dim UntB = (From s In LstUnt.CheckedItems Select s.trkUnit).ToList
            While i < lst.Count
                Dim tbRel As New cropUnit
                j = 0
                While j < UntB.Count
                    If lst.Item(i).trkUnit = UntB.Item(j) Then
                        'If LstUnt.Items(j).CheckState = CheckState.Unchecked Then
                        Exit While
                    Else
                        j = j + 1

                    End If
                    If j = UntB.Count Then
                        tbRel = (From s In db.cropUnits Where s.trkCrop = Val(TxtID.Text) And s.trkUnit = lst.Item(i).trkUnit And s.delCU = False Select s).SingleOrDefault
                        tbRel.delCU = True
                        db.SubmitChanges()
                    End If
                End While
                i = i + 1
            End While
            'While i < lst.Count
            '    Dim tbRel As New cropUnit
            '    tbRel = (From s In db.cropUnits Where s.trkCrop = Val(TxtID.Text) And s.trkUnit = lst.Item(i).trkUnit _
            '                                        And s.delCU = True Select s).SingleOrDefault
            '    Dim UntChk = (From s In LstUnt.CheckedItems Where s.trkunit = lst.Item(i).trkUnit Select s.trkUnit).ToList


            'End While
        End If


        Progress()
    End Sub

    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.crops Select trk.trkCrop).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Function NewKeyRel() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.cropUnits Select trk.trk).ToList
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
        Dim Mylst As New LstCrop
        Mylst.ShowDialog()
        If ID <> 0 Then
            TxtID.Text = ID
            Dim tb As New crop
            tb = (From s In db.crops Where s.trkCrop = Val(ID) Select s).Single
            TxtName.Text = tb.cropName
            '*******************************
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim lst = (From s In db.cropUnits Where s.trkCrop = Val(TxtID.Text) And s.delCU = False Select s).ToList

            While i < LstUnt.ItemCount
                If j < lst.Count Then
                    If LstUnt.GetItemValue(i) = lst.Item(j).trkUnit Then
                        'LstUnt.Items(i).CheckState = CheckState.Checked
                        LstUnt.SetItemChecked(i, LstUnt.GetItemValue(i))
                        j = j + 1
                    End If
                End If
                i = i + 1
            End While

            '*******************************
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
    Public Function CanDelete() As Boolean
        CanDelete = False

        Dim lst = (From s In db.buyDetails Where s.trkCrop = Val(TxtID.Text) And s.delBDet = 0 Select s).ToList()
        If lst.Count > 0 Then
            Exit Function
        End If
        Dim lstBuyDet = (From s In db.aStoreDets Where s.trkCrop = Val(TxtID.Text) And s.delASDet = 0 Select s).ToList()
        If lstBuyDet.Count > 0 Then
            Exit Function
        End If
        Dim lstShpDet = (From s In db.shipDetails Where s.trkCrop = Val(TxtID.Text) And s.delSD = 0 Select s).ToList()
        If lstShpDet.Count > 0 Then
            Exit Function
        End If
        Dim lstArDet = (From s In db.arriveDetails Where s.trkCrop = Val(TxtID.Text) And s.delAD = 0 Select s).ToList()
        If lstArDet.Count > 0 Then
            Exit Function
        End If
        Dim lstOutPrd = (From s In db.outPrdDets Where s.trkCrop = Val(TxtID.Text) And s.delPrd = 0 Select s).ToList()
        If lstOutPrd.Count > 0 Then
            Exit Function
        End If
        Dim lstToPrd = (From s In db.toPrds Where s.trkCrop = Val(TxtID.Text) And s.delToPrd = 0 Select s).ToList()
        If lstToPrd.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function


End Class