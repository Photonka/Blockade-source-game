using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x02000612 RID: 1554
	public abstract class Asn1Object : Asn1Encodable
	{
		// Token: 0x06003AF7 RID: 15095 RVA: 0x001700E8 File Offset: 0x0016E2E8
		public static Asn1Object FromByteArray(byte[] data)
		{
			Asn1Object result;
			try
			{
				MemoryStream memoryStream = new MemoryStream(data, false);
				Asn1Object asn1Object = new Asn1InputStream(memoryStream, data.Length).ReadObject();
				if (memoryStream.Position != memoryStream.Length)
				{
					throw new IOException("extra data found after object");
				}
				result = asn1Object;
			}
			catch (InvalidCastException)
			{
				throw new IOException("cannot recognise object in byte array");
			}
			return result;
		}

		// Token: 0x06003AF8 RID: 15096 RVA: 0x00170144 File Offset: 0x0016E344
		public static Asn1Object FromStream(Stream inStr)
		{
			Asn1Object result;
			try
			{
				result = new Asn1InputStream(inStr).ReadObject();
			}
			catch (InvalidCastException)
			{
				throw new IOException("cannot recognise object in stream");
			}
			return result;
		}

		// Token: 0x06003AF9 RID: 15097 RVA: 0x00096BA2 File Offset: 0x00094DA2
		public sealed override Asn1Object ToAsn1Object()
		{
			return this;
		}

		// Token: 0x06003AFA RID: 15098
		internal abstract void Encode(DerOutputStream derOut);

		// Token: 0x06003AFB RID: 15099
		protected abstract bool Asn1Equals(Asn1Object asn1Object);

		// Token: 0x06003AFC RID: 15100
		protected abstract int Asn1GetHashCode();

		// Token: 0x06003AFD RID: 15101 RVA: 0x0017017C File Offset: 0x0016E37C
		internal bool CallAsn1Equals(Asn1Object obj)
		{
			return this.Asn1Equals(obj);
		}

		// Token: 0x06003AFE RID: 15102 RVA: 0x00170185 File Offset: 0x0016E385
		internal int CallAsn1GetHashCode()
		{
			return this.Asn1GetHashCode();
		}
	}
}
