using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200055A RID: 1370
	public class ChaCha7539Engine : Salsa20Engine
	{
		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060033F4 RID: 13300 RVA: 0x00140AF5 File Offset: 0x0013ECF5
		public override string AlgorithmName
		{
			get
			{
				return "ChaCha7539" + this.rounds;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060033F5 RID: 13301 RVA: 0x00106579 File Offset: 0x00104779
		protected override int NonceSize
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x00140B0C File Offset: 0x0013ED0C
		protected override void AdvanceCounter()
		{
			uint[] engineState = this.engineState;
			int num = 12;
			uint num2 = engineState[num] + 1U;
			engineState[num] = num2;
			if (num2 == 0U)
			{
				throw new InvalidOperationException("attempt to increase counter past 2^32.");
			}
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x00140B3B File Offset: 0x0013ED3B
		protected override void ResetCounter()
		{
			this.engineState[12] = 0U;
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x00140B48 File Offset: 0x0013ED48
		protected override void SetKey(byte[] keyBytes, byte[] ivBytes)
		{
			if (keyBytes != null)
			{
				if (keyBytes.Length != 32)
				{
					throw new ArgumentException(this.AlgorithmName + " requires 256 bit key");
				}
				base.PackTauOrSigma(keyBytes.Length, this.engineState, 0);
				Pack.LE_To_UInt32(keyBytes, 0, this.engineState, 4, 8);
			}
			Pack.LE_To_UInt32(ivBytes, 0, this.engineState, 13, 3);
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x00140BA4 File Offset: 0x0013EDA4
		protected override void GenerateKeyStream(byte[] output)
		{
			ChaChaEngine.ChachaCore(this.rounds, this.engineState, this.x);
			Pack.UInt32_To_LE(this.x, output, 0);
		}
	}
}
