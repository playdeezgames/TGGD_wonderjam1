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
    Function ReadBattery(itemId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [BatteryId] FROM [TorchBatteries] WHERE [ItemId]=@ItemId;",
            MakeParameter("@ItemId", itemId))
    End Function
End Module
