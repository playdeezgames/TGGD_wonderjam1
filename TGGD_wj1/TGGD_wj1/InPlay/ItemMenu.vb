Imports Spectre.Console
Imports WJ1.Game

Module ItemMenu
    Private Sub DropOne(character As Character, item As Item)
        AnsiConsole.MarkupLine($"You drop {item.Name}.")
        character.Location.Inventory.Add(item)
    End Sub
    Private Sub DropAll(character As Character, items As List(Of Item))
        For Each item In items
            DropOne(character, item)
        Next
    End Sub
    Sub Run(character As Character, itemType As ItemType)
        Dim done = False
        While Not done
            AnsiConsole.WriteLine()
            Dim items = character.Inventory.Items.Where(Function(item) item.ItemType = itemType).ToList
            AnsiConsole.MarkupLine($"You have {items.Count} {itemType.Name}.")
            Dim prompt As New SelectionPrompt(Of String) With {.Title = "What do you want to do?"}
            If itemType.CanFaff Then
                If items.Count = 1 Then
                    prompt.AddChoice("Faff with")
                Else
                    prompt.AddChoice("Faff with...")
                End If
            End If
            Select Case items.Count
                Case 1
                    prompt.AddChoice("Drop")
                Case Else
                    prompt.AddChoice("Drop one")
                    prompt.AddChoice("Drop all")
            End Select
            prompt.AddChoice("Never mind")
            Dim answer = AnsiConsole.Prompt(prompt)
            Select Case answer
                Case "Faff with"
                    FaffMenu.Run(character, items.Single())
                Case "Faff with..."
                    PickMenu.Run(character, items)
                Case "Drop one"
                    DropOne(character, items.First)
                Case "Drop", "Drop all"
                    DropAll(character, items)
                    done = True
                Case "Never mind"
                    done = True
            End Select
        End While
    End Sub
End Module
