Imports Microsoft.Toolkit.Uwp.UI.Controls

Partial Public Class MainPage
    Inherits Page
    ''' <summary>
    ''' 情景集
    ''' </summary>
    Public Scenarios As New HamburgerMenuItemCollection From {
        New HamburgerMenuGlyphItem() With {
            .Label = "Start",
            .Glyph = "/Assets/MenuImages/Home.png",
            .TargetPageType = GetType(Scenario1_Start)
        },
        New HamburgerMenuGlyphItem() With {
            .Label = "Visuals",
            .Glyph = "/Assets/MenuImages/Object.png",
            .TargetPageType = GetType(Scenario2_Visuals)
        },
        New HamburgerMenuGlyphItem() With {
            .Label = "Compnents",
            .Glyph = "/Assets/MenuImages/Compnent.png",
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
