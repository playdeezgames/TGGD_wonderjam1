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
End Class
