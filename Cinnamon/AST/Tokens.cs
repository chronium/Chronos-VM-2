using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizerLib;

namespace Cinnamon.AST {
	public class ScopeStart : Token {
		public ScopeStart()
			: base("") {
		}
	}

	public class Keyword : Token {
		public KeywordType KeywordType { get; set; }
		public Keyword(KeywordType type, string val)
			: base(val) {
				KeywordType = type;
		}
	}

	public class TokenIntLiteral : Token {
		public int Value { get; private set; }

		public TokenIntLiteral(int value) : base(value.ToString()) {
			Value = value;
		}
	}

	public class Statement : Token {
		public Statement(string value)
			: base(value) {
		}
	}

	public enum KeywordType {
		Semicolon,
		Int,
		Assignment,
	}
}
