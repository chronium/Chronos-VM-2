using Cinnamon.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinnamon.AST {
	public class AssignmentNode : Ast {
		public TypeValue Type { get; private set; }
		public string Name { get; private set; }
		public int Value { get; private set; }

		public AssignmentNode(TypeValue type, string name, int value)
			: base() {
			this.Type = type;
			this.Name = name;
			this.Value = value;
		}

		public override void Visit(IAstVisitor visitor) {
			visitor.Visit(this);
		}

		public override Types.AstType AstType {
			get { return Types.AstType.Assignment; }
		}
	}

	public enum TypeValue {
		Integer
	}
}
