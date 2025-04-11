using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000568 RID: 1384
	public class NaccacheSternEngine : IAsymmetricBlockCipher
	{
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x00145BC3 File Offset: 0x00143DC3
		public string AlgorithmName
		{
			get
			{
				return "NaccacheStern";
			}
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x00145BCC File Offset: 0x00143DCC
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			if (parameters is ParametersWithRandom)
			{
				parameters = ((ParametersWithRandom)parameters).Parameters;
			}
			this.key = (NaccacheSternKeyParameters)parameters;
			if (!this.forEncryption)
			{
				NaccacheSternPrivateKeyParameters naccacheSternPrivateKeyParameters = (NaccacheSternPrivateKeyParameters)this.key;
				IList smallPrimesList = naccacheSternPrivateKeyParameters.SmallPrimesList;
				this.lookup = new IList[smallPrimesList.Count];
				for (int i = 0; i < smallPrimesList.Count; i++)
				{
					BigInteger bigInteger = (BigInteger)smallPrimesList[i];
					int intValue = bigInteger.IntValue;
					this.lookup[i] = Platform.CreateArrayList(intValue);
					this.lookup[i].Add(BigInteger.One);
					BigInteger bigInteger2 = BigInteger.Zero;
					for (int j = 1; j < intValue; j++)
					{
						bigInteger2 = bigInteger2.Add(naccacheSternPrivateKeyParameters.PhiN);
						BigInteger e = bigInteger2.Divide(bigInteger);
						this.lookup[i].Add(naccacheSternPrivateKeyParameters.G.ModPow(e, naccacheSternPrivateKeyParameters.Modulus));
					}
				}
			}
		}

		// Token: 0x170006FB RID: 1787
		// (set) Token: 0x06003497 RID: 13463 RVA: 0x00002B75 File Offset: 0x00000D75
		[Obsolete("Remove: no longer used")]
		public virtual bool Debug
		{
			set
			{
			}
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x00145CD0 File Offset: 0x00143ED0
		public virtual int GetInputBlockSize()
		{
			if (this.forEncryption)
			{
				return (this.key.LowerSigmaBound + 7) / 8 - 1;
			}
			return this.key.Modulus.BitLength / 8 + 1;
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x00145D00 File Offset: 0x00143F00
		public virtual int GetOutputBlockSize()
		{
			if (this.forEncryption)
			{
				return this.key.Modulus.BitLength / 8 + 1;
			}
			return (this.key.LowerSigmaBound + 7) / 8 - 1;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x00145D30 File Offset: 0x00143F30
		public virtual byte[] ProcessBlock(byte[] inBytes, int inOff, int length)
		{
			if (this.key == null)
			{
				throw new InvalidOperationException("NaccacheStern engine not initialised");
			}
			if (length > this.GetInputBlockSize() + 1)
			{
				throw new DataLengthException("input too large for Naccache-Stern cipher.\n");
			}
			if (!this.forEncryption && length < this.GetInputBlockSize())
			{
				throw new InvalidCipherTextException("BlockLength does not match modulus for Naccache-Stern cipher.\n");
			}
			BigInteger bigInteger = new BigInteger(1, inBytes, inOff, length);
			byte[] result;
			if (this.forEncryption)
			{
				result = this.Encrypt(bigInteger);
			}
			else
			{
				IList list = Platform.CreateArrayList();
				NaccacheSternPrivateKeyParameters naccacheSternPrivateKeyParameters = (NaccacheSternPrivateKeyParameters)this.key;
				IList smallPrimesList = naccacheSternPrivateKeyParameters.SmallPrimesList;
				for (int i = 0; i < smallPrimesList.Count; i++)
				{
					BigInteger value = bigInteger.ModPow(naccacheSternPrivateKeyParameters.PhiN.Divide((BigInteger)smallPrimesList[i]), naccacheSternPrivateKeyParameters.Modulus);
					IList list2 = this.lookup[i];
					if (this.lookup[i].Count != ((BigInteger)smallPrimesList[i]).IntValue)
					{
						throw new InvalidCipherTextException(string.Concat(new object[]
						{
							"Error in lookup Array for ",
							((BigInteger)smallPrimesList[i]).IntValue,
							": Size mismatch. Expected ArrayList with length ",
							((BigInteger)smallPrimesList[i]).IntValue,
							" but found ArrayList of length ",
							this.lookup[i].Count
						}));
					}
					int num = list2.IndexOf(value);
					if (num == -1)
					{
						throw new InvalidCipherTextException("Lookup failed");
					}
					list.Add(BigInteger.ValueOf((long)num));
				}
				result = NaccacheSternEngine.chineseRemainder(list, smallPrimesList).ToByteArray();
			}
			return result;
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x00145EDC File Offset: 0x001440DC
		public virtual byte[] Encrypt(BigInteger plain)
		{
			byte[] array = new byte[this.key.Modulus.BitLength / 8 + 1];
			byte[] array2 = this.key.G.ModPow(plain, this.key.Modulus).ToByteArray();
			Array.Copy(array2, 0, array, array.Length - array2.Length, array2.Length);
			return array;
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x00145F38 File Offset: 0x00144138
		public virtual byte[] AddCryptedBlocks(byte[] block1, byte[] block2)
		{
			if (this.forEncryption)
			{
				if (block1.Length > this.GetOutputBlockSize() || block2.Length > this.GetOutputBlockSize())
				{
					throw new InvalidCipherTextException("BlockLength too large for simple addition.\n");
				}
			}
			else if (block1.Length > this.GetInputBlockSize() || block2.Length > this.GetInputBlockSize())
			{
				throw new InvalidCipherTextException("BlockLength too large for simple addition.\n");
			}
			BigInteger bigInteger = new BigInteger(1, block1);
			BigInteger val = new BigInteger(1, block2);
			BigInteger bigInteger2 = bigInteger.Multiply(val).Mod(this.key.Modulus);
			byte[] array = new byte[this.key.Modulus.BitLength / 8 + 1];
			byte[] array2 = bigInteger2.ToByteArray();
			Array.Copy(array2, 0, array, array.Length - array2.Length, array2.Length);
			return array;
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x00145FE8 File Offset: 0x001441E8
		public virtual byte[] ProcessData(byte[] data)
		{
			if (data.Length > this.GetInputBlockSize())
			{
				int inputBlockSize = this.GetInputBlockSize();
				int outputBlockSize = this.GetOutputBlockSize();
				int i = 0;
				int num = 0;
				byte[] array = new byte[(data.Length / inputBlockSize + 1) * outputBlockSize];
				while (i < data.Length)
				{
					byte[] array2;
					if (i + inputBlockSize < data.Length)
					{
						array2 = this.ProcessBlock(data, i, inputBlockSize);
						i += inputBlockSize;
					}
					else
					{
						array2 = this.ProcessBlock(data, i, data.Length - i);
						i += data.Length - i;
					}
					if (array2 == null)
					{
						throw new InvalidCipherTextException("cipher returned null");
					}
					array2.CopyTo(array, num);
					num += array2.Length;
				}
				byte[] array3 = new byte[num];
				Array.Copy(array, 0, array3, 0, num);
				return array3;
			}
			return this.ProcessBlock(data, 0, data.Length);
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x001460A4 File Offset: 0x001442A4
		private static BigInteger chineseRemainder(IList congruences, IList primes)
		{
			BigInteger bigInteger = BigInteger.Zero;
			BigInteger bigInteger2 = BigInteger.One;
			for (int i = 0; i < primes.Count; i++)
			{
				bigInteger2 = bigInteger2.Multiply((BigInteger)primes[i]);
			}
			for (int j = 0; j < primes.Count; j++)
			{
				BigInteger bigInteger3 = (BigInteger)primes[j];
				BigInteger bigInteger4 = bigInteger2.Divide(bigInteger3);
				BigInteger val = bigInteger4.ModInverse(bigInteger3);
				BigInteger bigInteger5 = bigInteger4.Multiply(val);
				bigInteger5 = bigInteger5.Multiply((BigInteger)congruences[j]);
				bigInteger = bigInteger.Add(bigInteger5);
			}
			return bigInteger.Mod(bigInteger2);
		}

		// Token: 0x0400217D RID: 8573
		private bool forEncryption;

		// Token: 0x0400217E RID: 8574
		private NaccacheSternKeyParameters key;

		// Token: 0x0400217F RID: 8575
		private IList[] lookup;
	}
}
