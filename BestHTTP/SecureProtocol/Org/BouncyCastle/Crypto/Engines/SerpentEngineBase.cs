using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines
{
	// Token: 0x0200057C RID: 1404
	public abstract class SerpentEngineBase : IBlockCipher
	{
		// Token: 0x06003570 RID: 13680 RVA: 0x0014CF58 File Offset: 0x0014B158
		public virtual void Init(bool encrypting, ICipherParameters parameters)
		{
			if (!(parameters is KeyParameter))
			{
				throw new ArgumentException("invalid parameter passed to " + this.AlgorithmName + " init - " + Platform.GetTypeName(parameters));
			}
			this.encrypting = encrypting;
			this.wKey = this.MakeWorkingKey(((KeyParameter)parameters).GetKey());
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06003571 RID: 13681 RVA: 0x0014CFAC File Offset: 0x0014B1AC
		public virtual string AlgorithmName
		{
			get
			{
				return "Serpent";
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06003572 RID: 13682 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public virtual bool IsPartialBlockOkay
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x0014CFB3 File Offset: 0x0014B1B3
		public virtual int GetBlockSize()
		{
			return SerpentEngineBase.BlockSize;
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x0014CFBC File Offset: 0x0014B1BC
		public int ProcessBlock(byte[] input, int inOff, byte[] output, int outOff)
		{
			if (this.wKey == null)
			{
				throw new InvalidOperationException(this.AlgorithmName + " not initialised");
			}
			Check.DataLength(input, inOff, SerpentEngineBase.BlockSize, "input buffer too short");
			Check.OutputLength(output, outOff, SerpentEngineBase.BlockSize, "output buffer too short");
			if (this.encrypting)
			{
				this.EncryptBlock(input, inOff, output, outOff);
			}
			else
			{
				this.DecryptBlock(input, inOff, output, outOff);
			}
			return SerpentEngineBase.BlockSize;
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x00002B75 File Offset: 0x00000D75
		public virtual void Reset()
		{
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x0014A672 File Offset: 0x00148872
		protected static int RotateLeft(int x, int bits)
		{
			return x << bits | (int)((uint)x >> 32 - bits);
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x00138A99 File Offset: 0x00136C99
		private static int RotateRight(int x, int bits)
		{
			return (int)((uint)x >> bits | (uint)((uint)x << 32 - bits));
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x0014D030 File Offset: 0x0014B230
		protected void Sb0(int a, int b, int c, int d)
		{
			int num = a ^ d;
			int num2 = c ^ num;
			int num3 = b ^ num2;
			this.X3 = ((a & d) ^ num3);
			int num4 = a ^ (b & num);
			this.X2 = (num3 ^ (c | num4));
			int num5 = this.X3 & (num2 ^ num4);
			this.X1 = (~num2 ^ num5);
			this.X0 = (num5 ^ ~num4);
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x0014D08C File Offset: 0x0014B28C
		protected void Ib0(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = d ^ (num | num2);
			int num4 = c ^ num3;
			this.X2 = (num2 ^ num4);
			int num5 = num ^ (d & num2);
			this.X1 = (num3 ^ (this.X2 & num5));
			this.X3 = ((a & num3) ^ (num4 | this.X1));
			this.X0 = (this.X3 ^ (num4 ^ num5));
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x0014D0F0 File Offset: 0x0014B2F0
		protected void Sb1(int a, int b, int c, int d)
		{
			int num = b ^ ~a;
			int num2 = c ^ (a | num);
			this.X2 = (d ^ num2);
			int num3 = b ^ (d | num);
			int num4 = num ^ this.X2;
			this.X3 = (num4 ^ (num2 & num3));
			int num5 = num2 ^ num3;
			this.X1 = (this.X3 ^ num5);
			this.X0 = (num2 ^ (num4 & num5));
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x0014D150 File Offset: 0x0014B350
		protected void Ib1(int a, int b, int c, int d)
		{
			int num = b ^ d;
			int num2 = a ^ (b & num);
			int num3 = num ^ num2;
			this.X3 = (c ^ num3);
			int num4 = b ^ (num & num2);
			int num5 = this.X3 | num4;
			this.X1 = (num2 ^ num5);
			int num6 = ~this.X1;
			int num7 = this.X3 ^ num4;
			this.X0 = (num6 ^ num7);
			this.X2 = (num3 ^ (num6 | num7));
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x0014D1BC File Offset: 0x0014B3BC
		protected void Sb2(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = b ^ d;
			int num3 = c & num;
			this.X0 = (num2 ^ num3);
			int num4 = c ^ num;
			int num5 = c ^ this.X0;
			int num6 = b & num5;
			this.X3 = (num4 ^ num6);
			this.X2 = (a ^ ((d | num6) & (this.X0 | num4)));
			this.X1 = (num2 ^ this.X3 ^ (this.X2 ^ (d | num)));
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x0014D22C File Offset: 0x0014B42C
		protected void Ib2(int a, int b, int c, int d)
		{
			int num = b ^ d;
			int num2 = ~num;
			int num3 = a ^ c;
			int num4 = c ^ num;
			int num5 = b & num4;
			this.X0 = (num3 ^ num5);
			int num6 = a | num2;
			int num7 = d ^ num6;
			int num8 = num3 | num7;
			this.X3 = (num ^ num8);
			int num9 = ~num4;
			int num10 = this.X0 | this.X3;
			this.X1 = (num9 ^ num10);
			this.X2 = ((d & num9) ^ (num3 ^ num10));
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x0014D2A4 File Offset: 0x0014B4A4
		protected void Sb3(int a, int b, int c, int d)
		{
			int num = a ^ b;
			int num2 = a & c;
			int num3 = a | d;
			int num4 = c ^ d;
			int num5 = num & num3;
			int num6 = num2 | num5;
			this.X2 = (num4 ^ num6);
			int num7 = b ^ num3;
			int num8 = num6 ^ num7;
			int num9 = num4 & num8;
			this.X0 = (num ^ num9);
			int num10 = this.X2 & this.X0;
			this.X1 = (num8 ^ num10);
			this.X3 = ((b | d) ^ (num4 ^ num10));
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x0014D31C File Offset: 0x0014B51C
		protected void Ib3(int a, int b, int c, int d)
		{
			int num = a | b;
			int num2 = b ^ c;
			int num3 = b & num2;
			int num4 = a ^ num3;
			int num5 = c ^ num4;
			int num6 = d | num4;
			this.X0 = (num2 ^ num6);
			int num7 = num2 | num6;
			int num8 = d ^ num7;
			this.X2 = (num5 ^ num8);
			int num9 = num ^ num8;
			int num10 = this.X0 & num9;
			this.X3 = (num4 ^ num10);
			this.X1 = (this.X3 ^ (this.X0 ^ num9));
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x0014D394 File Offset: 0x0014B594
		protected void Sb4(int a, int b, int c, int d)
		{
			int num = a ^ d;
			int num2 = d & num;
			int num3 = c ^ num2;
			int num4 = b | num3;
			this.X3 = (num ^ num4);
			int num5 = ~b;
			int num6 = num | num5;
			this.X0 = (num3 ^ num6);
			int num7 = a & this.X0;
			int num8 = num ^ num5;
			int num9 = num4 & num8;
			this.X2 = (num7 ^ num9);
			this.X1 = (a ^ num3 ^ (num8 & this.X2));
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x0014D404 File Offset: 0x0014B604
		protected void Ib4(int a, int b, int c, int d)
		{
			int num = c | d;
			int num2 = a & num;
			int num3 = b ^ num2;
			int num4 = a & num3;
			int num5 = c ^ num4;
			this.X1 = (d ^ num5);
			int num6 = ~a;
			int num7 = num5 & this.X1;
			this.X3 = (num3 ^ num7);
			int num8 = this.X1 | num6;
			int num9 = d ^ num8;
			this.X0 = (this.X3 ^ num9);
			this.X2 = ((num3 & num9) ^ (this.X1 ^ num6));
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x0014D480 File Offset: 0x0014B680
		protected void Sb5(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = a ^ d;
			int num4 = c ^ num;
			int num5 = num2 | num3;
			this.X0 = (num4 ^ num5);
			int num6 = d & this.X0;
			int num7 = num2 ^ this.X0;
			this.X1 = (num6 ^ num7);
			int num8 = num | this.X0;
			int num9 = num2 | num6;
			int num10 = num3 ^ num8;
			this.X2 = (num9 ^ num10);
			this.X3 = (b ^ num6 ^ (this.X1 & num10));
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x0014D500 File Offset: 0x0014B700
		protected void Ib5(int a, int b, int c, int d)
		{
			int num = ~c;
			int num2 = b & num;
			int num3 = d ^ num2;
			int num4 = a & num3;
			int num5 = b ^ num;
			this.X3 = (num4 ^ num5);
			int num6 = b | this.X3;
			int num7 = a & num6;
			this.X1 = (num3 ^ num7);
			int num8 = a | d;
			int num9 = num ^ num6;
			this.X0 = (num8 ^ num9);
			this.X2 = ((b & num8) ^ (num4 | (a ^ c)));
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x0014D570 File Offset: 0x0014B770
		protected void Sb6(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ d;
			int num3 = b ^ num2;
			int num4 = num | num2;
			int num5 = c ^ num4;
			this.X1 = (b ^ num5);
			int num6 = num2 | this.X1;
			int num7 = d ^ num6;
			int num8 = num5 & num7;
			this.X2 = (num3 ^ num8);
			int num9 = num5 ^ num7;
			this.X0 = (this.X2 ^ num9);
			this.X3 = (~num5 ^ (num3 & num9));
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x0014D5DC File Offset: 0x0014B7DC
		protected void Ib6(int a, int b, int c, int d)
		{
			int num = ~a;
			int num2 = a ^ b;
			int num3 = c ^ num2;
			int num4 = c | num;
			int num5 = d ^ num4;
			this.X1 = (num3 ^ num5);
			int num6 = num3 & num5;
			int num7 = num2 ^ num6;
			int num8 = b | num7;
			this.X3 = (num5 ^ num8);
			int num9 = b | this.X3;
			this.X0 = (num7 ^ num9);
			this.X2 = ((d & num) ^ (num3 ^ num9));
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x0014D64C File Offset: 0x0014B84C
		protected void Sb7(int a, int b, int c, int d)
		{
			int num = b ^ c;
			int num2 = c & num;
			int num3 = d ^ num2;
			int num4 = a ^ num3;
			int num5 = d | num;
			int num6 = num4 & num5;
			this.X1 = (b ^ num6);
			int num7 = num3 | this.X1;
			int num8 = a & num4;
			this.X3 = (num ^ num8);
			int num9 = num4 ^ num7;
			int num10 = this.X3 & num9;
			this.X2 = (num3 ^ num10);
			this.X0 = (~num9 ^ (this.X3 & this.X2));
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x0014D6CC File Offset: 0x0014B8CC
		protected void Ib7(int a, int b, int c, int d)
		{
			int num = c | (a & b);
			int num2 = d & (a | b);
			this.X3 = (num ^ num2);
			int num3 = ~d;
			int num4 = b ^ num2;
			int num5 = num4 | (this.X3 ^ num3);
			this.X1 = (a ^ num5);
			this.X0 = (c ^ num4 ^ (d | this.X1));
			this.X2 = (num ^ this.X1 ^ (this.X0 ^ (a & this.X3)));
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x0014D740 File Offset: 0x0014B940
		protected void LT()
		{
			int num = SerpentEngineBase.RotateLeft(this.X0, 13);
			int num2 = SerpentEngineBase.RotateLeft(this.X2, 3);
			int x = this.X1 ^ num ^ num2;
			int x2 = this.X3 ^ num2 ^ num << 3;
			this.X1 = SerpentEngineBase.RotateLeft(x, 1);
			this.X3 = SerpentEngineBase.RotateLeft(x2, 7);
			this.X0 = SerpentEngineBase.RotateLeft(num ^ this.X1 ^ this.X3, 5);
			this.X2 = SerpentEngineBase.RotateLeft(num2 ^ this.X3 ^ this.X1 << 7, 22);
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x0014D7D4 File Offset: 0x0014B9D4
		protected void InverseLT()
		{
			int num = SerpentEngineBase.RotateRight(this.X2, 22) ^ this.X3 ^ this.X1 << 7;
			int num2 = SerpentEngineBase.RotateRight(this.X0, 5) ^ this.X1 ^ this.X3;
			int num3 = SerpentEngineBase.RotateRight(this.X3, 7);
			int num4 = SerpentEngineBase.RotateRight(this.X1, 1);
			this.X3 = (num3 ^ num ^ num2 << 3);
			this.X1 = (num4 ^ num2 ^ num);
			this.X2 = SerpentEngineBase.RotateRight(num, 3);
			this.X0 = SerpentEngineBase.RotateRight(num2, 13);
		}

		// Token: 0x0600358A RID: 13706
		protected abstract int[] MakeWorkingKey(byte[] key);

		// Token: 0x0600358B RID: 13707
		protected abstract void EncryptBlock(byte[] input, int inOff, byte[] output, int outOff);

		// Token: 0x0600358C RID: 13708
		protected abstract void DecryptBlock(byte[] input, int inOff, byte[] output, int outOff);

		// Token: 0x040021EE RID: 8686
		protected static readonly int BlockSize = 16;

		// Token: 0x040021EF RID: 8687
		internal const int ROUNDS = 32;

		// Token: 0x040021F0 RID: 8688
		internal const int PHI = -1640531527;

		// Token: 0x040021F1 RID: 8689
		protected bool encrypting;

		// Token: 0x040021F2 RID: 8690
		protected int[] wKey;

		// Token: 0x040021F3 RID: 8691
		protected int X0;

		// Token: 0x040021F4 RID: 8692
		protected int X1;

		// Token: 0x040021F5 RID: 8693
		protected int X2;

		// Token: 0x040021F6 RID: 8694
		protected int X3;
	}
}
