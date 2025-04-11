using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200075F RID: 1887
	public class PopoSigningKey : Asn1Encodable
	{
		// Token: 0x06004412 RID: 17426 RVA: 0x0018F53C File Offset: 0x0018D73C
		private PopoSigningKey(Asn1Sequence seq)
		{
			int index = 0;
			if (seq[index] is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[index++];
				if (asn1TaggedObject.TagNo != 0)
				{
					throw new ArgumentException("Unknown PopoSigningKeyInput tag: " + asn1TaggedObject.TagNo, "seq");
				}
				this.poposkInput = PopoSigningKeyInput.GetInstance(asn1TaggedObject.GetObject());
			}
			this.algorithmIdentifier = AlgorithmIdentifier.GetInstance(seq[index++]);
			this.signature = DerBitString.GetInstance(seq[index]);
		}

		// Token: 0x06004413 RID: 17427 RVA: 0x0018F5D1 File Offset: 0x0018D7D1
		public static PopoSigningKey GetInstance(object obj)
		{
			if (obj is PopoSigningKey)
			{
				return (PopoSigningKey)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PopoSigningKey((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004414 RID: 17428 RVA: 0x0018F610 File Offset: 0x0018D810
		public static PopoSigningKey GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return PopoSigningKey.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06004415 RID: 17429 RVA: 0x0018F61E File Offset: 0x0018D81E
		public PopoSigningKey(PopoSigningKeyInput poposkIn, AlgorithmIdentifier aid, DerBitString signature)
		{
			this.poposkInput = poposkIn;
			this.algorithmIdentifier = aid;
			this.signature = signature;
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06004416 RID: 17430 RVA: 0x0018F63B File Offset: 0x0018D83B
		public virtual PopoSigningKeyInput PoposkInput
		{
			get
			{
				return this.poposkInput;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x0018F643 File Offset: 0x0018D843
		public virtual AlgorithmIdentifier AlgorithmIdentifier
		{
			get
			{
				return this.algorithmIdentifier;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06004418 RID: 17432 RVA: 0x0018F64B File Offset: 0x0018D84B
		public virtual DerBitString Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x06004419 RID: 17433 RVA: 0x0018F654 File Offset: 0x0018D854
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.poposkInput != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.poposkInput)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.algorithmIdentifier
			});
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.signature
			});
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002BAF RID: 11183
		private readonly PopoSigningKeyInput poposkInput;

		// Token: 0x04002BB0 RID: 11184
		private readonly AlgorithmIdentifier algorithmIdentifier;

		// Token: 0x04002BB1 RID: 11185
		private readonly DerBitString signature;
	}
}
