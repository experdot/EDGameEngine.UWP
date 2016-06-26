Imports System.Numerics
Imports Windows.UI

Public Class Blocker
    Public ParentSpace As BaseSpace
    Public Property Location As Vector2
    Public Property Mass As Single = 10 '质量
    Public Property myColor As Color
    Public Property StepPointList As New List(Of Point)
    Public Property IsAdd As Integer = 1 '放大或缩小的方向，-1为缩小
    Public Property StrIndex As Integer = 0 '临时测试，要显示字的索引
    Shared rand As New Random
    Public ReadOnly Property imageRec As Rect
        Get
            Return New Rect(Location.X - Mass / 2, Location.Y - Mass / 2, Mass, Mass)
        End Get
    End Property
    Public Sub New(ByVal nSpace As BaseSpace, ByVal LocationX As Single, ByVal LocationY As Single)
        ParentSpace = nSpace
        Location = New Vector2(LocationX, LocationY)
        StrIndex = rand.NextDouble * 36
    End Sub
End Class
