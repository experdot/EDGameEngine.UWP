Imports System
Imports System.Numerics
Imports System.Runtime.CompilerServices

Namespace Global.Box2D.UWPExtensions
    <Extension>
    Public Module Extension
        ' Methods
        <Extension>
        Public Function Normalize(ByVal vec As Vector2) As Vector2
            Return Vector2.Normalize(vec)
        End Function

    End Module
End Namespace

