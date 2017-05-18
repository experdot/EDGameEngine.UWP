Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 光点粒子
''' </summary>
Public Class SpotPartical
    Inherits DynamicPartical

    Private AngleOffset As Single = 0.0F
    Public Sub New(loc As Vector2)
        MyBase.New(loc)

        AngleOffset = CSng(Rnd.NextDouble() * 0.2F - 0.1F)
        VelocityUpon = 8.0F
    End Sub

    ''' <summary>
    ''' 分裂
    ''' </summary>
    Public Sub Divide(particals As List(Of SpotPartical), Optional count As Integer = 2)

        If count > 1 Then
            Dim newSize As Single = Size * 0.7F

            Me.Size = newSize
            For i = 2 To count
                particals.Add(New SpotPartical(Location) With {.Color = GetRandomColor(), .Size = newSize})
                particals.Last.Velocity = Velocity.RotateNew(CSng(Rnd.NextDouble() * 0.2F - 0.1F) * 10.0F) * 0.62F
            Next
        End If
    End Sub

    ''' <summary>
    ''' 随机分裂
    ''' </summary>
    Public Sub DivideRandom(particals As List(Of SpotPartical), Optional count As Integer = 2)
        If count > 1 Then
            If Rnd.NextDouble < 0.01F AndAlso Size > 1.0F Then
                Divide(particals, count)
            Else
                Size = Size * 0.9976F
                Velocity = Velocity.RotateNew(AngleOffset * 0.2F)
                Color = GetRandomColor()
            End If
            If Size < 1.3F Then
                Velocity = Vector2.Zero
            End If
        End If
    End Sub
    Public Function GetRandomColor() As Color
        Dim upon As Integer = 255
        Dim r As Integer = CInt(Color.R) + Rnd.Next(-0, 2) * Rnd.Next(1, 3)
        Dim g As Integer = CInt(Color.G) + Rnd.Next(-0, 2) * Rnd.Next(1, 3)
        Dim b As Integer = CInt(Color.B) + Rnd.Next(-0, 2) * Rnd.Next(1, 3)
        If r < 0 Then r = 0
        If g < 0 Then g = 0
        If b < 0 Then b = 0
        If r > upon Then r = upon
        If g > upon Then g = upon
        If b > upon Then b = upon
        'Return Color.FromArgb(255, CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256)))
        Return Color.FromArgb(255, CByte(r), CByte(g), CByte(b))
    End Function

End Class
