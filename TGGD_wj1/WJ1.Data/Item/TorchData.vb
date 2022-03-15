Public Module TorchData
    Friend Sub Initialize()
        ItemData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Torches]
            (
                [ItemId] INT NOT NULL UNIQUE,
                [SwitchState] INT NOT NULL,
                FOREIGN KEY ([ItemId]) REFERENCES [Items]([ItemId])
            );")
    End Sub
    Sub Write(itemId As Long, switchState As Boolean)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [Torches]([ItemId], [SwitchState]) VALUES(@ItemId, @SwitchState);",
            MakeParameter("@ItemId", itemId),
            MakeParameter("@SwitchState", switchState))
    End Sub
End Module
