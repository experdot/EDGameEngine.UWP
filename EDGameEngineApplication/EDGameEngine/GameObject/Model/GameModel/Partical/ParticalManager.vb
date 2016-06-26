﻿Imports System.Numerics
Imports Windows.UI
Public Class ParticalManager
    Inherits GameVisualModel
    Public Overrides Property Presenter As GameView = New ParticalView(Me)
    Const WAlKERNUM As Single = 150 '粒子数量
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
    Public Sub New()
        Particals = New List(Of Partical)
        For i = 0 To WAlKERNUM - 1
            AddPartical(New Vector2(WorldSpace.SpaceWidth * Rnd.NextDouble, WorldSpace.SpaceHeight * Rnd.NextDouble))
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
        Rotation = (Rotation + 0.001) Mod （Math.PI * 2)
        Dim CenterVec As New Vector2(WorldSpace.SpaceWidth / 2, WorldSpace.SpaceHeight / 2)
        Dim RectLength As Single = Math.Sqrt(WorldSpace.SpaceWidth * WorldSpace.SpaceWidth / 4 + WorldSpace.SpaceHeight * WorldSpace.SpaceHeight / 4)
        For Each SubPartical In Particals
            SubPartical.ApplyForce(Vectors(Rnd.Next(8)) * 2)
            SubPartical.ApplyForce(New Vector2(2 * Rnd.NextDouble, 0))
            If (SubPartical.Location - CenterVec).Length > RectLength Then
                SubPartical.StartNew(New Vector2(WorldSpace.SpaceWidth * 0, WorldSpace.SpaceHeight * Rnd.NextDouble))
                SubPartical.Age = 0
            End If
            SubPartical.Move()
            SubPartical.Age += 0.1
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

