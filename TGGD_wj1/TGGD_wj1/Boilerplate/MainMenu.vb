Imports Spectre.Console
Module MainMenu
    Private Function ConfirmQuit() As Boolean
        AnsiConsole.WriteLine()
        Return AnsiConsole.Confirm("Are you sure you want to quit?", False)
    End Function
    Public Function Run() As Boolean
        AnsiConsole.WriteLine()
        Select Case AnsiConsole.Prompt(
            New SelectionPrompt(Of String)() With {.Title = "Main Menu:"}.AddChoices("Embark", "Quit"))
            Case "Embark"
                Embark.Run()
                Return True
            Case "Quit"
                Return Not ConfirmQuit()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
