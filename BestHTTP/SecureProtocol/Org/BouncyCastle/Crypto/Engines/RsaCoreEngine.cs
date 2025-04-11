using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000576 RID: 1398
	public class RsaCoreEngine : IRsa
	{
		// Token: 0x06003535 RID: 13621 RVA: 0x00149C87 File Offset: 0x00147E87
		private void CheckInitialised()
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("RSA engine not initialised");
			}
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x00149C9C File Offset: 0x00147E9C
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			if (!(parameters is RsaKeyParameters))
			{
				throw new InvalidKeyException("Not an RSA key");
			}
			this.key = (RsaKeyParameters)parameters;
			this.forEncryption = forEncryption;
			this.bitSize = this.key.Modulus.BitLength;
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x00149CFA File Offset: 0x00147EFA
		public virtual int GetInputBlockSize()
		{
			this.CheckInitialised();
			if (this.forEncryption)
			{
				return (this.bitSize - 1) / 8;
			}
			return (this.bitSize + 7) / 8;
		}

		// Token: 0x06003538 RID: 13624 RVA: 0x00149D1F File Offset: 0x00147F1F
		public virtual int GetOutputBlockSize()
		{
			this.CheckInitialised();
			if (this.forEncryption)
			{
				return (this.bitSize + 7) / 8;
			}
			return (this.bitSize - 1) / 8;
		}

		// Token: 0x06003539 RID: 13625 RVA: 0x00149D44 File Offset: 0x00147F44
		public virtual BigInteger ConvertInput(byte[] inBuf, int inOff, int inLen)
		{
			this.CheckInitialised();
			int num = (this.bitSize + 7) / 8;
			if (inLen > num)
			{
				throw new DataLengthException("input too large for RSA cipher.");
			}
			BigInteger bigInteger = new BigInteger(1, inBuf, inOff, inLen);
			if (bigInteger.CompareTo(this.key.Modulus) >= 0)
			{
				throw new DataLengthException("input too large for RSA cipher.");
			}
			return bigInteger;
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x00149D9C File Offset: 0x00147F9C
		public virtual byte[] ConvertOutput(BigInteger result)
		{
			this.CheckInitialised();
			byte[] array = result.ToByteArrayUnsigned();
			if (this.forEncryption)
			{
				int outputBlockSize = this.GetOutputBlockSize();
				if (array.Length < outputBlockSize)
				{
					byte[] array2 = new byte[outputBlockSize];
					array.CopyTo(array2, array2.Length - array.Length);
					array = array2;
				}
			}
			return array;
		}

		// Token: 0x0600353B RID: 13627 RVA: 0x00149DE4 File Offset: 0x00147FE4
		public virtual BigInteger ProcessBlock(BigInteger input)
		{
			this.CheckInitialised();
			if (this.key is RsaPrivateCrtKeyParameters)
			{
				RsaPrivateCrtKeyParameters rsaPrivateCrtKeyParameters = (RsaPrivateCrtKeyParameters)this.key;
				BigInteger p = rsaPrivateCrtKeyParameters.P;
				BigInteger q = rsaPrivateCrtKeyParameters.Q;
				BigInteger dp = rsaPrivateCrtKeyParameters.DP;
				BigInteger dq = rsaPrivateCrtKeyParameters.DQ;
				BigInteger qinv = rsaPrivateCrtKeyParameters.QInv;
				BigInteger bigInteger = input.Remainder(p).ModPow(dp, p);
				BigInteger bigInteger2 = input.Remainder(q).ModPow(dq, q);
				return bigInteger.Subtract(bigInteger2).Multiply(qinv).Mod(p).Multiply(q).Add(bigInteger2);
			}
			return input.ModPow(this.key.Exponent, this.key.Modulus);
		}

		// Token: 0x040021D4 RID: 8660
		private RsaKeyParameters key;

		// Token: 0x040021D5 RID: 8661
		private bool forEncryption;

		// Token: 0x040021D6 RID: 8662
		private int bitSize;
	}
}
