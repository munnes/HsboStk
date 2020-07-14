
Imports DevExpress.LookAndFeel
Public Class FrmMain
    Sub New()
        InitializeComponent()
        inSkin()


    End Sub
    Sub inSkin()
        DevExpress.Skins.SkinManager.EnableFormSkins()
        DevExpress.UserSkins.BonusSkins.Register()
        UserLookAndFeel.Default.SetSkinStyle(My.Settings.LastSkin)
    End Sub
    Private Sub FrmMain_FormClosing(sender As Object, e As EventArgs) Handles Me.FormClosing
        My.Settings.LastSkin = UserLookAndFeel.Default.SkinName
        My.Settings.Save()
    End Sub
    'Private Sub inSkinGalary()
    '    DevExpress.XtraBars.Helpers.SkinHelper.InitSkinGallery(rgbSkins, True)
    'End Sub
    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem1.ItemClick
        Dim MyFrmBuyLoc As New FrmBuyLoc
        MyFrmBuyLoc.MdiParent = Me
        MyFrmBuyLoc.Show()
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem2.ItemClick
        Dim MyFrmBuyStore As New FrmBuyStore
        MyFrmBuyStore.MdiParent = Me
        MyFrmBuyStore.Show()
    End Sub

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem3.ItemClick
        Dim MyFrmArival As New FrmArival
        MyFrmArival.MdiParent = Me
        MyFrmArival.Show()
    End Sub

    Private Sub BarButtonItem4_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem4.ItemClick
        Dim MyFrmArivalStore As New FrmArivalStore
        MyFrmArivalStore.MdiParent = Me
        MyFrmArivalStore.Show()
    End Sub

    Private Sub BarButtonItem5_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem5.ItemClick
        Dim MyFrmArPrdStore As New FrmArPrdStore
        MyFrmArPrdStore.MdiParent = Me
        MyFrmArPrdStore.Show()
    End Sub

    Private Sub BarButtonItem7_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem7.ItemClick
        Dim MyFrmExportLoc As New FrmExportLoc
        MyFrmExportLoc.MdiParent = Me
        MyFrmExportLoc.Show()
    End Sub

    Private Sub BarButtonItem9_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem9.ItemClick
        Dim MyFrmPeeler As New FrmPeeler
        MyFrmPeeler.MdiParent = Me
        MyFrmPeeler.Show()
    End Sub

    Private Sub BarButtonItem10_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem10.ItemClick
        Dim MyFrmPlrStr As New FrmPlrStr
        MyFrmPlrStr.MdiParent = Me
        MyFrmPlrStr.Show()
    End Sub

    Private Sub BarButtonItem13_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem13.ItemClick
        Dim MyFrmCrop As New FrmCrop
        MyFrmCrop.MdiParent = Me
        MyFrmCrop.Show()
    End Sub

    Private Sub BarButtonItem14_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem14.ItemClick
        Dim MyFrmProduct As New FrmProduct
        MyFrmProduct.MdiParent = Me
        MyFrmProduct.Show()
    End Sub

    Private Sub BarButtonItem15_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem15.ItemClick
        Dim MyFrmUnit As New FrmUnit
        MyFrmUnit.MdiParent = Me
        MyFrmUnit.Show()
    End Sub

    Private Sub BarButtonItem18_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem18.ItemClick
        Dim MyFrmCar As New FrmCar
        MyFrmCar.MdiParent = Me
        MyFrmCar.Show()
    End Sub

    Private Sub BarButtonItem16_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem16.ItemClick
        Dim MyFrmClient As New FrmClient
        MyFrmClient.MdiParent = Me
        MyFrmClient.Show()
    End Sub

    Private Sub BarButtonItem17_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem17.ItemClick
        Dim MyFrmDriver As New FrmDriver
        MyFrmDriver.MdiParent = Me
        MyFrmDriver.Show()
    End Sub

    Private Sub BarButtonItem8_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem8.ItemClick
        Dim MyFrmExpStore As New FrmExpStore
        MyFrmExpStore.MdiParent = Me
        MyFrmExpStore.Show()
    End Sub

    Private Sub BarButtonItem19_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem19.ItemClick
        Dim MyFrmAddBuy As New FrmAddBuy
        MyFrmAddBuy.MdiParent = Me
        MyFrmAddBuy.Show()
    End Sub

    Private Sub BarButtonItem20_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem20.ItemClick
        Dim MyFrmViewBuy As New FrmViewBuy
        MyFrmViewBuy.MdiParent = Me
        MyFrmViewBuy.Show()
    End Sub

    Private Sub BarButtonItem21_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem21.ItemClick
        Dim MyFrmAddShip As New FrmAddShip
        MyFrmAddShip.MdiParent = Me
        MyFrmAddShip.Show()
    End Sub

    Private Sub BarButtonItem22_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem22.ItemClick
        Dim MyFrmViewShip As New FrmViewShip
        MyFrmViewShip.MdiParent = Me
        MyFrmViewShip.Show()
    End Sub

    Private Sub BarButtonItem23_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem23.ItemClick
        Dim MyFrmAddArrive As New FrmAddArrive
        MyFrmAddArrive.MdiParent = Me
        MyFrmAddArrive.Show()
    End Sub

    Private Sub BarButtonItem25_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem25.ItemClick
        Dim MyFrmAddStrArvl As New FrmAddStrArvl
        MyFrmAddStrArvl.MdiParent = Me
        MyFrmAddStrArvl.Show()
    End Sub

    Private Sub BarButtonItem27_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem27.ItemClick
        Dim MyFrmPrdOut As New FrmPrdOut
        MyFrmPrdOut.MdiParent = Me
        MyFrmPrdOut.Show()
    End Sub

    Private Sub BarButtonItem29_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem29.ItemClick
        Dim MyFrmFromPlr As New FrmFromPlr
        MyFrmFromPlr.MdiParent = Me
        MyFrmFromPlr.Show()
    End Sub

    Private Sub BarButtonItem31_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem31.ItemClick
        Dim MyFrmAddReceive As New FrmReceive
        MyFrmAddReceive.MdiParent = Me
        MyFrmAddReceive.Show()
    End Sub



    Private Sub BarButtonItem39_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem39.ItemClick
        Dim MyFrmShipProd As New FrmShipProd
        MyFrmShipProd.MdiParent = Me
        MyFrmShipProd.Show()
    End Sub

    Private Sub BarButtonItem41_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem41.ItemClick
        Dim MyFrmArrivalProd As New FrmArrivalProd
        MyFrmArrivalProd.MdiParent = Me
        MyFrmArrivalProd.Show()
    End Sub

    Private Sub BarButtonItem43_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem43.ItemClick
        Dim MyFrmStoreProd As New FrmStoreProd
        MyFrmStoreProd.MdiParent = Me
        MyFrmStoreProd.Show()
    End Sub

    Private Sub BarButtonItem45_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem45.ItemClick
        Dim MyFrmGoodShip As New FrmGoodShip
        MyFrmGoodShip.MdiParent = Me
        MyFrmGoodShip.Show()
    End Sub



    Private Sub BarButtonItem24_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem24.ItemClick
        Dim MyFrmViewArrive As New FrmViewArrive
        MyFrmViewArrive.MdiParent = Me
        MyFrmViewArrive.Show()
    End Sub

    Private Sub BarButtonItem26_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem26.ItemClick
        Dim MyFrmViewStrArvl As New FrmViewStrArvl
        MyFrmViewStrArvl.MdiParent = Me
        MyFrmViewStrArvl.Show()
    End Sub

    Private Sub BarButtonItem28_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem28.ItemClick
        Dim MyFrmViewPrdOut As New FrmViewPrdOut
        MyFrmViewPrdOut.MdiParent = Me
        MyFrmViewPrdOut.Show()
    End Sub

    Private Sub BarButtonItem30_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem30.ItemClick
        Dim MyFrmViewToPrd As New FrmViewToPrd
        MyFrmViewToPrd.MdiParent = Me
        MyFrmViewToPrd.Show()
    End Sub

    Private Sub BarButtonItem32_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem32.ItemClick
        Dim MyFrmViewReceive As New FrmViewReceive
        MyFrmViewReceive.MdiParent = Me
        MyFrmViewReceive.Show()
    End Sub

    Private Sub BarButtonItem40_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem40.ItemClick
        Dim MyFrmViewShpPrd As New FrmViewShpPrd
        MyFrmViewShpPrd.MdiParent = Me
        MyFrmViewShpPrd.Show()
    End Sub

    Private Sub BarButtonItem42_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem42.ItemClick
        Dim MyFrmViewArPrd As New FrmViewArPrd
        MyFrmViewArPrd.MdiParent = Me
        MyFrmViewArPrd.Show()
    End Sub

    Private Sub BarButtonItem44_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem44.ItemClick
        Dim MyFrmViewStrPrd As New FrmViewStrPrd
        MyFrmViewStrPrd.MdiParent = Me
        MyFrmViewStrPrd.Show()
    End Sub

    Private Sub BarButtonItem46_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem46.ItemClick
        Dim MyFrmViewGoodShp As New FrmViewGoodShp
        MyFrmViewGoodShp.MdiParent = Me
        MyFrmViewGoodShp.Show()
    End Sub

    Private Sub RibbonControl_Click(sender As Object, e As EventArgs) Handles RibbonControl.Click

    End Sub



    Private Sub BarButtonItem50_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem50.ItemClick
        Dim MyFrmBuyCrp As New FrmBuyCrp
        MyFrmBuyCrp.MdiParent = Me
        MyFrmBuyCrp.Show()
    End Sub

    Private Sub BarButtonItem51_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem51.ItemClick
        Dim MyFrmBuyStk As New FrmBuyStk
        MyFrmBuyStk.MdiParent = Me
        MyFrmBuyStk.Show()
    End Sub

    Private Sub BarButtonItem52_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem52.ItemClick
        Dim MyFrmArvStk As New FrmArvStk
        MyFrmArvStk.MdiParent = Me
        MyFrmArvStk.Show()
    End Sub

    Private Sub BarButtonItem53_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem53.ItemClick
        Dim MyFrmArvPrdStk As New FrmArvPrdStk
        MyFrmArvPrdStk.MdiParent = Me
        MyFrmArvPrdStk.Show()
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim MyFrmLogin As New FrmLogin

        MyFrmLogin.MdiParent = Me
        MyFrmLogin.Show()
    End Sub

    Private Sub BarButtonItem54_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem54.ItemClick
        Dim MyFrmUntExch As New FrmUntExch
        MyFrmUntExch.MdiParent = Me
        MyFrmUntExch.Show()
    End Sub

    Private Sub BarButtonItem55_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem55.ItemClick

    End Sub

    Private Sub BarButtonItem57_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem57.ItemClick
        Dim MyFrmUpldHdr As New FrmUpldHdr
        MyFrmUpldHdr.MdiParent = Me
        MyFrmUpldHdr.Show()
    End Sub

    Private Sub BarButtonItem58_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem58.ItemClick
        Dim MyFrmUpldWtr As New FrmUpldWtr
        MyFrmUpldWtr.MdiParent = Me
        MyFrmUpldWtr.Show()
    End Sub

    Private Sub BarButtonItem59_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem59.ItemClick
        Dim MyFrmClient As New FrmClient
        MyFrmClient.MdiParent = Me
        MyFrmClient.Show()
    End Sub

    Private Sub BarButtonItem60_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem60.ItemClick
        Dim MyFrmGoodStk As New FrmGoodStk
        MyFrmGoodStk.MdiParent = Me
        MyFrmGoodStk.Show()
    End Sub

    Private Sub BarButtonItem61_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem61.ItemClick
        Dim MyFrmPlrClnt As New FrmPlrClnt
        MyFrmPlrClnt.MdiParent = Me
        MyFrmPlrClnt.Show()
    End Sub

    Private Sub BarButtonItem64_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem64.ItemClick
        Dim MyFrmUpldHdr As New FrmUpldHdr
        MyFrmUpldHdr.MdiParent = Me
        MyFrmUpldHdr.Show()
    End Sub

    Private Sub BarButtonItem65_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem65.ItemClick
        Dim MyFrmUpldWtr As New FrmUpldWtr
        MyFrmUpldWtr.MdiParent = Me
        MyFrmUpldWtr.Show()
    End Sub

    Private Sub BarButtonItem66_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem66.ItemClick
        Dim MyFrmStoreMgmt As New FrmStoreMgmt
        MyFrmStoreMgmt.MdiParent = Me
        MyFrmStoreMgmt.Show()
    End Sub

    Private Sub BarButtonItem67_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem67.ItemClick
        Dim MyFrmStoreProcess As New FrmStoreProcess
        MyFrmStoreProcess.MdiParent = Me
        MyFrmStoreProcess.Show()
    End Sub

    Private Sub BarButtonItem68_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem68.ItemClick
        Dim MyFrmCash As New FrmCash
        MyFrmCash.MdiParent = Me
        MyFrmCash.Show()
    End Sub

    Private Sub BarButtonItem69_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem69.ItemClick
        Dim MyFrmViewCash As New FrmViewCash
        MyFrmViewCash.MdiParent = Me
        MyFrmViewCash.Show()
    End Sub

    Private Sub BarButtonItem70_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem70.ItemClick
        Dim MyFrmShpPlr As New FrmShpPlr
        MyFrmShpPlr.MdiParent = Me
        MyFrmShpPlr.Show()
    End Sub

    Private Sub BarButtonItem71_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem71.ItemClick
        Dim MyFrmViewShpPlr As New FrmViewShpPlr
        MyFrmViewShpPlr.MdiParent = Me
        MyFrmViewShpPlr.Show()
    End Sub

    Private Sub BarButtonItem72_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem72.ItemClick
        Dim MyFrmAddReceive As New FrmAddReceive
        MyFrmAddReceive.MdiParent = Me
        MyFrmAddReceive.Show()
    End Sub

    Private Sub BarButtonItem73_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem73.ItemClick
        Dim MyFrmViewReceive As New FrmViewReceive
        MyFrmViewReceive.MdiParent = Me
        MyFrmViewReceive.Show()
    End Sub

    Private Sub BarButtonItem74_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem74.ItemClick
        Dim MyFrmReceive As New FrmReceive
        MyFrmReceive.MdiParent = Me
        MyFrmReceive.Show()
    End Sub

    Private Sub BarButtonItem75_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem75.ItemClick
        Dim MyFrmViewClntPrd As New FrmViewClntPrd
        MyFrmViewClntPrd.MdiParent = Me
        MyFrmViewClntPrd.Show()
    End Sub

    Private Sub BarButtonItem76_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem76.ItemClick
        Dim MyFrmClntFromStr As New FrmClntFromStr
        MyFrmClntFromStr.MdiParent = Me
        MyFrmClntFromStr.Show()
    End Sub

    Private Sub BarButtonItem78_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem78.ItemClick
        Dim MyFrmClntFromPlr As New FrmClntFromPlr
        MyFrmClntFromPlr.MdiParent = Me
        MyFrmClntFromPlr.Show()
    End Sub

    Private Sub BarButtonItem77_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem77.ItemClick
        Dim MyFrmViewClntStr As New FrmViewClntStr
        MyFrmViewClntStr.MdiParent = Me
        MyFrmViewClntStr.Show()
    End Sub

    Private Sub BarButtonItem79_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem79.ItemClick
        Dim MyFrmViewClntPlr As New FrmViewClntPlr
        MyFrmViewClntPlr.MdiParent = Me
        MyFrmViewClntPlr.Show()
    End Sub

    Private Sub BarButtonItem80_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem80.ItemClick
        Dim MyFrmInPeeler As New FrmInPeeler
        MyFrmInPeeler.MdiParent = Me
        MyFrmInPeeler.Show()
    End Sub

    Private Sub BarButtonItem81_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem81.ItemClick
        Dim MyFrmUpldHdr As New FrmUpldHdr
        MyFrmUpldHdr.MdiParent = Me
        MyFrmUpldHdr.Show()
    End Sub

    Private Sub BarButtonItem82_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem82.ItemClick
        Dim MyFrmUpldWtr As New FrmUpldWtr
        MyFrmUpldWtr.MdiParent = Me
        MyFrmUpldWtr.Show()
    End Sub

    Private Sub BarButtonItem83_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem83.ItemClick
        Dim MyFrmCrpInPeeler As New FrmCrpInPeeler
        MyFrmCrpInPeeler.MdiParent = Me
        MyFrmCrpInPeeler.Show()
    End Sub

    Private Sub BarButtonItem84_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem84.ItemClick
        Dim MyFrmFindCrp As New FrmFindCrp
        MyFrmFindCrp.MdiParent = Me
        MyFrmFindCrp.Show()
    End Sub

    Private Sub BarButtonItem85_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem85.ItemClick
        Dim MyFrmFindPrd As New FrmFindPrd
        MyFrmFindPrd.MdiParent = Me
        MyFrmFindPrd.Show()
    End Sub

    Private Sub BarButtonItem86_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles BarButtonItem86.ItemClick
        Dim MyFrmUsers As New FrmUsers
        MyFrmUsers.MdiParent = Me
        MyFrmUsers.Show()
    End Sub
End Class