Imports System.IO

Module Program
    Sub Main(args As String())
        Const target = 2020
        Const inputFilePath = "input.txt"

        Dim inputNumbers = GetInputNumbers(inputFilePath)

        Dim products = FindTargetProduct(inputNumbers, target)

        Dim result = products.Item1 * products.Item2

        Console.WriteLine($"Day 1 - Part 1: {result}")
    End Sub

    Function GetInputNumbers(inputFilePath As String) As Integer()
        Dim input As String() = File.ReadAllLines(inputFilePath)

        Dim parsedInput = Array.ConvertAll(input, Function(str) Integer.Parse(str))

        Return parsedInput
    End Function

    Function FindTargetProduct(inputNumbers As Integer(), target As Integer) As (Integer, Integer)
        For index As Integer = 0 To inputNumbers.Length - 1
            For innerIndex As Integer = 0 To inputNumbers.Length - 1
                Dim firstNumber = inputNumbers(index)
                Dim secondNumber = inputNumbers(innerIndex)

                If firstNumber + secondNumber = target Then
                    Return (firstNumber, secondNumber)
                End If
            Next
        Next

        Return (0, 0)
    End Function
End Module
