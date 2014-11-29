using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.LxecialAnalysis {
	class MatchChar : MatcherBase {

		public MatchChar() {
		}

		protected override Token IsMatchImpl(Tokenizer tokenizer) {
			var str = new StringBuilder();

			if (tokenizer.current == "'") {
				tokenizer.Consume();
				str.Append(tokenizer.current);
				tokenizer.Consume();
				tokenizer.Consume();
			}

			if (str.Length > 0)
				return new Token(TokenType.Char, str.ToString());

			return null;
		}
	}
}
