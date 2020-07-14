
Imports System.IO
Imports DevExpress.XtraBars.Docking2010
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Base
Imports System.ComponentModel
Imports DevExpress.Utils
Imports DevExpress.Data

Public Class FrmUpldHdr
    Private col1, col2, col3, col4, col5, col6 As Columns.GridColumn
    Private MyPic As String
    Private Sub BtnUpload_Click(sender As Object, e As EventArgs) Handles BtnUpload.Click
        Dim open As New OpenFileDialog()
        open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;)|*.jpg; *.jpeg; *.gif; *.bmp;"
        If open.ShowDialog() = DialogResult.OK Then
            TxtExt.Text = open.FileName

            Pic.Image = New Bitmap(open.FileName)

            MemAdd = True
        End If
    End Sub

    Private Sub FrmUpldHdr_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Center(Me)
        FillGrid()
        FormatColumns()
        ProgressBarControl1.Visible = False
    End Sub

    Private Sub BtnMeClose_Click(sender As Object, e As EventArgs) Handles BtnMeClose.Click
        Me.Close()
        ResetAtClose()
    End Sub


    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        If MemAdd Then
            Dim x As String = Application.StartupPath
            Dim y = Application.StartupPath.ToLower.Replace("\bin\debug", "").Replace("\Images", "")
            File.Copy(TxtExt.Text, Path.Combine(y & "\Images\", Path.GetFileName(TxtExt.Text)), True)
            MyPic = Path.Combine(y & "\Images\", Path.GetFileName(TxtExt.Text))

            SaveData()
            MemAdd = False
        End If
    End Sub
    Private Sub SaveData()
        Dim tb As New headImg
        Dim i As Integer
        Dim fRow As Integer
        If MemAdd = True Then
            tb.trkUpld = NewKey()
            tb.imgExt = MyPic
            tb.actv = True
            db.headImgs.InsertOnSubmit(tb)

            While i < GVHead.RowCount
                fRow = Val(GVHead.GetRowCellValue(i, "trkUpld"))
                tb = (From s In db.headImgs Where s.trkUpld = Val(fRow) And s.actv = True Select s).SingleOrDefault
                If Not IsNothing(tb) Then
                    tb.actv = False
                    GVHead.UnselectRow(i)
                    Exit While
                End If
                i = i + 1
            End While
        End If
        db.SubmitChanges()
        Progress()
        FillGrid()
        FormatColumns()
    End Sub
    Private Sub FillGrid()
        Dim i As Integer = 0
        Dim lst = (From s In db.headImgs Select s).ToList
        GridControl1.DataSource = lst
        GVHead.Columns(2).Visible = False
        GVHead.Columns(1).Visible = False

        GVHead.Columns.Add()
        GVHead.Columns(3).ColumnEdit = repBtnView
        GVHead.OptionsSelection.MultiSelect = True
        GVHead.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect
        While i < GVHead.RowCount
            If lst.Item(i).actv = True Then
                GVHead.FocusedRowHandle = i
                GVHead.SelectRow(i)
                Pic.Image = New Bitmap(lst.Item(i).imgExt)
                Exit While
            End If
            i = i + 1
        End While

    End Sub
    Private Sub FormatColumns()
        col1 = GVHead.Columns(0)
        col2 = GVHead.Columns(1)
        col3 = GVHead.Columns(2)
        col4 = GVHead.Columns(3)
        '  col5 = GVHead.Columns(4)

        '****************
        col1.Caption = "الرقم"
        col2.Caption = "رابط الصورة "
        col4.Caption = "عرض"

        GVHead.Columns(3).Visible = True

        GVHead.Columns(0).Width = 30
        GVHead.Columns(1).Width = 150
        GVHead.Columns(3).Width = 30

    End Sub
    Function NewKey() As Integer
        Dim max As Integer
        Dim Qrymax = (From trk In db.headImgs Select trk.trkUpld).ToList
        If Qrymax.Count = 0 Then
            max = 1
        Else
            max = Qrymax.Max() + 1
        End If
        Return max
    End Function

    Private Sub BtnAssign_Click(sender As Object, e As EventArgs) Handles BtnAssign.Click
        Dim tb As New headImg
        Dim i As Integer = 0
        Dim MyRow As Integer
        While i < GVHead.RowCount
            MyRow = Val(GVHead.GetRowCellValue(i, "trkUpld"))
            tb = (From s In db.headImgs Where s.trkUpld = Val(MyRow) Select s).SingleOrDefault
            If Not IsNothing(tb) Then
                If GVHead.IsRowSelected(i) Then
                    tb.actv = True
                    Pic.Image = New Bitmap(tb.imgExt)
                Else
                    tb.actv = False
                End If
            End If
            i = i + 1
        End While
        db.SubmitChanges()
    End Sub

    Private Sub Progress()
        ProgressBarControl1.Visible = True
        Me.ProgressBarControl1.EditValue = 0
        Me.Timer1.Start()
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        Dim tb As New headImg
        Dim i As Integer = 0
        Dim MyRow As Integer
        While i < GVHead.RowCount
            MyRow = Val(GVHead.GetRowCellValue(i, "trkUpld"))
            tb = (From s In db.headImgs Where s.trkUpld = Val(MyRow) And s.actv = True Select s).SingleOrDefault
            If Not IsNothing(tb) Then
                If GVHead.IsRowSelected(i) Then
                    tb.actv = False
                    Pic.CutImage()
                    GVHead.UnselectRow(i)
                End If
            End If
            i = i + 1
        End While
        db.SubmitChanges()
    End Sub

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
        ResetAtClose()
    End Sub
    Private Sub ProgressBarControl1_EditValueChanged(sender As Object, e As EventArgs) Handles ProgressBarControl1.EditValueChanged
        If Me.ProgressBarControl1.EditValue = 100 Then
            Me.ProgressBarControl1.Visible = False
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.ProgressBarControl1.Increment(1)
    End Sub

    Private Sub GVHead_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles GVHead.SelectionChanged
        Dim i As Integer = 0
        While i < GVHead.RowCount
            If GVHead.IsRowSelected(i) And i <> GVHead.FocusedRowHandle Then
                GVHead.UnselectRow(i)
            End If
            i = i + 1
        End While
    End Sub



    Private Sub GVHead_RowCellClick(sender As Object, e As RowCellClickEventArgs) Handles GVHead.RowCellClick
        Dim tb As New headImg
        Dim MyRow As Integer
        If e.Column.Caption = "عرض" Then
            MyRow = Val(GVHead.GetRowCellValue(e.RowHandle, "trkUpld"))
            tb = (From s In db.headImgs Where s.trkUpld = Val(MyRow) Select s).SingleOrDefault
            If Not IsNothing(tb) Then
                Pic.Image = New Bitmap(tb.imgExt)
            End If
        End If
    End Sub
End Class