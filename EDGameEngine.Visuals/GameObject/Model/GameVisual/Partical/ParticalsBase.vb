Imports System.Numerics
Imports EDGameEngine.Core
Imports Windows.UI
Public MustInherit Class ParticalsBase
    Inherits GameBody
    Public Overridable Property Count As Integer = 100
    Public Overridable Property Particals As List(Of Partical)
End Class


