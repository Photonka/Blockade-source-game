using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x0200066C RID: 1644
	public abstract class X9IntegerConverter
	{
		// Token: 0x06003D5B RID: 15707 RVA: 0x0017676A File Offset: 0x0017496A
		public static int GetByteLength(ECFieldElement fe)
		{
			return (fe.FieldSize + 7) / 8;
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x00176776 File Offset: 0x00174976
		public static int GetByteLength(ECCurve c)
		{
			return (c.FieldSize + 7) / 8;
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x00176784 File Offset: 0x00174984
		public static byte[] IntegerToBytes(BigInteger s, int qLength)
		{
			byte[] array = s.ToByteArrayUnsigned();
			if (qLength < array.Length)
			{
				byte[] array2 = new byte[qLength];
				Array.Copy(array, array.Length - array2.Length, array2, 0, array2.Length);
				return array2;
			}
			if (qLength > array.Length)
			{
				byte[] array3 = new byte[qLength];
				Array.Copy(array, 0, array3, array3.Length - array.Length, array.Length);
				return array3;
			}
			return array;
		}
	}
}
