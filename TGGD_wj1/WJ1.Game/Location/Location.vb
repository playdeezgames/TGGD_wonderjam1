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
    ReadOnly Property Decay As Long?
        Get
            Return LocationDecayData.Read(Id)
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
    ReadOnly Property Neighbors As List(Of Location)
        Get
            Return AllDirections.
                Select(Function(direction) GetNeighbor(direction)).
                Where(Function(neighbor) neighbor IsNot Nothing).
                ToList
        End Get
    End Property
    ReadOnly Property Features As List(Of Feature)
        Get
            Return FeatureData.ReadForLocationId(Id).Select(
                Function(featureId) New Feature(featureId)).
                ToList()
        End Get
    End Property
    ReadOnly Property Above As Location
        Get
            Dim locationId = LocationData.ReadForXYZ(LocationData.ReadX(Id).Value, LocationData.ReadY(Id).Value, LocationData.ReadZ(Id).Value + 1)
            If locationId.HasValue Then
                Return New Location(locationId.Value)
            End If
            Return Nothing
        End Get
    End Property
    ReadOnly Property Below As Location
        Get
            Dim locationId = LocationData.ReadForXYZ(LocationData.ReadX(Id).Value, LocationData.ReadY(Id).Value, LocationData.ReadZ(Id).Value - 1)
            If locationId.HasValue Then
                Return New Location(locationId.Value)
            End If
            Return Nothing
        End Get
    End Property
    ReadOnly Property Inventory As Inventory
        Get
            Dim inventoryId = LocationInventoryData.Read(Id)
            If Not inventoryId.HasValue Then
                inventoryId = InventoryData.Create()
                LocationInventoryData.Write(Id, inventoryId.Value)
            End If
            Return New Inventory(inventoryId.Value)
        End Get
    End Property
    ReadOnly Property LightLevel As Integer
        Get
            'TODO: any lit torch in a character's hand increases light level by 1
            'TODO: any lit torch on the ground increases light level by 1
            Return Neighbors.AsEnumerable.Count(Function(neighbor)
                                                    Return neighbor.LocationType = LocationType.Window
                                                End Function)
        End Get
    End Property
End Class
