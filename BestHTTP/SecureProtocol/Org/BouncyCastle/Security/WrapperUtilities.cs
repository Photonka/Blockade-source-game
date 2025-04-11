using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Kisa;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ntt;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Security
{
	// Token: 0x020002D6 RID: 726
	public sealed class WrapperUtilities
	{
		// Token: 0x06001AF2 RID: 6898 RVA: 0x00023EF4 File Offset: 0x000220F4
		private WrapperUtilities()
		{
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000D179C File Offset: 0x000CF99C
		static WrapperUtilities()
		{
			((WrapperUtilities.WrapAlgorithm)Enums.GetArbitraryValue(typeof(WrapperUtilities.WrapAlgorithm))).ToString();
			WrapperUtilities.algorithms[NistObjectIdentifiers.IdAes128Wrap.Id] = "AESWRAP";
			WrapperUtilities.algorithms[NistObjectIdentifiers.IdAes192Wrap.Id] = "AESWRAP";
			WrapperUtilities.algorithms[NistObjectIdentifiers.IdAes256Wrap.Id] = "AESWRAP";
			WrapperUtilities.algorithms[NttObjectIdentifiers.IdCamellia128Wrap.Id] = "CAMELLIAWRAP";
			WrapperUtilities.algorithms[NttObjectIdentifiers.IdCamellia192Wrap.Id] = "CAMELLIAWRAP";
			WrapperUtilities.algorithms[NttObjectIdentifiers.IdCamellia256Wrap.Id] = "CAMELLIAWRAP";
			WrapperUtilities.algorithms[PkcsObjectIdentifiers.IdAlgCms3DesWrap.Id] = "DESEDEWRAP";
			WrapperUtilities.algorithms["TDEAWRAP"] = "DESEDEWRAP";
			WrapperUtilities.algorithms[PkcsObjectIdentifiers.IdAlgCmsRC2Wrap.Id] = "RC2WRAP";
			WrapperUtilities.algorithms[KisaObjectIdentifiers.IdNpkiAppCmsSeedWrap.Id] = "SEEDWRAP";
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000D18CB File Offset: 0x000CFACB
		public static IWrapper GetWrapper(DerObjectIdentifier oid)
		{
			return WrapperUtilities.GetWrapper(oid.Id);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000D18D8 File Offset: 0x000CFAD8
		public static IWrapper GetWrapper(string algorithm)
		{
			string text = Platform.ToUpperInvariant(algorithm);
			string text2 = (string)WrapperUtilities.algorithms[text];
			if (text2 == null)
			{
				text2 = text;
			}
			try
			{
				switch ((WrapperUtilities.WrapAlgorithm)Enums.GetEnumValue(typeof(WrapperUtilities.WrapAlgorithm), text2))
				{
				case WrapperUtilities.WrapAlgorithm.AESWRAP:
					return new AesWrapEngine();
				case WrapperUtilities.WrapAlgorithm.CAMELLIAWRAP:
					return new CamelliaWrapEngine();
				case WrapperUtilities.WrapAlgorithm.DESEDEWRAP:
					return new DesEdeWrapEngine();
				case WrapperUtilities.WrapAlgorithm.RC2WRAP:
					return new RC2WrapEngine();
				case WrapperUtilities.WrapAlgorithm.SEEDWRAP:
					return new SeedWrapEngine();
				case WrapperUtilities.WrapAlgorithm.DESEDERFC3211WRAP:
					return new Rfc3211WrapEngine(new DesEdeEngine());
				case WrapperUtilities.WrapAlgorithm.AESRFC3211WRAP:
					return new Rfc3211WrapEngine(new AesEngine());
				case WrapperUtilities.WrapAlgorithm.CAMELLIARFC3211WRAP:
					return new Rfc3211WrapEngine(new CamelliaEngine());
				}
			}
			catch (ArgumentException)
			{
			}
			IBufferedCipher cipher = CipherUtilities.GetCipher(algorithm);
			if (cipher != null)
			{
				return new WrapperUtilities.BufferedCipherWrapper(cipher);
			}
			throw new SecurityUtilityException("Wrapper " + algorithm + " not recognised.");
		}

		// Token: 0x06001AF6 RID: 6902 RVA: 0x000D19D8 File Offset: 0x000CFBD8
		public static string GetAlgorithmName(DerObjectIdentifier oid)
		{
			return (string)WrapperUtilities.algorithms[oid.Id];
		}

		// Token: 0x040017B8 RID: 6072
		private static readonly IDictionary algorithms = Platform.CreateHashtable();

		// Token: 0x020008E1 RID: 2273
		private enum WrapAlgorithm
		{
			// Token: 0x04003416 RID: 13334
			AESWRAP,
			// Token: 0x04003417 RID: 13335
			CAMELLIAWRAP,
			// Token: 0x04003418 RID: 13336
			DESEDEWRAP,
			// Token: 0x04003419 RID: 13337
			RC2WRAP,
			// Token: 0x0400341A RID: 13338
			SEEDWRAP,
			// Token: 0x0400341B RID: 13339
			DESEDERFC3211WRAP,
			// Token: 0x0400341C RID: 13340
			AESRFC3211WRAP,
			// Token: 0x0400341D RID: 13341
			CAMELLIARFC3211WRAP
		}

		// Token: 0x020008E2 RID: 2274
		private class BufferedCipherWrapper : IWrapper
		{
			// Token: 0x06004D70 RID: 19824 RVA: 0x001B0BDA File Offset: 0x001AEDDA
			public BufferedCipherWrapper(IBufferedCipher cipher)
			{
				this.cipher = cipher;
			}

			// Token: 0x17000C11 RID: 3089
			// (get) Token: 0x06004D71 RID: 19825 RVA: 0x001B0BE9 File Offset: 0x001AEDE9
			public string AlgorithmName
			{
				get
				{
					return this.cipher.AlgorithmName;
				}
			}

			// Token: 0x06004D72 RID: 19826 RVA: 0x001B0BF6 File Offset: 0x001AEDF6
			public void Init(bool forWrapping, ICipherParameters parameters)
			{
				this.forWrapping = forWrapping;
				this.cipher.Init(forWrapping, parameters);
			}

			// Token: 0x06004D73 RID: 19827 RVA: 0x001B0C0C File Offset: 0x001AEE0C
			public byte[] Wrap(byte[] input, int inOff, int length)
			{
				if (!this.forWrapping)
				{
					throw new InvalidOperationException("Not initialised for wrapping");
				}
				return this.cipher.DoFinal(input, inOff, length);
			}

			// Token: 0x06004D74 RID: 19828 RVA: 0x001B0C2F File Offset: 0x001AEE2F
			public byte[] Unwrap(byte[] input, int inOff, int length)
			{
				if (this.forWrapping)
				{
					throw new InvalidOperationException("Not initialised for unwrapping");
				}
				return this.cipher.DoFinal(input, inOff, length);
			}

			// Token: 0x0400341E RID: 13342
			private readonly IBufferedCipher cipher;

			// Token: 0x0400341F RID: 13343
			private bool forWrapping;
		}
	}
}
