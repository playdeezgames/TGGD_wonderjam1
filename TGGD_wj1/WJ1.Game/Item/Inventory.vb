Imports WJ1.Data

Public Class Inventory
    ReadOnly Property Id As Long
    Sub New(inventoryId As Long)
        Id = inventoryId
    End Sub
    ReadOnly Property Items As List(Of Item)
        Get
            Return InventoryItemData.ReadForInventoryId(Id).Select(Function(itemId) New Item(itemId)).ToList()
        End Get
    End Property
    ReadOnly Property IsEmpty As Boolean
        Get
            Return Not Items.Any()
        End Get
    End Property
    Sub Add(item As Item)
        InventoryItemData.Write(Id, item.Id)
    End Sub
End Class
