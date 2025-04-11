using System;
using BestHTTP.Extensions;
using BestHTTP.WebSocket.Extensions;

namespace BestHTTP.WebSocket.Frames
{
	// Token: 0x020001AE RID: 430
	public sealed class WebSocketFrame
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x000A012C File Offset: 0x0009E32C
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x000A0134 File Offset: 0x0009E334
		public WebSocketFrameTypes Type { get; private set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x000A013D File Offset: 0x0009E33D
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x000A0145 File Offset: 0x0009E345
		public bool IsFinal { get; private set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x000A014E File Offset: 0x0009E34E
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x000A0156 File Offset: 0x0009E356
		public byte Header { get; private set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x000A015F File Offset: 0x0009E35F
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x000A0167 File Offset: 0x0009E367
		public byte[] Data { get; private set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x000A0170 File Offset: 0x0009E370
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x000A0178 File Offset: 0x0009E378
		public int DataLength { get; private set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x000A0181 File Offset: 0x0009E381
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x000A0189 File Offset: 0x0009E389
		public bool UseExtensions { get; private set; }

		// Token: 0x06001024 RID: 4132 RVA: 0x000A0192 File Offset: 0x0009E392
		public WebSocketFrame(WebSocket webSocket, WebSocketFrameTypes type, byte[] data) : this(webSocket, type, data, true)
		{
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000A019E File Offset: 0x0009E39E
		public WebSocketFrame(WebSocket webSocket, WebSocketFrameTypes type, byte[] data, bool useExtensions) : this(webSocket, type, data, 0UL, (ulong)((data != null) ? ((long)data.Length) : 0L), true, useExtensions)
		{
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000A01B9 File Offset: 0x0009E3B9
		public WebSocketFrame(WebSocket webSocket, WebSocketFrameTypes type, byte[] data, bool isFinal, bool useExtensions) : this(webSocket, type, data, 0UL, (ulong)((data != null) ? ((long)data.Length) : 0L), isFinal, useExtensions)
		{
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x000A01D8 File Offset: 0x0009E3D8
		public WebSocketFrame(WebSocket webSocket, WebSocketFrameTypes type, byte[] data, ulong pos, ulong length, bool isFinal, bool useExtensions)
		{
			this.Type = type;
			this.IsFinal = isFinal;
			this.UseExtensions = useExtensions;
			this.DataLength = (int)length;
			if (data != null)
			{
				this.Data = VariableSizedBufferPool.Get((long)this.DataLength, true);
				Array.Copy(data, (int)pos, this.Data, 0, this.DataLength);
			}
			else
			{
				data = VariableSizedBufferPool.NoData;
			}
			byte b = this.IsFinal ? 128 : 0;
			this.Header = (b | (byte)this.Type);
			if (this.UseExtensions && webSocket != null && webSocket.Extensions != null)
			{
				for (int i = 0; i < webSocket.Extensions.Length; i++)
				{
					IExtension extension = webSocket.Extensions[i];
					if (extension != null)
					{
						this.Header |= extension.GetFrameHeader(this, this.Header);
						byte[] array = extension.Encode(this);
						if (array != this.Data)
						{
							VariableSizedBufferPool.Release(this.Data);
							this.Data = array;
							this.DataLength = array.Length;
						}
					}
				}
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x000A02DC File Offset: 0x0009E4DC
		public RawFrameData Get()
		{
			if (this.Data == null)
			{
				this.Data = VariableSizedBufferPool.NoData;
			}
			RawFrameData result;
			using (BufferPoolMemoryStream bufferPoolMemoryStream = new BufferPoolMemoryStream(this.DataLength + 9))
			{
				bufferPoolMemoryStream.WriteByte(this.Header);
				if (this.DataLength < 126)
				{
					bufferPoolMemoryStream.WriteByte(128 | (byte)this.DataLength);
				}
				else if (this.DataLength < 65535)
				{
					bufferPoolMemoryStream.WriteByte(254);
					byte[] bytes = BitConverter.GetBytes((ushort)this.DataLength);
					if (BitConverter.IsLittleEndian)
					{
						Array.Reverse(bytes, 0, bytes.Length);
					}
					bufferPoolMemoryStream.Write(bytes, 0, bytes.Length);
				}
				else
				{
					bufferPoolMemoryStream.WriteByte(byte.MaxValue);
					byte[] bytes2 = BitConverter.GetBytes((ulong)((long)this.DataLength));
					if (BitConverter.IsLittleEndian)
					{
						Array.Reverse(bytes2, 0, bytes2.Length);
					}
					bufferPoolMemoryStream.Write(bytes2, 0, bytes2.Length);
				}
				byte[] bytes3 = BitConverter.GetBytes(this.GetHashCode());
				bufferPoolMemoryStream.Write(bytes3, 0, bytes3.Length);
				for (int i = 0; i < this.DataLength; i++)
				{
					bufferPoolMemoryStream.WriteByte(this.Data[i] ^ bytes3[i % 4]);
				}
				result = new RawFrameData(bufferPoolMemoryStream.ToArray(true), (int)bufferPoolMemoryStream.Length);
			}
			return result;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x000A0424 File Offset: 0x0009E624
		public WebSocketFrame[] Fragment(ushort maxFragmentSize)
		{
			if (this.Data == null)
			{
				return null;
			}
			if (this.Type != WebSocketFrameTypes.Binary && this.Type != WebSocketFrameTypes.Text)
			{
				return null;
			}
			if (this.DataLength <= (int)maxFragmentSize)
			{
				return null;
			}
			this.IsFinal = false;
			this.Header &= 127;
			int num = this.DataLength / (int)maxFragmentSize + ((this.DataLength % (int)maxFragmentSize == 0) ? -1 : 0);
			WebSocketFrame[] array = new WebSocketFrame[num];
			ulong num3;
			for (ulong num2 = (ulong)maxFragmentSize; num2 < (ulong)((long)this.DataLength); num2 += num3)
			{
				num3 = Math.Min((ulong)maxFragmentSize, (ulong)((long)this.DataLength - (long)num2));
				array[array.Length - num--] = new WebSocketFrame(null, WebSocketFrameTypes.Continuation, this.Data, num2, num3, num2 + num3 >= (ulong)((long)this.DataLength), false);
			}
			this.DataLength = (int)maxFragmentSize;
			return array;
		}
	}
}
