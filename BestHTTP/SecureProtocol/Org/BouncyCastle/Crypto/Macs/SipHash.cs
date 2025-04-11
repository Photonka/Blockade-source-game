using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs
{
	// Token: 0x02000524 RID: 1316
	public class SipHash : IMac
	{
		// Token: 0x0600323D RID: 12861 RVA: 0x00132EC2 File Offset: 0x001310C2
		public SipHash() : this(2, 4)
		{
		}

		// Token: 0x0600323E RID: 12862 RVA: 0x00132ECC File Offset: 0x001310CC
		public SipHash(int c, int d)
		{
			this.c = c;
			this.d = d;
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x00132EE2 File Offset: 0x001310E2
		public virtual string AlgorithmName
		{
			get
			{
				return string.Concat(new object[]
				{
					"SipHash-",
					this.c,
					"-",
					this.d
				});
			}
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x000FE681 File Offset: 0x000FC881
		public virtual int GetMacSize()
		{
			return 8;
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x00132F1C File Offset: 0x0013111C
		public virtual void Init(ICipherParameters parameters)
		{
			KeyParameter keyParameter = parameters as KeyParameter;
			if (keyParameter == null)
			{
				throw new ArgumentException("must be an instance of KeyParameter", "parameters");
			}
			byte[] key = keyParameter.GetKey();
			if (key.Length != 16)
			{
				throw new ArgumentException("must be a 128-bit key", "parameters");
			}
			this.k0 = (long)Pack.LE_To_UInt64(key, 0);
			this.k1 = (long)Pack.LE_To_UInt64(key, 8);
			this.Reset();
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x00132F80 File Offset: 0x00131180
		public virtual void Update(byte input)
		{
			this.m = (long)((ulong)this.m >> 8 | (ulong)input << 56);
			int num = this.wordPos + 1;
			this.wordPos = num;
			if (num == 8)
			{
				this.ProcessMessageWord();
				this.wordPos = 0;
			}
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x00132FC4 File Offset: 0x001311C4
		public virtual void BlockUpdate(byte[] input, int offset, int length)
		{
			int i = 0;
			int num = length & -8;
			if (this.wordPos == 0)
			{
				while (i < num)
				{
					this.m = (long)Pack.LE_To_UInt64(input, offset + i);
					this.ProcessMessageWord();
					i += 8;
				}
				while (i < length)
				{
					this.m = (long)((ulong)this.m >> 8 | (ulong)input[offset + i] << 56);
					i++;
				}
				this.wordPos = length - num;
				return;
			}
			int num2 = this.wordPos << 3;
			while (i < num)
			{
				ulong num3 = Pack.LE_To_UInt64(input, offset + i);
				this.m = (long)(num3 << num2 | (ulong)this.m >> -num2);
				this.ProcessMessageWord();
				this.m = (long)num3;
				i += 8;
			}
			while (i < length)
			{
				this.m = (long)((ulong)this.m >> 8 | (ulong)input[offset + i] << 56);
				int num4 = this.wordPos + 1;
				this.wordPos = num4;
				if (num4 == 8)
				{
					this.ProcessMessageWord();
					this.wordPos = 0;
				}
				i++;
			}
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x001330B4 File Offset: 0x001312B4
		public virtual long DoFinal()
		{
			this.m = (long)((ulong)this.m >> (7 - this.wordPos << 3));
			this.m = (long)((ulong)this.m >> 8);
			this.m |= (long)((this.wordCount << 3) + this.wordPos) << 56;
			this.ProcessMessageWord();
			this.v2 ^= 255L;
			this.ApplySipRounds(this.d);
			long result = this.v0 ^ this.v1 ^ this.v2 ^ this.v3;
			this.Reset();
			return result;
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x0013314F File Offset: 0x0013134F
		public virtual int DoFinal(byte[] output, int outOff)
		{
			Pack.UInt64_To_LE((ulong)this.DoFinal(), output, outOff);
			return 8;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x00133160 File Offset: 0x00131360
		public virtual void Reset()
		{
			this.v0 = (this.k0 ^ 8317987319222330741L);
			this.v1 = (this.k1 ^ 7237128888997146477L);
			this.v2 = (this.k0 ^ 7816392313619706465L);
			this.v3 = (this.k1 ^ 8387220255154660723L);
			this.m = 0L;
			this.wordPos = 0;
			this.wordCount = 0;
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x001331DC File Offset: 0x001313DC
		protected virtual void ProcessMessageWord()
		{
			this.wordCount++;
			this.v3 ^= this.m;
			this.ApplySipRounds(this.c);
			this.v0 ^= this.m;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x0013322C File Offset: 0x0013142C
		protected virtual void ApplySipRounds(int n)
		{
			long num = this.v0;
			long num2 = this.v1;
			long num3 = this.v2;
			long num4 = this.v3;
			for (int i = 0; i < n; i++)
			{
				num += num2;
				num3 += num4;
				num2 = SipHash.RotateLeft(num2, 13);
				num4 = SipHash.RotateLeft(num4, 16);
				num2 ^= num;
				num4 ^= num3;
				num = SipHash.RotateLeft(num, 32);
				num3 += num2;
				num += num4;
				num2 = SipHash.RotateLeft(num2, 17);
				num4 = SipHash.RotateLeft(num4, 21);
				num2 ^= num3;
				num4 ^= num;
				num3 = SipHash.RotateLeft(num3, 32);
			}
			this.v0 = num;
			this.v1 = num2;
			this.v2 = num3;
			this.v3 = num4;
		}

		// Token: 0x06003249 RID: 12873 RVA: 0x001332D8 File Offset: 0x001314D8
		protected static long RotateLeft(long x, int n)
		{
			return x << n | (long)((ulong)x >> -n);
		}

		// Token: 0x04002012 RID: 8210
		protected readonly int c;

		// Token: 0x04002013 RID: 8211
		protected readonly int d;

		// Token: 0x04002014 RID: 8212
		protected long k0;

		// Token: 0x04002015 RID: 8213
		protected long k1;

		// Token: 0x04002016 RID: 8214
		protected long v0;

		// Token: 0x04002017 RID: 8215
		protected long v1;

		// Token: 0x04002018 RID: 8216
		protected long v2;

		// Token: 0x04002019 RID: 8217
		protected long v3;

		// Token: 0x0400201A RID: 8218
		protected long m;

		// Token: 0x0400201B RID: 8219
		protected int wordPos;

		// Token: 0x0400201C RID: 8220
		protected int wordCount;
	}
}
