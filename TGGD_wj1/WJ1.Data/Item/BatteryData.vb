Public Module BatteryData
    Friend Sub Initialize()
        ItemData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Batteries]
            (
                [ItemId] INT NOT NULL UNIQUE,
                [Charge] INT NOT NULL,
                FOREIGN KEY ([ItemId]) REFERENCES [Items]([ItemId])
            );")
    End Sub
    Sub Write(itemId As Long, charge As Long)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [Batteries]([ItemId],[Charge]) VALUES(@ItemId, @Charge);",
            MakeParameter("@ItemId", itemId),
            MakeParameter("@Charge", charge))
    End Sub
End Module
