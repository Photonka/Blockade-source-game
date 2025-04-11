using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006E9 RID: 1769
	public class RsaesOaepParameters : Asn1Encodable
	{
		// Token: 0x06004109 RID: 16649 RVA: 0x00184930 File Offset: 0x00182B30
		public static RsaesOaepParameters GetInstance(object obj)
		{
			if (obj is RsaesOaepParameters)
			{
				return (RsaesOaepParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RsaesOaepParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x0018496F File Offset: 0x00182B6F
		public RsaesOaepParameters()
		{
			this.hashAlgorithm = RsaesOaepParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsaesOaepParameters.DefaultMaskGenFunction;
			this.pSourceAlgorithm = RsaesOaepParameters.DefaultPSourceAlgorithm;
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x00184998 File Offset: 0x00182B98
		public RsaesOaepParameters(AlgorithmIdentifier hashAlgorithm, AlgorithmIdentifier maskGenAlgorithm, AlgorithmIdentifier pSourceAlgorithm)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.maskGenAlgorithm = maskGenAlgorithm;
			this.pSourceAlgorithm = pSourceAlgorithm;
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x001849B8 File Offset: 0x00182BB8
		public RsaesOaepParameters(Asn1Sequence seq)
		{
			this.hashAlgorithm = RsaesOaepParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsaesOaepParameters.DefaultMaskGenFunction;
			this.pSourceAlgorithm = RsaesOaepParameters.DefaultPSourceAlgorithm;
			for (int num = 0; num != seq.Count; num++)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)seq[num];
				switch (asn1TaggedObject.TagNo)
				{
				case 0:
					this.hashAlgorithm = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				case 1:
					this.maskGenAlgorithm = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				case 2:
					this.pSourceAlgorithm = AlgorithmIdentifier.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag");
				}
			}
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x0600410D RID: 16653 RVA: 0x00184A5D File Offset: 0x00182C5D
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x00184A65 File Offset: 0x00182C65
		public AlgorithmIdentifier MaskGenAlgorithm
		{
			get
			{
				return this.maskGenAlgorithm;
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x0600410F RID: 16655 RVA: 0x00184A6D File Offset: 0x00182C6D
		public AlgorithmIdentifier PSourceAlgorithm
		{
			get
			{
				return this.pSourceAlgorithm;
			}
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x00184A78 File Offset: 0x00182C78
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (!this.hashAlgorithm.Equals(RsaesOaepParameters.DefaultHashAlgorithm))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.hashAlgorithm)
				});
			}
			if (!this.maskGenAlgorithm.Equals(RsaesOaepParameters.DefaultMaskGenFunction))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.maskGenAlgorithm)
				});
			}
			if (!this.pSourceAlgorithm.Equals(RsaesOaepParameters.DefaultPSourceAlgorithm))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.pSourceAlgorithm)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002930 RID: 10544
		private AlgorithmIdentifier hashAlgorithm;

		// Token: 0x04002931 RID: 10545
		private AlgorithmIdentifier maskGenAlgorithm;

		// Token: 0x04002932 RID: 10546
		private AlgorithmIdentifier pSourceAlgorithm;

		// Token: 0x04002933 RID: 10547
		public static readonly AlgorithmIdentifier DefaultHashAlgorithm = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);

		// Token: 0x04002934 RID: 10548
		public static readonly AlgorithmIdentifier DefaultMaskGenFunction = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, RsaesOaepParameters.DefaultHashAlgorithm);

		// Token: 0x04002935 RID: 10549
		public static readonly AlgorithmIdentifier DefaultPSourceAlgorithm = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdPSpecified, new DerOctetString(new byte[0]));
	}
}
