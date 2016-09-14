Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 水波效果器
''' </summary>
Public Class RippleEffect
    Inherits EffectBase
    Public Property Buffer1 As Integer()
    Public Property Buffer2 As Integer()
    Public Property Width As Integer
    Public Property Height As Integer
    Public Sub New(width As Integer, height As Integer)
        Me.Width = width
        Me.Height = height
        ReDim Buffer1(width * height - 1)
        ReDim Buffer2(width * height - 1)
    End Sub
    Public Overrides Function Effect(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession) As IGraphicsEffectSource
        Dim srcData() As Color = BitmapCacheHelper.CacheImageClip(DrawingSession, source, New Rect(0, 0, Width, Height)).GetPixelColors
        Dim desData() As Color = srcData.Clone
        Dim xoff, yoff As Integer
        Dim k As Integer = Width
        For i = 1 To Height - 2
            For j = 0 To Width - 1
                xoff = Buffer1(k - 1) - Buffer1(k + 1) 'x偏移
                yoff = Buffer1(k - Width) - Buffer1(k + Width) 'y偏移
                If (xoff = 0 AndAlso yoff = 0) OrElse i + yoff <= 0 OrElse i + yoff >= Height OrElse j + xoff <= 0 OrElse j + xoff >= Width Then
                    k += 1 '边界判断
                    Continue For
                End If
                Dim pos1, pos2 As Integer
                pos1 = (i + yoff) * Width + j + xoff
                pos2 = i * Width + j
                desData(pos2) = srcData(pos1) '根据偏移量重新渲染界面
                k += 1
            Next
        Next
        Return CanvasBitmap.CreateFromColors(DrawingSession, desData, Width, Height)
    End Function
    ''' <summary>
    ''' 投放波源
    ''' </summary>
    Public Sub DropSton(x As Integer, y As Integer, Optional SouSize As Integer = 6, Optional SouWeight As Integer = 64)
        If x + SouSize > Width OrElse y + SouSize > Height Or x - SouSize < 0 Or y - SouSize < 0 Then Return
        For posx = x - SouSize To x + SouSize
            For posy = y - SouSize To y + SouSize
                If ((posx - x) * (posx - x) + (posy - y) * (posy - y) < SouSize * SouSize) Then
                    Buffer1(Width * posy + posx) = -SouWeight '波源投放，其实就是在波源直径区域将Buff数组赋初始值，以便进一步产生水波
                End If
            Next
        Next
    End Sub
    ''' <summary>
    ''' 波源扩散
    ''' </summary>
    Public Sub RippleSpread()
        For i = Width To Width * Height - Width - 1
            Buffer2(i) = ((Buffer1(i - 1) + Buffer1(i + 1) + Buffer1(i - Width) + Buffer1(i + Width)) >> 1) - Buffer2(i)
            Buffer2(i) -= Buffer2(i) >> 4 '波幅衰减
        Next
        Dim tmp As Integer() = Buffer1 '这里开始交换buff1和buff2，是为了让波源能继续传播下去
        Buffer1 = Buffer2
        Buffer2 = tmp
    End Sub
End Class
