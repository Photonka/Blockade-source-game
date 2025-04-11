using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002AC RID: 684
	internal class ReasonsMask
	{
		// Token: 0x06001971 RID: 6513 RVA: 0x000C253C File Offset: 0x000C073C
		internal ReasonsMask(int reasons)
		{
			this._reasons = reasons;
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000C254B File Offset: 0x000C074B
		internal ReasonsMask() : this(0)
		{
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000C2554 File Offset: 0x000C0754
		internal void AddReasons(ReasonsMask mask)
		{
			this._reasons |= mask.Reasons.IntValue;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001974 RID: 6516 RVA: 0x000C256E File Offset: 0x000C076E
		internal bool IsAllReasons
		{
			get
			{
				return this._reasons == ReasonsMask.AllReasons._reasons;
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x000C2582 File Offset: 0x000C0782
		internal ReasonsMask Intersect(ReasonsMask mask)
		{
			ReasonsMask reasonsMask = new ReasonsMask();
			reasonsMask.AddReasons(new ReasonsMask(this._reasons & mask.Reasons.IntValue));
			return reasonsMask;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x000C25A6 File Offset: 0x000C07A6
		internal bool HasNewReasons(ReasonsMask mask)
		{
			return (this._reasons | (mask.Reasons.IntValue ^ this._reasons)) != 0;
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x000C25C4 File Offset: 0x000C07C4
		public ReasonFlags Reasons
		{
			get
			{
				return new ReasonFlags(this._reasons);
			}
		}

		// Token: 0x0400176D RID: 5997
		private int _reasons;

		// Token: 0x0400176E RID: 5998
		internal static readonly ReasonsMask AllReasons = new ReasonsMask(33023);
	}
}
