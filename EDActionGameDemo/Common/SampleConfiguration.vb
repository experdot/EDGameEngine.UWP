Imports Microsoft.Toolkit.Uwp.UI.Controls

Partial Public Class MainPage
    Inherits Page
    ''' <summary>
    ''' 情景集
    ''' </summary>
    Public Scenarios As New HamburgerMenuItemCollection From {
        New HamburgerMenuGlyphItem() With {
            .Label = "Start",
            .Glyph = "/Assets/MenuImages/Coffee.png",
            .TargetPageType = GetType(Scenario1_Start)
        },
        New HamburgerMenuGlyphItem() With {
            .Label = "Game",
            .Glyph = "/Assets/MenuImages/Fish.png",
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
