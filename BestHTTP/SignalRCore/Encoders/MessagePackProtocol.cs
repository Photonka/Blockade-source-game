using System;
using System.Collections.Generic;
using BestHTTP.SignalRCore.Messages;

namespace BestHTTP.SignalRCore.Encoders
{
	// Token: 0x020001DF RID: 479
	public sealed class MessagePackProtocol : IProtocol
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x0007FCD3 File Offset: 0x0007DED3
		public TransferModes Type
		{
			get
			{
				return TransferModes.Binary;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060011F0 RID: 4592 RVA: 0x000A5E62 File Offset: 0x000A4062
		// (set) Token: 0x060011F1 RID: 4593 RVA: 0x000A5E6A File Offset: 0x000A406A
		public IEncoder Encoder { get; private set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x000A5E73 File Offset: 0x000A4073
		// (set) Token: 0x060011F3 RID: 4595 RVA: 0x000A5E7B File Offset: 0x000A407B
		public HubConnection Connection { get; set; }

		// Token: 0x060011F4 RID: 4596 RVA: 0x000A5E84 File Offset: 0x000A4084
		public MessagePackProtocol()
		{
			this.Encoder = new MessagePackEncoder();
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00096B9B File Offset: 0x00094D9B
		public object ConvertTo(Type toType, object obj)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00096B9B File Offset: 0x00094D9B
		public byte[] EncodeMessage(Message message)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x00096B9B File Offset: 0x00094D9B
		public object[] GetRealArguments(Type[] argTypes, object[] arguments)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x00096B9B File Offset: 0x00094D9B
		public void ParseMessages(string data, ref List<Message> messages)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00096B9B File Offset: 0x00094D9B
		public void ParseMessages(byte[] data, ref List<Message> messages)
		{
			throw new NotImplementedException();
		}
	}
}
