using Cinnamon.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinnamon.Parsing {
	public interface IAstVisitor {
		void Visit(Test ast);
		void Visit(ScopeDeclr ast);
	}
}
