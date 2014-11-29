using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.LxecialAnalysis {
	class MatchString : MatcherBase {
		public const string QUOTE = "\"";
		public const string TIC = "'";

		private string StringDelim { get; set; }

		public MatchString(string delim) {
			StringDelim = delim;
		}

		protected override Token IsMatchImpl(Tokenizer tokenizer) {
			var str = new StringBuilder();

			if (tokenizer.current == StringDelim) {
				tokenizer.Consume();

				while (!tokenizer.End() && tokenizer.current != StringDelim) {
					str.Append(tokenizer.current);
					tokenizer.Consume();
				}

				if (tokenizer.current == StringDelim)
					tokenizer.Consume();
			}

			if (str.Length > 0)
				return new Token(TokenType.QuotedString, str.ToString());

			return null;
		}
	}
}
