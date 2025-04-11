using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cms;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Security;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Cms
{
	// Token: 0x020005CF RID: 1487
	public class CmsAuthenticatedDataStreamGenerator : CmsAuthenticatedGenerator
	{
		// Token: 0x06003924 RID: 14628 RVA: 0x00168759 File Offset: 0x00166959
		public CmsAuthenticatedDataStreamGenerator()
		{
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x00168761 File Offset: 0x00166961
		public CmsAuthenticatedDataStreamGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x00168B99 File Offset: 0x00166D99
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x00168BA2 File Offset: 0x00166DA2
		public void SetBerEncodeRecipients(bool berEncodeRecipientSet)
		{
			this._berEncodeRecipientSet = berEncodeRecipientSet;
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x00168BAC File Offset: 0x00166DAC
		private Stream Open(Stream outStr, string macOid, CipherKeyGenerator keyGen)
		{
			byte[] array = keyGen.GenerateKey();
			KeyParameter keyParameter = ParameterUtilities.CreateKeyParameter(macOid, array);
			Asn1Encodable asn1Params = this.GenerateAsn1Parameters(macOid, array);
			ICipherParameters cipherParameters;
			AlgorithmIdentifier algorithmIdentifier = this.GetAlgorithmIdentifier(macOid, keyParameter, asn1Params, out cipherParameters);
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in this.recipientInfoGenerators)
			{
				RecipientInfoGenerator recipientInfoGenerator = (RecipientInfoGenerator)obj;
				try
				{
					asn1EncodableVector.Add(new Asn1Encodable[]
					{
						recipientInfoGenerator.Generate(keyParameter, this.rand)
					});
				}
				catch (InvalidKeyException e)
				{
					throw new CmsException("key inappropriate for algorithm.", e);
				}
				catch (GeneralSecurityException e2)
				{
					throw new CmsException("error making encrypted content.", e2);
				}
			}
			return this.Open(outStr, algorithmIdentifier, keyParameter, asn1EncodableVector);
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x00168C9C File Offset: 0x00166E9C
		protected Stream Open(Stream outStr, AlgorithmIdentifier macAlgId, ICipherParameters cipherParameters, Asn1EncodableVector recipientInfos)
		{
			Stream result;
			try
			{
				BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStr);
				berSequenceGenerator.AddObject(CmsObjectIdentifiers.AuthenticatedData);
				BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
				berSequenceGenerator2.AddObject(new DerInteger(AuthenticatedData.CalculateVersion(null)));
				Stream rawOutputStream = berSequenceGenerator2.GetRawOutputStream();
				Asn1Generator asn1Generator = this._berEncodeRecipientSet ? new BerSetGenerator(rawOutputStream) : new DerSetGenerator(rawOutputStream);
				foreach (object obj in recipientInfos)
				{
					Asn1Encodable obj2 = (Asn1Encodable)obj;
					asn1Generator.AddObject(obj2);
				}
				asn1Generator.Close();
				berSequenceGenerator2.AddObject(macAlgId);
				BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(rawOutputStream);
				berSequenceGenerator3.AddObject(CmsObjectIdentifiers.Data);
				Stream output = CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, false, this._bufferSize);
				IMac mac = MacUtilities.GetMac(macAlgId.Algorithm);
				mac.Init(cipherParameters);
				result = new CmsAuthenticatedDataStreamGenerator.CmsAuthenticatedDataOutputStream(new TeeOutputStream(output, new MacSink(mac)), mac, berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
			}
			catch (SecurityUtilityException e)
			{
				throw new CmsException("couldn't create cipher.", e);
			}
			catch (InvalidKeyException e2)
			{
				throw new CmsException("key invalid in message.", e2);
			}
			catch (IOException e3)
			{
				throw new CmsException("exception decoding algorithm parameters.", e3);
			}
			return result;
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x00168E04 File Offset: 0x00167004
		public Stream Open(Stream outStr, string encryptionOid)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keyGenerator.DefaultStrength));
			return this.Open(outStr, encryptionOid, keyGenerator);
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x00168E38 File Offset: 0x00167038
		public Stream Open(Stream outStr, string encryptionOid, int keySize)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keySize));
			return this.Open(outStr, encryptionOid, keyGenerator);
		}

		// Token: 0x0400248A RID: 9354
		private int _bufferSize;

		// Token: 0x0400248B RID: 9355
		private bool _berEncodeRecipientSet;

		// Token: 0x02000958 RID: 2392
		private class CmsAuthenticatedDataOutputStream : BaseOutputStream
		{
			// Token: 0x06004EDE RID: 20190 RVA: 0x001B6A19 File Offset: 0x001B4C19
			public CmsAuthenticatedDataOutputStream(Stream macStream, IMac mac, BerSequenceGenerator cGen, BerSequenceGenerator authGen, BerSequenceGenerator eiGen)
			{
				this.macStream = macStream;
				this.mac = mac;
				this.cGen = cGen;
				this.authGen = authGen;
				this.eiGen = eiGen;
			}

			// Token: 0x06004EDF RID: 20191 RVA: 0x001B6A46 File Offset: 0x001B4C46
			public override void WriteByte(byte b)
			{
				this.macStream.WriteByte(b);
			}

			// Token: 0x06004EE0 RID: 20192 RVA: 0x001B6A54 File Offset: 0x001B4C54
			public override void Write(byte[] bytes, int off, int len)
			{
				this.macStream.Write(bytes, off, len);
			}

			// Token: 0x06004EE1 RID: 20193 RVA: 0x001B6A64 File Offset: 0x001B4C64
			public override void Close()
			{
				Platform.Dispose(this.macStream);
				this.eiGen.Close();
				byte[] str = MacUtilities.DoFinal(this.mac);
				this.authGen.AddObject(new DerOctetString(str));
				this.authGen.Close();
				this.cGen.Close();
				base.Close();
			}

			// Token: 0x040035AA RID: 13738
			private readonly Stream macStream;

			// Token: 0x040035AB RID: 13739
			private readonly IMac mac;

			// Token: 0x040035AC RID: 13740
			private readonly BerSequenceGenerator cGen;

			// Token: 0x040035AD RID: 13741
			private readonly BerSequenceGenerator authGen;

			// Token: 0x040035AE RID: 13742
			private readonly BerSequenceGenerator eiGen;
		}
	}
}
