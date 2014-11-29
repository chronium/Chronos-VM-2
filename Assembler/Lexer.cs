using Assembler.LxecialAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler {
	public class Lexer {
		public Tokenizer Tokenizer { get; set; }
		public List<IMatcher> Matchers { get; set; }

		public Lexer(string source) {
			Tokenizer = new Tokenizer(source);
		}

		public IEnumerable<Token> Lex() {
			Matchers = InitializeMatchList();

			var current = Next();

			while (current != null && current.Type != TokenType.EOF) {
				if (current.Type != TokenType.WhiteSpace)
					yield return current;
				current = Next();
			}
			yield return new Token(TokenType.EOF);
		}

		public List<IMatcher> InitializeMatchList() {
			var matchers = new List<IMatcher>();

			matchers.Add(new MatchWhiteSpace());
			matchers.Add(new MatchKeyword(TokenType.NOP, "nop"));
			matchers.Add(new MatchKeyword(TokenType.WRITE, "write"));
			matchers.Add(new MatchKeyword(TokenType.SET, "set"));
			matchers.Add(new MatchKeyword(TokenType.INC, "inc"));
			matchers.Add(new MatchKeyword(TokenType.DEC, "dec"));
			matchers.Add(new MatchKeyword(TokenType.JMP, "jmp"));
			matchers.Add(new MatchKeyword(TokenType.JE, "je"));
			matchers.Add(new MatchKeyword(TokenType.JG, "jg"));
			matchers.Add(new MatchKeyword(TokenType.CMP, "cmp"));
			matchers.Add(new MatchKeyword(TokenType.HLT, "hlt"));
			matchers.Add(new MatchKeyword(TokenType.Comma, ","));
			matchers.Add(new MatchKeyword(TokenType.Ref, "$"));
			matchers.Add(new MatchKeyword(TokenType.OpenCurly, "{"));
			matchers.Add(new MatchKeyword(TokenType.ClosedCurly, "}"));
			matchers.Add(new MatchRegister());
			matchers.Add(new MatchIntLiteral());
			matchers.Add(new MatchLabel());
			matchers.Add(new MatchWord());
			matchers.Add(new MatchChar());

			return matchers;
		}

		private Token Next() {
			if (Tokenizer.End()) {
				return new Token(TokenType.EOF);
			}

			return
					  (from match in Matchers
						let token = match.IsMatch(Tokenizer)
						where token != null
						select token).FirstOrDefault();
		}
	}
}
