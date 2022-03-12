Imports WJ1.Data

Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    ReadOnly Property Location As Location
        Get
            Return New Location(CharacterData.ReadLocation(Id).Value)
        End Get
    End Property
    Function GetNextLocation(moveDirection As MoveDirection) As Location
        Dim direction = CType(CharacterData.ReadDirection(Id).Value, Direction).RelativeDirection(moveDirection)
        Return Location.GetNeighbor(direction)
    End Function
End Class
