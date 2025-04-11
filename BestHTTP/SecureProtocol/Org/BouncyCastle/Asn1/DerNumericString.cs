using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000640 RID: 1600
	public class DerNumericString : DerStringBase
	{
		// Token: 0x06003C39 RID: 15417 RVA: 0x00173796 File Offset: 0x00171996
		public static DerNumericString GetInstance(object obj)
		{
			if (obj == null || obj is DerNumericString)
			{
				return (DerNumericString)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x001737C0 File Offset: 0x001719C0
		public static DerNumericString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerNumericString)
			{
				return DerNumericString.GetInstance(@object);
			}
			return new DerNumericString(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x001737F6 File Offset: 0x001719F6
		public DerNumericString(byte[] str) : this(Strings.FromAsciiByteArray(str), false)
		{
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x00173805 File Offset: 0x00171A05
		public DerNumericString(string str) : this(str, false)
		{
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x0017380F File Offset: 0x00171A0F
		public DerNumericString(string str, bool validate)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (validate && !DerNumericString.IsNumericString(str))
			{
				throw new ArgumentException("string contains illegal characters", "str");
			}
			this.str = str;
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x00173847 File Offset: 0x00171A47
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x0017384F File Offset: 0x00171A4F
		public byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.str);
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x0017385C File Offset: 0x00171A5C
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(18, this.GetOctets());
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x0017386C File Offset: 0x00171A6C
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerNumericString derNumericString = asn1Object as DerNumericString;
			return derNumericString != null && this.str.Equals(derNumericString.str);
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x00173898 File Offset: 0x00171A98
		public static bool IsNumericString(string str)
		{
			foreach (char c in str)
			{
				if (c > '\u007f' || (c != ' ' && !char.IsDigit(c)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040025B7 RID: 9655
		private readonly string str;
	}
}
