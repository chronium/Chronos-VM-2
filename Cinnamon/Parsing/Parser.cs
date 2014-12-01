using Cinnamon.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizerLib;

namespace Cinnamon.Parsing {
	public class Parser {
		private ParseableTokenStream TokenStream { get; set; }

		public Parser(Lexer lexer) {
			TokenStream = new ParseableTokenStream(lexer);
		}

		public Ast Parse() {
			var statement = new List<Ast>();

			while (TokenStream.current.GetType() != typeof(EOF))
				statement.Add(Test());
			return statement[0];
		}

		private Ast Test() {
			Func<Ast> op = () => {
				if (TokenStream.current is Keyword)
					if (((Keyword)TokenStream.current).KeywordType == KeywordType.Semicolon) {
						TokenStream.Consume();
						return new Test(TokenStream.current);
					}
				return null;
			};

			return TokenStream.Capture(op);
		}
	}
}
