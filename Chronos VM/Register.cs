using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos_VM {
	public class Registers {
		public static short AL;
		public static short AH;

		public static uint A {
			get{
				return (uint)((AL << 16) | (AH & 0xFFFF));
			}
			set {
				AL = (short)((value & 0xFFFF0000) >> 16);
				AH = (short)((value & 0x0000FFFF));
			}
		}

		public static short BL;
		public static short BH;

		public static uint B {
			get {
				return (uint)((BL << 16) | (BH & 0xFFFF));
			}
			set {
				BL = (short)((value & 0xFFFF0000) >> 16);
				BH = (short)((value & 0x0000FFFF));
			}
		}

		public Registers() {
			A = B = 0;
		}

		public void setReg(Register reg, uint value) {
			switch ((int)reg) {
				case 0:
					A = value;
					break;
				case 1:
					AL = (short)value;
					break;
				case 2:
					AH = (short)value;
					break;
				case 3:
					B = value;
					break;
				case 4:
					BL = (short)value;
					break;
				case 5:
					BH = (short)value;
					break;
			}
		}

		public void incReg(Register reg) {
			switch ((int)reg) {
				case 0:
					A++;
					break;
				case 1:
					AL++;
					break;
				case 2:
					AH++;
					break;
				case 3:
					B++;
					break;
				case 4:
					BL++;
					break;
				case 5:
					BH++;
					break;
			}
		}

		public void decReg(Register reg) {
			switch ((int)reg) {
				case 0:
					A--;
					break;
				case 1:
					AL--;
					break;
				case 2:
					AH--;
					break;
				case 3:
					B--;
					break;
				case 4:
					BL--;
					break;
				case 5:
					BH--;
					break;
			}
		}

		public uint getReg(Register reg) {
			switch ((int)reg) {
				case 0:
					return A;
				case 1:
					return (uint)AL;
				case 2:
					return (uint)AH;
				case 3:
					return B;
				case 4:
					return (uint)BL;
				case 5:
					return (uint)BH;
				default:
					return 0;
			}
		}
	}

	public enum Register {
		A,
		AL,
		AH,
		B,
		BL,
		BH
	}
}
