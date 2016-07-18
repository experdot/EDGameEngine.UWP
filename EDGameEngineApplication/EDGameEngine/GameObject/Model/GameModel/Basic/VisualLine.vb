Imports System.Numerics
Public Class VisualLine
    Inherits GameVisualModel
    Public Property Points As New List(Of Vector2)
    Public Overrides Property Presenter As GameView = New LineView(Me)
    Public Sub New()

    End Sub
    Public Overrides Sub Update()

    End Sub
End Class
