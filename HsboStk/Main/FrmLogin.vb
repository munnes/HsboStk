
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraEditors
Imports System.Data.Linq
Imports DevExpress.XtraEditors.Controls
Public Class FrmLogin


    Private Sub FrmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        Dim tbChk As New user
        Dim tb As New user
        tbChk = (From s In db.users Where s.uTrk = 1 Select s).SingleOrDefault
        If IsNothing(tbChk) Then
            tb.uTrk = 1
            tb.uName = "super admin"
            tb.uId = "Admin"
            tb.uPass = "376"
            tb.uDel = False
            db.users.InsertOnSubmit(tb)
            db.SubmitChanges()

            FillBuy(1, "عملية شراء")
            FillBuy(2, "ضبط المخزن خروج")
            FillBuy(3, "ضبط المخزن دخول")
            FillBuy(4, "ضبط المخزن اتلاف بضاعة")

            FillArv(1, "تخزين في المحطة")
            FillArv(2, "ضبط المخزن خروج")
            FillArv(3, "ضبط المخزن دخول")
            FillArv(4, "ضبط المخزن اتلاف بضاعة")

            FillRcv(1, "تخزين منتجات")
            FillRcv(2, "ضبط المخزن خروج")
            FillRcv(3, "ضبط المخزن دخول")
            FillRcv(4, "ضبط المخزن اتلاف بضاعة")

            StrTy(1, "مخازن مناطق الشراء")
            StrTy(2, "مخازن المحطات للمحاصيل")
            StrTy(3, "مخازن المحطات للمنتجات")
        End If
    End Sub
    Private Sub FillBuy(ByVal id As Integer, ByVal txt As String)
        Dim tb As New processBuy
        tb.trkPrs = id
        tb.prsName = txt
        db.processBuys.InsertOnSubmit(tb)
        db.SubmitChanges()

    End Sub

    Private Sub FillArv(ByVal id As Integer, ByVal txt As String)
        Dim tb As New processArv
        tb.trkPrs = id
        tb.prsName = txt
        db.processArvs.InsertOnSubmit(tb)
        db.SubmitChanges()

    End Sub
    Private Sub FillRcv(ByVal id As Integer, ByVal txt As String)
        Dim tb As New processRcv
        tb.trkPrs = id
        tb.prsName = txt
        db.processRcvs.InsertOnSubmit(tb)
        db.SubmitChanges()

    End Sub
    Private Sub StrTy(ByVal id As Integer, ByVal txt As String)
        Dim tb As New strType
        tb.trkStrType = id
        tb.typeName = txt
        db.strTypes.InsertOnSubmit(tb)
        db.SubmitChanges()
    End Sub


    Private Sub BtnLogin_Click_1(sender As Object, e As EventArgs) Handles BtnLogin.Click
        Dim tb As New user

        tb = (From s In db.users Where s.uId = TxtId.Text And s.uPass = TxtPass.Text And s.uDel = 0 Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            ResetAtClose()
            Me.Close()
            FrmMain.RibbonControl.Enabled = True
        Else
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً كلمة المرور أو كلمة السر ليست صحيحة", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)

        End If
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
        FrmMain.Close()
    End Sub
End Class