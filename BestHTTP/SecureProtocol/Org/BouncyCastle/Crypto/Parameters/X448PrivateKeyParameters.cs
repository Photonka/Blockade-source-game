using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc7748;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004F1 RID: 1265
	public sealed class X448PrivateKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06003070 RID: 12400 RVA: 0x00129BE4 File Offset: 0x00127DE4
		public X448PrivateKeyParameters(SecureRandom random) : base(true)
		{
			X448.GeneratePrivateKey(random, this.data);
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x00129C09 File Offset: 0x00127E09
		public X448PrivateKeyParameters(byte[] buf, int off) : base(true)
		{
			Array.Copy(buf, off, this.data, 0, X448PrivateKeyParameters.KeySize);
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x00129C35 File Offset: 0x00127E35
		public X448PrivateKeyParameters(Stream input) : base(true)
		{
			if (X448PrivateKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of X448 private key");
			}
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x00129C6C File Offset: 0x00127E6C
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, X448PrivateKeyParameters.KeySize);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x00129C81 File Offset: 0x00127E81
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x00129C90 File Offset: 0x00127E90
		public X448PublicKeyParameters GeneratePublicKey()
		{
			byte[] array = new byte[56];
			X448.GeneratePublicKey(this.data, 0, array, 0);
			return new X448PublicKeyParameters(array, 0);
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x00129CBC File Offset: 0x00127EBC
		public void GenerateSecret(X448PublicKeyParameters publicKey, byte[] buf, int off)
		{
			byte[] array = new byte[56];
			publicKey.Encode(array, 0);
			if (!X448.CalculateAgreement(this.data, 0, array, 0, buf, off))
			{
				throw new InvalidOperationException("X448 agreement failed");
			}
		}

		// Token: 0x04001F0D RID: 7949
		public static readonly int KeySize = 56;

		// Token: 0x04001F0E RID: 7950
		public static readonly int SecretSize = 56;

		// Token: 0x04001F0F RID: 7951
		private readonly byte[] data = new byte[X448PrivateKeyParameters.KeySize];
	}
}
