Public Class Item
    ReadOnly Property Id As Long
    Sub New(itemId As Long)
        Id = itemId
    End Sub
    ReadOnly Property ItemType As ItemType
        Get
            Throw New NotImplementedException
        End Get
    End Property
End Class
