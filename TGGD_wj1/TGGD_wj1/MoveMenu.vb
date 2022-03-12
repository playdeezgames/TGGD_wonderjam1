Imports Spectre.Console
Imports WJ1.Game

Module MoveMenu
    Sub Run(character As Character)
        Dim prompt As New SelectionPrompt(Of String)() With {.Title = "Which way?"}
        If character.CanMove(MoveDirection.Ahead) Then
            prompt.AddChoice("Ahead")
        End If
        If character.CanMove(MoveDirection.Left) Then
            prompt.AddChoice("Left")
        End If
        If character.CanMove(MoveDirection.Right) Then
            prompt.AddChoice("Right")
        End If
        If character.CanMove(MoveDirection.Back) Then
            prompt.AddChoice("Back")
        End If
        prompt.AddChoices("Never mind")
        Select Case AnsiConsole.Prompt(prompt)
            Case "Ahead"
                AnsiConsole.MarkupLine("You move ahead.")
                character.Move(MoveDirection.Ahead)
            Case "Left"
                AnsiConsole.MarkupLine("You move left.")
                character.Move(MoveDirection.Left)
            Case "Right"
                AnsiConsole.MarkupLine("You move right.")
                character.Move(MoveDirection.Right)
            Case "Back"
                AnsiConsole.MarkupLine("You move back.")
                character.Move(MoveDirection.Back)
            Case "Never mind"
                'do nothing
            Case Else
                Throw New NotImplementedException
        End Select

    End Sub
End Module
