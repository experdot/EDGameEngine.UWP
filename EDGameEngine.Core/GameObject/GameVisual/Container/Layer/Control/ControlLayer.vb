''' <summary>
''' 包含UI的图层
''' </summary>
Public Class ControlLayer
    Inherits Layer
    Public Property Controls As New List(Of UIElement)
    Public Sub AddControl(element As UIElement)
        Controls.Add(element)
        Scene.World.UIContainer.Children.Add(element)
    End Sub
End Class
