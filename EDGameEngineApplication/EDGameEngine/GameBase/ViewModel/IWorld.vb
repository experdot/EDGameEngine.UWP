Imports Microsoft.Graphics.Canvas
Public Interface IWorld
    Sub OnDraw(drawingSession As CanvasDrawingSession)
    Sub Update()
    Function LoadAsync(resourceCreator As ICanvasResourceCreator) As Task
End Interface
