using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.IO.Pem
{
	// Token: 0x0200026C RID: 620
	public class PemHeader
	{
		// Token: 0x06001731 RID: 5937 RVA: 0x000B9A4F File Offset: 0x000B7C4F
		public PemHeader(string name, string val)
		{
			this.name = name;
			this.val = val;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x000B9A65 File Offset: 0x000B7C65
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x000B9A6D File Offset: 0x000B7C6D
		public virtual string Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000B9A75 File Offset: 0x000B7C75
		public override int GetHashCode()
		{
			return this.GetHashCode(this.name) + 31 * this.GetHashCode(this.val);
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000B9A94 File Offset: 0x000B7C94
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (!(obj is PemHeader))
			{
				return false;
			}
			PemHeader pemHeader = (PemHeader)obj;
			return object.Equals(this.name, pemHeader.name) && object.Equals(this.val, pemHeader.val);
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000B9ADE File Offset: 0x000B7CDE
		private int GetHashCode(string s)
		{
			if (s == null)
			{
				return 1;
			}
			return s.GetHashCode();
		}

		// Token: 0x040016D2 RID: 5842
		private string name;

		// Token: 0x040016D3 RID: 5843
		private string val;
	}
}
