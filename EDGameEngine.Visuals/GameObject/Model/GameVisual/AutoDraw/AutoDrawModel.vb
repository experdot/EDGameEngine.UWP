Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Public Class AutoDrawModel
    Inherits GameBody
    ''' <summary>
    ''' AI管理器集合
    ''' </summary>
    ''' <returns></returns>
    Public Property SeqMgr As SeqManager()
    ''' <summary>
    ''' AI管理器索引
    ''' </summary>
    ''' <returns></returns>
    Public Property SeqMgrIndex As Integer = 0
    Public Property SeqAI As SequenceAI()
    ''' <summary>
    ''' 原图
    ''' </summary>
    Public Property Image As CanvasBitmap
    ''' <summary>
    ''' 原图大小
    ''' </summary>
    Public Property ImageSize As Size
    ''' <summary>
    ''' 当前帧绘制序列
    ''' </summary>
    Public Property CurrentList As New List(Of Vector2)
    ''' <summary>
    '''点序列画笔大小
    ''' </summary>
    ''' <returns></returns>
    Public Property PenSizeList As New List(Of Single)
    ''' <summary>
    ''' 当前帧画笔大小
    ''' </summary>
    Public Property Size As Single = 32
    ''' <summary>
    ''' 当前帧画笔颜色Alpha通道
    ''' </summary>
    Public Property Alpha As Integer = CInt(255 / Size)
    ''' <summary>
    ''' 每帧绘制长度
    ''' </summary>
    Public Property LinePointsCount As Integer = 200
    ''' <summary>
    ''' 倍率
    ''' </summary>
    Public Property Multi As Single = 1.3F

    Public Property Loc As Vector2
    Public Overrides Sub StartEx()
        ReDim SeqMgr(5)
        For i = 0 To 4
            SeqMgr(i) = New SeqManager(Image, i + 4)
        Next
        For i = 0 To 3
            SeqMgr(i).Denoising(20 - i * 5)
        Next
        SeqAI = SeqMgr(SeqMgrIndex).SeqAI
        ImageSize = New Size(Image.Bounds.Width, Image.Bounds.Height)
        GameComponents.Effects.Add(New GhostEffect() With {.SourceRect = Image.Bounds})
        GameComponents.Effects.Add(New ShadowEffect)
    End Sub
    Public Overrides Sub UpdateEx()
        Static ImageVec As Vector2 = New Vector2(CSng(ImageSize.Width), CSng(ImageSize.Height)) / 2
        '图像位置居中
        Transform.Translation = New Vector2(Scene.Width, Scene.Height) / 2 - ImageVec '- Loc / 4
        '更新绘制序列
        UpdateList()
    End Sub
    Private Sub UpdateList()
        Static Index0, Index1, Index2 As Integer
        If CurrentList.Count > 0 Then Exit Sub
        PenSizeList.Clear()
        For i = 0 To LinePointsCount - 1
            While (SeqAI(Index0).Sequences.Count <= 0 OrElse (Index1 <> 0 AndAlso Index1 >= SeqAI(Index0).Sequences.Count))
                Index1 = 0
                Index2 = 0
                Index0 += 1
                If Index0 >= SeqAI.Count Then
                    If SeqMgrIndex < SeqMgr.Count - 1 Then SeqMgrIndex += 1
                    SeqAI = SeqMgr(SeqMgrIndex).SeqAI
                    Index0 = 0
                    Size = Size / 6
                    Alpha = Alpha * 6
                    If Size < 1 Then Size = 1
                    If Alpha > 255 Then Alpha = 255
                    LinePointsCount = CInt(CSng(LinePointsCount) * Multi)
                    If LinePointsCount > 10000 Then LinePointsCount = 10000
                End If
            End While

            CurrentList.Add(SeqAI(Index0).Sequences(Index1).Points(Index2))
            Loc = Loc + CurrentList.Last
            Dim tempS As Single = SeqAI(Index0).Sequences(Index1).Sizes(Index2) * Size
            PenSizeList.Add(If(tempS < 1, 1, tempS))
            Dim temp = CInt(Size / 4)
            If temp < 1 Then temp = 1
            Index2 += temp
            If Index2 >= SeqAI(Index0).Sequences(Index1).Points.Count Then
                Index2 = 0
                Index1 = (Index1 + 1)
            End If
        Next
        Loc = Loc / CurrentList.Count / CSng(Math.Log(Loc.Length / 20))
    End Sub
End Class
