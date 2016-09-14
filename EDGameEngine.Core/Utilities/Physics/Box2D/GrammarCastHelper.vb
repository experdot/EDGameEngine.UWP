Namespace Global.Box2D
    Friend Module GrammarCastHelper
        <Extension>
        Public Function SetValue(Of T)(ByRef Target As T, Source As T) As T
            Target = Source
            Return Source
        End Function
        <Extension>
        Public Function ValueIncrement(ByRef Target As Integer) As Integer
            Dim val = Target
            Target += 1
            Return val
        End Function
        <Extension>
        Public Function IncrementValue(ByRef Target As Integer) As Integer
            Target += 1
            Return Target
        End Function
    End Module
End Namespace