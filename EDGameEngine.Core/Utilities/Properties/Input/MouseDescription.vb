Imports System.Numerics
''' <summary>
''' 用户鼠标状态描述
''' </summary>
Public Class MouseDescription
    ''' <summary>
    ''' 位置
    ''' </summary>
    Public Property Location As Vector2
        Set(value As Vector2)
            m_Location = value
            RaiseEvent MouseChanged(value)
        End Set
        Get
            Return m_Location
        End Get
    End Property
    ''' <summary>
    ''' 左键状态
    ''' </summary>
    Public Property LeftButton As Boolean
    ''' <summary>
    ''' 右键状态
    ''' </summary>
    Public Property RightButton As Boolean
    ''' <summary>
    ''' 鼠标位置发生改变
    ''' </summary>
    Public Event MouseChanged(ByVal loc As Vector2)
    ''' <summary>
    ''' 指针按下时发生
    ''' </summary>
    Public Event PointerPressed(ByVal loc As Vector2)
    ''' <summary>
    ''' 指针释放时发生
    ''' </summary>
    Public Event PointerReleased(ByVal loc As Vector2)

    Private m_Location As Vector2

    Friend Sub OnPointerPressed(ByVal loc As Vector2)
        RaiseEvent PointerPressed(loc)
    End Sub
    Friend Sub OnPointerReleased(ByVal loc As Vector2)
        RaiseEvent PointerReleased(loc)
    End Sub
    Friend Sub OnPointerMoved(ByVal loc As Vector2)
        Location = loc
    End Sub
End Class
