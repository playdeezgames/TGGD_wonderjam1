Public Module TorchBatteryData
    Friend Sub Initialize()
        TorchData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [TorchBatteries]
            (
                [ItemId] INT NOT NULL UNIQUE,
                [BatteryId] INT NOT NULL UNIQUE,
                FOREIGN KEY ([ItemId]) REFERENCES [Torches]([ItemId])
            );")
    End Sub
    Function Read(itemId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [BatteryId] FROM [TorchBatteries] WHERE [ItemId]=@ItemId;",
            MakeParameter("@ItemId", itemId))
    End Function
    Sub Write(itemId As Long, batteryId As Long)
        Initialize()
        ExecuteNonQuery("REPLACE INTO [TorchBatteries] ([ItemId],[BatteryId]) VALUES (@ItemId, @BatteryId);", MakeParameter("@ItemId", itemId), MakeParameter("@BatteryId", batteryId))
    End Sub
    Sub Clear(itemId As Long)
        Initialize()
        ExecuteNonQuery("DELETE FROM [TorchBatteries] WHERE [ItemId]=@ItemId;", MakeParameter("@ItemId", itemId))
    End Sub
End Module
