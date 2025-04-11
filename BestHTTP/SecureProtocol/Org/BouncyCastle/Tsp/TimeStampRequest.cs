using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x0200028E RID: 654
	public class TimeStampRequest : X509ExtensionBase
	{
		// Token: 0x0600182D RID: 6189 RVA: 0x000BB846 File Offset: 0x000B9A46
		public TimeStampRequest(TimeStampReq req)
		{
			this.req = req;
			this.extensions = req.Extensions;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x000BB861 File Offset: 0x000B9A61
		public TimeStampRequest(byte[] req) : this(new Asn1InputStream(req))
		{
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000BB86F File Offset: 0x000B9A6F
		public TimeStampRequest(Stream input) : this(new Asn1InputStream(input))
		{
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x000BB880 File Offset: 0x000B9A80
		private TimeStampRequest(Asn1InputStream str)
		{
			try
			{
				this.req = TimeStampReq.GetInstance(str.ReadObject());
			}
			catch (InvalidCastException arg)
			{
				throw new IOException("malformed request: " + arg);
			}
			catch (ArgumentException arg2)
			{
				throw new IOException("malformed request: " + arg2);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06001831 RID: 6193 RVA: 0x000BB8E8 File Offset: 0x000B9AE8
		public int Version
		{
			get
			{
				return this.req.Version.Value.IntValue;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x000BB8FF File Offset: 0x000B9AFF
		public string MessageImprintAlgOid
		{
			get
			{
				return this.req.MessageImprint.HashAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000BB91B File Offset: 0x000B9B1B
		public byte[] GetMessageImprintDigest()
		{
			return this.req.MessageImprint.GetHashedMessage();
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x000BB92D File Offset: 0x000B9B2D
		public string ReqPolicy
		{
			get
			{
				if (this.req.ReqPolicy != null)
				{
					return this.req.ReqPolicy.Id;
				}
				return null;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x000BB94E File Offset: 0x000B9B4E
		public BigInteger Nonce
		{
			get
			{
				if (this.req.Nonce != null)
				{
					return this.req.Nonce.Value;
				}
				return null;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x000BB96F File Offset: 0x000B9B6F
		public bool CertReq
		{
			get
			{
				return this.req.CertReq != null && this.req.CertReq.IsTrue;
			}
		}

		// Token: 0x06001837 RID: 6199 RVA: 0x000BB990 File Offset: 0x000B9B90
		public void Validate(IList algorithms, IList policies, IList extensions)
		{
			if (!algorithms.Contains(this.MessageImprintAlgOid))
			{
				throw new TspValidationException("request contains unknown algorithm", 128);
			}
			if (policies != null && this.ReqPolicy != null && !policies.Contains(this.ReqPolicy))
			{
				throw new TspValidationException("request contains unknown policy", 256);
			}
			if (this.Extensions != null && extensions != null)
			{
				foreach (object obj in this.Extensions.ExtensionOids)
				{
					DerObjectIdentifier derObjectIdentifier = (DerObjectIdentifier)obj;
					if (!extensions.Contains(derObjectIdentifier.Id))
					{
						throw new TspValidationException("request contains unknown extension", 8388608);
					}
				}
			}
			if (TspUtil.GetDigestLength(this.MessageImprintAlgOid) != this.GetMessageImprintDigest().Length)
			{
				throw new TspValidationException("imprint digest the wrong length", 4);
			}
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x000BBA78 File Offset: 0x000B9C78
		public byte[] GetEncoded()
		{
			return this.req.GetEncoded();
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x000BBA85 File Offset: 0x000B9C85
		internal X509Extensions Extensions
		{
			get
			{
				return this.req.Extensions;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x000BBA92 File Offset: 0x000B9C92
		public virtual bool HasExtensions
		{
			get
			{
				return this.extensions != null;
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x000BBA9D File Offset: 0x000B9C9D
		public virtual X509Extension GetExtension(DerObjectIdentifier oid)
		{
			if (this.extensions != null)
			{
				return this.extensions.GetExtension(oid);
			}
			return null;
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x000BBAB5 File Offset: 0x000B9CB5
		public virtual IList GetExtensionOids()
		{
			return TspUtil.GetExtensionOids(this.extensions);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x000BBAC2 File Offset: 0x000B9CC2
		protected override X509Extensions GetX509Extensions()
		{
			return this.Extensions;
		}

		// Token: 0x040016FA RID: 5882
		private TimeStampReq req;

		// Token: 0x040016FB RID: 5883
		private X509Extensions extensions;
	}
}
