using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000586 RID: 1414
	public class XSalsa20Engine : Salsa20Engine
	{
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x0015271A File Offset: 0x0015091A
		public override string AlgorithmName
		{
			get
			{
				return "XSalsa20";
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060035F8 RID: 13816 RVA: 0x00152721 File Offset: 0x00150921
		protected override int NonceSize
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x00152728 File Offset: 0x00150928
		protected override void SetKey(byte[] keyBytes, byte[] ivBytes)
		{
			if (keyBytes == null)
			{
				throw new ArgumentException(this.AlgorithmName + " doesn't support re-init with null key");
			}
			if (keyBytes.Length != 32)
			{
				throw new ArgumentException(this.AlgorithmName + " requires a 256 bit key");
			}
			base.SetKey(keyBytes, ivBytes);
			Pack.LE_To_UInt32(ivBytes, 8, this.engineState, 8, 2);
			uint[] array = new uint[this.engineState.Length];
			Salsa20Engine.SalsaCore(20, this.engineState, array);
			this.engineState[1] = array[0] - this.engineState[0];
			this.engineState[2] = array[5] - this.engineState[5];
			this.engineState[3] = array[10] - this.engineState[10];
			this.engineState[4] = array[15] - this.engineState[15];
			this.engineState[11] = array[6] - this.engineState[6];
			this.engineState[12] = array[7] - this.engineState[7];
			this.engineState[13] = array[8] - this.engineState[8];
			this.engineState[14] = array[9] - this.engineState[9];
			Pack.LE_To_UInt32(ivBytes, 16, this.engineState, 6, 2);
		}
	}
}
