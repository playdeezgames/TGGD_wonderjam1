Imports System.Runtime.CompilerServices

Public Enum LocationType As Long
    None
    Corner
    Wall
    Window
    Floor
    Entrance
End Enum
Public Module LocationTypeExtensions
    <Extension()>
    Function Name(locationType As LocationType) As String
        Select Case locationType
            Case LocationType.None
                Return ""
            Case LocationType.Floor
                Return "floor"
            Case LocationType.Corner
                Return "corner"
            Case LocationType.Wall
                Return "wall"
            Case LocationType.Window
                Return "window"
            Case LocationType.Entrance
                Return "entrance"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module
