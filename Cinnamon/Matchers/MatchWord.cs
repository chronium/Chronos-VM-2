using Cinnamon.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizerLib;

namespace Cinnamon.Matchers {
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
				return new Statement(accum.ToString());
			return null;
		}
	}
}
