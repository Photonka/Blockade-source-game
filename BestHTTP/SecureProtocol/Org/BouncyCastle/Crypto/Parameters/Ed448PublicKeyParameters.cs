using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Rfc8032;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004C5 RID: 1221
	public sealed class Ed448PublicKeyParameters : AsymmetricKeyParameter
	{
		// Token: 0x06002FA6 RID: 12198 RVA: 0x001285DA File Offset: 0x001267DA
		public Ed448PublicKeyParameters(byte[] buf, int off) : base(false)
		{
			Array.Copy(buf, off, this.data, 0, Ed448PublicKeyParameters.KeySize);
		}

		// Token: 0x06002FA7 RID: 12199 RVA: 0x00128606 File Offset: 0x00126806
		public Ed448PublicKeyParameters(Stream input) : base(false)
		{
			if (Ed448PublicKeyParameters.KeySize != Streams.ReadFully(input, this.data))
			{
				throw new EndOfStreamException("EOF encountered in middle of Ed448 public key");
			}
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x0012863D File Offset: 0x0012683D
		public void Encode(byte[] buf, int off)
		{
			Array.Copy(this.data, 0, buf, off, Ed448PublicKeyParameters.KeySize);
		}

		// Token: 0x06002FA9 RID: 12201 RVA: 0x00128652 File Offset: 0x00126852
		public byte[] GetEncoded()
		{
			return Arrays.Clone(this.data);
		}

		// Token: 0x04001EAB RID: 7851
		public static readonly int KeySize = Ed448.PublicKeySize;

		// Token: 0x04001EAC RID: 7852
		private readonly byte[] data = new byte[Ed448PublicKeyParameters.KeySize];
	}
}
