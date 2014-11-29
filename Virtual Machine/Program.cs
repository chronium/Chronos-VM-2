using AssemblerLibrary;
using Chronos_VM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Virtual_Machine {
	class Program {
		[DllImport("kernel32.dll")]
		static extern bool AttachConsole(int dwProcessId);
		private const int ATTACH_PARENT_PROCESS = -1;

		public static VirtualMachine vm;
		public static Assembler assembler;

		[STAThread]
		static void Main(string[] args) {

			BinaryReader font = new BinaryReader(File.Open("font.bin", FileMode.Open));

			vm = new VirtualMachine(0x20000000);

			vm.setProgram(File.ReadAllBytes("I:\\vm os\\os.bin"), 0);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// Attach to the parent process via AttachConsole SDK call
			AttachConsole(ATTACH_PARENT_PROCESS);
			Screen screen = new Screen();

			OutputDevice device = new GraphicalOutputDevice(OutputType.Graphical, screen, font);

			vm.mainOutputDevice = device;
			vm.mainOutputDevice.initialize();

			new Thread(x => vm.Start()).Start();

			Application.Run(screen);
		}
	}

	public class TextOutputDevice : OutputDevice {
		public TextOutputDevice(OutputType device)
			: base(device) {
		}

		public void writeChar(char c, byte attributes, int x, int y) {
			Console.SetCursorPosition(x, y);
			Console.Write(c);
		}

		protected override WriteCallbackChar GetInheritorDelegateChar() {
			return writeChar;
		}

		protected override WriteCallbackPixel GetInheritorDelegatePixel() {
			return new WriteCallbackPixel((color, x, y) => { });
		}
	}

	public class GraphicalOutputDevice : OutputDevice {
		Screen screen;
		BinaryReader font;
		private byte[] videoMemory = new byte[4000];

		public GraphicalOutputDevice(OutputType device, Screen screen, BinaryReader br)
			: base(device) {
				this.screen = screen;
				this.font = br;
				this.name = "Chrono Screen 1.0".ToCharArray();
				this.id = "chscr10".ToCharArray();
		}

		private uint[] colors = new uint[]{0xFF000000, 0xFF000080, 0xFF008000, 0xFF008080, 0xFF800000, 
			0xFF800080, 0xFF808000, 0xFFAAAAAA, 0xFF0000FF, 0xFF00FF00, 0xFF00FFFF, 0xFFFF0000, 0xFFFF00FF, 0xFFFFFF00, 0xFFFFFFFF, 0xFFFFFFFF};

		public void writeChar(char c, byte attributes, int cx, int cy) {
			font.BaseStream.Position = c * 16;
			byte[] bitmap = font.ReadBytes(16);
			int start = ((int)c * 16);
			byte fg = (byte)(attributes & 0x0F);
			byte bg = (byte)((attributes & 0xF0) >> 4);
			int xP = cx * 8;
			int yP = cy * 16;
			for (int y = 0; y < 16; y++) {
				int pixels = bitmap[y];
				for (int r = 0; r < 8; r++) {
					if (((1 << r) & pixels) != 0)
						screen.SetPixel(xP + r, yP + y, (int)colors[fg]);
					else
						screen.SetPixel(xP + r, yP + y, (int)colors[bg]);
				}
			}
		}

		public override void initialize() {
			Program.vm.memory.MapRegion(new DeviceMappedRegion(0xFFA00000, 0xFFD0FFFF, writeCallback, readCallback));
		}

		private void writeCallback(uint address, byte data) {
			int pos = (int)(address - 0xFFA00000);
			int pos2 = pos / 2;
			videoMemory[pos] = data;

			if (address % 2 == 0)
				writeChar((char)data, videoMemory[pos + 1], pos2 % 80, pos2 / 80);
			else
				writeChar((char)videoMemory[pos - 1], data, pos2 % 80, pos2 / 80);
		}
		private void readCallback(uint address, ref byte data) {
		}

		protected override WriteCallbackChar GetInheritorDelegateChar() {
			return writeChar;
		}

		protected override WriteCallbackPixel GetInheritorDelegatePixel() {
			return new WriteCallbackPixel((color, x, y) => { });
		}
	}
}
