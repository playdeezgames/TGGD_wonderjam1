Imports Spectre.Console
Imports WJ1.Game

Module ItemMenu
    Sub Run(character As Character, itemType As ItemType)
        AnsiConsole.WriteLine()
        Dim items = character.Inventory.Items.Where(Function(item) item.ItemType = itemType).ToList
        AnsiConsole.MarkupLine($"You have {items.Count} {itemType.Name}.")
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "What do you want to do?"}
        prompt.AddChoice("Never mind")
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case "Never mind"
                'do nothing
        End Select
    End Sub
End Module
