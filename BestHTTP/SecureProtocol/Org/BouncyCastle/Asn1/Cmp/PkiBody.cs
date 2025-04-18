﻿using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007A6 RID: 1958
	public class PkiBody : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x0600461C RID: 17948 RVA: 0x00195213 File Offset: 0x00193413
		public static PkiBody GetInstance(object obj)
		{
			if (obj is PkiBody)
			{
				return (PkiBody)obj;
			}
			if (obj is Asn1TaggedObject)
			{
				return new PkiBody((Asn1TaggedObject)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x00195252 File Offset: 0x00193452
		private PkiBody(Asn1TaggedObject tagged)
		{
			this.tagNo = tagged.TagNo;
			this.body = PkiBody.GetBodyForType(this.tagNo, tagged.GetObject());
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x0019527D File Offset: 0x0019347D
		public PkiBody(int type, Asn1Encodable content)
		{
			this.tagNo = type;
			this.body = PkiBody.GetBodyForType(type, content);
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x0019529C File Offset: 0x0019349C
		private static Asn1Encodable GetBodyForType(int type, Asn1Encodable o)
		{
			switch (type)
			{
			case 0:
				return CertReqMessages.GetInstance(o);
			case 1:
				return CertRepMessage.GetInstance(o);
			case 2:
				return CertReqMessages.GetInstance(o);
			case 3:
				return CertRepMessage.GetInstance(o);
			case 4:
				return CertificationRequest.GetInstance(o);
			case 5:
				return PopoDecKeyChallContent.GetInstance(o);
			case 6:
				return PopoDecKeyRespContent.GetInstance(o);
			case 7:
				return CertReqMessages.GetInstance(o);
			case 8:
				return CertRepMessage.GetInstance(o);
			case 9:
				return CertReqMessages.GetInstance(o);
			case 10:
				return KeyRecRepContent.GetInstance(o);
			case 11:
				return RevReqContent.GetInstance(o);
			case 12:
				return RevRepContent.GetInstance(o);
			case 13:
				return CertReqMessages.GetInstance(o);
			case 14:
				return CertRepMessage.GetInstance(o);
			case 15:
				return CAKeyUpdAnnContent.GetInstance(o);
			case 16:
				return CmpCertificate.GetInstance(o);
			case 17:
				return RevAnnContent.GetInstance(o);
			case 18:
				return CrlAnnContent.GetInstance(o);
			case 19:
				return PkiConfirmContent.GetInstance(o);
			case 20:
				return PkiMessages.GetInstance(o);
			case 21:
				return GenMsgContent.GetInstance(o);
			case 22:
				return GenRepContent.GetInstance(o);
			case 23:
				return ErrorMsgContent.GetInstance(o);
			case 24:
				return CertConfirmContent.GetInstance(o);
			case 25:
				return PollReqContent.GetInstance(o);
			case 26:
				return PollRepContent.GetInstance(o);
			default:
				throw new ArgumentException("unknown tag number: " + type, "type");
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06004620 RID: 17952 RVA: 0x001953F7 File Offset: 0x001935F7
		public virtual int Type
		{
			get
			{
				return this.tagNo;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06004621 RID: 17953 RVA: 0x001953FF File Offset: 0x001935FF
		public virtual Asn1Encodable Content
		{
			get
			{
				return this.body;
			}
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x00195407 File Offset: 0x00193607
		public override Asn1Object ToAsn1Object()
		{
			return new DerTaggedObject(true, this.tagNo, this.body);
		}

		// Token: 0x04002CA8 RID: 11432
		public const int TYPE_INIT_REQ = 0;

		// Token: 0x04002CA9 RID: 11433
		public const int TYPE_INIT_REP = 1;

		// Token: 0x04002CAA RID: 11434
		public const int TYPE_CERT_REQ = 2;

		// Token: 0x04002CAB RID: 11435
		public const int TYPE_CERT_REP = 3;

		// Token: 0x04002CAC RID: 11436
		public const int TYPE_P10_CERT_REQ = 4;

		// Token: 0x04002CAD RID: 11437
		public const int TYPE_POPO_CHALL = 5;

		// Token: 0x04002CAE RID: 11438
		public const int TYPE_POPO_REP = 6;

		// Token: 0x04002CAF RID: 11439
		public const int TYPE_KEY_UPDATE_REQ = 7;

		// Token: 0x04002CB0 RID: 11440
		public const int TYPE_KEY_UPDATE_REP = 8;

		// Token: 0x04002CB1 RID: 11441
		public const int TYPE_KEY_RECOVERY_REQ = 9;

		// Token: 0x04002CB2 RID: 11442
		public const int TYPE_KEY_RECOVERY_REP = 10;

		// Token: 0x04002CB3 RID: 11443
		public const int TYPE_REVOCATION_REQ = 11;

		// Token: 0x04002CB4 RID: 11444
		public const int TYPE_REVOCATION_REP = 12;

		// Token: 0x04002CB5 RID: 11445
		public const int TYPE_CROSS_CERT_REQ = 13;

		// Token: 0x04002CB6 RID: 11446
		public const int TYPE_CROSS_CERT_REP = 14;

		// Token: 0x04002CB7 RID: 11447
		public const int TYPE_CA_KEY_UPDATE_ANN = 15;

		// Token: 0x04002CB8 RID: 11448
		public const int TYPE_CERT_ANN = 16;

		// Token: 0x04002CB9 RID: 11449
		public const int TYPE_REVOCATION_ANN = 17;

		// Token: 0x04002CBA RID: 11450
		public const int TYPE_CRL_ANN = 18;

		// Token: 0x04002CBB RID: 11451
		public const int TYPE_CONFIRM = 19;

		// Token: 0x04002CBC RID: 11452
		public const int TYPE_NESTED = 20;

		// Token: 0x04002CBD RID: 11453
		public const int TYPE_GEN_MSG = 21;

		// Token: 0x04002CBE RID: 11454
		public const int TYPE_GEN_REP = 22;

		// Token: 0x04002CBF RID: 11455
		public const int TYPE_ERROR = 23;

		// Token: 0x04002CC0 RID: 11456
		public const int TYPE_CERT_CONFIRM = 24;

		// Token: 0x04002CC1 RID: 11457
		public const int TYPE_POLL_REQ = 25;

		// Token: 0x04002CC2 RID: 11458
		public const int TYPE_POLL_REP = 26;

		// Token: 0x04002CC3 RID: 11459
		private int tagNo;

		// Token: 0x04002CC4 RID: 11460
		private Asn1Encodable body;
	}
}
