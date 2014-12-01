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

		public static short CL;
		public static short CH;

		public static uint C {
			get {
				return (uint)((CL << 16) | (CH & 0xFFFF));
			}
			set {
				CL = (short)((value & 0xFFFF0000) >> 16);
				CH = (short)((value & 0x0000FFFF));
			}
		}

		public static short DL;
		public static short DH;

		public static uint D {
			get {
				return (uint)((DL << 16) | (DH & 0xFFFF));
			}
			set {
				DL = (short)((value & 0xFFFF0000) >> 16);
				DH = (short)((value & 0x0000FFFF));
			}
		}

		public static short EL;
		public static short EH;

		public static uint E {
			get {
				return (uint)((EL << 16) | (EH & 0xFFFF));
			}
			set {
				EL = (short)((value & 0xFFFF0000) >> 16);
				EH = (short)((value & 0x0000FFFF));
			}
		}

		public static short FL;
		public static short FH;

		public static uint F {
			get {
				return (uint)((FL << 16) | (FH & 0xFFFF));
			}
			set {
				FL = (short)((value & 0xFFFF0000) >> 16);
				FH = (short)((value & 0x0000FFFF));
			}
		}

		public static short XL;
		public static short XH;

		public static uint X {
			get {
				return (uint)((XL << 16) | (XH & 0xFFFF));
			}
			set {
				XL = (short)((value & 0xFFFF0000) >> 16);
				XH = (short)((value & 0x0000FFFF));
			}
		}

		public static short YL;
		public static short YH;

		public static uint Y {
			get {
				return (uint)((YL << 16) | (YH & 0xFFFF));
			}
			set {
				YL = (short)((value & 0xFFFF0000) >> 16);
				YH = (short)((value & 0x0000FFFF));
			}
		}

		public static uint BP, SP, IP;

		public Registers() {
			A = B = C = D = E = F = X = Y = 0;
			BP = SP = IP;
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
				case 6:
					C = value;
					break;
				case 7:
					CL = (short)value;
					break;
				case 8:
					CH = (short)value;
					break;
				case 9:
					D = value;
					break;
				case 10:
					DL = (short)value;
					break;
				case 11:
					DH = (short)value;
					break;
				case 12:
					E = value;
					break;
				case 13:
					EL = (short)value;
					break;
				case 14:
					EH = (short)value;
					break;
				case 15:
					F = value;
					break;
				case 16:
					FL = (short)value;
					break;
				case 17:
					FH = (short)value;
					break;
				case 18:
					X = value;
					break;
				case 19:
					Y = value;
					break;
				case 20:
					BP = value;
					break;
				case 21:
					SP = value;
					break;
				case 22:
					IP = value;
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
		BH,
		C,
		CL,
		CH,
		D,
		DL,
		DH,
		E,
		EL,
		EH,
		F,
		FL,
		FH,
		X,
		Y,
		BP,
		SP,
		IP
	}
}
