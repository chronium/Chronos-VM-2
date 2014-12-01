using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinnamon.Parsing {
	public static class Maybe {
		public static TInput Or<TInput>(this TInput input, Func<TInput> eval) where TInput : class {
			if (input != null)
				return input;

			return eval();
		}
	}
}
