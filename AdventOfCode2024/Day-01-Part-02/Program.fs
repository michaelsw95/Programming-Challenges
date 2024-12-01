open System.IO

let input = File.ReadAllLines("./input.txt")

let leftSide, rightSide =
    input
    |> Array.map (fun line ->
        let parts = line.Split("  ")
        int (parts[0]), int (parts[1]))
    |> Array.unzip
    
let rightLookup =
    rightSide
    |> Array.countBy id
    |> dict
    
let difference =
    leftSide
    |> Array.sort
    |> Array.sumBy (fun number ->
                if rightLookup.ContainsKey(number) then
                    number * rightLookup[number]
                else
                    0)

printfn $"Day 1 - Part 2: {difference}"
