Imports WJ1.Data
Public Module Game
    Private BuildingXSize = 11
    Private BuildingYSize = 11
    Private BuildingZSize = 7
    Private Sub CreateBuilding()
        For z = 1 To BuildingZSize

        Next
    End Sub
    Sub Start()
        Store.Reset()
        CreateBuilding()
    End Sub
    Sub Finish()
        Store.ShutDown()
    End Sub
    Public Event PlaySfx As Action(Of Sfx)
    Sub Play(sfx As Sfx)
        RaiseEvent PlaySfx(sfx)
    End Sub
End Module
