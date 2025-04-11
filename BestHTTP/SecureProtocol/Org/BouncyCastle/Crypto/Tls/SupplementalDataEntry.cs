using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x0200043A RID: 1082
	public class SupplementalDataEntry
	{
		// Token: 0x06002AEC RID: 10988 RVA: 0x001161E3 File Offset: 0x001143E3
		public SupplementalDataEntry(int dataType, byte[] data)
		{
			this.mDataType = dataType;
			this.mData = data;
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x001161F9 File Offset: 0x001143F9
		public virtual int DataType
		{
			get
			{
				return this.mDataType;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06002AEE RID: 10990 RVA: 0x00116201 File Offset: 0x00114401
		public virtual byte[] Data
		{
			get
			{
				return this.mData;
			}
		}

		// Token: 0x04001CC7 RID: 7367
		protected readonly int mDataType;

		// Token: 0x04001CC8 RID: 7368
		protected readonly byte[] mData;
	}
}
