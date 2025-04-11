using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x0200048F RID: 1167
	public class PlainDsaEncoding : IDsaEncoding
	{
		// Token: 0x06002E2A RID: 11818 RVA: 0x001235F0 File Offset: 0x001217F0
		public virtual BigInteger[] Decode(BigInteger n, byte[] encoding)
		{
			int unsignedByteLength = BigIntegers.GetUnsignedByteLength(n);
			if (encoding.Length != unsignedByteLength * 2)
			{
				throw new ArgumentException("Encoding has incorrect length", "encoding");
			}
			return new BigInteger[]
			{
				this.DecodeValue(n, encoding, 0, unsignedByteLength),
				this.DecodeValue(n, encoding, unsignedByteLength, unsignedByteLength)
			};
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x0012363C File Offset: 0x0012183C
		public virtual byte[] Encode(BigInteger n, BigInteger r, BigInteger s)
		{
			int unsignedByteLength = BigIntegers.GetUnsignedByteLength(n);
			byte[] array = new byte[unsignedByteLength * 2];
			this.EncodeValue(n, r, array, 0, unsignedByteLength);
			this.EncodeValue(n, s, array, unsignedByteLength, unsignedByteLength);
			return array;
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x00123670 File Offset: 0x00121870
		protected virtual BigInteger CheckValue(BigInteger n, BigInteger x)
		{
			if (x.SignValue < 0 || x.CompareTo(n) >= 0)
			{
				throw new ArgumentException("Value out of range", "x");
			}
			return x;
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x00123696 File Offset: 0x00121896
		protected virtual BigInteger DecodeValue(BigInteger n, byte[] buf, int off, int len)
		{
			return this.CheckValue(n, new BigInteger(1, buf, off, len));
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x001236AC File Offset: 0x001218AC
		protected virtual void EncodeValue(BigInteger n, BigInteger x, byte[] buf, int off, int len)
		{
			byte[] array = this.CheckValue(n, x).ToByteArrayUnsigned();
			int num = Math.Max(0, array.Length - len);
			int num2 = array.Length - num;
			int num3 = len - num2;
			Arrays.Fill(buf, off, off + num3, 0);
			Array.Copy(array, num, buf, off + num3, num2);
		}

		// Token: 0x04001DF0 RID: 7664
		public static readonly PlainDsaEncoding Instance = new PlainDsaEncoding();
	}
}
