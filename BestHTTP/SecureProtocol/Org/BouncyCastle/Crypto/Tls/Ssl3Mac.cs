using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000439 RID: 1081
	public class Ssl3Mac : IMac
	{
		// Token: 0x06002AE2 RID: 10978 RVA: 0x0011606D File Offset: 0x0011426D
		public Ssl3Mac(IDigest digest)
		{
			this.digest = digest;
			if (digest.GetDigestSize() == 20)
			{
				this.padLength = 40;
				return;
			}
			this.padLength = 48;
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x00116097 File Offset: 0x00114297
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "/SSL3MAC";
			}
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x001160AE File Offset: 0x001142AE
		public virtual void Init(ICipherParameters parameters)
		{
			this.secret = Arrays.Clone(((KeyParameter)parameters).GetKey());
			this.Reset();
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x001160CC File Offset: 0x001142CC
		public virtual int GetMacSize()
		{
			return this.digest.GetDigestSize();
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x001160D9 File Offset: 0x001142D9
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x001160E7 File Offset: 0x001142E7
		public virtual void BlockUpdate(byte[] input, int inOff, int len)
		{
			this.digest.BlockUpdate(input, inOff, len);
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x001160F8 File Offset: 0x001142F8
		public virtual int DoFinal(byte[] output, int outOff)
		{
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			this.digest.BlockUpdate(this.secret, 0, this.secret.Length);
			this.digest.BlockUpdate(Ssl3Mac.OPAD, 0, this.padLength);
			this.digest.BlockUpdate(array, 0, array.Length);
			int result = this.digest.DoFinal(output, outOff);
			this.Reset();
			return result;
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x00116178 File Offset: 0x00114378
		public virtual void Reset()
		{
			this.digest.Reset();
			this.digest.BlockUpdate(this.secret, 0, this.secret.Length);
			this.digest.BlockUpdate(Ssl3Mac.IPAD, 0, this.padLength);
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x001161B6 File Offset: 0x001143B6
		private static byte[] GenPad(byte b, int count)
		{
			byte[] array = new byte[count];
			Arrays.Fill(array, b);
			return array;
		}

		// Token: 0x04001CC0 RID: 7360
		private const byte IPAD_BYTE = 54;

		// Token: 0x04001CC1 RID: 7361
		private const byte OPAD_BYTE = 92;

		// Token: 0x04001CC2 RID: 7362
		internal static readonly byte[] IPAD = Ssl3Mac.GenPad(54, 48);

		// Token: 0x04001CC3 RID: 7363
		internal static readonly byte[] OPAD = Ssl3Mac.GenPad(92, 48);

		// Token: 0x04001CC4 RID: 7364
		private readonly IDigest digest;

		// Token: 0x04001CC5 RID: 7365
		private readonly int padLength;

		// Token: 0x04001CC6 RID: 7366
		private byte[] secret;
	}
}
