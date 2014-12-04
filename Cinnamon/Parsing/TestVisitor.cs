using Cinnamon.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinnamon.Parsing {
	public class TestVisitor : IAstVisitor {
		public void Visit(Test test) {
			Console.WriteLine("Semicolon");
		}

		public void Visit(ScopeDeclr test) {
			Console.WriteLine("Useless!");
		}

		public void Visit(AssignmentNode node) {
			Console.WriteLine("Assignment: {0} {1} = {2}", node.Type.ToString(), node.Name.ToString(), node.Value.ToString());
		}
	}
}
