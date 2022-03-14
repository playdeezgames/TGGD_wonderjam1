Imports System.Runtime.CompilerServices

Public Enum ItemType As Long
    Key
End Enum
Public Module ItemTypeExtensions
    <Extension()>
    Function Name(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.Key
                Return "key"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module