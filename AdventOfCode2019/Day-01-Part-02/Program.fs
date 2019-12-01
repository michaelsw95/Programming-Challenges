open System
open System.IO

let getInput (filePath: string) =
    File.ReadAllLines(filePath) |> Seq.cast<string>

let calculateFuelForMass moduleMass =
    let moduleMassAsFloat = float moduleMass
    let mass = moduleMassAsFloat / float 3
    let roundedMass = int (Math.Floor mass)
    roundedMass - 2

let rec calculateFuelForFuelMass fuleMass currentTotal =
    if fuleMass <= 0 then
        currentTotal
    else
        let fuelRequired = calculateFuelForMass fuleMass
        let fuelToInclude = if fuelRequired > 0 then fuelRequired else 0

        calculateFuelForFuelMass fuelRequired (currentTotal + fuelToInclude)

let rec calculateFuelForAllModules (moduleMasses: seq<string>) currentTotal =
    if Seq.isEmpty(moduleMasses) then
        currentTotal
    else
        let current = moduleMasses |> Seq.head |> int
        
        let currentFuelRequiredForModule = calculateFuelForMass current

        let fuelRequireForFuelMass = calculateFuelForFuelMass currentFuelRequiredForModule 0
        let fuelMassToInclude = if fuelRequireForFuelMass > 0 then fuelRequireForFuelMass else 0

        calculateFuelForAllModules
            (Seq.skip 1 moduleMasses)
            (currentFuelRequiredForModule + currentTotal + fuelMassToInclude)

[<EntryPoint>]
let main argv =
    if argv.Length <> 1 then
        raise (ArgumentException "Input path was not provider")

    let inputPath = argv |> Seq.head
    let input = getInput inputPath
    let result = calculateFuelForAllModules input 0

    Console.WriteLine ("Day 1 - Part 2: " + string result)

    0