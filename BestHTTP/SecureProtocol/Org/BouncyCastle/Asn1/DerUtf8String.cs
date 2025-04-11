using System;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000651 RID: 1617
	public class DerUtf8String : DerStringBase
	{
		// Token: 0x06003CBC RID: 15548 RVA: 0x00174C67 File Offset: 0x00172E67
		public static DerUtf8String GetInstance(object obj)
		{
			if (obj == null || obj is DerUtf8String)
			{
				return (DerUtf8String)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x00174C90 File Offset: 0x00172E90
		public static DerUtf8String GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerUtf8String)
			{
				return DerUtf8String.GetInstance(@object);
			}
			return new DerUtf8String(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x00174CC6 File Offset: 0x00172EC6
		public DerUtf8String(byte[] str) : this(Encoding.UTF8.GetString(str, 0, str.Length))
		{
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x00174CDD File Offset: 0x00172EDD
		public DerUtf8String(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x00174CFA File Offset: 0x00172EFA
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x00174D04 File Offset: 0x00172F04
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerUtf8String derUtf8String = asn1Object as DerUtf8String;
			return derUtf8String != null && this.str.Equals(derUtf8String.str);
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x00174D2E File Offset: 0x00172F2E
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(12, Encoding.UTF8.GetBytes(this.str));
		}

		// Token: 0x040025C8 RID: 9672
		private readonly string str;
	}
}
