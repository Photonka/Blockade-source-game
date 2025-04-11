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
	// Token: 0x02000231 RID: 561
	public class X509CrlParser
	{
		// Token: 0x060014AC RID: 5292 RVA: 0x000AE9A8 File Offset: 0x000ACBA8
		public X509CrlParser() : this(false)
		{
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x000AE9B1 File Offset: 0x000ACBB1
		public X509CrlParser(bool lazyAsn1)
		{
			this.lazyAsn1 = lazyAsn1;
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000AE9C0 File Offset: 0x000ACBC0
		private X509Crl ReadPemCrl(Stream inStream)
		{
			Asn1Sequence asn1Sequence = X509CrlParser.PemCrlParser.ReadPemObject(inStream);
			if (asn1Sequence != null)
			{
				return this.CreateX509Crl(CertificateList.GetInstance(asn1Sequence));
			}
			return null;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x000AE9EC File Offset: 0x000ACBEC
		private X509Crl ReadDerCrl(Asn1InputStream dIn)
		{
			Asn1Sequence asn1Sequence = (Asn1Sequence)dIn.ReadObject();
			if (asn1Sequence.Count > 1 && asn1Sequence[0] is DerObjectIdentifier && asn1Sequence[0].Equals(PkcsObjectIdentifiers.SignedData))
			{
				this.sCrlData = SignedData.GetInstance(Asn1Sequence.GetInstance((Asn1TaggedObject)asn1Sequence[1], true)).Crls;
				return this.GetCrl();
			}
			return this.CreateX509Crl(CertificateList.GetInstance(asn1Sequence));
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x000AEA64 File Offset: 0x000ACC64
		private X509Crl GetCrl()
		{
			if (this.sCrlData == null || this.sCrlDataObjectCount >= this.sCrlData.Count)
			{
				return null;
			}
			Asn1Set asn1Set = this.sCrlData;
			int num = this.sCrlDataObjectCount;
			this.sCrlDataObjectCount = num + 1;
			return this.CreateX509Crl(CertificateList.GetInstance(asn1Set[num]));
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000AEAB5 File Offset: 0x000ACCB5
		protected virtual X509Crl CreateX509Crl(CertificateList c)
		{
			return new X509Crl(c);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x000AEABD File Offset: 0x000ACCBD
		public X509Crl ReadCrl(byte[] input)
		{
			return this.ReadCrl(new MemoryStream(input, false));
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x000AEACC File Offset: 0x000ACCCC
		public ICollection ReadCrls(byte[] input)
		{
			return this.ReadCrls(new MemoryStream(input, false));
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000AEADC File Offset: 0x000ACCDC
		public X509Crl ReadCrl(Stream inStream)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			if (!inStream.CanRead)
			{
				throw new ArgumentException("inStream must be read-able", "inStream");
			}
			if (this.currentCrlStream == null)
			{
				this.currentCrlStream = inStream;
				this.sCrlData = null;
				this.sCrlDataObjectCount = 0;
			}
			else if (this.currentCrlStream != inStream)
			{
				this.currentCrlStream = inStream;
				this.sCrlData = null;
				this.sCrlDataObjectCount = 0;
			}
			X509Crl result;
			try
			{
				if (this.sCrlData != null)
				{
					if (this.sCrlDataObjectCount != this.sCrlData.Count)
					{
						result = this.GetCrl();
					}
					else
					{
						this.sCrlData = null;
						this.sCrlDataObjectCount = 0;
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
							result = this.ReadPemCrl(pushbackStream);
						}
						else
						{
							Asn1InputStream dIn = this.lazyAsn1 ? new LazyAsn1InputStream(pushbackStream) : new Asn1InputStream(pushbackStream);
							result = this.ReadDerCrl(dIn);
						}
					}
				}
			}
			catch (CrlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new CrlException(ex2.ToString());
			}
			return result;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000AEBFC File Offset: 0x000ACDFC
		public ICollection ReadCrls(Stream inStream)
		{
			IList list = Platform.CreateArrayList();
			X509Crl value;
			while ((value = this.ReadCrl(inStream)) != null)
			{
				list.Add(value);
			}
			return list;
		}

		// Token: 0x040014F8 RID: 5368
		private static readonly PemParser PemCrlParser = new PemParser("CRL");

		// Token: 0x040014F9 RID: 5369
		private readonly bool lazyAsn1;

		// Token: 0x040014FA RID: 5370
		private Asn1Set sCrlData;

		// Token: 0x040014FB RID: 5371
		private int sCrlDataObjectCount;

		// Token: 0x040014FC RID: 5372
		private Stream currentCrlStream;
	}
}
