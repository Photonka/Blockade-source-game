using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000762 RID: 1890
	public class SinglePubInfo : Asn1Encodable
	{
		// Token: 0x0600442A RID: 17450 RVA: 0x0018F966 File Offset: 0x0018DB66
		private SinglePubInfo(Asn1Sequence seq)
		{
			this.pubMethod = DerInteger.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.pubLocation = GeneralName.GetInstance(seq[1]);
			}
		}

		// Token: 0x0600442B RID: 17451 RVA: 0x0018F99B File Offset: 0x0018DB9B
		public static SinglePubInfo GetInstance(object obj)
		{
			if (obj is SinglePubInfo)
			{
				return (SinglePubInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SinglePubInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x0600442C RID: 17452 RVA: 0x0018F9DA File Offset: 0x0018DBDA
		public virtual GeneralName PubLocation
		{
			get
			{
				return this.pubLocation;
			}
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x0018F9E4 File Offset: 0x0018DBE4
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.pubMethod
			});
			asn1EncodableVector.AddOptional(new Asn1Encodable[]
			{
				this.pubLocation
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002BBB RID: 11195
		private readonly DerInteger pubMethod;

		// Token: 0x04002BBC RID: 11196
		private readonly GeneralName pubLocation;
	}
}
