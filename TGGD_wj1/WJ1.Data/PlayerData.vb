Public Module PlayerData
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Players]
            (
                [PlayerId] INT NOT NULL UNIQUE, 
                [CharacterId] INT NOT NULL,
                CHECK([PlayerId]=1),
                FOREIGN KEY ([CharacterId]) REFERENCES [Characters]([CharacterId])
            );")
    End Sub
    Function Read() As Long?
        Initialize()
        Return ExecuteScalar(Of Long)("SELECT [CharacterId] FROM [Players];")
    End Function
    Sub Write(characterId As Long)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [Players]
            (
                [PlayerId],
                [CharacterId]
            ) 
            VALUES 
            (
                1, 
                @CharacterId
            );",
            MakeParameter("@CharacterId", characterId))
    End Sub
End Module
