open System.IO;

let input = File.ReadAllLines("./input.txt")

let leftSide, rightSide =
    input
    |> Array.map (fun line ->
        let parts = line.Split("  ")
        int (parts[0]), int parts[1])
    |> Array.unzip
    
let leftSideSorted = leftSide |> Array.sort
let rightSideSorted = rightSide |> Array.sort

let difference =
    Array.zip leftSideSorted rightSideSorted
    |> Array.sumBy (fun (left, right) -> abs (left - right))
        
printfn $"Day 1 - Part 1: {difference}"
