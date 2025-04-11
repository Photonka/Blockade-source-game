using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000761 RID: 1889
	public class ProofOfPossession : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06004422 RID: 17442 RVA: 0x0018F838 File Offset: 0x0018DA38
		private ProofOfPossession(Asn1TaggedObject tagged)
		{
			this.tagNo = tagged.TagNo;
			switch (this.tagNo)
			{
			case 0:
				this.obj = DerNull.Instance;
				return;
			case 1:
				this.obj = PopoSigningKey.GetInstance(tagged, false);
				return;
			case 2:
			case 3:
				this.obj = PopoPrivKey.GetInstance(tagged, false);
				return;
			default:
				throw new ArgumentException("unknown tag: " + this.tagNo, "tagged");
			}
		}

		// Token: 0x06004423 RID: 17443 RVA: 0x0018F8BD File Offset: 0x0018DABD
		public static ProofOfPossession GetInstance(object obj)
		{
			if (obj is ProofOfPossession)
			{
				return (ProofOfPossession)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new ProofOfPossession((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004424 RID: 17444 RVA: 0x0018F8FC File Offset: 0x0018DAFC
		public ProofOfPossession()
		{
			this.tagNo = 0;
			this.obj = DerNull.Instance;
		}

		// Token: 0x06004425 RID: 17445 RVA: 0x0018F916 File Offset: 0x0018DB16
		public ProofOfPossession(PopoSigningKey Poposk)
		{
			this.tagNo = 1;
			this.obj = Poposk;
		}

		// Token: 0x06004426 RID: 17446 RVA: 0x0018F92C File Offset: 0x0018DB2C
		public ProofOfPossession(int type, PopoPrivKey privkey)
		{
			this.tagNo = type;
			this.obj = privkey;
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06004427 RID: 17447 RVA: 0x0018F942 File Offset: 0x0018DB42
		public virtual int Type
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06004428 RID: 17448 RVA: 0x0018F94A File Offset: 0x0018DB4A
		public virtual Asn1Encodable Object
		{
			get
			{
				return this.obj;
			}
		}

		// Token: 0x06004429 RID: 17449 RVA: 0x0018F952 File Offset: 0x0018DB52
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(false, this.tagNo, this.obj);
		}

		// Token: 0x04002BB5 RID: 11189
		public const int TYPE_RA_VERIFIED = 0;

		// Token: 0x04002BB6 RID: 11190
		public const int TYPE_SIGNING_KEY = 1;

		// Token: 0x04002BB7 RID: 11191
		public const int TYPE_KEY_ENCIPHERMENT = 2;

		// Token: 0x04002BB8 RID: 11192
		public const int TYPE_KEY_AGREEMENT = 3;

		// Token: 0x04002BB9 RID: 11193
		private readonly int tagNo;

		// Token: 0x04002BBA RID: 11194
		private readonly Asn1Encodable obj;
	}
}
