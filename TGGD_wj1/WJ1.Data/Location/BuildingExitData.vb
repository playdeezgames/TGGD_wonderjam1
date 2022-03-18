Public Module BuildingExitData
    Friend Sub Initialize()
        FeatureData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [BuildingExits]
            (
                [FeatureId] INT NOT NULL UNIQUE,
                [IsLocked] INT NOT NULL,
                FOREIGN KEY ([FeatureId]) REFERENCES [Features]([FeatureId])
            );")
    End Sub
    Sub Write(featureId As Long, isLocked As Boolean)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [BuildingExits]([FeatureId],[IsLocked]) VALUES(@FeatureId,@IsLocked);",
            MakeParameter("@FeatureId", featureId),
            MakeParameter("@IsLocked", isLocked))
    End Sub
    Function Read(featureId As Long) As Boolean?
        Initialize()
        Dim result = ExecuteScalar(Of Long)(
            "SELECT [IsLocked] FROM [BuildingExits] WHERE [FeatureId]=@FeatureId;",
            MakeParameter("@FeatureId", featureId))
        If result.HasValue Then
            Return CBool(result.Value)
        End If
        Return Nothing
    End Function

    Sub Clear(featureId As Long)
        Initialize()
        ExecuteNonQuery("DELETE FROM [BuildingExits] WHERE [FeatureId]=@FeatureId;", MakeParameter("@FeatureId", featureId))
    End Sub
End Module
