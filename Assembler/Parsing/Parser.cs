using Assembler.LxecialAnalysis;
using AssemblerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Parsing {
	public class Parser {
		public List<Token> Tokens { get; set; }
		public int index = 0;

		public List<Node> AST = new List<Node>();

		public Parser(List<Token> tokens) {
			Tokens = tokens;
		}

		public Token Peek(int offset = 0) {
			if (index + offset < Tokens.Count)
				return Tokens[index + offset];
			return null;
		}

		public Token Read() {
			if (index < Tokens.Count)
				return Tokens[index++];
			return null;
		}

		public void Parse() {
			while (Peek().Type != TokenType.EOF) {
				if (Peek().Type == TokenType.Label && Peek(1).Type == TokenType.OpenCurly) {
					string label = Read().Value;
					Read();
					Block block = parseBlock();
					Read();
					AST.Add(new LabeledBlock(label, block));
				}
			}
		}

		public Block parseBlock() {
			List<Node> nodes = new List<Node>();

			while (Peek().Type != TokenType.ClosedCurly)
				if (Peek().Type == TokenType.NOP) {
					Read();
					nodes.Add(new Nop());
				} else if (Peek().Type == TokenType.WRITE && Peek(1).Type == TokenType.IntLiteral && canBeNumber(Peek(3).Type)) {
					Read();
					uint address = getValue(Read());
					Read();
					byte value = (byte)getValue(Read());
					nodes.Add(new WriteB(address, value));
				} else if (Peek().Type == TokenType.SET && Peek(1).Type == TokenType.Register && canBeNumber(Peek(3).Type)) {
					Read();
					AsmRegisters reg = (AsmRegisters)Enum.Parse(typeof(AsmRegisters), Read().Value);
					Read();
					uint value = getValue(Read());
					nodes.Add(new Set(reg, value));
				} else if (Peek().Type == TokenType.WRITE && isRef(Peek(1)) && isRef(Peek(4))) {
					Read();
					Read();
					AsmRegisters reg = (AsmRegisters)Enum.Parse(typeof(AsmRegisters), Read().Value);
					Read();
					Read();
					AsmRegisters reg1 = (AsmRegisters)Enum.Parse(typeof(AsmRegisters), Read().Value);
					nodes.Add(new WriteR(reg, reg1));
				} else if (Peek().Type == TokenType.WRITE && isRef(Peek(1)) && canBeNumber(Peek(4).Type)) {
					Read();
					Read();
					AsmRegisters reg = (AsmRegisters)Enum.Parse(typeof(AsmRegisters), Read().Value);
					Read();
					byte value = (byte)getValue(Read());
					nodes.Add(new WriteVB(reg, value));
				} else if (Peek().Type == TokenType.WRITE && Peek(1).Type == TokenType.IntLiteral && isRef(Peek(3))) {
					Read();
					uint address = getValue(Read());
					Read();
					Read();
					AsmRegisters reg = (AsmRegisters)Enum.Parse(typeof(AsmRegisters), Read().Value);
					nodes.Add(new WriteReg(address, reg));
				} else if (Peek().Type == TokenType.INC && Peek(1).Type == TokenType.Register) {
					Read();
					AsmRegisters reg = (AsmRegisters)Enum.Parse(typeof(AsmRegisters), Read().Value);
					nodes.Add(new IncReg(reg));
				} else if (Peek().Type == TokenType.DEC && Peek(1).Type == TokenType.Register) {
					Read();
					AsmRegisters reg = (AsmRegisters)Enum.Parse(typeof(AsmRegisters), Read().Value);
					nodes.Add(new DecReg(reg));
				} else if (Peek().Type == TokenType.JMP && Peek(1).Type == TokenType.Label) {
					Read();
					string label = Read().Value;
					nodes.Add(new Jmp(label));
				} else if (Peek().Type == TokenType.CMP && isRef(Peek(1)) && canBeNumber(Peek(4).Type)) {
					Read();
					Read();
					AsmRegisters reg = (AsmRegisters)Enum.Parse(typeof(AsmRegisters), Read().Value);
					Read();
					byte value = (byte)getValue(Read());
					nodes.Add(new CmpRI(reg, value));
				} else if (Peek().Type == TokenType.JE && Peek(1).Type == TokenType.Label) {
					Read();
					string label = Read().Value;
					nodes.Add(new JE(label));
				} else if (Peek().Type == TokenType.JG && Peek(1).Type == TokenType.Label) {
					Read();
					string label = Read().Value;
					nodes.Add(new JG(label));
				} else if (Peek().Type == TokenType.HLT) {
					Read();
					nodes.Add(new Hlt());
				}

			return new Block(nodes);
		}

		public uint getValue(Token t) {
			string val = t.Value;
			uint value = (val.Contains('x') || val.Contains("X")) && val.Length > 1 ? Convert.ToUInt32(val, 16) : (t.Type == TokenType.Char ? (uint)val[0] : uint.Parse(val));
			return value;
		}

		public bool isRef(Token t) {
			return t.Type == TokenType.Ref;
		}

		public bool canBeNumber(TokenType type) {
			return type == TokenType.IntLiteral || type == TokenType.Char;
		}
	}
}
