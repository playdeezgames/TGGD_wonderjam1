Imports WJ1.Data

Public Class Character
    ReadOnly Property Id As Long
    Sub New(characterId As Long)
        Id = characterId
    End Sub
    Property Location As Location
        Get
            Return New Location(CharacterData.ReadLocationId(Id).Value)
        End Get
        Set(value As Location)
            CharacterData.WriteLocationId(Id, value.Id)
        End Set
    End Property
    Function GetNextLocation(moveDirection As MoveDirection) As Location
        Dim direction = CType(CharacterData.ReadDirection(Id).Value, Direction).RelativeDirection(moveDirection)
        Return Location.GetNeighbor(direction)
    End Function
    Sub Turn(turnDirection As TurnDirection)
        Dim direction = CType(CharacterData.ReadDirection(Id).Value, Direction)
        Select Case turnDirection
            Case TurnDirection.Around
                CharacterData.WriteDirection(Id, direction.OppositeDirection)
            Case TurnDirection.Left
                CharacterData.WriteDirection(Id, direction.PreviousDirection)
            Case TurnDirection.Right
                CharacterData.WriteDirection(Id, direction.NextDirection)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
    Sub Move(moveDirection As MoveDirection)
        If CanMove(moveDirection) Then
            CharacterData.WriteLocationId(Id, GetNextLocation(moveDirection).Id)
        End If
    End Sub
    Function CanMove(moveDirection As MoveDirection) As Boolean
        Return GetNextLocation(moveDirection).LocationType = LocationType.Floor
    End Function
End Class
