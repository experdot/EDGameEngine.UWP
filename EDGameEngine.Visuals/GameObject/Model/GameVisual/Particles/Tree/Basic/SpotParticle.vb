Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
''' <summary>
''' 可分裂的光点粒子
''' </summary>
Public Class SpotParticle
    Inherits DynamicParticle

    Private AngleOffset As Single = 0.0F
    Public Sub New(loc As Vector2)
        MyBase.New(loc)

        AngleOffset = CSng(Rnd.NextDouble() * 0.2F - 0.1F)
        VelocityUpon = 16.0F
    End Sub

    ''' <summary>
    ''' 分裂
    ''' </summary>
    Public Sub Divide(particals As List(Of SpotParticle), Optional count As Integer = 2)
        If count < 2 Then count = 2
        Dim newSize As Single = Size * 0.806F

        Me.Size = newSize

        'particals.Add(New SpotParticle(Location) With {.Color = GetRandomColor(), .Size = newSize})
        'particals.Last.Velocity = Velocity * 0.62F

        For i = 2 To count
            particals.Add(New SpotParticle(Location) With {.Color = GetRandomColor(), .Size = newSize})
            particals.Last.Velocity = Velocity.RotateNew(CSng(RandomHelper.NextNorm(-10000, 10000, 6) / 6180)) * 0.62F
        Next

    End Sub

    ''' <summary>
    ''' 随机分裂
    ''' </summary>
    Public Sub DivideRandom(particals As List(Of SpotParticle), Optional count As Integer = 2)
        If count < 2 Then count = 2
        If Rnd.NextDouble < 0.03F AndAlso Size > 0.8F Then
            Divide(particals, count)
        End If
        If Size < 0.8F Then
            Velocity = Vector2.Zero
            Me.Location = New Vector2(-1, -1)
        End If
    End Sub
    Public Function GetRandomColor() As Color
        Dim upon As Integer = 255
        Dim uu As Single = 1.0F
        ' Dim a As Integer = 255 ' CInt(Color.A) + RandomHelper.NextNorm(-3, 4) * Rnd.Next(1, 10)
        Static r As Single = CInt(Color.R) '+ RandomHelper.NextNorm(-3, 4) * Rnd.Next(1, 10)
        Static g As Single = CInt(Color.G) '+ RandomHelper.NextNorm(-3, 4) * Rnd.Next(1, 10)
        Static b As Single = CInt(Color.B) '+ RandomHelper.NextNorm(-3, 4) * Rnd.Next(1, 10)

        r += uu
        g += uu
        b += uu

        'If a < 1 Then a = 1
        If r < 0 Then r = 0
        If g < 0 Then g = 0
        If b < 0 Then b = 0

        'If a > upon Then a = upon
        If r > upon Then r = upon
        If g > upon Then g = upon
        If b > upon Then b = upon

        Return Color.FromArgb(CByte(255), CByte(r), CByte(g), CByte(b))
    End Function

    Public Sub Update()
        Static count As Integer = 60
        If count > 0 Then
            count -= 1
            Size = Size * 0.995F
            Velocity = Velocity.RotateNew(AngleOffset * 0.05F)
            Velocity = Velocity.RotateNew(CSng(RandomHelper.NextNorm(-100, 100) / 1000))
            Color = GetRandomColor()
            Move()
        End If
    End Sub


End Class
