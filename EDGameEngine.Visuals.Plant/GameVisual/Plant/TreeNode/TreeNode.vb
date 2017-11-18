Imports System.Numerics
Imports EDGameEngine.Core.Utilities
''' <summary>
''' 树节点
''' </summary>
Public Class TreeNode
    ''' <summary>
    ''' 相对位置
    ''' </summary>
    Public Property Location As Vector2
    ''' <summary>
    ''' 绝对位置
    ''' </summary>
    Public Property RealLocation As Vector2
    ''' <summary>
    ''' 长度
    ''' </summary>
    Public Property Length As Single
    ''' <summary>
    ''' 层级
    ''' </summary>
    Public Property Rank As Integer
    ''' <summary>
    ''' 父级
    ''' </summary>
    Public Property Parent As TreeNode
    ''' <summary>
    ''' 子集
    ''' </summary>
    Public Property Children As New List(Of TreeNode)
    ''' <summary>
    ''' 花集
    ''' </summary>
    Public Property Flowers As New List(Of Flower)
    ''' <summary>
    ''' 中折角度
    ''' </summary>
    Public Property MidRotateAngle As Single
    ''' <summary>
    ''' 生长比例
    ''' </summary>
    Public Property Percent As Single
    ''' <summary>
    ''' 凋零比例
    ''' </summary>
    Public Property DiePercent As Single

    Public Shared Property Rnd As New Random

    ''' <summary>
    ''' 创建并初始化一个实例
    ''' </summary>
    Public Sub New(location As Vector2, length As Single, rank As Integer)
        Me.Location = location
        Me.Location.SetMag(length)
        Me.Length = length
        Me.Rank = rank
        Me.MidRotateAngle = CSng(Rnd.NextDouble)
    End Sub

    ''' <summary>
    ''' 添加子节点
    ''' </summary>
    Public Sub AddChild(node As TreeNode)
        node.Parent = Me
        Children.Add(node)
    End Sub
    ''' <summary>
    ''' 添加花朵
    ''' </summary>
    Public Sub AddFlower(flower As Flower)
        flower.Parent = Me
        Flowers.Add(flower)
    End Sub
    ''' <summary>
    ''' 创建子树
    ''' </summary>
    Public Sub Generate()
        'Dim newRank As Integer = Rank - 1
        'If newRank > 0 Then
        '    Dim newLocation As Vector2
        '    Dim newLength As Single
        '    newLocation = Me.Location.RotateNew(CSng(Rnd.NextDouble * Math.PI / 2 - Math.PI / 4) / Math.Log((Rank + 10), 10))
        '    newLength = CSng(Rank * 25 * Rnd.NextDouble)
        '    Dim immediate As New TreeNode(newLocation, newLength, newRank)
        '    immediate.Generate()
        '    AddChild(immediate)
        '    For i = 0 To Rnd.Next(1, Math.Max(CInt((12 - Rank) / 2), 1)） - 1
        '        newLocation = Me.Location.RotateNew(CSng(Rnd.NextDouble * Math.PI / 2 - Math.PI / 4) / Math.Log((Rank + 10), 10)）
        '        newLength = CSng(Rank * 25 * Rnd.NextDouble)
        '        Dim child As New TreeNode(newLocation, newLength, newRank)
        '        child.Generate()
        '        AddChild(child)
        '    Next
        'End If
        Dim newRank As Integer = Rank - 1
        If newRank > 0 Then
            Dim newLocation As Vector2
            Dim newLength As Single
            newLocation = Me.Location.RotateNew(CSng(Rnd.NextDouble * Math.PI / 2 - Math.PI / 4))
            newLength = CSng(Me.Length * (0.618F + Rnd.NextDouble * 0.24F))
            Dim immediate As New TreeNode(newLocation, newLength, newRank)
            immediate.Generate()
            AddChild(immediate)

            Dim extra As Integer = Math.Min(3, RandomHelper.NextNorm(0, Math.Max(CInt((12 - Rank) / 4), 1)))
            If extra > 0 Then
                For i = 0 To extra - 1
                    newLocation = Me.Location.RotateNew(CSng(Rnd.NextDouble * Math.PI / 1.5 - Math.PI / 3)）
                    newLength = CSng(Me.Length * (0.618F + Rnd.NextDouble * 0.24F))
                    Dim child As New TreeNode(newLocation, newLength, newRank)
                    child.Generate()
                    AddChild(child)
                Next
            End If
        End If
    End Sub
    ''' <summary>
    ''' 树成长
    ''' </summary>
    Public Sub GrowUp(increment As Single, Optional ratio As Single = 0.19)
        Static IsBeginDie As Boolean = False
        If Percent < 1 Then
            Percent += CSng(increment * Rnd.NextDouble + 0.001F)
            If Percent >= 1 AndAlso Rank < 2 Then
                IsBeginDie = True
            End If
        Else
            If IsBeginDie = True Then
                DiePercent += increment
                If DiePercent > Math.PI * 2 Then
                    DiePercent = 0
                End If
            End If
        End If
        If Percent > ratio Then
            If Children.Count > 0 Then
                For i = 0 To Children.Count - 1
                    Dim child As TreeNode = Children(i)
                    child.GrowUp(increment)
                Next
            End If
        End If
        If Percent > 0.95 Then
            If Flowers.Count > 0 Then
                For i = 0 To Flowers.Count - 1
                    Flowers(i).Grow()
                Next
            Else
                If Rank < 6 AndAlso Rnd.NextDouble > 0.95 Then
                    AddFlower(New Flower)
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' 摇动树
    ''' </summary>
    Public Sub Wave(angle As Single, Optional ratio As Single = 1.618)
        If Children.Count > 0 Then
            For i = 0 To Children.Count - 1
                Dim child As TreeNode = Children(i)
                child.Location.Rotate(angle * child.Location.Y / child.Location.Length)
                child.Wave(angle * ratio)
            Next
        End If
    End Sub
End Class
