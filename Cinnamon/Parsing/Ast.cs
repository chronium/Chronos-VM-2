using Cinnamon.AST;
using Cinnamon.AST.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizerLib;

namespace Cinnamon.Parsing {
	public abstract class Ast : IAcceptVisitor {
		public Ast() {
		}

		public abstract void Visit(IAstVisitor visitor);

		public abstract AstType AstType {
			get;
		}
	}
}
