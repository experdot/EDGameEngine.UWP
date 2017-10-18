Imports System.Numerics
Imports EDGameEngine.Core
''' <summary>
''' 树节点
''' </summary>
Public Class TreeNode
    Public Property Location As Vector2
    Public Property RealLoc As Vector2
    Public Property Length As Single
    Public Property Rank As Integer
    Public Property ParentNode As TreeNode
    Public Property Children As New List(Of TreeNode）
    Public Property HasFlower As Boolean
    Public Property FlowerSize As Single
    Public Property MidRotateAngle As Single
    Public Property Percent As Single
    Public Property DiePercent As Single
    Public Property GrowEnergy As Single
    Public Property LineShape As New List(Of Vector2)
    Public Sub New(Loc As Vector2, Len As Single, Rk As Integer)
        Location = Loc
        Location.SetMag(Len)
        Length = Len
        Rank = Rk
    End Sub
End Class
