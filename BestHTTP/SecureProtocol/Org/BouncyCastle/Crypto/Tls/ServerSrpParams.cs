using System;
using System.IO;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Tls
{
	// Token: 0x02000430 RID: 1072
	public class ServerSrpParams
	{
		// Token: 0x06002AAD RID: 10925 RVA: 0x00115A75 File Offset: 0x00113C75
		public ServerSrpParams(BigInteger N, BigInteger g, byte[] s, BigInteger B)
		{
			this.m_N = N;
			this.m_g = g;
			this.m_s = Arrays.Clone(s);
			this.m_B = B;
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06002AAE RID: 10926 RVA: 0x00115A9F File Offset: 0x00113C9F
		public virtual BigInteger B
		{
			get
			{
				return this.m_B;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x00115AA7 File Offset: 0x00113CA7
		public virtual BigInteger G
		{
			get
			{
				return this.m_g;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x00115AAF File Offset: 0x00113CAF
		public virtual BigInteger N
		{
			get
			{
				return this.m_N;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06002AB1 RID: 10929 RVA: 0x00115AB7 File Offset: 0x00113CB7
		public virtual byte[] S
		{
			get
			{
				return this.m_s;
			}
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x00115ABF File Offset: 0x00113CBF
		public virtual void Encode(Stream output)
		{
			TlsSrpUtilities.WriteSrpParameter(this.m_N, output);
			TlsSrpUtilities.WriteSrpParameter(this.m_g, output);
			TlsUtilities.WriteOpaque8(this.m_s, output);
			TlsSrpUtilities.WriteSrpParameter(this.m_B, output);
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x00115AF4 File Offset: 0x00113CF4
		public static ServerSrpParams Parse(Stream input)
		{
			BigInteger n = TlsSrpUtilities.ReadSrpParameter(input);
			BigInteger g = TlsSrpUtilities.ReadSrpParameter(input);
			byte[] s = TlsUtilities.ReadOpaque8(input);
			BigInteger b = TlsSrpUtilities.ReadSrpParameter(input);
			return new ServerSrpParams(n, g, s, b);
		}

		// Token: 0x04001C9D RID: 7325
		protected BigInteger m_N;

		// Token: 0x04001C9E RID: 7326
		protected BigInteger m_g;

		// Token: 0x04001C9F RID: 7327
		protected BigInteger m_B;

		// Token: 0x04001CA0 RID: 7328
		protected byte[] m_s;
	}
}
