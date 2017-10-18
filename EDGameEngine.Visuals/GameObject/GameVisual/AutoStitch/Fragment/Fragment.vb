Imports System.Numerics
Imports EDGameEngine.Core.Graphics
Imports Microsoft.Graphics.Canvas
''' <summary>
''' 碎片，用于表示和预处理碎片
''' </summary>
Public Class Fragment
    ''' <summary>
    ''' 位置
    ''' </summary>
    Public Property Location As Vector2
    ''' <summary>
    ''' 边沿
    ''' </summary>
    Public Property Border As FragmentBorder
    ''' <summary>
    ''' 图像
    ''' </summary>
    Public Property Image As CanvasBitmap
    ''' <summary>
    ''' 阈值信息
    ''' </summary>
    ''' <returns></returns>
    Public Property ThresholdArray As Integer(,)
    ''' <summary>
    ''' 宽度
    ''' </summary>
    Public ReadOnly Property Width As Integer
        Get
            Return CInt(Image.Bounds.Width)
        End Get
    End Property
    ''' <summary>
    ''' 高度
    ''' </summary>
    Public ReadOnly Property Height As Integer
        Get
            Return CInt(Image.Bounds.Height)
        End Get
    End Property

    Public Property SplitNum As Integer = 127
    Public Sub New(image As CanvasBitmap)
        Me.Image = image
        ThresholdArray = BitmapPixelHelper.GetImageBol(Me.Image, SplitNum)
        Border = New FragmentBorder(ThresholdArray)
    End Sub
End Class
