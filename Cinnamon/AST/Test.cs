using Cinnamon.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenizerLib;

namespace Cinnamon.AST {
	public class Test : Ast {

		public Test() : base() {
		}

		public override void Visit(IAstVisitor visitor) {
			visitor.Visit(this);
		}

		public override Types.AstType AstType {
			get { return Types.AstType.Test; }
		}
	}
}
