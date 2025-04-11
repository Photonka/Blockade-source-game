using System;
using System.Collections.Generic;

namespace BestHTTP.SignalRCore.Messages
{
	// Token: 0x020001E6 RID: 486
	public sealed class SupportedTransport
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x000A6568 File Offset: 0x000A4768
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x000A6570 File Offset: 0x000A4770
		public string Name { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x000A6579 File Offset: 0x000A4779
		// (set) Token: 0x06001214 RID: 4628 RVA: 0x000A6581 File Offset: 0x000A4781
		public List<string> SupportedFormats { get; private set; }

		// Token: 0x06001215 RID: 4629 RVA: 0x000A658A File Offset: 0x000A478A
		internal SupportedTransport(string transportName, List<string> transferFormats)
		{
			this.Name = transportName;
			this.SupportedFormats = transferFormats;
		}
	}
}
