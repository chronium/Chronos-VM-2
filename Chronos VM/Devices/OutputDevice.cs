using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos_VM {
	public delegate void WriteCallbackChar(char c, byte attributes, int x, int y);
	public delegate void WriteCallbackPixel(uint color, int x, int y);
	public class OutputDevice : Device {
		public WriteCallbackChar writeChar;
		public WriteCallbackPixel writePixel;
		public OutputType type;

		public OutputDevice(OutputType type)
			: base() {
			this.writeChar = GetInheritorDelegateChar();
			this.writePixel = GetInheritorDelegatePixel();
			this.type = type;
		}

		public void WriteChar(char c, byte attributes, int x, int y) {
			writeChar(c, attributes, x, y);
		}

		public void WritePixel(uint color, int x, int y) {
			if (this.type != OutputType.Graphical)
				throw new Exception("Output type not compatible");

			writePixel(color, x, y);
		}

		protected virtual WriteCallbackChar GetInheritorDelegateChar() {
			throw new NotImplementedException("Inheritors that uses the default constructor must implement the GetInheritorDelegate() method.");
		}

		protected virtual WriteCallbackPixel GetInheritorDelegatePixel() {
			throw new NotImplementedException("Inheritors that uses the default constructor must implement the GetInheritorDelegate() method.");
		}
		public override void Init() { }

		public virtual void initialize() {
		}
	}

	public enum OutputType {
		Text = 0,
		Graphical = 1
	}
}
