using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenizerLib {
	public class Lexer {
		public Tokenizer Tokenizer { get; private set; }
		public List<IMatcher> Matchers { get; private set; }

		public Lexer(string source, List<IMatcher> matchers) {
			Tokenizer = new Tokenizer(source);
			Matchers = new List<IMatcher>();
			Matchers.Add(new MatchWhiteSpace());
			Matchers.AddRange(matchers);
		}

		public IEnumerable<Token> Lex() {
			var current = Next();

			while (current != null && !(current is EOF)) {
				if (!(current is WhiteSpace))
					yield return current;
				current = Next();
			}
			yield return new EOF();
		}

		private Token Next() {
			if (Tokenizer.End()) {
				return new EOF();
			}

			return
					  (from match in Matchers
						let token = match.IsMatch(Tokenizer)
						where token != null
						select token).FirstOrDefault();
		}
	}
}
