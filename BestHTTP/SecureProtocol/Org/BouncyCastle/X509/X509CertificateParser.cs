using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security.Certificates;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509
{
	// Token: 0x0200022D RID: 557
	public class X509CertificateParser
	{
		// Token: 0x0600147B RID: 5243 RVA: 0x000ADB04 File Offset: 0x000ABD04
		private X509Certificate ReadDerCertificate(Asn1InputStream dIn)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)dIn.ReadObject();
			if (asn1Sequence.Count > 1 && asn1Sequence[0] is DerObjectIdentifier && asn1Sequence[0].Equals(PkcsObjectIdentifiers.SignedData))
			{
				this.sData = SignedData.GetInstance(Asn1Sequence.GetInstance((Asn1TaggedObject)asn1Sequence[1], true)).Certificates;
				return this.GetCertificate();
			}
			return this.CreateX509Certificate(X509CertificateStructure.GetInstance(asn1Sequence));
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x000ADB7C File Offset: 0x000ABD7C
		private X509Certificate GetCertificate()
		{
			if (this.sData != null)
			{
				while (this.sDataObjectCount < this.sData.Count)
				{
					Asn1Set asn1Set = this.sData;
					int num = this.sDataObjectCount;
					this.sDataObjectCount = num + 1;
					object obj = asn1Set[num];
					if (obj is Asn1Sequence)
					{
						return this.CreateX509Certificate(X509CertificateStructure.GetInstance(obj));
					}
				}
			}
			return null;
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x000ADBDC File Offset: 0x000ABDDC
		private X509Certificate ReadPemCertificate(Stream inStream)
		{
			Asn1Sequence asn1Sequence = X509CertificateParser.PemCertParser.ReadPemObject(inStream);
			if (asn1Sequence != null)
			{
				return this.CreateX509Certificate(X509CertificateStructure.GetInstance(asn1Sequence));
			}
			return null;
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x000ADC06 File Offset: 0x000ABE06
		protected virtual X509Certificate CreateX509Certificate(X509CertificateStructure c)
		{
			return new X509Certificate(c);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000ADC0E File Offset: 0x000ABE0E
		public X509Certificate ReadCertificate(byte[] input)
		{
			return this.ReadCertificate(new MemoryStream(input, false));
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000ADC1D File Offset: 0x000ABE1D
		public ICollection ReadCertificates(byte[] input)
		{
			return this.ReadCertificates(new MemoryStream(input, false));
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x000ADC2C File Offset: 0x000ABE2C
		public X509Certificate ReadCertificate(Stream inStream)
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
				this.sData = null;
				this.sDataObjectCount = 0;
			}
			else if (this.currentStream != inStream)
			{
				this.currentStream = inStream;
				this.sData = null;
				this.sDataObjectCount = 0;
			}
			X509Certificate result;
			try
			{
				if (this.sData != null)
				{
					if (this.sDataObjectCount != this.sData.Count)
					{
						result = this.GetCertificate();
					}
					else
					{
						this.sData = null;
						this.sDataObjectCount = 0;
						result = null;
					}
				}
				else
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
						if (num != 48)
						{
							result = this.ReadPemCertificate(pushbackStream);
						}
						else
						{
							result = this.ReadDerCertificate(new Asn1InputStream(pushbackStream));
						}
					}
				}
			}
			catch (Exception exception)
			{
				throw new CertificateException("Failed to read certificate", exception);
			}
			return result;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x000ADD2C File Offset: 0x000ABF2C
		public ICollection ReadCertificates(Stream inStream)
		{
			IList list = Platform.CreateArrayList();
			X509Certificate value;
			while ((value = this.ReadCertificate(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x040014EB RID: 5355
		private static readonly PemParser PemCertParser = new PemParser("CERTIFICATE");

		// Token: 0x040014EC RID: 5356
		private Asn1Set sData;

		// Token: 0x040014ED RID: 5357
		private int sDataObjectCount;

		// Token: 0x040014EE RID: 5358
		private Stream currentStream;
	}
}
