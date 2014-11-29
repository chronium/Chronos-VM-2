using Assembler.Assembly;
using Assembler.LxecialAnalysis;
using Assembler.Parsing;
using Assembly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler {
	class Program {
		static void Main(string[] args) {
			var test = File.ReadAllText("I:\\vm os\\os.txt");

			var tokens = new Lexer(test).Lex().ToList();
			Console.WriteLine("Tokens done!");
			var parser = new Parser(tokens);
			parser.Parse();
			var nodes = parser.AST;
			Console.WriteLine("Done Parsing!");

			AssemblerLibrary.Assembler assembler = new AssemblerLibrary.Assembler();
			MainAssembler asm = new MainAssembler(assembler, new SymbolHelper());

			asm.StartAssembly(nodes);

			assembler.WriteToFile("I:\\vm os\\os.bin");

			Console.WriteLine("Done...");
			Console.Read();
		}
	}
}
