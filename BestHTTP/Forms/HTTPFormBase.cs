using System;
using System.Collections.Generic;
using System.Text;

namespace BestHTTP.Forms
{
	// Token: 0x020007C8 RID: 1992
	public class HTTPFormBase
	{
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06004743 RID: 18243 RVA: 0x0019850D File Offset: 0x0019670D
		// (set) Token: 0x06004744 RID: 18244 RVA: 0x00198515 File Offset: 0x00196715
		public List<HTTPFieldData> Fields { get; set; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06004745 RID: 18245 RVA: 0x0019851E File Offset: 0x0019671E
		public bool IsEmpty
		{
			get
			{
				return this.Fields == null || this.Fields.Count == 0;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06004746 RID: 18246 RVA: 0x00198538 File Offset: 0x00196738
		// (set) Token: 0x06004747 RID: 18247 RVA: 0x00198540 File Offset: 0x00196740
		public bool IsChanged { get; protected set; }

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06004748 RID: 18248 RVA: 0x00198549 File Offset: 0x00196749
		// (set) Token: 0x06004749 RID: 18249 RVA: 0x00198551 File Offset: 0x00196751
		public bool HasBinary { get; protected set; }

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x0600474A RID: 18250 RVA: 0x0019855A File Offset: 0x0019675A
		// (set) Token: 0x0600474B RID: 18251 RVA: 0x00198562 File Offset: 0x00196762
		public bool HasLongValue { get; protected set; }

		// Token: 0x0600474C RID: 18252 RVA: 0x0019856B File Offset: 0x0019676B
		public void AddBinaryData(string fieldName, byte[] content)
		{
			this.AddBinaryData(fieldName, content, null, null);
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x00198577 File Offset: 0x00196777
		public void AddBinaryData(string fieldName, byte[] content, string fileName)
		{
			this.AddBinaryData(fieldName, content, fileName, null);
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x00198584 File Offset: 0x00196784
		public void AddBinaryData(string fieldName, byte[] content, string fileName, string mimeType)
		{
			if (this.Fields == null)
			{
				this.Fields = new List<HTTPFieldData>();
			}
			HTTPFieldData httpfieldData = new HTTPFieldData();
			httpfieldData.Name = fieldName;
			if (fileName == null)
			{
				httpfieldData.FileName = fieldName + ".dat";
			}
			else
			{
				httpfieldData.FileName = fileName;
			}
			if (mimeType == null)
			{
				httpfieldData.MimeType = "application/octet-stream";
			}
			else
			{
				httpfieldData.MimeType = mimeType;
			}
			httpfieldData.Binary = content;
			this.Fields.Add(httpfieldData);
			this.HasBinary = (this.IsChanged = true);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x0019860A File Offset: 0x0019680A
		public void AddField(string fieldName, string value)
		{
			this.AddField(fieldName, value, Encoding.UTF8);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x0019861C File Offset: 0x0019681C
		public void AddField(string fieldName, string value, Encoding e)
		{
			if (this.Fields == null)
			{
				this.Fields = new List<HTTPFieldData>();
			}
			HTTPFieldData httpfieldData = new HTTPFieldData();
			httpfieldData.Name = fieldName;
			httpfieldData.FileName = null;
			if (e != null)
			{
				httpfieldData.MimeType = "text/plain; charset=" + e.WebName;
			}
			httpfieldData.Text = value;
			httpfieldData.Encoding = e;
			this.Fields.Add(httpfieldData);
			this.IsChanged = true;
			this.HasLongValue |= (value.Length > 256);
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x001986A4 File Offset: 0x001968A4
		public virtual void CopyFrom(HTTPFormBase fields)
		{
			this.Fields = new List<HTTPFieldData>(fields.Fields);
			this.IsChanged = true;
			this.HasBinary = fields.HasBinary;
			this.HasLongValue = fields.HasLongValue;
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x00096B9B File Offset: 0x00094D9B
		public virtual void PrepareRequest(HTTPRequest request)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x00096B9B File Offset: 0x00094D9B
		public virtual byte[] GetData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002D97 RID: 11671
		private const int LongLength = 256;
	}
}
