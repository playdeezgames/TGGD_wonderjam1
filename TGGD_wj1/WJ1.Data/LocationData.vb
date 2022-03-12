Public Module LocationData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Locations]
            (
                [LocationId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [X] INT NOT NULL,
                [Y] INT NOT NULL,
                [Z] INT NOT NULL,
                [LocationType] INT NOT NULL,
                UNIQUE([X],[Y],[Z])
            );")
    End Sub
    Function Create(locationType As Long, x As Long, y As Long, z As Long) As Long
        Initialize()
        Using command = CreateCommand(
            "INSERT INTO [Locations]([LocationType],[X],[Y],[Z]) VALUES(@LocationType,@X,@Y,@Z);",
            MakeParameter("@LocationType", locationType),
            MakeParameter("@X", x),
            MakeParameter("@Y", y),
            MakeParameter("@Z", z))
            command.ExecuteNonQuery()
            Return LastInsertRowId
        End Using
    End Function
End Module
