using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200064D RID: 1613
	public class DerT61String : DerStringBase
	{
		// Token: 0x06003C98 RID: 15512 RVA: 0x0017469A File Offset: 0x0017289A
		public static DerT61String GetInstance(object obj)
		{
			if (obj == null || obj is DerT61String)
			{
				return (DerT61String)obj;
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003C99 RID: 15513 RVA: 0x001746C4 File Offset: 0x001728C4
		public static DerT61String GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			Asn1Object @object = obj.GetObject();
			if (isExplicit || @object is DerT61String)
			{
				return DerT61String.GetInstance(@object);
			}
			return new DerT61String(Asn1OctetString.GetInstance(@object).GetOctets());
		}

		// Token: 0x06003C9A RID: 15514 RVA: 0x001746FA File Offset: 0x001728FA
		public DerT61String(byte[] str) : this(Strings.FromByteArray(str))
		{
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x00174708 File Offset: 0x00172908
		public DerT61String(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x00174725 File Offset: 0x00172925
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x0017472D File Offset: 0x0017292D
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(20, this.GetOctets());
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x0017473D File Offset: 0x0017293D
		public byte[] GetOctets()
		{
			return Strings.ToByteArray(this.str);
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x0017474C File Offset: 0x0017294C
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerT61String derT61String = asn1Object as DerT61String;
			return derT61String != null && this.str.Equals(derT61String.str);
		}

		// Token: 0x040025C4 RID: 9668
		private readonly string str;
	}
}
