using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200066E RID: 1646
	public class AccessDescription : Asn1Encodable
	{
		// Token: 0x06003D61 RID: 15713 RVA: 0x00176CBC File Offset: 0x00174EBC
		public static AccessDescription GetInstance(object obj)
		{
			if (obj is AccessDescription)
			{
				return (AccessDescription)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AccessDescription((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x00176CFB File Offset: 0x00174EFB
		private AccessDescription(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("wrong number of elements in sequence");
			}
			this.accessMethod = DerObjectIdentifier.GetInstance(seq[0]);
			this.accessLocation = GeneralName.GetInstance(seq[1]);
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x00176D3B File Offset: 0x00174F3B
		public AccessDescription(DerObjectIdentifier oid, GeneralName location)
		{
			this.accessMethod = oid;
			this.accessLocation = location;
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06003D64 RID: 15716 RVA: 0x00176D51 File Offset: 0x00174F51
		public DerObjectIdentifier AccessMethod
		{
			get
			{
				return this.accessMethod;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06003D65 RID: 15717 RVA: 0x00176D59 File Offset: 0x00174F59
		public GeneralName AccessLocation
		{
			get
			{
				return this.accessLocation;
			}
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x00176D61 File Offset: 0x00174F61
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.accessMethod,
				this.accessLocation
			});
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x00176D80 File Offset: 0x00174F80
		public override string ToString()
		{
			return "AccessDescription: Oid(" + this.accessMethod.Id + ")";
		}

		// Token: 0x04002636 RID: 9782
		public static readonly DerObjectIdentifier IdADCAIssuers = new DerObjectIdentifier("1.3.6.1.5.5.7.48.2");

		// Token: 0x04002637 RID: 9783
		public static readonly DerObjectIdentifier IdADOcsp = new DerObjectIdentifier("1.3.6.1.5.5.7.48.1");

		// Token: 0x04002638 RID: 9784
		private readonly DerObjectIdentifier accessMethod;

		// Token: 0x04002639 RID: 9785
		private readonly GeneralName accessLocation;
	}
}
