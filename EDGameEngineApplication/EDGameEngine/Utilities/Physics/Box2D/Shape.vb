Imports System
Imports System.Diagnostics
Imports System.Numerics
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    Public MustInherit Class Shape
        
        ' Methods
        Public Sub New()
            Me.ShapeType = ShapeType.Unknown
        End Sub

        Public MustOverride Function Clone() As Shape

        Public MustOverride Sub ComputeAABB(<Out> ByRef aabb As AABB, ByRef xf As XForm)

        Public MustOverride Sub ComputeMass(<Out> ByRef massData As MassData, ByVal density As Single)

        Public MustOverride Function GetSupport(ByVal d As Vector2) As Integer

        Public MustOverride Function GetSupportVertex(ByVal d As Vector2) As Vector2

        Public MustOverride Function GetVertex(ByVal index As Integer) As Vector2

        Public MustOverride Function GetVertexCount() As Integer

        Public MustOverride Function TestPoint(ByRef xf As XForm, ByVal p As Vector2) As Boolean

        Public MustOverride Function TestSegment(ByRef xf As XForm, <Out> ByRef lambda As Single, <Out> ByRef normal As Vector2, ByRef segment As Segment, ByVal maxLambda As Single) As SegmentCollide


        ' Properties
        Property ShapeType As ShapeType

        ' Fields
        Public _radius As Single
    End Class
End Namespace

