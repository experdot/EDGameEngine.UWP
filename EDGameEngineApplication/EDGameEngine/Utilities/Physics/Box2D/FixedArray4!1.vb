Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    Public Structure FixedArray4(Of T As {Structure})
        Private _value0 As T
        Private _value1 As T
        Private _value2 As T
        Private _value3 As T
        Default Public Property Item(ByVal index As Integer) As T
            Get
                Select Case index
                    Case 0
                        Return Me._value0
                    Case 1
                        Return Me._value1
                    Case 2
                        Return Me._value2
                    Case 3
                        Return Me._value3
                End Select
                Throw New IndexOutOfRangeException
            End Get
            Set(ByVal value As T)
                Select Case index
                    Case 0
                        Me._value0 = value
                        Exit Select
                    Case 1
                        Me._value1 = value
                        Exit Select
                    Case 2
                        Me._value2 = value
                        Exit Select
                    Case 3
                        Me._value3 = value
                        Exit Select
                    Case Else
                        Throw New IndexOutOfRangeException
                End Select
            End Set
        End Property

    End Structure
End Namespace

