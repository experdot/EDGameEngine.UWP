Imports System.Numerics
Imports Windows.UI
Public Class ParticalManager
    Inherits GameVisualModel
    Public Overrides Property Presenter As GameView = New ParticalView(Me)
    Const WAlKERNUM As Single = 100 '粒子数量
    Public Shared Rnd As New Random
    Public Particals As List(Of Partical)
    Dim Vectors() As Vector2 = {
        New Vector2(0, 1),
        New Vector2(0, -1),
        New Vector2(-1, 0),
        New Vector2(1, 0),
        New Vector2(0.7, 0.71),
        New Vector2(0.7, -0.7),
        New Vector2(-0.7, 0.71),
        New Vector2(-0.7, -0.7)
    }
    Public Sub New(se As Scene)
        Particals = New List(Of Partical)
        For i = 0 To WAlKERNUM - 1
            AddPartical(New Vector2(se.Width * Rnd.NextDouble, se.Height * Rnd.NextDouble))
            Particals(i).Mass = 1 + Rnd.NextDouble * 99
            Particals(i).Size = 0.1 + Rnd.NextDouble * 2
            Particals(i).ImageSize = 2 + Rnd.NextDouble * 2
            Particals(i).Color = Color.FromArgb(255, 255, 255, 255)
        Next

    End Sub
    ''' <summary>
    ''' 更新
    ''' </summary>
    Public Overrides Sub Update()
        If Transform.Scale.Length > 3 Then
            Transform.Scale = New Vector2()
        Else
            Transform.Scale += New Vector2(0.01, 0.01)
        End If
        Transform.Center = New Vector2(Scene.Width / 2, Scene.Height / 2)
        Transform.Rotation = (Transform.Rotation + 0.005) Mod （Math.PI * 2)
        Dim CenterVec As New Vector2(Scene.Width / 2, Scene.Height / 2)
        Dim RectLength As Single = Math.Sqrt(Scene.Width * Scene.Width / 4 + Scene.Height * Scene.Height / 4)
        For Each SubEle In Particals
            SubEle.ApplyForce(Vectors(Rnd.Next(8)) * 2)
            'SubPartical.ApplyForce(New Vector2(2 * Rnd.NextDouble, 0))
            If Particals.IndexOf(SubEle) = 0 Then SubEle.ApplyForce(New Vector2(World.MouseX - SubEle.Location.X, World.MouseY - SubEle.Location.Y))
            If (SubEle.Location - CenterVec).Length > RectLength Then
                SubEle.StartNew(New Vector2(Scene.Width * Rnd.NextDouble, Scene.Height * Rnd.NextDouble))
                SubEle.Age = 0
            End If
            SubEle.Move()
            SubEle.Age += 0.1
        Next
    End Sub
    ''' <summary>
    ''' 新建粒子
    ''' </summary>
    ''' <param name="loc"></param>
    Private Sub AddPartical(loc As Vector2)
        Particals.Add(New Partical(loc))
    End Sub
End Class


