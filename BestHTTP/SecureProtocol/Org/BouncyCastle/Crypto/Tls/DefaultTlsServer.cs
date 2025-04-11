using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000403 RID: 1027
	public abstract class DefaultTlsServer : AbstractTlsServer
	{
		// Token: 0x06002989 RID: 10633 RVA: 0x001110BC File Offset: 0x0010F2BC
		public DefaultTlsServer()
		{
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x001110C4 File Offset: 0x0010F2C4
		public DefaultTlsServer(TlsCipherFactory cipherFactory) : base(cipherFactory)
		{
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		protected virtual TlsSignerCredentials GetDsaSignerCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		protected virtual TlsSignerCredentials GetECDsaSignerCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		protected virtual TlsEncryptionCredentials GetRsaEncryptionCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x0010EA5F File Offset: 0x0010CC5F
		protected virtual TlsSignerCredentials GetRsaSignerCredentials()
		{
			throw new TlsFatalAlert(80);
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x001110CD File Offset: 0x0010F2CD
		protected virtual DHParameters GetDHParameters()
		{
			return DHStandardGroups.rfc7919_ffdhe2048;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x001110D4 File Offset: 0x0010F2D4
		protected override int[] GetCipherSuites()
		{
			return new int[]
			{
				49200,
				49199,
				49192,
				49191,
				49172,
				49171,
				159,
				158,
				107,
				103,
				57,
				51,
				157,
				156,
				61,
				60,
				53,
				47
			};
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x001110E8 File Offset: 0x0010F2E8
		public override TlsCredentials GetCredentials()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			switch (keyExchangeAlgorithm)
			{
			case 1:
				return this.GetRsaEncryptionCredentials();
			case 2:
			case 4:
				goto IL_66;
			case 3:
				return this.GetDsaSignerCredentials();
			case 5:
				break;
			default:
				if (keyExchangeAlgorithm != 11)
				{
					switch (keyExchangeAlgorithm)
					{
					case 17:
						return this.GetECDsaSignerCredentials();
					case 18:
						goto IL_66;
					case 19:
						goto IL_58;
					case 20:
						break;
					default:
						goto IL_66;
					}
				}
				return null;
			}
			IL_58:
			return this.GetRsaSignerCredentials();
			IL_66:
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x00111164 File Offset: 0x0010F364
		public override TlsKeyExchange GetKeyExchange()
		{
			int keyExchangeAlgorithm = TlsUtilities.GetKeyExchangeAlgorithm(this.mSelectedCipherSuite);
			switch (keyExchangeAlgorithm)
			{
			case 1:
				return this.CreateRsaKeyExchange();
			case 3:
			case 5:
				return this.CreateDheKeyExchange(keyExchangeAlgorithm);
			case 7:
			case 9:
			case 11:
				return this.CreateDHKeyExchange(keyExchangeAlgorithm);
			case 16:
			case 18:
			case 20:
				return this.CreateECDHKeyExchange(keyExchangeAlgorithm);
			case 17:
			case 19:
				return this.CreateECDheKeyExchange(keyExchangeAlgorithm);
			}
			throw new TlsFatalAlert(80);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x00111205 File Offset: 0x0010F405
		protected virtual TlsKeyExchange CreateDHKeyExchange(int keyExchange)
		{
			return new TlsDHKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, null, this.GetDHParameters());
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x0011121A File Offset: 0x0010F41A
		protected virtual TlsKeyExchange CreateDheKeyExchange(int keyExchange)
		{
			return new TlsDheKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, null, this.GetDHParameters());
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x0011122F File Offset: 0x0010F42F
		protected virtual TlsKeyExchange CreateECDHKeyExchange(int keyExchange)
		{
			return new TlsECDHKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x06002996 RID: 10646 RVA: 0x0011124F File Offset: 0x0010F44F
		protected virtual TlsKeyExchange CreateECDheKeyExchange(int keyExchange)
		{
			return new TlsECDheKeyExchange(keyExchange, this.mSupportedSignatureAlgorithms, this.mNamedCurves, this.mClientECPointFormats, this.mServerECPointFormats);
		}

		// Token: 0x06002997 RID: 10647 RVA: 0x0011126F File Offset: 0x0010F46F
		protected virtual TlsKeyExchange CreateRsaKeyExchange()
		{
			return new TlsRsaKeyExchange(this.mSupportedSignatureAlgorithms);
		}
	}
}
