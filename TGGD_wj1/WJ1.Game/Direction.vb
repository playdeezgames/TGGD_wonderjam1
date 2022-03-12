Imports System.Runtime.CompilerServices

Public Enum Direction
    North
    East
    South
    West
End Enum
Public Module DirectionExtensions
    Public ReadOnly AllDirections As New List(Of Direction) From {Direction.East, Direction.North, Direction.South, Direction.West}
    <Extension>
    Function NextX(direction As Direction, x As Long) As Long
        Select Case direction
            Case Direction.East
                Return x + 1
            Case Direction.West
                Return x - 1
            Case Else
                Return x
        End Select
    End Function
    <Extension>
    Function NextY(direction As Direction, y As Long) As Long
        Select Case direction
            Case Direction.South
                Return y + 1
            Case Direction.North
                Return y - 1
            Case Else
                Return y
        End Select
    End Function
    <Extension>
    Function OppositeDirection(direction As Direction) As Direction
        Select Case direction
            Case Direction.North
                Return Direction.South
            Case Direction.East
                Return Direction.West
            Case Direction.South
                Return Direction.North
            Case Direction.West
                Return Direction.East
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Function NextDirection(direction As Direction) As Direction
        Select Case direction
            Case Direction.North
                Return Direction.East
            Case Direction.East
                Return Direction.South
            Case Direction.South
                Return Direction.West
            Case Direction.West
                Return Direction.North
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Function PreviousDirection(direction As Direction) As Direction
        Select Case direction
            Case Direction.North
                Return Direction.West
            Case Direction.East
                Return Direction.North
            Case Direction.South
                Return Direction.East
            Case Direction.West
                Return Direction.South
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension>
    Function RelativeDirection(direction As Direction, moveDirection As MoveDirection) As Direction
        Select Case moveDirection
            Case MoveDirection.Ahead
                Return direction
            Case MoveDirection.Back
                Return direction.OppositeDirection
            Case MoveDirection.Right
                Return direction.NextDirection
            Case MoveDirection.Left
                Return direction.PreviousDirection
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
