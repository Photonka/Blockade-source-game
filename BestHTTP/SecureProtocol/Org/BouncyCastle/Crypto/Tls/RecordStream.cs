using System;
using System.IO;
using BestHTTP.Extensions;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200042B RID: 1067
	internal sealed class RecordStream
	{
		// Token: 0x06002A77 RID: 10871 RVA: 0x00115198 File Offset: 0x00113398
		internal RecordStream(TlsProtocol handler, Stream input, Stream output)
		{
			this.mHandler = handler;
			this.mInput = input;
			this.mOutput = output;
			this.mReadCompression = new TlsNullCompression();
			this.mWriteCompression = this.mReadCompression;
			this.mHandshakeHashUpdater = new RecordStream.HandshakeHashUpdateStream(this);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x0011520B File Offset: 0x0011340B
		internal void Init(TlsContext context)
		{
			this.mReadCipher = new TlsNullCipher(context);
			this.mWriteCipher = this.mReadCipher;
			this.mHandshakeHash = new DeferredHash();
			this.mHandshakeHash.Init(context);
			this.SetPlaintextLimit(16384);
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x00115247 File Offset: 0x00113447
		internal int GetPlaintextLimit()
		{
			return this.mPlaintextLimit;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x0011524F File Offset: 0x0011344F
		internal void SetPlaintextLimit(int plaintextLimit)
		{
			this.mPlaintextLimit = plaintextLimit;
			this.mCompressedLimit = this.mPlaintextLimit + 1024;
			this.mCiphertextLimit = this.mCompressedLimit + 1024;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002A7B RID: 10875 RVA: 0x0011527C File Offset: 0x0011347C
		// (set) Token: 0x06002A7C RID: 10876 RVA: 0x00115284 File Offset: 0x00113484
		internal ProtocolVersion ReadVersion
		{
			get
			{
				return this.mReadVersion;
			}
			set
			{
				this.mReadVersion = value;
			}
		}

		// Token: 0x06002A7D RID: 10877 RVA: 0x0011528D File Offset: 0x0011348D
		internal void SetWriteVersion(ProtocolVersion writeVersion)
		{
			this.mWriteVersion = writeVersion;
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x00115296 File Offset: 0x00113496
		internal void SetRestrictReadVersion(bool enabled)
		{
			this.mRestrictReadVersion = enabled;
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x0011529F File Offset: 0x0011349F
		internal void SetPendingConnectionState(TlsCompression tlsCompression, TlsCipher tlsCipher)
		{
			this.mPendingCompression = tlsCompression;
			this.mPendingCipher = tlsCipher;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x001152AF File Offset: 0x001134AF
		internal void SentWriteCipherSpec()
		{
			if (this.mPendingCompression == null || this.mPendingCipher == null)
			{
				throw new TlsFatalAlert(40);
			}
			this.mWriteCompression = this.mPendingCompression;
			this.mWriteCipher = this.mPendingCipher;
			this.mWriteSeqNo = new RecordStream.SequenceNumber();
		}

		// Token: 0x06002A81 RID: 10881 RVA: 0x001152EC File Offset: 0x001134EC
		internal void ReceivedReadCipherSpec()
		{
			if (this.mPendingCompression == null || this.mPendingCipher == null)
			{
				throw new TlsFatalAlert(40);
			}
			this.mReadCompression = this.mPendingCompression;
			this.mReadCipher = this.mPendingCipher;
			this.mReadSeqNo = new RecordStream.SequenceNumber();
		}

		// Token: 0x06002A82 RID: 10882 RVA: 0x0011532C File Offset: 0x0011352C
		internal void FinaliseHandshake()
		{
			if (this.mReadCompression != this.mPendingCompression || this.mWriteCompression != this.mPendingCompression || this.mReadCipher != this.mPendingCipher || this.mWriteCipher != this.mPendingCipher)
			{
				throw new TlsFatalAlert(40);
			}
			this.mPendingCompression = null;
			this.mPendingCipher = null;
		}

		// Token: 0x06002A83 RID: 10883 RVA: 0x00115388 File Offset: 0x00113588
		internal void CheckRecordHeader(byte[] recordHeader)
		{
			RecordStream.CheckType(TlsUtilities.ReadUint8(recordHeader, 0), 10);
			if (!this.mRestrictReadVersion)
			{
				if (((long)TlsUtilities.ReadVersionRaw(recordHeader, 1) & (long)((ulong)-256)) != 768L)
				{
					throw new TlsFatalAlert(47);
				}
			}
			else
			{
				ProtocolVersion protocolVersion = TlsUtilities.ReadVersion(recordHeader, 1);
				if (this.mReadVersion != null && !protocolVersion.Equals(this.mReadVersion))
				{
					throw new TlsFatalAlert(47);
				}
			}
			RecordStream.CheckLength(TlsUtilities.ReadUint16(recordHeader, 3), this.mCiphertextLimit, 22);
		}

		// Token: 0x06002A84 RID: 10884 RVA: 0x00115404 File Offset: 0x00113604
		internal bool ReadRecord()
		{
			byte[] array = TlsUtilities.ReadAllOrNothing(5, this.mInput);
			if (array == null)
			{
				return false;
			}
			byte b = TlsUtilities.ReadUint8(array, 0);
			RecordStream.CheckType(b, 10);
			if (!this.mRestrictReadVersion)
			{
				if (((long)TlsUtilities.ReadVersionRaw(array, 1) & (long)((ulong)-256)) != 768L)
				{
					throw new TlsFatalAlert(47);
				}
			}
			else
			{
				ProtocolVersion protocolVersion = TlsUtilities.ReadVersion(array, 1);
				if (this.mReadVersion == null)
				{
					this.mReadVersion = protocolVersion;
				}
				else if (!protocolVersion.Equals(this.mReadVersion))
				{
					throw new TlsFatalAlert(47);
				}
			}
			int num = TlsUtilities.ReadUint16(array, 3);
			RecordStream.CheckLength(num, this.mCiphertextLimit, 22);
			byte[] array2 = this.DecodeAndVerify(b, this.mInput, num);
			this.mHandler.ProcessRecord(b, array2, 0, array2.Length);
			return true;
		}

		// Token: 0x06002A85 RID: 10885 RVA: 0x001154C4 File Offset: 0x001136C4
		internal byte[] DecodeAndVerify(byte type, Stream input, int len)
		{
			byte[] array = TlsUtilities.ReadFully(len, input);
			long seqNo = this.mReadSeqNo.NextValue(10);
			byte[] array2 = this.mReadCipher.DecodeCiphertext(seqNo, type, array, 0, array.Length);
			RecordStream.CheckLength(array2.Length, this.mCompressedLimit, 22);
			Stream stream = this.mReadCompression.Decompress(this.mBuffer);
			if (stream != this.mBuffer)
			{
				stream.Write(array2, 0, array2.Length);
				stream.Flush();
				array2 = this.GetBufferContents();
			}
			RecordStream.CheckLength(array2.Length, this.mPlaintextLimit, 30);
			if (array2.Length < 1 && type != 23)
			{
				throw new TlsFatalAlert(47);
			}
			return array2;
		}

		// Token: 0x06002A86 RID: 10886 RVA: 0x00115564 File Offset: 0x00113764
		internal void WriteRecord(byte type, byte[] plaintext, int plaintextOffset, int plaintextLength)
		{
			if (this.mWriteVersion == null)
			{
				return;
			}
			RecordStream.CheckType(type, 80);
			RecordStream.CheckLength(plaintextLength, this.mPlaintextLimit, 80);
			if (plaintextLength < 1 && type != 23)
			{
				throw new TlsFatalAlert(80);
			}
			Stream stream = this.mWriteCompression.Compress(this.mBuffer);
			long seqNo = this.mWriteSeqNo.NextValue(80);
			byte[] array;
			if (stream == this.mBuffer)
			{
				array = this.mWriteCipher.EncodePlaintext(seqNo, type, plaintext, plaintextOffset, plaintextLength);
			}
			else
			{
				stream.Write(plaintext, plaintextOffset, plaintextLength);
				stream.Flush();
				byte[] bufferContents = this.GetBufferContents();
				RecordStream.CheckLength(bufferContents.Length, plaintextLength + 1024, 80);
				array = this.mWriteCipher.EncodePlaintext(seqNo, type, bufferContents, 0, bufferContents.Length);
			}
			RecordStream.CheckLength(array.Length, this.mCiphertextLimit, 80);
			int num = array.Length + 5;
			byte[] array2 = VariableSizedBufferPool.Get((long)num, true);
			TlsUtilities.WriteUint8(type, array2, 0);
			TlsUtilities.WriteVersion(this.mWriteVersion, array2, 1);
			TlsUtilities.WriteUint16(array.Length, array2, 3);
			Array.Copy(array, 0, array2, 5, array.Length);
			this.mOutput.Write(array2, 0, num);
			VariableSizedBufferPool.Release(array2);
			this.mOutput.Flush();
		}

		// Token: 0x06002A87 RID: 10887 RVA: 0x00115690 File Offset: 0x00113890
		internal void NotifyHelloComplete()
		{
			this.mHandshakeHash = this.mHandshakeHash.NotifyPrfDetermined();
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x001156A3 File Offset: 0x001138A3
		internal TlsHandshakeHash HandshakeHash
		{
			get
			{
				return this.mHandshakeHash;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002A89 RID: 10889 RVA: 0x001156AB File Offset: 0x001138AB
		internal Stream HandshakeHashUpdater
		{
			get
			{
				return this.mHandshakeHashUpdater;
			}
		}

		// Token: 0x06002A8A RID: 10890 RVA: 0x001156B3 File Offset: 0x001138B3
		internal TlsHandshakeHash PrepareToFinish()
		{
			TlsHandshakeHash result = this.mHandshakeHash;
			this.mHandshakeHash = this.mHandshakeHash.StopTracking();
			return result;
		}

		// Token: 0x06002A8B RID: 10891 RVA: 0x001156CC File Offset: 0x001138CC
		internal void SafeClose()
		{
			try
			{
				Platform.Dispose(this.mInput);
			}
			catch (IOException)
			{
			}
			try
			{
				Platform.Dispose(this.mOutput);
			}
			catch (IOException)
			{
			}
		}

		// Token: 0x06002A8C RID: 10892 RVA: 0x00115718 File Offset: 0x00113918
		internal void Flush()
		{
			this.mOutput.Flush();
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x00115725 File Offset: 0x00113925
		private byte[] GetBufferContents()
		{
			byte[] result = this.mBuffer.ToArray();
			this.mBuffer.SetLength(0L);
			return result;
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x0011573F File Offset: 0x0011393F
		private static void CheckType(byte type, byte alertDescription)
		{
			if (type - 20 > 3)
			{
				throw new TlsFatalAlert(alertDescription);
			}
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x0011574F File Offset: 0x0011394F
		private static void CheckLength(int length, int limit, byte alertDescription)
		{
			if (length > limit)
			{
				throw new TlsFatalAlert(alertDescription);
			}
		}

		// Token: 0x04001C72 RID: 7282
		private const int DEFAULT_PLAINTEXT_LIMIT = 16384;

		// Token: 0x04001C73 RID: 7283
		internal const int TLS_HEADER_SIZE = 5;

		// Token: 0x04001C74 RID: 7284
		internal const int TLS_HEADER_TYPE_OFFSET = 0;

		// Token: 0x04001C75 RID: 7285
		internal const int TLS_HEADER_VERSION_OFFSET = 1;

		// Token: 0x04001C76 RID: 7286
		internal const int TLS_HEADER_LENGTH_OFFSET = 3;

		// Token: 0x04001C77 RID: 7287
		private TlsProtocol mHandler;

		// Token: 0x04001C78 RID: 7288
		private Stream mInput;

		// Token: 0x04001C79 RID: 7289
		private Stream mOutput;

		// Token: 0x04001C7A RID: 7290
		private TlsCompression mPendingCompression;

		// Token: 0x04001C7B RID: 7291
		private TlsCompression mReadCompression;

		// Token: 0x04001C7C RID: 7292
		private TlsCompression mWriteCompression;

		// Token: 0x04001C7D RID: 7293
		private TlsCipher mPendingCipher;

		// Token: 0x04001C7E RID: 7294
		private TlsCipher mReadCipher;

		// Token: 0x04001C7F RID: 7295
		private TlsCipher mWriteCipher;

		// Token: 0x04001C80 RID: 7296
		private RecordStream.SequenceNumber mReadSeqNo = new RecordStream.SequenceNumber();

		// Token: 0x04001C81 RID: 7297
		private RecordStream.SequenceNumber mWriteSeqNo = new RecordStream.SequenceNumber();

		// Token: 0x04001C82 RID: 7298
		private MemoryStream mBuffer = new MemoryStream();

		// Token: 0x04001C83 RID: 7299
		private TlsHandshakeHash mHandshakeHash;

		// Token: 0x04001C84 RID: 7300
		private readonly BaseOutputStream mHandshakeHashUpdater;

		// Token: 0x04001C85 RID: 7301
		private ProtocolVersion mReadVersion;

		// Token: 0x04001C86 RID: 7302
		private ProtocolVersion mWriteVersion;

		// Token: 0x04001C87 RID: 7303
		private bool mRestrictReadVersion = true;

		// Token: 0x04001C88 RID: 7304
		private int mPlaintextLimit;

		// Token: 0x04001C89 RID: 7305
		private int mCompressedLimit;

		// Token: 0x04001C8A RID: 7306
		private int mCiphertextLimit;

		// Token: 0x0200091F RID: 2335
		private class HandshakeHashUpdateStream : BaseOutputStream
		{
			// Token: 0x06004E1A RID: 19994 RVA: 0x001B30DA File Offset: 0x001B12DA
			public HandshakeHashUpdateStream(RecordStream mOuter)
			{
				this.mOuter = mOuter;
			}

			// Token: 0x06004E1B RID: 19995 RVA: 0x001B30E9 File Offset: 0x001B12E9
			public override void Write(byte[] buf, int off, int len)
			{
				this.mOuter.mHandshakeHash.BlockUpdate(buf, off, len);
			}

			// Token: 0x040034E5 RID: 13541
			private readonly RecordStream mOuter;
		}

		// Token: 0x02000920 RID: 2336
		private class SequenceNumber
		{
			// Token: 0x06004E1C RID: 19996 RVA: 0x001B3100 File Offset: 0x001B1300
			internal long NextValue(byte alertDescription)
			{
				if (this.exhausted)
				{
					throw new TlsFatalAlert(alertDescription);
				}
				long result = this.value;
				long num = this.value + 1L;
				this.value = num;
				if (num == 0L)
				{
					this.exhausted = true;
				}
				return result;
			}

			// Token: 0x040034E6 RID: 13542
			private long value;

			// Token: 0x040034E7 RID: 13543
			private bool exhausted;
		}
	}
}
