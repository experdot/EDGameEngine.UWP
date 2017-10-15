Imports Microsoft.Graphics.Canvas
Imports EDGameEngine.Core
Imports System.Text
Imports System.Numerics
Imports Windows.UI
''' <summary>
''' L系统树模型的视图
''' </summary>
Public Class LSystemTreeView
    Inherits TypedGameView(Of IStateMachine)
    Public Sub New(Target As IStateMachine)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(drawingSession As CanvasDrawingSession)
        Static rnd As New Random
        Dim len As Single = -5.0F
        Dim center As Vector2 = New Vector2(Target.Scene.Width / 2, Target.Scene.Height * 0.95F)
        Dim offset As New Vector2(0, len)
        Dim centerStack As New Stack(Of Vector2)
        Dim offsetStack As New Stack(Of Vector2)

        Dim subState As State
        For i = 0 To Target.States.Count - 1
            subState = Target.States(i)
            Select Case subState.Id
                Case AscW("F")
                    Dim realOffset = offset '* CSng(rnd.NextDouble)
                    drawingSession.DrawLine(center, center + realOffset, Colors.Black)
                    center += realOffset
                Case AscW("+")
                    offset.Rotate(CSng(Math.PI / 6))
                Case AscW("-")
                    offset.Rotate(-CSng(Math.PI / 6))
                Case AscW("[")
                    centerStack.Push(center)
                    offsetStack.Push(offset)
                Case AscW("]")
                    center = centerStack.Pop
                    offset = offsetStack.Pop
            End Select
        Next
    End Sub
End Class