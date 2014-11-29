using AssemblerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Parsing {
	public class Node {
	}

	public class Nop : Node {
	}

	public class Hlt : Node {
	}

	public class WriteB : Node {
		public uint address;
		public byte value;

		public WriteB(uint address, byte value) {
			this.address = address;
			this.value = value;
		}
	}

	public class WriteReg : Node {
		public uint address;
		public AsmRegisters reg;

		public WriteReg(uint address, AsmRegisters reg) {
			this.address = address;
			this.reg = reg;
		}
	}

	public class WriteR : Node {
		public AsmRegisters reg1;
		public AsmRegisters reg;

		public WriteR(AsmRegisters reg1, AsmRegisters reg) {
			this.reg1 = reg1;
			this.reg = reg;
		}
	}

	public class WriteVB : Node {
		public AsmRegisters reg;
		public byte value;

		public WriteVB(AsmRegisters reg, byte value) {
			this.reg = reg;
			this.value = value;
		}
	}

	public class CmpRI : Node {
		public AsmRegisters reg;
		public byte value;

		public CmpRI(AsmRegisters reg, byte value) {
			this.reg = reg;
			this.value = value;
		}
	}

	public class Set : Node {
		public AsmRegisters reg;
		public uint value;

		public Set(AsmRegisters reg, uint value) {
			this.reg = reg;
			this.value = value;
		}
	}

	public class IncReg : Node {
		public AsmRegisters reg;

		public IncReg(AsmRegisters reg) {
			this.reg = reg;
		}
	}

	public class DecReg : Node {
		public AsmRegisters reg;

		public DecReg(AsmRegisters reg) {
			this.reg = reg;
		}
	}

	public class Jmp : Node {
		public string label;

		public Jmp(string label) {
			this.label = label;
		}
	}

	public class JE : Node {
		public string label;

		public JE(string label) {
			this.label = label;
		}
	}

	public class JG : Node {
		public string label;

		public JG(string label) {
			this.label = label;
		}
	}

	public class Block : Node {
		public List<Node> nodes;

		public Block(List<Node> nodes) {
			this.nodes = nodes;
		}
	}

	public class LabeledBlock : Node {
		public string name;
		public Block block;

		public LabeledBlock(string name, Block block) {
			this.name = name;
			this.block = block;
		}
	}
}
