using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200041D RID: 1053
	public class HeartbeatMessage
	{
		// Token: 0x06002A3C RID: 10812 RVA: 0x001149F8 File Offset: 0x00112BF8
		public HeartbeatMessage(byte type, byte[] payload, int paddingLength)
		{
			if (!HeartbeatMessageType.IsValid(type))
			{
				throw new ArgumentException("not a valid HeartbeatMessageType value", "type");
			}
			if (payload == null || payload.Length >= 65536)
			{
				throw new ArgumentException("must have length < 2^16", "payload");
			}
			if (paddingLength < 16)
			{
				throw new ArgumentException("must be at least 16", "paddingLength");
			}
			this.mType = type;
			this.mPayload = payload;
			this.mPaddingLength = paddingLength;
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x00114A6C File Offset: 0x00112C6C
		public virtual void Encode(TlsContext context, Stream output)
		{
			TlsUtilities.WriteUint8(this.mType, output);
			TlsUtilities.CheckUint16(this.mPayload.Length);
			TlsUtilities.WriteUint16(this.mPayload.Length, output);
			output.Write(this.mPayload, 0, this.mPayload.Length);
			byte[] array = new byte[this.mPaddingLength];
			context.NonceRandomGenerator.NextBytes(array);
			output.Write(array, 0, array.Length);
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x00114AD8 File Offset: 0x00112CD8
		public static HeartbeatMessage Parse(Stream input)
		{
			byte b = TlsUtilities.ReadUint8(input);
			if (!HeartbeatMessageType.IsValid(b))
			{
				throw new TlsFatalAlert(47);
			}
			int payloadLength = TlsUtilities.ReadUint16(input);
			HeartbeatMessage.PayloadBuffer payloadBuffer = new HeartbeatMessage.PayloadBuffer();
			Streams.PipeAll(input, payloadBuffer);
			byte[] array = payloadBuffer.ToTruncatedByteArray(payloadLength);
			if (array == null)
			{
				return null;
			}
			TlsUtilities.CheckUint16(payloadBuffer.Length);
			int paddingLength = (int)payloadBuffer.Length - array.Length;
			return new HeartbeatMessage(b, array, paddingLength);
		}

		// Token: 0x04001C15 RID: 7189
		protected readonly byte mType;

		// Token: 0x04001C16 RID: 7190
		protected readonly byte[] mPayload;

		// Token: 0x04001C17 RID: 7191
		protected readonly int mPaddingLength;

		// Token: 0x0200091E RID: 2334
		internal class PayloadBuffer : MemoryStream
		{
			// Token: 0x06004E18 RID: 19992 RVA: 0x001B30B0 File Offset: 0x001B12B0
			internal byte[] ToTruncatedByteArray(int payloadLength)
			{
				int num = payloadLength + 16;
				if (this.Length < (long)num)
				{
					return null;
				}
				return Arrays.CopyOf(this.GetBuffer(), payloadLength);
			}
		}
	}
}
