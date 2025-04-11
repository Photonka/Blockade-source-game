using System;

namespace BestHTTP.SocketIO
{
	// Token: 0x020001B8 RID: 440
	public sealed class Error
	{
		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x000A0E34 File Offset: 0x0009F034
		// (set) Token: 0x06001058 RID: 4184 RVA: 0x000A0E3C File Offset: 0x0009F03C
		public SocketIOErrors Code { get; private set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x000A0E45 File Offset: 0x0009F045
		// (set) Token: 0x0600105A RID: 4186 RVA: 0x000A0E4D File Offset: 0x0009F04D
		public string Message { get; private set; }

		// Token: 0x0600105B RID: 4187 RVA: 0x000A0E56 File Offset: 0x0009F056
		public Error(SocketIOErrors code, string msg)
		{
			this.Code = code;
			this.Message = msg;
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000A0E6C File Offset: 0x0009F06C
		public override string ToString()
		{
			return string.Format("Code: {0} Message: \"{1}\"", this.Code.ToString(), this.Message);
		}
	}
}
