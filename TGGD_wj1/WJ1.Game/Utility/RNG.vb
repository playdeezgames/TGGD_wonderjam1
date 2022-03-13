Imports System.Runtime.CompilerServices

Module RNG
    Private ReadOnly random As New Random
    Function FromGenerator(Of TGenerated)(table As Dictionary(Of TGenerated, Integer)) As TGenerated
        Dim generated = random.Next(table.Values.Sum)
        For Each entry In table
            generated -= entry.Value
            If generated < 0 Then
                Return entry.Key
            End If
        Next
        Throw New NotImplementedException()
    End Function
    Function FromGenerator(Of TGenerated)(hashSet As HashSet(Of TGenerated)) As TGenerated
        Dim table As New Dictionary(Of TGenerated, Integer)
        For Each item In hashSet
            table.Add(item, 1)
        Next
        Return FromGenerator(table)
    End Function
    Function FromRange(minimum As Integer, maximum As Integer) As Integer
        Return random.Next(maximum - minimum + 1) + minimum
    End Function
    Function FromDice(dieCount As Integer, dieSize As Integer) As Integer
        Dim total = 0
        While dieCount > 0
            dieCount -= 1
            total += FromRange(1, dieSize)
        End While
        Return total
    End Function
    Function FromList(Of TItem)(items As List(Of TItem)) As TItem
        Return items(FromRange(0, items.Count - 1))
    End Function
End Module
Module DictionaryExtensions
    <Extension()>
    Function CombineGenerator(first As Dictionary(Of Integer, Integer), second As Dictionary(Of Integer, Integer)) As Dictionary(Of Integer, Integer)
        Dim result As New Dictionary(Of Integer, Integer)
        For Each firstItem In first
            For Each secondItem In second
                Dim combinedKey = firstItem.Key + secondItem.Key
                Dim combinedValue = firstItem.Value * secondItem.Value
                If result.ContainsKey(combinedKey) Then
                    result(combinedKey) += combinedValue
                Else
                    result.Add(combinedKey, combinedValue)
                End If
            Next
        Next
        Return result
    End Function
End Module