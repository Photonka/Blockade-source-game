using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000726 RID: 1830
	public class EssCertIDv2 : Asn1Encodable
	{
		// Token: 0x06004290 RID: 17040 RVA: 0x0018A148 File Offset: 0x00188348
		public static EssCertIDv2 GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			EssCertIDv2 essCertIDv = obj as EssCertIDv2;
			if (essCertIDv != null)
			{
				return essCertIDv;
			}
			return new EssCertIDv2(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06004291 RID: 17041 RVA: 0x0018A174 File Offset: 0x00188374
		private EssCertIDv2(Asn1Sequence seq)
		{
			if (seq.Count > 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			int num = 0;
			if (seq[0] is Asn1OctetString)
			{
				this.hashAlgorithm = EssCertIDv2.DefaultAlgID;
			}
			else
			{
				this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[num++].ToAsn1Object());
			}
			this.certHash = Asn1OctetString.GetInstance(seq[num++].ToAsn1Object()).GetOctets();
			if (seq.Count > num)
			{
				this.issuerSerial = IssuerSerial.GetInstance(Asn1Sequence.GetInstance(seq[num].ToAsn1Object()));
			}
		}

		// Token: 0x06004292 RID: 17042 RVA: 0x0018A22D File Offset: 0x0018842D
		public EssCertIDv2(byte[] certHash) : this(null, certHash, null)
		{
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x0018A238 File Offset: 0x00188438
		public EssCertIDv2(AlgorithmIdentifier algId, byte[] certHash) : this(algId, certHash, null)
		{
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x0018A243 File Offset: 0x00188443
		public EssCertIDv2(byte[] certHash, IssuerSerial issuerSerial) : this(null, certHash, issuerSerial)
		{
		}

		// Token: 0x06004295 RID: 17045 RVA: 0x0018A24E File Offset: 0x0018844E
		public EssCertIDv2(AlgorithmIdentifier algId, byte[] certHash, IssuerSerial issuerSerial)
		{
			if (algId == null)
			{
				this.hashAlgorithm = EssCertIDv2.DefaultAlgID;
			}
			else
			{
				this.hashAlgorithm = algId;
			}
			this.certHash = certHash;
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x0018A27B File Offset: 0x0018847B
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x0018A283 File Offset: 0x00188483
		public byte[] GetCertHash()
		{
			return Arrays.Clone(this.certHash);
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06004298 RID: 17048 RVA: 0x0018A290 File Offset: 0x00188490
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x0018A298 File Offset: 0x00188498
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (!this.hashAlgorithm.Equals(EssCertIDv2.DefaultAlgID))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.hashAlgorithm
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				new DerOctetString(this.certHash).ToAsn1Object()
			});
			if (this.issuerSerial != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerSerial
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002AD3 RID: 10963
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04002AD4 RID: 10964
		private readonly byte[] certHash;

		// Token: 0x04002AD5 RID: 10965
		private readonly IssuerSerial issuerSerial;

		// Token: 0x04002AD6 RID: 10966
		private static readonly AlgorithmIdentifier DefaultAlgID = new AlgorithmIdentifier(NistObjectIdentifiers.IdSha256);
	}
}
