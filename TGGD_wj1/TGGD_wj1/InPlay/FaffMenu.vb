Imports Spectre.Console
Imports WJ1.Game

Module FaffMenu
    Sub Run(character As Character, item As Item)
        Select Case item.ItemType
            Case ItemType.Torch
                TorchFaffMenu.Run(character, item)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
End Module
