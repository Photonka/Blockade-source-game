using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006FE RID: 1790
	public class ServiceLocator : Asn1Encodable
	{
		// Token: 0x060041AB RID: 16811 RVA: 0x001865EC File Offset: 0x001847EC
		public static ServiceLocator GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ServiceLocator.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x001865FC File Offset: 0x001847FC
		public static ServiceLocator GetInstance(object obj)
		{
			if (obj == null || obj is ServiceLocator)
			{
				return (ServiceLocator)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ServiceLocator((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x00186649 File Offset: 0x00184849
		public ServiceLocator(X509Name issuer) : this(issuer, null)
		{
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x00186653 File Offset: 0x00184853
		public ServiceLocator(X509Name issuer, Asn1Object locator)
		{
			if (issuer == null)
			{
				throw new ArgumentNullException("issuer");
			}
			this.issuer = issuer;
			this.locator = locator;
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x00186677 File Offset: 0x00184877
		private ServiceLocator(Asn1Sequence seq)
		{
			this.issuer = X509Name.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.locator = seq[1].ToAsn1Object();
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x001866AC File Offset: 0x001848AC
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x060041B1 RID: 16817 RVA: 0x001866B4 File Offset: 0x001848B4
		public Asn1Object Locator
		{
			get
			{
				return this.locator;
			}
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x001866BC File Offset: 0x001848BC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.issuer
			});
			if (this.locator != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.locator
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002992 RID: 10642
		private readonly X509Name issuer;

		// Token: 0x04002993 RID: 10643
		private readonly Asn1Object locator;
	}
}
