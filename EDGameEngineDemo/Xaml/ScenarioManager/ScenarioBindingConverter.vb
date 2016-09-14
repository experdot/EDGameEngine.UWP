Imports EDGameEngineDemo
Public Class ScenarioBindingConverter
    Implements IValueConverter
    Private Function IValueConverter_Convert(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.Convert
        Dim s As Scenario = TryCast(value, Scenario)
        Return ((MainPage.Current.Scenarios.IndexOf(s) + 1) & ") ") + s.Title
    End Function
    Private Function IValueConverter_ConvertBack(value As Object, targetType As Type, parameter As Object, language As String) As Object Implements IValueConverter.ConvertBack
        Return True
    End Function
End Class
