using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Digests;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Generators
{
	// Token: 0x02000546 RID: 1350
	public class OpenSslPbeParametersGenerator : PbeParametersGenerator
	{
		// Token: 0x06003326 RID: 13094 RVA: 0x001378FB File Offset: 0x00135AFB
		public override void Init(byte[] password, byte[] salt, int iterationCount)
		{
			base.Init(password, salt, 1);
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x001378FB File Offset: 0x00135AFB
		public virtual void Init(byte[] password, byte[] salt)
		{
			base.Init(password, salt, 1);
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x00137908 File Offset: 0x00135B08
		private byte[] GenerateDerivedKey(int bytesNeeded)
		{
			byte[] array = new byte[this.digest.GetDigestSize()];
			byte[] array2 = new byte[bytesNeeded];
			int num = 0;
			for (;;)
			{
				this.digest.BlockUpdate(this.mPassword, 0, this.mPassword.Length);
				this.digest.BlockUpdate(this.mSalt, 0, this.mSalt.Length);
				this.digest.DoFinal(array, 0);
				int num2 = (bytesNeeded > array.Length) ? array.Length : bytesNeeded;
				Array.Copy(array, 0, array2, num, num2);
				num += num2;
				bytesNeeded -= num2;
				if (bytesNeeded == 0)
				{
					break;
				}
				this.digest.Reset();
				this.digest.BlockUpdate(array, 0, array.Length);
			}
			return array2;
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x001379B5 File Offset: 0x00135BB5
		[Obsolete("Use version with 'algorithm' parameter")]
		public override ICipherParameters GenerateDerivedParameters(int keySize)
		{
			return this.GenerateDerivedMacParameters(keySize);
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x001379C0 File Offset: 0x00135BC0
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize)
		{
			keySize /= 8;
			byte[] keyBytes = this.GenerateDerivedKey(keySize);
			return ParameterUtilities.CreateKeyParameter(algorithm, keyBytes, 0, keySize);
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x001379E4 File Offset: 0x00135BE4
		[Obsolete("Use version with 'algorithm' parameter")]
		public override ICipherParameters GenerateDerivedParameters(int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			return new ParametersWithIV(new KeyParameter(array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x00137A18 File Offset: 0x00135C18
		public override ICipherParameters GenerateDerivedParameters(string algorithm, int keySize, int ivSize)
		{
			keySize /= 8;
			ivSize /= 8;
			byte[] array = this.GenerateDerivedKey(keySize + ivSize);
			return new ParametersWithIV(ParameterUtilities.CreateKeyParameter(algorithm, array, 0, keySize), array, keySize, ivSize);
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x00137A4A File Offset: 0x00135C4A
		public override ICipherParameters GenerateDerivedMacParameters(int keySize)
		{
			keySize /= 8;
			return new KeyParameter(this.GenerateDerivedKey(keySize), 0, keySize);
		}

		// Token: 0x04002083 RID: 8323
		private readonly IDigest digest = new MD5Digest();
	}
}
