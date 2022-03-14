Public Module LocationInventoryData
    Friend Sub Initialize()
        LocationData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [LocationInventories]
            (
                [LocationId] INT NOT NULL UNIQUE,
                [InventoryId] INT NOT NULL,
                FOREIGN KEY ([LocationId]) REFERENCES [Locations]([LocationId]),
                FOREIGN KEY ([InventoryId]) REFERENCES [Inventories]([InventoryId])
            );")
    End Sub
    Function Read(locationId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [InventoryId] FROM [LocationInventories] WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId))
    End Function
    Sub Write(locationId As Long, inventoryId As Long)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [LocationInventories]
            (
                [LocationId],
                [InventoryId]
            ) 
            VALUES 
            (
                @LocationId,
                @InventoryId
            );",
            MakeParameter("@LocationId", locationId),
            MakeParameter("@InventoryId", inventoryId))
    End Sub
End Module
