using System;
using System.Text;
using UnityEngine;

namespace BestHTTP.Logger
{
	// Token: 0x020007C3 RID: 1987
	public class DefaultLogger : ILogger
	{
		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x00197945 File Offset: 0x00195B45
		// (set) Token: 0x06004700 RID: 18176 RVA: 0x0019794D File Offset: 0x00195B4D
		public Loglevels Level { get; set; }

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06004701 RID: 18177 RVA: 0x00197956 File Offset: 0x00195B56
		// (set) Token: 0x06004702 RID: 18178 RVA: 0x0019795E File Offset: 0x00195B5E
		public string FormatVerbose { get; set; }

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06004703 RID: 18179 RVA: 0x00197967 File Offset: 0x00195B67
		// (set) Token: 0x06004704 RID: 18180 RVA: 0x0019796F File Offset: 0x00195B6F
		public string FormatInfo { get; set; }

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06004705 RID: 18181 RVA: 0x00197978 File Offset: 0x00195B78
		// (set) Token: 0x06004706 RID: 18182 RVA: 0x00197980 File Offset: 0x00195B80
		public string FormatWarn { get; set; }

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06004707 RID: 18183 RVA: 0x00197989 File Offset: 0x00195B89
		// (set) Token: 0x06004708 RID: 18184 RVA: 0x00197991 File Offset: 0x00195B91
		public string FormatErr { get; set; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06004709 RID: 18185 RVA: 0x0019799A File Offset: 0x00195B9A
		// (set) Token: 0x0600470A RID: 18186 RVA: 0x001979A2 File Offset: 0x00195BA2
		public string FormatEx { get; set; }

		// Token: 0x0600470B RID: 18187 RVA: 0x001979AC File Offset: 0x00195BAC
		public DefaultLogger()
		{
			this.FormatVerbose = "[{0}] D [{1}]: {2}";
			this.FormatInfo = "[{0}] I [{1}]: {2}";
			this.FormatWarn = "[{0}] W [{1}]: {2}";
			this.FormatErr = "[{0}] Err [{1}]: {2}";
			this.FormatEx = "[{0}] Ex [{1}]: {2} - Message: {3}  StackTrace: {4}";
			this.Level = (Debug.isDebugBuild ? Loglevels.Warning : Loglevels.Error);
		}

		// Token: 0x0600470C RID: 18188 RVA: 0x00197A08 File Offset: 0x00195C08
		public void Verbose(string division, string verb)
		{
			if (this.Level <= Loglevels.All)
			{
				try
				{
					Debug.Log(string.Format(this.FormatVerbose, this.GetFormattedTime(), division, verb));
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600470D RID: 18189 RVA: 0x00197A4C File Offset: 0x00195C4C
		public void Information(string division, string info)
		{
			if (this.Level <= Loglevels.Information)
			{
				try
				{
					Debug.Log(string.Format(this.FormatInfo, this.GetFormattedTime(), division, info));
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600470E RID: 18190 RVA: 0x00197A90 File Offset: 0x00195C90
		public void Warning(string division, string warn)
		{
			if (this.Level <= Loglevels.Warning)
			{
				try
				{
					Debug.LogWarning(string.Format(this.FormatWarn, this.GetFormattedTime(), division, warn));
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600470F RID: 18191 RVA: 0x00197AD4 File Offset: 0x00195CD4
		public void Error(string division, string err)
		{
			if (this.Level <= Loglevels.Error)
			{
				try
				{
					Debug.LogError(string.Format(this.FormatErr, this.GetFormattedTime(), division, err));
				}
				catch
				{
				}
			}
		}

		// Token: 0x06004710 RID: 18192 RVA: 0x00197B18 File Offset: 0x00195D18
		public void Exception(string division, string msg, Exception ex)
		{
			if (this.Level <= Loglevels.Exception)
			{
				try
				{
					string text = string.Empty;
					if (ex == null)
					{
						text = "null";
					}
					else
					{
						StringBuilder stringBuilder = new StringBuilder();
						Exception ex2 = ex;
						int num = 1;
						while (ex2 != null)
						{
							stringBuilder.AppendFormat("{0}: {1} {2}", num++.ToString(), ex2.Message, ex2.StackTrace);
							ex2 = ex2.InnerException;
							if (ex2 != null)
							{
								stringBuilder.AppendLine();
							}
						}
						text = stringBuilder.ToString();
					}
					Debug.LogError(string.Format(this.FormatEx, new object[]
					{
						this.GetFormattedTime(),
						division,
						msg,
						text,
						(ex != null) ? ex.StackTrace : "null"
					}));
				}
				catch
				{
				}
			}
		}

		// Token: 0x06004711 RID: 18193 RVA: 0x00197BE4 File Offset: 0x00195DE4
		private string GetFormattedTime()
		{
			return DateTime.Now.Ticks.ToString();
		}
	}
}
