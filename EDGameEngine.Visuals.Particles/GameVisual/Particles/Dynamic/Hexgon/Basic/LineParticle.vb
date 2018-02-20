Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Utilities
Imports Windows.UI
''' <summary>
''' 可分裂的粒子
''' </summary>
Public Class LineParticle
    Inherits DynamicParticle

    Private AngleOffset As Single = 0.0F
    Public Sub New(loc As Vector2)
        MyBase.New(loc)

        AngleOffset = 1.0F 'CSng(Rnd.NextDouble() * 0.2F - 0.1F)
        VelocityUpon = 16.0F
        Age = 40
    End Sub

    ''' <summary>
    ''' 分裂
    ''' </summary>
    Public Sub Divide(particles As List(Of LineParticle))
        Static rotate As Single = Math.PI / 3
        Dim newSize As Single = Size

        particles.Add(New LineParticle(Location) With {.Color = Me.Color(), .Size = newSize})
        particles.Last.Velocity = Velocity.RotateNew(rotate) * 0.618F

        Me.Size = newSize
        Me.Age = 40
        Me.Velocity = Velocity.RotateNew(-rotate) * 0.618F

    End Sub

    Public Function GetRandomColor() As Color
        Dim upon As Integer = 255
        Dim uu As Single = 2.0F
        Static a As Single = CInt(Color.A) '+ RandomHelper.NextNorm(-3, 3) * Rnd.Next(1, 10)
        Static r As Single = CInt(Color.R) '+ CSng(RandomHelper.NextNorm(-30, 30) / 10 * Rnd.Next(1, 10))
        Static g As Single = CInt(Color.G) '+ CSng(RandomHelper.NextNorm(-30, 30) / 10 * Rnd.Next(1, 10))
        Static b As Single = CInt(Color.B) '+ CSng(RandomHelper.NextNorm(-30, 30) / 10 * Rnd.Next(1, 10))

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

    Public Sub Update(particles As List(Of LineParticle))
        If Age > 0 Then
            Age -= 1
            Size = Size * 0.992F
            'Velocity = Velocity * 0.99F
            Velocity = Velocity.RotateNew(AngleOffset * 0.06F)
            Velocity = Velocity.RotateNew(CSng(RandomHelper.NextNorm(-100, 100) / 1000))
            Color = GetRandomColor()
            Move()
        ElseIf Age = 0 AndAlso Size > 0.5F Then
            Divide(particles)
            Age -= 1
        End If
        'If Age > 0 AndAlso Size < 0.5F Then
        '    Age = -1
        '    Velocity = Vector2.Zero
        '    Me.Location = New Vector2(-1, -1)
        'End If
    End Sub


End Class
