using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizerLib;

namespace Cinnamon.Parsing {
	public class ParseableTokenStream : TokenizableStreamBase<Token> {
		public ParseableTokenStream(Lexer lexer)
			: base(() => lexer.Lex().ToList()) {
		}

		public bool IsMatch(Type type) {
			return current.GetType() == type;
		}

		public Token Take(Type type) {
			if (IsMatch(type)) {
				var curr = current;
				Consume();
				return curr;
			}

			throw new Exception();
		}

		public bool Alt(Func<Ast> action) {
			TakeSnapshot();
			bool found = false;

			try {
				var currentIndex = Index;
				var ast = action();

				if (ast != null) {
					found = true;
					CachedAst[currentIndex] = new Memo { Ast = ast, NextIndex = Index };
				}
			} catch { }

			RollbackSnapshot();

			return found;
		}

		private Dictionary<int, Memo> CachedAst = new Dictionary<int, Memo>();

		internal class Memo {
			public Ast Ast { get; set; }
			public int NextIndex { get; set; }
		}

		public Ast Capture(Func<Ast> ast) {
			if (Alt(ast))
				return Get(ast);

			return null;
		}

		public Ast Get(Func<Ast> getter) {
			Memo memo;
			if (!CachedAst.TryGetValue(Index, out memo))
				return getter();

			Index = memo.NextIndex;

			return memo.Ast;
		}
	}
}
