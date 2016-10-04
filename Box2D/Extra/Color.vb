Public Structure Color
    Implements IEquatable(Of Color)
    Sub New(a As Byte, r As Byte, g As Byte, b As Byte)
        Me.A = a
        Me.R = r
        Me.G = g
        Me.B = b
    End Sub
    Sub New(r As Byte, g As Byte, b As Byte)
        MyClass.New(255, r, g, b)
    End Sub
    Public Property A As Byte
    Public Property R As Byte
    Public Property G As Byte
    Public Property B As Byte
    Public Shared Function FromArgb(a As Byte, r As Byte, g As Byte, b As Byte) As Color
        Return New Color(a, r, g, b)
    End Function
    Public Overloads Function Equals(other As Color) As Boolean Implements IEquatable(Of Color).Equals
        Return A = other.A AndAlso R = other.R AndAlso G = other.G AndAlso B = other.B
    End Function
    Public Shared Operator =(color1 As Color, color2 As Color) As Boolean
        Return color1.Equals(color2)
    End Operator
    Public Shared Operator <>(color1 As Color, color2 As Color) As Boolean
        Return Not color1.Equals(color2)
    End Operator
    Public Overrides Function Equals(obj As Object) As Boolean
        If TypeOf obj Is Color Then
            Return DirectCast(obj, Color).Equals(Me)
        End If
        Return False
    End Function
    Public Overrides Function GetHashCode() As Integer
        Return B Or G << 8 Or R << 16 Or A << 24
    End Function
    ''' <summary>
    ''' 将颜色转换成可以在Xaml使用的格式
    ''' </summary>
    Public Overrides Function ToString() As String
        Return "#" + GetHashCode.ToString("X")
    End Function
End Structure
