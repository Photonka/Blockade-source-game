using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x0200028F RID: 655
	public class TimeStampRequestGenerator
	{
		// Token: 0x0600183E RID: 6206 RVA: 0x000BBACA File Offset: 0x000B9CCA
		public void SetReqPolicy(string reqPolicy)
		{
			this.reqPolicy = new DerObjectIdentifier(reqPolicy);
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000BBAD8 File Offset: 0x000B9CD8
		public void SetCertReq(bool certReq)
		{
			this.certReq = DerBoolean.GetInstance(certReq);
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x000BBAE6 File Offset: 0x000B9CE6
		[Obsolete("Use method taking DerObjectIdentifier")]
		public void AddExtension(string oid, bool critical, Asn1Encodable value)
		{
			this.AddExtension(oid, critical, value.GetEncoded());
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x000BBAF8 File Offset: 0x000B9CF8
		[Obsolete("Use method taking DerObjectIdentifier")]
		public void AddExtension(string oid, bool critical, byte[] value)
		{
			DerObjectIdentifier derObjectIdentifier = new DerObjectIdentifier(oid);
			this.extensions[derObjectIdentifier] = new X509Extension(critical, new DerOctetString(value));
			this.extOrdering.Add(derObjectIdentifier);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000BBB31 File Offset: 0x000B9D31
		public virtual void AddExtension(DerObjectIdentifier oid, bool critical, Asn1Encodable extValue)
		{
			this.AddExtension(oid, critical, extValue.GetEncoded());
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000BBB41 File Offset: 0x000B9D41
		public virtual void AddExtension(DerObjectIdentifier oid, bool critical, byte[] extValue)
		{
			this.extensions.Add(oid, new X509Extension(critical, new DerOctetString(extValue)));
			this.extOrdering.Add(oid);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000BBB68 File Offset: 0x000B9D68
		public TimeStampRequest Generate(string digestAlgorithm, byte[] digest)
		{
			return this.Generate(digestAlgorithm, digest, null);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000BBB74 File Offset: 0x000B9D74
		public TimeStampRequest Generate(string digestAlgorithmOid, byte[] digest, BigInteger nonce)
		{
			if (digestAlgorithmOid == null)
			{
				throw new ArgumentException("No digest algorithm specified");
			}
			MessageImprint messageImprint = new MessageImprint(new AlgorithmIdentifier(new DerObjectIdentifier(digestAlgorithmOid), DerNull.Instance), digest);
			X509Extensions x509Extensions = null;
			if (this.extOrdering.Count != 0)
			{
				x509Extensions = new X509Extensions(this.extOrdering, this.extensions);
			}
			DerInteger nonce2 = (nonce == null) ? null : new DerInteger(nonce);
			return new TimeStampRequest(new TimeStampReq(messageImprint, this.reqPolicy, nonce2, this.certReq, x509Extensions));
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000BBBEB File Offset: 0x000B9DEB
		public virtual TimeStampRequest Generate(DerObjectIdentifier digestAlgorithm, byte[] digest)
		{
			return this.Generate(digestAlgorithm.Id, digest);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000BBBFA File Offset: 0x000B9DFA
		public virtual TimeStampRequest Generate(DerObjectIdentifier digestAlgorithm, byte[] digest, BigInteger nonce)
		{
			return this.Generate(digestAlgorithm.Id, digest, nonce);
		}

		// Token: 0x040016FC RID: 5884
		private DerObjectIdentifier reqPolicy;

		// Token: 0x040016FD RID: 5885
		private DerBoolean certReq;

		// Token: 0x040016FE RID: 5886
		private IDictionary extensions = Platform.CreateHashtable();

		// Token: 0x040016FF RID: 5887
		private IList extOrdering = Platform.CreateArrayList();
	}
}
