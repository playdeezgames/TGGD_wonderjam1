Imports System.Runtime.CompilerServices

Public Enum FeatureType
    StairsUp
    StairsDown
End Enum
Public Module FeatureTypeExtensions
    <Extension()>
    Function Name(featureType As FeatureType) As String
        Select Case featureType
            Case FeatureType.StairsDown
                Return "stairs going down"
            Case FeatureType.StairsUp
                Return "stairs going up"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module