using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly {
	public class Scope {
		public string name = "";
		public List<Label> labels = new List<Label>();
	}

	public class Label {
		public string name;
		public string codeName;
		public int address;

		public Label(string name, string realName, int relativeAddress) {
			this.name = name;
			this.codeName = realName;
			this.address = relativeAddress;
		}
	}
}
