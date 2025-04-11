using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x02000759 RID: 1881
	public class EncryptedValue : Asn1Encodable
	{
		// Token: 0x060043E7 RID: 17383 RVA: 0x0018EE40 File Offset: 0x0018D040
		private EncryptedValue(Asn1Sequence seq)
		{
			int num = 0;
			while (seq[num] is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[num];
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.intendedAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, false);
					break;
				case 1:
					this.symmAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, false);
					break;
				case 2:
					this.encSymmKey = DerBitString.GetInstance(asn1TaggedObject, false);
					break;
				case 3:
					this.keyAlg = AlgorithmIdentifier.GetInstance(asn1TaggedObject, false);
					break;
				case 4:
					this.valueHint = Asn1OctetString.GetInstance(asn1TaggedObject, false);
					break;
				}
				num++;
			}
			this.encValue = DerBitString.GetInstance(seq[num]);
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x0018EEF7 File Offset: 0x0018D0F7
		public static EncryptedValue GetInstance(object obj)
		{
			if (obj is EncryptedValue)
			{
				return (EncryptedValue)obj;
			}
			if (obj != null)
			{
				return new EncryptedValue(Asn1Sequence.GetInstance(obj));
			}
			return null;
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x0018EF18 File Offset: 0x0018D118
		public EncryptedValue(AlgorithmIdentifier intendedAlg, AlgorithmIdentifier symmAlg, DerBitString encSymmKey, AlgorithmIdentifier keyAlg, Asn1OctetString valueHint, DerBitString encValue)
		{
			if (encValue == null)
			{
				throw new ArgumentNullException("encValue");
			}
			this.intendedAlg = intendedAlg;
			this.symmAlg = symmAlg;
			this.encSymmKey = encSymmKey;
			this.keyAlg = keyAlg;
			this.valueHint = valueHint;
			this.encValue = encValue;
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060043EA RID: 17386 RVA: 0x0018EF67 File Offset: 0x0018D167
		public virtual AlgorithmIdentifier IntendedAlg
		{
			get
			{
				return this.intendedAlg;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x0018EF6F File Offset: 0x0018D16F
		public virtual AlgorithmIdentifier SymmAlg
		{
			get
			{
				return this.symmAlg;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x0018EF77 File Offset: 0x0018D177
		public virtual DerBitString EncSymmKey
		{
			get
			{
				return this.encSymmKey;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060043ED RID: 17389 RVA: 0x0018EF7F File Offset: 0x0018D17F
		public virtual AlgorithmIdentifier KeyAlg
		{
			get
			{
				return this.keyAlg;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060043EE RID: 17390 RVA: 0x0018EF87 File Offset: 0x0018D187
		public virtual Asn1OctetString ValueHint
		{
			get
			{
				return this.valueHint;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060043EF RID: 17391 RVA: 0x0018EF8F File Offset: 0x0018D18F
		public virtual DerBitString EncValue
		{
			get
			{
				return this.encValue;
			}
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x0018EF98 File Offset: 0x0018D198
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			this.AddOptional(asn1EncodableVector, 0, this.intendedAlg);
			this.AddOptional(asn1EncodableVector, 1, this.symmAlg);
			this.AddOptional(asn1EncodableVector, 2, this.encSymmKey);
			this.AddOptional(asn1EncodableVector, 3, this.keyAlg);
			this.AddOptional(asn1EncodableVector, 4, this.valueHint);
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.encValue
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x0018F011 File Offset: 0x0018D211
		private void AddOptional(Asn1EncodableVector v, int tagNo, Asn1Encodable obj)
		{
			if (obj != null)
			{
				v.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, tagNo, obj)
				});
			}
		}

		// Token: 0x04002B98 RID: 11160
		private readonly AlgorithmIdentifier intendedAlg;

		// Token: 0x04002B99 RID: 11161
		private readonly AlgorithmIdentifier symmAlg;

		// Token: 0x04002B9A RID: 11162
		private readonly DerBitString encSymmKey;

		// Token: 0x04002B9B RID: 11163
		private readonly AlgorithmIdentifier keyAlg;

		// Token: 0x04002B9C RID: 11164
		private readonly Asn1OctetString valueHint;

		// Token: 0x04002B9D RID: 11165
		private readonly DerBitString encValue;
	}
}
