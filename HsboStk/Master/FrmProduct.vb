Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Public Class FrmProduct
    Private Sub FrmProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        ClearForm()
        RestForm()
        FillCrop()
        FillUnit()
        ProgressBarControl1.Visible = False
    End Sub
    Private Sub FillUnit()
        Dim lst = (From c In db.units Where c.delUn = False Select c).ToList
        LstUnt.DataSource = lst
        LstUnt.DisplayMember = "unitName"
        LstUnt.ValueMember = "trkUnit"

    End Sub
    Private Sub FillCrop()
        Dim lst = (From c In db.crops Where c.delCrop = False Select c).ToList
        Me.LokCrop.Properties.DataSource = lst
        LokCrop.Properties.DisplayMember = "cropName"
        LokCrop.Properties.ValueMember = "trkCrop"
        LokCrop.Properties.PopulateColumns()
        LokCrop.Properties.Columns(0).Visible = False
        LokCrop.Properties.Columns(2).Visible = False
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
                    Dim tb As New product
                    tb = (From s In db.products Where s.trkProd = Val(TxtID.Text) Select s).Single()
                    tb.delPrd = True
                    '*****************************
                    Dim i As Integer = 0
                    Dim lst = (From s In db.prodUnits Where s.trkProd = Val(TxtID.Text) And s.delPU = False Select s).ToList
                    While i < lst.Count
                        Dim tbPU As New prodUnit
                        tbPU = (From s In db.prodUnits Where s.trkProd = Val(TxtID.Text) And s.trkUnit = lst.Item(i).trkUnit
                                Select s).Single()
                        tbPU.delPU = True
                        i = i + 1
                    End While
                    '*****************************
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

        Dim lstRecDet = (From s In db.receiveDets Where s.trkProd = Val(TxtID.Text) _
                      And s.delRecDet = 0 Select s).ToList()
        If lstRecDet.Count > 0 Then
            Exit Function
        End If
        Dim lstExStk = (From s In db.expStockDets Where s.trkProd = Val(TxtID.Text) _
                    And s.delExStkDet = 0 Select s).ToList()
        If lstExStk.Count > 0 Then
            Exit Function
        End If
        Dim lstGood = (From s In db.goodShpDets Where s.trkProd = Val(TxtID.Text) _
                    And s.delGoodDet = 0 Select s).ToList()
        If lstGood.Count > 0 Then
            Exit Function
        End If
        Dim lstExpShp = (From s In db.expShipDets Where s.trkProd = Val(TxtID.Text) _
                    And s.delExShDet = 0 Select s).ToList()
        If lstExpShp.Count > 0 Then
            Exit Function
        End If
        Dim lstArExp = (From s In db.arriveExpDets Where s.trkProd = Val(TxtID.Text) _
                    And s.delArExDt = 0 Select s).ToList()
        If lstArExp.Count > 0 Then
            Exit Function
        End If
        Dim lstTo = (From s In db.toPrdDets Where s.trkProd = Val(TxtID.Text) _
                    And s.delToDet = 0 Select s).ToList()
        If lstTo.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function
    Private Sub ClearForm()
        TxtID.EditValue = ""
        TxtName.EditValue = ""
        LokCrop.Text = ""
        LstUnt.UnCheckAll()
    End Sub
    Private Sub RestForm()
        MemAdd = False
        MemEdit = False
        MemFind = False
    End Sub
    Private Function CanSave() As Boolean
        CanSave = False
        If Val(TxtID.Text) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  رقم المحصول", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtID.Focus()
            TxtID.SelectAll()
            Exit Function
        End If
        If LokCrop.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال  المحصول ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokCrop.Focus()
            LokCrop.SelectAll()
            Exit Function
        End If
        If Trim(TxtName.Text) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء إدخال  اسم المنتج", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        If LocAvail() = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً اسم المنتج مسجل مسبقاً", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            TxtName.Focus()
            TxtName.SelectAll()
            Exit Function
        End If
        If LstUnt.CheckedItemsCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء تحديد وحدات القياس", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Exit Function
        End If
        If LstUnt.CheckedItemsCount > 2 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً لا يمكنك تعين أكثر من وحدتين للمنتج الواحد", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            Exit Function

        End If
        CanSave = True
    End Function
    Private Function LocAvail() As Boolean
        TxtName.Text = CleanStr(TxtName.Text)
        LocAvail = False
        Dim tb As New product
        tb = (From s In db.products Where s.prodName.Trim Like Trim(TxtName.Text) And s.delPrd = False _
        And s.trkProd <> Val(TxtID.Text)
              Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            LocAvail = False
        Else
            LocAvail = True
        End If
    End Function
    Private Sub SaveData()
        Dim tb As New product
        If MemAdd = True Then
            tb.trkProd = NewKey()
            tb.prodName = TxtName.Text
            tb.trkCrop = Val(LokCrop.EditValue.ToString())
            tb.delPrd = False
            db.products.InsertOnSubmit(tb)
            db.SubmitChanges()
            '******************Unit
            Dim i As Integer = 0
            Dim Unt = (From s In LstUnt.CheckedItems Select s.trkUnit).ToList
            While i < Unt.Count
                Dim tbRel As New prodUnit
                tbRel.trk = NewKeyRel()
                tbRel.trkProd = Val(TxtID.Text)
                tbRel.trkUnit = Unt.Item(i)
                tbRel.delPU = False
                db.prodUnits.InsertOnSubmit(tbRel)
                db.SubmitChanges()
                i = i + 1
            End While

        ElseIf MemEdit = True
            tb = (From s In db.products Where s.trkProd = Val(TxtID.Text) Select s).Single()
            tb.prodName = TxtName.Text
            tb.trkCrop = Val(LokCrop.EditValue.ToString())
            db.SubmitChanges()
            '**********************
            Dim Unt = (From s In LstUnt.CheckedItems Select s.trkUnit).ToList
            Dim i As Integer = 0
            While i < Unt.Count
                Dim tbRel As New prodUnit
                Dim TheUnt As Integer
                TheUnt = Unt.Item(i)
                tbRel = (From s In db.prodUnits Where s.trkProd = Val(TxtID.Text) And s.trkUnit = TheUnt Select s).SingleOrDefault


                If Not IsNothing(tbRel) Then

                    tbRel = (From s In db.prodUnits Where s.trkProd = Val(TxtID.Text) And s.trkUnit = TheUnt And s.delPU = True Select s).SingleOrDefault
                    If Not IsNothing(tbRel) Then
                        tbRel.delPU = 0
                        db.SubmitChanges()
                    End If
                Else
                    Dim tbRelSave As New prodUnit
                    tbRelSave.trk = NewKeyRel()
                    tbRelSave.trkProd = Val(TxtID.Text)
                    tbRelSave.trkUnit = Unt.Item(i)
                    tbRelSave.delPU = False
                    db.prodUnits.InsertOnSubmit(tbRelSave)
                    db.SubmitChanges()
                End If

                i = i + 1
            End While
            '*******************************************

            Dim lst = (From s In db.prodUnits Where s.trkProd = Val(TxtID.Text) And s.delPU = False Select s).ToList
            i = 0
            Dim j As Integer = 0
            Dim UntB = (From s In LstUnt.CheckedItems Select s.trkUnit).ToList
            While i < lst.Count
                Dim tbRel As New prodUnit
                j = 0
                While j < UntB.Count
                    If lst.Item(i).trkUnit = UntB.Item(j) Then
                        'If LstUnt.Items(j).CheckState = CheckState.Unchecked Then
                        Exit While
                    Else
                        j = j + 1

                    End If
                    If j = UntB.Count Then
                        tbRel = (From s In db.prodUnits Where s.trkProd = Val(TxtID.Text) And s.trkUnit = lst.Item(i).trkUnit And s.delPU = False Select s).SingleOrDefault
                        tbRel.delPU = True
                        db.SubmitChanges()
                    End If
                End While
                i = i + 1
            End While

        End If

        Progress()
    End Sub
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.products Select trk.trkProd).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    Function NewKeyRel() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.prodUnits Select trk.trk).ToList
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
        Dim Mylst As New LstProduct
        Mylst.ShowDialog()
        If ID <> 0 Then
            TxtID.Text = ID
            Dim tb As New V_Product
            tb = (From s In db.V_Products Where s.trkProd = Val(ID) Select s).Single
            TxtName.Text = tb.prodName
            LokCrop.EditValue = tb.trkCrop
            LokCrop.Text = tb.cropName
            '*******************************
            Dim i As Integer = 0
            Dim j As Integer = 0
            Dim lst = (From s In db.prodUnits Where s.trkProd = Val(TxtID.Text) And s.delPU = False Select s).ToList

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
End Class