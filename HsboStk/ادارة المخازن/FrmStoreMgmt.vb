
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils

Public Class FrmStoreMgmt
    Private Row As Integer = 0
    Private RowInd As Integer = 0
    Private SavedRow As Integer = 0
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Public repUnit As Repository.RepositoryItemLookUpEdit
    Private HOfGrd As Integer
    Private HOfGrp As Integer
    Private LocOfGrd As Point
    Private LocOfGrp As Point
    Private Sub FrmStoreMgmt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        'Dim lstPros As New List(Of String)
        'lstPros.Add("ضبط المخزن خروج")
        'lstPros.Add("ضبط المخزن دخول")
        'lstPros.Add("ضبط المخزن اتلاف")

        '**********************
        'Dim lstStrType As New List(Of String)
        'lstStrType.Add("مخازن مناطق الشراء")
        'lstStrType.Add("مخازن المحطات للمحاصيل")
        'lstStrType.Add("مخازن المحطات للمنتجات")
        'LokStrType.Properties.DataSource = lstStrType
        StoreType()
        Prs()
        FillGrid()
        FormatColumns()
        ProgressBarControl1.Visible = False
        '********************
        RdoLocal.Checked = True
        LokClient.Enabled = False
        '*******************
        HOfGrd = GridControl1.Height
        HOfGrp = GrpCrp.Height
        LocOfGrp = GrpCrp.Location
        LocOfGrd = GridControl1.Location

        GrpCrp.Visible = False
        GridControl1.Location = LocOfGrp
        GridControl1.Height = HOfGrd + HOfGrp + 10
        LokStrType.ReadOnly = False
        If TheStrType = 1 Then
            ViewDetBuy()
            Me.Refresh()
            TheStrType = 0
            LokStrType.ReadOnly = True

        ElseIf TheStrType = 2
            ViewDetArv()
            Me.Refresh()
            TheStrType = 0
            LokStrType.ReadOnly = True
        ElseIf TheStrType = 3
            ViewArvPrdDet()
            Me.Refresh()
            TheStrType = 0
            LokStrType.ReadOnly = True
        End If

    End Sub

    Private Sub StoreType()
        Dim lst = (From s In db.strTypes Select s).ToList
        LokStrType.Properties.DataSource = lst
        LokStrType.Properties.ValueMember = "trkStrType"
        LokStrType.Properties.DisplayMember = "typeName"
        LokStrType.Properties.PopulateColumns()
        LokStrType.Properties.Columns(0).Visible = False
    End Sub
    Private Sub Prs()
        Dim lst = (From s In db.processBuys Where s.trkPrs <> 1 Select s).ToList
        LokProcess.Properties.DataSource = lst
        LokProcess.Properties.ValueMember = "trkPrs"
        LokProcess.Properties.DisplayMember = "prsName"
        LokProcess.Properties.PopulateColumns()
        LokProcess.Properties.Columns(0).Visible = False
    End Sub

#Region "TheStk"
    Private Sub TheStk()
        Dim TheDate As DateTime
        If Me.DateRecv.Text <> "" Then
            TheDate = CType(DateRecv.Text, DateTime)
        End If
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = True
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = False
            Clnt = Val(LokClient.EditValue)
        End If
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            TheStkBuy(TheDate)
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            TheStkArv(TheDate, isLoc, Clnt)
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            TheStkArvPrd(TheDate, isLoc, Clnt)
        End If
    End Sub
    Private Sub TheStkBuy(ByVal TheDate As DateTime)
        Dim count As Integer = GVTheStk.RowCount
        Dim i As Integer = 0
        Dim UndPreShip As Double = 0
        Dim tbShip As New V_buyDetail
        Dim Total As Double
        Dim curCrop As Integer
        While i < count
            Dim TbUn As New V_cropUnit
            curCrop = GVTheStk.GetRowCellValue(i, "trkCrop")
            Dim j As Integer = 0
            Dim StrUn As String = Trim(GVTheStk.GetRowCellValue(i, "trkUnit"))
            '$$$$$$$$$$$$$$$$$$$$$$$$
            If StrUn <> "" And curCrop <> 0 Then
                Dim lst = (From s In db.V_cropUnits Where s.trkCrop = curCrop Select s).ToList
                If lst.Count = 2 Then
                    While j < 2
                        If lst.Item(j).unitName = StrUn Then
                            Exit While
                        End If
                        j = j + 1
                    End While
                End If
                '$$$$$$$$$$$$$$$$$$$$$$$
                If Val(GVTheStk.GetRowCellValue(i, "trkDet")) <> 0 Then
                    tbShip = (From s In db.V_buyDetails Where s.trkBuyDet = Val(GVTheStk.GetRowCellValue(i, "trkDet")) _
                                                         And s.trkCrop = curCrop And s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                         And s.trkBStore = Val(LokStore.EditValue) And s.buyDate <= TheDate
                              Select s).SingleOrDefault()
                    If Not IsNothing(tbShip) Then
                        UndPreShip = tbShip.storeAmount
                    Else
                        UndPreShip = 0
                    End If
                    GVTheStk.SelectRow(i)
                End If
                Total = CalTotal(curCrop, j) - UndPreShip
                GVTheStk.SetRowCellValue(i, "total", Total)
            End If

            If i = count - 1 Then
                If Val(GVTheStk.GetRowCellValue(count - 1, "trkDet")) = 0 Then
                    GVTheStk.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub

    Private Sub TheStkArv(ByVal TheDate As DateTime, ByVal isLoc As Boolean, ByVal Clnt As Integer)
        Dim count As Integer = GVTheStk.RowCount
        Dim i As Integer = 0
        Dim UndPreShip As Double = 0
        Dim tbOutPrd As New V_ArvStr
        Dim Total As Double
        Dim curCrop As Integer
        '****

        While i < count
            Dim TbUn As New V_cropUnit
            Dim j As Integer = 0
            Dim StrUn As String = Trim(GVTheStk.GetRowCellValue(i, "trkUnit"))

            curCrop = GVTheStk.GetRowCellValue(i, "trkCrop")
            '$$$$$$$$$$$$$$$$$$$$$$$$
            If StrUn <> "" And curCrop <> 0 Then
                Dim lst = (From s In db.V_cropUnits Where s.trkCrop = curCrop Select s).ToList
                If lst.Count = 2 Then
                    While j < 2
                        If lst.Item(j).unitName = StrUn Then
                            Exit While
                        End If
                        j = j + 1
                    End While
                End If

                If Val(GVTheStk.GetRowCellValue(i, "trkDet")) <> 0 Then


                    tbOutPrd = (From s In db.V_ArvStrs Where s.trkASDet = Val(GVTheStk.GetRowCellValue(i, "trkDet")) _
                                                          And s.trkCrop = curCrop And s.trkArival = Val(LokLoc.EditValue) _
                                                         And s.trkAStore = Val(LokStore.EditValue) And s.isLocal = isLoc _
                                                       And s.trkClntCrp = Clnt And s.SArDate <= TheDate Select s).SingleOrDefault()

                    If Not IsNothing(tbOutPrd) Then
                        UndPreShip = tbOutPrd.aStock
                    Else
                        UndPreShip = 0
                    End If
                    GVTheStk.SelectRow(i)
                End If
                Total = CalTotal(curCrop, j) - UndPreShip
                GVTheStk.SetRowCellValue(i, "total", Total)
            End If

            If i = count - 1 Then
                If Val(GVTheStk.GetRowCellValue(count - 1, "trkDet")) = 0 Then
                    GVTheStk.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub

    Private Sub TheStkArvPrd(ByVal TheDate As DateTime, ByVal isLoc As Boolean, ByVal Clnt As Integer)
        Dim count As Integer = GVTheStk.RowCount
        Dim i As Integer = 0
        Dim UndPreShip As Double = 0
        Dim tbShip As New V_RecvDet
        Dim Total As Double
        Dim curProd As Integer
        While i < count
            Dim TbUn As New V_prdUnit
            curProd = GVTheStk.GetRowCellValue(i, "trkCrop")

            Dim j As Integer = 0
            Dim StrUn As String = Trim(GVTheStk.GetRowCellValue(i, "trkUnit"))
            '$$$$$$$$$$$$$$$$$$$$$$$$
            If StrUn <> "" And curProd <> 0 Then
                Dim lst = (From s In db.V_prdUnits Where s.trkProd = curProd Select s).ToList
                If lst.Count = 2 Then
                    While j < 2
                        If lst.Item(j).unitName = StrUn Then
                            Exit While
                        End If
                        j = j + 1
                    End While
                End If

                '$$$$$$$$$$$$$$$$$$$$$$$

                If Val(GVTheStk.GetRowCellValue(i, "trkDet")) <> 0 Then
                    tbShip = (From s In db.V_RecvDets Where s.trkRecvDet = Val(GVTheStk.GetRowCellValue(i, "trkDet")) _
                                 And s.trkProd = curProd And s.trkArival = Val(LokLoc.EditValue) _
                                                         And s.trkAPrdStr = Val(LokStore.EditValue) And s.isLocal = isLoc _
                                                       And s.trkClntCrp = Clnt And s.recvDate <= TheDate Select s).SingleOrDefault()
                    If Not IsNothing(tbShip) Then
                        UndPreShip = tbShip.amount
                    Else
                        UndPreShip = 0
                    End If
                    GVTheStk.SelectRow(i)
                End If
                Total = CalTotal(curProd, j) - UndPreShip
                GVTheStk.SetRowCellValue(i, "total", Total)
            End If

            If i = count - 1 Then
                If Val(GVTheStk.GetRowCellValue(count - 1, "trkDet")) = 0 Then
                    GVTheStk.UnselectRow((count - 1))
                End If
            End If
            i = i + 1
        End While
    End Sub


#End Region

#Region "views"
    Private Sub ViewDetBuy()

        Dim i As Integer = 0
        Dim tb = (From s In db.V_buys Where s.trkBuyClient = ID And s.delBuy = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        LokStrType.Text = "مخازن مناطق الشراء"
        Me.LokLoc.EditValue = tb.trkBuyLoc
        Me.LokStore.EditValue = tb.trkBStore
        'LokProcess.Text = tb.prsName
        LokProcess.EditValue = tb.trkPrs
        Me.DateRecv.Text = tb.buyDate
        buyInf.Text = tb.buyInfo

        Dim TbUn As New V_cropUnit
        '*******************************
        Dim lst = (From s In db.buyDetails Where s.trkBuyClient = ID And s.delBDet = 0 Select s).ToList
        While i < lst.Count
            '******************Total
            TbUn = (From s In db.V_cropUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkCrop = lst.Item(i).trkCrop Select s).SingleOrDefault
            '$$$$$$$$$$$$$$$$ Calculate Index
            Dim j As Integer = 0
            Dim lstInd = (From s In db.V_cropUnits Where s.trkCrop = lst.Item(i).trkCrop Select s).ToList
            If lstInd.Count = 2 Then
                While j < 2
                    If lstInd.Item(j).trkUnit = lst.Item(i).trkUnit Then
                        Exit While
                    End If
                    j = j + 1
                End While
            End If
            '$$$$$$$$$$$$$

            Dim CurTotal As Double = CalTotal(lst.Item(i).trkCrop, j) - lst.Item(i).storeAmount
            '**********************
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVTheStk.AddNewRow()
            FillCrop()
            GVTheStk.SetFocusedRowCellValue("trkDet", lst.Item(i).trkBuyDet)
            GVTheStk.SetFocusedRowCellValue("trkCrop", lst.Item(i).trkCrop)
            GVTheStk.SetFocusedRowCellValue("total", CurTotal)
            GVTheStk.SetFocusedRowCellValue("Amount", lst.Item(i).storeAmount)
            GVTheStk.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVTheStk.SetFocusedRowCellValue("Weight", lst.Item(i).weight)

            GVTheStk.UpdateCurrentRow()
            i = i + 1

        End While
        trk = ID
        i = 0
        CountView = 0
        ID = 0
        saved = True
        LokStrType.ReadOnly = True
    End Sub
    Private Sub ViewDetArv()
        Dim i As Integer = 0
        Dim tb = (From s In db.V_aStoreReqs Where s.trkASReq = ID And s.delAS = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        LokStrType.Text = "مخازن المحطات للمحاصيل"
        Me.LokLoc.EditValue = tb.trkArival
        Me.LokStore.EditValue = tb.trkAStore
        Me.LokProcess.EditValue = tb.trkPrs
        Dim CurTotal As Double = 0
        If tb.isLocal = False Then
            RdoClient.Checked = True
            LokClient.EditValue = tb.trkClntCrp
            LokClient.Enabled = True
            RdoLocal.Enabled = False
            RdoClient.Enabled = True
        Else
            RdoLocal.Checked = True
            LokClient.Enabled = False
            RdoClient.Enabled = False
        End If

        Me.DateRecv.Text = tb.SArDate
        buyInf.Text = tb.aStrDet
        Dim TbUn As New V_cropUnit
        '*******************************
        Dim lst = (From s In db.aStoreDets Where s.trkASReq = ID And s.delASDet = 0 Select s).ToList
        While i < lst.Count
            '******************Total
            TbUn = (From s In db.V_cropUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkCrop = lst.Item(i).trkCrop Select s).SingleOrDefault
            '$$$$$$$$$$$$$$$$ Calculate Index
            Dim j As Integer = 0
            Dim lstInd = (From s In db.V_cropUnits Where s.trkCrop = lst.Item(i).trkCrop Select s).ToList
            If lstInd.Count = 2 Then
                While j < 2
                    If lstInd.Item(j).trkUnit = lst.Item(i).trkUnit Then
                        Exit While
                    End If
                    j = j + 1
                End While
            End If
            '$$$$$$$$$$$$$$$$$$$$$$
            CurTotal = CalTotal(lst.Item(i).trkCrop, j) - lst.Item(i).aStock
            '**********************
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVTheStk.AddNewRow()
            TbUn = (From s In db.V_cropUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkCrop = lst.Item(i).trkCrop Select s).SingleOrDefault
            GVTheStk.SetFocusedRowCellValue("trkDet", lst.Item(i).trkASDet)
            GVTheStk.SetFocusedRowCellValue("trkCrop", lst.Item(i).trkCrop)
            GVTheStk.SetFocusedRowCellValue("total", CurTotal)
            GVTheStk.SetFocusedRowCellValue("Amount", lst.Item(i).aStock)
            GVTheStk.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVTheStk.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVTheStk.UpdateCurrentRow()
            i = i + 1
        End While
        trk = ID
        i = 0
        CountView = 0
        ID = 0
        saved = True
    End Sub

    Private Sub ViewArvPrdDet()
        Dim i As Integer = 0
        Dim tb = (From s In db.V_Receives Where s.trkRecv = ID And s.delRecv = 0 Select s).SingleOrDefault()
        Me.TxtRId.Text = ID
        LokStrType.Text = "مخازن المحطات للمنتجات"
        LokLoc.EditValue = tb.trkArival
        LokProcess.EditValue = tb.trkPrs
        Me.DateRecv.Text = tb.recvDate
        buyInf.Text = tb.recvInf

        Me.LokStore.EditValue = tb.trkAPrdStr
        'If tb.isLocal = False Then
        '    RdoClient.Checked = True
        '    LokClient.EditValue = tb.trkClntCrp
        'Else
        '    RdoLocal.Checked = True
        '    LokClient.Text = ""
        'End If

        If tb.isLocal = False Then
            RdoClient.Checked = True
            LokClient.EditValue = tb.trkClntCrp
            LokClient.Enabled = True
            RdoLocal.Enabled = False
            RdoClient.Enabled = True
        Else
            RdoLocal.Checked = True
            LokClient.Enabled = False
            RdoClient.Enabled = False
        End If
        Dim TbUn As New V_prdUnit
        '*******************************
        Dim lst = (From s In db.V_RecvDets Where s.trkRecv = ID Select s).ToList
        While i < lst.Count
            Row = Row + 1
            RowInd = Row - 1
            SavedRow = Row
            MemEdit = False
            GVTheStk.AddNewRow()
            '******************Total
            TbUn = (From s In db.V_prdUnits Where s.trkUnit = lst.Item(i).trkUnit _
                                                    And s.trkProd = lst.Item(i).trkProd Select s).SingleOrDefault

            '$$$$$$$$$$$$$$$$ Calculate Index
            Dim j As Integer = 0
            Dim lstInd = (From s In db.V_prdUnits Where s.trkProd = lst.Item(i).trkProd Select s).ToList
            If lstInd.Count = 2 Then
                While j < 2
                    If lstInd.Item(j).trkUnit = lst.Item(i).trkUnit Then
                        Exit While
                    End If
                    j = j + 1
                End While
            End If
            '$$$$$$$$$$$$$

            Dim CurTotal As Double = CalTotal(lst.Item(i).trkProd, j) - lst.Item(i).amount
            '**********************
            GVTheStk.SetFocusedRowCellValue("trkDet", lst.Item(i).trkRecvDet)
            GVTheStk.SetFocusedRowCellValue("trkCrop", lst.Item(i).trkProd)
            GVTheStk.SetFocusedRowCellValue("total", CurTotal)

            GVTheStk.SetFocusedRowCellValue("Amount", lst.Item(i).amount)
            GVTheStk.SetFocusedRowCellValue("trkUnit", TbUn.unitName)
            GVTheStk.SetFocusedRowCellValue("Weight", lst.Item(i).weight)
            GVTheStk.UpdateCurrentRow()
            i = i + 1
        End While
        trk = ID
        i = 0
        CountView = 0
        ID = 0
        saved = True
    End Sub


#End Region
    Public Sub FillGrid()

        GridControl1.RepositoryItems.Add(repTxt)
        '***************** should be added her to avoid disappear when focus changed
        'GridControl1.RepositoryItems.Add(repCrop)
        Dim list As BindingList(Of stkDetail) = New BindingList(Of stkDetail)

        GridControl1.DataSource = list
        GVTheStk.Columns(0).ColumnEdit = repTxt
        GVTheStk.Columns(0).Visible = False
        GVTheStk.Columns(1).ColumnEdit = repCrop
        GVTheStk.Columns(2).ColumnEdit = repTxt
        GVTheStk.Columns(3).ColumnEdit = repTxt
        '  GVShipDet.Columns(4).ColumnEdit = repUnit
        GVTheStk.Columns(5).ColumnEdit = repTxt
        GVTheStk.OptionsSelection.MultiSelect = True
        GVTheStk.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect

    End Sub
    Private Sub FormatColumns()
        col1 = GVTheStk.Columns(1)
        col2 = GVTheStk.Columns(2)
        col3 = GVTheStk.Columns(3)
        col4 = GVTheStk.Columns(4)
        col5 = GVTheStk.Columns(5)

        '****************
        col1.Caption = "المحصول"
        col2.Caption = "المخزون"
        col3.Caption = "الكمية"
        col4.Caption = "الوحدة"
        col5.Caption = "الوزن"
        GVTheStk.Columns(2).OptionsColumn.ReadOnly = True
        GVTheStk.Columns(2).AppearanceCell.BackColor = Color.DarkRed
        GVTheStk.Columns(2).AppearanceCell.ForeColor = Color.White
        GVTheStk.Columns(2).AppearanceCell.FontSizeDelta = Font.Bold
    End Sub
    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged

        If done = True And LokLoc.Text <> "" Then
            If LokStrType.Text = "مخازن مناطق الشراء" Then
                FillBuyStore()
            ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
                FillArriveCrpStore()
                FillClient()
            ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
                FillArrivePrdStore()
                FillClient()
            End If
        End If
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub LokStrType_TextChanged(sender As Object, e As EventArgs) Handles LokStrType.TextChanged

        done = False
        ClearForm()
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            FillBuyLoc()
            '******************
            GrpCrp.Visible = False
            GridControl1.Location = LocOfGrp
            GridControl1.Height = HOfGrd + HOfGrp + 10
            col1.Caption = "المحصول"
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            FillArivalLoc()
            GrpCrp.Visible = True
            GridControl1.Location = LocOfGrd
            GridControl1.Height = HOfGrd
            col1.Caption = "المحصول"
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            FillArivalLoc()
            GrpCrp.Visible = True
            GridControl1.Location = LocOfGrd
            GridControl1.Height = HOfGrd
            col1.Caption = "المنتج"
        End If
        FillCrop()

        done = True
    End Sub
    Private Sub FillBuyLoc()
        Dim lst = (From s In db.buyerLocations Where s.delL = 0 Select s).ToList
        LokLoc.Properties.DataSource = lst
        LokLoc.Properties.ValueMember = "trkBuyLoc"
        LokLoc.Properties.DisplayMember = "buyLoc"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
    End Sub
    Private Sub FillArivalLoc()
        Dim lst = (From c In db.arivalLocs Where c.delAr = False Select c).ToList
        Me.LokLoc.Properties.DataSource = lst
        LokLoc.Properties.DisplayMember = "arivalName"
        LokLoc.Properties.ValueMember = "trkArival"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
    End Sub

    Private Sub FillBuyStore()
        Dim lst = (From c In db.buyerStores Where c.delSL = False And c.trkBuyLoc = Val(LokLoc.EditValue.ToString()) Select c).ToList
        LokStore.Properties.DataSource = lst
        LokStore.Properties.DisplayMember = "bStore"
        LokStore.Properties.ValueMember = "trkBStore"
        LokStore.Properties.PopulateColumns()
        LokStore.Properties.Columns(0).Visible = False
        LokStore.Properties.Columns(2).Visible = False
        LokStore.Properties.Columns(3).Visible = False
    End Sub

    Private Sub FillArriveCrpStore()
        Dim lst = (From c In db.arivalStores Where c.delSa = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
        LokStore.Properties.DataSource = lst
        LokStore.Properties.DisplayMember = "AStore"
        LokStore.Properties.ValueMember = "trkAStore"
        LokStore.Properties.PopulateColumns()
        LokStore.Properties.Columns(0).Visible = False
        LokStore.Properties.Columns(2).Visible = False
        LokStore.Properties.Columns(3).Visible = False
    End Sub

    Private Sub FillArrivePrdStore()
        Dim lst = (From c In db.arivalPrdStores Where c.delAPrd = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
        LokStore.Properties.DataSource = lst
        LokStore.Properties.DisplayMember = "APrdStr"
        LokStore.Properties.ValueMember = "trkAPrdStr"
        LokStore.Properties.PopulateColumns()
        LokStore.Properties.Columns(0).Visible = False
        LokStore.Properties.Columns(2).Visible = False
        LokStore.Properties.Columns(3).Visible = False
    End Sub

    Private Sub ClearForm()
        'repCrop.Columns.Clear()
        LokLoc.Properties.Columns.Clear()
        LokLoc.Properties.NullText = ""
        LokStore.Properties.Columns.Clear()
        LokStore.Properties.NullText = ""

        LokLoc.Text = ""
        LokStore.Text = ""
    End Sub
    Public Sub FillCrop()
        repCrop.Columns.Clear()
        If LokStrType.Text = "مخازن مناطق الشراء" Or LokStrType.Text = "مخازن المحطات للمحاصيل" Then
            Dim crp = (From s In db.crops Where s.delCrop = False
                       Select s).ToList
            repCrop.DataSource = crp
            repCrop.ValueMember = "trkCrop"
            repCrop.DisplayMember = "cropName"
            repCrop.ShowHeader = False
            repCrop.PopulateColumns()
            repCrop.Columns(0).Visible = False
            repCrop.Columns(2).Visible = False
            repCrop.NullText = ""
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            Dim prd = (From s In db.products Where s.delPrd = False
                       Select s).ToList
            repCrop.DataSource = prd
            repCrop.ValueMember = "trkProd"
            repCrop.DisplayMember = "prodName"
            repCrop.ShowHeader = False
            repCrop.PopulateColumns()
            repCrop.Columns(0).Visible = False
            repCrop.Columns(2).Visible = False
            repCrop.Columns(3).Visible = False
            repCrop.NullText = ""
        End If
    End Sub

    Public Function SaveInclose() As Boolean
        If Row <> 0 Then
            If Val(GVTheStk.GetRowCellValue(GVTheStk.RowCount - 1, "trkDet")) = 0 Then
                'If tb.trkBuyDet = 0 Then
                Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ السطر الأخير من التفاصيل؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
                If Msg = System.Windows.Forms.DialogResult.Yes Then
                    SaveInclose = True
                Else
                    SaveInclose = False
                    Dim lastRow As Integer = GVTheStk.RowCount - 1
                    GVTheStk.FocusedRowHandle = lastRow
                    GVTheStk.DeleteRow(lastRow)
                    SavedRow = Row - 1
                    Row = SavedRow
                    MemAddDet = False
                    IsDet = False
                End If
            Else
                SaveInclose = False
            End If
        End If
        If MemEdit = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حفظ التعديلات الأخيرة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                SaveInclose = True
            End If
        End If
    End Function
    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        If GVTheStk.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لم يتم حفظ السجلات المعدلة  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        Else
            If SaveInclose() = False Then
                ResetAtClose()
                Me.Close()
            Else
                BtnSave_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub repTxt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles repTxt.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub
    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click

        GVTheStk.OptionsFind.FindFilterColumns = ""
        GVTheStk.OptionsFind.AlwaysVisible = False

        If RowInd = 0 Then
            If CanSaveReq() = False Then
                Exit Sub
            Else
                If Not MemAdd Then
                    MemAdd = True
                End If
            End If
        End If
        If Row = SavedRow Then
            If Not MemAddDet Then
                MemAddDet = True
                Row = Row + 1
                RowInd = Row - 1
                GVTheStk.AddNewRow()
            End If
        End If
    End Sub
    Private Function CanSaveReq() As Boolean
        CanSaveReq = False
        If DateRecv.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال التاريخ ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
            DateRecv.Focus()
            Exit Function
        End If
        If CType(DateRecv.Text, DateTime) > Today Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            DateRecv.Focus()
            Exit Function
        End If
        If LokStrType.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء تحديد نوع المخزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokStrType.Focus()
            LokStrType.SelectAll()
            Exit Function
        End If
        If LokProcess.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الرجاء تحديد نوع العملية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokProcess.Focus()
            LokProcess.SelectAll()
            Exit Function
        End If
        If LokLoc.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المنطقة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokLoc.Focus()
            LokLoc.SelectAll()
            Exit Function
        End If
        If LokStore.Text = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المخزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            LokStore.Focus()
            LokStore.SelectAll()
            Exit Function
        End If
        CanSaveReq = True
    End Function

    Private Sub GVTheStk_CustomRowCellEditForEditing(sender As Object, e As CustomRowCellEditEventArgs) Handles GVTheStk.CustomRowCellEditForEditing
        Dim crp As Integer
        If e.Column.Caption = "المحصول" Or e.Column.Caption = "المنتج" Then
            GVTheStk.SetFocusedRowCellValue("trkUnit", "")
        End If

        If e.Column.Caption = "الوحدة" Then
            crp = GVTheStk.GetRowCellValue(e.RowHandle, "trkCrop")
            If crp <> 0 Then
                FillUnit(crp)
                e.RepositoryItem = repUnit

            End If
        End If
    End Sub
    Public Sub FillUnit(ByVal crp As Integer)
        If LokStrType.Text = "مخازن مناطق الشراء" Or LokStrType.Text = "مخازن المحطات للمحاصيل" Then

            Dim un = (From s In db.V_cropUnits Where s.delCU = False And s.trkCrop = crp
                      Select s Order By s.trkUnit).ToList
            repUnit = New Repository.RepositoryItemLookUpEdit

            repUnit.NullText = ""
            repUnit.DataSource = un
            repUnit.DisplayMember = "unitName"
            repUnit.ValueMember = "unitName"

            repUnit.DisplayFormat.ToString()
            repUnit.ShowHeader = False
            repUnit.PopulateColumns()

            repUnit.Columns(0).Visible = False
            repUnit.Columns(1).Visible = False
            repUnit.Columns(2).Visible = False
            repUnit.Columns(4).Visible = False
            repUnit.Columns(5).Visible = False
            repUnit.Columns(6).Visible = False
            repUnit.Columns(7).Visible = False
            repUnit.NullText = ""
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            Dim un = (From s In db.V_prdUnits Where s.delPU = False And s.trkProd = crp
                      Select s).ToList
            repUnit = New Repository.RepositoryItemLookUpEdit
            repUnit.DataSource = un
            repUnit.DisplayMember = "unitName"
            repUnit.ValueMember = "unitName"

            repUnit.DisplayFormat.ToString()
            repUnit.ShowHeader = False
            repUnit.PopulateColumns()
            repUnit.Columns(0).Visible = False
            repUnit.Columns(1).Visible = False
            repUnit.Columns(2).Visible = False
            repUnit.Columns(3).Visible = False
            repUnit.Columns(4).Visible = False
            repUnit.Columns(6).Visible = False
            repUnit.Columns(7).Visible = False
            repUnit.Columns(8).Visible = False
            repUnit.NullText = ""
        End If
    End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        GVTheStk.OptionsFind.AlwaysVisible = False
        If CanSaveReq() = True Then
            If Trim(TxtRId.Text) = "" Then
                MemAdd = True
            End If
            If MemAdd Or MemEdit Then
                SaveReqData()
                LokStrType.ReadOnly = True
                MemEdit = False
                MemAdd = False
            End If
            If IsDet = False Then
                IsDet = True
                Exit Sub
            End If
        End If

        If Row > 0 Then
            If MemAddDet Then
                If CanSave(RowInd) = True Then
                    SavedRow = Row
                    SaveData()
                    MemAddDet = False
                End If
            End If
            SaveEdit()
        End If
    End Sub

    '**************************
    Private Sub Progress()
        ProgressBarControl1.Visible = True
        Me.ProgressBarControl1.EditValue = 0
        Me.Timer1.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub

    '**************************
    Public Function CalFlag() As Boolean
        If RdoLocal.Checked = True Then
            CalFlag = True
        ElseIf RdoClient.Checked = True
            CalFlag = False
        End If
    End Function
#Region "SaveReq"
    Private Sub SaveReqData()
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            SaveReqBuy()
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            SaveReqArv()
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            SaveReqArvPrd()
        End If
    End Sub
    '*********************************** 1
    Private Sub SaveReqBuy()
        Dim tb As New buy
        Dim theDate As DateTime
        theDate = CType(DateRecv.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewBuyKey()
            tb.trkBuyClient = trk
            tb.trkBuyLoc = Val(LokLoc.EditValue.ToString())
            tb.buyDate = theDate.ToShortDateString
            tb.trkBStore = Val(LokStore.EditValue.ToString())
            tb.buyInfo = buyInf.Text
            tb.trkPrs = Val(LokProcess.EditValue)

            'If LokProcess.Text = "ضبط المخزن خروج" Then
            '    tb.trkPrs = 2
            'ElseIf LokProcess.Text = "ضبط المخزن دخول" Then
            '    tb.trkPrs = 3
            'ElseIf LokProcess.EditValue = "ضبط المخزن اتلاف"
            '    tb.trkPrs = 4
            'End If

            tb.delBuy = False
            db.buys.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.buys Where s.trkBuyClient = Val(TxtRId.Text) And s.delBuy = False Select s).Single()
            tb.trkBuyLoc = Val(LokLoc.EditValue.ToString())
            tb.buyDate = theDate.ToShortDateString
            tb.trkBStore = Val(LokStore.EditValue.ToString())
            tb.trkPrs = Val(LokProcess.EditValue)
            'If LokProcess.Text = "ضبط المخزن خروج" Then
            '    tb.trkPrs = 2
            'ElseIf LokProcess.Text = "ضبط المخزن دخول" Then
            '    tb.trkPrs = 3
            'ElseIf LokProcess.EditValue = "ضبط المخزن اتلاف"
            '    tb.trkPrs = 4
            'End If
            tb.delBuy = False
            tb.buyInfo = buyInf.Text
            trk = Val(TxtRId.Text)
            db.SubmitChanges()
            Progress()
        End If
        TxtRId.Text = trk
    End Sub
    '************************************** 2
    Private Sub SaveReqArv()
        Dim tb As New aStoreReq
        Dim theDate As DateTime
        theDate = CType(DateRecv.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewArvKey()
            tb.trkASReq = trk
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.SArDate = theDate.ToShortDateString
            tb.trkAStore = Val(LokStore.EditValue.ToString())
            tb.isLocal = CalFlag()
            If RdoClient.Checked = True Then
                tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            Else
                tb.trkClntCrp = 0
            End If
            tb.trkPrs = Val(LokProcess.EditValue)
            'If LokProcess.Text = "ضبط المخزن خروج" Then
            '    tb.trkPrs = 2
            'ElseIf LokProcess.Text = "ضبط المخزن دخول" Then
            '    tb.trkPrs = 3
            'ElseIf LokProcess.EditValue = "ضبط المخزن اتلاف"
            '    tb.trkPrs = 4
            'End If
            tb.aStrDet = buyInf.Text
            tb.delAS = False
            db.aStoreReqs.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.aStoreReqs Where s.trkASReq = Val(TxtRId.Text) And s.delAS = False Select s).Single()
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.SArDate = theDate.ToShortDateString
            tb.trkAStore = Val(LokStore.EditValue.ToString())
            tb.aStrDet = buyInf.Text
            tb.trkPrs = Val(LokProcess.EditValue)
            'If LokProcess.Text = "ضبط المخزن خروج" Then
            '    tb.trkPrs = 2
            'ElseIf LokProcess.Text = "ضبط المخزن دخول" Then
            '    tb.trkPrs = 3
            'ElseIf LokProcess.EditValue = "ضبط المخزن اتلاف"
            '    tb.trkPrs = 4
            'End If
            tb.isLocal = CalFlag()
            If RdoClient.Checked = True Then
                tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            Else
                tb.trkClntCrp = 0
            End If
            tb.delAS = False
            db.SubmitChanges()
            Progress()
        End If
        TxtRId.Text = trk
    End Sub
    '**************************3
    Private Sub SaveReqArvPrd()
        Dim tb As New receive
        Dim theDate As DateTime
        theDate = CType(DateRecv.Text, DateTime)
        If MemAdd = True And saved = False Then
            trk = NewArvPrdKey()
            tb.trkRecv = trk
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.recvDate = theDate.ToShortDateString
            tb.trkAPrdStr = Val(LokStore.EditValue.ToString())
            tb.trkPrs = Val(LokProcess.EditValue)
            'If LokProcess.Text = "ضبط المخزن خروج" Then
            '    tb.trkPrs = 2
            'ElseIf LokProcess.Text = "ضبط المخزن دخول" Then
            '    tb.trkPrs = 3
            'ElseIf LokProcess.EditValue = "ضبط المخزن اتلاف"
            '    tb.trkPrs = 4
            'End If
            tb.isLocal = CalFlag()
            If RdoClient.Checked = True Then
                tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            Else
                tb.trkClntCrp = 0
            End If
            tb.recvInf = buyInf.Text
            tb.delRecv = False
            db.receives.InsertOnSubmit(tb)
            saved = True
            db.SubmitChanges()
            Progress()
        End If

        If MemEdit = True Then
            tb = (From s In db.receives Where s.trkRecv = Val(TxtRId.Text) And s.delRecv = False Select s).Single()
            tb.trkArival = Val(LokLoc.EditValue.ToString())
            tb.recvDate = theDate.ToShortDateString
            tb.trkAPrdStr = Val(LokStore.EditValue.ToString())
            tb.trkPrs = Val(LokProcess.EditValue)
            'If LokProcess.Text = "ضبط المخزن خروج" Then
            '    tb.trkPrs = 2
            'ElseIf LokProcess.Text = "ضبط المخزن دخول" Then
            '    tb.trkPrs = 3
            'ElseIf LokProcess.EditValue = "ضبط المخزن اتلاف"
            '    tb.trkPrs = 4
            'End If
            tb.isLocal = CalFlag()
            If RdoClient.Checked = True Then
                tb.trkClntCrp = Val(LokClient.EditValue.ToString())
            Else
                tb.trkClntCrp = 0
            End If
            tb.recvInf = buyInf.Text
            tb.delRecv = False
            db.SubmitChanges()
            Progress()
        End If

        TxtRId.Text = trk
    End Sub
    '*****************************4
#End Region
#Region "SaveDetail"
    Private Function CanSave(ByVal Ind As Integer) As Boolean
        CanSave = False
        If Val(GVTheStk.GetRowCellValue(Ind, "trkCrop")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال اسم المحصول\المنتج ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVTheStk.SelectCell(RowInd, col1)
            Exit Function
        End If

        If Val(GVTheStk.GetRowCellValue(Ind, "Amount")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الكمية ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVTheStk.SelectCell(Ind, col3)
            Exit Function
        End If
        If Val(LokProcess.EditValue) = 2 Or Val(LokProcess.EditValue) = 4 Then
            If Val(GVTheStk.GetRowCellValue(Ind, "Amount")) > Val(GVTheStk.GetRowCellValue(Ind, "total")) Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "عفواً الكمية أكثر من المخزون ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                GVTheStk.SelectCell(Ind, col3)
                Exit Function
            End If
        End If
        If (GVTheStk.GetRowCellValue(Ind, "trkUnit")) = "" Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوحدة ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVTheStk.SelectCell(Ind, col5)
            Exit Function
        End If
        If Val(GVTheStk.GetRowCellValue(Ind, "Weight")) = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء إدخال الوزن ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            GVTheStk.SelectCell(Ind, col4)
            Exit Function
        End If

        If IsSingleRow(Ind) = False Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, "  عفواً لقد قمت بادخال المحصول\المنتج مسبقا  يمكنك التعديل ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Function
        End If
        CanSave = True
    End Function
    Private Function IsSingleRow(ByVal Ind As Integer) As Boolean
        Dim count As Integer = Me.GVTheStk.RowCount
        'Dim MyRow As Integer = count - 1
        Dim i As Integer = 0
        Dim Crop As Integer = Val(GVTheStk.GetRowCellValue(Ind, "trkCrop"))
        IsSingleRow = True

        While i < count
            If i <> Ind Then
                If Val(GVTheStk.GetRowCellValue(i, "trkCrop")) = Crop Then
                    IsSingleRow = False
                    Exit While
                End If
            End If
            i = i + 1
        End While
        Return IsSingleRow
    End Function
    Private Sub SaveData()
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            SaveBuyData()
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            SaveArvData()
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            SaveArvPrdData()
        End If
    End Sub
    Public Sub SaveEdit()
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            SaveBuyEdit()
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            SaveArvEdit()
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            SaveArvPrdEdit()
        End If
    End Sub
    Private Sub SaveBuyData()

        Dim tb As New buyDetail
        Dim TbUn As New V_cropUnit
        Dim TheAmt As Double
        If MemAddDet = True Then
            GVTheStk.SetRowCellValue(RowInd, "trkDet", NewKeyBuyDet())
            tb.trkBuyDet = Val(GVTheStk.GetRowCellValue(RowInd, "trkDet"))
            tb.trkBuyClient = Val(TxtRId.Text)
            TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVTheStk.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkCrop = Val(GVTheStk.GetRowCellValue(RowInd, "trkCrop")) Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVTheStk.GetRowCellValue(RowInd, "Weight"))
            tb.delBDet = False

            tb.trkCrop = Val(GVTheStk.GetRowCellValue(RowInd, "trkCrop"))
            TheAmt = Val(GVTheStk.GetRowCellValue(RowInd, "Amount"))
            If TheAmt < 0 Then
                GVTheStk.SetRowCellValue(RowInd, "Amount", -1 * TheAmt)
            End If
            '***************************************
            CalculateUnit(TbUn.trkUnit, Val(GVTheStk.GetRowCellValue(RowInd, "Amount")),
                    Val(GVTheStk.GetRowCellValue(RowInd, "trkCrop")))
            '***************************************

            If Val(LokProcess.EditValue) = 2 Or Val(LokProcess.EditValue) = 4 Then
                tb.storeAmount = Val(GVTheStk.GetRowCellValue(RowInd, "Amount")) * -1
                tb.untOne = UOne
                tb.amtOne = AOne * -1
                tb.untTwo = UTwo
                tb.amtTwo = ATwo * -1
            ElseIf Val(LokProcess.EditValue) = 3 Then
                tb.storeAmount = Val(GVTheStk.GetRowCellValue(RowInd, "Amount"))
                tb.untOne = UOne
                tb.amtOne = AOne
                tb.untTwo = UTwo
                tb.amtTwo = ATwo
            End If
            db.buyDetails.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()

        End If
    End Sub
    '************************
    Public Sub SaveBuyEdit()
        Dim i As Integer = 0
        Dim tb As New buyDetail
        Dim TheCrp As Integer
        Dim TheTrk As Integer
        Dim TbUn As New V_cropUnit
        Dim TheAmt As Double
        If GVTheStk.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVTheStk.RowCount

                If (GVTheStk.IsRowSelected(i) = True) Then
                    TheCrp = Val(GVTheStk.GetRowCellValue(i, "trkCrop"))
                    TheTrk = Val(GVTheStk.GetRowCellValue(i, "trkDet"))
                    'If CheckEditDel(TheCrp) = True Then
                    If CanSave(i) = True Then
                        TheAmt = Val(GVTheStk.GetRowCellValue(i, "Amount"))
                        If TheAmt < 0 Then
                            GVTheStk.SetRowCellValue(i, "Amount", -1 * TheAmt)
                        End If
                        tb = (From s In db.buyDetails Where s.trkBuyDet = Val(TheTrk) And s.trkBuyClient = Val(TxtRId.Text) Select s).Single()
                        tb.trkBuyClient = Val(TxtRId.Text)
                        TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVTheStk.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkCrop = Val(GVTheStk.GetRowCellValue(i, "trkCrop")) Select s).SingleOrDefault
                        tb.trkUnit = TbUn.trkUnit
                        tb.weight = Val(GVTheStk.GetRowCellValue(i, "Weight"))
                        tb.delBDet = False
                        tb.trkCrop = Val(GVTheStk.GetRowCellValue(i, "trkCrop"))
                        CalculateUnit(TbUn.trkUnit, Val(GVTheStk.GetRowCellValue(i, "Amount")),
                            Val(GVTheStk.GetRowCellValue(i, "trkCrop")))

                        If Val(LokProcess.EditValue) = 2 Or Val(LokProcess.EditValue) = 4 Then
                            tb.storeAmount = Val(GVTheStk.GetRowCellValue(RowInd, "Amount")) * -1
                            tb.untOne = UOne
                            tb.amtOne = AOne * -1
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo * -1
                        ElseIf Val(LokProcess.EditValue) = 3 Then
                            tb.storeAmount = Val(GVTheStk.GetRowCellValue(RowInd, "Amount"))
                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                        End If
                        GVTheStk.UnselectRow(i)
                    End If
                    'End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVTheStk.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub
    '********************
    Private Sub SaveArvData()
        Dim tb As New aStoreDet
        Dim TbUn As New V_cropUnit
        If MemAddDet = True Then
            GVTheStk.SetRowCellValue(RowInd, "trkDet", NewKeyArvDet())
            tb.trkASDet = Val(GVTheStk.GetRowCellValue(RowInd, "trkDet"))
            tb.trkASReq = Val(TxtRId.Text)
            TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVTheStk.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkCrop = Val(GVTheStk.GetRowCellValue(RowInd, "trkCrop")) Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVTheStk.GetRowCellValue(RowInd, "Weight"))
            tb.delASDet = False
            tb.trkCrop = Val(GVTheStk.GetRowCellValue(RowInd, "trkCrop"))
            tb.aStock = Val(GVTheStk.GetRowCellValue(RowInd, "Amount"))
            '***************************************
            Dim TheAmt As Double
            If TheAmt < 0 Then
                GVTheStk.SetRowCellValue(RowInd, "Amount", -1 * TheAmt)
            End If
            CalculateUnit(TbUn.trkUnit, Val(GVTheStk.GetRowCellValue(RowInd, "Amount")),
                    Val(GVTheStk.GetRowCellValue(RowInd, "trkCrop")))
            '***************************************
            If Val(LokProcess.EditValue) = 2 Or Val(LokProcess.EditValue) = 4 Then
                tb.aStock = Val(GVTheStk.GetRowCellValue(RowInd, "Amount")) * -1
                tb.untOne = UOne
                tb.amtOne = AOne * -1
                tb.untTwo = UTwo
                tb.amtTwo = ATwo * -1
            ElseIf Val(LokProcess.EditValue) = 3 Then
                tb.aStock = Val(GVTheStk.GetRowCellValue(RowInd, "Amount"))
                tb.untOne = UOne
                tb.amtOne = AOne
                tb.untTwo = UTwo
                tb.amtTwo = ATwo
            End If
            db.aStoreDets.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveArvEdit()
        Dim i As Integer = 0
        Dim tb As New aStoreDet
        Dim TbUn As New V_cropUnit
        Dim TheCrp As Integer
        Dim TheAmt As Double
        Dim TheTrk As Integer
        If GVTheStk.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVTheStk.RowCount

                If (GVTheStk.IsRowSelected(i) = True) Then
                    TheCrp = Val(GVTheStk.GetRowCellValue(i, "trkCrop"))
                    TheTrk = Val(GVTheStk.GetRowCellValue(i, "trkDet"))

                    If CanSave(i) = True Then
                        TheAmt = Val(GVTheStk.GetRowCellValue(i, "Amount"))
                        If TheAmt < 0 Then
                            GVTheStk.SetRowCellValue(i, "Amount", -1 * TheAmt)
                        End If
                        tb = (From s In db.aStoreDets Where s.trkASDet = Val(TheTrk) And s.trkASReq = Val(TxtRId.Text) Select s).Single()
                        tb.trkASReq = Val(TxtRId.Text)
                        TbUn = (From s In db.V_cropUnits Where s.unitName = Trim(GVTheStk.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkCrop = Val(GVTheStk.GetRowCellValue(RowInd, "trkCrop")) Select s).SingleOrDefault
                        tb.trkUnit = TbUn.trkUnit
                        tb.weight = Val(GVTheStk.GetRowCellValue(i, "Weight"))
                        tb.delASDet = False
                        tb.trkCrop = Val(GVTheStk.GetRowCellValue(i, "trkCrop"))
                        CalculateUnit(TbUn.trkUnit, Val(GVTheStk.GetRowCellValue(i, "Amount")),
                                  Val(GVTheStk.GetRowCellValue(i, "trkCrop")))

                        If Val(LokProcess.EditValue) = 2 Or Val(LokProcess.EditValue) = 4 Then
                            tb.aStock = Val(GVTheStk.GetRowCellValue(RowInd, "Amount")) * -1
                            tb.untOne = UOne
                            tb.amtOne = AOne * -1
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo * -1
                        ElseIf val(LokProcess.EditValue) = 3 Then
                            tb.aStock = Val(GVTheStk.GetRowCellValue(RowInd, "Amount"))
                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                        End If
                        GVTheStk.UnselectRow(i)
                    End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVTheStk.SelectedRowsCount = 0 Then
                Progress()
            End If

        End If
    End Sub
    '*******************
    Private Sub SaveArvPrdData()
        Dim tb As New receiveDet
        Dim TbUn As New V_prdUnit
        If MemAddDet = True Then
            GVTheStk.SetRowCellValue(RowInd, "trkDet", NewKeyArvPrdDet())
            tb.trkRecvDet = Val(GVTheStk.GetRowCellValue(RowInd, "trkDet"))
            tb.trkRecv = Val(TxtRId.Text)

            TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVTheStk.GetRowCellValue(RowInd, "trkUnit")) _
                                                    And s.trkProd = Val(GVTheStk.GetRowCellValue(RowInd, "trkCrop"))
                    Select s).SingleOrDefault
            tb.trkUnit = TbUn.trkUnit
            tb.weight = Val(GVTheStk.GetRowCellValue(RowInd, "Weight"))
            tb.delRecDet = False
            tb.trkProd = Val(GVTheStk.GetRowCellValue(RowInd, "trkCrop"))

            '***************************************
            Dim TheAmt As Double
            If TheAmt < 0 Then
                GVTheStk.SetRowCellValue(RowInd, "Amount", -1 * TheAmt)
            End If
            CalculatePrdUnit(TbUn.trkUnit, Val(GVTheStk.GetRowCellValue(RowInd, "Amount")),
                    Val(GVTheStk.GetRowCellValue(RowInd, "trkCrop")))
            '***************************************
            If Val(LokProcess.EditValue) = 2 Or Val(LokProcess.EditValue) = 4 Then
                tb.amount = Val(GVTheStk.GetRowCellValue(RowInd, "Amount")) * -1
                tb.untOne = UOne
                tb.amtOne = AOne * -1
                tb.untTwo = UTwo
                tb.amtTwo = ATwo * -1
            ElseIf Val(LokProcess.EditValue) = 3 Then
                tb.amount = Val(GVTheStk.GetRowCellValue(RowInd, "Amount"))
                tb.untOne = UOne
                tb.amtOne = AOne
                tb.untTwo = UTwo
                tb.amtTwo = ATwo
            End If
            db.receiveDets.InsertOnSubmit(tb)
            db.SubmitChanges()
            Progress()
        End If
    End Sub
    Public Sub SaveArvPrdEdit()
        Dim i As Integer = 0
        Dim tb As New receiveDet
        Dim TbUn As New V_prdUnit
        Dim ThePrd As Integer
        Dim TheAmt As Double
        If GVTheStk.SelectedRowsCount <> 0 Then
            'EDIT record
            While i < GVTheStk.RowCount
                Dim TheReq As Integer
                TheReq = Val(GVTheStk.GetRowCellValue(i, "trkDet"))
                If (GVTheStk.IsRowSelected(i) = True) Then
                    ThePrd = Val(GVTheStk.GetRowCellValue(i, "trkCrop"))
                    'If CheckEditDel(ThePrd) = True Then
                    If CanSave(i) = True Then
                        TheAmt = Val(GVTheStk.GetRowCellValue(i, "Amount"))
                        If TheAmt < 0 Then
                            GVTheStk.SetRowCellValue(i, "Amount", -1 * TheAmt)
                        End If
                        tb = (From s In db.receiveDets Where s.trkRecvDet = Val(TheReq) And s.trkRecv = Val(TxtRId.Text) Select s).Single()
                        tb.trkRecv = Val(TxtRId.Text)
                        TbUn = (From s In db.V_prdUnits Where s.unitName = Trim(GVTheStk.GetRowCellValue(i, "trkUnit")) _
                                                    And s.trkProd = Val(GVTheStk.GetRowCellValue(i, "trkCrop"))
                                Select s).SingleOrDefault
                        tb.trkUnit = TbUn.trkUnit
                        tb.weight = Val(GVTheStk.GetRowCellValue(i, "Weight"))
                        tb.delRecDet = False
                        tb.trkProd = Val(GVTheStk.GetRowCellValue(i, "trkCrop"))

                        CalculatePrdUnit(TbUn.trkUnit, Val(GVTheStk.GetRowCellValue(i, "Amount")),
                            Val(GVTheStk.GetRowCellValue(i, "trkCrop")))
                        If Val(LokProcess.EditValue) = 2 Or Val(LokProcess.EditValue) = 4 Then
                            tb.amount = Val(GVTheStk.GetRowCellValue(RowInd, "Amount")) * -1
                            tb.untOne = UOne
                            tb.amtOne = AOne * -1
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo * -1
                        ElseIf val(LokProcess.EditValue) = 3 Then
                            tb.amount = Val(GVTheStk.GetRowCellValue(RowInd, "Amount"))
                            tb.untOne = UOne
                            tb.amtOne = AOne
                            tb.untTwo = UTwo
                            tb.amtTwo = ATwo
                        End If
                        GVTheStk.UnselectRow(i)
                    End If
                    'End If
                End If
                i = i + 1
            End While
            db.SubmitChanges()
            If GVTheStk.SelectedRowsCount = 0 Then
                Progress()
            End If
        End If
    End Sub
    '**************************
#End Region

#Region "Key"
    Function NewBuyKey() As Integer
        Dim TheKey As Integer
        Dim Qrymax = (From trk In db.buys Select trk.trkBuyClient).ToList
        If Qrymax.Count = 0 Then
            TheKey = 1
        Else
            TheKey = Qrymax.Max() + 1
        End If
        Return TheKey
    End Function
    Function NewKeyBuyDet() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.buyDetails Select trk.trkBuyDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function
    '********************************************************
    Function NewArvKey() As Integer
        Dim TheKey As Integer
        Dim Qrymax = (From trk In db.aStoreReqs Select trk.trkASReq).ToList
        If Qrymax.Count = 0 Then
            TheKey = 1
        Else
            TheKey = Qrymax.Max() + 1
        End If
        Return TheKey
    End Function
    Function NewKeyArvDet() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.aStoreDets Select trk.trkASDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function

    Function NewArvPrdKey() As Integer
        Dim TheKey As Integer
        Dim Qrymax = (From trk In db.receives Select trk.trkRecv).ToList
        If Qrymax.Count = 0 Then
            TheKey = 1
        Else
            TheKey = Qrymax.Max() + 1
        End If
        Return TheKey
    End Function
    Function NewKeyArvPrdDet() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.receiveDets Select trk.trkRecvDet).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function


#End Region

#Region "Delete"
    Private Sub Delete(ByVal TheTrk As Integer)
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            DeleteBuy(TheTrk)
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            DeleteArv(TheTrk)
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            DeleteArvPrd(TheTrk)
        End If
    End Sub
    Private Sub DeleteBuy(ByVal TheTrk As Integer)
        Dim tb As New buyDetail
        tb = (From s In db.buyDetails Where s.trkBuyDet = Val(TheTrk) And s.trkBuyClient = Val(TxtRId.Text) Select s).Single()
        tb.delBDet = True
    End Sub

    Private Sub DeleteArv(ByVal TheTrk As Integer)
        Dim tb As New aStoreDet
        tb = (From s In db.aStoreDets Where s.trkASDet = Val(TheTrk) And s.trkASReq = Val(TxtRId.Text) Select s).Single()
        tb.delASDet = True
    End Sub
    Private Sub DeleteArvPrd(ByVal TheTrk As Integer)
        Dim tb As New receiveDet
        tb = (From s In db.receiveDets Where s.trkRecvDet = Val(TheTrk) And s.trkRecv = Val(TxtRId.Text) Select s).Single()
        tb.delRecDet = True
    End Sub


    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        GVTheStk.OptionsFind.AlwaysVisible = False
        Dim tb As New expStockDet
        Dim i As Integer = 0

        Dim lastRow As Integer = GVTheStk.RowCount - 1
        Dim lastValue As Integer = Val(GVTheStk.GetRowCellValue(lastRow, "trkDet"))
        If lastValue = 0 And GVTheStk.IsRowSelected(lastRow) = True Then
            GVTheStk.DeleteRow(lastRow)
            SavedRow = Row - 1
            Row = SavedRow
            MemAddDet = False
        End If
        If GVTheStk.SelectedRowsCount <> 0 Then

            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً هل ترغب في حذف السجلات المحددة؟  ", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            If Msg = System.Windows.Forms.DialogResult.Yes Then
                While i < GVTheStk.RowCount
                    Dim TheReq As Integer
                    TheReq = Val(GVTheStk.GetRowCellValue(i, "trkDet"))
                    If GVTheStk.IsRowSelected(i) = True Then
                        Delete(TheReq)
                        GVTheStk.DeleteRow(i)
                        i = i - 1
                        SavedRow = Row - 1
                        Row = SavedRow
                        MemAddDet = False
                    End If
                    i = i + 1
                End While
                db.SubmitChanges()
            Else
                Exit Sub
            End If
        End If
    End Sub
#End Region

#Region "Total"
    Private Function CalTotal(ByVal crp As Integer, ByVal UInd As Integer) As Double
        Dim TheDate As DateTime
        If Me.DateRecv.Text <> "" Then
            TheDate = CType(DateRecv.Text, DateTime)
        End If
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If LokStrType.Text = "مخازن المحطات للمحاصيل" Or LokStrType.Text = "مخازن المحطات للمنتجات" Then
            If RdoLocal.Checked = True Then
                isLoc = True
                Clnt = 0
            ElseIf RdoClient.Checked = True
                isLoc = 0
                Clnt = Val(LokClient.EditValue)
            End If
        End If
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            CalTotal = CalTotalBuy(TheDate, crp, UInd)
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            CalTotal = CalTotalArv(TheDate, crp, UInd, isLoc, Clnt)
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            CalTotal = CalTotalArvPrd(TheDate, crp, UInd, isLoc, Clnt)
        End If
    End Function

    '******************************** 1
    Private Function CalTotalBuy(ByVal TheDate As DateTime, ByVal crp As Integer, ByVal UInd As Integer) As Double
        Dim tb As New CrpBuyResult
        tb = (From s In db.CrpBuy(TheDate, crp, Val(LokLoc.EditValue), Val(LokStore.EditValue)) Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            If UInd = 0 Then
                CalTotalBuy = tb.oneUnt
            Else
                CalTotalBuy = tb.twoUnt
            End If
        End If
        CalTotalBuy = Math.Round(CalTotalBuy, 2)
    End Function
    '********************************** 2
    Private Function CalTotalArv(ByVal TheDate As DateTime, ByVal crp As Integer, ByVal UInd As Integer, ByVal isLoc As Boolean, ByVal Clnt As Integer) As Double
        Dim tb As New CrpArvResult
        tb = (From s In db.CrpArv(TheDate, Val(LokLoc.EditValue), Val(LokStore.EditValue), crp, isLoc, Clnt) Select s).SingleOrDefault
        If Not IsNothing(tb) Then
            If UInd = 0 Then
                CalTotalArv = tb.oneUnt
            Else
                CalTotalArv = tb.twoUnt
            End If
        End If
        CalTotalArv = Math.Round(CalTotalArv, 2)
    End Function
    '**************************** 3
    Private Function CalTotalArvPrd(ByVal TheDate As DateTime, ByVal prd As Integer, ByVal UInd As Integer, ByVal isLoc As Boolean, ByVal Clnt As Integer) As Double
        Dim tb As New PrdArvResult
        Dim tbClnt As New PrdClntTotalResult
        If RdoLocal.Checked = True Then
            tb = (From s In db.PrdArv(TheDate, Val(LokLoc.EditValue), Val(LokStore.EditValue), prd) Select s).SingleOrDefault
            If Not IsNothing(tb) Then
                If UInd = 0 Then
                    CalTotalArvPrd = tb.oneUnt
                Else
                    CalTotalArvPrd = tb.twoUnt

                End If
            End If
        ElseIf RdoClient.Checked = True
            '************************
            tbClnt = (From s In db.PrdClntTotal(TheDate, Val(LokLoc.EditValue), Val(LokStore.EditValue), prd) Select s).SingleOrDefault
            If Not IsNothing(tbClnt) Then
                If UInd = 0 Then
                    CalTotalArvPrd = tbClnt.oneUnt
                Else
                    CalTotalArvPrd = tbClnt.twoUnt

                End If
            End If
        End If
        CalTotalArvPrd = Math.Round(CalTotalArvPrd, 2)
    End Function


#End Region
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If GVTheStk.RowCount = 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لاتوجد تفاصيل  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
        Else
            GVTheStk.OptionsFind.FindFilterColumns = "*"
            GVTheStk.ShowFindPanel()
            GVTheStk.OptionsFind.ShowClearButton = False
            GVTheStk.OptionsFind.ShowFindButton = False
        End If
    End Sub
    Private Sub GVTheStk_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GVTheStk.CellValueChanged
        Dim crp As Integer = GVTheStk.GetRowCellValue(e.RowHandle, "trkCrop")
        Dim StrUn As String = Trim(GVTheStk.GetFocusedRowCellValue("trkUnit"))
        Dim TheId As Integer = GVTheStk.GetRowCellValue(e.RowHandle, "trkDet")
        Dim Amt As Double = GVTheStk.GetRowCellValue(e.RowHandle, "Amount")
        Dim Total As Double
        Dim lst
        Dim TheDate As DateTime
        If Me.DateRecv.Text <> "" Then
            TheDate = CType(DateRecv.Text, DateTime)
        End If
        '  Dim TbUn As New V_cropUnit
        Dim i As Integer = 0
        If e.Column.Caption = "الوحدة" Then
            's.unitName = StrUn
            If StrUn <> "" And crp <> 0 Then

                If LokStrType.Text = "مخازن مناطق الشراء" Or LokStrType.Text = "مخازن المحطات للمحاصيل" Then
                    lst = (From s In db.V_cropUnits Where s.trkCrop = crp And s.delCU = 0 Select s).ToList

                ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
                    lst = (From s In db.V_prdUnits Where s.trkProd = crp And s.delPU = 0 Select s).ToList
                End If

                If lst.Count = 2 Then
                    While i < 2
                        If lst.Item(i).unitName = StrUn Then
                            Exit While
                        End If
                        i = i + 1
                    End While
                End If

                Total = CalTotal(crp, i)

                If TheId <> 0 Then
                    'If Val(LokProcess.EditValue) = 2 Or Val(LokProcess.EditValue) = 3 Or Val(LokProcess.EditValue) = 4 Then
                    '    Total = Total - FindAmt(i, TheId)
                    'Else
                    '    Total = Total + FindAmt(i, TheId)
                    'End If
                    Total = Total - FindAmt(i, TheId, crp, TheDate)
                    GVTheStk.SetFocusedRowCellValue("Amount", FindAmt(i, TheId, crp, TheDate))
                End If
                GVTheStk.SetFocusedRowCellValue("total", Total)
            Else
                GVTheStk.SetFocusedRowCellValue("total", 0)
            End If
        End If
    End Sub
    Private Function FindAmt(ByVal ind As Integer, ByVal TheId As Integer, ByVal crp As Integer, ByVal TheDate As DateTime) As Double
        Dim tbBuy As New V_buyDetail
        Dim tbOutPrd As New V_ArvStr
        Dim tbRecv As New V_RecvDet
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = True
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = False
            Clnt = Val(LokClient.EditValue)
        End If
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            'tbBuy = (From s In db.buyDetails Where s.trkBuyDet = TheId Select s).SingleOrDefault
            tbBuy = (From s In db.V_buyDetails Where s.trkBuyDet = Val(TheId) _
                                                         And s.trkCrop = crp And s.trkBuyLoc = Val(LokLoc.EditValue) _
                                                         And s.trkBStore = Val(LokStore.EditValue) And s.buyDate <= TheDate
                     Select s).SingleOrDefault
            If ind = 0 Then
                FindAmt = tbBuy.amtOne
            Else
                FindAmt = tbBuy.amtTwo
            End If
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل" Then
            tbOutPrd = (From s In db.V_ArvStrs Where s.trkASDet = Val(TheId) _
                                                          And s.trkCrop = crp And s.trkArival = Val(LokLoc.EditValue) _
                                                         And s.trkAStore = Val(LokStore.EditValue) And s.isLocal = isLoc _
                                                       And s.trkClntCrp = Clnt And s.SArDate <= TheDate Select s).SingleOrDefault()
            If ind = 0 Then
                FindAmt = tbOutPrd.amtOne
            Else
                FindAmt = tbOutPrd.amtTwo
            End If
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            tbRecv = (From s In db.V_RecvDets Where s.trkRecvDet = Val(TheId) _
                                 And s.trkProd = crp And s.trkArival = Val(LokLoc.EditValue) _
                                                         And s.trkAPrdStr = Val(LokStore.EditValue) And s.isLocal = isLoc _
                                                       And s.trkClntCrp = Clnt And s.recvDate <= TheDate Select s).SingleOrDefault()
            If ind = 0 Then
                FindAmt = tbRecv.amtOne
            Else
                FindAmt = tbRecv.amtTwo
            End If
        End If
        FindAmt = Math.Round(FindAmt, 2)
    End Function

    Private Sub RdoLocal_Click(sender As Object, e As EventArgs) Handles RdoLocal.Click
        LokClient.Enabled = False
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub RdoClient_Click(sender As Object, e As EventArgs) Handles RdoClient.Click
        LokClient.Enabled = True
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
    Private Sub FillClient()
        If done = True And LokLoc.Text <> "" Then
            LokClient.Properties.DataSource = ""
            Dim lst = (From c In db.clientCrps Where c.delClntCrp = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokClient.Properties.DataSource = lst
            LokClient.Properties.DisplayMember = "clntCrpName"
            LokClient.Properties.ValueMember = "trkClntCrp"
            LokClient.Properties.PopulateColumns()
            LokClient.Properties.Columns(0).Visible = False
            LokClient.Properties.Columns(2).Visible = False
            LokClient.Properties.Columns(3).Visible = False
            LokClient.Properties.Columns(4).Visible = False
            LokClient.Properties.Columns(5).Visible = False
        End If
    End Sub

    Private Sub LokProcess_TextChanged(sender As Object, e As EventArgs) Handles LokProcess.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub LokStore_TextChanged(sender As Object, e As EventArgs) Handles LokStore.TextChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        If GVTheStk.SelectedRowsCount <> 0 Then
            XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لم يتم حفظ السجلات المعدلة  ", "نقص في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
            Exit Sub
        Else
            If SaveInclose() = False Then
                ResetAtClose()
                Me.Close()
            Else
                BtnSave_Click(Nothing, Nothing)
            End If
        End If
    End Sub

#Region "Report"
    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        Dim isLoc As Boolean
        Dim Clnt As Integer
        If RdoLocal.Checked = True Then
            isLoc = 1
            Clnt = 0
        ElseIf RdoClient.Checked = True
            isLoc = 0
            Clnt = Val(LokClient.EditValue)
        End If
        If LokStrType.Text = "مخازن مناطق الشراء" Then
            RepBuy()
        ElseIf LokStrType.Text = "مخازن المحطات للمحاصيل"
            RepArrive(isLoc, Clnt)
        ElseIf LokStrType.Text = "مخازن المحطات للمنتجات"
            RepArvPrd(isLoc, Clnt)
        End If

    End Sub

    Private Sub RepBuy()
        Dim rpt As New RepStkBuy
        rpt.XrLHead.Text = "ضبط المخزن (مخازن مناطق الشراء)"
        rpt.FilterString = " [trkBuyClient] =" & Val(TxtRId.Text)
        Dim head As String = RepHeader()
        Dim wtr As String = RepWater()

        If IsHeader = True Then
            rpt.XrPic.ImageUrl = head
            rpt.XrPic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        End If
        If IsWater Then
            Dim imgWtr As Image = Image.FromFile(wtr)
            rpt.Watermark.Image = imgWtr
            rpt.Watermark.ImageTransparency = 240
        End If
        '******************
        rpt.TheFilter.Visible = False
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub
    Private Sub RepArrive(ByVal IsLoc As Boolean, ByVal clnt As Integer)
        Dim rpt As New RepStkArv
        rpt.XrLHead.Text = "ضبط المخزن (مخازن المحطات للمحاصيل)"
        rpt.FilterString = " [trkASReq] =" & Val(TxtRId.Text) & " and [trkPrs] <> 1" & " and [isLocal]= " & IsLoc & " and [trkClntCrp]= " & clnt
        Dim head As String = RepHeader()
        Dim wtr As String = RepWater()

        If IsHeader = True Then
            rpt.XrPic.ImageUrl = head
            rpt.XrPic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        End If
        If IsWater Then
            Dim imgWtr As Image = Image.FromFile(wtr)
            rpt.Watermark.Image = imgWtr
            rpt.Watermark.ImageTransparency = 240
        End If
        '******************
        rpt.TheFilter.Visible = False
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub
    Private Sub RepArvPrd(ByVal IsLoc As Boolean, ByVal clnt As Integer)
        Dim rpt As New RepStkArvPrd
        rpt.XrLHead.Text = "ضبط المخزن (مخازن المحطات للمنتجات)"
        rpt.FilterString = " [trkRecv] =" & Val(TxtRId.Text) & " and [trkPrs] <> 1" & " and [isLocal]= " & IsLoc & " and [trkClntCrp]= " & clnt
        Dim head As String = RepHeader()
        Dim wtr As String = RepWater()

        If IsHeader = True Then
            rpt.XrPic.ImageUrl = head
            rpt.XrPic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        End If
        If IsWater Then
            Dim imgWtr As Image = Image.FromFile(wtr)
            rpt.Watermark.Image = imgWtr
            rpt.Watermark.ImageTransparency = 240
        End If
        '******************
        rpt.TheFilter.Visible = False
        rpt.RequestParameters = False
        rpt.ShowPreview()
    End Sub


#End Region
    Private Sub buyInf_TextChanged(sender As Object, e As EventArgs) Handles buyInf.TextChanged
        If saved = True Then
            MemEdit = True
        End If
    End Sub

    Private Sub LokClient_TextChanged(sender As Object, e As EventArgs) Handles LokClient.TextChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub

    Private Sub DateRecv_EditValueChanged(sender As Object, e As EventArgs) Handles DateRecv.EditValueChanged
        If saved = True Then
            MemEdit = True
        End If
        TheStk()
    End Sub
End Class