using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs
{
	// Token: 0x020006E2 RID: 1762
	public class PbeS2Parameters : Asn1Encodable
	{
		// Token: 0x060040D4 RID: 16596 RVA: 0x0018382C File Offset: 0x00181A2C
		public static PbeS2Parameters GetInstance(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			PbeS2Parameters pbeS2Parameters = obj as PbeS2Parameters;
			if (pbeS2Parameters != null)
			{
				return pbeS2Parameters;
			}
			return new PbeS2Parameters(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x00183855 File Offset: 0x00181A55
		public PbeS2Parameters(KeyDerivationFunc keyDevFunc, EncryptionScheme encScheme)
		{
			this.func = keyDevFunc;
			this.scheme = encScheme;
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x0018386C File Offset: 0x00181A6C
		[Obsolete("Use GetInstance() instead")]
		public PbeS2Parameters(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			Asn1Sequence asn1Sequence = (Asn1Sequence)seq[0].ToAsn1Object();
			if (asn1Sequence[0].Equals(PkcsObjectIdentifiers.IdPbkdf2))
			{
				this.func = new KeyDerivationFunc(PkcsObjectIdentifiers.IdPbkdf2, Pbkdf2Params.GetInstance(asn1Sequence[1]));
			}
			else
			{
				this.func = new KeyDerivationFunc(asn1Sequence);
			}
			this.scheme = EncryptionScheme.GetInstance(seq[1].ToAsn1Object());
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x001838FE File Offset: 0x00181AFE
		public KeyDerivationFunc KeyDerivationFunc
		{
			get
			{
				return this.func;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x00183906 File Offset: 0x00181B06
		public EncryptionScheme EncryptionScheme
		{
			get
			{
				return this.scheme;
			}
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x0018390E File Offset: 0x00181B0E
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.func,
				this.scheme
			});
		}

		// Token: 0x0400288E RID: 10382
		private readonly KeyDerivationFunc func;

		// Token: 0x0400288F RID: 10383
		private readonly EncryptionScheme scheme;
	}
}
