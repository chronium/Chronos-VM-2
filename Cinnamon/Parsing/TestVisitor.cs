using Cinnamon.AST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinnamon.Parsing {
	public class TestVisitor : IAstVisitor {
		public void Visit(Test test) {
			Console.WriteLine("Hello!");
		}

		public void Visit(ScopeDeclr test) {
			Console.WriteLine("Useless!");
		}
	}
}
