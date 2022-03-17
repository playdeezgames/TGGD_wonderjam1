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
    Function ReadAllBatteryIds() As List(Of Long)
        Initialize()
        Using command = CreateCommand("SELECT [BatteryId] FROM [TorchBatteries];")
            Using reader = command.ExecuteReader
                Dim result As New List(Of Long)
                While reader.Read
                    result.Add(CLng(reader("BatteryId")))
                End While
                Return result
            End Using
        End Using
    End Function
    Function ReadForBatteryId(batteryId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [ItemId] FROM [TorchBatteries] WHERE [BatteryId]=@BatteryId;",
            MakeParameter("@BatteryId", batteryId))
    End Function
End Module
