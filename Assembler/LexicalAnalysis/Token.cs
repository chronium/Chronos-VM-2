using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assembler.LxecialAnalysis {
	public class Token {
		public TokenType Type { get; set; }
		public string Value { get; set; }
		public Token(TokenType type, string value = "") {
			Type = type;
			Value = value;
		}
	}

	public enum TokenType {
		NOP,
		EOF,
		WhiteSpace,
		QuotedString,
		IntLiteral,
		WRITE,
		Comma,
		Char,
		Register,
		SET,
		Ref,
		INC,
		DEC,
		Label,
		ClosedCurly,
		OpenCurly,
		JMP,
		Word,
		CMP,
		JE,
		JG,
		HLT
	}
}
