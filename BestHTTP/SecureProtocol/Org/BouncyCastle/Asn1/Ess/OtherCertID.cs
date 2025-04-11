using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000727 RID: 1831
	[Obsolete("Use version in Asn1.Esf instead")]
	public class OtherCertID : Asn1Encodable
	{
		// Token: 0x0600429B RID: 17051 RVA: 0x0018A32C File Offset: 0x0018852C
		public static OtherCertID GetInstance(object o)
		{
			if (o == null || o is OtherCertID)
			{
				return (OtherCertID)o;
			}
			if (o is Asn1Sequence)
			{
				return new OtherCertID((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'OtherCertID' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x0018A37C File Offset: 0x0018857C
		public OtherCertID(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			if (seq[0].ToAsn1Object() is Asn1OctetString)
			{
				this.otherCertHash = Asn1OctetString.GetInstance(seq[0]);
			}
			else
			{
				this.otherCertHash = DigestInfo.GetInstance(seq[0]);
			}
			if (seq.Count > 1)
			{
				this.issuerSerial = IssuerSerial.GetInstance(Asn1Sequence.GetInstance(seq[1]));
			}
		}

		// Token: 0x0600429D RID: 17053 RVA: 0x0018A415 File Offset: 0x00188615
		public OtherCertID(AlgorithmIdentifier algId, byte[] digest)
		{
			this.otherCertHash = new DigestInfo(algId, digest);
		}

		// Token: 0x0600429E RID: 17054 RVA: 0x0018A42A File Offset: 0x0018862A
		public OtherCertID(AlgorithmIdentifier algId, byte[] digest, IssuerSerial issuerSerial)
		{
			this.otherCertHash = new DigestInfo(algId, digest);
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x0600429F RID: 17055 RVA: 0x0018A446 File Offset: 0x00188646
		public AlgorithmIdentifier AlgorithmHash
		{
			get
			{
				if (this.otherCertHash.ToAsn1Object() is Asn1OctetString)
				{
					return new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1);
				}
				return DigestInfo.GetInstance(this.otherCertHash).AlgorithmID;
			}
		}

		// Token: 0x060042A0 RID: 17056 RVA: 0x0018A475 File Offset: 0x00188675
		public byte[] GetCertHash()
		{
			if (this.otherCertHash.ToAsn1Object() is Asn1OctetString)
			{
				return ((Asn1OctetString)this.otherCertHash.ToAsn1Object()).GetOctets();
			}
			return DigestInfo.GetInstance(this.otherCertHash).GetDigest();
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x060042A1 RID: 17057 RVA: 0x0018A4AF File Offset: 0x001886AF
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x060042A2 RID: 17058 RVA: 0x0018A4B8 File Offset: 0x001886B8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.otherCertHash
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

		// Token: 0x04002AD7 RID: 10967
		private Asn1Encodable otherCertHash;

		// Token: 0x04002AD8 RID: 10968
		private IssuerSerial issuerSerial;
	}
}
