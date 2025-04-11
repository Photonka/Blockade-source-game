using System;

namespace BestHTTP.Logger
{
	// Token: 0x020007C5 RID: 1989
	public interface ILogger
	{
		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06004712 RID: 18194
		// (set) Token: 0x06004713 RID: 18195
		Loglevels Level { get; set; }

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06004714 RID: 18196
		// (set) Token: 0x06004715 RID: 18197
		string FormatVerbose { get; set; }

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06004716 RID: 18198
		// (set) Token: 0x06004717 RID: 18199
		string FormatInfo { get; set; }

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06004718 RID: 18200
		// (set) Token: 0x06004719 RID: 18201
		string FormatWarn { get; set; }

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x0600471A RID: 18202
		// (set) Token: 0x0600471B RID: 18203
		string FormatErr { get; set; }

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x0600471C RID: 18204
		// (set) Token: 0x0600471D RID: 18205
		string FormatEx { get; set; }

		// Token: 0x0600471E RID: 18206
		void Verbose(string division, string verb);

		// Token: 0x0600471F RID: 18207
		void Information(string division, string info);

		// Token: 0x06004720 RID: 18208
		void Warning(string division, string warn);

		// Token: 0x06004721 RID: 18209
		void Error(string division, string err);

		// Token: 0x06004722 RID: 18210
		void Exception(string division, string msg, Exception ex);
	}
}
