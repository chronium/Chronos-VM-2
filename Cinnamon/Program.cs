using Cinnamon.Matchers;
using Cinnamon.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizerLib;

namespace Cinnamon {
	class Program {
		static void Main(string[] args) {
			Parser parser = new Parser(new Lexer("int test = 10; int anotherTest = 15;", getMatchers()));
			TestVisitor visitor = new TestVisitor();
			foreach (Ast ast in parser.Parse())
				ast.Visit(visitor);
			Console.Read();
		}

		public static List<IMatcher> getMatchers() {
			List<IMatcher> matchers = new List<IMatcher>();

			matchers.Add(new MatchKeyword(AST.KeywordType.Semicolon, ";"));
			matchers.Add(new MatchKeyword(AST.KeywordType.Assignment, "="));
			matchers.Add(new MatchKeyword(AST.KeywordType.Int, "int"));
			matchers.Add(new MatchIntLiteral());
			matchers.Add(new MatchWord());

			return matchers;
		}
	}
}
