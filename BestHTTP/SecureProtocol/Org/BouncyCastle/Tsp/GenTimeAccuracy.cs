using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x0200028D RID: 653
	public class GenTimeAccuracy
	{
		// Token: 0x06001827 RID: 6183 RVA: 0x000BB790 File Offset: 0x000B9990
		public GenTimeAccuracy(Accuracy accuracy)
		{
			this.accuracy = accuracy;
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x000BB79F File Offset: 0x000B999F
		public int Seconds
		{
			get
			{
				return this.GetTimeComponent(this.accuracy.Seconds);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x000BB7B2 File Offset: 0x000B99B2
		public int Millis
		{
			get
			{
				return this.GetTimeComponent(this.accuracy.Millis);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x000BB7C5 File Offset: 0x000B99C5
		public int Micros
		{
			get
			{
				return this.GetTimeComponent(this.accuracy.Micros);
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000BB7D8 File Offset: 0x000B99D8
		private int GetTimeComponent(DerInteger time)
		{
			if (time != null)
			{
				return time.Value.IntValue;
			}
			return 0;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000BB7EC File Offset: 0x000B99EC
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Seconds,
				".",
				this.Millis.ToString("000"),
				this.Micros.ToString("000")
			});
		}

		// Token: 0x040016F9 RID: 5881
		private Accuracy accuracy;
	}
}
