using System;
using System.Text;

namespace BestHTTP.Forms
{
	// Token: 0x020007C7 RID: 1991
	public class HTTPFieldData
	{
		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06004735 RID: 18229 RVA: 0x0019845B File Offset: 0x0019665B
		// (set) Token: 0x06004736 RID: 18230 RVA: 0x00198463 File Offset: 0x00196663
		public string Name { get; set; }

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06004737 RID: 18231 RVA: 0x0019846C File Offset: 0x0019666C
		// (set) Token: 0x06004738 RID: 18232 RVA: 0x00198474 File Offset: 0x00196674
		public string FileName { get; set; }

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06004739 RID: 18233 RVA: 0x0019847D File Offset: 0x0019667D
		// (set) Token: 0x0600473A RID: 18234 RVA: 0x00198485 File Offset: 0x00196685
		public string MimeType { get; set; }

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x0600473B RID: 18235 RVA: 0x0019848E File Offset: 0x0019668E
		// (set) Token: 0x0600473C RID: 18236 RVA: 0x00198496 File Offset: 0x00196696
		public Encoding Encoding { get; set; }

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x0600473D RID: 18237 RVA: 0x0019849F File Offset: 0x0019669F
		// (set) Token: 0x0600473E RID: 18238 RVA: 0x001984A7 File Offset: 0x001966A7
		public string Text { get; set; }

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x0600473F RID: 18239 RVA: 0x001984B0 File Offset: 0x001966B0
		// (set) Token: 0x06004740 RID: 18240 RVA: 0x001984B8 File Offset: 0x001966B8
		public byte[] Binary { get; set; }

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06004741 RID: 18241 RVA: 0x001984C4 File Offset: 0x001966C4
		public byte[] Payload
		{
			get
			{
				if (this.Binary != null)
				{
					return this.Binary;
				}
				if (this.Encoding == null)
				{
					this.Encoding = Encoding.UTF8;
				}
				return this.Binary = this.Encoding.GetBytes(this.Text);
			}
		}
	}
}
