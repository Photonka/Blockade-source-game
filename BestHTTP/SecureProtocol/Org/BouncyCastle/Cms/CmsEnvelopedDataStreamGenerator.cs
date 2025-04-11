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
	// Token: 0x020005DB RID: 1499
	public class CmsEnvelopedDataStreamGenerator : CmsEnvelopedGenerator
	{
		// Token: 0x0600395A RID: 14682 RVA: 0x00168E67 File Offset: 0x00167067
		public CmsEnvelopedDataStreamGenerator()
		{
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x00168E6F File Offset: 0x0016706F
		public CmsEnvelopedDataStreamGenerator(SecureRandom rand) : base(rand)
		{
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x001697BD File Offset: 0x001679BD
		public void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x001697C6 File Offset: 0x001679C6
		public void SetBerEncodeRecipients(bool berEncodeRecipientSet)
		{
			this._berEncodeRecipientSet = berEncodeRecipientSet;
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x0600395E RID: 14686 RVA: 0x001697CF File Offset: 0x001679CF
		private DerInteger Version
		{
			get
			{
				return new DerInteger((this._originatorInfo != null || this._unprotectedAttributes != null) ? 2 : 0);
			}
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x001697EC File Offset: 0x001679EC
		private Stream Open(Stream outStream, string encryptionOid, CipherKeyGenerator keyGen)
		{
			byte[] array = keyGen.GenerateKey();
			KeyParameter keyParameter = ParameterUtilities.CreateKeyParameter(encryptionOid, array);
			Asn1Encodable asn1Params = this.GenerateAsn1Parameters(encryptionOid, array);
			ICipherParameters cipherParameters;
			AlgorithmIdentifier algorithmIdentifier = this.GetAlgorithmIdentifier(encryptionOid, keyParameter, asn1Params, out cipherParameters);
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
			return this.Open(outStream, algorithmIdentifier, cipherParameters, asn1EncodableVector);
		}

		// Token: 0x06003960 RID: 14688 RVA: 0x001698DC File Offset: 0x00167ADC
		private Stream Open(Stream outStream, AlgorithmIdentifier encAlgID, ICipherParameters cipherParameters, Asn1EncodableVector recipientInfos)
		{
			Stream result;
			try
			{
				BerSequenceGenerator berSequenceGenerator = new BerSequenceGenerator(outStream);
				berSequenceGenerator.AddObject(CmsObjectIdentifiers.EnvelopedData);
				BerSequenceGenerator berSequenceGenerator2 = new BerSequenceGenerator(berSequenceGenerator.GetRawOutputStream(), 0, true);
				berSequenceGenerator2.AddObject(this.Version);
				Stream rawOutputStream = berSequenceGenerator2.GetRawOutputStream();
				Asn1Generator asn1Generator = this._berEncodeRecipientSet ? new BerSetGenerator(rawOutputStream) : new DerSetGenerator(rawOutputStream);
				foreach (object obj in recipientInfos)
				{
					Asn1Encodable obj2 = (Asn1Encodable)obj;
					asn1Generator.AddObject(obj2);
				}
				asn1Generator.Close();
				BerSequenceGenerator berSequenceGenerator3 = new BerSequenceGenerator(rawOutputStream);
				berSequenceGenerator3.AddObject(CmsObjectIdentifiers.Data);
				berSequenceGenerator3.AddObject(encAlgID);
				Stream stream = CmsUtilities.CreateBerOctetOutputStream(berSequenceGenerator3.GetRawOutputStream(), 0, false, this._bufferSize);
				IBufferedCipher cipher = CipherUtilities.GetCipher(encAlgID.Algorithm);
				cipher.Init(true, new ParametersWithRandom(cipherParameters, this.rand));
				CipherStream outStream2 = new CipherStream(stream, null, cipher);
				result = new CmsEnvelopedDataStreamGenerator.CmsEnvelopedDataOutputStream(this, outStream2, berSequenceGenerator, berSequenceGenerator2, berSequenceGenerator3);
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

		// Token: 0x06003961 RID: 14689 RVA: 0x00169A48 File Offset: 0x00167C48
		public Stream Open(Stream outStream, string encryptionOid)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keyGenerator.DefaultStrength));
			return this.Open(outStream, encryptionOid, keyGenerator);
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x00169A7C File Offset: 0x00167C7C
		public Stream Open(Stream outStream, string encryptionOid, int keySize)
		{
			CipherKeyGenerator keyGenerator = GeneratorUtilities.GetKeyGenerator(encryptionOid);
			keyGenerator.Init(new KeyGenerationParameters(this.rand, keySize));
			return this.Open(outStream, encryptionOid, keyGenerator);
		}

		// Token: 0x040024A8 RID: 9384
		private object _originatorInfo;

		// Token: 0x040024A9 RID: 9385
		private object _unprotectedAttributes;

		// Token: 0x040024AA RID: 9386
		private int _bufferSize;

		// Token: 0x040024AB RID: 9387
		private bool _berEncodeRecipientSet;

		// Token: 0x0200095B RID: 2395
		private class CmsEnvelopedDataOutputStream : BaseOutputStream
		{
			// Token: 0x06004EEA RID: 20202 RVA: 0x001B6B5F File Offset: 0x001B4D5F
			public CmsEnvelopedDataOutputStream(CmsEnvelopedGenerator outer, CipherStream outStream, BerSequenceGenerator cGen, BerSequenceGenerator envGen, BerSequenceGenerator eiGen)
			{
				this._outer = outer;
				this._out = outStream;
				this._cGen = cGen;
				this._envGen = envGen;
				this._eiGen = eiGen;
			}

			// Token: 0x06004EEB RID: 20203 RVA: 0x001B6B8C File Offset: 0x001B4D8C
			public override void WriteByte(byte b)
			{
				this._out.WriteByte(b);
			}

			// Token: 0x06004EEC RID: 20204 RVA: 0x001B6B9A File Offset: 0x001B4D9A
			public override void Write(byte[] bytes, int off, int len)
			{
				this._out.Write(bytes, off, len);
			}

			// Token: 0x06004EED RID: 20205 RVA: 0x001B6BAC File Offset: 0x001B4DAC
			public override void Close()
			{
				Platform.Dispose(this._out);
				this._eiGen.Close();
				if (this._outer.unprotectedAttributeGenerator != null)
				{
					Asn1Set obj = new BerSet(this._outer.unprotectedAttributeGenerator.GetAttributes(Platform.CreateHashtable()).ToAsn1EncodableVector());
					this._envGen.AddObject(new DerTaggedObject(false, 1, obj));
				}
				this._envGen.Close();
				this._cGen.Close();
				base.Close();
			}

			// Token: 0x040035B4 RID: 13748
			private readonly CmsEnvelopedGenerator _outer;

			// Token: 0x040035B5 RID: 13749
			private readonly CipherStream _out;

			// Token: 0x040035B6 RID: 13750
			private readonly BerSequenceGenerator _cGen;

			// Token: 0x040035B7 RID: 13751
			private readonly BerSequenceGenerator _envGen;

			// Token: 0x040035B8 RID: 13752
			private readonly BerSequenceGenerator _eiGen;
		}
	}
}
