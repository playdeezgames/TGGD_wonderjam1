Imports Spectre.Console
Imports WJ1.Game

Module InventoryMenu
    Private Sub HandleItemName(character As Character, itemName As String)
        Dim result = AllItemTypes.Single(Function(itemType) itemType.Name = itemName)
        ItemMenu.Run(character, result)
    End Sub
    Sub Run(character As Character)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String)() With
                {
                .Title = "Inventory:"
                }
            Dim items = character.Inventory.StackedItems
            For Each item In items
                prompt.AddChoice(item.Key.Name)
            Next
            prompt.AddChoice("Never mind")
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case "Never mind"
                    done = True
                Case Else
                    HandleItemName(character, answer)
                    done = character.Inventory.IsEmpty
            End Select
        End While
    End Sub
End Module
