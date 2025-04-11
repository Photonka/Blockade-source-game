using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C1 RID: 1217
	public sealed class Ed25519PrivateKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002F90 RID: 12176 RVA: 0x0012820E File Offset: 0x0012640E
		public Ed25519PrivateKeyParameters(SecureRandom random) : base(true)
		{
			Ed25519.GeneratePrivateKey(random, this.data);
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x00128233 File Offset: 0x00126433
		public Ed25519PrivateKeyParameters(byte[] buf, int off) : base(true)
		{
			Array.Copy(buf, off, this.data, 0, Ed25519PrivateKeyParameters.KeySize);
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x0012825F File Offset: 0x0012645F
		public Ed25519PrivateKeyParameters(Stream input) : base(true)
		{
			if (Ed25519PrivateKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of Ed25519 private key");
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x00128296 File Offset: 0x00126496
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, Ed25519PrivateKeyParameters.KeySize);
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x001282AB File Offset: 0x001264AB
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x001282B8 File Offset: 0x001264B8
		public Ed25519PublicKeyParameters GeneratePublicKey()
		{
			byte[] array = new byte[Ed25519.PublicKeySize];
			Ed25519.GeneratePublicKey(this.data, 0, array, 0);
			return new Ed25519PublicKeyParameters(array, 0);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x001282E8 File Offset: 0x001264E8
		public void Sign(Ed25519.Algorithm algorithm, Ed25519PublicKeyParameters publicKey, byte[] ctx, byte[] msg, int msgOff, int msgLen, byte[] sig, int sigOff)
		{
			byte[] array = new byte[Ed25519.PublicKeySize];
			if (publicKey == null)
			{
				Ed25519.GeneratePublicKey(this.data, 0, array, 0);
			}
			else
			{
				publicKey.Encode(array, 0);
			}
			switch (algorithm)
			{
			case Ed25519.Algorithm.Ed25519:
				if (ctx != null)
				{
					throw new ArgumentException("ctx");
				}
				Ed25519.Sign(this.data, 0, array, 0, msg, msgOff, msgLen, sig, sigOff);
				return;
			case Ed25519.Algorithm.Ed25519ctx:
				Ed25519.Sign(this.data, 0, array, 0, ctx, msg, msgOff, msgLen, sig, sigOff);
				return;
			case Ed25519.Algorithm.Ed25519ph:
				if (Ed25519.PrehashSize != msgLen)
				{
					throw new ArgumentException("msgLen");
				}
				Ed25519.SignPrehash(this.data, 0, array, 0, ctx, msg, msgOff, sig, sigOff);
				return;
			default:
				throw new ArgumentException("algorithm");
			}
		}

		// Token: 0x04001EA3 RID: 7843
		public static readonly int KeySize = Ed25519.SecretKeySize;

		// Token: 0x04001EA4 RID: 7844
		public static readonly int SignatureSize = Ed25519.SignatureSize;

		// Token: 0x04001EA5 RID: 7845
		private readonly byte[] data = new byte[Ed25519PrivateKeyParameters.KeySize];
	}
}
