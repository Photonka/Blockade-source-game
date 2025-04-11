using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro
{
	// Token: 0x0200074D RID: 1869
	public class Gost3410PublicKeyAlgParameters : Asn1Encodable
	{
		// Token: 0x0600438B RID: 17291 RVA: 0x0018E20F File Offset: 0x0018C40F
		public static Gost3410PublicKeyAlgParameters GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return Gost3410PublicKeyAlgParameters.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x0600438C RID: 17292 RVA: 0x0018E21D File Offset: 0x0018C41D
		public static Gost3410PublicKeyAlgParameters GetInstance(object obj)
		{
			if (obj == null || obj is Gost3410PublicKeyAlgParameters)
			{
				return (Gost3410PublicKeyAlgParameters)obj;
			}
			return new Gost3410PublicKeyAlgParameters(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x0600438D RID: 17293 RVA: 0x0018E23C File Offset: 0x0018C43C
		public Gost3410PublicKeyAlgParameters(DerObjectIdentifier publicKeyParamSet, DerObjectIdentifier digestParamSet) : this(publicKeyParamSet, digestParamSet, null)
		{
		}

		// Token: 0x0600438E RID: 17294 RVA: 0x0018E247 File Offset: 0x0018C447
		public Gost3410PublicKeyAlgParameters(DerObjectIdentifier publicKeyParamSet, DerObjectIdentifier digestParamSet, DerObjectIdentifier encryptionParamSet)
		{
			if (publicKeyParamSet == null)
			{
				throw new ArgumentNullException("publicKeyParamSet");
			}
			if (digestParamSet == null)
			{
				throw new ArgumentNullException("digestParamSet");
			}
			this.publicKeyParamSet = publicKeyParamSet;
			this.digestParamSet = digestParamSet;
			this.encryptionParamSet = encryptionParamSet;
		}

		// Token: 0x0600438F RID: 17295 RVA: 0x0018E280 File Offset: 0x0018C480
		public Gost3410PublicKeyAlgParameters(Asn1Sequence seq)
		{
			this.publicKeyParamSet = (DerObjectIdentifier)seq[0];
			this.digestParamSet = (DerObjectIdentifier)seq[1];
			if (seq.Count > 2)
			{
				this.encryptionParamSet = (DerObjectIdentifier)seq[2];
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06004390 RID: 17296 RVA: 0x0018E2D2 File Offset: 0x0018C4D2
		public DerObjectIdentifier PublicKeyParamSet
		{
			get
			{
				return this.publicKeyParamSet;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06004391 RID: 17297 RVA: 0x0018E2DA File Offset: 0x0018C4DA
		public DerObjectIdentifier DigestParamSet
		{
			get
			{
				return this.digestParamSet;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06004392 RID: 17298 RVA: 0x0018E2E2 File Offset: 0x0018C4E2
		public DerObjectIdentifier EncryptionParamSet
		{
			get
			{
				return this.encryptionParamSet;
			}
		}

		// Token: 0x06004393 RID: 17299 RVA: 0x0018E2EC File Offset: 0x0018C4EC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.publicKeyParamSet,
				this.digestParamSet
			});
			if (this.encryptionParamSet != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.encryptionParamSet
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002B68 RID: 11112
		private DerObjectIdentifier publicKeyParamSet;

		// Token: 0x04002B69 RID: 11113
		private DerObjectIdentifier digestParamSet;

		// Token: 0x04002B6A RID: 11114
		private DerObjectIdentifier encryptionParamSet;
	}
}
