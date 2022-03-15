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
    Function Read(itemId As Long) As Boolean?
        Initialize()
        Dim result = ExecuteScalar(Of Long)(
            "SELECT [SwitchState] FROM [Torches] WHEREN [ItemId]=@ItemId;",
            MakeParameter("@ItemId", itemId))
        If result.HasValue Then
            Return CBool(result.Value)
        End If
        Return Nothing
    End Function
End Module
