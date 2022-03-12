Imports Spectre.Console
Imports WJ1.Game

Module InPlay
    Private Function HandleGameMenu() As Boolean
        Select Case AnsiConsole.Prompt(
            New SelectionPrompt(Of String)() With
            {
                .Title = "Game Menu"
            }.
            AddChoices(
                "Resume",
                "Abandon"))
            Case "Abandon"
                Return AnsiConsole.Confirm("Are you sure you want to abandon the game?", False)
            Case "Resume"
                Return False
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Sub Run()
        Dim done = False
        Dim character As New PlayerCharacter()
        While Not done
            Dim location = character.Location
            AnsiConsole.WriteLine()
            AnsiConsole.MarkupLine($"CharacterId: {character.Id}")
            AnsiConsole.MarkupLine($"Terrain: {location.LocationType.Name}")
            Select Case AnsiConsole.Prompt(New SelectionPrompt(Of String)() With {.Title = "What Next?"}.AddChoices("Menu"))
                Case "Menu"
                    done = HandleGameMenu()
                    If done Then
                        AnsiConsole.WriteLine()
                        AnsiConsole.MarkupLine("You abandon the game.")
                    End If
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
