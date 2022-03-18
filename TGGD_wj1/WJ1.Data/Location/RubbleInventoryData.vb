Public Module RubbleInventoryData
    Friend Sub Initialize()
        FeatureData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [RubbleInventories]
            (
                [FeatureId] INT NOT NULL UNIQUE,
                [InventoryId] INT NOT NULL UNIQUE,
                FOREIGN KEY ([FeatureId]) REFERENCES [Features]([FeatureId]),
                FOREIGN KEY ([InventoryId]) REFERENCES [Inventories]([InventoryId])
            );")
    End Sub
    Sub Write(featureId As Long, inventoryId As Long)
        Initialize()
        ExecuteNonQuery(
            "REPLACE INTO [RubbleInventories]
            (
                [FeatureId],
                [InventoryId]
            )
            VALUES
            (
                @FeatureId,
                @InventoryId
            );",
            MakeParameter("@FeatureId", featureId),
            MakeParameter("@InventoryId", inventoryId))
    End Sub
    Function Read(featureId As Long) As Long?
        Initialize()
        Read = ExecuteScalar(Of Long)(
            "SELECT [InventoryId] FROM [RubbleInventories] WHERE [FeatureId]=@FeatureId;",
            MakeParameter("@FeatureId", featureId))
    End Function

    Friend Sub Clear(featureId As Long)
        Initialize()
        ExecuteNonQuery("DELETE FROM [RubbleInventories] WHERE [FeatureId]=@FeatureId;", MakeParameter("@FeatureId", featureId))
    End Sub
End Module
