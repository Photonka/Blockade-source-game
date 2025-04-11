using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004F2 RID: 1266
	public sealed class X448PublicKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06003078 RID: 12408 RVA: 0x00129D06 File Offset: 0x00127F06
		public X448PublicKeyParameters(byte[] buf, int off) : base(false)
		{
			Array.Copy(buf, off, this.data, 0, X448PublicKeyParameters.KeySize);
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x00129D32 File Offset: 0x00127F32
		public X448PublicKeyParameters(Stream input) : base(false)
		{
			if (X448PublicKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of X448 public key");
			}
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x00129D69 File Offset: 0x00127F69
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, X448PublicKeyParameters.KeySize);
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x00129D7E File Offset: 0x00127F7E
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x04001F10 RID: 7952
		public static readonly int KeySize = 56;

		// Token: 0x04001F11 RID: 7953
		private readonly byte[] data = new byte[X448PublicKeyParameters.KeySize];
	}
}
