' https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

''' <summary>
''' 可用于自身或导航至 Frame 内部的空白页。
''' </summary>
Public NotInheritable Class Scenario2_Visuals
    Inherits Page

    Public Sub New()

        ' 此调用是设计器所必需的。
        InitializeComponent()

        ' 在 InitializeComponent() 调用之后添加任何初始化。

        GalleryBox.ViewModel = New GalleryVisuals
    End Sub
End Class
