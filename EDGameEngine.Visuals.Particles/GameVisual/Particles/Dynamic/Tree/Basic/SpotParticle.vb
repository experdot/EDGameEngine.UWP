Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Utilities
Imports Windows.UI
''' <summary>
''' 可分裂的光点粒子
''' </summary>
Public Class SpotParticle
    Inherits DynamicParticle

    Public Property AngleOffset As Single = 0.0F

    Public Property SizeMinimum As Single = 1.0F
    Public Property SizeRadius As Single = 0.995

    Public Property RotateRadius As Single = 0.01F
    Public Property RotateRadiusRandom As Single = 0.001F

    Public Property DivideRadius As Single = 0.03F
    Public Property DivideSizeRadius As Single = 0.8F
    Public Property DivideSizeRadiusEx As Single = 0.92F
    Public Property DivideColorIncrement As Single = 0.9F

    Public Sub New(loc As Vector2)
        MyBase.New(loc)
        AngleOffset = CSng(Rnd.NextDouble() * 0.03F - 0.015F)
        VelocityUpon = 16.0F
    End Sub
    Public Sub Update(particles As List(Of SpotParticle), Optional count As Integer = 2)
        If Age > 0 Then
            Age -= 1
            Size = Size * SizeRadius
            Velocity = Velocity.RotateNew(AngleOffset * RotateRadius)
            Velocity = Velocity.RotateNew(CSng(RandomHelper.NextNorm(-100, 100) * RotateRadiusRandom))
            Color = GetGradientColor()
            VelocityUpon = Size * 0.38F
            Move()
        End If

        If Size > SizeMinimum Then
            If Rnd.NextDouble < DivideRadius Then
                Divide(particles, count)
            End If
        Else
            If Size < 1.0F Then
                Velocity = Vector2.Zero
                Me.Location = New Vector2(-1, -1)
                Me.IsDead = True
            End If
        End If
    End Sub
    ''' <summary>
    ''' 分裂
    ''' </summary>
    Public Sub Divide(particles As List(Of SpotParticle), Optional count As Integer = 2)
        If count < 2 Then count = 2
        Dim newSize As Single = Size * DivideSizeRadius

        Me.Size = newSize
        Me.Age = RandomHelper.NextNorm(0, 30)
        Me.AngleOffset = CSng(Rnd.NextDouble() * 0.02F - 0.01F)

        If count > 1 Then
            For i = 2 To count
                newSize *= DivideSizeRadiusEx
                Dim particle = New SpotParticle(Location) With {.Color = GetDivideColor(), .Size = newSize}
                particle.Velocity = Velocity.RotateNew(CSng(Rnd.Next(-100, 100) * 0.01F)) * 0.618F
                particle.Age = RandomHelper.NextNorm(0, 40)
                particles.Add(particle)
            Next
        End If
    End Sub

    Public Function GetGradientColor(Optional increment As Single = 1.0F) As Color
        Dim upon As Integer = 255
        Static R As Single = CInt(Color.R)
        Static G As Single = CInt(Color.G)
        Static B As Single = CInt(Color.B)

        R = IncreaseNumber(R, increment, 0, upon)
        G = IncreaseNumber(G, increment, 0, upon)
        B = IncreaseNumber(B, increment, 0, upon)

        Return Color.FromArgb(Color.A, CByte(R), CByte(G), CByte(B))
    End Function

    Public Function GetDivideColor(Optional increment As Single = 1.0F) As Color
        Dim upon As Integer = 255
        Dim len = Velocity.Length * 5
        Dim half = len / 2

        Dim r As Single = CInt(Color.R)
        Dim g As Single = CInt(Color.G)
        Dim b As Single = CInt(Color.B)

        r = IncreaseNumber(r, increment * CSng(Rnd.NextDouble * len - half), 0, upon)
        g = IncreaseNumber(g, increment * CSng(Rnd.NextDouble * len - half), 0, upon)
        b = IncreaseNumber(b, increment * CSng(Rnd.NextDouble * len - half), 0, upon)

        Return Color.FromArgb(Color.A, CByte(r), CByte(g), CByte(b))
    End Function

    Private Function IncreaseNumber(number As Single, increment As Single, Optional min As Single = Single.MinValue, Optional max As Single = Single.MaxValue) As Single
        Dim value As Single = number
        value += increment
        value = Math.Min(max, Math.Max(min, value))
        Return value
    End Function
End Class
