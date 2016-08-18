Imports System.Numerics
Imports Microsoft.Graphics.Canvas
Public Class AutoDrawModel
    Inherits GameVisualModel
    Public Property SeqAI As SequenceAI
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
    Public Property LinePointsCount As Integer = 200
    Public Overrides Sub Start()
        SeqAI = New SequenceAI(BitmapPixelHelper.GetImageBol(Image, 0))
        ImageSize = New Size(Image.Bounds.Width, Image.Bounds.Height)
    End Sub
    Public Overrides Sub Update()
        '图像位置居中
        Transform.Position = New Vector2(Scene.Width / 2 - ImageSize.Width / 2, Scene.Height / 2 - ImageSize.Height / 2)
        '更新绘制序列
        UpdateList()
    End Sub
    Private Sub UpdateList()
        Static Index, Index2, Split As Integer
        CurrentList.Clear()
        For i = 0 To LinePointsCount - 1
            While (SeqAI.Sequences.Count <= 0 OrElse (Index <> 0 AndAlso Index >= SeqAI.Sequences.Count))
                Index = 0
                Index2 = 0
                Split = (Split + 32)
                SeqAI = New SequenceAI(BitmapPixelHelper.GetImageBolLimit(Image, Split - 16, Split + 16))
                If Split > 255 Then
                    Split = 0
                    Size /= 4
                    Alpha *= 4
                    If Size < 1 Then Size = 1
                    If Alpha > 255 Then Alpha = 255
                End If
            End While
            CurrentList.Add(SeqAI.Sequences(Index).Points(Index2))
            Index2 += 1
            If Index2 >= SeqAI.Sequences(Index).Points.Count Then
                Index2 = 0
                Index = (Index + 1)
            End If
        Next
    End Sub
End Class
