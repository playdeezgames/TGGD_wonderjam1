Public Module PlayerData
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Players]
            (
                [PlayerId] INT NOT NULL UNIQUE, 
                [CharacterId] INT NOT NULL,
                [DidWin] INT NOT NULL,
                CHECK([PlayerId]=1),
                FOREIGN KEY ([CharacterId]) REFERENCES [Characters]([CharacterId])
            );")
    End Sub
    Function Read() As Long?
        Initialize()
        Return ExecuteScalar(Of Long)("SELECT [CharacterId] FROM [Players];")
    End Function
    Sub Write(characterId As Long, didWin As Boolean)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [Players]
            (
                [PlayerId],
                [CharacterId],
                [DidWin]
            ) 
            VALUES 
            (
                1, 
                @CharacterId,
                @DidWin
            );",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@DidWin", didWin))
    End Sub
    Function ReadDidWin() As Boolean?
        Initialize()
        Dim value = ExecuteScalar(Of Long)("SELECT [DidWin] FROM [Players];")
        If value.HasValue Then
            Return value.Value <> 0
        End If
        Return Nothing
    End Function
End Module
