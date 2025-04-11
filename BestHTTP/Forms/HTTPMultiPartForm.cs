using System;
using BestHTTP.Extensions;

namespace BestHTTP.Forms
{
	// Token: 0x020007CA RID: 1994
	public sealed class HTTPMultiPartForm : HTTPFormBase
	{
		// Token: 0x06004755 RID: 18261 RVA: 0x001986D8 File Offset: 0x001968D8
		public HTTPMultiPartForm()
		{
			this.Boundary = "BestHTTP_HTTPMultiPartForm_" + this.GetHashCode().ToString("X");
		}

		// Token: 0x06004756 RID: 18262 RVA: 0x0019870E File Offset: 0x0019690E
		public override void PrepareRequest(HTTPRequest request)
		{
			request.SetHeader("Content-Type", "multipart/form-data; boundary=\"" + this.Boundary + "\"");
		}

		// Token: 0x06004757 RID: 18263 RVA: 0x00198730 File Offset: 0x00196930
		public override byte[] GetData()
		{
			if (this.CachedData != null)
			{
				return this.CachedData;
			}
			byte[] array;
			using (BufferPoolMemoryStream bufferPoolMemoryStream = new BufferPoolMemoryStream())
			{
				for (int i = 0; i < base.Fields.Count; i++)
				{
					HTTPFieldData httpfieldData = base.Fields[i];
					bufferPoolMemoryStream.WriteLine("--" + this.Boundary);
					bufferPoolMemoryStream.WriteLine("Content-Disposition: form-data; name=\"" + httpfieldData.Name + "\"" + ((!string.IsNullOrEmpty(httpfieldData.FileName)) ? ("; filename=\"" + httpfieldData.FileName + "\"") : string.Empty));
					if (!string.IsNullOrEmpty(httpfieldData.MimeType))
					{
						bufferPoolMemoryStream.WriteLine("Content-Type: " + httpfieldData.MimeType);
					}
					bufferPoolMemoryStream.WriteLine("Content-Length: " + httpfieldData.Payload.Length.ToString());
					bufferPoolMemoryStream.WriteLine();
					bufferPoolMemoryStream.Write(httpfieldData.Payload, 0, httpfieldData.Payload.Length);
					bufferPoolMemoryStream.Write(HTTPRequest.EOL, 0, HTTPRequest.EOL.Length);
				}
				bufferPoolMemoryStream.WriteLine("--" + this.Boundary + "--");
				base.IsChanged = false;
				array = (this.CachedData = bufferPoolMemoryStream.ToArray());
				array = array;
			}
			return array;
		}

		// Token: 0x04002DA1 RID: 11681
		private string Boundary;

		// Token: 0x04002DA2 RID: 11682
		private byte[] CachedData;
	}
}
