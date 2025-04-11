using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ess
{
	// Token: 0x02000725 RID: 1829
	public class EssCertID : Asn1Encodable
	{
		// Token: 0x06004289 RID: 17033 RVA: 0x00189FFC File Offset: 0x001881FC
		public static EssCertID GetInstance(object o)
		{
			if (o == null || o is EssCertID)
			{
				return (EssCertID)o;
			}
			if (o is Asn1Sequence)
			{
				return new EssCertID((Asn1Sequence)o);
			}
			throw new ArgumentException("unknown object in 'EssCertID' factory : " + Platform.GetTypeName(o) + ".");
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x0018A04C File Offset: 0x0018824C
		public EssCertID(Asn1Sequence seq)
		{
			if (seq.Count < 1 || seq.Count > 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.certHash = Asn1OctetString.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.issuerSerial = IssuerSerial.GetInstance(seq[1]);
			}
		}

		// Token: 0x0600428B RID: 17035 RVA: 0x0018A0B9 File Offset: 0x001882B9
		public EssCertID(byte[] hash)
		{
			this.certHash = new DerOctetString(hash);
		}

		// Token: 0x0600428C RID: 17036 RVA: 0x0018A0CD File Offset: 0x001882CD
		public EssCertID(byte[] hash, IssuerSerial issuerSerial)
		{
			this.certHash = new DerOctetString(hash);
			this.issuerSerial = issuerSerial;
		}

		// Token: 0x0600428D RID: 17037 RVA: 0x0018A0E8 File Offset: 0x001882E8
		public byte[] GetCertHash()
		{
			return this.certHash.GetOctets();
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x0600428E RID: 17038 RVA: 0x0018A0F5 File Offset: 0x001882F5
		public IssuerSerial IssuerSerial
		{
			get
			{
				return this.issuerSerial;
			}
		}

		// Token: 0x0600428F RID: 17039 RVA: 0x0018A100 File Offset: 0x00188300
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.certHash
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

		// Token: 0x04002AD1 RID: 10961
		private Asn1OctetString certHash;

		// Token: 0x04002AD2 RID: 10962
		private IssuerSerial issuerSerial;
	}
}
