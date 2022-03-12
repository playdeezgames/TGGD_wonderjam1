Imports WJ1.Data

Public Class PlayerCharacter
    Inherits Character
    Sub New()
        MyBase.New(PlayerData.Read().Value)
    End Sub
End Class
