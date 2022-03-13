Imports Spectre.Console
Imports WJ1.Game

Module TurnMenu
    Sub Run(character As Character)
        Select Case AnsiConsole.Prompt(
            New SelectionPrompt(Of String)() With {.Title = "Which way?"}.
            AddChoices("Left", "Right", "Around", "Never mind"))
            Case "Left"
                AnsiConsole.MarkupLine("You turn left.")
                character.Turn(TurnDirection.Left)
            Case "Right"
                AnsiConsole.MarkupLine("You turn right.")
                character.Turn(TurnDirection.Right)
            Case "Around"
                AnsiConsole.MarkupLine("You turn around.")
                character.Turn(TurnDirection.Around)
            Case "Never mind"
                'do nothing
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
End Module
