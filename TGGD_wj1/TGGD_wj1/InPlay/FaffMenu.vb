Imports Spectre.Console
Imports WJ1.Game

Module FaffMenu
    Private Sub RunTorch(character As Character, item As Item)
        AnsiConsole.WriteLine()
        Dim prompt As New SelectionPrompt(Of String)() With
            {
            .Title = $"What would you like to do with {item.ItemType.Name}"
            }
        If item.IsSwitchedOn Then
            AnsiConsole.MarkupLine($"{item.ItemType.Name} is switched on.")
        Else
            AnsiConsole.MarkupLine($"{item.ItemType.Name} is switched off.")
        End If
        prompt.AddChoice("Never mind")
        Select Case AnsiConsole.Prompt(prompt)
            Case "Never mind"
                'do nothing!
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
    Sub Run(character As Character, item As Item)
        Select Case item.ItemType
            Case ItemType.Torch
                RunTorch(character, item)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
End Module
