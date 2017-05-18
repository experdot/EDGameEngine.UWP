Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals

Public Class Scene2
    Inherits Scene
    Public Sub New(world As World, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Public Overrides Sub CreateObject()
        Dim tempModel As New ParticalsFollow()
        Me.AddGameVisual(tempModel, New ParticalsView(tempModel))
        tempModel.GameComponents.Effects.Add(New GhostEffect With {.SourceRect = New Rect(0, 0, Width, Height)})
    End Sub
    Public Overrides Sub CreateUI()
        Dim b As New Button
        Me.AddUIElement(New Button, 1)
    End Sub

    Public Overrides Function CreateResouces(imgRes As ImageResourceManager) As Task
        Throw New NotImplementedException()
    End Function
End Class
