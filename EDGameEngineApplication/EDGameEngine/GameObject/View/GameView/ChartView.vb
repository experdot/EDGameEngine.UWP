Imports System.Numerics
Imports EDGameEngine
Imports Microsoft.Graphics.Canvas
Imports Windows.UI

Public Class ChartView
    Inherits TypedGameView(Of VisualChart)
    Public Sub New(Target As VisualChart)
        MyBase.New(Target)
    End Sub
    Public Overrides Sub OnDraw(DrawingSession As CanvasDrawingSession)
        Using cmdList = New CanvasCommandList(DrawingSession.Device)
            Using Dl = cmdList.CreateDrawingSession
                'Dl.DrawLine(Location,
                '            Location + New Vector2(DataList(0).Count * 2, 0),
                '            Color.FromArgb(255, 255, 255, 255))
                For i = 0 To 2
                    For j = 0 To Target.DataList(i).Count - 1
                        Dim col = Color.FromArgb(j / Target.DataList(i).Count * 255, Target.ColorList(i).R, Target.ColorList(i).G, Target.ColorList(i).B)
                        Dl.DrawLine(Target.Location + New Vector2(j * 2, Target.DataList(i)(j)),
                                    Target.Location + New Vector2(j * 2, 0),
                                    col)
                        'Dl.FillCircle(Target.Location + New Vector2(j * 2, Target.DataList(i)(j)), 3, col)
                    Next
                    Dl.DrawText(Target.LabelList(i) & Target.DataList(i).Last,
                                Target.Location + New Vector2(0, 200 + i * 24),
                                Target.ColorList(i),
                                New Text.CanvasTextFormat() With {.FontFamily = "微软雅黑",
                                                                  .FontSize = 12})
                    Dl.FillCircle(Target.Location + New Vector2(Target.DataList(i).Count * 2, Target.DataList(i).Last), 3, Target.ColorList(i）)
                Next
            End Using
            Using blur1 = New Effects.GaussianBlurEffect() With {.Source = cmdList, .BlurAmount = 3}
                'DrawingSession.DrawImage(blur1)
                DrawingSession.DrawImage(cmdList)
            End Using
        End Using
    End Sub
End Class
