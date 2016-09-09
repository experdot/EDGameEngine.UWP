Imports System.Numerics
Imports Windows.UI

Namespace Global.Box2D
    Public MustInherit Class DebugDraw

        ' Methods
        Protected Sub New()
        End Sub

        Public Sub AppendFlags(ByVal flags As DebugDrawFlags)
            Me.Flags = (Me.Flags Or flags)
        End Sub

        Public Sub ClearFlags(ByVal flags As DebugDrawFlags)
            Me.Flags = (Me.Flags And Not flags)
        End Sub

        Public MustOverride Sub DrawCircle(ByVal center As Vector2, ByVal radius As Single, ByVal color As Color)

        Public MustOverride Sub DrawPolygon(ByRef vertices As FixedArray8(Of Vector2), ByVal count As Integer, ByVal color As Color)

        Public MustOverride Sub DrawSegment(ByVal p1 As Vector2, ByVal p2 As Vector2, ByVal color As Color)

        Public MustOverride Sub DrawSolidCircle(ByVal center As Vector2, ByVal radius As Single, ByVal axis As Vector2, ByVal color As Color)

        Public MustOverride Sub DrawSolidPolygon(ByRef vertices As FixedArray8(Of Vector2), ByVal count As Integer, ByVal color As Color)

        Public MustOverride Sub DrawXForm(ByRef xf As XForm)


        ' Properties
        Public Property Flags As DebugDrawFlags

    End Class
End Namespace

