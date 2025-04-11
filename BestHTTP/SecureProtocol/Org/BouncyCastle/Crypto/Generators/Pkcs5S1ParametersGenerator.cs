using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000548 RID: 1352
	public class Pkcs5S1ParametersGenerator : PbeParametersGenerator
	{
		// Token: 0x06003336 RID: 13110 RVA: 0x00137E28 File Offset: 0x00136028
		public Pkcs5S1ParametersGenerator(IDigest digest)
		{
			this.digest = digest;
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x00137E38 File Offset: 0x00136038
		private byte[] GenerateDerivedKey()
		{
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.BlockUpdate(this.mPassword, 0, this.mPassword.Length);
			this.digest.BlockUpdate(this.mSalt, 0, this.mSalt.Length);
			this.digest.DoFinal(array, 0);
			for (int i = 1; i < this.mIterationCount; i++)
			{
				this.digest.BlockUpdate(array, 0, array.Length);
				this.digest.DoFinal(array, 0);
			}
			return array;
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x001379B5 File Offset: 0x00135BB5
		public override ICipherParameters GenerateDerivedParameters(int keySize)
		{
			return this.GenerateDerivedMacParameters(keySize);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x00137EC8 File Offset: 0x001360C8
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize)
		{
			keySize /= 8;
			if (keySize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + keySize + " bytes long.");
			}
			byte[] keyBytes = this.GenerateDerivedKey();
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x00137F14 File Offset: 0x00136114
		public override ICipherParameters GenerateDerivedParameters(int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			if (keySize + ivSize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + (keySize + ivSize) + " bytes long.");
			}
			byte[] array = this.GenerateDerivedKey();
			return new ParametersWithIV(new KeyParameter(array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x00137F70 File Offset: 0x00136170
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			if (keySize + ivSize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + (keySize + ivSize) + " bytes long.");
			}
			byte[] array = this.GenerateDerivedKey();
			return new ParametersWithIV(ParameterUtilities.CreateKeyParameter(algorithm, array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x00137FCC File Offset: 0x001361CC
		public override ICipherParameters GenerateDerivedMacParameters(int keySize)
		{
			keySize /= 8;
			if (keySize > this.digest.GetDigestSize())
			{
				throw new ArgumentException("Can't Generate a derived key " + keySize + " bytes long.");
			}
			return new KeyParameter(this.GenerateDerivedKey(), 0, keySize);
		}

		// Token: 0x0400208A RID: 8330
		private readonly IDigest digest;
	}
}
