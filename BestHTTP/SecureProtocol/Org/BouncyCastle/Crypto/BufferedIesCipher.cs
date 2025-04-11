using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto
{
	// Token: 0x020003B2 RID: 946
	public class BufferedIesCipher : BufferedCipherBase
	{
		// Token: 0x0600277F RID: 10111 RVA: 0x0010DF24 File Offset: 0x0010C124
		public BufferedIesCipher(IesEngine engine)
		{
			if (engine == null)
			{
				throw new ArgumentNullException("engine");
			}
			this.engine = engine;
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x0010DF4C File Offset: 0x0010C14C
		public override string AlgorithmName
		{
			get
			{
				return "IES";
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x0010DF53 File Offset: 0x0010C153
		public override void Init(bool forEncryption, ICipherParameters parameters)
		{
			this.forEncryption = forEncryption;
			throw Platform.CreateNotImplementedException("IES");
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override int GetBlockSize()
		{
			return 0;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x0010DF68 File Offset: 0x0010C168
		public override int GetOutputSize(int inputLen)
		{
			if (this.engine == null)
			{
				throw new InvalidOperationException("cipher not initialised");
			}
			int num = inputLen + (int)this.buffer.Length;
			if (!this.forEncryption)
			{
				return num - 20;
			}
			return num + 20;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public override int GetUpdateOutputSize(int inputLen)
		{
			return 0;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x0010DFA8 File Offset: 0x0010C1A8
		public override byte[] ProcessByte(byte input)
		{
			this.buffer.WriteByte(input);
			return null;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0010DFB8 File Offset: 0x0010C1B8
		public override byte[] ProcessBytes(byte[] input, int inOff, int length)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (inOff < 0)
			{
				throw new ArgumentException("inOff");
			}
			if (length < 0)
			{
				throw new ArgumentException("length");
			}
			if (inOff + length > input.Length)
			{
				throw new ArgumentException("invalid offset/length specified for input array");
			}
			this.buffer.Write(input, inOff, length);
			return null;
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0010E014 File Offset: 0x0010C214
		public override byte[] DoFinal()
		{
			byte[] array = this.buffer.ToArray();
			this.Reset();
			return this.engine.ProcessBlock(array, 0, array.Length);
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0010D951 File Offset: 0x0010BB51
		public override byte[] DoFinal(byte[] input, int inOff, int length)
		{
			this.ProcessBytes(input, inOff, length);
			return this.DoFinal();
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0010E043 File Offset: 0x0010C243
		public override void Reset()
		{
			this.buffer.SetLength(0L);
		}

		// Token: 0x040019C2 RID: 6594
		private readonly IesEngine engine;

		// Token: 0x040019C3 RID: 6595
		private bool forEncryption;

		// Token: 0x040019C4 RID: 6596
		private MemoryStream buffer = new MemoryStream();
	}
}
