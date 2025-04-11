using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000621 RID: 1569
	public class BerBitString : DerBitString
	{
		// Token: 0x06003B4F RID: 15183 RVA: 0x0017104A File Offset: 0x0016F24A
		public BerBitString(byte[] data, int padBits) : base(data, padBits)
		{
		}

		// Token: 0x06003B50 RID: 15184 RVA: 0x00171054 File Offset: 0x0016F254
		public BerBitString(byte[] data) : base(data)
		{
		}

		// Token: 0x06003B51 RID: 15185 RVA: 0x0017105D File Offset: 0x0016F25D
		public BerBitString(int namedBits) : base(namedBits)
		{
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x00171066 File Offset: 0x0016F266
		public BerBitString(Asn1Encodable obj) : base(obj)
		{
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x0017106F File Offset: 0x0016F26F
		internal override void Encode(DerOutputStream derOut)
		{
			if (derOut is Asn1OutputStream || derOut is BerOutputStream)
			{
				derOut.WriteEncoded(3, (byte)this.mPadBits, this.mData);
				return;
			}
			base.Encode(derOut);
		}
	}
}
