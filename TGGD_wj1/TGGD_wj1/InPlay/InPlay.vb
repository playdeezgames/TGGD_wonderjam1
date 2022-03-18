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
    Private Function CreatePrompt(character As Character, canSee As Boolean) As IPrompt(Of String)
        Dim result As New SelectionPrompt(Of String)() With
                {
                    .Title = "What Next?"
                }
        If character.MayMove Then
            result.AddChoices("Move...")
        Else
            result.AddChoice("Wait")
        End If
        result.AddChoices("Turn...")
        If Not character.Inventory.IsEmpty Then
            result.AddChoice("Inventory...")
        End If
        If canSee Then
            If character.Location.Features.Any Then
                result.AddChoice("Interact...")
            End If
            If Not character.Location.Inventory.IsEmpty Then
                result.AddChoice("Ground...")
            End If
        Else
            result.AddChoice("Search area")
        End If
        result.AddChoices("Menu...")
        Return result
    End Function
    Sub Run()
        Dim done = False
        Dim character As New PlayerCharacter()
        Dim searchedLocation As Location = Nothing
        While Not done
            Dim location = character.Location
            AnsiConsole.WriteLine()
            Dim canSee = location.IsLit OrElse (searchedLocation IsNot Nothing AndAlso searchedLocation.Id = location.Id)
            If canSee Then
                AnsiConsole.MarkupLine($"Here: {location.LocationType.Name}")
                If location.Decay.HasValue Then
                    AnsiConsole.MarkupLine($"Decay: {location.Decay.Value}%")
                End If
                Dim features = location.Features
                If features.Any Then
                    AnsiConsole.MarkupLine($"[teal]Features: {String.Join(",", features.Select(Of String)(Function(feature) feature.FeatureType.Name))}[/]")
                End If
                If Not location.Inventory.IsEmpty Then
                    AnsiConsole.MarkupLine("There is stuff on the ground.")
                End If
                Dim aheadLocation = character.GetNextLocation(MoveDirection.Ahead)
                AnsiConsole.MarkupLine($"Ahead: {aheadLocation.LocationType.Name}")
            Else
                AnsiConsole.MarkupLine("It is too dark to see anything.")
            End If
            Select Case AnsiConsole.Prompt(CreatePrompt(character, canSee))
                Case "Menu..."
                    done = HandleGameMenu()
                    If done Then
                        AnsiConsole.WriteLine()
                        AnsiConsole.MarkupLine("You abandon the game.")
                    End If
                Case "Turn..."
                    TurnMenu.Run(character)
                Case "Inventory..."
                    InventoryMenu.Run(character)
                Case "Ground..."
                    GroundMenu.Run(character)
                Case "Move..."
                    MoveMenu.Run(character)
                Case "Wait"
                    AnsiConsole.MarkupLine("You wait a moment.")
                    Game.Update()
                Case "Search area"
                    searchedLocation = location
                    Game.Update()
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
