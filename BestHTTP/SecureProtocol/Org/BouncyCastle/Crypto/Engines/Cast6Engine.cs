using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x02000559 RID: 1369
	public sealed class Cast6Engine : Cast5Engine
	{
		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x001402C5 File Offset: 0x0013E4C5
		public override string AlgorithmName
		{
			get
			{
				return "CAST6";
			}
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x00002B75 File Offset: 0x00000D75
		public override void Reset()
		{
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x0012C4C1 File Offset: 0x0012A6C1
		public override int GetBlockSize()
		{
			return 16;
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x001402CC File Offset: 0x0013E4CC
		internal override void SetKey(byte[] key)
		{
			uint num = 1518500249U;
			uint num2 = 1859775393U;
			int num3 = 19;
			int num4 = 17;
			for (int i = 0; i < 24; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					this._Tm[i * 8 + j] = num;
					num += num2;
					this._Tr[i * 8 + j] = num3;
					num3 = (num3 + num4 & 31);
				}
			}
			byte[] array = new byte[64];
			key.CopyTo(array, 0);
			for (int k = 0; k < 8; k++)
			{
				this._workingKey[k] = Pack.BE_To_UInt32(array, k * 4);
			}
			for (int l = 0; l < 12; l++)
			{
				int num5 = l * 2 * 8;
				this._workingKey[6] ^= Cast5Engine.F1(this._workingKey[7], this._Tm[num5], this._Tr[num5]);
				this._workingKey[5] ^= Cast5Engine.F2(this._workingKey[6], this._Tm[num5 + 1], this._Tr[num5 + 1]);
				this._workingKey[4] ^= Cast5Engine.F3(this._workingKey[5], this._Tm[num5 + 2], this._Tr[num5 + 2]);
				this._workingKey[3] ^= Cast5Engine.F1(this._workingKey[4], this._Tm[num5 + 3], this._Tr[num5 + 3]);
				this._workingKey[2] ^= Cast5Engine.F2(this._workingKey[3], this._Tm[num5 + 4], this._Tr[num5 + 4]);
				this._workingKey[1] ^= Cast5Engine.F3(this._workingKey[2], this._Tm[num5 + 5], this._Tr[num5 + 5]);
				this._workingKey[0] ^= Cast5Engine.F1(this._workingKey[1], this._Tm[num5 + 6], this._Tr[num5 + 6]);
				this._workingKey[7] ^= Cast5Engine.F2(this._workingKey[0], this._Tm[num5 + 7], this._Tr[num5 + 7]);
				num5 = (l * 2 + 1) * 8;
				this._workingKey[6] ^= Cast5Engine.F1(this._workingKey[7], this._Tm[num5], this._Tr[num5]);
				this._workingKey[5] ^= Cast5Engine.F2(this._workingKey[6], this._Tm[num5 + 1], this._Tr[num5 + 1]);
				this._workingKey[4] ^= Cast5Engine.F3(this._workingKey[5], this._Tm[num5 + 2], this._Tr[num5 + 2]);
				this._workingKey[3] ^= Cast5Engine.F1(this._workingKey[4], this._Tm[num5 + 3], this._Tr[num5 + 3]);
				this._workingKey[2] ^= Cast5Engine.F2(this._workingKey[3], this._Tm[num5 + 4], this._Tr[num5 + 4]);
				this._workingKey[1] ^= Cast5Engine.F3(this._workingKey[2], this._Tm[num5 + 5], this._Tr[num5 + 5]);
				this._workingKey[0] ^= Cast5Engine.F1(this._workingKey[1], this._Tm[num5 + 6], this._Tr[num5 + 6]);
				this._workingKey[7] ^= Cast5Engine.F2(this._workingKey[0], this._Tm[num5 + 7], this._Tr[num5 + 7]);
				this._Kr[l * 4] = (int)(this._workingKey[0] & 31U);
				this._Kr[l * 4 + 1] = (int)(this._workingKey[2] & 31U);
				this._Kr[l * 4 + 2] = (int)(this._workingKey[4] & 31U);
				this._Kr[l * 4 + 3] = (int)(this._workingKey[6] & 31U);
				this._Km[l * 4] = this._workingKey[7];
				this._Km[l * 4 + 1] = this._workingKey[5];
				this._Km[l * 4 + 2] = this._workingKey[3];
				this._Km[l * 4 + 3] = this._workingKey[1];
			}
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x0014076C File Offset: 0x0013E96C
		internal override int EncryptBlock(byte[] src, int srcIndex, byte[] dst, int dstIndex)
		{
			uint a = Pack.BE_To_UInt32(src, srcIndex);
			uint b = Pack.BE_To_UInt32(src, srcIndex + 4);
			uint c = Pack.BE_To_UInt32(src, srcIndex + 8);
			uint d = Pack.BE_To_UInt32(src, srcIndex + 12);
			uint[] array = new uint[4];
			this.CAST_Encipher(a, b, c, d, array);
			Pack.UInt32_To_BE(array[0], dst, dstIndex);
			Pack.UInt32_To_BE(array[1], dst, dstIndex + 4);
			Pack.UInt32_To_BE(array[2], dst, dstIndex + 8);
			Pack.UInt32_To_BE(array[3], dst, dstIndex + 12);
			return 16;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x001407F0 File Offset: 0x0013E9F0
		internal override int DecryptBlock(byte[] src, int srcIndex, byte[] dst, int dstIndex)
		{
			uint a = Pack.BE_To_UInt32(src, srcIndex);
			uint b = Pack.BE_To_UInt32(src, srcIndex + 4);
			uint c = Pack.BE_To_UInt32(src, srcIndex + 8);
			uint d = Pack.BE_To_UInt32(src, srcIndex + 12);
			uint[] array = new uint[4];
			this.CAST_Decipher(a, b, c, d, array);
			Pack.UInt32_To_BE(array[0], dst, dstIndex);
			Pack.UInt32_To_BE(array[1], dst, dstIndex + 4);
			Pack.UInt32_To_BE(array[2], dst, dstIndex + 8);
			Pack.UInt32_To_BE(array[3], dst, dstIndex + 12);
			return 16;
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x00140874 File Offset: 0x0013EA74
		private void CAST_Encipher(uint A, uint B, uint C, uint D, uint[] result)
		{
			for (int i = 0; i < 6; i++)
			{
				int num = i * 4;
				C ^= Cast5Engine.F1(D, this._Km[num], this._Kr[num]);
				B ^= Cast5Engine.F2(C, this._Km[num + 1], this._Kr[num + 1]);
				A ^= Cast5Engine.F3(B, this._Km[num + 2], this._Kr[num + 2]);
				D ^= Cast5Engine.F1(A, this._Km[num + 3], this._Kr[num + 3]);
			}
			for (int j = 6; j < 12; j++)
			{
				int num2 = j * 4;
				D ^= Cast5Engine.F1(A, this._Km[num2 + 3], this._Kr[num2 + 3]);
				A ^= Cast5Engine.F3(B, this._Km[num2 + 2], this._Kr[num2 + 2]);
				B ^= Cast5Engine.F2(C, this._Km[num2 + 1], this._Kr[num2 + 1]);
				C ^= Cast5Engine.F1(D, this._Km[num2], this._Kr[num2]);
			}
			result[0] = A;
			result[1] = B;
			result[2] = C;
			result[3] = D;
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x001409AC File Offset: 0x0013EBAC
		private void CAST_Decipher(uint A, uint B, uint C, uint D, uint[] result)
		{
			for (int i = 0; i < 6; i++)
			{
				int num = (11 - i) * 4;
				C ^= Cast5Engine.F1(D, this._Km[num], this._Kr[num]);
				B ^= Cast5Engine.F2(C, this._Km[num + 1], this._Kr[num + 1]);
				A ^= Cast5Engine.F3(B, this._Km[num + 2], this._Kr[num + 2]);
				D ^= Cast5Engine.F1(A, this._Km[num + 3], this._Kr[num + 3]);
			}
			for (int j = 6; j < 12; j++)
			{
				int num2 = (11 - j) * 4;
				D ^= Cast5Engine.F1(A, this._Km[num2 + 3], this._Kr[num2 + 3]);
				A ^= Cast5Engine.F3(B, this._Km[num2 + 2], this._Kr[num2 + 2]);
				B ^= Cast5Engine.F2(C, this._Km[num2 + 1], this._Kr[num2 + 1]);
				C ^= Cast5Engine.F1(D, this._Km[num2], this._Kr[num2]);
			}
			result[0] = A;
			result[1] = B;
			result[2] = C;
			result[3] = D;
		}

		// Token: 0x0400210A RID: 8458
		private const int ROUNDS = 12;

		// Token: 0x0400210B RID: 8459
		private const int BLOCK_SIZE = 16;

		// Token: 0x0400210C RID: 8460
		private int[] _Kr = new int[48];

		// Token: 0x0400210D RID: 8461
		private uint[] _Km = new uint[48];

		// Token: 0x0400210E RID: 8462
		private int[] _Tr = new int[192];

		// Token: 0x0400210F RID: 8463
		private uint[] _Tm = new uint[192];

		// Token: 0x04002110 RID: 8464
		private uint[] _workingKey = new uint[8];
	}
}
