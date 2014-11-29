using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly {
	public class SymbolHelper {
		public Stack<Scope> scopes = new Stack<Scope>();
		public int ScopeIndex { get; set; }

		public SymbolHelper() {
			ScopeIndex = 0;
		}

		public void BeginScope() {
			BeginScope(ScopeIndex.ToString());
			ScopeIndex++;
		}

		public void BeginScope(string name) {
			scopes.Push(new Scope() { name = GetScopePrefix() + name });
		}

		public void EndScope(AssemblerLibrary.Assembler assembler) {
			tempScopes.Add(PeekScope());
			scopes.Pop();
		}

		public Scope PeekScope() {
			return scopes.Peek();
		}

		public void addLabel(string label, int address) {
			Scope scope = scopes.Peek();
			scope.labels.Add(new Label(label, GetScopePrefix() + label, address));
		}

		public List<Scope> tempScopes = new List<Scope>();

		public Label GetLabel(string label) {
			foreach (Scope scope in scopes)
				foreach (Label l in scope.labels)
					if (l.codeName == label)
						return l;
			return null;
		}
		public Label GettLabel(string label) {
			foreach (Scope s in tempScopes)
				foreach (Label l in s.labels)
					if (l.name == label)
						return l;
			return null;
		}

		public string GetScopePrefix() {
			StringBuilder res = new StringBuilder();

			foreach (Scope scope in scopes)
				res.Append(scope.name + "_");

			return res.ToString();
		}
	}
}
