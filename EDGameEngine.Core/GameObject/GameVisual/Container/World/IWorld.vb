Imports EDGameEngine.Core

Public Interface IWorld
    Inherits IGameObject

    Property Width As Integer
    Property Height As Integer
    Property RenderMode As RenderMode

    Property ActiveScene As IScene
    Property Scenes As Dictionary(Of String, IScene)
    Sub SwitchScene(key As String, Optional reStart As Boolean = False)
End Interface
