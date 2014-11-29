using Assembly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblerLibrary {
	public class Assembler {
		public static List<Instruction> instructions = new List<Instruction>();
		public int addr = 0;

		public void Emit(Instruction instruction, params object[] obj) {
			instruction.Emit(obj);
			instructions.Add(instruction);
			addr += instruction.bytes.Count;
		}

		public byte[] Release() {
			List<Byte> ramTemp = new List<byte>();

			foreach (Instruction i in instructions)
				foreach (byte b in i.bytes)
					ramTemp.Add(b);

			return ramTemp.ToArray();
		}

		public void WriteToFile(string path) {
			BinaryWriter bw = new BinaryWriter(new FileStream(path, FileMode.Create));
			bw.Write(Release());
		}

		public void FixLabels(SymbolHelper helper) {
			foreach (Instruction i in instructions)
				if (i is Jmp) {
					((Jmp)i).setAddress(helper.GettLabel(((Jmp)i).label).address - ((Jmp)i).addr);
					Console.WriteLine(helper.GettLabel(((Jmp)i).label).address - ((Jmp)i).addr);
				}
		}
	}

	public abstract class Instruction {
		public List<byte> bytes;
		public string name;

		public Instruction(string name) {
			bytes = new List<byte>();
			this.name = name;
		}

		public abstract List<byte> Emit(params object[] obj);

		public void setOpCode(byte b) {
			bytes.Add(b);
		}

		public void setByte(byte value) {
			bytes.Add(value);
		}

		public void setInt(int value) {
			byte[] i = BitConverter.GetBytes(value);
			foreach (byte b in i)
				bytes.Add(b);
		}

		public void setInt(int value, int offset) {
			byte[] i = BitConverter.GetBytes(value);
			int u = offset;
			foreach (byte b in i)
				bytes[u++] = b;
		}

		public void setInt(uint value) {
			byte[] i = BitConverter.GetBytes(value);
			foreach (byte b in i)
				bytes.Add(b);
		}

		public void reserveInt() {
			bytes.Add(0);
			bytes.Add(0);
			bytes.Add(0);
			bytes.Add(0);
		}

		public OperationType getType(object o1, object o2) {
			if (o1 is AsmRegisters) {
				if (o2 is AsmRegisters)
					return OperationType.RegisterRegister;
				else
					return OperationType.RegisterImmediate;
			} else
				if (o2 is AsmRegisters)
					return OperationType.ImmediateRegister;
				else
					return OperationType.ImmediateImmediate;
		}
	}

	public class Nop : Instruction {
		public Nop() : base("nop") {
		}

		public override List<byte> Emit(params object[] obj) {
			setOpCode(0);
			return bytes;
		}
	}

	public class Set : Instruction {
		public Set()
			: base("set") {
		}

		public override List<byte> Emit(params object[] obj) {
			setOpCode(1);
			setByte((byte)((AsmRegisters)obj[0]));
			setInt((uint)obj[1]);
			return bytes;
		}
	}

	public class Write : Instruction {
		public Write()
			: base("write") {
		}

		public override List<byte> Emit(params object[] obj) {
			setOpCode(2);
			setByte((byte)getType(obj[0], obj[1]));
			setByte(obj[1] is AsmRegisters ? (byte)ValueType.Register : (byte)ValueType.bit);
			if (obj[0] is AsmRegisters)
				setByte((byte)((AsmRegisters)obj[0]));
			else
				setInt((uint)obj[0]);
			if (obj[1] is AsmRegisters)
				setByte((byte)((AsmRegisters)obj[1]));
			else
				setByte((byte)obj[1]);
			return bytes;
		}
	}

	public class IncReg : Instruction {
		public IncReg()
			: base("inc") {
		}

		public override List<byte> Emit(params object[] obj) {
			setOpCode(3);
			setByte((byte)((AsmRegisters)obj[0]));
			return bytes;
		}
	}

	public class DecReg : Instruction {
		public DecReg()
			: base("dec") {
		}

		public override List<byte> Emit(params object[] obj) {
			setOpCode(4);
			setByte((byte)((AsmRegisters)obj[0]));
			return bytes;
		}
	}

	public class Jmp : Instruction {
		public string label;
		public int addr;
		public Jmp()
			: base("jmp") {
		}

		public override List<byte> Emit(params object[] obj) {
			setOpCode(5);
			reserveInt();
			this.label = (string)obj[0];
			this.addr = (int)obj[1];
			return bytes;
		}

		public void setAddress(int addr) {
			setInt(addr, 1);
		}
	}

	public class JE : Jmp {
		public JE()
			: base() {
		}

		public override List<byte> Emit(params object[] obj) {
			setOpCode(7);
			reserveInt();
			this.label = (string)obj[0];
			this.addr = (int)obj[1];
			return bytes;
		}
	}

	public class JG : Jmp {
		public JG()
			: base() {
		}

		public override List<byte> Emit(params object[] obj) {
			setOpCode(8);
			reserveInt();
			this.label = (string)obj[0];
			this.addr = (int)obj[1];
			return bytes;
		}
	}

	public class Cmp : Instruction {
		public Cmp()
			: base("cmp") {
		}

		public override List<byte> Emit(params object[] obj) {
			setOpCode(6);
			setByte((byte)getType(obj[0], obj[1]));
			setByte(obj[1] is AsmRegisters ? (byte)ValueType.Register : (byte)ValueType.bit);
			if (obj[0] is AsmRegisters)
				setByte((byte)((AsmRegisters)obj[0]));
			else
				setInt((uint)obj[0]);
			if (obj[1] is AsmRegisters)
				setByte((byte)((AsmRegisters)obj[1]));
			else
				setByte((byte)obj[1]);
			return bytes;
		}
	}

	public class Halt : Instruction {
		public Halt()
			: base("hlt") {
		}

		public override List<byte> Emit(params object[] obj) {
			setOpCode(9);
			return bytes;
		}
	}

	public enum AsmRegisters {
		A,
		AL,
		AH,
		B,
		BL,
		BH
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
