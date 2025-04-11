using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC
{
	// Token: 0x02000313 RID: 787
	public abstract class ECPointBase : ECPoint
	{
		// Token: 0x06001E8F RID: 7823 RVA: 0x000E5B56 File Offset: 0x000E3D56
		protected internal ECPointBase(ECCurve curve, ECFieldElement x, ECFieldElement y, bool withCompression) : base(curve, x, y, withCompression)
		{
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x000E5B63 File Offset: 0x000E3D63
		protected internal ECPointBase(ECCurve curve, ECFieldElement x, ECFieldElement y, ECFieldElement[] zs, bool withCompression) : base(curve, x, y, zs, withCompression)
		{
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x000E5B74 File Offset: 0x000E3D74
		public override byte[] GetEncoded(bool compressed)
		{
			if (base.IsInfinity)
			{
				return new byte[1];
			}
			ECPoint ecpoint = this.Normalize();
			byte[] encoded = ecpoint.XCoord.GetEncoded();
			if (compressed)
			{
				byte[] array = new byte[encoded.Length + 1];
				array[0] = (ecpoint.CompressionYTilde ? 3 : 2);
				Array.Copy(encoded, 0, array, 1, encoded.Length);
				return array;
			}
			byte[] encoded2 = ecpoint.YCoord.GetEncoded();
			byte[] array2 = new byte[encoded.Length + encoded2.Length + 1];
			array2[0] = 4;
			Array.Copy(encoded, 0, array2, 1, encoded.Length);
			Array.Copy(encoded2, 0, array2, encoded.Length + 1, encoded2.Length);
			return array2;
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x000E5C10 File Offset: 0x000E3E10
		public override ECPoint Multiply(BigInteger k)
		{
			return this.Curve.GetMultiplier().Multiply(this, k);
		}
	}
}
