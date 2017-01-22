Imports System.Numerics
Imports EDGameEngine.Components
Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
Imports Windows.UI
Public Class Scene3
    Inherits Scene
    Public Sub New(world As World, WindowSize As Size)
        MyBase.New(world, WindowSize)
    End Sub
    Public Overrides Sub CreateObject()

        Scene.GameComponents.Behaviors.Add(New PhysicsScript)

    End Sub

    Public Overrides Sub CreateUI()

    End Sub
End Class
