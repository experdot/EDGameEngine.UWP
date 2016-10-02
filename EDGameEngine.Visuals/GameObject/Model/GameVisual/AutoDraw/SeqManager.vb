Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
''' <summary>
''' 包含若干SequenceAI的管理器
''' </summary>
Public Class SeqManager
    Public Property SeqAI As SequenceAI()
    Public Sub New(Image As CanvasBitmap, split As Integer)
        ReDim SeqAI(split)
        Dim temp As Integer = CInt(255 / split)
        For i = 0 To split
            SeqAI(i) = New SequenceAI(BitmapPixelHelper.GetImageBolLimit(Image, CInt(i * temp - temp / 2), CInt(i * temp + temp / 2)))
        Next
    End Sub
    Public Sub Denoising(Optional count As Integer = 10)
        For Each SubAI In SeqAI
            SubAI.Denoising(count)
        Next
    End Sub
End Class
