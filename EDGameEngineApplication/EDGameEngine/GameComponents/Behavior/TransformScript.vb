Imports System.Numerics
Imports EDGameEngine
''' <summary>
''' 提供多种自定义变换的脚本
''' </summary>
Public Class TransformScript
    Inherits BehaviorBase
    Public Overrides Sub Start()

    End Sub

    Public Overrides Sub Update()
        Target.Transform.Translation = New Vector2(Target.Scene.Width / 2, Target.Scene.Height / 2)
        'Target.Transform.Rotation = (Target.Transform.Rotation + 0.1 * Rnd.NextDouble) Mod （Math.PI * 2)
        'Target.Transform.Scale = New Vector2(Math.Sin(Target.Transform.Rotation), Math.Cos(Target.Transform.Rotation))
    End Sub
End Class
