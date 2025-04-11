using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000489 RID: 1161
	public class HMacDsaKCalculator : IDsaKCalculator
	{
		// Token: 0x06002DF9 RID: 11769 RVA: 0x00121D8B File Offset: 0x0011FF8B
		public HMacDsaKCalculator(IDigest digest)
		{
			this.hMac = new HMac(digest);
			this.V = new byte[this.hMac.GetMacSize()];
			this.K = new byte[this.hMac.GetMacSize()];
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x0006CF70 File Offset: 0x0006B170
		public virtual bool IsDeterministic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x00121DCB File Offset: 0x0011FFCB
		public virtual void Init(BigInteger n, SecureRandom random)
		{
			throw new InvalidOperationException("Operation not supported");
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x00121DD8 File Offset: 0x0011FFD8
		public void Init(BigInteger n, BigInteger d, byte[] message)
		{
			this.n = n;
			Arrays.Fill(this.V, 1);
			Arrays.Fill(this.K, 0);
			int unsignedByteLength = BigIntegers.GetUnsignedByteLength(n);
			byte[] array = new byte[unsignedByteLength];
			byte[] array2 = BigIntegers.AsUnsignedByteArray(d);
			Array.Copy(array2, 0, array, array.Length - array2.Length, array2.Length);
			byte[] array3 = new byte[unsignedByteLength];
			BigInteger bigInteger = this.BitsToInt(message);
			if (bigInteger.CompareTo(n) >= 0)
			{
				bigInteger = bigInteger.Subtract(n);
			}
			byte[] array4 = BigIntegers.AsUnsignedByteArray(bigInteger);
			Array.Copy(array4, 0, array3, array3.Length - array4.Length, array4.Length);
			this.hMac.Init(new KeyParameter(this.K));
			this.hMac.BlockUpdate(this.V, 0, this.V.Length);
			this.hMac.Update(0);
			this.hMac.BlockUpdate(array, 0, array.Length);
			this.hMac.BlockUpdate(array3, 0, array3.Length);
			this.hMac.DoFinal(this.K, 0);
			this.hMac.Init(new KeyParameter(this.K));
			this.hMac.BlockUpdate(this.V, 0, this.V.Length);
			this.hMac.DoFinal(this.V, 0);
			this.hMac.BlockUpdate(this.V, 0, this.V.Length);
			this.hMac.Update(1);
			this.hMac.BlockUpdate(array, 0, array.Length);
			this.hMac.BlockUpdate(array3, 0, array3.Length);
			this.hMac.DoFinal(this.K, 0);
			this.hMac.Init(new KeyParameter(this.K));
			this.hMac.BlockUpdate(this.V, 0, this.V.Length);
			this.hMac.DoFinal(this.V, 0);
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x00121FB8 File Offset: 0x001201B8
		public virtual BigInteger NextK()
		{
			byte[] array = new byte[BigIntegers.GetUnsignedByteLength(this.n)];
			BigInteger bigInteger;
			for (;;)
			{
				int num;
				for (int i = 0; i < array.Length; i += num)
				{
					this.hMac.BlockUpdate(this.V, 0, this.V.Length);
					this.hMac.DoFinal(this.V, 0);
					num = Math.Min(array.Length - i, this.V.Length);
					Array.Copy(this.V, 0, array, i, num);
				}
				bigInteger = this.BitsToInt(array);
				if (bigInteger.SignValue > 0 && bigInteger.CompareTo(this.n) < 0)
				{
					break;
				}
				this.hMac.BlockUpdate(this.V, 0, this.V.Length);
				this.hMac.Update(0);
				this.hMac.DoFinal(this.K, 0);
				this.hMac.Init(new KeyParameter(this.K));
				this.hMac.BlockUpdate(this.V, 0, this.V.Length);
				this.hMac.DoFinal(this.V, 0);
			}
			return bigInteger;
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x001220D8 File Offset: 0x001202D8
		private BigInteger BitsToInt(byte[] t)
		{
			BigInteger bigInteger = new BigInteger(1, t);
			if (t.Length * 8 > this.n.BitLength)
			{
				bigInteger = bigInteger.ShiftRight(t.Length * 8 - this.n.BitLength);
			}
			return bigInteger;
		}

		// Token: 0x04001DB4 RID: 7604
		private readonly HMac hMac;

		// Token: 0x04001DB5 RID: 7605
		private readonly byte[] K;

		// Token: 0x04001DB6 RID: 7606
		private readonly byte[] V;

		// Token: 0x04001DB7 RID: 7607
		private BigInteger n;
	}
}
