open System
open System.IO

let getInput (filePath: string) =
    File.ReadAllLines(filePath) |> Seq.cast<string>

let calculateFuelForModule moduleMass =
    let moduleMassAsFloat = float moduleMass
    let mass = moduleMassAsFloat / float 3
    let roundedMass = int (Math.Floor mass)
    roundedMass - 2

let rec calculateFuelForAllModules (moduleMasses: seq<string>) currentTotal =
    if Seq.isEmpty(moduleMasses) then
        currentTotal
    else
        let current = moduleMasses |> Seq.head |> int
        let currentMass = calculateFuelForModule current

        calculateFuelForAllModules
            (Seq.skip 1 moduleMasses)
            (currentMass + currentTotal)

[<EntryPoint>]
let main argv =
    if argv.Length <> 1 then
        raise (ArgumentException "Input path was not provider")

    let inputPath = argv |> Seq.head
    let input = getInput inputPath
    let result = calculateFuelForAllModules input 0

    Console.WriteLine ("Day 1 - Part 1: " + string result)

    0