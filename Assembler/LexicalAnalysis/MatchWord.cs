using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.LxecialAnalysis {
	public class MatchLabel : MatcherBase {

		protected override Token IsMatchImpl(Tokenizer tokenizer) {
			StringBuilder accum = new StringBuilder();

			if (tokenizer.current == "'")
				return null;

			while (!tokenizer.End() && !String.IsNullOrWhiteSpace(tokenizer.current) && tokenizer.current != "{") {
				accum.Append(tokenizer.current);
				tokenizer.Consume();
			}

			if (accum.Length > 0)
				return new Token(TokenType.Label, accum.ToString());
			return null;
		}
	}
	public class MatchWord : MatcherBase {

		protected override Token IsMatchImpl(Tokenizer tokenizer) {
			StringBuilder accum = new StringBuilder();

			if (tokenizer.current == "'")
				return null;

			while (!tokenizer.End() && !String.IsNullOrWhiteSpace(tokenizer.current)) {
				accum.Append(tokenizer.current);
				tokenizer.Consume();
			}

			if (accum.Length > 0)
				return new Token(TokenType.Word, accum.ToString());
			return null;
		}
	}
}
