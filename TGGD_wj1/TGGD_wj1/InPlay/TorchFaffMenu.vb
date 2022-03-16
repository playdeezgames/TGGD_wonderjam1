Imports Spectre.Console
Imports WJ1.Game

Module TorchFaffMenu
    Sub Run(character As Character, item As Item)
        Dim done As Boolean
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String)() With
            {
            .Title = $"What would you like to do with {item.ItemType.Name}?"
            }
            If item.IsSwitchedOn Then
                AnsiConsole.MarkupLine($"{item.ItemType.Name} is switched on.")
                prompt.AddChoice("Switch off")
            Else
                AnsiConsole.MarkupLine($"{item.ItemType.Name} is switched off.")
                prompt.AddChoice("Switch on")
            End If
            prompt.AddChoice("Never mind")
            Select Case AnsiConsole.Prompt(prompt)
                Case "Switch on"
                    item.SwitchOn()
                    AnsiConsole.MarkupLine($"You switch {item.ItemType.Name} on.")
                Case "Switch off"
                    item.SwitchOff()
                    AnsiConsole.MarkupLine($"You switch {item.ItemType.Name} off.")
                Case "Never mind"
                    done = True
                Case Else
                    Throw New NotImplementedException
            End Select
        End While
    End Sub

End Module
