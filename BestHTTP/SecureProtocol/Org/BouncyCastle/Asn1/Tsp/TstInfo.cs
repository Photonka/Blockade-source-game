using System;
using System.Collections;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp
{
	// Token: 0x020006C7 RID: 1735
	public class TstInfo : Asn1Encodable
	{
		// Token: 0x0600402F RID: 16431 RVA: 0x0018136C File Offset: 0x0017F56C
		public static TstInfo GetInstance(object o)
		{
			if (o == null || o is TstInfo)
			{
				return (TstInfo)o;
			}
			if (o is Asn1Sequence)
			{
				return new TstInfo((Asn1Sequence)o);
			}
			if (o is Asn1OctetString)
			{
				try
				{
					return TstInfo.GetInstance(Asn1Object.FromByteArray(((Asn1OctetString)o).GetOctets()));
				}
				catch (IOException)
				{
					throw new ArgumentException("Bad object format in 'TstInfo' factory.");
				}
			}
			throw new ArgumentException("Unknown object in 'TstInfo' factory: " + Platform.GetTypeName(o));
		}

		// Token: 0x06004030 RID: 16432 RVA: 0x001813F4 File Offset: 0x0017F5F4
		private TstInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			enumerator.MoveNext();
			this.version = DerInteger.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.tsaPolicyId = DerObjectIdentifier.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.messageImprint = MessageImprint.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.serialNumber = DerInteger.GetInstance(enumerator.Current);
			enumerator.MoveNext();
			this.genTime = DerGeneralizedTime.GetInstance(enumerator.Current);
			this.ordering = DerBoolean.False;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1Object asn1Object = (Asn1Object)obj;
				if (asn1Object is Asn1TaggedObject)
				{
					DerTaggedObject derTaggedObject = (DerTaggedObject)asn1Object;
					int tagNo = derTaggedObject.TagNo;
					if (tagNo != 0)
					{
						if (tagNo != 1)
						{
							throw new ArgumentException("Unknown tag value " + derTaggedObject.TagNo);
						}
						this.extensions = X509Extensions.GetInstance(derTaggedObject, false);
					}
					else
					{
						this.tsa = GeneralName.GetInstance(derTaggedObject, true);
					}
				}
				if (asn1Object is DerSequence)
				{
					this.accuracy = Accuracy.GetInstance(asn1Object);
				}
				if (asn1Object is DerBoolean)
				{
					this.ordering = DerBoolean.GetInstance(asn1Object);
				}
				if (asn1Object is DerInteger)
				{
					this.nonce = DerInteger.GetInstance(asn1Object);
				}
			}
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x00181544 File Offset: 0x0017F744
		public TstInfo(DerObjectIdentifier tsaPolicyId, MessageImprint messageImprint, DerInteger serialNumber, DerGeneralizedTime genTime, Accuracy accuracy, DerBoolean ordering, DerInteger nonce, GeneralName tsa, X509Extensions extensions)
		{
			this.version = new DerInteger(1);
			this.tsaPolicyId = tsaPolicyId;
			this.messageImprint = messageImprint;
			this.serialNumber = serialNumber;
			this.genTime = genTime;
			this.accuracy = accuracy;
			this.ordering = ordering;
			this.nonce = nonce;
			this.tsa = tsa;
			this.extensions = extensions;
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x001815A8 File Offset: 0x0017F7A8
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x001815B0 File Offset: 0x0017F7B0
		public MessageImprint MessageImprint
		{
			get
			{
				return this.messageImprint;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x001815B8 File Offset: 0x0017F7B8
		public DerObjectIdentifier Policy
		{
			get
			{
				return this.tsaPolicyId;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06004035 RID: 16437 RVA: 0x001815C0 File Offset: 0x0017F7C0
		public DerInteger SerialNumber
		{
			get
			{
				return this.serialNumber;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06004036 RID: 16438 RVA: 0x001815C8 File Offset: 0x0017F7C8
		public Accuracy Accuracy
		{
			get
			{
				return this.accuracy;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06004037 RID: 16439 RVA: 0x001815D0 File Offset: 0x0017F7D0
		public DerGeneralizedTime GenTime
		{
			get
			{
				return this.genTime;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06004038 RID: 16440 RVA: 0x001815D8 File Offset: 0x0017F7D8
		public DerBoolean Ordering
		{
			get
			{
				return this.ordering;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06004039 RID: 16441 RVA: 0x001815E0 File Offset: 0x0017F7E0
		public DerInteger Nonce
		{
			get
			{
				return this.nonce;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x001815E8 File Offset: 0x0017F7E8
		public GeneralName Tsa
		{
			get
			{
				return this.tsa;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x001815F0 File Offset: 0x0017F7F0
		public X509Extensions Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x0600403C RID: 16444 RVA: 0x001815F8 File Offset: 0x0017F7F8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.tsaPolicyId,
				this.messageImprint,
				this.serialNumber,
				this.genTime
			});
			if (this.accuracy != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.accuracy
				});
			}
			if (this.ordering != null && this.ordering.IsTrue)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.ordering
				});
			}
			if (this.nonce != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.nonce
				});
			}
			if (this.tsa != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.tsa)
				});
			}
			if (this.extensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.extensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040027F9 RID: 10233
		private readonly DerInteger version;

		// Token: 0x040027FA RID: 10234
		private readonly DerObjectIdentifier tsaPolicyId;

		// Token: 0x040027FB RID: 10235
		private readonly MessageImprint messageImprint;

		// Token: 0x040027FC RID: 10236
		private readonly DerInteger serialNumber;

		// Token: 0x040027FD RID: 10237
		private readonly DerGeneralizedTime genTime;

		// Token: 0x040027FE RID: 10238
		private readonly Accuracy accuracy;

		// Token: 0x040027FF RID: 10239
		private readonly DerBoolean ordering;

		// Token: 0x04002800 RID: 10240
		private readonly DerInteger nonce;

		// Token: 0x04002801 RID: 10241
		private readonly GeneralName tsa;

		// Token: 0x04002802 RID: 10242
		private readonly X509Extensions extensions;
	}
}
