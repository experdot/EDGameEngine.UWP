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
        VelocityUpon = 8.0F
    End Sub

    ''' <summary>
    ''' 分裂
    ''' </summary>
    Public Sub Divide(particals As List(Of SpotParticle), Optional count As Integer = 2)
        If count < 2 Then count = 2
        Dim newSize As Single = Size * 0.7F

        Me.Size = newSize
        For i = 2 To count
            particals.Add(New SpotParticle(Location) With {.Color = GetRandomColor(), .Size = newSize})
            particals.Last.Velocity = Velocity.RotateNew(CSng(Rnd.NextDouble() * 0.2F - 0.1F) * 10.0F) * 0.62F
        Next

    End Sub

    ''' <summary>
    ''' 随机分裂
    ''' </summary>
    Public Sub DivideRandom(particals As List(Of SpotParticle), Optional count As Integer = 2)
        If count < 2 Then count = 2
        If Rnd.NextDouble < 0.03F AndAlso Size > 1.0F Then
            Divide(particals, count)
        End If
        If Size < 0.1F Then
            Velocity = Vector2.Zero
            Me.Finalize()
        End If
    End Sub
    Public Function GetRandomColor() As Color
        Dim upon As Integer = 255
        Dim uu As Integer = 2
        Dim r As Integer = CInt(Color.R) + uu 'Rnd.Next(-0, 1) * Rnd.Next(1, 10)
        Dim g As Integer = CInt(Color.G) + uu 'Rnd.Next(-0, 1) * Rnd.Next(1, 10)
        Dim b As Integer = CInt(Color.B) + uu 'Rnd.Next(-0, 1) * Rnd.Next(1, 10)
        If r < 0 Then r = 0
        If g < 0 Then g = 0
        If b < 0 Then b = 0
        If r > upon Then r = upon
        If g > upon Then g = upon
        If b > upon Then b = upon
        'Return Color.FromArgb(255, CByte(Rnd.Next(256)), CByte(Rnd.Next(256)), CByte(Rnd.Next(256)))
        Return Color.FromArgb(255, CByte(r), CByte(g), CByte(b))
    End Function

    Public Sub Update()
        Static count As Integer = 40
        If count > 0 Then
            count -= 1
            Size = Size * 0.99F
            Velocity = Velocity.RotateNew(AngleOffset * 0.05F)
            Color = GetRandomColor()
            Me.ApplyForce(PhysicUnit.VecAll(Rnd.Next(8)) * 2)
            Move()
        End If
    End Sub

End Class
