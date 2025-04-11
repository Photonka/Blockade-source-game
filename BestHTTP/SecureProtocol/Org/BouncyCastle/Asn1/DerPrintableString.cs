using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000645 RID: 1605
	public class DerPrintableString : DerStringBase
	{
		// Token: 0x06003C6A RID: 15466 RVA: 0x0017410B File Offset: 0x0017230B
		public static DerPrintableString GetInstance(object obj)
		{
			if (obj == null || obj is DerPrintableString)
			{
				return (DerPrintableString)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x00174134 File Offset: 0x00172334
		public static DerPrintableString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerPrintableString)
			{
				return DerPrintableString.GetInstance(@object);
			}
			return new DerPrintableString(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x0017416A File Offset: 0x0017236A
		public DerPrintableString(byte[] str) : this(Strings.FromAsciiByteArray(str), false)
		{
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x00174179 File Offset: 0x00172379
		public DerPrintableString(string str) : this(str, false)
		{
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x00174183 File Offset: 0x00172383
		public DerPrintableString(string str, bool validate)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (validate && !DerPrintableString.IsPrintableString(str))
			{
				throw new ArgumentException("string contains illegal characters", "str");
			}
			this.str = str;
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x001741BB File Offset: 0x001723BB
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x001741C3 File Offset: 0x001723C3
		public byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.str);
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x001741D0 File Offset: 0x001723D0
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(19, this.GetOctets());
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x001741E0 File Offset: 0x001723E0
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerPrintableString derPrintableString = asn1Object as DerPrintableString;
			return derPrintableString != null && this.str.Equals(derPrintableString.str);
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x0017420C File Offset: 0x0017240C
		public static bool IsPrintableString(string str)
		{
			foreach (char c in str)
			{
				if (c > '\u007f')
				{
					return false;
				}
				if (!char.IsLetterOrDigit(c))
				{
					if (c <= ':')
					{
						switch (c)
						{
						case ' ':
						case '\'':
						case '(':
						case ')':
						case '+':
						case ',':
						case '-':
						case '.':
						case '/':
							goto IL_7E;
						case '!':
						case '"':
						case '#':
						case '$':
						case '%':
						case '&':
						case '*':
							break;
						default:
							if (c == ':')
							{
								goto IL_7E;
							}
							break;
						}
					}
					else if (c == '=' || c == '?')
					{
						goto IL_7E;
					}
					return false;
				}
				IL_7E:;
			}
			return true;
		}

		// Token: 0x040025BD RID: 9661
		private readonly string str;
	}
}
