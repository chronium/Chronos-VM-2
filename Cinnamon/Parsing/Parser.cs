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

		public List<Ast> Parse() {
			var statement = new List<Ast>();

			while (TokenStream.current.GetType() != typeof(EOF))
				statement.Add(Assignment());
			return statement;
		}

		private Ast Test() {
			Func<Ast> op = () => {
				if (TokenStream.current is Keyword)
					if (((Keyword)TokenStream.current).KeywordType == KeywordType.Semicolon) {
						TokenStream.Consume();
						return new Test();
					}
				return null;
			};

			return TokenStream.Capture(op);
		}

		private Ast IntLiteral() {
			Func<Ast> op = () => {
				if (TokenStream.current is TokenIntLiteral) {
					int value = ((TokenIntLiteral)TokenStream.current).Value;
					TokenStream.Consume();
					return new IntLiteralNode(value);
				}

				return null;
			};

			return TokenStream.Capture(op);
		}

		private Ast Assignment() {
			Func<Ast> op = () => {
				if (TokenStream.current is Keyword)
					if (((Keyword)TokenStream.current).KeywordType == KeywordType.Int) {
						TokenStream.Consume();
						if (TokenStream.current is Statement) {
							string name = TokenStream.current.Value;
							TokenStream.Consume();
							if (((Keyword)TokenStream.current).KeywordType == KeywordType.Assignment) {
								TokenStream.Consume();
								IntLiteralNode val = (IntLiteralNode)TokenStream.Capture(IntLiteral);
								TokenStream.Consume();
								return new AssignmentNode(TypeValue.Integer, name, val.Value);
							}
						}
					}
				return null;
			};

			return TokenStream.Capture(op);
		}
	}
}
