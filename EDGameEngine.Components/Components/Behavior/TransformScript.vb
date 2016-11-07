Imports System.Numerics
Imports EDGameEngine.Core
''' <summary>
''' 提供多种自定义变换的脚本
''' </summary>
Public Class TransformScript
    Inherits BehaviorBase
    Public Overrides Sub Start()

    End Sub

    Public Overrides Sub Update()
        'Target.Transform.Translation = New Vector2(Target.Scene.Width / 2, Target.Scene.Height / 2)
        Target.Transform.Center = New Vector2(Target.Scene.Width / 2, Target.Scene.Height / 2)
        Target.Transform.Rotation = CSng((Target.Transform.Rotation + 0.001) Mod （Math.PI * 2))
        Target.Transform.Scale = New Vector2(CSng(Math.Sin(Target.Transform.Rotation)), CSng(Math.Cos(Target.Transform.Rotation)))
    End Sub
End Class
