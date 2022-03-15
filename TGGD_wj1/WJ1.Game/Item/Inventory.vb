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
    ReadOnly Property StackedItems As Dictionary(Of ItemType, List(Of Item))
        Get
            Dim groups = Items.GroupBy(Function(item) item.ItemType)
            Dim result As New Dictionary(Of ItemType, List(Of Item))
            For Each group In groups
                result.Add(group.Key, group.ToList())
            Next
            Return result
        End Get
    End Property
    Function HasItemType(itemType As ItemType) As Boolean
        Return Items.Any(Function(item) item.ItemType = itemType)
    End Function
End Class
