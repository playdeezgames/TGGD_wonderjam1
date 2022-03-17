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
    Function ReadLocationId(characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)("SELECT [LocationId] FROM [Characters] WHERE [CharacterId]=@CharacterId;", MakeParameter("@CharacterId", characterId))
    End Function
    Sub WriteLocationId(characterId As Long, locationId As Long)
        Initialize()
        ExecuteNonQuery(
            "UPDATE [Characters] SET [LocationId]=@LocationId WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@LocationId", locationId))
    End Sub
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
    Function ReadForLocationId(locationId As Long) As List(Of Long)
        Initialize()
        Using command = CreateCommand("SELECT [CharacterId] FROM [Characters] WHERE [LocationId]=@LocationId;", MakeParameter("@LocationId", locationId))
            Using reader = command.ExecuteReader
                ReadForLocationId = New List(Of Long)
                While reader.Read
                    ReadForLocationId.Add(CLng(reader("CharacterId")))
                End While
            End Using
        End Using
    End Function
End Module
