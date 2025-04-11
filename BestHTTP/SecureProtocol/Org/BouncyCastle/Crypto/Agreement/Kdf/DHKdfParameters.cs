using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Kdf
{
	// Token: 0x020005BE RID: 1470
	public class DHKdfParameters : IDerivationParameters
	{
		// Token: 0x060038B9 RID: 14521 RVA: 0x00167455 File Offset: 0x00165655
		public DHKdfParameters(DerObjectIdentifier algorithm, int keySize, byte[] z) : this(algorithm, keySize, z, null)
		{
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x00167461 File Offset: 0x00165661
		public DHKdfParameters(DerObjectIdentifier algorithm, int keySize, byte[] z, byte[] extraInfo)
		{
			this.algorithm = algorithm;
			this.keySize = keySize;
			this.z = z;
			this.extraInfo = extraInfo;
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060038BB RID: 14523 RVA: 0x00167486 File Offset: 0x00165686
		public DerObjectIdentifier Algorithm
		{
			get
			{
				return this.algorithm;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x0016748E File Offset: 0x0016568E
		public int KeySize
		{
			get
			{
				return this.keySize;
			}
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x00167496 File Offset: 0x00165696
		public byte[] GetZ()
		{
			return this.z;
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x0016749E File Offset: 0x0016569E
		public byte[] GetExtraInfo()
		{
			return this.extraInfo;
		}

		// Token: 0x0400243F RID: 9279
		private readonly DerObjectIdentifier algorithm;

		// Token: 0x04002440 RID: 9280
		private readonly int keySize;

		// Token: 0x04002441 RID: 9281
		private readonly byte[] z;

		// Token: 0x04002442 RID: 9282
		private readonly byte[] extraInfo;
	}
}
