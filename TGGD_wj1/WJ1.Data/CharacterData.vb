Public Module CharacterData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Characters]
            (
                [CharacterId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [CharacterType] INT NOT NULL,
                [Direction] INT NOT NULL,
                [LocationId] INT NOT NULL
            );")
    End Sub
    Function Create(characterType As Long, locationId As Long, direction As Long) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Characters]([CharacterType],[LocationId],[Direction]) VALUES(@CharacterType,@LocationId,@Direction);",
            MakeParameter("@CharacterType", characterType),
            MakeParameter("@Direction", direction),
            MakeParameter("@LocationId", locationId))
        Return LastInsertRowId
    End Function
    Function ReadLocation(characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)("SELECT [LocationId] FROM [Characters] WHERE [CharacterId]=@CharacterId;", MakeParameter("@CharacterId", characterId))
    End Function
    Function ReadDirection(characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)("SELECT [Direction] FROM [Characters] WHERE [CharacterId]=@CharacterId;", MakeParameter("@CharacterId", characterId))
    End Function
    Sub WriteDirection(characterId As Long, direction As Long)
        Initialize()
        ExecuteNonQuery(
            "UPDATE [Characters] SET [Direction]=@Direction WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@Direction", direction))
    End Sub
End Module
