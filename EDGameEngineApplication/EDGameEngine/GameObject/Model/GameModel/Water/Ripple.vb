Imports System.Numerics
Imports Microsoft.Graphics.Canvas

Public Class Ripple
    Inherits GameVisualModel
    Public Property Buffer1 As Integer()
    Public Property Buffer2 As Integer()
    Public Property Width As Integer
    Public Property Height As Integer
    Public Property Image As CanvasBitmap
        Set(value As CanvasBitmap)
            m_Image = value
            Width = m_Image.Bounds.Width
            Height = m_Image.Bounds.Height
            ReDim Buffer1(Width * Height - 1)
            ReDim Buffer2(Width * Height - 1)
        End Set
        Get
            Return m_Image
        End Get
    End Property
    Private m_Image As CanvasBitmap
    Public Overrides Sub Start()

    End Sub
    Public Overrides Sub Update()
        Dim pos As Vector2 = New Vector2(World.MouseX, World.MouseY) - Transform.Position
        Transform.Position = New Vector2(Scene.Width / 2 - Width / 2, Scene.Height / 2 - Height / 2)
        DropSton(pos.X, pos.Y, 5, Rnd.Next(64, 84))
        RippleSpread()
    End Sub
    ''' <summary>
    ''' 投放波源
    ''' </summary>
    Public Sub DropSton(x As Integer, y As Integer, Optional SouSize As Integer = 6, Optional SouWeight As Integer = 64)
        If x + SouSize > Width OrElse y + SouSize > Height Or x - SouSize < 0 Or y - SouSize < 0 Then Return
        For posx = x - SouSize To x + SouSize
            For posy = y - SouSize To y + SouSize
                If ((posx - x) * (posx - x) + (posy - y) * (posy - y) < SouSize * SouSize) Then
                    Buffer1(Width * posy + posx) = -SouWeight '波源投放，其实就是在波源直径区域将Buff数组赋初始值，以便进一步产生水波
                End If
            Next
        Next
    End Sub
    ''' <summary>
    ''' 波源扩散
    ''' </summary>
    Public Sub RippleSpread()
        For i = Width To Width * Height - Width - 1
            Buffer2(i) = ((Buffer1(i - 1) + Buffer1(i + 1) + Buffer1(i - Width) + Buffer1(i + Width)) >> 1) - Buffer2(i)
            Buffer2(i) -= Buffer2(i) >> 4 '波幅衰减
        Next
        Dim tmp As Integer() = Buffer1 '这里开始交换buff1和buff2，是为了让波源能继续传播下去
        Buffer1 = Buffer2
        Buffer2 = tmp
    End Sub
End Class
