using Chronos_VM.Instructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos_VM {
	public class VirtualMachine {
		public OutputDevice mainOutputDevice;
		public Registers registers = new Registers();
		public Memory memory;

		public bool equal = false;
		public bool greater = false;

		public int IP = 0;
		private long instructionsPerSecond = 1200000;

		public VirtualMachine(int size) {
			memory = new Memory(size);
		}

		public void setProgram(byte[] program, int entryPoint) {
			for (uint i = 0; i < program.Length; i++)
				memory[i] = program[i];
			IP = entryPoint;
		}

		private System.Timers.Timer clock;
		public void Start() {
			int msecond = System.DateTime.Now.Millisecond;
			clock = new System.Timers.Timer(100);
			clock.Elapsed += new System.Timers.ElapsedEventHandler(run);

			this.clock.AutoReset = true;
			clock.Start();
			while (true)
				System.Threading.Thread.Sleep(0x1000);
		}

		public void run(object o, System.Timers.ElapsedEventArgs args) {
			this.clock.Enabled = false;
			for (int i = 0; i < this.instructionsPerSecond / 100; i++) {
				int oldIP = IP;
				byte opcode = memory[(uint)IP];
				if (opcode <= instructions.Length)
					instructions[memory[(uint)IP]].Execute(ref IP, this, registers);
				i += oldIP - IP;
			}
			this.clock.Enabled = true;
		}

		public int Read(int address, int size, int offset, byte[] data) {
			int read;
			for (read = 0; read < size; read++) {
				data[offset + read] = (byte)ReadInt8(address++);
			}
			return read;
		}
		public uint ReadInt32(int address) {
			byte[] data = new byte[4];
			Read(address, 4, 0, data);
			return BitConverter.ToUInt32(data, 0);
		}

		public int ReadInt8(int address) {
			return memory[(uint)address];
		}

		public void WriteInt32(uint address, uint value) {
			byte[] i = BitConverter.GetBytes(value);
			memory[address] = i[0];
			memory[address + 1] = i[1];
			memory[address + 2] = i[2];
			memory[address + 3] = i[3];
		}

		public Instruction[] instructions = new Instruction[] {
			new Nop(),
			new Set(),
			new Write(),
			new IncReg(),
			new DecReg(),
			new Jump(),
			new Cmp(),
			new JE(),
			new JG(),
			new Halt()
		};
	}
}
