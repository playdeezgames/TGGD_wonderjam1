Imports System.Runtime.CompilerServices

Public Enum FeatureType As Long
    StairsUp
    StairsDown
    BuildingExit
    Rubble
End Enum
Public Module FeatureTypeExtensions
    <Extension()>
    Function Name(featureType As FeatureType) As String
        Select Case featureType
            Case FeatureType.StairsDown
                Return "stairs going down"
            Case FeatureType.StairsUp
                Return "stairs going up"
            Case FeatureType.BuildingExit
                Return "building exit"
            Case FeatureType.Rubble
                Return "pile of rubble"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Module