Imports System.Numerics
Imports Windows.UI
Public MustInherit Class ParticalsBase
    Inherits GameVisualModel
    Public Overridable Property Count As Integer = 100
    Public Overridable Property Particals As List(Of Partical)
    Public Sub New()
        InitParticals()
    End Sub
    ''' <summary>
    ''' 初始化粒子
    ''' </summary>
    Public MustOverride Sub InitParticals()
End Class


