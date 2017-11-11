Imports System.Numerics
Imports Windows.UI
''' <summary>
''' 颜色辅助类
''' </summary>
Public Class ColorHelper
    ''' <summary>
    ''' 返回颜色集合的平均颜色
    ''' </summary>
    Public Shared Function GetAverageColor(colors As IEnumerable(Of Color)) As Color
        Dim result As Color
        Dim a As Integer = CInt(colors.Sum(Function(c As Color) c.A) / colors.Count)
        Dim r As Integer = CInt(colors.Sum(Function(c As Color) c.R) / colors.Count)
        Dim g As Integer = CInt(colors.Sum(Function(c As Color) c.G) / colors.Count)
        Dim b As Integer = CInt(colors.Sum(Function(c As Color) c.B) / colors.Count)
        result = Color.FromArgb(CByte(a), CByte(r), CByte(g), CByte(b))
        Return result
    End Function
    ''' <summary>
    ''' 返回两个颜色的平均颜色
    ''' </summary>
    Public Shared Function GetAverageColor(color1 As Color, color2 As Color) As Color
        Dim result As Color
        Dim a As Integer = CInt((CInt(color1.A) + CInt(color2.A)) / 2)
        Dim r As Integer = CInt((CInt(color1.R) + CInt(color2.R)) / 2)
        Dim g As Integer = CInt((CInt(color1.G) + CInt(color2.G)) / 2)
        Dim b As Integer = CInt((CInt(color1.B) + CInt(color2.B)) / 2)
        result = Color.FromArgb(CByte(a), CByte(r), CByte(g), CByte(b))
        Return result
    End Function
    ''' <summary>
    ''' 返回两个颜色的相似度
    ''' </summary>
    Public Shared Function GetColorSimilarity(color1 As Color, color2 As Color) As Single
        Dim result As Single = 0
        Dim vec1 As New Vector3(color1.R, color1.G, color1.B)
        Dim vec2 As New Vector3(color2.R, color2.G, color2.B)
        result = 1 / (1 + (vec1 - vec2).LengthSquared)
        Return result
    End Function
    ''' <summary> 
    ''' 返回指定颜色的灰度值
    ''' </summary> 
    Public Shared Function GetGrayOfColor(color As Color) As Integer
        Dim mid, r, g, b As Integer
        r = color.R
        g = color.G
        b = color.B
        mid = CInt((r + g + b) / 3)
        Return mid
    End Function
End Class

