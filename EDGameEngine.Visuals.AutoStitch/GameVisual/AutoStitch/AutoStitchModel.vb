Imports EDGameEngine.Core
Imports Microsoft.Graphics.Canvas
''' <summary>
''' 碎片模型
''' </summary>
Public Class AutoStitchModel
    Inherits GameBody
    ''' <summary>
    ''' 碎片集合
    ''' </summary>
    ''' <returns></returns>
    Public Property Fragments As New List(Of Fragment)
    ''' <summary>
    ''' 碎片拼接器
    ''' </summary>
    Public Property Splicer As FragmentSplicer
    Public Sub New(images As CanvasBitmap())
        For Each SubImage In images
            Fragments.Add(New Fragment(SubImage))
        Next
    End Sub
    Public Overrides Sub StartEx()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub UpdateEx()
        Throw New NotImplementedException()
    End Sub
End Class
