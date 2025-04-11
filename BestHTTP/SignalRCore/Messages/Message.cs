using System;

namespace BestHTTP.SignalRCore.Messages
{
	// Token: 0x020001E5 RID: 485
	public struct Message
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x000A6444 File Offset: 0x000A4644
		public override string ToString()
		{
			switch (this.type)
			{
			case MessageTypes.Invocation:
				return string.Format("[Invocation Id: {0}, Target: '{1}', Argument count: {2}]", this.invocationId, this.target, (this.arguments != null) ? this.arguments.Length : 0);
			case MessageTypes.StreamItem:
				return string.Format("[StreamItem Id: {0}, Item: {1}]", this.invocationId, this.item.ToString());
			case MessageTypes.Completion:
				return string.Format("[Completion Id: {0}, Result: {1}, Error: '{2}']", this.invocationId, this.result, this.error);
			case MessageTypes.StreamInvocation:
				return string.Format("[StreamInvocation Id: {0}, Target: '{1}', Argument count: {2}]", this.invocationId, this.target, (this.arguments != null) ? this.arguments.Length : 0);
			case MessageTypes.CancelInvocation:
				return string.Format("[CancelInvocation Id: {0}]", this.invocationId);
			case MessageTypes.Ping:
				return "[Ping]";
			case MessageTypes.Close:
				if (!string.IsNullOrEmpty(this.error))
				{
					return string.Format("[Close {0}]", this.error);
				}
				return "[Close]";
			default:
				return "Unknown message! Type: " + this.type;
			}
		}

		// Token: 0x04001408 RID: 5128
		public MessageTypes type;

		// Token: 0x04001409 RID: 5129
		public string invocationId;

		// Token: 0x0400140A RID: 5130
		public bool nonblocking;

		// Token: 0x0400140B RID: 5131
		public string target;

		// Token: 0x0400140C RID: 5132
		public object[] arguments;

		// Token: 0x0400140D RID: 5133
		public object item;

		// Token: 0x0400140E RID: 5134
		public object result;

		// Token: 0x0400140F RID: 5135
		public string error;
	}
}
