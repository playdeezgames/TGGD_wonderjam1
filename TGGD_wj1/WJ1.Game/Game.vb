Imports WJ1.Data
Public Module Game
    Const BuildingXSize As Long = 7
    Const BuildingYSize As Long = 7
    Const BuildingZSize As Long = 7
    Const GroundZ As Long = 1
    Const PlayerZ As Long = 1
    Const KeyZ As Long = BuildingZSize
    Const BatteryCount As Long = 20

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
    Private Function GenerateInteriorWall(z As Long) As Boolean
        Static deltas As New List(Of Tuple(Of Long, Long)) From
            {
                New Tuple(Of Long, Long)(0, -1),
                New Tuple(Of Long, Long)(1, -1),
                New Tuple(Of Long, Long)(1, 0),
                New Tuple(Of Long, Long)(1, 1),
                New Tuple(Of Long, Long)(0, 1),
                New Tuple(Of Long, Long)(-1, 1),
                New Tuple(Of Long, Long)(-1, 0),
                New Tuple(Of Long, Long)(-1, -1)
            }
        Dim location = New Location(RNG.FromList(LocationData.ReadForZAndLocationType(z, LocationType.Floor)))
        If deltas.All(Function(delta) New Location(
                              LocationData.ReadForXYZ(
                                  LocationData.ReadX(location.Id).Value + delta.Item1,
                                  LocationData.ReadY(location.Id).Value + delta.Item2,
                                  z).Value).
                              LocationType = LocationType.Floor) Then
            LocationData.WriteLocationType(location.Id, LocationType.Wall)
            Return True
        End If
        Return False
    End Function
    Private Sub CreateFloor(z As Long)
        For x = 1 To BuildingXSize
            For y = 1 To BuildingYSize
                Dim locationType = DetermineLocationType(x, y)
                LocationData.Create(locationType, x, y, z)
            Next
        Next
        Dim retries = 0
        While retries < 10
            If GenerateInteriorWall(z) Then
                retries = 0
            Else
                retries += 1
            End If
        End While
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
        Dim featureId = FeatureData.Create(exitLocation.Id, FeatureType.BuildingExit)
        BuildingExitData.Write(featureId, True)
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
        'Dim locationId = RNG.FromList(LocationData.ReadForZAndLocationType(PlayerZ, LocationType.Floor))
        Dim locationId = RNG.FromList(LocationData.ReadForZAndLocationType(2, LocationType.Floor))
        Dim direction = RNG.FromList(AllDirections)
        Dim characterId = CharacterData.Create(CharacterType.Player, locationId, direction)
        PlayerData.Write(characterId, False)
    End Sub
    Private Sub CreateKey()
        Dim location = New Location(RNG.FromList(LocationData.ReadForZAndLocationType(KeyZ, LocationType.Floor)))
        Dim item = New Item(ItemData.Create(ItemType.Key))
        location.Inventory.Add(item)
    End Sub
    Private Sub CreateTorch()
        Dim location = New Location(RNG.FromList(LocationData.ReadForZAndLocationType(PlayerZ, LocationType.Floor)))
        Dim item = New Item(ItemData.Create(ItemType.Torch))
        TorchData.Write(item.Id, True)
        location.Inventory.Add(item)
    End Sub
    Private Sub StartDecay()
        Dim location = New Location(RNG.FromList(LocationData.ReadForZAndLocationType(BuildingZSize, LocationType.Floor)))
        LocationDecayData.Write(Location.Id, 1)
    End Sub
    Private Sub CreateBatteries()
        Dim batteryIds As New List(Of Long)
        While batteryIds.Count < BatteryCount
            Dim batteryId = ItemData.Create(ItemType.Battery)
            BatteryData.Write(batteryId, RNG.FromRange(0, 100))
            Dim location = New Location(RNG.FromList(LocationData.ReadForZAndLocationType(RNG.FromRange(1, BuildingZSize), LocationType.Floor)))
            location.Inventory.Add(New Item(batteryId))
            batteryIds.Add(batteryId)
        End While
    End Sub
    Sub Start()
        Store.Reset()
        CreateBuilding()
        StartDecay()
        CreateTorch()
        CreateBatteries()
        CreateKey()
        CreatePlayerCharacter()
    End Sub
    Sub Finish()
        Store.ShutDown()
    End Sub
    Public Event PlaySfx As Action(Of Sfx)
    Sub Play(sfx As Sfx)
        RaiseEvent PlaySfx(sfx)
    End Sub
    Private Sub RotFloor(locationId As Long)
        If LocationData.ReadZ(locationId) > GroundZ Then
            Dim location As New Location(locationId)
            Dim below = location.Below
            below.LocationType = LocationType.Floor
            Dim feature As New Feature(FeatureData.Create(below.Id, FeatureType.Rubble))
            If Not below.Decay.HasValue Then
                below.Decay = 1
            End If
            For Each rubblePile In location.Features.Where(Function(f) f.FeatureType = FeatureType.Rubble)
                For Each item In rubblePile.Inventory.Items
                    feature.Inventory.Add(item)
                Next
            Next
            For Each character In location.Characters
                character.Location = below
            Next
            For Each item In location.Inventory.Items
                feature.Inventory.Add(item)
            Next
            For Each neighbor In location.Neighbors.Where(Function(l) l IsNot Nothing AndAlso l.LocationType = LocationType.Floor)
                If Not neighbor.Decay.HasValue Then
                    neighbor.Decay = 1
                End If
            Next
            location.Destroy()
        End If
    End Sub
    Private Sub UpdateDecay()
        Dim locationIds = LocationDecayData.ReadAll()
        For Each locationId In locationIds
            Dim decay = LocationDecayData.Read(locationId).Value
            If RNG.FromRange(1, 100) < decay Then
                RotFloor(locationId)
            Else
                LocationDecayData.Write(locationId, decay + 1)
            End If
        Next
    End Sub
    Private Sub DrainBatteries()
        Dim batteryIds = TorchBatteryData.ReadAllBatteryIds()
        For Each batteryId In batteryIds
            Dim battery As New Item(batteryId)
            Dim torch As New Item(TorchBatteryData.ReadForBatteryId(batteryId).Value)
            If torch.IsLit Then
                battery.Charge -= 1
            End If
        Next
    End Sub
    Sub Update()
        UpdateDecay()
        DrainBatteries()
    End Sub
End Module
