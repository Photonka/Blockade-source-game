using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms
{
	// Token: 0x02000767 RID: 1895
	public class AuthenticatedData : Asn1Encodable
	{
		// Token: 0x0600444C RID: 17484 RVA: 0x0018FFA4 File Offset: 0x0018E1A4
		public AuthenticatedData(OriginatorInfo originatorInfo, Asn1Set recipientInfos, AlgorithmIdentifier macAlgorithm, AlgorithmIdentifier digestAlgorithm, ContentInfo encapsulatedContent, Asn1Set authAttrs, Asn1OctetString mac, Asn1Set unauthAttrs)
		{
			if ((digestAlgorithm != null || authAttrs != null) && (digestAlgorithm == null || authAttrs == null))
			{
				throw new ArgumentException("digestAlgorithm and authAttrs must be set together");
			}
			this.version = new DerInteger(AuthenticatedData.CalculateVersion(originatorInfo));
			this.originatorInfo = originatorInfo;
			this.macAlgorithm = macAlgorithm;
			this.digestAlgorithm = digestAlgorithm;
			this.recipientInfos = recipientInfos;
			this.encapsulatedContentInfo = encapsulatedContent;
			this.authAttrs = authAttrs;
			this.mac = mac;
			this.unauthAttrs = unauthAttrs;
		}

		// Token: 0x0600444D RID: 17485 RVA: 0x00190020 File Offset: 0x0018E220
		private AuthenticatedData(Asn1Sequence seq)
		{
			int num = 0;
			this.version = (DerInteger)seq[num++];
			Asn1Encodable asn1Encodable = seq[num++];
			if (asn1Encodable is Asn1TaggedObject)
			{
				this.originatorInfo = OriginatorInfo.GetInstance((Asn1TaggedObject)asn1Encodable, false);
				asn1Encodable = seq[num++];
			}
			this.recipientInfos = Asn1Set.GetInstance(asn1Encodable);
			this.macAlgorithm = AlgorithmIdentifier.GetInstance(seq[num++]);
			asn1Encodable = seq[num++];
			if (asn1Encodable is Asn1TaggedObject)
			{
				this.digestAlgorithm = AlgorithmIdentifier.GetInstance((Asn1TaggedObject)asn1Encodable, false);
				asn1Encodable = seq[num++];
			}
			this.encapsulatedContentInfo = ContentInfo.GetInstance(asn1Encodable);
			asn1Encodable = seq[num++];
			if (asn1Encodable is Asn1TaggedObject)
			{
				this.authAttrs = Asn1Set.GetInstance((Asn1TaggedObject)asn1Encodable, false);
				asn1Encodable = seq[num++];
			}
			this.mac = Asn1OctetString.GetInstance(asn1Encodable);
			if (seq.Count > num)
			{
				this.unauthAttrs = Asn1Set.GetInstance((Asn1TaggedObject)seq[num], false);
			}
		}

		// Token: 0x0600444E RID: 17486 RVA: 0x0019013C File Offset: 0x0018E33C
		public static AuthenticatedData GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return AuthenticatedData.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x0600444F RID: 17487 RVA: 0x0019014A File Offset: 0x0018E34A
		public static AuthenticatedData GetInstance(object obj)
		{
			if (obj == null || obj is AuthenticatedData)
			{
				return (AuthenticatedData)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new AuthenticatedData((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid AuthenticatedData: " + Platform.GetTypeName(obj));
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06004450 RID: 17488 RVA: 0x00190187 File Offset: 0x0018E387
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06004451 RID: 17489 RVA: 0x0019018F File Offset: 0x0018E38F
		public OriginatorInfo OriginatorInfo
		{
			get
			{
				return this.originatorInfo;
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06004452 RID: 17490 RVA: 0x00190197 File Offset: 0x0018E397
		public Asn1Set RecipientInfos
		{
			get
			{
				return this.recipientInfos;
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06004453 RID: 17491 RVA: 0x0019019F File Offset: 0x0018E39F
		public AlgorithmIdentifier MacAlgorithm
		{
			get
			{
				return this.macAlgorithm;
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x001901A7 File Offset: 0x0018E3A7
		public AlgorithmIdentifier DigestAlgorithm
		{
			get
			{
				return this.digestAlgorithm;
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x001901AF File Offset: 0x0018E3AF
		public ContentInfo EncapsulatedContentInfo
		{
			get
			{
				return this.encapsulatedContentInfo;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06004456 RID: 17494 RVA: 0x001901B7 File Offset: 0x0018E3B7
		public Asn1Set AuthAttrs
		{
			get
			{
				return this.authAttrs;
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06004457 RID: 17495 RVA: 0x001901BF File Offset: 0x0018E3BF
		public Asn1OctetString Mac
		{
			get
			{
				return this.mac;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x001901C7 File Offset: 0x0018E3C7
		public Asn1Set UnauthAttrs
		{
			get
			{
				return this.unauthAttrs;
			}
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x001901D0 File Offset: 0x0018E3D0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version
			});
			if (this.originatorInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.originatorInfo)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.recipientInfos,
				this.macAlgorithm
			});
			if (this.digestAlgorithm != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.digestAlgorithm)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.encapsulatedContentInfo
			});
			if (this.authAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 2, this.authAttrs)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.mac
			});
			if (this.unauthAttrs != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 3, this.unauthAttrs)
				});
			}
			return new BerSequence(asn1EncodableVector);
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x001902D0 File Offset: 0x0018E4D0
		public static int CalculateVersion(OriginatorInfo origInfo)
		{
			if (origInfo == null)
			{
				return 0;
			}
			int result = 0;
			foreach (object obj in origInfo.Certificates)
			{
				if (obj is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
					if (asn1TaggedObject.TagNo == 2)
					{
						result = 1;
					}
					else if (asn1TaggedObject.TagNo == 3)
					{
						result = 3;
						break;
					}
				}
			}
			foreach (object obj2 in origInfo.Crls)
			{
				if (obj2 is Asn1TaggedObject && ((Asn1TaggedObject)obj2).TagNo == 1)
				{
					result = 3;
					break;
				}
			}
			return result;
		}

		// Token: 0x04002BC3 RID: 11203
		private DerInteger version;

		// Token: 0x04002BC4 RID: 11204
		private OriginatorInfo originatorInfo;

		// Token: 0x04002BC5 RID: 11205
		private Asn1Set recipientInfos;

		// Token: 0x04002BC6 RID: 11206
		private AlgorithmIdentifier macAlgorithm;

		// Token: 0x04002BC7 RID: 11207
		private AlgorithmIdentifier digestAlgorithm;

		// Token: 0x04002BC8 RID: 11208
		private ContentInfo encapsulatedContentInfo;

		// Token: 0x04002BC9 RID: 11209
		private Asn1Set authAttrs;

		// Token: 0x04002BCA RID: 11210
		private Asn1OctetString mac;

		// Token: 0x04002BCB RID: 11211
		private Asn1Set unauthAttrs;
	}
}
