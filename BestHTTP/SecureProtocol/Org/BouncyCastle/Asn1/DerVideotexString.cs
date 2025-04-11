using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000652 RID: 1618
	public class DerVideotexString : DerStringBase
	{
		// Token: 0x06003CC3 RID: 15555 RVA: 0x00174D48 File Offset: 0x00172F48
		public static DerVideotexString GetInstance(object obj)
		{
			if (obj == null || obj is DerVideotexString)
			{
				return (DerVideotexString)obj;
			}
			if (obj is byte[])
			{
				try
				{
					return (DerVideotexString)Asn1Object.FromByteArray((byte[])obj);
				}
				catch (Exception ex)
				{
					throw new ArgumentException("encoding error in GetInstance: " + ex.ToString(), "obj");
				}
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x00174DCC File Offset: 0x00172FCC
		public static DerVideotexString GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerVideotexString)
			{
				return DerVideotexString.GetInstance(@object);
			}
			return new DerVideotexString(((Asn1OctetString)@object).GetOctets());
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x00174E02 File Offset: 0x00173002
		public DerVideotexString(byte[] encoding)
		{
			this.mString = Arrays.Clone(encoding);
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x00174E16 File Offset: 0x00173016
		public override string GetString()
		{
			return Strings.FromByteArray(this.mString);
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x00174E23 File Offset: 0x00173023
		public byte[] GetOctets()
		{
			return Arrays.Clone(this.mString);
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x00174E30 File Offset: 0x00173030
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(21, this.mString);
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x00174E40 File Offset: 0x00173040
		protected override int Asn1GetHashCode()
		{
			return Arrays.GetHashCode(this.mString);
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x00174E50 File Offset: 0x00173050
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerVideotexString derVideotexString = asn1Object as DerVideotexString;
			return derVideotexString != null && Arrays.AreEqual(this.mString, derVideotexString.mString);
		}

		// Token: 0x040025C9 RID: 9673
		private readonly byte[] mString;
	}
}
