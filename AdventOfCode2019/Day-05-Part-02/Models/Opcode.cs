namespace Day_05_Part_02.Models
{
    public enum Opcode 
    {
        Addition = 1,
        Multiplication = 2,
        GetInput = 3,
        GetOutput = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        Exit = 99
    }
}