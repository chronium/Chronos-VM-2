using Cinnamon.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinnamon.AST {
	public class IntLiteralNode : Ast {
		public int Value { get; set; }

		public IntLiteralNode(int value) {
			Value = value;
		}

		public override void Visit(IAstVisitor visitor) {
		}

		public override Types.AstType AstType {
			get { return Types.AstType.IntLiteral; }
		}
	}
}
