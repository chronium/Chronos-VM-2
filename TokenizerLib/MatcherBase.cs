﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenizerLib {
	public abstract class MatcherBase : IMatcher {
		public Token IsMatch(Tokenizer tokenizer) {
			if (tokenizer.End())
				return new EOF();

			tokenizer.TakeSnapshot();
			var match = IsMatchImpl(tokenizer);

			if (match == null)
				tokenizer.RollbackSnapshot();
			else
				tokenizer.CommitSnapshot();

			return match;
		}

		protected abstract Token IsMatchImpl(Tokenizer tokenizer);
	}
}
