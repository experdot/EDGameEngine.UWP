Imports EDGameEngine
Public Class Scene2
    Inherits Scene
    Public Sub New(WindowSize As Size)
        MyBase.New(WindowSize)
    End Sub
    Public Overrides Sub CreateObject()
        Dim tempModel As New ParticalFollow()
        Me.AddGameVisual(tempModel, New ParticalView(tempModel))
        tempModel.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height)})
    End Sub
End Class
