Imports System.Numerics
Imports EDGameEngine.Core
Imports EDGameEngine.Core.Utilities
Imports EDGameEngine.Visuals
''' <summary>
''' 植物模型
''' </summary>
Public Class Plant
    Inherits GameBody
    Implements IPlant
    Public Property Root As TreeNode Implements IPlant.Root
    Public Property IsBeginDie As Boolean


    Public Sub New(loc As Vector2, Optional Rank As Integer = 8)
        Root = New TreeNode(New Vector2(0, -100), 100, 1)
        Root.RealLoc = loc
        CreateTree(Root, Rank)
    End Sub
    Public Overrides Sub StartEx()
    End Sub
    Public Overrides Sub UpdateEx()
        Static TempSingle, Ts2 As Single
        'Transform.Center = Root.RealLoc
        'Transform.Scale = New Vector2(CSng(0.95 + Math.Sin(Ts2) * 0.05）, 1.0F)
        Ts2 = CSng((Ts2 + 0.04) Mod (Math.PI * 2)）

        Root.RealLoc = New Vector2(Scene.Width / 2, CSng(Scene.Height * 0.8)）
        TempSingle = CSng((TempSingle + 0.05) Mod (Math.PI * 2)）
        GrowUp(Root, 0.01)
        WaveTree(Root, CSng(Math.Sin(TempSingle) / 1000))
    End Sub
    ''' <summary>
    ''' 创建树
    ''' </summary>
    Private Sub CreateTree(node As TreeNode, rank As Integer)
        node.Rank = rank
        CreateChildNode(node, rank - 1)
    End Sub
    ''' <summary>
    ''' 创建子树
    ''' </summary>
    Private Sub CreateChildNode(node As TreeNode, ByVal count As Integer)
        If count > 0 Then
            node.Children.Add(New TreeNode(node.Location.RotateNew(CSng(Rnd.NextDouble * Math.PI / 8)), CSng(Rnd.NextDouble * count * Root.Rank * 4), count))
            node.Children.Last.ParentNode = node
            node.Children.Last.MidRotateAngle = CSng(Rnd.NextDouble)
            CreateChildNode(node.Children.Last, count - 1)

            For i = 0 To Rnd.Next(1, CInt((9 - count) / 2)） - 1
                node.Children.Add(New TreeNode(node.Location.RotateNew(CSng(Rnd.NextDouble * Math.PI / 2 - Math.PI / 4)）, CSng(Rnd.NextDouble * count * Root.Rank * 4）, count))
                node.Children.Last.ParentNode = node
                node.Children.Last.MidRotateAngle = CSng(Rnd.NextDouble)
                CreateChildNode(node.Children.Last, count - 1)
                If Rnd.NextDouble > 0.8 + 0.02 * count Then
                    node.Children.Last.HasFlower = True
                    node.Children.Last.FlowerSize = CSng(Rnd.NextDouble）
                End If
            Next
        End If
    End Sub
    ''' <summary>
    ''' 树成长
    ''' </summary>
    Private Sub GrowUp(node As TreeNode, grow As Single, Optional ratio As Single = 0.19)
        If node.Percent < 1 Then
            node.Percent += CSng(grow * Rnd.NextDouble + 0.001F)
            If node.Percent >= 1 And node.Rank < 2 Then
                IsBeginDie = True
            End If
        Else
            If IsBeginDie = True Or True Then
                node.DiePercent += grow
                If node.DiePercent > Math.PI * 2 Then
                    node.DiePercent = 0
                End If
            End If
        End If
        If node.Percent > ratio Then
            For Each SubNode In node.Children
                GrowUp(SubNode, grow)
            Next
        End If
    End Sub
    ''' <summary>
    ''' 摇动树
    ''' </summary>
    Private Sub WaveTree(node As TreeNode, angle As Single, Optional ratio As Single = 1.618)
        For Each SubNode In node.Children
            SubNode.Location.Rotate(angle * SubNode.Location.Y / SubNode.Location.Length)
            If SubNode.HasFlower AndAlso SubNode.Percent > 0.5 Then
                SubNode.FlowerSize += 0.001F
                If SubNode.FlowerSize > 1 Then
                    SubNode.FlowerSize = 0
                End If
            End If
            WaveTree(SubNode, angle * ratio)
        Next
    End Sub
End Class
