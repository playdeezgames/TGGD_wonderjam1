Imports WJ1.Data

Public Class Location
    ReadOnly Property Id As Long
    Sub New(locationId As Long)
        Id = locationId
    End Sub
    ReadOnly Property LocationType As LocationType
        Get
            Return CType(LocationData.ReadLocationType(Id).Value, LocationType)
        End Get
    End Property
    Function GetNeighbor(direction As Direction) As Location
        Dim z = LocationData.ReadZ(Id).Value
        Dim x = direction.NextX(LocationData.ReadX(Id).Value)
        Dim y = direction.NextY(LocationData.ReadY(Id).Value)
        Dim neighborId = LocationData.ReadForXYZ(x, y, z)
        If neighborId.HasValue Then
            Return New Location(neighborId.Value)
        End If
        Return Nothing
    End Function
    ReadOnly Property Features As List(Of Feature)
        Get
            Return FeatureData.ReadForLocationId(Id).Select(
                Function(featureId) New Feature(featureId)).
                ToList()
        End Get
    End Property
End Class
