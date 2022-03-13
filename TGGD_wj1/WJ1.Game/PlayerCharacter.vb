Imports WJ1.Data

Public Class PlayerCharacter
    Inherits Character
    Sub New()
        MyBase.New(PlayerData.Read().Value)
    End Sub
    Sub Win()
        PlayerData.Write(Id, True)
    End Sub
    ReadOnly Property DidWin As Boolean
        Get
            Return PlayerData.ReadDidWin.Value
        End Get
    End Property
End Class
