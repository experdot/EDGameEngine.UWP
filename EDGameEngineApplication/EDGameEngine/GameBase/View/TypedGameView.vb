''' <summary>
''' 表示某种类型模型的视图
''' </summary>
''' <typeparam name="T"></typeparam>
Public MustInherit Class TypedGameView(Of T As GameVisualModel)
    Inherits GameView
    Protected Target As T
    Sub New(Target As T)
        Me.Target = Target
    End Sub
End Class
