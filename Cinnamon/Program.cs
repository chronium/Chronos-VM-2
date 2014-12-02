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
			Parser parser = new Parser(new Lexer(";", getMatchers()));
			TestVisitor visitor = new TestVisitor();
			parser.Parse().Visit(visitor);
			Console.Read();
		}

		public static List<IMatcher> getMatchers() {
			List<IMatcher> matchers = new List<IMatcher>();

			matchers.Add(new MatchKeyword(AST.KeywordType.Semicolon, ";"));

			return matchers;
		}
	}
}
