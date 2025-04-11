using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x020005BF RID: 1471
	public class DHKekGenerator : IDerivationFunction
	{
		// Token: 0x060038BF RID: 14527 RVA: 0x001674A6 File Offset: 0x001656A6
		public DHKekGenerator(IDigest digest)
		{
			this.digest = digest;
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x001674B8 File Offset: 0x001656B8
		public virtual void Init(IDerivationParameters param)
		{
			DHKdfParameters dhkdfParameters = (DHKdfParameters)param;
			this.algorithm = dhkdfParameters.Algorithm;
			this.keySize = dhkdfParameters.KeySize;
			this.z = dhkdfParameters.GetZ();
			this.partyAInfo = dhkdfParameters.GetExtraInfo();
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060038C1 RID: 14529 RVA: 0x001674FC File Offset: 0x001656FC
		public virtual IDigest Digest
		{
			get
			{
				return this.digest;
			}
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x00167504 File Offset: 0x00165704
		public virtual int GenerateBytes(byte[] outBytes, int outOff, int len)
		{
			if (outBytes.Length - len < outOff)
			{
				throw new DataLengthException("output buffer too small");
			}
			long num = (long)len;
			int digestSize = this.digest.GetDigestSize();
			if (num > 8589934591L)
			{
				throw new ArgumentException("Output length too large");
			}
			int num2 = (int)((num + (long)digestSize - 1L) / (long)digestSize);
			byte[] array = new byte[this.digest.GetDigestSize()];
			uint num3 = 1U;
			for (int i = 0; i < num2; i++)
			{
				this.digest.BlockUpdate(this.z, 0, this.z.Length);
				DerSequence derSequence = new DerSequence(new Asn1Encodable[]
				{
					this.algorithm,
					new DerOctetString(Pack.UInt32_To_BE(num3))
				});
				Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
				{
					derSequence
				});
				if (this.partyAInfo != null)
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						new DerTaggedObject(true, 0, new DerOctetString(this.partyAInfo))
					});
				}
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, new DerOctetString(Pack.UInt32_To_BE((uint)this.keySize)))
				});
				byte[] derEncoded = new DerSequence(asn1EncodableVector).GetDerEncoded();
				this.digest.BlockUpdate(derEncoded, 0, derEncoded.Length);
				this.digest.DoFinal(array, 0);
				if (len > digestSize)
				{
					Array.Copy(array, 0, outBytes, outOff, digestSize);
					outOff += digestSize;
					len -= digestSize;
				}
				else
				{
					Array.Copy(array, 0, outBytes, outOff, len);
				}
				num3 += 1U;
			}
			this.digest.Reset();
			return (int)num;
		}

		// Token: 0x04002443 RID: 9283
		private readonly IDigest digest;

		// Token: 0x04002444 RID: 9284
		private DerObjectIdentifier algorithm;

		// Token: 0x04002445 RID: 9285
		private int keySize;

		// Token: 0x04002446 RID: 9286
		private byte[] z;

		// Token: 0x04002447 RID: 9287
		private byte[] partyAInfo;
	}
}
