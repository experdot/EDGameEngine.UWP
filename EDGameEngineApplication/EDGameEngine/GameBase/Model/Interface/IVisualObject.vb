''' <summary>
''' 表示场景里的可视化对象
''' </summary>
Public Interface IVisualObject
    ''' <summary>
    ''' 转换
    ''' </summary>
    Property Transform As Transform
    ''' <summary>
    ''' 外观
    ''' </summary>
    Property Appearance As Appearance
    ''' <summary>
    ''' 图层或物体所在场景
    ''' </summary>
    Property Scene As IScene
    ''' <summary>
    ''' 图层或物体的附加组件管理对象
    ''' </summary>
    Property GameComponents As GameComponents
End Interface
