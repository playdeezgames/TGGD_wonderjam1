Public Module LocationDecayData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [LocationDecay]
            (
                [LocationId] INT NOT NULL UNIQUE,
                [Decay] INT NOT NULL,
                FOREIGN KEY([LocationId]) REFERENCES [Locations]([LocationId])
            );")
    End Sub
    Function Read(locationId As Long) As Long?
        Initialize()
        Return ExecuteScalar(Of Long)("SELECT [Decay] FROM [LocationDecay] WHERE [LocationId]=@LocationId;", MakeParameter("@LocationId", locationId))
    End Function
    Sub Write(locationId As Long, decay As Long)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [LocationDecay]([LocationId],[Decay]) VALUES(@LocationId, @Decay);",
            MakeParameter("@LocationId", locationId),
            MakeParameter("@Decay", decay))
    End Sub
    Function ReadAll() As List(Of Long)
        Initialize()
        Using command = CreateCommand("SELECT [LocationId] FROM [LocationDecay];")
            Using reader = command.ExecuteReader
                Dim result As New List(Of Long)
                While reader.Read
                    result.Add(CLng(reader("LocationId")))
                End While
                Return result
            End Using
        End Using
    End Function

    Friend Sub Clear(locationId As Long)
        Initialize()
        ExecuteNonQuery("DELETE FROM [LocationDecay] WHERE [LocationId]=@LocationId;", MakeParameter("@LocationId", locationId))
    End Sub
End Module
