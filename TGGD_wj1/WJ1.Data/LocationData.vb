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
    Function ReadForZAndLocationType(z As Long, locationType As Long) As List(Of Long)
        Initialize()
        Using command = CreateCommand(
            "SELECT [LocationId] FROM [Locations] WHERE [Z]=@Z AND [LocationType]=@LocationType;",
            MakeParameter("@Z", z),
            MakeParameter("@LocationType", locationType))
            Using reader = command.ExecuteReader
                Dim result As New List(Of Long)
                While reader.Read
                    result.Add(CLng(reader("LocationId")))
                End While
                Return result
            End Using
        End Using
    End Function
    Function ReadLocationType(locationId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [LocationType] FROM [Locations] WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId))
    End Function
End Module
