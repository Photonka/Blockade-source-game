using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003B1 RID: 945
	public abstract class BufferedCipherBase : IBufferedCipher
	{
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600276B RID: 10091
		public abstract string AlgorithmName { get; }

		// Token: 0x0600276C RID: 10092
		public abstract void Init(bool forEncryption, ICipherParameters parameters);

		// Token: 0x0600276D RID: 10093
		public abstract int GetBlockSize();

		// Token: 0x0600276E RID: 10094
		public abstract int GetOutputSize(int inputLen);

		// Token: 0x0600276F RID: 10095
		public abstract int GetUpdateOutputSize(int inputLen);

		// Token: 0x06002770 RID: 10096
		public abstract byte[] ProcessByte(byte input);

		// Token: 0x06002771 RID: 10097 RVA: 0x0010DE00 File Offset: 0x0010C000
		public virtual int ProcessByte(byte input, byte[] output, int outOff)
		{
			byte[] array = this.ProcessByte(input);
			if (array == null)
			{
				return 0;
			}
			if (outOff + array.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			array.CopyTo(output, outOff);
			return array.Length;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0010DE3A File Offset: 0x0010C03A
		public virtual byte[] ProcessBytes(byte[] input)
		{
			return this.ProcessBytes(input, 0, input.Length);
		}

		// Token: 0x06002773 RID: 10099
		public abstract byte[] ProcessBytes(byte[] input, int inOff, int length);

		// Token: 0x06002774 RID: 10100 RVA: 0x0010DE47 File Offset: 0x0010C047
		public virtual int ProcessBytes(byte[] input, byte[] output, int outOff)
		{
			return this.ProcessBytes(input, 0, input.Length, output, outOff);
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0010DE58 File Offset: 0x0010C058
		public virtual int ProcessBytes(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			byte[] array = this.ProcessBytes(input, inOff, length);
			if (array == null)
			{
				return 0;
			}
			if (outOff + array.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			array.CopyTo(output, outOff);
			return array.Length;
		}

		// Token: 0x06002776 RID: 10102
		public abstract byte[] DoFinal();

		// Token: 0x06002777 RID: 10103 RVA: 0x0010DE98 File Offset: 0x0010C098
		public virtual byte[] DoFinal(byte[] input)
		{
			return this.DoFinal(input, 0, input.Length);
		}

		// Token: 0x06002778 RID: 10104
		public abstract byte[] DoFinal(byte[] input, int inOff, int length);

		// Token: 0x06002779 RID: 10105 RVA: 0x0010DEA8 File Offset: 0x0010C0A8
		public virtual int DoFinal(byte[] output, int outOff)
		{
			byte[] array = this.DoFinal();
			if (outOff + array.Length > output.Length)
			{
				throw new DataLengthException("output buffer too short");
			}
			array.CopyTo(output, outOff);
			return array.Length;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0010DEDC File Offset: 0x0010C0DC
		public virtual int DoFinal(byte[] input, byte[] output, int outOff)
		{
			return this.DoFinal(input, 0, input.Length, output, outOff);
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x0010DEEC File Offset: 0x0010C0EC
		public virtual int DoFinal(byte[] input, int inOff, int length, byte[] output, int outOff)
		{
			int num = this.ProcessBytes(input, inOff, length, output, outOff);
			return num + this.DoFinal(output, outOff + num);
		}

		// Token: 0x0600277C RID: 10108
		public abstract void Reset();

		// Token: 0x040019C1 RID: 6593
		protected static readonly byte[] EmptyBuffer = new byte[0];
	}
}
