using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BestHTTP.Extensions;
using BestHTTP.WebSocket.Extensions;

namespace BestHTTP.WebSocket.Frames
{
	// Token: 0x020001AF RID: 431
	public struct WebSocketFrameReader
	{
		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x000A04E6 File Offset: 0x0009E6E6
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x000A04EE File Offset: 0x0009E6EE
		public byte Header { get; private set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x000A04F7 File Offset: 0x0009E6F7
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x000A04FF File Offset: 0x0009E6FF
		public bool IsFinal { get; private set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x000A0508 File Offset: 0x0009E708
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x000A0510 File Offset: 0x0009E710
		public WebSocketFrameTypes Type { get; private set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x000A0519 File Offset: 0x0009E719
		// (set) Token: 0x06001031 RID: 4145 RVA: 0x000A0521 File Offset: 0x0009E721
		public bool HasMask { get; private set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x000A052A File Offset: 0x0009E72A
		// (set) Token: 0x06001033 RID: 4147 RVA: 0x000A0532 File Offset: 0x0009E732
		public ulong Length { get; private set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x000A053B File Offset: 0x0009E73B
		// (set) Token: 0x06001035 RID: 4149 RVA: 0x000A0543 File Offset: 0x0009E743
		public byte[] Data { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x000A054C File Offset: 0x0009E74C
		// (set) Token: 0x06001037 RID: 4151 RVA: 0x000A0554 File Offset: 0x0009E754
		public string DataAsText { get; private set; }

		// Token: 0x06001038 RID: 4152 RVA: 0x000A0560 File Offset: 0x0009E760
		internal void Read(Stream stream)
		{
			this.Header = this.ReadByte(stream);
			this.IsFinal = ((this.Header & 128) > 0);
			this.Type = (WebSocketFrameTypes)(this.Header & 15);
			byte b = this.ReadByte(stream);
			this.HasMask = ((b & 128) > 0);
			this.Length = (ulong)((long)(b & 127));
			if (this.Length == 126UL)
			{
				byte[] array = VariableSizedBufferPool.Get(2L, true);
				stream.ReadBuffer(array, 2);
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(array, 0, 2);
				}
				this.Length = (ulong)BitConverter.ToUInt16(array, 0);
				VariableSizedBufferPool.Release(array);
			}
			else if (this.Length == 127UL)
			{
				byte[] array2 = VariableSizedBufferPool.Get(8L, true);
				stream.ReadBuffer(array2, 8);
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(array2, 0, 8);
				}
				this.Length = BitConverter.ToUInt64(array2, 0);
				VariableSizedBufferPool.Release(array2);
			}
			byte[] array3 = null;
			if (this.HasMask)
			{
				array3 = VariableSizedBufferPool.Get(4L, true);
				if (stream.Read(array3, 0, 4) < array3.Length)
				{
					throw ExceptionHelper.ServerClosedTCPStream();
				}
			}
			if (this.Type == WebSocketFrameTypes.Text || this.Type == WebSocketFrameTypes.Continuation)
			{
				this.Data = VariableSizedBufferPool.Get((long)this.Length, true);
			}
			else if (this.Length == 0UL)
			{
				this.Data = VariableSizedBufferPool.NoData;
			}
			else
			{
				this.Data = new byte[this.Length];
			}
			if (this.Length == 0UL)
			{
				return;
			}
			uint num = 0U;
			for (;;)
			{
				int num2 = stream.Read(this.Data, (int)num, (int)(this.Length - (ulong)num));
				if (num2 <= 0)
				{
					break;
				}
				num += (uint)num2;
				if ((ulong)num >= this.Length)
				{
					goto Block_11;
				}
			}
			throw ExceptionHelper.ServerClosedTCPStream();
			Block_11:
			if (this.HasMask)
			{
				uint num3 = 0U;
				while ((ulong)num3 < this.Length)
				{
					this.Data[(int)num3] = (this.Data[(int)num3] ^ array3[(int)(num3 % 4U)]);
					num3 += 1U;
				}
				VariableSizedBufferPool.Release(array3);
			}
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x000A0735 File Offset: 0x0009E935
		private byte ReadByte(Stream stream)
		{
			int num = stream.ReadByte();
			if (num < 0)
			{
				throw ExceptionHelper.ServerClosedTCPStream();
			}
			return (byte)num;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x000A0748 File Offset: 0x0009E948
		public void Assemble(List<WebSocketFrameReader> fragments)
		{
			fragments.Add(this);
			ulong num = 0UL;
			for (int i = 0; i < fragments.Count; i++)
			{
				num += fragments[i].Length;
			}
			byte[] array = (fragments[0].Type == WebSocketFrameTypes.Text) ? VariableSizedBufferPool.Get((long)num, true) : new byte[num];
			ulong num2 = 0UL;
			for (int j = 0; j < fragments.Count; j++)
			{
				Array.Copy(fragments[j].Data, 0, array, (int)num2, (int)fragments[j].Length);
				VariableSizedBufferPool.Release(fragments[j].Data);
				num2 += fragments[j].Length;
			}
			this.Type = fragments[0].Type;
			this.Header = fragments[0].Header;
			this.Length = num;
			this.Data = array;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x000A0854 File Offset: 0x0009EA54
		public void DecodeWithExtensions(WebSocket webSocket)
		{
			if (webSocket.Extensions != null)
			{
				for (int i = 0; i < webSocket.Extensions.Length; i++)
				{
					IExtension extension = webSocket.Extensions[i];
					if (extension != null)
					{
						byte[] array = extension.Decode(this.Header, this.Data, (int)this.Length);
						if (this.Data != array)
						{
							VariableSizedBufferPool.Release(this.Data);
							this.Data = array;
							this.Length = (ulong)((long)array.Length);
						}
					}
				}
			}
			if (this.Type == WebSocketFrameTypes.Text && this.Data != null)
			{
				this.DataAsText = Encoding.UTF8.GetString(this.Data, 0, (int)this.Length);
				VariableSizedBufferPool.Release(this.Data);
				this.Data = null;
			}
		}
	}
}
