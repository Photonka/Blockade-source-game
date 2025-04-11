using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200063D RID: 1597
	public class DerIA5String : DerStringBase
	{
		// Token: 0x06003C1C RID: 15388 RVA: 0x0017347A File Offset: 0x0017167A
		public static DerIA5String GetInstance(object obj)
		{
			if (obj == null || obj is DerIA5String)
			{
				return (DerIA5String)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x001734A4 File Offset: 0x001716A4
		public static DerIA5String GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerIA5String)
			{
				return DerIA5String.GetInstance(@object);
			}
			return new DerIA5String(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06003C1E RID: 15390 RVA: 0x001734DA File Offset: 0x001716DA
		public DerIA5String(byte[] str) : this(Strings.FromAsciiByteArray(str), false)
		{
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x001734E9 File Offset: 0x001716E9
		public DerIA5String(string str) : this(str, false)
		{
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x001734F3 File Offset: 0x001716F3
		public DerIA5String(string str, bool validate)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (validate && !DerIA5String.IsIA5String(str))
			{
				throw new ArgumentException("string contains illegal characters", "str");
			}
			this.str = str;
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x0017352B File Offset: 0x0017172B
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x00173533 File Offset: 0x00171733
		public byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.str);
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x00173540 File Offset: 0x00171740
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(22, this.GetOctets());
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x00173550 File Offset: 0x00171750
		protected override int Asn1GetHashCode()
		{
			return this.str.GetHashCode();
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x00173560 File Offset: 0x00171760
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerIA5String derIA5String = asn1Object as DerIA5String;
			return derIA5String != null && this.str.Equals(derIA5String.str);
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x0017358C File Offset: 0x0017178C
		public static bool IsIA5String(string str)
		{
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] > '\u007f')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040025B2 RID: 9650
		private readonly string str;
	}
}
