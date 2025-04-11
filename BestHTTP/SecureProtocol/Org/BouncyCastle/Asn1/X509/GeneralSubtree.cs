using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000688 RID: 1672
	public class GeneralSubtree : Asn1Encodable
	{
		// Token: 0x06003E37 RID: 15927 RVA: 0x00179458 File Offset: 0x00177658
		private GeneralSubtree(Asn1Sequence seq)
		{
			this.baseName = GeneralName.GetInstance(seq[0]);
			switch (seq.Count)
			{
			case 1:
				return;
			case 2:
			{
				Asn1TaggedObject instance = Asn1TaggedObject.GetInstance(seq[1]);
				int tagNo = instance.TagNo;
				if (tagNo == 0)
				{
					this.minimum = DerInteger.GetInstance(instance, false);
					return;
				}
				if (tagNo != 1)
				{
					throw new ArgumentException("Bad tag number: " + instance.TagNo);
				}
				this.maximum = DerInteger.GetInstance(instance, false);
				return;
			}
			case 3:
			{
				Asn1TaggedObject instance2 = Asn1TaggedObject.GetInstance(seq[1]);
				if (instance2.TagNo != 0)
				{
					throw new ArgumentException("Bad tag number for 'minimum': " + instance2.TagNo);
				}
				this.minimum = DerInteger.GetInstance(instance2, false);
				Asn1TaggedObject instance3 = Asn1TaggedObject.GetInstance(seq[2]);
				if (instance3.TagNo != 1)
				{
					throw new ArgumentException("Bad tag number for 'maximum': " + instance3.TagNo);
				}
				this.maximum = DerInteger.GetInstance(instance3, false);
				return;
			}
			default:
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
		}

		// Token: 0x06003E38 RID: 15928 RVA: 0x00179588 File Offset: 0x00177788
		public GeneralSubtree(GeneralName baseName, BigInteger minimum, BigInteger maximum)
		{
			this.baseName = baseName;
			if (minimum != null)
			{
				this.minimum = new DerInteger(minimum);
			}
			if (maximum != null)
			{
				this.maximum = new DerInteger(maximum);
			}
		}

		// Token: 0x06003E39 RID: 15929 RVA: 0x001795B5 File Offset: 0x001777B5
		public GeneralSubtree(GeneralName baseName) : this(baseName, null, null)
		{
		}

		// Token: 0x06003E3A RID: 15930 RVA: 0x001795C0 File Offset: 0x001777C0
		public static GeneralSubtree GetInstance(Asn1TaggedObject o, bool isExplicit)
		{
			return new GeneralSubtree(Asn1Sequence.GetInstance(o, isExplicit));
		}

		// Token: 0x06003E3B RID: 15931 RVA: 0x001795CE File Offset: 0x001777CE
		public static GeneralSubtree GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is GeneralSubtree)
			{
				return (GeneralSubtree)obj;
			}
			return new GeneralSubtree(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06003E3C RID: 15932 RVA: 0x001795EF File Offset: 0x001777EF
		public GeneralName Base
		{
			get
			{
				return this.baseName;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06003E3D RID: 15933 RVA: 0x001795F7 File Offset: 0x001777F7
		public BigInteger Minimum
		{
			get
			{
				if (this.minimum != null)
				{
					return this.minimum.Value;
				}
				return BigInteger.Zero;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06003E3E RID: 15934 RVA: 0x00179612 File Offset: 0x00177812
		public BigInteger Maximum
		{
			get
			{
				if (this.maximum != null)
				{
					return this.maximum.Value;
				}
				return null;
			}
		}

		// Token: 0x06003E3F RID: 15935 RVA: 0x0017962C File Offset: 0x0017782C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.baseName
			});
			if (this.minimum != null && this.minimum.Value.SignValue != 0)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.minimum)
				});
			}
			if (this.maximum != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.maximum)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002688 RID: 9864
		private readonly GeneralName baseName;

		// Token: 0x04002689 RID: 9865
		private readonly DerInteger minimum;

		// Token: 0x0400268A RID: 9866
		private readonly DerInteger maximum;
	}
}
