using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200060C RID: 1548
	public abstract class Asn1Encodable : IAsn1Convertible
	{
		// Token: 0x06003ACF RID: 15055 RVA: 0x0016F969 File Offset: 0x0016DB69
		public byte[] GetEncoded()
		{
			MemoryStream memoryStream = new MemoryStream();
			new Asn1OutputStream(memoryStream).WriteObject(this);
			return memoryStream.ToArray();
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x0016F981 File Offset: 0x0016DB81
		public byte[] GetEncoded(string encoding)
		{
			if (encoding.Equals("DER"))
			{
				MemoryStream memoryStream = new MemoryStream();
				new DerOutputStream(memoryStream).WriteObject(this);
				return memoryStream.ToArray();
			}
			return this.GetEncoded();
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x0016F9B0 File Offset: 0x0016DBB0
		public byte[] GetDerEncoded()
		{
			byte[] result;
			try
			{
				result = this.GetEncoded("DER");
			}
			catch (IOException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x0016F9E4 File Offset: 0x0016DBE4
		public sealed override int GetHashCode()
		{
			return this.ToAsn1Object().CallAsn1GetHashCode();
		}

		// Token: 0x06003AD3 RID: 15059 RVA: 0x0016F9F4 File Offset: 0x0016DBF4
		public sealed override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			IAsn1Convertible asn1Convertible = obj as IAsn1Convertible;
			if (asn1Convertible == null)
			{
				return false;
			}
			Asn1Object asn1Object = this.ToAsn1Object();
			Asn1Object asn1Object2 = asn1Convertible.ToAsn1Object();
			return asn1Object == asn1Object2 || asn1Object.CallAsn1Equals(asn1Object2);
		}

		// Token: 0x06003AD4 RID: 15060
		public abstract Asn1Object ToAsn1Object();

		// Token: 0x04002559 RID: 9561
		public const string Der = "DER";

		// Token: 0x0400255A RID: 9562
		public const string Ber = "BER";
	}
}
