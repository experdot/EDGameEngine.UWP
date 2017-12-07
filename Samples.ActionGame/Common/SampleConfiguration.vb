Imports Microsoft.Toolkit.Uwp.UI.Controls

Partial Public Class MainPage
    Inherits Page
    ''' <summary>
    ''' 情景集
    ''' </summary>
    Public Scenarios As New HamburgerMenuItemCollection From {
        New HamburgerMenuImageItem() With {
            .Label = "Start",
            .Thumbnail = New BitmapImage(New Uri("ms-appx:///Assets/MenuImages/Coffee.png")),
            .TargetPageType = GetType(Scenario1_Start)
        },
        New HamburgerMenuImageItem() With {
            .Label = "Game",
               .Thumbnail = New BitmapImage(New Uri("ms-appx:///Assets/MenuImages/Fish.png")),
            .TargetPageType = GetType(Scenario2_Game)
        }
    }
    ''' <summary>
    ''' 选项卡集
    ''' </summary>
    Public Options As New HamburgerMenuItemCollection From {
        New HamburgerMenuGlyphItem() With {
            .Label = "Help",
            .Glyph = "",
            .TargetPageType = GetType(Scenario3_Help)
        }
    }
End Class
