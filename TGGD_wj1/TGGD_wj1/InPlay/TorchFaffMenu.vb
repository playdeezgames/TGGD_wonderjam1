Imports Spectre.Console
Imports WJ1.Game

Module TorchFaffMenu
    Private Sub HandleInsertBattery(character As Character, item As Item)
        Dim batteries = character.Inventory.StackedItems(ItemType.Battery)
        AnsiConsole.WriteLine()
        Dim prompt As New SelectionPrompt(Of String) With {.Title = "Which battery do you want to insert?"}
        prompt.AddChoice("Never mind")
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case "Never mind"
                'do nothing
        End Select
    End Sub
    Sub Run(character As Character, item As Item)
        Dim done As Boolean
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String)() With
            {
            .Title = $"What would you like to do with {item.Name}?"
            }
            If item.IsSwitchedOn Then
                AnsiConsole.MarkupLine($"{item.Name} is switched on.")
                prompt.AddChoice("Switch off")
            Else
                AnsiConsole.MarkupLine($"{item.Name} is switched off.")
                prompt.AddChoice("Switch on")
            End If
            If item.HasBattery Then
                AnsiConsole.MarkupLine($"{item.Name} has a battery.")
                prompt.AddChoice("Remove battery")
            Else
                AnsiConsole.MarkupLine($"{item.Name} has no battery.")
                If character.Inventory.HasItemType(ItemType.Battery) Then
                    prompt.AddChoice("Insert battery")
                End If
            End If
            prompt.AddChoice("Never mind")
            Select Case AnsiConsole.Prompt(prompt)
                Case "Switch on"
                    item.SwitchOn()
                    AnsiConsole.MarkupLine($"You switch {item.Name} on.")
                Case "Switch off"
                    item.SwitchOff()
                    AnsiConsole.MarkupLine($"You switch {item.Name} off.")
                Case "Remove battery"
                    item.RemoveBattery()
                    AnsiConsole.MarkupLine($"You remove the battery from {item.Name}.")
                Case "Insert battery"
                    HandleInsertBattery(character, item)
                Case "Never mind"
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

End Module
