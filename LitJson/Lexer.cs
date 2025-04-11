using System;
using System.IO;
using System.Text;

namespace LitJson
{
	// Token: 0x0200015C RID: 348
	internal sealed class Lexer
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x000909FE File Offset: 0x0008EBFE
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x00090A06 File Offset: 0x0008EC06
		public bool AllowComments
		{
			get
			{
				return this.allow_comments;
			}
			set
			{
				this.allow_comments = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00090A0F File Offset: 0x0008EC0F
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x00090A17 File Offset: 0x0008EC17
		public bool AllowSingleQuotedStrings
		{
			get
			{
				return this.allow_single_quoted_strings;
			}
			set
			{
				this.allow_single_quoted_strings = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00090A20 File Offset: 0x0008EC20
		public bool EndOfInput
		{
			get
			{
				return this.end_of_input;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00090A28 File Offset: 0x0008EC28
		public int Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00090A30 File Offset: 0x0008EC30
		public string StringValue
		{
			get
			{
				return this.string_value;
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00090A38 File Offset: 0x0008EC38
		static Lexer()
		{
			Lexer.PopulateFsmTables();
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00090A40 File Offset: 0x0008EC40
		public Lexer(TextReader reader)
		{
			this.allow_comments = true;
			this.allow_single_quoted_strings = true;
			this.input_buffer = 0;
			this.string_buffer = new StringBuilder(128);
			this.state = 1;
			this.end_of_input = false;
			this.reader = reader;
			this.fsm_context = new FsmContext();
			this.fsm_context.L = this;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00090AA4 File Offset: 0x0008ECA4
		private static int HexValue(int digit)
		{
			switch (digit)
			{
			case 65:
				break;
			case 66:
				return 11;
			case 67:
				return 12;
			case 68:
				return 13;
			case 69:
				return 14;
			case 70:
				return 15;
			default:
				switch (digit)
				{
				case 97:
					break;
				case 98:
					return 11;
				case 99:
					return 12;
				case 100:
					return 13;
				case 101:
					return 14;
				case 102:
					return 15;
				default:
					return digit - 48;
				}
				break;
			}
			return 10;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00090B0C File Offset: 0x0008ED0C
		private static void PopulateFsmTables()
		{
			Lexer.fsm_handler_table = new Lexer.StateHandler[]
			{
				new Lexer.StateHandler(Lexer.State1),
				new Lexer.StateHandler(Lexer.State2),
				new Lexer.StateHandler(Lexer.State3),
				new Lexer.StateHandler(Lexer.State4),
				new Lexer.StateHandler(Lexer.State5),
				new Lexer.StateHandler(Lexer.State6),
				new Lexer.StateHandler(Lexer.State7),
				new Lexer.StateHandler(Lexer.State8),
				new Lexer.StateHandler(Lexer.State9),
				new Lexer.StateHandler(Lexer.State10),
				new Lexer.StateHandler(Lexer.State11),
				new Lexer.StateHandler(Lexer.State12),
				new Lexer.StateHandler(Lexer.State13),
				new Lexer.StateHandler(Lexer.State14),
				new Lexer.StateHandler(Lexer.State15),
				new Lexer.StateHandler(Lexer.State16),
				new Lexer.StateHandler(Lexer.State17),
				new Lexer.StateHandler(Lexer.State18),
				new Lexer.StateHandler(Lexer.State19),
				new Lexer.StateHandler(Lexer.State20),
				new Lexer.StateHandler(Lexer.State21),
				new Lexer.StateHandler(Lexer.State22),
				new Lexer.StateHandler(Lexer.State23),
				new Lexer.StateHandler(Lexer.State24),
				new Lexer.StateHandler(Lexer.State25),
				new Lexer.StateHandler(Lexer.State26),
				new Lexer.StateHandler(Lexer.State27),
				new Lexer.StateHandler(Lexer.State28)
			};
			Lexer.fsm_return_table = new int[]
			{
				65542,
				0,
				65537,
				65537,
				0,
				65537,
				0,
				65537,
				0,
				0,
				65538,
				0,
				0,
				0,
				65539,
				0,
				0,
				65540,
				65541,
				65542,
				0,
				0,
				65541,
				65542,
				0,
				0,
				0,
				0
			};
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00090CF4 File Offset: 0x0008EEF4
		private static char ProcessEscChar(int esc_char)
		{
			if (esc_char <= 92)
			{
				if (esc_char <= 39)
				{
					if (esc_char != 34 && esc_char != 39)
					{
						return '?';
					}
				}
				else if (esc_char != 47 && esc_char != 92)
				{
					return '?';
				}
				return Convert.ToChar(esc_char);
			}
			if (esc_char <= 102)
			{
				if (esc_char == 98)
				{
					return '\b';
				}
				if (esc_char == 102)
				{
					return '\f';
				}
			}
			else
			{
				if (esc_char == 110)
				{
					return '\n';
				}
				if (esc_char == 114)
				{
					return '\r';
				}
				if (esc_char == 116)
				{
					return '\t';
				}
			}
			return '?';
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00090D5C File Offset: 0x0008EF5C
		private static bool State1(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				if (ctx.L.input_char != 32 && (ctx.L.input_char < 9 || ctx.L.input_char > 13))
				{
					if (ctx.L.input_char >= 49 && ctx.L.input_char <= 57)
					{
						ctx.L.string_buffer.Append((char)ctx.L.input_char);
						ctx.NextState = 3;
						return true;
					}
					int num = ctx.L.input_char;
					if (num <= 91)
					{
						if (num <= 39)
						{
							if (num == 34)
							{
								ctx.NextState = 19;
								ctx.Return = true;
								return true;
							}
							if (num != 39)
							{
								return false;
							}
							if (!ctx.L.allow_single_quoted_strings)
							{
								return false;
							}
							ctx.L.input_char = 34;
							ctx.NextState = 23;
							ctx.Return = true;
							return true;
						}
						else
						{
							switch (num)
							{
							case 44:
								break;
							case 45:
								ctx.L.string_buffer.Append((char)ctx.L.input_char);
								ctx.NextState = 2;
								return true;
							case 46:
								return false;
							case 47:
								if (!ctx.L.allow_comments)
								{
									return false;
								}
								ctx.NextState = 25;
								return true;
							case 48:
								ctx.L.string_buffer.Append((char)ctx.L.input_char);
								ctx.NextState = 4;
								return true;
							default:
								if (num != 58 && num != 91)
								{
									return false;
								}
								break;
							}
						}
					}
					else if (num <= 110)
					{
						if (num != 93)
						{
							if (num == 102)
							{
								ctx.NextState = 12;
								return true;
							}
							if (num != 110)
							{
								return false;
							}
							ctx.NextState = 16;
							return true;
						}
					}
					else
					{
						if (num == 116)
						{
							ctx.NextState = 9;
							return true;
						}
						if (num != 123 && num != 125)
						{
							return false;
						}
					}
					ctx.NextState = 1;
					ctx.Return = true;
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00090F54 File Offset: 0x0008F154
		private static bool State2(FsmContext ctx)
		{
			ctx.L.GetChar();
			if (ctx.L.input_char >= 49 && ctx.L.input_char <= 57)
			{
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
				ctx.NextState = 3;
				return true;
			}
			int num = ctx.L.input_char;
			if (num == 48)
			{
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
				ctx.NextState = 4;
				return true;
			}
			return false;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x00090FEC File Offset: 0x0008F1EC
		private static bool State3(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				if (ctx.L.input_char >= 48 && ctx.L.input_char <= 57)
				{
					ctx.L.string_buffer.Append((char)ctx.L.input_char);
				}
				else
				{
					if (ctx.L.input_char == 32 || (ctx.L.input_char >= 9 && ctx.L.input_char <= 13))
					{
						ctx.Return = true;
						ctx.NextState = 1;
						return true;
					}
					int num = ctx.L.input_char;
					if (num <= 69)
					{
						if (num != 44)
						{
							if (num == 46)
							{
								ctx.L.string_buffer.Append((char)ctx.L.input_char);
								ctx.NextState = 5;
								return true;
							}
							if (num != 69)
							{
								return false;
							}
							goto IL_F4;
						}
					}
					else if (num != 93)
					{
						if (num == 101)
						{
							goto IL_F4;
						}
						if (num != 125)
						{
							return false;
						}
					}
					ctx.L.UngetChar();
					ctx.Return = true;
					ctx.NextState = 1;
					return true;
					IL_F4:
					ctx.L.string_buffer.Append((char)ctx.L.input_char);
					ctx.NextState = 7;
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00091128 File Offset: 0x0008F328
		private static bool State4(FsmContext ctx)
		{
			ctx.L.GetChar();
			if (ctx.L.input_char == 32 || (ctx.L.input_char >= 9 && ctx.L.input_char <= 13))
			{
				ctx.Return = true;
				ctx.NextState = 1;
				return true;
			}
			int num = ctx.L.input_char;
			if (num <= 69)
			{
				if (num != 44)
				{
					if (num == 46)
					{
						ctx.L.string_buffer.Append((char)ctx.L.input_char);
						ctx.NextState = 5;
						return true;
					}
					if (num != 69)
					{
						return false;
					}
					goto IL_BB;
				}
			}
			else if (num != 93)
			{
				if (num == 101)
				{
					goto IL_BB;
				}
				if (num != 125)
				{
					return false;
				}
			}
			ctx.L.UngetChar();
			ctx.Return = true;
			ctx.NextState = 1;
			return true;
			IL_BB:
			ctx.L.string_buffer.Append((char)ctx.L.input_char);
			ctx.NextState = 7;
			return true;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00091218 File Offset: 0x0008F418
		private static bool State5(FsmContext ctx)
		{
			ctx.L.GetChar();
			if (ctx.L.input_char >= 48 && ctx.L.input_char <= 57)
			{
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
				ctx.NextState = 6;
				return true;
			}
			return false;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00091278 File Offset: 0x0008F478
		private static bool State6(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				if (ctx.L.input_char >= 48 && ctx.L.input_char <= 57)
				{
					ctx.L.string_buffer.Append((char)ctx.L.input_char);
				}
				else
				{
					if (ctx.L.input_char == 32 || (ctx.L.input_char >= 9 && ctx.L.input_char <= 13))
					{
						ctx.Return = true;
						ctx.NextState = 1;
						return true;
					}
					int num = ctx.L.input_char;
					if (num <= 69)
					{
						if (num != 44)
						{
							if (num != 69)
							{
								return false;
							}
							goto IL_C9;
						}
					}
					else if (num != 93)
					{
						if (num == 101)
						{
							goto IL_C9;
						}
						if (num != 125)
						{
							return false;
						}
					}
					ctx.L.UngetChar();
					ctx.Return = true;
					ctx.NextState = 1;
					return true;
					IL_C9:
					ctx.L.string_buffer.Append((char)ctx.L.input_char);
					ctx.NextState = 7;
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00091388 File Offset: 0x0008F588
		private static bool State7(FsmContext ctx)
		{
			ctx.L.GetChar();
			if (ctx.L.input_char >= 48 && ctx.L.input_char <= 57)
			{
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
				ctx.NextState = 8;
				return true;
			}
			int num = ctx.L.input_char;
			if (num == 43 || num == 45)
			{
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
				ctx.NextState = 8;
				return true;
			}
			return false;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x00091424 File Offset: 0x0008F624
		private static bool State8(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				if (ctx.L.input_char >= 48 && ctx.L.input_char <= 57)
				{
					ctx.L.string_buffer.Append((char)ctx.L.input_char);
				}
				else
				{
					if (ctx.L.input_char == 32 || (ctx.L.input_char >= 9 && ctx.L.input_char <= 13))
					{
						ctx.Return = true;
						ctx.NextState = 1;
						return true;
					}
					int num = ctx.L.input_char;
					if (num == 44 || num == 93 || num == 125)
					{
						ctx.L.UngetChar();
						ctx.Return = true;
						ctx.NextState = 1;
						return true;
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000914FC File Offset: 0x0008F6FC
		private static bool State9(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 114)
			{
				ctx.NextState = 10;
				return true;
			}
			return false;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x00091534 File Offset: 0x0008F734
		private static bool State10(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 117)
			{
				ctx.NextState = 11;
				return true;
			}
			return false;
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0009156C File Offset: 0x0008F76C
		private static bool State11(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 101)
			{
				ctx.Return = true;
				ctx.NextState = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000915A8 File Offset: 0x0008F7A8
		private static bool State12(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 97)
			{
				ctx.NextState = 13;
				return true;
			}
			return false;
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x000915E0 File Offset: 0x0008F7E0
		private static bool State13(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 108)
			{
				ctx.NextState = 14;
				return true;
			}
			return false;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x00091618 File Offset: 0x0008F818
		private static bool State14(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 115)
			{
				ctx.NextState = 15;
				return true;
			}
			return false;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x00091650 File Offset: 0x0008F850
		private static bool State15(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 101)
			{
				ctx.Return = true;
				ctx.NextState = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0009168C File Offset: 0x0008F88C
		private static bool State16(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 117)
			{
				ctx.NextState = 17;
				return true;
			}
			return false;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000916C4 File Offset: 0x0008F8C4
		private static bool State17(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 108)
			{
				ctx.NextState = 18;
				return true;
			}
			return false;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x000916FC File Offset: 0x0008F8FC
		private static bool State18(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 108)
			{
				ctx.Return = true;
				ctx.NextState = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00091738 File Offset: 0x0008F938
		private static bool State19(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				int num = ctx.L.input_char;
				if (num == 34)
				{
					ctx.L.UngetChar();
					ctx.Return = true;
					ctx.NextState = 20;
					return true;
				}
				if (num == 92)
				{
					ctx.StateStack = 19;
					ctx.NextState = 21;
					return true;
				}
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
			}
			return true;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x000917B8 File Offset: 0x0008F9B8
		private static bool State20(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 34)
			{
				ctx.Return = true;
				ctx.NextState = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000917F4 File Offset: 0x0008F9F4
		private static bool State21(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num <= 92)
			{
				if (num <= 39)
				{
					if (num != 34 && num != 39)
					{
						return false;
					}
				}
				else if (num != 47 && num != 92)
				{
					return false;
				}
			}
			else if (num <= 102)
			{
				if (num != 98 && num != 102)
				{
					return false;
				}
			}
			else if (num != 110)
			{
				switch (num)
				{
				case 114:
				case 116:
					break;
				case 115:
					return false;
				case 117:
					ctx.NextState = 22;
					return true;
				default:
					return false;
				}
			}
			ctx.L.string_buffer.Append(Lexer.ProcessEscChar(ctx.L.input_char));
			ctx.NextState = ctx.StateStack;
			return true;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000918A8 File Offset: 0x0008FAA8
		private static bool State22(FsmContext ctx)
		{
			int num = 0;
			int num2 = 4096;
			ctx.L.unichar = 0;
			while (ctx.L.GetChar())
			{
				if ((ctx.L.input_char < 48 || ctx.L.input_char > 57) && (ctx.L.input_char < 65 || ctx.L.input_char > 70) && (ctx.L.input_char < 97 || ctx.L.input_char > 102))
				{
					return false;
				}
				ctx.L.unichar += Lexer.HexValue(ctx.L.input_char) * num2;
				num++;
				num2 /= 16;
				if (num == 4)
				{
					ctx.L.string_buffer.Append(Convert.ToChar(ctx.L.unichar));
					ctx.NextState = ctx.StateStack;
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0009199C File Offset: 0x0008FB9C
		private static bool State23(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				int num = ctx.L.input_char;
				if (num == 39)
				{
					ctx.L.UngetChar();
					ctx.Return = true;
					ctx.NextState = 24;
					return true;
				}
				if (num == 92)
				{
					ctx.StateStack = 23;
					ctx.NextState = 21;
					return true;
				}
				ctx.L.string_buffer.Append((char)ctx.L.input_char);
			}
			return true;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00091A1C File Offset: 0x0008FC1C
		private static bool State24(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 39)
			{
				ctx.L.input_char = 34;
				ctx.Return = true;
				ctx.NextState = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00091A64 File Offset: 0x0008FC64
		private static bool State25(FsmContext ctx)
		{
			ctx.L.GetChar();
			int num = ctx.L.input_char;
			if (num == 42)
			{
				ctx.NextState = 27;
				return true;
			}
			if (num != 47)
			{
				return false;
			}
			ctx.NextState = 26;
			return true;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00091AAA File Offset: 0x0008FCAA
		private static bool State26(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				if (ctx.L.input_char == 10)
				{
					ctx.NextState = 1;
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00091AD4 File Offset: 0x0008FCD4
		private static bool State27(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				if (ctx.L.input_char == 42)
				{
					ctx.NextState = 28;
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00091B00 File Offset: 0x0008FD00
		private static bool State28(FsmContext ctx)
		{
			while (ctx.L.GetChar())
			{
				if (ctx.L.input_char != 42)
				{
					if (ctx.L.input_char == 47)
					{
						ctx.NextState = 1;
						return true;
					}
					ctx.NextState = 27;
					return true;
				}
			}
			return true;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00091B50 File Offset: 0x0008FD50
		private bool GetChar()
		{
			if ((this.input_char = this.NextChar()) != -1)
			{
				return true;
			}
			this.end_of_input = true;
			return false;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00091B79 File Offset: 0x0008FD79
		private int NextChar()
		{
			if (this.input_buffer != 0)
			{
				int result = this.input_buffer;
				this.input_buffer = 0;
				return result;
			}
			return this.reader.Read();
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00091B9C File Offset: 0x0008FD9C
		public bool NextToken()
		{
			this.fsm_context.Return = false;
			while (Lexer.fsm_handler_table[this.state - 1](this.fsm_context))
			{
				if (this.end_of_input)
				{
					return false;
				}
				if (this.fsm_context.Return)
				{
					this.string_value = this.string_buffer.ToString();
					this.string_buffer.Remove(0, this.string_buffer.Length);
					this.token = Lexer.fsm_return_table[this.state - 1];
					if (this.token == 65542)
					{
						this.token = this.input_char;
					}
					this.state = this.fsm_context.NextState;
					return true;
				}
				this.state = this.fsm_context.NextState;
			}
			throw new JsonException(this.input_char);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00091C71 File Offset: 0x0008FE71
		private void UngetChar()
		{
			this.input_buffer = this.input_char;
		}

		// Token: 0x0400113D RID: 4413
		private static int[] fsm_return_table;

		// Token: 0x0400113E RID: 4414
		private static Lexer.StateHandler[] fsm_handler_table;

		// Token: 0x0400113F RID: 4415
		private bool allow_comments;

		// Token: 0x04001140 RID: 4416
		private bool allow_single_quoted_strings;

		// Token: 0x04001141 RID: 4417
		private bool end_of_input;

		// Token: 0x04001142 RID: 4418
		private FsmContext fsm_context;

		// Token: 0x04001143 RID: 4419
		private int input_buffer;

		// Token: 0x04001144 RID: 4420
		private int input_char;

		// Token: 0x04001145 RID: 4421
		private TextReader reader;

		// Token: 0x04001146 RID: 4422
		private int state;

		// Token: 0x04001147 RID: 4423
		private StringBuilder string_buffer;

		// Token: 0x04001148 RID: 4424
		private string string_value;

		// Token: 0x04001149 RID: 4425
		private int token;

		// Token: 0x0400114A RID: 4426
		private int unichar;

		// Token: 0x020008B7 RID: 2231
		// (Invoke) Token: 0x06004D0F RID: 19727
		private delegate bool StateHandler(FsmContext ctx);
	}
}
