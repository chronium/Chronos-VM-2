using Assembler.Parsing;
using Assembly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Assembly {
	public class MainAssembler {
		public AssemblerLibrary.Assembler assembler;
		public SymbolHelper helper;

		public MainAssembler(AssemblerLibrary.Assembler assembler, SymbolHelper helper) {
			this.assembler = assembler;
			this.helper = helper;
		}

		public void StartAssembly(List<Node> nodes) {
			helper.BeginScope();
			LabeledBlock entryPoint = getEntryPoint(nodes);
			AssembleLabel(entryPoint);
			nodes.Remove(entryPoint);
			foreach (Node n in nodes)
				if (n is LabeledBlock)
					AssembleLabel((LabeledBlock)n);
			helper.EndScope(assembler);
			assembler.FixLabels(helper);
		}

		public LabeledBlock getEntryPoint(List<Node> nodes) {
			foreach (Node n in nodes)
				if (n is LabeledBlock)
					if (((LabeledBlock)n).name == "EntryPoint")
						return (LabeledBlock)n;

			return null;
		}

		public void AssembleLabel(LabeledBlock lab) {
			helper.BeginScope(lab.name);
			helper.addLabel(lab.name, assembler.addr);
			AssembleBlock(lab.block);
			helper.EndScope(assembler);
		}

		public void AssembleBlock(Block block) {
			foreach (Node node in block.nodes)
				if (node is Nop)
					assembler.Emit(new AssemblerLibrary.Nop());
				else if (node is WriteB)
					assembler.Emit(new AssemblerLibrary.Write(), ((WriteB)node).address, ((WriteB)node).value);
				else if (node is Set)
					assembler.Emit(new AssemblerLibrary.Set(), ((Set)node).reg, ((Set)node).value);
				else if (node is WriteReg)
					assembler.Emit(new AssemblerLibrary.Write(), ((WriteReg)node).address, ((WriteReg)node).reg);
				else if (node is WriteR)
					assembler.Emit(new AssemblerLibrary.Write(), ((WriteR)node).reg1, ((WriteR)node).reg);
				else if (node is WriteVB)
					assembler.Emit(new AssemblerLibrary.Write(), ((WriteVB)node).reg, ((WriteVB)node).value);
				else if (node is IncReg)
					assembler.Emit(new AssemblerLibrary.IncReg(), ((IncReg)node).reg);
				else if (node is DecReg)
					assembler.Emit(new AssemblerLibrary.DecReg(), ((DecReg)node).reg);
				else if (node is LabeledBlock)
					AssembleLabel((LabeledBlock)node);
				else if (node is Jmp)
					assembler.Emit(new AssemblerLibrary.Jmp(), ((Jmp)node).label, assembler.addr);
				else if (node is JE)
					assembler.Emit(new AssemblerLibrary.JE(), ((JE)node).label, assembler.addr);
				else if (node is JG)
					assembler.Emit(new AssemblerLibrary.JG(), ((JG)node).label, assembler.addr);
				else if (node is CmpRI)
					assembler.Emit(new AssemblerLibrary.Cmp(), ((CmpRI)node).reg, ((CmpRI)node).value);
				else if (node is Hlt)
					assembler.Emit(new AssemblerLibrary.Halt());
		}
	}
}
