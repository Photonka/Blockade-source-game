using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000653 RID: 1619
	public class DerVisibleString : DerStringBase
	{
		// Token: 0x06003CCB RID: 15563 RVA: 0x00174E7C File Offset: 0x0017307C
		public static DerVisibleString GetInstance(object obj)
		{
			if (obj == null || obj is DerVisibleString)
			{
				return (DerVisibleString)obj;
			}
			if (obj is Asn1OctetString)
			{
				return new DerVisibleString(((Asn1OctetString)obj).GetOctets());
			}
			if (obj is Asn1TaggedObject)
			{
				return DerVisibleString.GetInstance(((Asn1TaggedObject)obj).GetObject());
			}
			throw new ArgumentException("illegal object in GetInstance: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x00174EE2 File Offset: 0x001730E2
		public static DerVisibleString GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return DerVisibleString.GetInstance(obj.GetObject());
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x00174EEF File Offset: 0x001730EF
		public DerVisibleString(byte[] str) : this(Strings.FromAsciiByteArray(str))
		{
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x00174EFD File Offset: 0x001730FD
		public DerVisibleString(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			this.str = str;
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x00174F1A File Offset: 0x0017311A
		public override string GetString()
		{
			return this.str;
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x00174F22 File Offset: 0x00173122
		public byte[] GetOctets()
		{
			return Strings.ToAsciiByteArray(this.str);
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x00174F2F File Offset: 0x0017312F
		internal override void Encode(DerOutputStream derOut)
		{
			derOut.WriteEncoded(26, this.GetOctets());
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x00174F40 File Offset: 0x00173140
		protected override bool Asn1Equals(Asn1Object asn1Object)
		{
			DerVisibleString derVisibleString = asn1Object as DerVisibleString;
			return derVisibleString != null && this.str.Equals(derVisibleString.str);
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x00174F6A File Offset: 0x0017316A
		protected override int Asn1GetHashCode()
		{
			return this.str.GetHashCode();
		}

		// Token: 0x040025CA RID: 9674
		private readonly string str;
	}
}
