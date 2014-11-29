using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.LxecialAnalysis {
	public class MatchRegister : MatcherBase {
		public string[] regs = { "A", "AL", "AH", "B", "BL", "BH" };

		protected override Token IsMatchImpl(Tokenizer tokenizer) {
			StringBuilder accum = new StringBuilder();
			bool b = false;
			foreach (string Match in regs) {
				foreach (var character in Match)
					if (tokenizer.current == character.ToString()) {
						accum.Append(tokenizer.current);
						tokenizer.Consume();
						b = true;
						break;
					}
				if (b) break;
			}

			if (accum.Length > 0)
				return new Token(TokenType.Register, accum.ToString());
			return null;
		}
	}
}
