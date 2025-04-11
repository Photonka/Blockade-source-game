using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Modes.Gcm
{
	// Token: 0x02000518 RID: 1304
	public sealed class Tables8kGcmMultiplier : IGcmMultiplier
	{
		// Token: 0x060031C5 RID: 12741 RVA: 0x001302AC File Offset: 0x0012E4AC
		public void Init(byte[] H)
		{
			if (this.M == null)
			{
				this.M = new uint[32][][];
			}
			else if (Arrays.AreEqual(this.H, H))
			{
				return;
			}
			this.H = Arrays.Clone(H);
			this.M[0] = new uint[16][];
			this.M[1] = new uint[16][];
			this.M[0][0] = new uint[4];
			this.M[1][0] = new uint[4];
			this.M[1][8] = GcmUtilities.AsUints(H);
			for (int i = 4; i >= 1; i >>= 1)
			{
				uint[] array = (uint[])this.M[1][i + i].Clone();
				GcmUtilities.MultiplyP(array);
				this.M[1][i] = array;
			}
			uint[] array2 = (uint[])this.M[1][1].Clone();
			GcmUtilities.MultiplyP(array2);
			this.M[0][8] = array2;
			for (int j = 4; j >= 1; j >>= 1)
			{
				uint[] array3 = (uint[])this.M[0][j + j].Clone();
				GcmUtilities.MultiplyP(array3);
				this.M[0][j] = array3;
			}
			int num = 0;
			for (;;)
			{
				for (int k = 2; k < 16; k += k)
				{
					for (int l = 1; l < k; l++)
					{
						uint[] array4 = (uint[])this.M[num][k].Clone();
						GcmUtilities.Xor(array4, this.M[num][l]);
						this.M[num][k + l] = array4;
					}
				}
				if (++num == 32)
				{
					break;
				}
				if (num > 1)
				{
					this.M[num] = new uint[16][];
					this.M[num][0] = new uint[4];
					for (int m = 8; m > 0; m >>= 1)
					{
						uint[] array5 = (uint[])this.M[num - 2][m].Clone();
						GcmUtilities.MultiplyP8(array5);
						this.M[num][m] = array5;
					}
				}
			}
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x001304A4 File Offset: 0x0012E6A4
		public void MultiplyH(byte[] x)
		{
			Array.Clear(this.z, 0, this.z.Length);
			for (int i = 15; i >= 0; i--)
			{
				uint[] array = this.M[i + i][(int)(x[i] & 15)];
				this.z[0] ^= array[0];
				this.z[1] ^= array[1];
				this.z[2] ^= array[2];
				this.z[3] ^= array[3];
				array = this.M[i + i + 1][(x[i] & 240) >> 4];
				this.z[0] ^= array[0];
				this.z[1] ^= array[1];
				this.z[2] ^= array[2];
				this.z[3] ^= array[3];
			}
			Pack.UInt32_To_BE(this.z, x, 0);
		}

		// Token: 0x04001FB4 RID: 8116
		private byte[] H;

		// Token: 0x04001FB5 RID: 8117
		private uint[][][] M;

		// Token: 0x04001FB6 RID: 8118
		private uint[] z = new uint[4];
	}
}
