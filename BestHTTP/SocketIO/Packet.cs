using System;
using System.Collections.Generic;
using System.Text;
using BestHTTP.JSON;
using BestHTTP.SocketIO.JsonEncoders;

namespace BestHTTP.SocketIO
{
	// Token: 0x020001BC RID: 444
	public sealed class Packet
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x000A101E File Offset: 0x0009F21E
		// (set) Token: 0x0600107E RID: 4222 RVA: 0x000A1026 File Offset: 0x0009F226
		public TransportEventTypes TransportEvent { get; private set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600107F RID: 4223 RVA: 0x000A102F File Offset: 0x0009F22F
		// (set) Token: 0x06001080 RID: 4224 RVA: 0x000A1037 File Offset: 0x0009F237
		public SocketIOEventTypes SocketIOEvent { get; private set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x000A1040 File Offset: 0x0009F240
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x000A1048 File Offset: 0x0009F248
		public int AttachmentCount { get; private set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x000A1051 File Offset: 0x0009F251
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x000A1059 File Offset: 0x0009F259
		public int Id { get; private set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x000A1062 File Offset: 0x0009F262
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x000A106A File Offset: 0x0009F26A
		public string Namespace { get; private set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x000A1073 File Offset: 0x0009F273
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x000A107B File Offset: 0x0009F27B
		public string Payload { get; private set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x000A1084 File Offset: 0x0009F284
		// (set) Token: 0x0600108A RID: 4234 RVA: 0x000A108C File Offset: 0x0009F28C
		public string EventName { get; private set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x000A1095 File Offset: 0x0009F295
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x000A109D File Offset: 0x0009F29D
		public List<byte[]> Attachments
		{
			get
			{
				return this.attachments;
			}
			set
			{
				this.attachments = value;
				this.AttachmentCount = ((this.attachments != null) ? this.attachments.Count : 0);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600108D RID: 4237 RVA: 0x000A10C2 File Offset: 0x0009F2C2
		public bool HasAllAttachment
		{
			get
			{
				return this.Attachments != null && this.Attachments.Count == this.AttachmentCount;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x000A10E1 File Offset: 0x0009F2E1
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x000A10E9 File Offset: 0x0009F2E9
		public bool IsDecoded { get; private set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x000A10F2 File Offset: 0x0009F2F2
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x000A10FA File Offset: 0x0009F2FA
		public object[] DecodedArgs { get; private set; }

		// Token: 0x06001092 RID: 4242 RVA: 0x000A1103 File Offset: 0x0009F303
		internal Packet()
		{
			this.TransportEvent = TransportEventTypes.Unknown;
			this.SocketIOEvent = SocketIOEventTypes.Unknown;
			this.Payload = string.Empty;
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x000A1124 File Offset: 0x0009F324
		internal Packet(string from)
		{
			this.Parse(from);
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x000A1133 File Offset: 0x0009F333
		public Packet(TransportEventTypes transportEvent, SocketIOEventTypes packetType, string nsp, string payload, int attachment = 0, int id = 0)
		{
			this.TransportEvent = transportEvent;
			this.SocketIOEvent = packetType;
			this.Namespace = nsp;
			this.Payload = payload;
			this.AttachmentCount = attachment;
			this.Id = id;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x000A1168 File Offset: 0x0009F368
		public object[] Decode(IJsonEncoder encoder)
		{
			if (this.IsDecoded || encoder == null)
			{
				return this.DecodedArgs;
			}
			this.IsDecoded = true;
			if (string.IsNullOrEmpty(this.Payload))
			{
				return this.DecodedArgs;
			}
			List<object> list = encoder.Decode(this.Payload);
			if (list != null && list.Count > 0)
			{
				if (this.SocketIOEvent == SocketIOEventTypes.Ack || this.SocketIOEvent == SocketIOEventTypes.BinaryAck)
				{
					this.DecodedArgs = list.ToArray();
				}
				else
				{
					list.RemoveAt(0);
					this.DecodedArgs = list.ToArray();
				}
			}
			return this.DecodedArgs;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x000A11F4 File Offset: 0x0009F3F4
		public string DecodeEventName()
		{
			if (!string.IsNullOrEmpty(this.EventName))
			{
				return this.EventName;
			}
			if (string.IsNullOrEmpty(this.Payload))
			{
				return string.Empty;
			}
			if (this.Payload[0] != '[')
			{
				return string.Empty;
			}
			int num = 1;
			while (this.Payload.Length > num && this.Payload[num] != '"' && this.Payload[num] != '\'')
			{
				num++;
			}
			if (this.Payload.Length <= num)
			{
				return string.Empty;
			}
			int num2;
			num = (num2 = num + 1);
			while (this.Payload.Length > num && this.Payload[num] != '"' && this.Payload[num] != '\'')
			{
				num++;
			}
			if (this.Payload.Length <= num)
			{
				return string.Empty;
			}
			return this.EventName = this.Payload.Substring(num2, num - num2);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x000A12F0 File Offset: 0x0009F4F0
		public string RemoveEventName(bool removeArrayMarks)
		{
			if (string.IsNullOrEmpty(this.Payload))
			{
				return string.Empty;
			}
			if (this.Payload[0] != '[')
			{
				return string.Empty;
			}
			int num = 1;
			while (this.Payload.Length > num && this.Payload[num] != '"' && this.Payload[num] != '\'')
			{
				num++;
			}
			if (this.Payload.Length <= num)
			{
				return string.Empty;
			}
			int num2 = num;
			while (this.Payload.Length > num && this.Payload[num] != ',' && this.Payload[num] != ']')
			{
				num++;
			}
			if (this.Payload.Length <= ++num)
			{
				return string.Empty;
			}
			string text = this.Payload.Remove(num2, num - num2);
			if (removeArrayMarks)
			{
				text = text.Substring(1, text.Length - 2);
			}
			return text;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000A13E2 File Offset: 0x0009F5E2
		public bool ReconstructAttachmentAsIndex()
		{
			return this.PlaceholderReplacer(delegate(string json, Dictionary<string, object> obj)
			{
				int num = Convert.ToInt32(obj["num"]);
				this.Payload = this.Payload.Replace(json, num.ToString());
				this.IsDecoded = false;
			});
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x000A13F6 File Offset: 0x0009F5F6
		public bool ReconstructAttachmentAsBase64()
		{
			return this.HasAllAttachment && this.PlaceholderReplacer(delegate(string json, Dictionary<string, object> obj)
			{
				int index = Convert.ToInt32(obj["num"]);
				this.Payload = this.Payload.Replace(json, string.Format("\"{0}\"", Convert.ToBase64String(this.Attachments[index])));
				this.IsDecoded = false;
			});
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000A1414 File Offset: 0x0009F614
		internal void Parse(string from)
		{
			int num = 0;
			this.TransportEvent = (TransportEventTypes)this.ToInt(from[num++]);
			if (from.Length > num && this.ToInt(from[num]) >= 0)
			{
				this.SocketIOEvent = (SocketIOEventTypes)this.ToInt(from[num++]);
			}
			else
			{
				this.SocketIOEvent = SocketIOEventTypes.Unknown;
			}
			if (this.SocketIOEvent == SocketIOEventTypes.BinaryEvent || this.SocketIOEvent == SocketIOEventTypes.BinaryAck)
			{
				int num2 = from.IndexOf('-', num);
				if (num2 == -1)
				{
					num2 = from.Length;
				}
				int attachmentCount = 0;
				int.TryParse(from.Substring(num, num2 - num), out attachmentCount);
				this.AttachmentCount = attachmentCount;
				num = num2 + 1;
			}
			if (from.Length > num && from[num] == '/')
			{
				int num3 = from.IndexOf(',', num);
				if (num3 == -1)
				{
					num3 = from.Length;
				}
				this.Namespace = from.Substring(num, num3 - num);
				num = num3 + 1;
			}
			else
			{
				this.Namespace = "/";
			}
			if (from.Length > num && this.ToInt(from[num]) >= 0)
			{
				int num4 = num++;
				while (from.Length > num && this.ToInt(from[num]) >= 0)
				{
					num++;
				}
				int id = 0;
				int.TryParse(from.Substring(num4, num - num4), out id);
				this.Id = id;
			}
			if (from.Length > num)
			{
				this.Payload = from.Substring(num);
				return;
			}
			this.Payload = string.Empty;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x000A1584 File Offset: 0x0009F784
		private int ToInt(char ch)
		{
			int num = Convert.ToInt32(ch) - 48;
			if (num < 0 || num > 9)
			{
				return -1;
			}
			return num;
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000A15A8 File Offset: 0x0009F7A8
		internal string Encode()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.TransportEvent == TransportEventTypes.Unknown && this.AttachmentCount > 0)
			{
				this.TransportEvent = TransportEventTypes.Message;
			}
			if (this.TransportEvent != TransportEventTypes.Unknown)
			{
				stringBuilder.Append(((int)this.TransportEvent).ToString());
			}
			if (this.SocketIOEvent == SocketIOEventTypes.Unknown && this.AttachmentCount > 0)
			{
				this.SocketIOEvent = SocketIOEventTypes.BinaryEvent;
			}
			if (this.SocketIOEvent != SocketIOEventTypes.Unknown)
			{
				stringBuilder.Append(((int)this.SocketIOEvent).ToString());
			}
			if (this.SocketIOEvent == SocketIOEventTypes.BinaryEvent || this.SocketIOEvent == SocketIOEventTypes.BinaryAck)
			{
				stringBuilder.Append(this.AttachmentCount.ToString());
				stringBuilder.Append("-");
			}
			bool flag = false;
			if (this.Namespace != "/")
			{
				stringBuilder.Append(this.Namespace);
				flag = true;
			}
			if (this.Id != 0)
			{
				if (flag)
				{
					stringBuilder.Append(",");
					flag = false;
				}
				stringBuilder.Append(this.Id.ToString());
			}
			if (!string.IsNullOrEmpty(this.Payload))
			{
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(this.Payload);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000A16E0 File Offset: 0x0009F8E0
		internal byte[] EncodeBinary()
		{
			if (this.AttachmentCount != 0 || (this.Attachments != null && this.Attachments.Count != 0))
			{
				if (this.Attachments == null)
				{
					throw new ArgumentException("packet.Attachments are null!");
				}
				if (this.AttachmentCount != this.Attachments.Count)
				{
					throw new ArgumentException("packet.AttachmentCount != packet.Attachments.Count. Use the packet.AddAttachment function to add data to a packet!");
				}
			}
			string s = this.Encode();
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			byte[] array = this.EncodeData(bytes, Packet.PayloadTypes.Textual, null);
			if (this.AttachmentCount != 0)
			{
				int num = array.Length;
				List<byte[]> list = new List<byte[]>(this.AttachmentCount);
				int num2 = 0;
				for (int i = 0; i < this.AttachmentCount; i++)
				{
					byte[] array2 = this.EncodeData(this.Attachments[i], Packet.PayloadTypes.Binary, new byte[]
					{
						4
					});
					list.Add(array2);
					num2 += array2.Length;
				}
				Array.Resize<byte>(ref array, array.Length + num2);
				for (int j = 0; j < this.AttachmentCount; j++)
				{
					byte[] array3 = list[j];
					Array.Copy(array3, 0, array, num, array3.Length);
					num += array3.Length;
				}
			}
			return array;
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000A1804 File Offset: 0x0009FA04
		internal void AddAttachmentFromServer(byte[] data, bool copyFull)
		{
			if (data == null || data.Length == 0)
			{
				return;
			}
			if (this.attachments == null)
			{
				this.attachments = new List<byte[]>(this.AttachmentCount);
			}
			if (copyFull)
			{
				this.Attachments.Add(data);
				return;
			}
			byte[] array = new byte[data.Length - 1];
			Array.Copy(data, 1, array, 0, data.Length - 1);
			this.Attachments.Add(array);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000A1868 File Offset: 0x0009FA68
		private byte[] EncodeData(byte[] data, Packet.PayloadTypes type, byte[] afterHeaderData)
		{
			int num = (afterHeaderData != null) ? afterHeaderData.Length : 0;
			string text = (data.Length + num).ToString();
			byte[] array = new byte[text.Length];
			for (int i = 0; i < text.Length; i++)
			{
				array[i] = (byte)char.GetNumericValue(text[i]);
			}
			byte[] array2 = new byte[data.Length + array.Length + 2 + num];
			array2[0] = (byte)type;
			for (int j = 0; j < array.Length; j++)
			{
				array2[1 + j] = array[j];
			}
			int num2 = 1 + array.Length;
			array2[num2++] = byte.MaxValue;
			if (afterHeaderData != null && afterHeaderData.Length != 0)
			{
				Array.Copy(afterHeaderData, 0, array2, num2, afterHeaderData.Length);
				num2 += afterHeaderData.Length;
			}
			Array.Copy(data, 0, array2, num2, data.Length);
			return array2;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000A1934 File Offset: 0x0009FB34
		private bool PlaceholderReplacer(Action<string, Dictionary<string, object>> onFound)
		{
			if (string.IsNullOrEmpty(this.Payload))
			{
				return false;
			}
			for (int i = this.Payload.IndexOf("_placeholder"); i >= 0; i = this.Payload.IndexOf("_placeholder"))
			{
				int num = i;
				while (this.Payload[num] != '{')
				{
					num--;
				}
				int num2 = i;
				while (this.Payload.Length > num2 && this.Payload[num2] != '}')
				{
					num2++;
				}
				if (this.Payload.Length <= num2)
				{
					return false;
				}
				string text = this.Payload.Substring(num, num2 - num + 1);
				bool flag = false;
				Dictionary<string, object> dictionary = Json.Decode(text, ref flag) as Dictionary<string, object>;
				if (!flag)
				{
					return false;
				}
				object obj;
				if (!dictionary.TryGetValue("_placeholder", out obj) || !(bool)obj)
				{
					return false;
				}
				if (!dictionary.TryGetValue("num", out obj))
				{
					return false;
				}
				onFound(text, dictionary);
			}
			return true;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000A1A2D File Offset: 0x0009FC2D
		public override string ToString()
		{
			return this.Payload;
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000A1A38 File Offset: 0x0009FC38
		internal Packet Clone()
		{
			return new Packet(this.TransportEvent, this.SocketIOEvent, this.Namespace, this.Payload, 0, this.Id)
			{
				EventName = this.EventName,
				AttachmentCount = this.AttachmentCount,
				attachments = this.attachments
			};
		}

		// Token: 0x04001366 RID: 4966
		public const string Placeholder = "_placeholder";

		// Token: 0x0400136E RID: 4974
		private List<byte[]> attachments;

		// Token: 0x020008C7 RID: 2247
		private enum PayloadTypes : byte
		{
			// Token: 0x04003357 RID: 13143
			Textual,
			// Token: 0x04003358 RID: 13144
			Binary
		}
	}
}
