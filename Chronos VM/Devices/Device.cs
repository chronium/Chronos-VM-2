using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronos_VM {
	public abstract class Device {
		public char[] name = new char[256];
		public char[] id = new char[32];

		public abstract void Init();
		public virtual void ReceiveData(int port, uint data) { }
		public virtual uint RequestData(int port) { return 0; }
		public virtual void Update() { }
	}
}
