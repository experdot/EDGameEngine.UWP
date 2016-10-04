Imports System

Namespace Global.Box2D
    Public Interface IDestructionListener
        ' Methods
        Sub SayGoodbye(ByVal fixture As Fixture)
        Sub SayGoodbye(ByVal joint As Joint)
    End Interface
End Namespace

