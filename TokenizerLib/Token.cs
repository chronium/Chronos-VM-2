using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TokenizerLib {
	public class Token {
		public string Value { get; set; }

		public Token(string val) {
			Value = val;
		}
	}

	public class EOF : Token {
		public EOF()
			: base("") {
		}
	}

	public class WhiteSpace : Token {
		public WhiteSpace()
			: base(" ") {
		}
	}
}
