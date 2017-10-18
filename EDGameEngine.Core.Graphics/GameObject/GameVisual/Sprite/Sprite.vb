Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
''' <summary>
''' 贴图
''' </summary>
Public Class Sprite
    Inherits GameBody
    Implements ISprite
    Public Property Image As CanvasBitmap Implements ISprite.Image
        Set(value As CanvasBitmap)
            m_Image = value
            Rect = New Rect(0, 0, m_Image.Bounds.Width, m_Image.Bounds.Height)
        End Set
        Get
            Return m_Image
        End Get
    End Property
    Private m_Image As CanvasBitmap
    Public Overrides Sub StartEx()

    End Sub
    Public Overrides Sub UpdateEx()

    End Sub
End Class
