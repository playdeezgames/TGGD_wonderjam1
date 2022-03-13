Imports WJ1.Data
Public Module Game
    Private BuildingXSize As Long = 7
    Private BuildingYSize As Long = 7
    Private BuildingZSize As Long = 7
    Private Function DetermineLocationType(x As Long, y As Long) As LocationType
        If (x = 1 AndAlso y = 1) OrElse (x = BuildingXSize AndAlso y = 1) OrElse (x = 1 AndAlso y = BuildingYSize) OrElse (x = BuildingXSize AndAlso y = BuildingYSize) Then
            Return LocationType.Corner
        ElseIf x = 1 OrElse x = BuildingXSize Then
            If y Mod 2 = 0 Then
                Return LocationType.Window
            Else
                Return LocationType.Wall
            End If
        ElseIf y = 1 OrElse y = BuildingYSize Then
            If x Mod 2 = 0 Then
                Return LocationType.Window
            Else
                Return LocationType.Wall
            End If
        Else
            Return LocationType.Floor
        End If
    End Function
    Private Sub CreateFloor(z As Long)
        For x = 1 To BuildingXSize
            For y = 1 To BuildingYSize
                Dim locationType = DetermineLocationType(x, y)
                LocationData.Create(locationType, x, y, z)
            Next
        Next
    End Sub
    Private Sub CreateStairs(z As Long)
        Dim locationId = RNG.FromList(LocationData.ReadForZAndLocationType(z, LocationType.Floor))
        FeatureData.Create(locationId, FeatureType.StairsUp)
        Dim nextLocationId = LocationData.ReadForXYZ(LocationData.ReadX(locationId).Value, LocationData.ReadY(locationId).Value, z + 1).Value
        FeatureData.Create(nextLocationId, FeatureType.StairsDown)
    End Sub
    Private Sub CreateEntrance()
        Dim location = New Location(RNG.FromList(LocationData.ReadForZAndLocationType(1, LocationType.Wall)))
        LocationData.WriteLocationType(location.Id, LocationType.Entrance)
        Dim exitLocation = RNG.FromList(location.Neighbors.Where(Function(neighbor) neighbor.LocationType = LocationType.Floor).ToList())
        FeatureData.Create(exitLocation.Id, FeatureType.BuildingExit)
    End Sub
    Private Sub CreateBuilding()
        For z = 1 To BuildingZSize
            CreateFloor(z)
        Next
        For z = 1 To BuildingZSize - 1
            CreateStairs(z)
        Next
        CreateEntrance()
    End Sub
    Private Sub CreatePlayerCharacter()
        Dim locationId = RNG.FromList(LocationData.ReadForZAndLocationType(1, LocationType.Floor))
        Dim direction = RNG.FromList(AllDirections)
        Dim characterId = CharacterData.Create(CharacterType.Player, locationId, direction)
        PlayerData.Write(characterId, False)
    End Sub
    Sub Start()
        Store.Reset()
        CreateBuilding()
        CreatePlayerCharacter()
    End Sub
    Sub Finish()
        Store.ShutDown()
    End Sub
    Public Event PlaySfx As Action(Of Sfx)
    Sub Play(sfx As Sfx)
        RaiseEvent PlaySfx(sfx)
    End Sub
End Module
