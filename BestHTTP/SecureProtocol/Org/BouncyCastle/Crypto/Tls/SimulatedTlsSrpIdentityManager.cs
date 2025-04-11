using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Agreement.Srp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Macs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000435 RID: 1077
	public class SimulatedTlsSrpIdentityManager : TlsSrpIdentityManager
	{
		// Token: 0x06002AC9 RID: 10953 RVA: 0x00115D34 File Offset: 0x00113F34
		public static SimulatedTlsSrpIdentityManager GetRfc5054Default(Srp6GroupParameters group, byte[] seedKey)
		{
			Srp6VerifierGenerator srp6VerifierGenerator = new Srp6VerifierGenerator();
			srp6VerifierGenerator.Init(group, TlsUtilities.CreateHash(2));
			HMac hmac = new HMac(TlsUtilities.CreateHash(2));
			hmac.Init(new KeyParameter(seedKey));
			return new SimulatedTlsSrpIdentityManager(group, srp6VerifierGenerator, hmac);
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x00115D74 File Offset: 0x00113F74
		public SimulatedTlsSrpIdentityManager(Srp6GroupParameters group, Srp6VerifierGenerator verifierGenerator, IMac mac)
		{
			this.mGroup = group;
			this.mVerifierGenerator = verifierGenerator;
			this.mMac = mac;
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x00115D94 File Offset: 0x00113F94
		public virtual TlsSrpLoginParameters GetLoginParameters(byte[] identity)
		{
			this.mMac.BlockUpdate(SimulatedTlsSrpIdentityManager.PREFIX_SALT, 0, SimulatedTlsSrpIdentityManager.PREFIX_SALT.Length);
			this.mMac.BlockUpdate(identity, 0, identity.Length);
			byte[] array = new byte[this.mMac.GetMacSize()];
			this.mMac.DoFinal(array, 0);
			this.mMac.BlockUpdate(SimulatedTlsSrpIdentityManager.PREFIX_PASSWORD, 0, SimulatedTlsSrpIdentityManager.PREFIX_PASSWORD.Length);
			this.mMac.BlockUpdate(identity, 0, identity.Length);
			byte[] array2 = new byte[this.mMac.GetMacSize()];
			this.mMac.DoFinal(array2, 0);
			BigInteger verifier = this.mVerifierGenerator.GenerateVerifier(array, identity, array2);
			return new TlsSrpLoginParameters(this.mGroup, verifier, array);
		}

		// Token: 0x04001CAF RID: 7343
		private static readonly byte[] PREFIX_PASSWORD = Strings.ToByteArray("password");

		// Token: 0x04001CB0 RID: 7344
		private static readonly byte[] PREFIX_SALT = Strings.ToByteArray("salt");

		// Token: 0x04001CB1 RID: 7345
		protected readonly Srp6GroupParameters mGroup;

		// Token: 0x04001CB2 RID: 7346
		protected readonly Srp6VerifierGenerator mVerifierGenerator;

		// Token: 0x04001CB3 RID: 7347
		protected readonly IMac mMac;
	}
}
