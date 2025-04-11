using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Esf
{
	// Token: 0x02000741 RID: 1857
	public class SignaturePolicyIdentifier : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06004345 RID: 17221 RVA: 0x0018CD5C File Offset: 0x0018AF5C
		public static SignaturePolicyIdentifier GetInstance(object obj)
		{
			if (obj == null || obj is SignaturePolicyIdentifier)
			{
				return (SignaturePolicyIdentifier)obj;
			}
			if (obj is SignaturePolicyId)
			{
				return new SignaturePolicyIdentifier((SignaturePolicyId)obj);
			}
			if (obj is Asn1Null)
			{
				return new SignaturePolicyIdentifier();
			}
			throw new ArgumentException("Unknown object in 'SignaturePolicyIdentifier' factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x0018CDB7 File Offset: 0x0018AFB7
		public SignaturePolicyIdentifier()
		{
			this.sigPolicy = null;
		}

		// Token: 0x06004347 RID: 17223 RVA: 0x0018CDC6 File Offset: 0x0018AFC6
		public SignaturePolicyIdentifier(SignaturePolicyId signaturePolicyId)
		{
			if (signaturePolicyId == null)
			{
				throw new ArgumentNullException("signaturePolicyId");
			}
			this.sigPolicy = signaturePolicyId;
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06004348 RID: 17224 RVA: 0x0018CDE3 File Offset: 0x0018AFE3
		public SignaturePolicyId SignaturePolicyId
		{
			get
			{
				return this.sigPolicy;
			}
		}

		// Token: 0x06004349 RID: 17225 RVA: 0x0018CDEB File Offset: 0x0018AFEB
		public override Asn1Object ToAsn1Object()
		{
			if (this.sigPolicy != null)
			{
				return this.sigPolicy.ToAsn1Object();
			}
			return DerNull.Instance;
		}

		// Token: 0x04002B1A RID: 11034
		private readonly SignaturePolicyId sigPolicy;
	}
}
