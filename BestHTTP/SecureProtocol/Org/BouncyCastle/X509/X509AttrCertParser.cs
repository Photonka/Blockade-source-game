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
	// Token: 0x02000229 RID: 553
	public class X509AttrCertParser
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x000ACCC4 File Offset: 0x000AAEC4
		private IX509AttributeCertificate ReadDerCertificate(Asn1InputStream dIn)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)dIn.ReadObject();
			if (asn1Sequence.Count > 1 && asn1Sequence[0] is DerObjectIdentifier && asn1Sequence[0].Equals(PkcsObjectIdentifiers.SignedData))
			{
				this.sData = SignedData.GetInstance(Asn1Sequence.GetInstance((Asn1TaggedObject)asn1Sequence[1], true)).Certificates;
				return this.GetCertificate();
			}
			return new X509V2AttributeCertificate(AttributeCertificate.GetInstance(asn1Sequence));
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000ACD3C File Offset: 0x000AAF3C
		private IX509AttributeCertificate GetCertificate()
		{
			if (this.sData != null)
			{
				while (this.sDataObjectCount < this.sData.Count)
				{
					Asn1Set asn1Set = this.sData;
					int num = this.sDataObjectCount;
					this.sDataObjectCount = num + 1;
					object obj = asn1Set[num];
					if (obj is Asn1TaggedObject && ((Asn1TaggedObject)obj).TagNo == 2)
					{
						return new X509V2AttributeCertificate(AttributeCertificate.GetInstance(Asn1Sequence.GetInstance((Asn1TaggedObject)obj, false)));
					}
				}
			}
			return null;
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x000ACDB4 File Offset: 0x000AAFB4
		private IX509AttributeCertificate ReadPemCertificate(Stream inStream)
		{
			Asn1Sequence asn1Sequence = X509AttrCertParser.PemAttrCertParser.ReadPemObject(inStream);
			if (asn1Sequence != null)
			{
				return new X509V2AttributeCertificate(AttributeCertificate.GetInstance(asn1Sequence));
			}
			return null;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x000ACDDD File Offset: 0x000AAFDD
		public IX509AttributeCertificate ReadAttrCert(byte[] input)
		{
			return this.ReadAttrCert(new MemoryStream(input, false));
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x000ACDEC File Offset: 0x000AAFEC
		public ICollection ReadAttrCerts(byte[] input)
		{
			return this.ReadAttrCerts(new MemoryStream(input, false));
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x000ACDFC File Offset: 0x000AAFFC
		public IX509AttributeCertificate ReadAttrCert(Stream inStream)
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
			IX509AttributeCertificate result;
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
			catch (Exception ex)
			{
				throw new CertificateException(ex.ToString());
			}
			return result;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x000ACEFC File Offset: 0x000AB0FC
		public ICollection ReadAttrCerts(Stream inStream)
		{
			IList list = Platform.CreateArrayList();
			IX509AttributeCertificate value;
			while ((value = this.ReadAttrCert(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x040014DF RID: 5343
		private static readonly PemParser PemAttrCertParser = new PemParser("ATTRIBUTE CERTIFICATE");

		// Token: 0x040014E0 RID: 5344
		private Asn1Set sData;

		// Token: 0x040014E1 RID: 5345
		private int sDataObjectCount;

		// Token: 0x040014E2 RID: 5346
		private Stream currentStream;
	}
}
