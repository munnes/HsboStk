
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel

Public Class FrmViewClntPrd
    Private col1, col2, col3, col4, col5, col6, col7 As Columns.GridColumn
    Private FDate, TDate As DateTime

    Private Sub FrmViewClntPrd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillLoc()
        LokLoc.Properties.ShowFooter = True

    End Sub

    Private Sub FillLoc()
        done = False
        Dim lst = (From c In db.arivalLocs Where c.delAr = False Select c).ToList
        LokLoc.Text = ""
        Me.LokLoc.Properties.DataSource = lst
        LokLoc.Properties.DisplayMember = "arivalName"
        LokLoc.Properties.ValueMember = "trkArival"
        LokLoc.Properties.PopulateColumns()
        LokLoc.Properties.Columns(0).Visible = False
        LokLoc.Properties.Columns(2).Visible = False
        done = True
    End Sub
    Private Sub FillStore()
        If done = True And LokLoc.Text <> "" Then
            LokStore.Properties.DataSource = ""
            Dim lst = (From c In db.arivalPrdStores Where c.delAPrd = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokStore.Properties.DataSource = lst
            LokStore.Properties.DisplayMember = "APrdStr"
            LokStore.Properties.ValueMember = "trkAPrdStr"
            LokStore.Properties.PopulateColumns()
            LokStore.Properties.Columns(0).Visible = False
            LokStore.Properties.Columns(2).Visible = False
            LokStore.Properties.Columns(3).Visible = False
        End If
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
    Public Sub FillPeeler()
        If done = True And LokLoc.Text <> "" Then
            LokPeeler.Properties.DataSource = ""
            Dim lst = (From c In db.peelers Where c.delPe = False And c.trkArival = Val(LokLoc.EditValue.ToString()) Select c).ToList
            LokPeeler.Text = ""
            Me.LokPeeler.Properties.DataSource = lst
            LokPeeler.Properties.DisplayMember = "peelerName"
            LokPeeler.Properties.ValueMember = "trkPeeler"
            LokPeeler.Properties.PopulateColumns()
            LokPeeler.Properties.Columns(0).Visible = False
            LokPeeler.Properties.Columns(2).Visible = False
            LokPeeler.Properties.Columns(3).Visible = False

        End If
    End Sub


    Private Sub LokLoc_TextChanged(sender As Object, e As EventArgs) Handles LokLoc.TextChanged
        LokPeeler.Text = ""
        LokStore.Text = ""
        LokClient.Text = ""
        FillStore()
        FillPeeler()
        FillClient()

    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        ResetAtClose()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        ResetAtClose()
    End Sub
    Public Sub ResetAtClose()
        '************fill store ,and save req number should be rest, b/c when re-open form 
        done = False
        Me.Close()
        FrmMain.RibbonControl.Enabled = True
    End Sub
    Private Sub FillGrid()

        TxtJobOrd.Text = ""
        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)
        End If

        Select Case True
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""
                '000011
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                   And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList

                GridControl1.DataSource = lst

            '000111
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString And s.trkPrs = 1 _
                                                   And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '001011
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                   And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst

            '    '001111
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) And s.trkPrs = 1 _
                                                    And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst

            '    '010011
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                    And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst

            '    '010111
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString And s.trkPrs = 1 _
                                                    And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst

            '    '011011
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                            And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst

            '    '011111
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) And s.trkPrs = 1 _
                                                   And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst


            '    '********************************
            '    '*****************missing options due to store location relation ( 1000, 1001, 1010, 1011)
            '    '*********************************
            '    '110011
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                            And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst

            '    '110111
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.recvDate >= FDate.ToShortDateString And s.trkPrs = 1 _
                                                            And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '111011
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                            And s.delRecv = False And s.isLocal = False
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '111111
            Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.delRecv = False And s.isLocal = False And s.trkPrs = 1
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst

            '**************************add the peeler
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text = ""

                '000001
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                           And s.recvDate >= FDate.ToShortDateString _
                                                           And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                           And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList

                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text = ""
                '000101
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString And s.trkPrs = 1 _
                                                   And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '001001
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                   And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '    '001101
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) And s.trkPrs = 1 _
                                                    And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '    '010001
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                    And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst

            '    '010101
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString And s.trkPrs = 1 _
                                                    And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '    '011001
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text = ""
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                            And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '    '011101
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text = ""
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) And s.trkPrs = 1 _
                                                   And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '*****************Client
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                '000000
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                           And s.recvDate >= FDate.ToShortDateString _
                                                           And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                           And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                       And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                '000100
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString And s.trkPrs = 1 _
                                                   And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                     And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '001000
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                   And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                    And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '    '001100
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.trkAPrdStr = Val(LokStore.EditValue) And s.trkPrs = 1 _
                                                    And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                           And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '    '010000
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text <> ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                    And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                          And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst

            '    '010100
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text = ""

                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate >= FDate.ToShortDateString And s.trkPrs = 1 _
                                                    And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                          And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '    '011000
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                   And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                            And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                          And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '    '011100
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) And s.trkPrs = 1 _
                                                   And s.delRecv = False And s.isLocal = False And s.trkPeeler = Val(LokPeeler.EditValue) _
                                                          And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '**********************
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text <> ""
                '000010
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                           And s.recvDate >= FDate.ToShortDateString _
                                                           And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                           And s.delRecv = False And s.isLocal = False _
                                                       And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text <> ""
                '000110
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                           And s.recvDate >= FDate.ToShortDateString _
                                                         And s.trkPrs = 1 _
                                                           And s.delRecv = False And s.isLocal = False _
                                                       And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text <> ""
                '001010
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                         And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                           And s.delRecv = False And s.isLocal = False _
                                                       And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text <> ""
                '001110
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.trkAPrdStr = Val(LokStore.EditValue) _
                                                           And s.trkPrs = 1 _
                                                           And s.delRecv = False And s.isLocal = False _
                                                       And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text <> ""
                '010010
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                         And s.recvDate >= FDate.ToShortDateString _
                                                           And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                           And s.delRecv = False And s.isLocal = False _
                                                       And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            '***************************
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text <> ""
                '011010
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.recvDate >= FDate.ToShortDateString _
                                                         And s.trkPrs = 1 _
                                                           And s.delRecv = False And s.isLocal = False _
                                                       And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text <> ""
                '011010
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                           And s.recvDate <= TDate.ToShortDateString And s.trkPrs = 1 _
                                                           And s.delRecv = False And s.isLocal = False _
                                                       And s.trkClntCrp = Val(LokClient.EditValue)
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text <> ""
                '011110
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                      And s.trkClntCrp = Val(LokClient.EditValue) And s.isLocal = False And s.delRecv = False And s.trkPrs = 1
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst
            Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text <> ""
                '011110
                Dim lst = (From s In db.V_Receives Where s.trkArival = Val(LokLoc.EditValue) _
                                                      And s.trkClntCrp = Val(LokClient.EditValue) _
                                                         And s.recvDate >= FDate.ToShortDateString And s.isLocal = False And s.delRecv = False And s.trkPrs = 1
                           Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
                GridControl1.DataSource = lst

        End Select

        ViewBtns()

    End Sub
    Private Sub ViewBtns()
        ' ********************Add repository button for details
        GVBuyView.Columns(0).Visible = False
        GVBuyView.Columns.Add()
        GVBuyView.Columns(6).ColumnEdit = repBtnView
        GVBuyView.Columns.Add()
        GVBuyView.Columns(7).ColumnEdit = repBtnDel
    End Sub
    'Public Function CalFlag() As Boolean
    '    If RdoLocal.Checked = True Then
    '        CalFlag = True
    '    ElseIf RdoClient.Checked = True
    '        CalFlag = False
    '    End If
    'End Function
    Private Sub BtnSearch_Click(sender As Object, e As EventArgs) Handles BtnSearch.Click
        If CanSearch() = True Then
            FillGrid()
            FormatColumns()
        End If
    End Sub

    Private Sub btnLok_Click(sender As Object, e As EventArgs) Handles btnLok.Click
        LokLoc.Text = ""
        LokPeeler.Text = ""
        LokStore.Text = ""
        LokClient.Text = ""
        LblHead.Focus()
    End Sub

    Private Sub btnStore_Click(sender As Object, e As EventArgs) Handles btnStore.Click
        LokPeeler.Text = ""
        LblHead.Focus()
    End Sub

    Private Function CanSearch() As Boolean
        CanSearch = False
        If FromDate.Text <> "" And ToDate.Text <> "" Then

            If CType(FromDate.Text, DateTime) > CType(ToDate.Text, DateTime) Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً الرجاء تاريخ البداية لا يمكن أن يكون أكبر من تاريخ النهاية! ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                FromDate.Focus()
                Exit Function
            End If

        End If

        If FromDate.Text <> "" Then
            If CType(FromDate.Text, DateTime) > Today Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل! ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                FromDate.Focus()
                Exit Function
            End If
        End If
        If ToDate.Text <> "" Then
            If CType(ToDate.Text, DateTime) > Today Then
                XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً لا يمكنك ادخال تاريخ في المستقبل! ", "خطأ في الإدخال", MessageBoxButtons.OK, MessageBoxIcon.Hand)
                ToDate.Focus()
                Exit Function
            End If
        End If

        CanSearch = True
    End Function

    Private Sub btnClnt_Click(sender As Object, e As EventArgs) Handles btnClnt.Click
        LokClient.Text = ""
    End Sub

    Private Sub BtnFind_Click(sender As Object, e As EventArgs) Handles BtnFind.Click
        Dim lst = (From s In db.V_Receives Where s.trkToPrd = Val(TxtJobOrd.Text) And s.trkPrs = 1 And s.isLocal = False
                   Select s.trkRecv, s.arivalName, s.peelerName, s.APrdStr, s.clntCrpName, s.recvDate).ToList
        GridControl1.DataSource = lst
        Dim lst2 = (From s In db.V_Receives Where s.trkToPrd = Val(TxtJobOrd.Text) And s.trkPrs = 1 And s.isLocal = False
                    Select s).ToList
        If lst2.Count <> 0 Then
            LokPeeler.Reset()
            LokLoc.Reset()
            LokStore.Reset()
            LokClient.Reset()
            LokLoc.EditValue = lst2.Item(0).trkArival
            LokStore.EditValue = lst2.Item(0).trkAPrdStr
            LokPeeler.EditValue = lst2.Item(0).trkPeeler
            LokClient.EditValue = lst2.Item(0).trkClntCrp
            LokPeeler.Refresh()
            LokLoc.Refresh()
            LokStore.Refresh()
            LokClient.Refresh()
        Else
            clearFrm()
        End If
        ViewBtns()
        FormatColumns()
    End Sub
    Private Sub clearFrm()
        LokLoc.Text = ""
        LokStore.Text = ""
        LokPeeler.Text = ""
        LokClient.Text = ""
        GridControl1.DataSource = ""
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        'Dim isLoc As Boolean
        'If RdoLocal.Checked = True Then
        '    isLoc = True
        'ElseIf RdoClient.Checked = True
        '    isLoc = False
        'End If
        '***********************

        Dim rpt As New RepViewRecv

        rpt.XrLHead.Text = LblHead.Text
        Dim head As String = RepHeader()
        Dim wtr As String = RepWater()
        rpt.XrLHead.Text = LblHead.Text
        If IsHeader = True Then
            rpt.XrPic.ImageUrl = head
            rpt.XrPic.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage
        End If
        If IsWater Then
            Dim imgWtr As Image = Image.FromFile(wtr)
            rpt.Watermark.Image = imgWtr
            rpt.Watermark.ImageTransparency = 240
        End If
        '**************************
        If FromDate.Text <> "" Then
            FDate = CType(FromDate.Text, DateTime)
        End If
        If ToDate.Text <> "" Then
            TDate = CType(ToDate.Text, DateTime)
        End If
        '**************************
        If Val(TxtJobOrd.Text) <> 0 Then
            rpt.TheFilter.Value = " امر انتاج: " & Val(TxtJobOrd.Text)
            rpt.FilterString = "[trkToPrd]= " & Val(TxtJobOrd.Text)
        Else
            If CanSearch() = True Then
                FillGrid()
                FormatColumns()

                Select Case True
                    '0000
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false"
                    '0001
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false "
                    '0010
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                            & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false"
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "/ مخزن: " & LokStore.Text
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) & " and [trkPrs]= 1 and [isLocal]= false"
                    '0100
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false "
                    '0101
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false"
                    '0110
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false "
                    '0111
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkPrs]= 1 and [isLocal]= false"

                    '1100
                    Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                        rpt.FilterString = "[recvDate] >= #" & FDate.ToShortDateString & "# And [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false"

                    '    '1101
                    Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = " من تاريخ: " & FDate.ToShortDateString
                        rpt.FilterString = "[recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false"
                    '1110
                    Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "الى تاريخ: " & TDate.ToShortDateString
                        rpt.FilterString = " [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false"

                    '1111
                    Case Me.LokLoc.Text = "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text = ""
                        rpt.TheFilter.Value = ""
                        rpt.FilterString = "[trkPrs] = 1 and [isLocal]= false"
                    '******************************************PEELER
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "- مخزن: " & LokStore.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)
                    '0001
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "- مخزن: " & LokStore.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)
                    '0010
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "- مخزن: " & LokStore.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                            & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)
                    '0011
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "- مخزن: " & LokStore.Text
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) & " and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)
                    '0100
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)
                    '0101
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)
                    '0110
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)
                    '0111
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text = ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)

                    '1100
                    ''*********************client
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "- مخزن: " & LokStore.Text & "؛ " & "  - لصالح العميل: " & LokClient.Text & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)
                    '0001
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "- مخزن: " & LokStore.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue)
                    '0010
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "- مخزن: " & LokStore.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                            & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue)
                    '0011
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "- مخزن: " & LokStore.Text
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) & " and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue)
                    '0100
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)
                    '0101
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue)
                    '0110
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue) _
                        & " and [trkClntCrp]= " & Val(LokClient.EditValue)
                    '0111
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text <> "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & " - القشارة\الغربال: " & LokPeeler.Text & "  - لصالح العميل: " & LokClient.Text
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkPrs]= 1 and [isLocal]= false And [trkPeeler] = " & Val(LokPeeler.EditValue) _
                        & " and [trkClntCrp]= " & Val(LokClient.EditValue)
                    '**********************************
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "- مخزن: " & LokStore.Text & "؛ " & "  - لصالح العميل: " & LokClient.Text & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false"
                    '0001
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "- مخزن: " & LokStore.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false  and [trkClntCrp]= " & Val(LokClient.EditValue)
                    '0010
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "- مخزن: " & LokStore.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) _
                            & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false  and [trkClntCrp]= " & Val(LokClient.EditValue)
                    '0011
                    Case Me.LokLoc.Text <> "" And LokStore.Text <> "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "- مخزن: " & LokStore.Text & "  - لصالح العميل: " & LokClient.Text
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " And [trkAPrdStr]= " & Val(LokStore.EditValue) & " and [trkPrs]= 1 and [isLocal]= false  and [trkClntCrp]= " & Val(LokClient.EditValue)
                    '0100
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " في الفترة بين: " & FDate.ToShortDateString & " و " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false" _
                    '0101
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text <> "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " من تاريخ: " & FDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkClntCrp]= " & Val(LokClient.EditValue) _
                            & " and [recvDate] >= #" & FDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false " _
                    '0110
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text <> "" And LokPeeler.Text = "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "  - لصالح العميل: " & LokClient.Text & "؛ " & " الى تاريخ: " & TDate.ToShortDateString
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [recvDate] <= #" & TDate.ToShortDateString & "# and [trkPrs]= 1 and [isLocal]= false" _
                        & " and [trkClntCrp]= " & Val(LokClient.EditValue)
                    '0111
                    Case Me.LokLoc.Text <> "" And LokStore.Text = "" And FromDate.Text = "" And ToDate.Text = "" And LokPeeler.Text = "" And LokClient.Text <> ""
                        rpt.TheFilter.Value = "محطة:  " & LokLoc.Text & "  - لصالح العميل: " & LokClient.Text
                        rpt.FilterString = "[trkArival]= " & Val(Me.LokLoc.EditValue) & " and [trkPrs]= 1 and [isLocal]= false " _
                        & " and [trkClntCrp]= " & Val(LokClient.EditValue)
                End Select
            End If
            rpt.TheFilter.Visible = False
            '******************
            rpt.RequestParameters = False

            rpt.ShowPreview()
        End If
    End Sub

    Private Sub FormatColumns()
        col1 = GVBuyView.Columns(1)
        col2 = GVBuyView.Columns(2)
        col3 = GVBuyView.Columns(3)
        col4 = GVBuyView.Columns(4)
        col5 = GVBuyView.Columns(5)
        col6 = GVBuyView.Columns(6)
        col7 = GVBuyView.Columns(7)
        '****************
        col1.Caption = "المحطة"
        col2.Caption = "القشارة\الغربال "
        col3.Caption = "مخزن المنتجات "
        col4.Caption = "العميل "
        col5.Caption = "تاريخ الاستلام"
        col6.Caption = "عرض التفاصيل"
        col7.Caption = "حذف"
        GVBuyView.Columns(5).Width = 100
        GVBuyView.Columns(6).Width = 100
        GVBuyView.Columns(6).Visible = True
        GVBuyView.Columns(7).Width = 40
        GVBuyView.Columns(7).Visible = True
    End Sub

    Public Function CanDelete() As Boolean
        CanDelete = False
        Dim TrkRecv As Integer = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkRecv"))
        Dim lstRecv = (From s In db.receiveDets Where s.trkRecv = Val(TrkRecv) And s.delRecDet = 0 Select s).ToList()
        If lstRecv.Count > 0 Then
            Exit Function
        End If
        CanDelete = True
    End Function

    Private Sub repBtnView_Click(sender As Object, e As EventArgs) Handles repBtnView.Click
        ID = GVBuyView.GetFocusedRowCellValue("trkRecv")
        Dim tb As New receiveDet
        Dim lst = (From s In db.receiveDets Where s.trkRecv = ID And s.delRecDet = 0 Select s).ToList
        CountView = lst.Count
        '**********************
        IsView = True
        MemAddDet = False
        done = False
        MemEdit = False
        Dim MyFrmAddReceive As New FrmReceive
        'MyFrmAddBuy.MdiParent = Me
        MyFrmAddReceive.Show()
    End Sub

    Private Sub repBtnDel_Click(sender As Object, e As EventArgs) Handles repBtnDel.Click
        If CanDelete() = True Then
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..سيتم حذف السجل الحالي بصورة نهائية ... هل أنت متأكد من ذلك", "تأكيد الحذف", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If Msg = MsgBoxResult.Yes Then
                Dim tb As New receive
                tb = (From s In db.receives Where s.trkRecv = Val(GVBuyView.GetRowCellValue(GVBuyView.GetSelectedRows(0), "trkRecv")) Select s).Single()
                tb.delRecv = True
                db.SubmitChanges()
                FillGrid()
            End If
        Else
            Msg = XtraMessageBox.Show(DevExpress.LookAndFeel.UserLookAndFeel.Default, " عفواً..لا يمكنك حذف هذا السجل لارتباطه بسجلات أخرى", "منع الحذف", MessageBoxButtons.OK, MessageBoxIcon.Stop)

        End If
    End Sub

    Private Sub TxtJobOrd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtJobOrd.KeyPress
        e.Handled = NumOnly(e.KeyChar)
    End Sub
End Class