Imports WJ1.Data

Public Class Item
    ReadOnly Property Id As Long
    Sub New(itemId As Long)
        Id = itemId
    End Sub
    ReadOnly Property ItemType As ItemType
        Get
            Return CType(ItemData.ReadItemType(Id).Value, ItemType)
        End Get
    End Property
    ReadOnly Property IsLit As Boolean
        Get
            'TODO: if it is a torch and the torch is lit and the torch has non-dead batteries, it is lit
            Return False
        End Get
    End Property
    ReadOnly Property IsSwitchedOn As Boolean
        Get
            Select Case ItemType
                Case ItemType.Torch
                    Return TorchData.Read(Id).Value
                Case Else
                    Return False
            End Select
        End Get
    End Property
    Sub SwitchOn()
        Select Case ItemType
            Case ItemType.Torch
                TorchData.Write(Id, True)
        End Select
    End Sub
    Sub SwitchOff()
        Select Case ItemType
            Case ItemType.Torch
                TorchData.Write(Id, False)
        End Select
    End Sub
    ReadOnly Property Name As String
        Get
            Return ItemType.Name
        End Get
    End Property
    ReadOnly Property HasBattery As Boolean
        Get
            Select Case ItemType
                Case ItemType.Torch
                    Return TorchBatteryData.ReadBattery(Id).HasValue
                Case Else
                    Return False
            End Select
        End Get
    End Property
    Sub RemoveBattery()
        Throw New NotImplementedException
    End Sub
End Class
