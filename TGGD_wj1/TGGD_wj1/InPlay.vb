Imports Spectre.Console
Imports WJ1.Game
Imports WJ1.Data

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
            Case "Debug Save"
                Store.Save("debug.db")
                Return False
            Case "Resume"
                Return False
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    Private Function CreatePrompt(character As Character) As IPrompt(Of String)
        Dim result As New SelectionPrompt(Of String)() With
                {
                    .Title = "What Next?"
                }
        result.AddChoices("Move...")
        result.AddChoices("Turn...")
        If character.Location.Features.Any Then
            result.AddChoice("Interact...")
        End If
        result.AddChoices("Menu...")
        Return result
    End Function
    Sub Run()
        Dim done = False
        Dim character As New PlayerCharacter()
        While Not done
            Dim location = character.Location
            AnsiConsole.WriteLine()
            AnsiConsole.MarkupLine($"Here: {location.LocationType.Name}")
            Dim features = location.Features
            If features.Any Then
                AnsiConsole.MarkupLine($"[teal]Features: {String.Join(",", features.Select(Of String)(Function(feature) feature.FeatureType.Name))}[/]")
            End If
            Dim aheadLocation = character.GetNextLocation(MoveDirection.Ahead)
            AnsiConsole.MarkupLine($"Ahead: {aheadLocation.LocationType.Name}")
            Select Case AnsiConsole.Prompt(CreatePrompt(character))
                Case "Menu..."
                    done = HandleGameMenu()
                    If done Then
                        AnsiConsole.WriteLine()
                        AnsiConsole.MarkupLine("You abandon the game.")
                    End If
                Case "Turn..."
                    TurnMenu.Run(character)
                Case "Move..."
                    MoveMenu.Run(character)
                Case "Interact..."
                    InteractMenu.Run(character)
                    If character.DidWin Then
                        AnsiConsole.MarkupLine("[green]You Win![/]")
                        done = True
                    End If
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub
End Module
