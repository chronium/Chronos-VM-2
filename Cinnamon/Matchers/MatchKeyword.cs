using Cinnamon.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizerLib;

namespace Cinnamon.Matchers {
	public class MatchKeyword : MatcherBase {
		public string Match { get; set; }
		private KeywordType TokenType { get; set; }

		public bool AllowAsSubstring { get; set; }
		public List<MatchKeyword> SpecialCharacters { get; set; }

		public MatchKeyword(KeywordType type, string match) {
			Match = match;
			this.TokenType = type;
			AllowAsSubstring = true;
		}

		protected override Token IsMatchImpl(Tokenizer tokenizer) {
			foreach (var character in Match)
				if (tokenizer.current == character.ToString())
					tokenizer.Consume();
				else
					return null;

			bool found;

			if (!AllowAsSubstring) {
				var next = tokenizer.current;
				found = String.IsNullOrWhiteSpace(next) || SpecialCharacters.Any(c => c.Match == next);
			} else
				found = true;

			if (found)
				return new Keyword(TokenType, Match);

			return null;
		}
	}
}
