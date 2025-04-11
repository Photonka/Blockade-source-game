using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x0200022E RID: 558
	public class X509CertPairParser
	{
		// Token: 0x06001485 RID: 5253 RVA: 0x000ADD66 File Offset: 0x000ABF66
		private X509CertificatePair ReadDerCrossCertificatePair(Stream inStream)
		{
			return new X509CertificatePair(CertificatePair.GetInstance((Asn1Sequence)new Asn1InputStream(inStream).ReadObject()));
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x000ADD82 File Offset: 0x000ABF82
		public X509CertificatePair ReadCertPair(byte[] input)
		{
			return this.ReadCertPair(new MemoryStream(input, false));
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x000ADD91 File Offset: 0x000ABF91
		public ICollection ReadCertPairs(byte[] input)
		{
			return this.ReadCertPairs(new MemoryStream(input, false));
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x000ADDA0 File Offset: 0x000ABFA0
		public X509CertificatePair ReadCertPair(Stream inStream)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			if (!inStream.CanRead)
			{
				throw new ArgumentException("inStream must be read-able", "inStream");
			}
			if (this.currentStream == null)
			{
				this.currentStream = inStream;
			}
			else if (this.currentStream != inStream)
			{
				this.currentStream = inStream;
			}
			X509CertificatePair result;
			try
			{
				PushbackStream pushbackStream = new PushbackStream(inStream);
				int num = pushbackStream.ReadByte();
				if (num < 0)
				{
					result = null;
				}
				else
				{
					pushbackStream.Unread(num);
					result = this.ReadDerCrossCertificatePair(pushbackStream);
				}
			}
			catch (Exception ex)
			{
				throw new CertificateException(ex.ToString());
			}
			return result;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x000ADE38 File Offset: 0x000AC038
		public ICollection ReadCertPairs(Stream inStream)
		{
			IList list = Platform.CreateArrayList();
			X509CertificatePair value;
			while ((value = this.ReadCertPair(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x040014EF RID: 5359
		private Stream currentStream;
	}
}
