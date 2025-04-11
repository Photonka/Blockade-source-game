using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004EF RID: 1263
	public sealed class X25519PublicKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x0600306A RID: 12394 RVA: 0x00129B56 File Offset: 0x00127D56
		public X25519PublicKeyParameters(byte[] buf, int off) : base(false)
		{
			Array.Copy(buf, off, this.data, 0, X25519PublicKeyParameters.KeySize);
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x00129B82 File Offset: 0x00127D82
		public X25519PublicKeyParameters(Stream input) : base(false)
		{
			if (X25519PublicKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of X25519 public key");
			}
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x00129BB9 File Offset: 0x00127DB9
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, X25519PublicKeyParameters.KeySize);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x00129BCE File Offset: 0x00127DCE
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x04001F0B RID: 7947
		public static readonly int KeySize = 32;

		// Token: 0x04001F0C RID: 7948
		private readonly byte[] data = new byte[X25519PublicKeyParameters.KeySize];
	}
}
