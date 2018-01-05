Imports Microsoft.Toolkit.Uwp.UI.Controls

Partial Public Class MainPage
    Inherits Page
    ''' <summary>
    ''' 情景集
    ''' </summary>
    Public Scenarios As New HamburgerMenuItemCollection From {
        New HamburgerMenuImageItem() With {
            .Label = "Start",
            .Thumbnail = New BitmapImage(New Uri("ms-appx:///Assets/MenuImages/Home.png")),
            .TargetPageType = GetType(Scenario1_Start)
        },
        New HamburgerMenuImageItem() With {
            .Label = "Visuals",
            .Thumbnail = New BitmapImage(New Uri("ms-appx:///Assets/MenuImages/Object.png")),
            .TargetPageType = GetType(Scenario2_Visuals)
        },
        New HamburgerMenuImageItem() With {
            .Label = "Compnents",
            .Thumbnail = New BitmapImage(New Uri("ms-appx:///Assets/MenuImages/Compnent.png")),
            .TargetPageType = GetType(Scenario3_Compnents)
        }
    }
    ''' <summary>
    ''' 选项卡集
    ''' </summary>
    Public Options As New HamburgerMenuItemCollection From {
        New HamburgerMenuGlyphItem() With {
            .Label = "Help",
            .Glyph = "",
            .TargetPageType = GetType(Scenario4_Help)
        }
    }
End Class
