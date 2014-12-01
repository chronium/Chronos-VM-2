using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenizerLib {
	public interface IMatcher {
		Token IsMatch(Tokenizer tokenizer);
	}
}
