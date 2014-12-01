using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenizerLib {
	class MatchWhiteSpace : MatcherBase {
		protected override Token IsMatchImpl(Tokenizer tokenizer) {
			bool foundWhiteSpace = false;

			while (!tokenizer.End() && (String.IsNullOrWhiteSpace(tokenizer.current) || tokenizer.current == "\r")) {
				foundWhiteSpace = true;
				tokenizer.Consume();
			}

			if (foundWhiteSpace)
				return new WhiteSpace();

			return null;
		}
	}
}
