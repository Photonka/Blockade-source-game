using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006E7 RID: 1767
	public class PrivateKeyInfo : Asn1Encodable
	{
		// Token: 0x060040F4 RID: 16628 RVA: 0x0018455D File Offset: 0x0018275D
		public static PrivateKeyInfo GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return PrivateKeyInfo.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x0018456B File Offset: 0x0018276B
		public static PrivateKeyInfo GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is PrivateKeyInfo)
			{
				return (PrivateKeyInfo)obj;
			}
			return new PrivateKeyInfo(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x0018458C File Offset: 0x0018278C
		private static int GetVersionValue(DerInteger version)
		{
			BigInteger value = version.Value;
			if (value.CompareTo(BigInteger.Zero) < 0 || value.CompareTo(BigInteger.One) > 0)
			{
				throw new ArgumentException("invalid version for private key info", "version");
			}
			return value.IntValue;
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x001845D2 File Offset: 0x001827D2
		public PrivateKeyInfo(AlgorithmIdentifier privateKeyAlgorithm, Asn1Encodable privateKey) : this(privateKeyAlgorithm, privateKey, null, null)
		{
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x001845DE File Offset: 0x001827DE
		public PrivateKeyInfo(AlgorithmIdentifier privateKeyAlgorithm, Asn1Encodable privateKey, Asn1Set attributes) : this(privateKeyAlgorithm, privateKey, attributes, null)
		{
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x001845EC File Offset: 0x001827EC
		public PrivateKeyInfo(AlgorithmIdentifier privateKeyAlgorithm, Asn1Encodable privateKey, Asn1Set attributes, byte[] publicKey)
		{
			this.version = new DerInteger((publicKey != null) ? BigInteger.One : BigInteger.Zero);
			this.privateKeyAlgorithm = privateKeyAlgorithm;
			this.privateKey = new DerOctetString(privateKey);
			this.attributes = attributes;
			this.publicKey = ((publicKey == null) ? null : new DerBitString(publicKey));
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x00184648 File Offset: 0x00182848
		private PrivateKeyInfo(Asn1Sequence seq)
		{
			IEnumerator enumerator = seq.GetEnumerator();
			this.version = DerInteger.GetInstance(CollectionUtilities.RequireNext(enumerator));
			int versionValue = PrivateKeyInfo.GetVersionValue(this.version);
			this.privateKeyAlgorithm = AlgorithmIdentifier.GetInstance(CollectionUtilities.RequireNext(enumerator));
			this.privateKey = Asn1OctetString.GetInstance(CollectionUtilities.RequireNext(enumerator));
			int num = -1;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)obj;
				int tagNo = asn1TaggedObject.TagNo;
				if (tagNo <= num)
				{
					throw new ArgumentException("invalid optional field in private key info", "seq");
				}
				num = tagNo;
				if (tagNo != 0)
				{
					if (tagNo != 1)
					{
						throw new ArgumentException("unknown optional field in private key info", "seq");
					}
					if (versionValue < 1)
					{
						throw new ArgumentException("'publicKey' requires version v2(1) or later", "seq");
					}
					this.publicKey = DerBitString.GetInstance(asn1TaggedObject, false);
				}
				else
				{
					this.attributes = Asn1Set.GetInstance(asn1TaggedObject, false);
				}
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x060040FB RID: 16635 RVA: 0x00184729 File Offset: 0x00182929
		public virtual Asn1Set Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x060040FC RID: 16636 RVA: 0x00184731 File Offset: 0x00182931
		public virtual bool HasPublicKey
		{
			get
			{
				return this.publicKey != null;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x060040FD RID: 16637 RVA: 0x0018473C File Offset: 0x0018293C
		public virtual AlgorithmIdentifier PrivateKeyAlgorithm
		{
			get
			{
				return this.privateKeyAlgorithm;
			}
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x00184744 File Offset: 0x00182944
		public virtual Asn1Object ParsePrivateKey()
		{
			return Asn1Object.FromByteArray(this.privateKey.GetOctets());
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x00184756 File Offset: 0x00182956
		public virtual Asn1Object ParsePublicKey()
		{
			if (this.publicKey != null)
			{
				return Asn1Object.FromByteArray(this.publicKey.GetOctets());
			}
			return null;
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06004100 RID: 16640 RVA: 0x00184772 File Offset: 0x00182972
		public virtual DerBitString PublicKeyData
		{
			get
			{
				return this.publicKey;
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x0018477C File Offset: 0x0018297C
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.version,
				this.privateKeyAlgorithm,
				this.privateKey
			});
			if (this.attributes != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 0, this.attributes)
				});
			}
			if (this.publicKey != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(false, 1, this.publicKey)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002929 RID: 10537
		private readonly DerInteger version;

		// Token: 0x0400292A RID: 10538
		private readonly AlgorithmIdentifier privateKeyAlgorithm;

		// Token: 0x0400292B RID: 10539
		private readonly Asn1OctetString privateKey;

		// Token: 0x0400292C RID: 10540
		private readonly Asn1Set attributes;

		// Token: 0x0400292D RID: 10541
		private readonly DerBitString publicKey;
	}
}
