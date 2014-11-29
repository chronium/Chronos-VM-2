using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chronos_VM.Instructions {
	public abstract class Instruction {

		public Instruction() {
		}

		public abstract void Execute(ref int IP, VirtualMachine vm, Registers registers);
	}

	public class Nop : Instruction {
		public Nop()
			: base() {
		}

		public override void Execute(ref int IP, VirtualMachine vm, Registers registers) {
			IP += 1;
		}
	}

	public class Halt : Instruction {
		public Halt()
			: base() {
		}

		public override void Execute(ref int IP, VirtualMachine vm, Registers registers) {
			while (true) Thread.Sleep(1000);
		}
	}

	public class Set : Instruction {
		public Set()
			: base() {
		}

		public override void Execute(ref int IP, VirtualMachine vm, Registers registers) {
			registers.setReg((Register)vm.ReadInt8(IP + 1), (uint)vm.ReadInt32(IP + 2));
			IP += 6;
		}
	}

	public class Write : Instruction {
		public Write()
			: base() {
		}

		public override void Execute(ref int IP, VirtualMachine vm, Registers registers) {
			OperationType type = (OperationType)vm.ReadInt8(IP + 1);
			ValueType val = (ValueType)vm.ReadInt8(IP + 2);
			if ((type & OperationType.Register) == OperationType.Register && (type & OperationType.Inverted) == 0) {
				uint addr = registers.getReg((Register)(vm.ReadInt8(IP + 3)));
				if (val == ValueType.bit)
					vm.memory[addr] = (byte)vm.ReadInt8(IP + 4);
				else if (val == ValueType.Register)
					vm.memory[addr] = (byte)registers.getReg((Register)vm.ReadInt8(IP + 4));
				IP += 5;
			} else if ((type & OperationType.Immediate) == OperationType.Immediate && (type & OperationType.Inverted) == OperationType.Inverted) {
				uint addr = vm.ReadInt32(IP + 3);

				if (val == ValueType.bit)
					vm.memory[addr] = (byte)vm.ReadInt8(IP + 7);
				else if (val == ValueType.Register)
					vm.memory[addr] = (byte)registers.getReg((Register)vm.ReadInt8(IP + 7));

				IP += 8;
			} else if (type == OperationType.RegisterRegister) {
				uint addr = registers.getReg((Register)(vm.ReadInt8(IP + 3)));
				vm.memory[addr] = (byte)registers.getReg((Register)vm.ReadInt8(IP + 4));

				IP += 5;
			}

			/*
				uint addr = registers.getReg((Register)(vm.ReadInt32(IP + 3)));

				if (val == ValueType.bit)
					vm.memory[addr] = (byte)vm.ReadInt8(IP + 7);
				else if (val == ValueType.Register)
					vm.memory[addr] = (byte)registers.getReg((Register)vm.ReadInt8(IP + 7));

				IP += 8;*/
		}
	}
	public class Cmp : Instruction {
		public Cmp()
			: base() {
		}

		public override void Execute(ref int IP, VirtualMachine vm, Registers registers) {
			OperationType type = (OperationType)vm.ReadInt8(IP + 1);
			ValueType val = (ValueType)vm.ReadInt8(IP + 2);

			if (type == OperationType.RegisterImmediate) {
				uint value = registers.getReg((Register)(vm.ReadInt8(IP + 3)));

				if (val == ValueType.bit) {
					vm.equal = (byte)vm.ReadInt8(IP + 4) == (byte)value;
					vm.greater = (byte)vm.ReadInt8(IP + 4) < (byte)value;
				}
				IP += 5;
			}
		}
	}
	public class Jump : Instruction {
		public Jump()
			: base() {
		}

		public override void Execute(ref int IP, VirtualMachine vm, Registers registers) {
			IP += (int)vm.ReadInt32(IP + 1);
		}
	}
	public class JE : Instruction {
		public JE()
			: base() {
		}

		public override void Execute(ref int IP, VirtualMachine vm, Registers registers) {
			if (vm.equal) IP += (int)vm.ReadInt32(IP + 1);
			else IP += 5;
		}
	}
	public class JG : Instruction {
		public JG()
			: base() {
		}

		public override void Execute(ref int IP, VirtualMachine vm, Registers registers) {
			if (vm.greater) IP += (int)vm.ReadInt32(IP + 1);
			else IP += 5;
		}
	}

	public class IncReg : Instruction {
		public IncReg()
			: base() {
		}

		public override void Execute(ref int IP, VirtualMachine vm, Registers registers) {
			registers.incReg((Register)vm.ReadInt8(IP + 1));

			IP += 2;
		}
	}

	public class DecReg : Instruction {
		public DecReg()
			: base() {
		}

		public override void Execute(ref int IP, VirtualMachine vm, Registers registers) {
			registers.decReg((Register)vm.ReadInt8(IP + 1));

			IP += 2;
		}
	}

	[Flags]
	public enum OperationType {
		Register = 1 << 0,
		Immediate = 1 << 1,
		Inverted = 1 << 3,
		RegisterImmediate = Register | Immediate,
		ImmediateRegister = RegisterImmediate | Inverted,
		RegisterRegister = Register | Inverted,
		ImmediateImmediate = Immediate | Inverted,
	}

	public enum ValueType {
		Register,
		bit,
		shrt,
		ushrt,
		integer,
		uinteger,
		lng,
		ulng
	}
}
