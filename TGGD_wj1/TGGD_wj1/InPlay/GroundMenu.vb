Imports Spectre.Console
Imports WJ1.Game

Module GroundMenu
    Private Sub PickUpItem(character As Character, itemName As String)
        Dim firstItem = character.Location.Inventory.Items.First(Function(item) item.ItemType.Name = itemName)
        character.Inventory.Add(firstItem)
        AnsiConsole.WriteLine()
        AnsiConsole.MarkupLine($"You pick up {firstItem.ItemType.Name}.")
    End Sub
    Sub Run(character As Character)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String)() With
            {
                .Title = "On the ground:"
            }
            Dim items = character.Location.Inventory.Items
            For Each item In items
                prompt.AddChoice(item.ItemType.Name)
            Next
            prompt.AddChoice("Never mind")
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case "Never mind"
                    done = True
                Case Else
                    PickUpItem(character, answer)
                    done = character.Location.Inventory.IsEmpty
            End Select
        End While
    End Sub
End Module
