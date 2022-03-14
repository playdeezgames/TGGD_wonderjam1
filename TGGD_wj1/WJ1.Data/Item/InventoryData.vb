Public Module InventoryData
    Friend Sub Initialize()
        ExecuteNonQuery("CREATE TABLE IF NOT EXISTS [Inventories]([InventoryId] INTEGER PRIMARY KEY AUTOINCREMENT);")
    End Sub
    Function Create() As Long
        Initialize()
        ExecuteNonQuery("INSERT INTO [Inventories] DEFAULT VALUES;")
        Return LastInsertRowId
    End Function
End Module
