Imports Spectre.Console
Imports WJ1.Game

Module PickMenu
    Sub Run(character As Character, items As List(Of Item))
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim prompt As New SelectionPrompt(Of String) With
            {
                .Title = "Which one would you like to faff with?"
            }
            Dim index = 1
            For Each item In items
                prompt.AddChoice($"{index}. {item.Name}")
                index += 1
            Next
            prompt.AddChoice("Never mind")
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case "Never mind"
                    done = True
                Case Else
                    index = CInt(answer.Split("."c).First()) - 1
                    FaffMenu.Run(character, items(index))
            End Select
        End While
    End Sub
End Module
