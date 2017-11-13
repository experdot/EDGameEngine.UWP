Imports EDGameEngine.Core
''' <summary>
''' 元胞自动机
''' </summary>
Public Interface ICellularAutomata
    Inherits IGameBody
    ''' <summary>
    ''' 细胞集合
    ''' </summary>
    Property Cells As ICell(,)
End Interface
