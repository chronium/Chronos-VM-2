using Cinnamon.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizerLib;

namespace Cinnamon.Matchers {
	public class MatchIntLiteral : MatcherBase {
		public MatchIntLiteral() {
		}

		public char[] hexChars = { 'A', 'B', 'C', 'D', 'E', 'F' };

		protected override Token IsMatchImpl(Tokenizer tokenizer) {
			StringBuilder accum = new StringBuilder();
			int index = 0;
			bool hex = false;
			while (!tokenizer.End()) {
				char current = tokenizer.current.ToCharArray()[0];
				if (char.IsDigit(current))
					accum.Append(current);
				else if (index == 1 && current == 'x' || current == 'X') {
					hex = true;
					accum.Append(current);
				} else if (hexChars.Any(c => c == char.ToUpper(current)) && hex)
					accum.Append(char.ToUpper(current));
				else
					break;
				tokenizer.Consume();
				index++;
			}
			if (accum.Length > 0)
				return new TokenIntLiteral(Int32.Parse(accum.ToString(), hex ? System.Globalization.NumberStyles.HexNumber : System.Globalization.NumberStyles.Integer));
			return null;
		}
	}
}
