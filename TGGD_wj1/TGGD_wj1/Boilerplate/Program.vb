Imports Spectre.Console

Module Program
    Sub Main(args As String())
        Console.Title = "A Game in VB.NET About Building, Decay, and Light"
        AnsiConsole.MarkupLine("[gray]Welcome to[/]")
        AnsiConsole.MarkupLine("[blue]A Game in VB.NET About Building, Decay, and Light[/]")
        AnsiConsole.MarkupLine("[gray]A Presentation of TheGrumpyGameDev[/]")
        While MainMenu.Run()

        End While
    End Sub
End Module
