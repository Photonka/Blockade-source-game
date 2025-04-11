using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200068E RID: 1678
	public class KeyUsage : DerBitString
	{
		// Token: 0x06003E69 RID: 15977 RVA: 0x0017A1E8 File Offset: 0x001783E8
		public new static KeyUsage GetInstance(object obj)
		{
			if (obj is KeyUsage)
			{
				return (KeyUsage)obj;
			}
			if (obj is X509Extension)
			{
				return KeyUsage.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			return new KeyUsage(DerBitString.GetInstance(obj));
		}

		// Token: 0x06003E6A RID: 15978 RVA: 0x0017105D File Offset: 0x0016F25D
		public KeyUsage(int usage) : base(usage)
		{
		}

		// Token: 0x06003E6B RID: 15979 RVA: 0x0017A21D File Offset: 0x0017841D
		private KeyUsage(DerBitString usage) : base(usage.GetBytes(), usage.PadBits)
		{
		}

		// Token: 0x06003E6C RID: 15980 RVA: 0x0017A234 File Offset: 0x00178434
		public override string ToString()
		{
			byte[] bytes = this.GetBytes();
			if (bytes.Length == 1)
			{
				return "KeyUsage: 0x" + ((int)(bytes[0] & byte.MaxValue)).ToString("X");
			}
			return "KeyUsage: 0x" + ((int)(bytes[1] & byte.MaxValue) << 8 | (int)(bytes[0] & byte.MaxValue)).ToString("X");
		}

		// Token: 0x040026AC RID: 9900
		public const int DigitalSignature = 128;

		// Token: 0x040026AD RID: 9901
		public const int NonRepudiation = 64;

		// Token: 0x040026AE RID: 9902
		public const int KeyEncipherment = 32;

		// Token: 0x040026AF RID: 9903
		public const int DataEncipherment = 16;

		// Token: 0x040026B0 RID: 9904
		public const int KeyAgreement = 8;

		// Token: 0x040026B1 RID: 9905
		public const int KeyCertSign = 4;

		// Token: 0x040026B2 RID: 9906
		public const int CrlSign = 2;

		// Token: 0x040026B3 RID: 9907
		public const int EncipherOnly = 1;

		// Token: 0x040026B4 RID: 9908
		public const int DecipherOnly = 32768;
	}
}
