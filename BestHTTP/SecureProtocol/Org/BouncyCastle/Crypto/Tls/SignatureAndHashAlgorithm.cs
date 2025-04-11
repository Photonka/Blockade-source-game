using System;
using System.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000433 RID: 1075
	public class SignatureAndHashAlgorithm
	{
		// Token: 0x06002AC0 RID: 10944 RVA: 0x00115C28 File Offset: 0x00113E28
		public SignatureAndHashAlgorithm(byte hash, byte signature)
		{
			if (!TlsUtilities.IsValidUint8((int)hash))
			{
				throw new ArgumentException("should be a uint8", "hash");
			}
			if (!TlsUtilities.IsValidUint8((int)signature))
			{
				throw new ArgumentException("should be a uint8", "signature");
			}
			if (signature == 0)
			{
				throw new ArgumentException("MUST NOT be \"anonymous\"", "signature");
			}
			this.mHash = hash;
			this.mSignature = signature;
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x00115C8C File Offset: 0x00113E8C
		public virtual byte Hash
		{
			get
			{
				return this.mHash;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x00115C94 File Offset: 0x00113E94
		public virtual byte Signature
		{
			get
			{
				return this.mSignature;
			}
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x00115C9C File Offset: 0x00113E9C
		public override bool Equals(object obj)
		{
			if (!(obj is SignatureAndHashAlgorithm))
			{
				return false;
			}
			SignatureAndHashAlgorithm signatureAndHashAlgorithm = (SignatureAndHashAlgorithm)obj;
			return signatureAndHashAlgorithm.Hash == this.Hash && signatureAndHashAlgorithm.Signature == this.Signature;
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x00115CD8 File Offset: 0x00113ED8
		public override int GetHashCode()
		{
			return (int)this.Hash << 16 | (int)this.Signature;
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x00115CEA File Offset: 0x00113EEA
		public virtual void Encode(Stream output)
		{
			TlsUtilities.WriteUint8(this.Hash, output);
			TlsUtilities.WriteUint8(this.Signature, output);
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x00115D04 File Offset: 0x00113F04
		public static SignatureAndHashAlgorithm Parse(Stream input)
		{
			byte hash = TlsUtilities.ReadUint8(input);
			byte signature = TlsUtilities.ReadUint8(input);
			return new SignatureAndHashAlgorithm(hash, signature);
		}

		// Token: 0x04001CAD RID: 7341
		protected readonly byte mHash;

		// Token: 0x04001CAE RID: 7342
		protected readonly byte mSignature;
	}
}
