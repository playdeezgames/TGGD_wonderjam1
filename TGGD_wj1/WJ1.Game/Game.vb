Imports WJ1.Data
Public Module Game
    Private BuildingXSize As Long = 11
    Private BuildingYSize As Long = 11
    Private BuildingZSize As Long = 7
    Private Function DetermineLocationType(x As Long, y As Long) As LocationType
        If x = 1 OrElse x = BuildingXSize Then
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
    Private Sub CreateBuilding()
        For z = 1 To BuildingZSize
            CreateFloor(z)
        Next
    End Sub
    Private Sub CreatePlayerCharacter()
        Dim locationId = RNG.FromList(LocationData.ReadForZAndLocationType(BuildingZSize, LocationType.Floor))
        Dim characterId = CharacterData.Create(CharacterType.Player, locationId)
        PlayerData.Write(characterId)
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
