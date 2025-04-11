using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x0200073B RID: 1851
	public class OtherHashAlgAndValue : Asn1Encodable
	{
		// Token: 0x06004318 RID: 17176 RVA: 0x0018C0A4 File Offset: 0x0018A2A4
		public static OtherHashAlgAndValue GetInstance(object obj)
		{
			if (obj == null || obj is OtherHashAlgAndValue)
			{
				return (OtherHashAlgAndValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new OtherHashAlgAndValue((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in 'OtherHashAlgAndValue' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004319 RID: 17177 RVA: 0x0018C0F4 File Offset: 0x0018A2F4
		private OtherHashAlgAndValue(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			if (seq.Count != 2)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count, "seq");
			}
			this.hashAlgorithm = AlgorithmIdentifier.GetInstance(seq[0].ToAsn1Object());
			this.hashValue = (Asn1OctetString)seq[1].ToAsn1Object();
		}

		// Token: 0x0600431A RID: 17178 RVA: 0x0018C16C File Offset: 0x0018A36C
		public OtherHashAlgAndValue(AlgorithmIdentifier hashAlgorithm, byte[] hashValue)
		{
			if (hashAlgorithm == null)
			{
				throw new ArgumentNullException("hashAlgorithm");
			}
			if (hashValue == null)
			{
				throw new ArgumentNullException("hashValue");
			}
			this.hashAlgorithm = hashAlgorithm;
			this.hashValue = new DerOctetString(hashValue);
		}

		// Token: 0x0600431B RID: 17179 RVA: 0x0018C1A3 File Offset: 0x0018A3A3
		public OtherHashAlgAndValue(AlgorithmIdentifier hashAlgorithm, Asn1OctetString hashValue)
		{
			if (hashAlgorithm == null)
			{
				throw new ArgumentNullException("hashAlgorithm");
			}
			if (hashValue == null)
			{
				throw new ArgumentNullException("hashValue");
			}
			this.hashAlgorithm = hashAlgorithm;
			this.hashValue = hashValue;
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x0018C1D5 File Offset: 0x0018A3D5
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x0600431D RID: 17181 RVA: 0x0018C1DD File Offset: 0x0018A3DD
		public byte[] GetHashValue()
		{
			return this.hashValue.GetOctets();
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x0018C1EA File Offset: 0x0018A3EA
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.hashAlgorithm,
				this.hashValue
			});
		}

		// Token: 0x04002B0C RID: 11020
		private readonly AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04002B0D RID: 11021
		private readonly Asn1OctetString hashValue;
	}
}
