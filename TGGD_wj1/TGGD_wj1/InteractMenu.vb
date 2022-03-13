Imports Spectre.Console
Imports WJ1.Game

Module InteractMenu
    Private Sub GoUpStairs(character As Character)
        AnsiConsole.MarkupLine("You go up the stairs.")
        character.Location = character.Location.Above
    End Sub
    Private Sub GoDownStairs(character As Character)
        AnsiConsole.MarkupLine("You go down the stairs.")
        character.Location = character.Location.Below
    End Sub
    Private Sub InteractWithFeature(character As Character, feature As Feature)
        Select Case feature.FeatureType
            Case FeatureType.StairsUp
                GoUpStairs(character)
            Case FeatureType.StairsDown
                GoDownStairs(character)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
    Sub Run(character As Character)
        Dim prompt As New SelectionPrompt(Of String)() With
            {
                .Title = "Game Menu"
            }
        Dim features = character.Location.Features
        prompt.AddChoices(features.Select(Function(x) x.FeatureType.Name))
        prompt.AddChoice("Never mind")
        Dim answer = AnsiConsole.Prompt(prompt)
        Select Case answer
            Case "Never mind"
                'do nothing
            Case Else
                Dim feature = features.Single(Function(x) x.FeatureType.Name = answer)
                InteractWithFeature(character, feature)
        End Select
    End Sub
End Module
