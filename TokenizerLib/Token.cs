namespace TokenizerLib {
	public class Token {
		public string Value { get; set; }

		public Token(string val) {
			Value = val;
		}

		public override string ToString() {
			return Value;
		}

		public override bool Equals(object obj) {

			if (obj == null || GetType() != obj.GetType()) {
				return false;
			}

			if (((Token)obj).Value == this.Value)
				return true;

			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return Value != null ? Value.GetHashCode() : 0;
		}
	}

	public class EOF : Token {
		public EOF()
			: base("") {
		}
	}

	public class WhiteSpace : Token {
		public WhiteSpace()
			: base(" ") {
		}
	}
}
