using System;
using System.Collections.Generic;
using BestHTTP.SignalRCore.Messages;

namespace BestHTTP.SignalRCore
{
	// Token: 0x020001DB RID: 475
	public interface IProtocol
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060011CC RID: 4556
		TransferModes Type { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060011CD RID: 4557
		IEncoder Encoder { get; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060011CE RID: 4558
		// (set) Token: 0x060011CF RID: 4559
		HubConnection Connection { get; set; }

		// Token: 0x060011D0 RID: 4560
		void ParseMessages(string data, ref List<Message> messages);

		// Token: 0x060011D1 RID: 4561
		void ParseMessages(byte[] data, ref List<Message> messages);

		// Token: 0x060011D2 RID: 4562
		byte[] EncodeMessage(Message message);

		// Token: 0x060011D3 RID: 4563
		object[] GetRealArguments(Type[] argTypes, object[] arguments);

		// Token: 0x060011D4 RID: 4564
		object ConvertTo(Type toType, object obj);
	}
}
