Imports System.Numerics
Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
Imports Microsoft.Graphics.Canvas.Effects
Imports Windows.Graphics.Effects
''' <summary>
''' 水流效果器
''' </summary>
Public Class StreamEffect
    Inherits EffectBase
    Public Overrides Function Effect(source As IGraphicsEffectSource, DrawingSession As CanvasDrawingSession) As IGraphicsEffectSource
        Static ts As Single
        ts = (ts + 0.01) Mod (Math.PI * 2)
        Dim dispX As Single = 75.0F * Math.Sin(ts)
        Dim dispY As Single = 75.0F * Math.Cos(ts)
        Dim dispMap = New DisplacementMapEffect() With {
                     .Source = source,
                     .XChannelSelect = EffectChannelSelect.Red,
                     .YChannelSelect = EffectChannelSelect.Green,
                     .Amount = 50.0F,
                     .Displacement = New Transform2DEffect() With {
                        .TransformMatrix = Matrix3x2.CreateTranslation(dispX, dispY),
                         .Source = New BorderEffect() With {
                         .ExtendX = CanvasEdgeBehavior.Mirror,
                         .ExtendY = CanvasEdgeBehavior.Mirror,
                         .Source = New TurbulenceEffect() With {.Octaves = 10, .Frequency = New Vector2(0.01, 0.01)}}}}
        Return dispMap
    End Function
End Class
