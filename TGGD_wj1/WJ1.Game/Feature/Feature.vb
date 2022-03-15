Imports WJ1.Data

Public Class Feature
    ReadOnly Property Id As Long
    Sub New(featureId As Long)
        Id = featureId
    End Sub
    ReadOnly Property FeatureType As FeatureType
        Get
            Return CType(FeatureData.ReadFeatureType(Id).Value, FeatureType)
        End Get
    End Property
    ReadOnly Property IsLocked As Boolean
        Get
            Select Case FeatureType
                Case FeatureType.BuildingExit
                    Return BuildingExitData.Read(Id).Value
                Case Else
                    Return False
            End Select
        End Get
    End Property
    Sub Unlock()
        Select Case FeatureType
            Case FeatureType.BuildingExit
                BuildingExitData.Write(Id, False)
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub
End Class
