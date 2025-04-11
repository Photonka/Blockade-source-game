using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C4 RID: 1220
	public sealed class Ed448PrivateKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002F9E RID: 12190 RVA: 0x0012845B File Offset: 0x0012665B
		public Ed448PrivateKeyParameters(SecureRandom random) : base(true)
		{
			Ed448.GeneratePrivateKey(random, this.data);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x00128480 File Offset: 0x00126680
		public Ed448PrivateKeyParameters(byte[] buf, int off) : base(true)
		{
			Array.Copy(buf, off, this.data, 0, Ed448PrivateKeyParameters.KeySize);
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x001284AC File Offset: 0x001266AC
		public Ed448PrivateKeyParameters(Stream input) : base(true)
		{
			if (Ed448PrivateKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of Ed448 private key");
			}
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x001284E3 File Offset: 0x001266E3
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, Ed448PrivateKeyParameters.KeySize);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x001284F8 File Offset: 0x001266F8
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x00128508 File Offset: 0x00126708
		public Ed448PublicKeyParameters GeneratePublicKey()
		{
			byte[] array = new byte[Ed448.PublicKeySize];
			Ed448.GeneratePublicKey(this.data, 0, array, 0);
			return new Ed448PublicKeyParameters(array, 0);
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x00128538 File Offset: 0x00126738
		public void Sign(Ed448.Algorithm algorithm, Ed448PublicKeyParameters publicKey, byte[] ctx, byte[] msg, int msgOff, int msgLen, byte[] sig, int sigOff)
		{
			byte[] array = new byte[Ed448.PublicKeySize];
			if (publicKey == null)
			{
				Ed448.GeneratePublicKey(this.data, 0, array, 0);
			}
			else
			{
				publicKey.Encode(array, 0);
			}
			if (algorithm == Ed448.Algorithm.Ed448)
			{
				Ed448.Sign(this.data, 0, array, 0, ctx, msg, msgOff, msgLen, sig, sigOff);
				return;
			}
			if (algorithm != Ed448.Algorithm.Ed448ph)
			{
				throw new ArgumentException("algorithm");
			}
			if (Ed448.PrehashSize != msgLen)
			{
				throw new ArgumentException("msgLen");
			}
			Ed448.SignPrehash(this.data, 0, array, 0, ctx, msg, msgOff, sig, sigOff);
		}

		// Token: 0x04001EA8 RID: 7848
		public static readonly int KeySize = Ed448.SecretKeySize;

		// Token: 0x04001EA9 RID: 7849
		public static readonly int SignatureSize = Ed448.SignatureSize;

		// Token: 0x04001EAA RID: 7850
		private readonly byte[] data = new byte[Ed448PrivateKeyParameters.KeySize];
	}
}
