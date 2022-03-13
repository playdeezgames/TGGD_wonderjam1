Imports WJ1.Data

Public Class Feature
    ReadOnly Property Id As Long
    Sub New(featureId As Long)
        Id = featureId
    End Sub
    ReadOnly Property FeatureType As FeatureType
        Get
            Return CType(FeatureData.ReadFeatureType(Id).Value, FeatureType)
        End Get
    End Property
End Class
