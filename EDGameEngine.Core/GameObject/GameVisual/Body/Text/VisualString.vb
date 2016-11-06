Imports System.Numerics
Imports Windows.UI

Public Class VisualString
    Inherits GameBody
    Implements IVisualText

    Public Property Color As Color = Colors.Black Implements IVisualText.Color
    Public Property Offset As Vector2 = Vector2.Zero Implements IVisualText.Offset
    Public Property Text As String = String.Empty Implements IVisualText.Text

    Public Overrides Sub StartEx()

    End Sub
    Public Overrides Sub UpdateEx()

    End Sub
End Class
