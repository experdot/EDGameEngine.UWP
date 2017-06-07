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
        Dim newSize As Single = Size * 0.85F

        Me.Size = newSize
        Me.Age = RandomHelper.NextNorm(0, 60)
        Me.AngleOffset = CSng(Rnd.NextDouble() * 0.2F - 0.1F)

        If count > 1 Then
            For i = 2 To count
                newSize *= 0.9F
                particals.Add(New SpotParticle(Location) With {.Color = GetRandomColor(), .Size = newSize})
                particals.Last.Velocity = Velocity.RotateNew(CSng(Rnd.Next(-100, 100) * 0.01F)) * 0.618F
                particals.Last.Age = RandomHelper.NextNorm(0, 40)
            Next
        End If

        'Me.Velocity = Velocity.RotateNew(CSng(Rnd.Next(-100, 100) * 0.01F)) * 0.618F
    End Sub

    Public Function GetRandomColor() As Color
        Dim upon As Integer = 255
        Dim uu As Single = 1.0F
        Dim a As Single = CInt(Color.A) '+ RandomHelper.NextNorm(-3, 4) * Rnd.Next(1, 10)
        Dim r As Single = CInt(Color.R) '+ RandomHelper.NextNorm(-3, 4) * Rnd.Next(1, 10)
        Dim g As Single = CInt(Color.G) '+ RandomHelper.NextNorm(-3, 4) * Rnd.Next(1, 10)
        Dim b As Single = CInt(Color.B) '+ RandomHelper.NextNorm(-3, 4) * Rnd.Next(1, 10)

        If r > 250 Then a += -1.0F
        r += uu
        g += uu
        b += uu

        If a < 1 Then a = 1
        If r < 0 Then r = 0
        If g < 0 Then g = 0
        If b < 0 Then b = 0

        If a > upon Then a = upon
        If r > upon Then r = upon
        If g > upon Then g = upon
        If b > upon Then b = upon

        Return Color.FromArgb(CByte(a), CByte(r), CByte(g), CByte(b))
    End Function
    Public Sub Update(particals As List(Of SpotParticle), Optional count As Integer = 2)
        If Age > 0 Then
            Age -= 1
            Size = Size * 0.995F
            Velocity = Velocity.RotateNew(AngleOffset * 0.3F)
            Velocity = Velocity.RotateNew(CSng(RandomHelper.NextNorm(-100, 100) * 0.005))
            Color = GetRandomColor()
            Move()
        Else
            'Divide(particals, count)
        End If

        If Rnd.NextDouble < 0.03F AndAlso Size > 1.0F Then
            Divide(particals, count)
        End If
        If Size < 1.0F Then
            Velocity = Vector2.Zero
            Me.Location = New Vector2(-1, -1)
            Me.IsDead = True
        End If
    End Sub


End Class
