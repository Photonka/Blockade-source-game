using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000549 RID: 1353
	public class Pkcs5S2ParametersGenerator : PbeParametersGenerator
	{
		// Token: 0x0600333D RID: 13117 RVA: 0x00138009 File Offset: 0x00136209
		public Pkcs5S2ParametersGenerator() : this(new Sha1Digest())
		{
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x00138016 File Offset: 0x00136216
		public Pkcs5S2ParametersGenerator(IDigest digest)
		{
			this.hMac = new HMac(digest);
			this.state = new byte[this.hMac.GetMacSize()];
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x00138040 File Offset: 0x00136240
		private void F(byte[] S, int c, byte[] iBuf, byte[] outBytes, int outOff)
		{
			if (c == 0)
			{
				throw new ArgumentException("iteration count must be at least 1.");
			}
			if (S != null)
			{
				this.hMac.BlockUpdate(S, 0, S.Length);
			}
			this.hMac.BlockUpdate(iBuf, 0, iBuf.Length);
			this.hMac.DoFinal(this.state, 0);
			Array.Copy(this.state, 0, outBytes, outOff, this.state.Length);
			for (int i = 1; i < c; i++)
			{
				this.hMac.BlockUpdate(this.state, 0, this.state.Length);
				this.hMac.DoFinal(this.state, 0);
				for (int j = 0; j < this.state.Length; j++)
				{
					int num = outOff + j;
					outBytes[num] ^= this.state[j];
				}
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x00138110 File Offset: 0x00136310
		private byte[] GenerateDerivedKey(int dkLen)
		{
			int macSize = this.hMac.GetMacSize();
			int num = (dkLen + macSize - 1) / macSize;
			byte[] array = new byte[4];
			byte[] array2 = new byte[num * macSize];
			int num2 = 0;
			ICipherParameters parameters = new KeyParameter(this.mPassword);
			this.hMac.Init(parameters);
			for (int i = 1; i <= num; i++)
			{
				int num3 = 3;
				for (;;)
				{
					byte[] array3 = array;
					int num4 = num3;
					byte b = array3[num4] + 1;
					array3[num4] = b;
					if (b != 0)
					{
						break;
					}
					num3--;
				}
				this.F(this.mSalt, this.mIterationCount, array, array2, num2);
				num2 += macSize;
			}
			return array2;
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x001379B5 File Offset: 0x00135BB5
		public override ICipherParameters GenerateDerivedParameters(int keySize)
		{
			return this.GenerateDerivedMacParameters(keySize);
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x001381AC File Offset: 0x001363AC
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize)
		{
			keySize /= 8;
			byte[] keyBytes = this.GenerateDerivedKey(keySize);
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x001381D0 File Offset: 0x001363D0
		public override ICipherParameters GenerateDerivedParameters(int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			return new ParametersWithIV(new KeyParameter(array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x00138204 File Offset: 0x00136404
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			return new ParametersWithIV(ParameterUtilities.CreateKeyParameter(algorithm, array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x00138236 File Offset: 0x00136436
		public override ICipherParameters GenerateDerivedMacParameters(int keySize)
		{
			keySize /= 8;
			return new KeyParameter(this.GenerateDerivedKey(keySize), 0, keySize);
		}

		// Token: 0x0400208B RID: 8331
		private readonly IMac hMac;

		// Token: 0x0400208C RID: 8332
		private readonly byte[] state;
	}
}
