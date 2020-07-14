Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraEditors
Module ModMain
#Region "Num Only"
    Public Function NumOnly(ByVal MyChar As String) As Boolean
        Dim StrNum As String
        StrNum = "0123456789."
        If Asc(MyChar) > 26 Then
            If InStr(1, StrNum, MyChar) = 0 Then
                Beep()
                NumOnly = True
            End If
        End If
    End Function
#End Region
    Public Sub Center(ByVal Frm As System.Windows.Forms.Form, Optional ByVal FormTop As Short = 0)
        Frm.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Frm.Width) / 2
        If FormTop = -1 Then
            Frm.Top = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - Frm.Height) / 2
        Else
            Frm.Top = FormTop
        End If
        SetDate(Frm)
        FrmMain.RibbonControl.Enabled = False

    End Sub
#Region "Variable"
    Public MemReq As Integer
    Public MemLoc As Integer
    Public MyFrmMain As New FrmMain
#End Region
#Region "Variables Declaration"
    Public MemAdd As Boolean
    Public MemAddDet As Boolean
    Public MemEdit As Boolean
    Public MemFind As Boolean
    Public MemLoad As Boolean = False
    Public MemInf As Boolean = False
    Public MemBtnText As String = "UnDo"
    Public db As LINQHsbDataContext = New LINQHsbDataContext()
    Public Msg As DialogResult
    Public done As Boolean
    Public saved As Boolean = False
    Public trk As Integer
    Public FrmChild As Boolean = False
    Public TheStrType As Integer
#End Region
#Region "Grd col"
    'Public EditCol As New Columns.GridColumn()
    'Public DeleteCol As New Columns.GridColumn()
#End Region

#Region "center"
    'Public Sub Center(ByVal Frm As DevExpress.XtraEditors.XtraForm, Optional ByVal FormTop As Short = 0)
    '    Frm.Left = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width - Frm.Width) / 2
    '    If FormTop = -1 Then
    '        Frm.Top = (System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height - Frm.Height) / 2
    '    Else
    '        Frm.Top = FormTop
    '    End If
    '    SetDate(Frm)
    '    MyFrmMain.RibbonControl.Enabled = False

    '    Frm.Enabled = True

    'End Sub
#End Region

#Region "ThisAppVar"
    Public ID As Integer = 0
    Public CountView As Integer = 0
    Public Name As String = ""
    Public Loaded As Boolean = False
    Public IsView As Boolean = False
#End Region
#Region "ShipPeeler"
    Public PToTrk As Integer
    Public ViewShip As Boolean = False
#End Region
    Public Sub ResetAtClose()
        '************fill store ,and save req number should be rest, b/c when re-open form 

        MemAddDet = False
        done = False
        saved = False
        MemEdit = False
        MemAdd = False
        MemInf =false
        FrmMain.RibbonControl.Enabled = True
        If IsView Then
            FrmMain.RibbonControl.Enabled = False
            done = True
            IsView = False
        End If

    End Sub

    Public Function CleanStr(ByVal str As String) As String
        While str.Contains("  ")
            str.Trim()
            str = str.Replace("  ", " ")
        End While
        CleanStr = str
    End Function

    Public UOne, AOne As Double
    Public UTwo, ATwo As Double
    Public IsDet As Boolean = True

    Public Sub SetDate(ByVal Frm As DevExpress.XtraEditors.XtraForm)
        Dim Ctrl As Control
        Dim CtrlGrp As Control

        For Each Ctrl In Frm.Controls
            If TypeOf Ctrl Is GroupBox Then
                For Each CtrlGrp In Ctrl.Controls
                    If TypeOf CtrlGrp Is DevExpress.XtraEditors.DateEdit Then
                        If Trim(CtrlGrp.Name) <> "FromDate" And Trim(CtrlGrp.Name) <> "ToDate" Then
                            CtrlGrp.Text = Today
                        End If
                    End If
                Next

            End If

        Next
    End Sub
    Public Sub CalculateUnit(ByVal MyUnt As Integer, ByVal MyAmt As Decimal, ByVal MyCrp As Integer)
        'Dim MyUnt As Integer = GVBuyDet.GetFocusedRowCellValue(" ThentrkUnit")
        'Dim MyAmt As Integer = GVBuyDet.GetFocusedRowCellValue("Amount")
        Dim TheUnt As Double
        Dim TheAmt As Double
        Dim tb As New unitExch
        Dim i As Integer = 0
        Dim lst = (From s In db.cropUnits Where s.trkCrop = MyCrp And s.delCU = False
                   Select s).ToList
        If lst.Count <> 0 Then
            If lst.Count = 2 Then
                While i < 2
                    If lst.Item(i).trkUnit <> MyUnt Then
                        TheUnt = lst.Item(i).trkUnit
                    End If
                    i = i + 1
                End While
                tb = (From s In db.unitExches Where s.trkFstUnt = TheUnt And s.trkSecUnt = MyUnt And s.delUnEx = 0).SingleOrDefault
                If Not IsNothing(tb) Then
                    TheAmt = MyAmt / tb.exchgVal
                Else
                    tb = (From s In db.unitExches Where s.trkFstUnt = MyUnt And s.trkSecUnt = TheUnt And s.delUnEx = 0).SingleOrDefault
                    If Not IsNothing(tb) Then
                        TheAmt = MyAmt * tb.exchgVal
                    End If
                End If
            Else
                TheAmt = MyAmt
                TheUnt = MyUnt
            End If
            If MyUnt < TheUnt Then
                UOne = MyUnt
                AOne = MyAmt
                UTwo = TheUnt
                ATwo = TheAmt
            ElseIf MyUnt > TheUnt
                UOne = TheUnt
                AOne = TheAmt
                UTwo = MyUnt
                ATwo = MyAmt
            Else
                UOne = TheUnt
                AOne = TheAmt
                UTwo = UOne
                ATwo = 0
            End If
        End If

    End Sub

    Public Sub CalculatePrdUnit(ByVal MyUnt As Integer, ByVal MyAmt As Decimal, ByVal MyPrd As Integer)
        'Dim MyUnt As Integer = GVBuyDet.GetFocusedRowCellValue("trkUnit")
        'Dim MyAmt As Integer = GVBuyDet.GetFocusedRowCellValue("Amount")
        Dim TheUnt As Double
        Dim TheAmt As Double
        Dim tb As New unitExch
        Dim i As Integer = 0
        Dim lst = (From s In db.prodUnits Where s.trkProd = MyPrd And s.delPU = False
                   Select s).ToList
        If lst.Count <> 0 Then
            If lst.Count = 2 Then
                While i < 2
                    If lst.Item(i).trkUnit <> MyUnt Then
                        TheUnt = lst.Item(i).trkUnit
                    End If
                    i = i + 1
                End While
                tb = (From s In db.unitExches Where s.trkFstUnt = TheUnt And s.trkSecUnt = MyUnt And s.delUnEx = 0).SingleOrDefault
                If Not IsNothing(tb) Then
                    TheAmt = MyAmt / tb.exchgVal
                Else
                    tb = (From s In db.unitExches Where s.trkFstUnt = MyUnt And s.trkSecUnt = TheUnt And s.delUnEx = 0).SingleOrDefault
                    If Not IsNothing(tb) Then
                        TheAmt = MyAmt * tb.exchgVal
                    End If
                End If
            Else
                TheAmt = MyAmt
                TheUnt = MyUnt
            End If
            If MyUnt < TheUnt Then
                UOne = MyUnt
                AOne = MyAmt
                UTwo = TheUnt
                ATwo = TheAmt
            ElseIf MyUnt > TheUnt
                UOne = TheUnt
                AOne = TheAmt
                UTwo = MyUnt
                ATwo = MyAmt
            Else
                UOne = TheUnt
                AOne = TheAmt
                UTwo = UOne
                ATwo = 0
            End If
        End If

    End Sub

    Public Function CanEditChild(ByVal ToPrd As Integer) As Boolean
        'Dim tbShp As New expShip
        'Dim tbSale As New sale
        'CanEditChild = False
        'tbShp = (From s In db.expShips Where s.trkToPrd = ToPrd And s.delExpShip = 0 Select s).SingleOrDefault
        'If Not IsNothing(tbShp) Then
        '    XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً لا يمكنك التعديل لارتباط السجل ..بسجل الشحن! ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        '    Exit Function
        'End If
        'tbSale = (From s In db.sales Where s.delSale = 0 Select s).SingleOrDefault
        'If Not IsNothing(tbSale) Then
        '    XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً لا يمكنك التعديل لارتباط السجل ..بسجل الصرف المحلي! ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        '    Exit Function
        'End If
        'CanEditChild = True
    End Function

    Public IsHeader As Boolean
    Public IsWater As Boolean

    Public Function RepHeader() As String
        IsHeader = False
        Dim tb As New headImg
        tb = (From s In db.headImgs Where s.actv = True Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            RepHeader = tb.imgExt
            IsHeader = True
        End If
    End Function
    Public Function RepWater() As String
        IsWater = False
        Dim tb As New waterImg
        tb = (From s In db.waterImgs Where s.actv = True Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            RepWater = tb.imgExt
            IsWater = True
        End If
    End Function
End Module
