Imports System.Numerics
Imports EDGameEngine

Public Class VisualLine
    Inherits GameVisualModel
    Public Property Points As Vector2()
    Public Overrides Property Presenter As GameView = New LineView(Me)
    Public Sub New(points() As Vector2)
        Me.Points = points
    End Sub
    Public Overrides Sub Update()

    End Sub
End Class
