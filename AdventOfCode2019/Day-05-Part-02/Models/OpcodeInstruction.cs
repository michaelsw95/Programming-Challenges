using System.Collections.Generic;

namespace Day_05_Part_02.Models
{
    public class OpcodeInstruction
    {
        public Opcode Opcode { get; set; }
        public List<ParameterMode> ModesOfParameter { get; set; }
    }
}