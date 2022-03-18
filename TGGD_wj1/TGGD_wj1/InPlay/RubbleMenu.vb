Imports Spectre.Console
Imports WJ1.Game

Module RubbleMenu
    Private Sub PickUpItem(character As Character, feature As Feature, itemName As String)
        Dim firstItem = feature.Inventory.Items.First(Function(item) item.Name = itemName)
        character.Inventory.Add(firstItem)
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine($"You pick up {firstItem.Name}.")
    End Sub
    Sub Run(character As Character, feature As Feature)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String)() With
            {
                .Title = "In the rubble:"
            }
            Dim items = feature.Inventory.Items
            For Each item In items
                prompt.AddChoice(item.Name)
            Next
            prompt.AddChoice("Never mind")
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case "Never mind"
                    done = True
                Case Else
                    PickUpItem(character, feature, answer)
                    done = character.Location.Inventory.IsEmpty
            End Select
        End While
    End Sub

End Module
