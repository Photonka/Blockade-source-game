using System;
using System.Linq;
using System.Text;
using BestHTTP.JSON;

namespace BestHTTP.Forms
{
	// Token: 0x020007CC RID: 1996
	public sealed class RawJsonForm : HTTPFormBase
	{
		// Token: 0x0600475C RID: 18268 RVA: 0x00198A31 File Offset: 0x00196C31
		public override void PrepareRequest(HTTPRequest request)
		{
			request.SetHeader("Content-Type", "application/json");
		}

		// Token: 0x0600475D RID: 18269 RVA: 0x00198A44 File Offset: 0x00196C44
		public override byte[] GetData()
		{
			if (this.CachedData != null && !base.IsChanged)
			{
				return this.CachedData;
			}
			string s = Json.Encode(base.Fields.ToDictionary((HTTPFieldData x) => x.Name, (HTTPFieldData x) => x.Text));
			base.IsChanged = false;
			return this.CachedData = Encoding.UTF8.GetBytes(s);
		}

		// Token: 0x04002DA5 RID: 11685
		private byte[] CachedData;
	}
}
