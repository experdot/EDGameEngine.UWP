Imports Microsoft.Graphics.Canvas
Public Interface IScene
    Inherits IDisposable
    Property Width As Single
    Property Height As Single
    Function LoadAsync(resourceCreator As ICanvasResourceCreator) As Task
    Sub OnDraw(drawingSession As CanvasDrawingSession)
    Sub Update()
End Interface
