Public Module CharacterInventoryData
    Friend Sub Initialize()
        CharacterData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [CharacterInventories]
            (
                [CharacterId] INT NOT NULL UNIQUE,
                [InventoryId] INT NOT NULL,
                FOREIGN KEY ([CharacterId]) REFERENCES [Characters]([CharacterId]),
                FOREIGN KEY ([InventoryId]) REFERENCES [Inventories]([InventoryId])
            );")
    End Sub
    Function Read(characterId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [InventoryId] FROM [CharacterInventories] WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
    End Function
    Sub Write(characterId As Long, inventoryId As Long)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [CharacterInventories]
            (
                [CharacterId],
                [InventoryId]
            ) 
            VALUES 
            (
                @CharacterId,
                @InventoryId
            );",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@InventoryId", inventoryId))
    End Sub
End Module
