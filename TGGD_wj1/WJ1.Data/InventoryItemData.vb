Public Module InventoryItemData
    Friend Sub Initialize()
        InventoryData.Initialize()
        ItemData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [InventoryItems]
            (
                [InventoryId] INT NOT NULL,
                [ItemId] INT NOT NULL UNIQUE,
                FOREIGN KEY ([InventoryId]) REFERENCES [Inventories]([InventoryId]),
                FOREIGN KEY ([ItemId]) REFERENCES [Items]([ItemId])
            );")
    End Sub
    Function ReadForInventoryId(inventoryId As Long) As List(Of Long)
        Initialize()
        Using command = CreateCommand(
            "SELECT [ItemId] FROM [InventoryItems] WHERE [InventoryId]=@InventoryId;",
            MakeParameter("@InventoryId", inventoryId))
            Using reader = command.ExecuteReader
                Dim result As New List(Of Long)
                While reader.Read
                    result.Add(CLng(reader("ItemId")))
                End While
                Return result
            End Using
        End Using
    End Function
    Sub Write(inventoryId As Long, itemId As Long)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [InventoryItems]([InventoryId],[ItemId]) VALUES(@InventoryId,@ItemId);",
            MakeParameter("@InventoryId", inventoryId),
            MakeParameter("@ItemId", itemId))
    End Sub
End Module
