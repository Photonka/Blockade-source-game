using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006F1 RID: 1777
	public class BasicOcspResponse : Asn1Encodable
	{
		// Token: 0x0600414C RID: 16716 RVA: 0x00185721 File Offset: 0x00183921
		public static BasicOcspResponse GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return BasicOcspResponse.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x00185730 File Offset: 0x00183930
		public static BasicOcspResponse GetInstance(object obj)
		{
			if (obj == null || obj is BasicOcspResponse)
			{
				return (BasicOcspResponse)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new BasicOcspResponse((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x0018577D File Offset: 0x0018397D
		public BasicOcspResponse(ResponseData tbsResponseData, AlgorithmIdentifier signatureAlgorithm, DerBitString signature, Asn1Sequence certs)
		{
			this.tbsResponseData = tbsResponseData;
			this.signatureAlgorithm = signatureAlgorithm;
			this.signature = signature;
			this.certs = certs;
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x001857A4 File Offset: 0x001839A4
		private BasicOcspResponse(Asn1Sequence seq)
		{
			this.tbsResponseData = ResponseData.GetInstance(seq[0]);
			this.signatureAlgorithm = AlgorithmIdentifier.GetInstance(seq[1]);
			this.signature = (DerBitString)seq[2];
			if (seq.Count > 3)
			{
				this.certs = Asn1Sequence.GetInstance((Asn1TaggedObject)seq[3], true);
			}
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x0018580E File Offset: 0x00183A0E
		[Obsolete("Use TbsResponseData property instead")]
		public ResponseData GetTbsResponseData()
		{
			return this.tbsResponseData;
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06004151 RID: 16721 RVA: 0x0018580E File Offset: 0x00183A0E
		public ResponseData TbsResponseData
		{
			get
			{
				return this.tbsResponseData;
			}
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x00185816 File Offset: 0x00183A16
		[Obsolete("Use SignatureAlgorithm property instead")]
		public AlgorithmIdentifier GetSignatureAlgorithm()
		{
			return this.signatureAlgorithm;
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06004153 RID: 16723 RVA: 0x00185816 File Offset: 0x00183A16
		public AlgorithmIdentifier SignatureAlgorithm
		{
			get
			{
				return this.signatureAlgorithm;
			}
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x0018581E File Offset: 0x00183A1E
		[Obsolete("Use Signature property instead")]
		public DerBitString GetSignature()
		{
			return this.signature;
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06004155 RID: 16725 RVA: 0x0018581E File Offset: 0x00183A1E
		public DerBitString Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x00185826 File Offset: 0x00183A26
		public byte[] GetSignatureOctets()
		{
			return this.signature.GetOctets();
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x00185833 File Offset: 0x00183A33
		[Obsolete("Use Certs property instead")]
		public Asn1Sequence GetCerts()
		{
			return this.certs;
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06004158 RID: 16728 RVA: 0x00185833 File Offset: 0x00183A33
		public Asn1Sequence Certs
		{
			get
			{
				return this.certs;
			}
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x0018583C File Offset: 0x00183A3C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.tbsResponseData,
				this.signatureAlgorithm,
				this.signature
			});
			if (this.certs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.certs)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002964 RID: 10596
		private readonly ResponseData tbsResponseData;

		// Token: 0x04002965 RID: 10597
		private readonly AlgorithmIdentifier signatureAlgorithm;

		// Token: 0x04002966 RID: 10598
		private readonly DerBitString signature;

		// Token: 0x04002967 RID: 10599
		private readonly Asn1Sequence certs;
	}
}
