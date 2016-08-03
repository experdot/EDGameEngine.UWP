Partial Public Class MainPage
    Inherits Page
    ' This is used on the main page as the title of the sample.
    Public Const FEATURE_NAME As String = "Optional Functions"

    ' Change the array below to reflect the name of your scenarios.
    ' This will be used to populate the list of scenarios on the main page with
    ' which the user will choose the specific scenario that they are interested in.
    ' These should be in the form: "Navigating to a web page".
    ' The code in MainPage will take care of turning this into: "1) Navigating to a web page"
    Public Scenarios As New List(Of Scenario)() From {
        New Scenario() With {
            .Title = "Start",
            .ClassType = GetType(Scenario1_Start)
        },
        New Scenario() With {
            .Title = "Geometric",
            .ClassType = GetType(Scenario2_Geometric)
        }
    }
End Class
Public Class Scenario
    Public Property Title As String
    Public Property ClassType As Type
    Public Overrides Function ToString() As String
        Return Title
    End Function
End Class
