using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x020005C0 RID: 1472
	public class ECDHKekGenerator : IDerivationFunction
	{
		// Token: 0x060038C3 RID: 14531 RVA: 0x00167687 File Offset: 0x00165887
		public ECDHKekGenerator(IDigest digest)
		{
			this.kdf = new Kdf2BytesGenerator(digest);
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x0016769C File Offset: 0x0016589C
		public virtual void Init(IDerivationParameters param)
		{
			DHKdfParameters dhkdfParameters = (DHKdfParameters)param;
			this.algorithm = dhkdfParameters.Algorithm;
			this.keySize = dhkdfParameters.KeySize;
			this.z = dhkdfParameters.GetZ();
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060038C5 RID: 14533 RVA: 0x001676D4 File Offset: 0x001658D4
		public virtual IDigest Digest
		{
			get
			{
				return this.kdf.Digest;
			}
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x001676E4 File Offset: 0x001658E4
		public virtual int GenerateBytes(byte[] outBytes, int outOff, int len)
		{
			DerSequence derSequence = new DerSequence(new Asn1Encodable[]
			{
				new AlgorithmIdentifier(this.algorithm, DerNull.Instance),
				new DerTaggedObject(true, 2, new DerOctetString(Pack.UInt32_To_BE((uint)this.keySize)))
			});
			this.kdf.Init(new KdfParameters(this.z, derSequence.GetDerEncoded()));
			return this.kdf.GenerateBytes(outBytes, outOff, len);
		}

		// Token: 0x04002448 RID: 9288
		private readonly IDerivationFunction kdf;

		// Token: 0x04002449 RID: 9289
		private DerObjectIdentifier algorithm;

		// Token: 0x0400244A RID: 9290
		private int keySize;

		// Token: 0x0400244B RID: 9291
		private byte[] z;
	}
}
