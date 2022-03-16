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
                    Return TorchBatteryData.Read(Id).HasValue
                Case Else
                    Return False
            End Select
        End Get
    End Property
    Sub RemoveBattery()
        TorchBatteryData.Clear(Id)
    End Sub
    Sub AddBattery(battery As Item)
        If ItemType = ItemType.Torch AndAlso Not HasBattery AndAlso battery.ItemType = ItemType.Battery Then
            TorchBatteryData.Write(Id, battery.Id)
        End If
    End Sub
    ReadOnly Property Battery As Item
        Get
            Dim itemId = TorchBatteryData.Read(Id)
            If itemId.HasValue Then
                Return New Item(itemId.Value)
            End If
            Return Nothing
        End Get
    End Property
    ReadOnly Property Charge As Long?
        Get
            If ItemType = ItemType.Battery Then
                Return BatteryData.Read(Id)
            Else
                Return Nothing
            End If
        End Get
    End Property
    ReadOnly Property IsLit As Boolean
        Get
            Return ItemType = ItemType.Torch AndAlso
                Battery IsNot Nothing AndAlso
                Battery.Charge.HasValue AndAlso
                Battery.Charge.Value > 0
        End Get
    End Property
End Class
