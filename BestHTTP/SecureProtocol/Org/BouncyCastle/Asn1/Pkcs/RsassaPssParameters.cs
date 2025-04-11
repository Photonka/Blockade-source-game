using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Oiw;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006EB RID: 1771
	public class RsassaPssParameters : Asn1Encodable
	{
		// Token: 0x0600411F RID: 16671 RVA: 0x00184DB4 File Offset: 0x00182FB4
		public static RsassaPssParameters GetInstance(object obj)
		{
			if (obj == null || obj is RsassaPssParameters)
			{
				return (RsassaPssParameters)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new RsassaPssParameters((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004120 RID: 16672 RVA: 0x00184E01 File Offset: 0x00183001
		public RsassaPssParameters()
		{
			this.hashAlgorithm = RsassaPssParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsassaPssParameters.DefaultMaskGenFunction;
			this.saltLength = RsassaPssParameters.DefaultSaltLength;
			this.trailerField = RsassaPssParameters.DefaultTrailerField;
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x00184E35 File Offset: 0x00183035
		public RsassaPssParameters(AlgorithmIdentifier hashAlgorithm, AlgorithmIdentifier maskGenAlgorithm, DerInteger saltLength, DerInteger trailerField)
		{
			this.hashAlgorithm = hashAlgorithm;
			this.maskGenAlgorithm = maskGenAlgorithm;
			this.saltLength = saltLength;
			this.trailerField = trailerField;
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x00184E5C File Offset: 0x0018305C
		public RsassaPssParameters(Asn1Sequence seq)
		{
			this.hashAlgorithm = RsassaPssParameters.DefaultHashAlgorithm;
			this.maskGenAlgorithm = RsassaPssParameters.DefaultMaskGenFunction;
			this.saltLength = RsassaPssParameters.DefaultSaltLength;
			this.trailerField = RsassaPssParameters.DefaultTrailerField;
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
					this.saltLength = DerInteger.GetInstance(asn1TaggedObject, true);
					break;
				case 3:
					this.trailerField = DerInteger.GetInstance(asn1TaggedObject, true);
					break;
				default:
					throw new ArgumentException("unknown tag");
				}
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06004123 RID: 16675 RVA: 0x00184F1F File Offset: 0x0018311F
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.hashAlgorithm;
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x00184F27 File Offset: 0x00183127
		public AlgorithmIdentifier MaskGenAlgorithm
		{
			get
			{
				return this.maskGenAlgorithm;
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x00184F2F File Offset: 0x0018312F
		public DerInteger SaltLength
		{
			get
			{
				return this.saltLength;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x00184F37 File Offset: 0x00183137
		public DerInteger TrailerField
		{
			get
			{
				return this.trailerField;
			}
		}

		// Token: 0x06004127 RID: 16679 RVA: 0x00184F40 File Offset: 0x00183140
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (!this.hashAlgorithm.Equals(RsassaPssParameters.DefaultHashAlgorithm))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.hashAlgorithm)
				});
			}
			if (!this.maskGenAlgorithm.Equals(RsassaPssParameters.DefaultMaskGenFunction))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.maskGenAlgorithm)
				});
			}
			if (!this.saltLength.Equals(RsassaPssParameters.DefaultSaltLength))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.saltLength)
				});
			}
			if (!this.trailerField.Equals(RsassaPssParameters.DefaultTrailerField))
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 3, this.trailerField)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400293E RID: 10558
		private AlgorithmIdentifier hashAlgorithm;

		// Token: 0x0400293F RID: 10559
		private AlgorithmIdentifier maskGenAlgorithm;

		// Token: 0x04002940 RID: 10560
		private DerInteger saltLength;

		// Token: 0x04002941 RID: 10561
		private DerInteger trailerField;

		// Token: 0x04002942 RID: 10562
		public static readonly AlgorithmIdentifier DefaultHashAlgorithm = new AlgorithmIdentifier(OiwObjectIdentifiers.IdSha1, DerNull.Instance);

		// Token: 0x04002943 RID: 10563
		public static readonly AlgorithmIdentifier DefaultMaskGenFunction = new AlgorithmIdentifier(PkcsObjectIdentifiers.IdMgf1, RsassaPssParameters.DefaultHashAlgorithm);

		// Token: 0x04002944 RID: 10564
		public static readonly DerInteger DefaultSaltLength = new DerInteger(20);

		// Token: 0x04002945 RID: 10565
		public static readonly DerInteger DefaultTrailerField = new DerInteger(1);
	}
}
