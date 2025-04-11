using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006F9 RID: 1785
	public class Request : Asn1Encodable
	{
		// Token: 0x06004182 RID: 16770 RVA: 0x00185F60 File Offset: 0x00184160
		public static Request GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Request.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x00185F70 File Offset: 0x00184170
		public static Request GetInstance(object obj)
		{
			if (obj == null || obj is Request)
			{
				return (Request)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new Request((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x00185FBD File Offset: 0x001841BD
		public Request(CertID reqCert, X509Extensions singleRequestExtensions)
		{
			if (reqCert == null)
			{
				throw new ArgumentNullException("reqCert");
			}
			this.reqCert = reqCert;
			this.singleRequestExtensions = singleRequestExtensions;
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x00185FE1 File Offset: 0x001841E1
		private Request(Asn1Sequence seq)
		{
			this.reqCert = CertID.GetInstance(seq[0]);
			if (seq.Count == 2)
			{
				this.singleRequestExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[1], true);
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06004186 RID: 16774 RVA: 0x0018601C File Offset: 0x0018421C
		public CertID ReqCert
		{
			get
			{
				return this.reqCert;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x00186024 File Offset: 0x00184224
		public X509Extensions SingleRequestExtensions
		{
			get
			{
				return this.singleRequestExtensions;
			}
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x0018602C File Offset: 0x0018422C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.reqCert
			});
			if (this.singleRequestExtensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.singleRequestExtensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002984 RID: 10628
		private readonly CertID reqCert;

		// Token: 0x04002985 RID: 10629
		private readonly X509Extensions singleRequestExtensions;
	}
}
