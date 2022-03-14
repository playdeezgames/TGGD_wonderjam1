Public Module ItemData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Items]
            (
                [ItemId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [ItemType] INT NOT NULL
            );")
    End Sub
    Function ReadItemType(itemId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [ItemType] FROM [Items] WHERE [ItemId]=@ItemId;",
            MakeParameter("@ItemId", itemId))
    End Function
End Module
