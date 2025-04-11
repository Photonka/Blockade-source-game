using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters
{
	// Token: 0x020004DD RID: 1245
	public class ParametersWithID : ICipherParameters
	{
		// Token: 0x06003015 RID: 12309 RVA: 0x00129164 File Offset: 0x00127364
		public ParametersWithID(ICipherParameters parameters, byte[] id) : this(parameters, id, 0, id.Length)
		{
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x00129172 File Offset: 0x00127372
		public ParametersWithID(ICipherParameters parameters, byte[] id, int idOff, int idLen)
		{
			this.parameters = parameters;
			this.id = Arrays.CopyOfRange(id, idOff, idOff + idLen);
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x00129192 File Offset: 0x00127392
		public byte[] GetID()
		{
			return this.id;
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06003018 RID: 12312 RVA: 0x0012919A File Offset: 0x0012739A
		public ICipherParameters Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x04001EDB RID: 7899
		private readonly ICipherParameters parameters;

		// Token: 0x04001EDC RID: 7900
		private readonly byte[] id;
	}
}
