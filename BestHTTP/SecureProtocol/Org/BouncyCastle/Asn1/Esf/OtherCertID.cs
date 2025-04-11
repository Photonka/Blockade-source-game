using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000739 RID: 1849
	public class OtherCertID : Asn1Encodable
	{
		// Token: 0x0600430A RID: 17162 RVA: 0x0018BE50 File Offset: 0x0018A050
		public static OtherCertID GetInstance(object obj)
		{
			if (obj == null || obj is OtherCertID)
			{
				return (OtherCertID)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherCertID((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherCertID' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x0018BEA0 File Offset: 0x0018A0A0
		private OtherCertID(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.otherCertHash = OtherHash.GetInstance(seq[0].ToAsn1Object());
			if (seq.Count > 1)
			{
				this.issuerSerial = IssuerSerial.GetInstance(seq[1].ToAsn1Object());
			}
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x0018BF2A File Offset: 0x0018A12A
		public OtherCertID(OtherHash otherCertHash) : this(otherCertHash, null)
		{
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x0018BF34 File Offset: 0x0018A134
		public OtherCertID(OtherHash otherCertHash, IssuerSerial issuerSerial)
		{
			if (otherCertHash == null)
			{
				throw new ArgumentNullException("otherCertHash");
			}
			this.otherCertHash = otherCertHash;
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600430E RID: 17166 RVA: 0x0018BF58 File Offset: 0x0018A158
		public OtherHash OtherCertHash
		{
			get
			{
				return this.otherCertHash;
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x0018BF60 File Offset: 0x0018A160
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x06004310 RID: 17168 RVA: 0x0018BF68 File Offset: 0x0018A168
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.otherCertHash.ToAsn1Object()
			});
			if (this.issuerSerial != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerSerial.ToAsn1Object()
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B08 RID: 11016
		private readonly OtherHash otherCertHash;

		// Token: 0x04002B09 RID: 11017
		private readonly IssuerSerial issuerSerial;
	}
}
