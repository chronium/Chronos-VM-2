using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.LxecialAnalysis {
	public interface IMatcher {
		Token IsMatch(Tokenizer tokenizer);
	}
}
