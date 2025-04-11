using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc7748;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004EE RID: 1262
	public sealed class X25519PrivateKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06003062 RID: 12386 RVA: 0x00129A36 File Offset: 0x00127C36
		public X25519PrivateKeyParameters(SecureRandom random) : base(true)
		{
			X25519.GeneratePrivateKey(random, this.data);
		}

		// Token: 0x06003063 RID: 12387 RVA: 0x00129A5B File Offset: 0x00127C5B
		public X25519PrivateKeyParameters(byte[] buf, int off) : base(true)
		{
			Array.Copy(buf, off, this.data, 0, X25519PrivateKeyParameters.KeySize);
		}

		// Token: 0x06003064 RID: 12388 RVA: 0x00129A87 File Offset: 0x00127C87
		public X25519PrivateKeyParameters(Stream input) : base(true)
		{
			if (X25519PrivateKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of X25519 private key");
			}
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x00129ABE File Offset: 0x00127CBE
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, X25519PrivateKeyParameters.KeySize);
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x00129AD3 File Offset: 0x00127CD3
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x00129AE0 File Offset: 0x00127CE0
		public X25519PublicKeyParameters GeneratePublicKey()
		{
			byte[] array = new byte[32];
			X25519.GeneratePublicKey(this.data, 0, array, 0);
			return new X25519PublicKeyParameters(array, 0);
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x00129B0C File Offset: 0x00127D0C
		public void GenerateSecret(X25519PublicKeyParameters publicKey, byte[] buf, int off)
		{
			byte[] array = new byte[32];
			publicKey.Encode(array, 0);
			if (!X25519.CalculateAgreement(this.data, 0, array, 0, buf, off))
			{
				throw new InvalidOperationException("X25519 agreement failed");
			}
		}

		// Token: 0x04001F08 RID: 7944
		public static readonly int KeySize = 32;

		// Token: 0x04001F09 RID: 7945
		public static readonly int SecretSize = 32;

		// Token: 0x04001F0A RID: 7946
		private readonly byte[] data = new byte[X25519PrivateKeyParameters.KeySize];
	}
}
