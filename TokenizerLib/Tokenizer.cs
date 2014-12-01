using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenizerLib {
	public class Tokenizer : TokenizableStreamBase<string> {
		public Tokenizer(String source)
			: base(() => source.ToCharArray().Select(i => i.ToString()).ToList()) {
		}
	}
}
