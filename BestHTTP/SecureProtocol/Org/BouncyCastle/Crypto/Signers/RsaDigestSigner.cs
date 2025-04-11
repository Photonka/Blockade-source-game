using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.TeleTrust;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Encodings;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Engines;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Signers
{
	// Token: 0x02000492 RID: 1170
	public class RsaDigestSigner : ISigner
	{
		// Token: 0x06002E4A RID: 11850 RVA: 0x00123EE0 File Offset: 0x001220E0
		static RsaDigestSigner()
		{
			RsaDigestSigner.oidMap["RIPEMD128"] = TeleTrusTObjectIdentifiers.RipeMD128;
			RsaDigestSigner.oidMap["RIPEMD160"] = TeleTrusTObjectIdentifiers.RipeMD160;
			RsaDigestSigner.oidMap["RIPEMD256"] = TeleTrusTObjectIdentifiers.RipeMD256;
			RsaDigestSigner.oidMap["SHA-1"] = X509ObjectIdentifiers.IdSha1;
			RsaDigestSigner.oidMap["SHA-224"] = NistObjectIdentifiers.IdSha224;
			RsaDigestSigner.oidMap["SHA-256"] = NistObjectIdentifiers.IdSha256;
			RsaDigestSigner.oidMap["SHA-384"] = NistObjectIdentifiers.IdSha384;
			RsaDigestSigner.oidMap["SHA-512"] = NistObjectIdentifiers.IdSha512;
			RsaDigestSigner.oidMap["MD2"] = PkcsObjectIdentifiers.MD2;
			RsaDigestSigner.oidMap["MD4"] = PkcsObjectIdentifiers.MD4;
			RsaDigestSigner.oidMap["MD5"] = PkcsObjectIdentifiers.MD5;
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x00123FD3 File Offset: 0x001221D3
		public RsaDigestSigner(IDigest digest) : this(digest, (DerObjectIdentifier)RsaDigestSigner.oidMap[digest.AlgorithmName])
		{
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x00123FF1 File Offset: 0x001221F1
		public RsaDigestSigner(IDigest digest, DerObjectIdentifier digestOid) : this(digest, new AlgorithmIdentifier(digestOid, DerNull.Instance))
		{
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x00124005 File Offset: 0x00122205
		public RsaDigestSigner(IDigest digest, AlgorithmIdentifier algId) : this(new RsaCoreEngine(), digest, algId)
		{
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x00124014 File Offset: 0x00122214
		public RsaDigestSigner(IRsa rsa, IDigest digest, DerObjectIdentifier digestOid) : this(rsa, digest, new AlgorithmIdentifier(digestOid, DerNull.Instance))
		{
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x00124029 File Offset: 0x00122229
		public RsaDigestSigner(IRsa rsa, IDigest digest, AlgorithmIdentifier algId) : this(new RsaBlindedEngine(rsa), digest, algId)
		{
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x00124039 File Offset: 0x00122239
		public RsaDigestSigner(IAsymmetricBlockCipher rsaEngine, IDigest digest, AlgorithmIdentifier algId)
		{
			this.rsaEngine = new Pkcs1Encoding(rsaEngine);
			this.digest = digest;
			this.algId = algId;
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06002E51 RID: 11857 RVA: 0x0012405B File Offset: 0x0012225B
		public virtual string AlgorithmName
		{
			get
			{
				return this.digest.AlgorithmName + "withRSA";
			}
		}

		// Token: 0x06002E52 RID: 11858 RVA: 0x00124074 File Offset: 0x00122274
		public virtual void Init(bool forSigning, ICipherParameters parameters)
		{
			this.forSigning = forSigning;
			AsymmetricKeyParameter asymmetricKeyParameter;
			if (parameters is ParametersWithRandom)
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)((ParametersWithRandom)parameters).Parameters;
			}
			else
			{
				asymmetricKeyParameter = (AsymmetricKeyParameter)parameters;
			}
			if (forSigning && !asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Signing requires private key.");
			}
			if (!forSigning && asymmetricKeyParameter.IsPrivate)
			{
				throw new InvalidKeyException("Verification requires public key.");
			}
			this.Reset();
			this.rsaEngine.Init(forSigning, parameters);
		}

		// Token: 0x06002E53 RID: 11859 RVA: 0x001240E9 File Offset: 0x001222E9
		public virtual void Update(byte input)
		{
			this.digest.Update(input);
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x001240F7 File Offset: 0x001222F7
		public virtual void BlockUpdate(byte[] input, int inOff, int length)
		{
			this.digest.BlockUpdate(input, inOff, length);
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x00124108 File Offset: 0x00122308
		public virtual byte[] GenerateSignature()
		{
			if (!this.forSigning)
			{
				throw new InvalidOperationException("RsaDigestSigner not initialised for signature generation.");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			byte[] array2 = this.DerEncode(array);
			return this.rsaEngine.ProcessBlock(array2, 0, array2.Length);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x00124160 File Offset: 0x00122360
		public virtual bool VerifySignature(byte[] signature)
		{
			if (this.forSigning)
			{
				throw new InvalidOperationException("RsaDigestSigner not initialised for verification");
			}
			byte[] array = new byte[this.digest.GetDigestSize()];
			this.digest.DoFinal(array, 0);
			byte[] array2;
			byte[] array3;
			try
			{
				array2 = this.rsaEngine.ProcessBlock(signature, 0, signature.Length);
				array3 = this.DerEncode(array);
			}
			catch (Exception)
			{
				return false;
			}
			if (array2.Length == array3.Length)
			{
				return Arrays.ConstantTimeAreEqual(array2, array3);
			}
			if (array2.Length == array3.Length - 2)
			{
				int num = array2.Length - array.Length - 2;
				int num2 = array3.Length - array.Length - 2;
				byte[] array4 = array3;
				int num3 = 1;
				array4[num3] -= 2;
				byte[] array5 = array3;
				int num4 = 3;
				array5[num4] -= 2;
				int num5 = 0;
				for (int i = 0; i < array.Length; i++)
				{
					num5 |= (int)(array2[num + i] ^ array3[num2 + i]);
				}
				for (int j = 0; j < num; j++)
				{
					num5 |= (int)(array2[j] ^ array3[j]);
				}
				return num5 == 0;
			}
			return false;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x00124270 File Offset: 0x00122470
		public virtual void Reset()
		{
			this.digest.Reset();
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x0012427D File Offset: 0x0012247D
		private byte[] DerEncode(byte[] hash)
		{
			if (this.algId == null)
			{
				return hash;
			}
			return new DigestInfo(this.algId, hash).GetDerEncoded();
		}

		// Token: 0x04001E02 RID: 7682
		private readonly IAsymmetricBlockCipher rsaEngine;

		// Token: 0x04001E03 RID: 7683
		private readonly AlgorithmIdentifier algId;

		// Token: 0x04001E04 RID: 7684
		private readonly IDigest digest;

		// Token: 0x04001E05 RID: 7685
		private bool forSigning;

		// Token: 0x04001E06 RID: 7686
		private static readonly IDictionary oidMap = Platform.CreateHashtable();
	}
}
