using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000681 RID: 1665
	public class DisplayText : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x06003DF2 RID: 15858 RVA: 0x001783C8 File Offset: 0x001765C8
		public DisplayText(int type, string text)
		{
			if (text.Length > 200)
			{
				text = text.Substring(0, 200);
			}
			this.contentType = type;
			switch (type)
			{
			case 0:
				this.contents = new DerIA5String(text);
				return;
			case 1:
				this.contents = new DerBmpString(text);
				return;
			case 2:
				this.contents = new DerUtf8String(text);
				return;
			case 3:
				this.contents = new DerVisibleString(text);
				return;
			default:
				this.contents = new DerUtf8String(text);
				return;
			}
		}

		// Token: 0x06003DF3 RID: 15859 RVA: 0x00178455 File Offset: 0x00176655
		public DisplayText(string text)
		{
			if (text.Length > 200)
			{
				text = text.Substring(0, 200);
			}
			this.contentType = 2;
			this.contents = new DerUtf8String(text);
		}

		// Token: 0x06003DF4 RID: 15860 RVA: 0x0017848B File Offset: 0x0017668B
		public DisplayText(IAsn1String contents)
		{
			this.contents = contents;
		}

		// Token: 0x06003DF5 RID: 15861 RVA: 0x0017849A File Offset: 0x0017669A
		public static DisplayText GetInstance(object obj)
		{
			if (obj is IAsn1String)
			{
				return new DisplayText((IAsn1String)obj);
			}
			if (obj is DisplayText)
			{
				return (DisplayText)obj;
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x001784D9 File Offset: 0x001766D9
		public override Asn1Object ToAsn1Object()
		{
			return (Asn1Object)this.contents;
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x001784E6 File Offset: 0x001766E6
		public string GetString()
		{
			return this.contents.GetString();
		}

		// Token: 0x04002669 RID: 9833
		public const int ContentTypeIA5String = 0;

		// Token: 0x0400266A RID: 9834
		public const int ContentTypeBmpString = 1;

		// Token: 0x0400266B RID: 9835
		public const int ContentTypeUtf8String = 2;

		// Token: 0x0400266C RID: 9836
		public const int ContentTypeVisibleString = 3;

		// Token: 0x0400266D RID: 9837
		public const int DisplayTextMaximumSize = 200;

		// Token: 0x0400266E RID: 9838
		internal readonly int contentType;

		// Token: 0x0400266F RID: 9839
		internal readonly IAsn1String contents;
	}
}
