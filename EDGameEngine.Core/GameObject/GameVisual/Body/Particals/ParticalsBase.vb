''' <summary>
''' 粒子系统基类
''' </summary>
Public MustInherit Class ParticalsBase
    Inherits GameBody
    Implements IParticals
    Public Overridable Property Count As Integer = 100 Implements IParticals.Count
    Public Overridable Property Particals As IEnumerable(Of IPartical) Implements IParticals.Particals
End Class
