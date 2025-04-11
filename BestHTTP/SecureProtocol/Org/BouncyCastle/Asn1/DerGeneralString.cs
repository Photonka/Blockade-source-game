using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200063A RID: 1594
	public class DerGeneralString : DerStringBase
	{
		// Token: 0x06003C06 RID: 15366 RVA: 0x00173147 File Offset: 0x00171347
		public static DerGeneralString GetInstance(object obj)
		{
			if (obj == null || obj is DerGeneralString)
			{
				return (DerGeneralString)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x00173170 File Offset: 0x00171370
		public static DerGeneralString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerGeneralString)
			{
				return DerGeneralString.GetInstance(@object);
			}
			return new DerGeneralString(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x001731A6 File Offset: 0x001713A6
		public DerGeneralString(byte[] str) : this(Strings.FromAsciiByteArray(str))
		{
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x001731B4 File Offset: 0x001713B4
		public DerGeneralString(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x001731D1 File Offset: 0x001713D1
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x001731D9 File Offset: 0x001713D9
		public byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.str);
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x001731E6 File Offset: 0x001713E6
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(27, this.GetOctets());
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x001731F8 File Offset: 0x001713F8
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerGeneralString derGeneralString = asn1Object as DerGeneralString;
			return derGeneralString != null && this.str.Equals(derGeneralString.str);
		}

		// Token: 0x040025AD RID: 9645
		private readonly string str;
	}
}
