using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000331 RID: 817
	internal class ValidityPreCompInfo : PreCompInfo
	{
		// Token: 0x06001FEE RID: 8174 RVA: 0x000F11A3 File Offset: 0x000EF3A3
		internal bool HasFailed()
		{
			return this.failed;
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x000F11AB File Offset: 0x000EF3AB
		internal void ReportFailed()
		{
			this.failed = true;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x000F11B4 File Offset: 0x000EF3B4
		internal bool HasCurveEquationPassed()
		{
			return this.curveEquationPassed;
		}

		// Token: 0x06001FF1 RID: 8177 RVA: 0x000F11BC File Offset: 0x000EF3BC
		internal void ReportCurveEquationPassed()
		{
			this.curveEquationPassed = true;
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x000F11C5 File Offset: 0x000EF3C5
		internal bool HasOrderPassed()
		{
			return this.orderPassed;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x000F11CD File Offset: 0x000EF3CD
		internal void ReportOrderPassed()
		{
			this.orderPassed = true;
		}

		// Token: 0x040018A7 RID: 6311
		internal static readonly string PRECOMP_NAME = "bc_validity";

		// Token: 0x040018A8 RID: 6312
		private bool failed;

		// Token: 0x040018A9 RID: 6313
		private bool curveEquationPassed;

		// Token: 0x040018AA RID: 6314
		private bool orderPassed;
	}
}
