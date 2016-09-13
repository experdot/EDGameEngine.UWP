Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Public Class AutoDrawModel
    Inherits GameVisual
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
    Public Property Alpha As Integer = 8
    ''' <summary>
    ''' 每帧绘制长度
    ''' </summary>
    Public Property LinePointsCount As Integer = 3200
    Public Overrides Sub StartEx()
        ReDim SeqAI(8)
        For i = 0 To 8
            SeqAI(i) = New SequenceAI(BitmapPixelHelper.GetImageBolLimit(Image, i * 32 - 16, i * 32 + 16))
        Next
        ImageSize = New Size(Image.Bounds.Width, Image.Bounds.Height)
        GameComponents.Effects.Add(New GhostEffect() With {.SourceRect = Image.Bounds})
    End Sub
    Public Overrides Sub UpdateEx()
        '图像位置居中
        Transform.Translation = New Vector2(Scene.Width / 2 - ImageSize.Width / 2, Scene.Height / 2 - ImageSize.Height / 2)
        '更新绘制序列
        UpdateList()
    End Sub
    Private Sub UpdateList()
        Static Index0, Index1, Index2 As Integer
        CurrentList.Clear()
        PenSizeList.Clear()
        For i = 0 To LinePointsCount - 1
            While (SeqAI(Index0).Sequences.Count <= 0 OrElse (Index1 <> 0 AndAlso Index1 >= SeqAI(Index0).Sequences.Count))
                Index1 = 0
                Index2 = 0
                Index0 += 1
                If Index0 > 8 Then
                    Index0 = 0
                    Size = Size / 4
                    Alpha = Alpha * 4
                    If Size < 1 Then Size = 1
                    If Alpha > 255 Then Alpha = 255
                    LinePointsCount = CSng(LinePointsCount) / 1.4
                End If
            End While

            CurrentList.Add(SeqAI(Index0).Sequences(Index1).Points(Index2))
            Dim tempS As Single = SeqAI(Index0).Sequences(Index1).Sizes(Index2) * Size
            PenSizeList.Add(If(tempS < 1, 1, tempS))

            Index2 += 1
                If Index2 >= SeqAI(Index0).Sequences(Index1).Points.Count Then
                    Index2 = 0
                    Index1 = (Index1 + 1)
                End If
            Next
    End Sub
End Class
