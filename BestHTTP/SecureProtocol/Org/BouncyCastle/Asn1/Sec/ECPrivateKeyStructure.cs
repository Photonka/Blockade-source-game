using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Sec
{
	// Token: 0x020006D0 RID: 1744
	public class ECPrivateKeyStructure : Asn1Encodable
	{
		// Token: 0x06004062 RID: 16482 RVA: 0x00182033 File Offset: 0x00180233
		public static ECPrivateKeyStructure GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is ECPrivateKeyStructure)
			{
				return (ECPrivateKeyStructure)obj;
			}
			return new ECPrivateKeyStructure(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x06004063 RID: 16483 RVA: 0x00182054 File Offset: 0x00180254
		[Obsolete("Use 'GetInstance' instead")]
		public ECPrivateKeyStructure(Asn1Sequence seq)
		{
			if (seq == null)
			{
				throw new ArgumentNullException("seq");
			}
			this.seq = seq;
		}

		// Token: 0x06004064 RID: 16484 RVA: 0x00182071 File Offset: 0x00180271
		[Obsolete("Use constructor which takes 'orderBitLength' instead, to guarantee correct encoding")]
		public ECPrivateKeyStructure(BigInteger key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.seq = new DerSequence(new Asn1Encodable[]
			{
				new DerInteger(1),
				new DerOctetString(key.ToByteArrayUnsigned())
			});
		}

		// Token: 0x06004065 RID: 16485 RVA: 0x001820AF File Offset: 0x001802AF
		public ECPrivateKeyStructure(int orderBitLength, BigInteger key) : this(orderBitLength, key, null)
		{
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x001820BA File Offset: 0x001802BA
		[Obsolete("Use constructor which takes 'orderBitLength' instead, to guarantee correct encoding")]
		public ECPrivateKeyStructure(BigInteger key, Asn1Encodable parameters) : this(key, null, parameters)
		{
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x001820C8 File Offset: 0x001802C8
		[Obsolete("Use constructor which takes 'orderBitLength' instead, to guarantee correct encoding")]
		public ECPrivateKeyStructure(BigInteger key, DerBitString publicKey, Asn1Encodable parameters)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(1),
				new DerOctetString(key.ToByteArrayUnsigned())
			});
			if (parameters != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, parameters)
				});
			}
			if (publicKey != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, publicKey)
				});
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x0018214C File Offset: 0x0018034C
		public ECPrivateKeyStructure(int orderBitLength, BigInteger key, Asn1Encodable parameters) : this(orderBitLength, key, null, parameters)
		{
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x00182158 File Offset: 0x00180358
		public ECPrivateKeyStructure(int orderBitLength, BigInteger key, DerBitString publicKey, Asn1Encodable parameters)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (orderBitLength < key.BitLength)
			{
				throw new ArgumentException("must be >= key bitlength", "orderBitLength");
			}
			byte[] str = BigIntegers.AsUnsignedByteArray((orderBitLength + 7) / 8, key);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				new DerInteger(1),
				new DerOctetString(str)
			});
			if (parameters != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, parameters)
				});
			}
			if (publicKey != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, publicKey)
				});
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x00182200 File Offset: 0x00180400
		public virtual BigInteger GetKey()
		{
			Asn1OctetString asn1OctetString = (Asn1OctetString)this.seq[1];
			return new BigInteger(1, asn1OctetString.GetOctets());
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x0018222B File Offset: 0x0018042B
		public virtual DerBitString GetPublicKey()
		{
			return (DerBitString)this.GetObjectInTag(1);
		}

		// Token: 0x0600406C RID: 16492 RVA: 0x00182239 File Offset: 0x00180439
		public virtual Asn1Object GetParameters()
		{
			return this.GetObjectInTag(0);
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x00182244 File Offset: 0x00180444
		private Asn1Object GetObjectInTag(int tagNo)
		{
			foreach (object obj in this.seq)
			{
				Asn1Object asn1Object = ((Asn1Encodable)obj).ToAsn1Object();
				if (asn1Object is Asn1TaggedObject)
				{
					Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Object;
					if (asn1TaggedObject.TagNo == tagNo)
					{
						return asn1TaggedObject.GetObject();
					}
				}
			}
			return null;
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x001822C4 File Offset: 0x001804C4
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x04002839 RID: 10297
		private readonly Asn1Sequence seq;
	}
}
