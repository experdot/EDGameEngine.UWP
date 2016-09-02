Imports System.Numerics
Imports EDGameEngine
Public Class Plant
    Inherits GameVisualModel
    Public Property Tree As TreeNode
    Public Property IsBeginDie As Boolean
    Dim TempSingle, Ts2 As Single
    Public Sub New(loc As Vector2, Optional Rank As Integer = 8)
        Tree = New TreeNode(New Vector2(0, -100), 100, 1)
        Tree.RealLoc = loc
        CreateTree(Tree, Rank)
    End Sub
    Public Overrides Sub StartSelf()
    End Sub
    Public Overrides Sub Updateself()
        Transform.Center = Tree.RealLoc
        Transform.Scale = New Vector2(0.95 + Math.Sin(Ts2) * 0.05, 1)
        Ts2 = (Ts2 + 0.04) Mod (Math.PI * 2)

        Tree.RealLoc = New Vector2(Scene.Width / 2, Scene.Height * 0.8)
        TempSingle = (TempSingle + 0.05) Mod (Math.PI * 2)
        GrowUp(Tree, 0.01)
        WaveTree(Tree, Math.Sin(TempSingle) / 1000)
    End Sub
    ''' <summary>
    ''' 创建树
    ''' </summary>
    ''' <param name="Tree"></param>
    ''' <param name="rank"></param>
    Private Sub CreateTree(ByRef Tree As TreeNode, rank As Integer)
        Tree.Rank = rank
        CreateChildNode(Tree, rank - 1)
    End Sub
    ''' <summary>
    ''' 创建子树
    ''' </summary>
    ''' <param name="ParentNode"></param>
    ''' <param name="Count"></param>
    Private Sub CreateChildNode(ByRef ParentNode As TreeNode, ByVal Count As Integer)
        If Count > 0 Then
            ParentNode.ChildNode.Add(New TreeNode(ParentNode.Location.RotateNew(Rnd.NextDouble * Math.PI / 8), Rnd.NextDouble * Count * Tree.Rank * 4, Count))
            ParentNode.ChildNode.Last.ParentNode = ParentNode
            CreateChildNode(ParentNode.ChildNode.Last, Count - 1)
            ParentNode.ChildNode.Last.MidRotateAngle = Rnd.NextDouble
            For i = 0 To Rnd.Next(1, (9 - Count) / 2) - 1
                ParentNode.ChildNode.Add(New TreeNode(ParentNode.Location.RotateNew(Rnd.NextDouble * Math.PI / 2 - Math.PI / 4), Rnd.NextDouble * Count * Tree.Rank * 4, Count))
                ParentNode.ChildNode.Last.ParentNode = ParentNode
                CreateChildNode(ParentNode.ChildNode.Last, Count - 1)
                If Rnd.NextDouble > 0.8 + 0.02 * Count Then
                    ParentNode.ChildNode.Last.HasFlower = True
                    ParentNode.ChildNode.Last.FlowerSize = Rnd.NextDouble
                End If
                ParentNode.ChildNode.Last.MidRotateAngle = Rnd.NextDouble
            Next
        End If
    End Sub
    ''' <summary>
    ''' 树成长
    ''' </summary>
    ''' <param name="ParentNode"></param>
    ''' <param name="gValue"></param>
    ''' <param name="Ratio"></param>
    Private Sub GrowUp(ByRef ParentNode As TreeNode, gValue As Single, Optional Ratio As Single = 0.19)
        If ParentNode.Percent < 1 Then
            ParentNode.Percent += gValue * Rnd.NextDouble + 0.001
            If ParentNode.Percent >= 1 And ParentNode.Rank < 2 Then
                IsBeginDie = True
            End If
        Else
            If IsBeginDie = True Or True Then
                ParentNode.DiePercent += gValue
                If ParentNode.DiePercent > Math.PI * 2 Then
                    ParentNode.DiePercent = 0
                End If
            End If
        End If
        If ParentNode.Percent > Ratio Then
            Dim SubNode As TreeNode
            For i = 0 To ParentNode.ChildNode.Count - 1
                SubNode = ParentNode.ChildNode(i)
                GrowUp(SubNode, gValue)
            Next
        End If
    End Sub
    ''' <summary>
    ''' 摇动树
    ''' </summary>
    ''' <param name="ParentNode"></param>
    ''' <param name="Angle"></param>
    ''' <param name="Ratio"></param>
    Private Sub WaveTree(ByRef ParentNode As TreeNode, ByVal Angle As Single, Optional Ratio As Single = 1.618)
        Dim SubNode As TreeNode
        For i = 0 To ParentNode.ChildNode.Count - 1
            SubNode = ParentNode.ChildNode(i)
            SubNode.Location.Rotate(Angle * SubNode.Location.Y / SubNode.Location.Length)
            If SubNode.HasFlower And SubNode.Percent > 0.5 Then
                SubNode.FlowerSize += 0.001
                If SubNode.FlowerSize > 1 Then
                    SubNode.FlowerSize = 0
                End If
            End If
            If Not SubNode.ChildNode.Count = 0 Then
                WaveTree(SubNode, Angle * Ratio)
            End If
        Next
    End Sub


End Class
