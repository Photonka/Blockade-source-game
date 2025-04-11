using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000569 RID: 1385
	public class NoekeonEngine : IBlockCipher
	{
		// Token: 0x060034A0 RID: 13472 RVA: 0x00146140 File Offset: 0x00144340
		public NoekeonEngine()
		{
			this._initialised = false;
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x00146173 File Offset: 0x00144373
		public virtual string AlgorithmName
		{
			get
			{
				return "Noekeon";
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060034A2 RID: 13474 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060034A3 RID: 13475 RVA: 0x0012C4C1 File Offset: 0x0012A6C1
		public virtual int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x060034A4 RID: 13476 RVA: 0x0014617C File Offset: 0x0014437C
		public virtual void Init(bool forEncryption, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("Invalid parameters passed to Noekeon init - " + Platform.GetTypeName(parameters), "parameters");
			}
			this._forEncryption = forEncryption;
			this._initialised = true;
			KeyParameter keyParameter = (KeyParameter)parameters;
			this.setKey(keyParameter.GetKey());
		}

		// Token: 0x060034A5 RID: 13477 RVA: 0x001461D0 File Offset: 0x001443D0
		public virtual int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (!this._initialised)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			Check.DataLength(input, inOff, 16, "input buffer too short");
			Check.OutputLength(output, outOff, 16, "output buffer too short");
			if (!this._forEncryption)
			{
				return this.decryptBlock(input, inOff, output, outOff);
			}
			return this.encryptBlock(input, inOff, output, outOff);
		}

		// Token: 0x060034A6 RID: 13478 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x060034A7 RID: 13479 RVA: 0x00146237 File Offset: 0x00144437
		private void setKey(byte[] key)
		{
			this.subKeys[0] = Pack.BE_To_UInt32(key, 0);
			this.subKeys[1] = Pack.BE_To_UInt32(key, 4);
			this.subKeys[2] = Pack.BE_To_UInt32(key, 8);
			this.subKeys[3] = Pack.BE_To_UInt32(key, 12);
		}

		// Token: 0x060034A8 RID: 13480 RVA: 0x00146278 File Offset: 0x00144478
		private int encryptBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.state[0] = Pack.BE_To_UInt32(input, inOff);
			this.state[1] = Pack.BE_To_UInt32(input, inOff + 4);
			this.state[2] = Pack.BE_To_UInt32(input, inOff + 8);
			this.state[3] = Pack.BE_To_UInt32(input, inOff + 12);
			int i;
			for (i = 0; i < 16; i++)
			{
				this.state[0] ^= NoekeonEngine.roundConstants[i];
				this.theta(this.state, this.subKeys);
				this.pi1(this.state);
				this.gamma(this.state);
				this.pi2(this.state);
			}
			this.state[0] ^= NoekeonEngine.roundConstants[i];
			this.theta(this.state, this.subKeys);
			Pack.UInt32_To_BE(this.state[0], output, outOff);
			Pack.UInt32_To_BE(this.state[1], output, outOff + 4);
			Pack.UInt32_To_BE(this.state[2], output, outOff + 8);
			Pack.UInt32_To_BE(this.state[3], output, outOff + 12);
			return 16;
		}

		// Token: 0x060034A9 RID: 13481 RVA: 0x00146394 File Offset: 0x00144594
		private int decryptBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			this.state[0] = Pack.BE_To_UInt32(input, inOff);
			this.state[1] = Pack.BE_To_UInt32(input, inOff + 4);
			this.state[2] = Pack.BE_To_UInt32(input, inOff + 8);
			this.state[3] = Pack.BE_To_UInt32(input, inOff + 12);
			Array.Copy(this.subKeys, 0, this.decryptKeys, 0, this.subKeys.Length);
			this.theta(this.decryptKeys, NoekeonEngine.nullVector);
			int i;
			for (i = 16; i > 0; i--)
			{
				this.theta(this.state, this.decryptKeys);
				this.state[0] ^= NoekeonEngine.roundConstants[i];
				this.pi1(this.state);
				this.gamma(this.state);
				this.pi2(this.state);
			}
			this.theta(this.state, this.decryptKeys);
			this.state[0] ^= NoekeonEngine.roundConstants[i];
			Pack.UInt32_To_BE(this.state[0], output, outOff);
			Pack.UInt32_To_BE(this.state[1], output, outOff + 4);
			Pack.UInt32_To_BE(this.state[2], output, outOff + 8);
			Pack.UInt32_To_BE(this.state[3], output, outOff + 12);
			return 16;
		}

		// Token: 0x060034AA RID: 13482 RVA: 0x001464DC File Offset: 0x001446DC
		private void gamma(uint[] a)
		{
			a[1] ^= (~a[3] & ~a[2]);
			a[0] ^= (a[2] & a[1]);
			uint num = a[3];
			a[3] = a[0];
			a[0] = num;
			a[2] ^= (a[0] ^ a[1] ^ a[3]);
			a[1] ^= (~a[3] & ~a[2]);
			a[0] ^= (a[2] & a[1]);
		}

		// Token: 0x060034AB RID: 13483 RVA: 0x0014655C File Offset: 0x0014475C
		private void theta(uint[] a, uint[] k)
		{
			uint num = a[0] ^ a[2];
			num ^= (this.rotl(num, 8) ^ this.rotl(num, 24));
			a[1] ^= num;
			a[3] ^= num;
			for (int i = 0; i < 4; i++)
			{
				a[i] ^= k[i];
			}
			num = (a[1] ^ a[3]);
			num ^= (this.rotl(num, 8) ^ this.rotl(num, 24));
			a[0] ^= num;
			a[2] ^= num;
		}

		// Token: 0x060034AC RID: 13484 RVA: 0x001465ED File Offset: 0x001447ED
		private void pi1(uint[] a)
		{
			a[1] = this.rotl(a[1], 1);
			a[2] = this.rotl(a[2], 5);
			a[3] = this.rotl(a[3], 2);
		}

		// Token: 0x060034AD RID: 13485 RVA: 0x00146616 File Offset: 0x00144816
		private void pi2(uint[] a)
		{
			a[1] = this.rotl(a[1], 31);
			a[2] = this.rotl(a[2], 27);
			a[3] = this.rotl(a[3], 30);
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x00146642 File Offset: 0x00144842
		private uint rotl(uint x, int y)
		{
			return x << y | x >> 32 - y;
		}

		// Token: 0x04002180 RID: 8576
		private const int GenericSize = 16;

		// Token: 0x04002181 RID: 8577
		private static readonly uint[] nullVector = new uint[4];

		// Token: 0x04002182 RID: 8578
		private static readonly uint[] roundConstants = new uint[]
		{
			128U,
			27U,
			54U,
			108U,
			216U,
			171U,
			77U,
			154U,
			47U,
			94U,
			188U,
			99U,
			198U,
			151U,
			53U,
			106U,
			212U
		};

		// Token: 0x04002183 RID: 8579
		private uint[] state = new uint[4];

		// Token: 0x04002184 RID: 8580
		private uint[] subKeys = new uint[4];

		// Token: 0x04002185 RID: 8581
		private uint[] decryptKeys = new uint[4];

		// Token: 0x04002186 RID: 8582
		private bool _initialised;

		// Token: 0x04002187 RID: 8583
		private bool _forEncryption;
	}
}
