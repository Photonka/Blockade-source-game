using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020006C5 RID: 1733
	public class TimeStampReq : Asn1Encodable
	{
		// Token: 0x0600401F RID: 16415 RVA: 0x00181037 File Offset: 0x0017F237
		public static TimeStampReq GetInstance(object o)
		{
			if (o == null || o is TimeStampReq)
			{
				return (TimeStampReq)o;
			}
			if (o is Asn1Sequence)
			{
				return new TimeStampReq((Asn1Sequence)o);
			}
			throw new ArgumentException("Unknown object in 'TimeStampReq' factory: " + Platform.GetTypeName(o));
		}

		// Token: 0x06004020 RID: 16416 RVA: 0x00181074 File Offset: 0x0017F274
		private TimeStampReq(Asn1Sequence seq)
		{
			int count = seq.Count;
			int num = 0;
			this.version = DerInteger.GetInstance(seq[num++]);
			this.messageImprint = MessageImprint.GetInstance(seq[num++]);
			for (int i = num; i < count; i++)
			{
				if (seq[i] is DerObjectIdentifier)
				{
					this.tsaPolicy = DerObjectIdentifier.GetInstance(seq[i]);
				}
				else if (seq[i] is DerInteger)
				{
					this.nonce = DerInteger.GetInstance(seq[i]);
				}
				else if (seq[i] is DerBoolean)
				{
					this.certReq = DerBoolean.GetInstance(seq[i]);
				}
				else if (seq[i] is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[i];
					if (asn1TaggedObject.TagNo == 0)
					{
						this.extensions = X509Extensions.GetInstance(asn1TaggedObject, false);
					}
				}
			}
		}

		// Token: 0x06004021 RID: 16417 RVA: 0x00181164 File Offset: 0x0017F364
		public TimeStampReq(MessageImprint messageImprint, DerObjectIdentifier tsaPolicy, DerInteger nonce, DerBoolean certReq, X509Extensions extensions)
		{
			this.version = new DerInteger(1);
			this.messageImprint = messageImprint;
			this.tsaPolicy = tsaPolicy;
			this.nonce = nonce;
			this.certReq = certReq;
			this.extensions = extensions;
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06004022 RID: 16418 RVA: 0x0018119D File Offset: 0x0017F39D
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x001811A5 File Offset: 0x0017F3A5
		public MessageImprint MessageImprint
		{
			get
			{
				return this.messageImprint;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06004024 RID: 16420 RVA: 0x001811AD File Offset: 0x0017F3AD
		public DerObjectIdentifier ReqPolicy
		{
			get
			{
				return this.tsaPolicy;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06004025 RID: 16421 RVA: 0x001811B5 File Offset: 0x0017F3B5
		public DerInteger Nonce
		{
			get
			{
				return this.nonce;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x001811BD File Offset: 0x0017F3BD
		public DerBoolean CertReq
		{
			get
			{
				return this.certReq;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x001811C5 File Offset: 0x0017F3C5
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x001811D0 File Offset: 0x0017F3D0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.messageImprint
			});
			if (this.tsaPolicy != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.tsaPolicy
				});
			}
			if (this.nonce != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.nonce
				});
			}
			if (this.certReq != null && this.certReq.IsTrue)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.certReq
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.extensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040027F1 RID: 10225
		private readonly DerInteger version;

		// Token: 0x040027F2 RID: 10226
		private readonly MessageImprint messageImprint;

		// Token: 0x040027F3 RID: 10227
		private readonly DerObjectIdentifier tsaPolicy;

		// Token: 0x040027F4 RID: 10228
		private readonly DerInteger nonce;

		// Token: 0x040027F5 RID: 10229
		private readonly DerBoolean certReq;

		// Token: 0x040027F6 RID: 10230
		private readonly X509Extensions extensions;
	}
}
