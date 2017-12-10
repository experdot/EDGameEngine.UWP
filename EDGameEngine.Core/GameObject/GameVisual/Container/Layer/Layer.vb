Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.Graphics.Effects
Imports Windows.UI
''' <summary>
''' 图层
''' </summary>
Public Class Layer
    Inherits GameVisualBase
    Implements ILayer
    Public Overridable Property GameBodys As New List(Of IGameBody) Implements ILayer.GameBodys
    Public Overridable Property Background As Color = Colors.Transparent Implements ILayer.Background

    Public Overrides Sub Start()
        For Each SubBody In GameBodys
            SubBody.Start()
        Next
        GameComponents.Start()
    End Sub
    Public Overrides Sub Update()
        For Each SubBody In GameBodys
            SubBody.Update()
        Next
        GameComponents.Update()
    End Sub
End Class
