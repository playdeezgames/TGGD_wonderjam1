Imports System.Runtime.CompilerServices

Public Enum ItemType As Long
    Key
    Torch
End Enum
Public Module ItemTypeExtensions
    <Extension()>
    Function Name(itemType As ItemType) As String
        Select Case itemType
            Case ItemType.Key
                Return "key"
            Case ItemType.Torch
                Return "torch"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
    <Extension()>
    Function CanFaff(itemType As ItemType) As Boolean
        Select Case itemType
            Case ItemType.Torch
                Return True
            Case Else
                Return False
        End Select
    End Function
    Public ReadOnly AllItemTypes As New List(Of ItemType) From
        {
            ItemType.Key,
            ItemType.Torch
        }
End Module