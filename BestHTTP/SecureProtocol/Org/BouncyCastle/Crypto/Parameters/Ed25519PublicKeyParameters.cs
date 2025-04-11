using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C2 RID: 1218
	public sealed class Ed25519PublicKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002F98 RID: 12184 RVA: 0x001283BC File Offset: 0x001265BC
		public Ed25519PublicKeyParameters(byte[] buf, int off) : base(false)
		{
			Array.Copy(buf, off, this.data, 0, Ed25519PublicKeyParameters.KeySize);
		}

		// Token: 0x06002F99 RID: 12185 RVA: 0x001283E8 File Offset: 0x001265E8
		public Ed25519PublicKeyParameters(Stream input) : base(false)
		{
			if (Ed25519PublicKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of Ed25519 public key");
			}
		}

		// Token: 0x06002F9A RID: 12186 RVA: 0x0012841F File Offset: 0x0012661F
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, Ed25519PublicKeyParameters.KeySize);
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x00128434 File Offset: 0x00126634
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x04001EA6 RID: 7846
		public static readonly int KeySize = Ed25519.PublicKeySize;

		// Token: 0x04001EA7 RID: 7847
		private readonly byte[] data = new byte[Ed25519PublicKeyParameters.KeySize];
	}
}
