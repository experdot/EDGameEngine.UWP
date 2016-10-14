Imports System
Imports System.Numerics
Imports System.Runtime.InteropServices

Namespace Global.Box2D
    ''' <summary>
    ''' 描述二维空间坐标与方向的对象
    ''' </summary>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure XForm
        ''' <summary>
        ''' 位置
        ''' </summary>
        Public Property Position As Vector2
        ''' <summary>
        ''' 旋转矩阵
        ''' </summary>
        Public Property RoateMatrix As Mat22
        ''' <summary>
        ''' 旋转角度
        ''' </summary>
        Public ReadOnly Property Angle As Single
            Get
                Return CType(Math.Atan2(CType(Me.RoateMatrix.Column1.Y, Double), CType(Me.RoateMatrix.Column1.X, Double)), Single)
            End Get
        End Property
        Public Sub New(ByVal position As Vector2, ByRef r As Mat22)
            Me.Position = position
            Me.RoateMatrix = r
        End Sub
        ''' <summary>
        ''' 设置当前的<see cref="XForm"/>对象为标量
        ''' </summary>
        Public Sub Identity()
            Me.Position = Vector2.Zero
            Me.RoateMatrix.SetIdentity()
        End Sub
        ''' <summary>
        ''' 设置指定的位置和角度到当前的<see cref="XForm"/>对象
        ''' </summary>
        ''' <param name="p"></param>
        ''' <param name="angle"></param>
        Public Sub SetValue(ByVal p As Vector2, ByVal angle As Single)
            Me.Position = p
            Me.RoateMatrix.SetValue(angle)
        End Sub
    End Structure
End Namespace

