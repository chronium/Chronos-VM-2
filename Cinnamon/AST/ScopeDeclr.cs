using Cinnamon.AST.Types;
using Cinnamon.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinnamon.AST {
	public class ScopeDeclr : Ast {
		public List<Ast> ScopedStatements { get; private set; }

		public ScopeDeclr(List<Ast> statements)
			: base(new ScopeStart()) {
				ScopedStatements = statements;
		}

		public override void Visit(IAstVisitor visitor) {
			visitor.Visit(this);
		}

		public override AstType AstType {
			get {
				return AstType.ScopeDeclr;
			}
		}
	}
}
