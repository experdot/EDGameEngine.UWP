Imports Microsoft.Graphics.Canvas
Public Interface IWorld
    Inherits IDisposable
    Sub OnDraw(drawingSession As CanvasDrawingSession)
    Sub Update()
    Function LoadAsync(resourceCreator As ICanvasResourceCreator) As Task
End Interface
