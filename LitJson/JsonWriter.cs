using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace LitJson
{
	// Token: 0x0200015A RID: 346
	public sealed class JsonWriter
	{
		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00090118 File Offset: 0x0008E318
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x00090120 File Offset: 0x0008E320
		public int IndentValue
		{
			get
			{
				return this.indent_value;
			}
			set
			{
				this.indentation = this.indentation / this.indent_value * value;
				this.indent_value = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0009013E File Offset: 0x0008E33E
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x00090146 File Offset: 0x0008E346
		public bool PrettyPrint
		{
			get
			{
				return this.pretty_print;
			}
			set
			{
				this.pretty_print = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0009014F File Offset: 0x0008E34F
		public TextWriter TextWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00090157 File Offset: 0x0008E357
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0009015F File Offset: 0x0008E35F
		public bool Validate
		{
			get
			{
				return this.validate;
			}
			set
			{
				this.validate = value;
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00090174 File Offset: 0x0008E374
		public JsonWriter()
		{
			this.inst_string_builder = new StringBuilder();
			this.writer = new StringWriter(this.inst_string_builder);
			this.Init();
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0009019E File Offset: 0x0008E39E
		public JsonWriter(StringBuilder sb) : this(new StringWriter(sb))
		{
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000901AC File Offset: 0x0008E3AC
		public JsonWriter(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.Init();
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000901D0 File Offset: 0x0008E3D0
		private void DoValidation(Condition cond)
		{
			if (!this.context.ExpectingValue)
			{
				this.context.Count++;
			}
			if (!this.validate)
			{
				return;
			}
			if (this.has_reached_end)
			{
				throw new JsonException("A complete JSON symbol has already been written");
			}
			switch (cond)
			{
			case Condition.InArray:
				if (!this.context.InArray)
				{
					throw new JsonException("Can't close an array here");
				}
				break;
			case Condition.InObject:
				if (!this.context.InObject || this.context.ExpectingValue)
				{
					throw new JsonException("Can't close an object here");
				}
				break;
			case Condition.NotAProperty:
				if (this.context.InObject && !this.context.ExpectingValue)
				{
					throw new JsonException("Expected a property");
				}
				break;
			case Condition.Property:
				if (!this.context.InObject || this.context.ExpectingValue)
				{
					throw new JsonException("Can't add a property here");
				}
				break;
			case Condition.Value:
				if (!this.context.InArray && (!this.context.InObject || !this.context.ExpectingValue))
				{
					throw new JsonException("Can't add a value here");
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000902F4 File Offset: 0x0008E4F4
		private void Init()
		{
			this.has_reached_end = false;
			this.hex_seq = new char[4];
			this.indentation = 0;
			this.indent_value = 4;
			this.pretty_print = false;
			this.validate = true;
			this.ctx_stack = new Stack<WriterContext>();
			this.context = new WriterContext();
			this.ctx_stack.Push(this.context);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00090358 File Offset: 0x0008E558
		private static void IntToHex(int n, char[] hex)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = n % 16;
				if (num < 10)
				{
					hex[3 - i] = (char)(48 + num);
				}
				else
				{
					hex[3 - i] = (char)(65 + (num - 10));
				}
				n >>= 4;
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00090399 File Offset: 0x0008E599
		private void Indent()
		{
			if (this.pretty_print)
			{
				this.indentation += this.indent_value;
			}
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x000903B8 File Offset: 0x0008E5B8
		private void Put(string str)
		{
			if (this.pretty_print && !this.context.ExpectingValue)
			{
				for (int i = 0; i < this.indentation; i++)
				{
					this.writer.Write(' ');
				}
			}
			this.writer.Write(str);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00090404 File Offset: 0x0008E604
		private void PutNewline()
		{
			this.PutNewline(true);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00090410 File Offset: 0x0008E610
		private void PutNewline(bool add_comma)
		{
			if (add_comma && !this.context.ExpectingValue && this.context.Count > 1)
			{
				this.writer.Write(',');
			}
			if (this.pretty_print && !this.context.ExpectingValue)
			{
				this.writer.Write('\n');
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0009046C File Offset: 0x0008E66C
		private void PutString(string str)
		{
			this.Put(string.Empty);
			this.writer.Write('"');
			int length = str.Length;
			int i = 0;
			while (i < length)
			{
				char c = str[i];
				switch (c)
				{
				case '\b':
					this.writer.Write("\\b");
					break;
				case '\t':
					this.writer.Write("\\t");
					break;
				case '\n':
					this.writer.Write("\\n");
					break;
				case '\v':
					goto IL_E4;
				case '\f':
					this.writer.Write("\\f");
					break;
				case '\r':
					this.writer.Write("\\r");
					break;
				default:
					if (c != '"' && c != '\\')
					{
						goto IL_E4;
					}
					this.writer.Write('\\');
					this.writer.Write(str[i]);
					break;
				}
				IL_141:
				i++;
				continue;
				IL_E4:
				if (str[i] >= ' ' && str[i] <= '~')
				{
					this.writer.Write(str[i]);
					goto IL_141;
				}
				JsonWriter.IntToHex((int)str[i], this.hex_seq);
				this.writer.Write("\\u");
				this.writer.Write(this.hex_seq);
				goto IL_141;
			}
			this.writer.Write('"');
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000905D2 File Offset: 0x0008E7D2
		private void Unindent()
		{
			if (this.pretty_print)
			{
				this.indentation -= this.indent_value;
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x000905EF File Offset: 0x0008E7EF
		public override string ToString()
		{
			if (this.inst_string_builder == null)
			{
				return string.Empty;
			}
			return this.inst_string_builder.ToString();
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0009060C File Offset: 0x0008E80C
		public void Reset()
		{
			this.has_reached_end = false;
			this.ctx_stack.Clear();
			this.context = new WriterContext();
			this.ctx_stack.Push(this.context);
			if (this.inst_string_builder != null)
			{
				this.inst_string_builder.Remove(0, this.inst_string_builder.Length);
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00090667 File Offset: 0x0008E867
		public void Write(bool boolean)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(boolean ? "true" : "false");
			this.context.ExpectingValue = false;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00090697 File Offset: 0x0008E897
		public void Write(decimal number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000906C4 File Offset: 0x0008E8C4
		public void Write(double number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			string text = Convert.ToString(number, JsonWriter.number_format);
			this.Put(text);
			if (text.IndexOf('.') == -1 && text.IndexOf('E') == -1)
			{
				this.writer.Write(".0");
			}
			this.context.ExpectingValue = false;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00090723 File Offset: 0x0008E923
		public void Write(int number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0009074F File Offset: 0x0008E94F
		public void Write(long number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0009077B File Offset: 0x0008E97B
		public void Write(string str)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			if (str == null)
			{
				this.Put("null");
			}
			else
			{
				this.PutString(str);
			}
			this.context.ExpectingValue = false;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000907AD File Offset: 0x0008E9AD
		public void Write(ulong number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000907DC File Offset: 0x0008E9DC
		public void WriteArrayEnd()
		{
			this.DoValidation(Condition.InArray);
			this.PutNewline(false);
			this.ctx_stack.Pop();
			if (this.ctx_stack.Count == 1)
			{
				this.has_reached_end = true;
			}
			else
			{
				this.context = this.ctx_stack.Peek();
				this.context.ExpectingValue = false;
			}
			this.Unindent();
			this.Put("]");
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00090848 File Offset: 0x0008EA48
		public void WriteArrayStart()
		{
			this.DoValidation(Condition.NotAProperty);
			this.PutNewline();
			this.Put("[");
			this.context = new WriterContext();
			this.context.InArray = true;
			this.ctx_stack.Push(this.context);
			this.Indent();
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0009089C File Offset: 0x0008EA9C
		public void WriteObjectEnd()
		{
			this.DoValidation(Condition.InObject);
			this.PutNewline(false);
			this.ctx_stack.Pop();
			if (this.ctx_stack.Count == 1)
			{
				this.has_reached_end = true;
			}
			else
			{
				this.context = this.ctx_stack.Peek();
				this.context.ExpectingValue = false;
			}
			this.Unindent();
			this.Put("}");
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00090908 File Offset: 0x0008EB08
		public void WriteObjectStart()
		{
			this.DoValidation(Condition.NotAProperty);
			this.PutNewline();
			this.Put("{");
			this.context = new WriterContext();
			this.context.InObject = true;
			this.ctx_stack.Push(this.context);
			this.Indent();
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0009095C File Offset: 0x0008EB5C
		public void WritePropertyName(string property_name)
		{
			this.DoValidation(Condition.Property);
			this.PutNewline();
			this.PutString(property_name);
			if (this.pretty_print)
			{
				if (property_name.Length > this.context.Padding)
				{
					this.context.Padding = property_name.Length;
				}
				for (int i = this.context.Padding - property_name.Length; i >= 0; i--)
				{
					this.writer.Write(' ');
				}
				this.writer.Write(": ");
			}
			else
			{
				this.writer.Write(':');
			}
			this.context.ExpectingValue = true;
		}

		// Token: 0x0400112E RID: 4398
		private static NumberFormatInfo number_format = NumberFormatInfo.InvariantInfo;

		// Token: 0x0400112F RID: 4399
		private WriterContext context;

		// Token: 0x04001130 RID: 4400
		private Stack<WriterContext> ctx_stack;

		// Token: 0x04001131 RID: 4401
		private bool has_reached_end;

		// Token: 0x04001132 RID: 4402
		private char[] hex_seq;

		// Token: 0x04001133 RID: 4403
		private int indentation;

		// Token: 0x04001134 RID: 4404
		private int indent_value;

		// Token: 0x04001135 RID: 4405
		private StringBuilder inst_string_builder;

		// Token: 0x04001136 RID: 4406
		private bool pretty_print;

		// Token: 0x04001137 RID: 4407
		private bool validate;

		// Token: 0x04001138 RID: 4408
		private TextWriter writer;
	}
}
