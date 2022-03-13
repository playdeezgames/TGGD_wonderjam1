Public Module FeatureData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Features]
            (
                [FeatureId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [LocationId] INT NOT NULL,
                [FeatureType] INT NOT NULL,
                UNIQUE([LocationId],[FeatureType]),
                FOREIGN KEY ([LocationId]) REFERENCES [Locations]([LocationId])
            );")
    End Sub
    Function ReadForLocationId(locationId As Long) As List(Of Long)
        Initialize()
        Using command = CreateCommand("SELECT [FeatureId] FROM [Features] WHERE [LocationId]=@LocationId;", MakeParameter("@LocationId", locationId))
            Using reader = command.ExecuteReader
                Dim result As New List(Of Long)
                While reader.Read
                    result.Add(CLng(reader("FeatureId")))
                End While
                Return result
            End Using
        End Using
    End Function
    Function ReadFeatureType(featureId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)(
            "SELECT [FeatureType] FROM [Features] WHERE [FeatureId]=@FeatureId;",
            MakeParameter("@FeatureId", featureId))
    End Function
    Function Create(locationId As Long, featureType As Long) As Long
        Initialize()
        ExecuteNonQuery(
            "INSERT INTO [Features]([LocationId],[FeatureType]) VALUES(@LocationId,@FeatureType);",
            MakeParameter("@LocationId", locationId),
            MakeParameter("@FeatureType", featureType))
        Return LastInsertRowId
    End Function
End Module
