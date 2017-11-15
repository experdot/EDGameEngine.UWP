Imports EDGameEngine.Core
Imports EDGameEngine.Visuals
''' <summary>
''' L系统树模型
''' </summary>
Partial Public Class LSystemTree
    Inherits GameBody
    Implements IStateMachine
    ''' <summary>
    ''' 状态集
    ''' </summary>
    Public Property States As List(Of State) Implements IStateMachine.States
        Get
            Return LSystem.States
        End Get
        Set(value As List(Of State))
            LSystem.States = value
        End Set
    End Property

    Private LSystem As LSystem

    Public Overrides Sub StartEx()
        LSystem = New LSystem
        LSystem.InitRoot(New State(AscW("F"), Nothing, 0))
        LSystem.AddRule(New RuleTree(AscW("F"), 0))
        LSystem.Generate(5)
    End Sub

    Public Overrides Sub UpdateEx()

    End Sub
End Class

