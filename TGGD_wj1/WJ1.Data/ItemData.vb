Public Module ItemData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Items]
            (
                [ItemId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [ItemType] INT NOT NULL
            );")
    End Sub
    Function Create(itemType As Long) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Items]([ItemType]) VALUES(@ItemType);",
            MakeParameter("@ItemType", itemType))
        Return LastInsertRowId
    End Function
    Function ReadItemType(itemId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [ItemType] FROM [Items] WHERE [ItemId]=@ItemId;",
            MakeParameter("@ItemId", itemId))
    End Function
End Module
