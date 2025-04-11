using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Tsp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Tsp
{
	// Token: 0x02000294 RID: 660
	public class TimeStampTokenInfo
	{
		// Token: 0x06001872 RID: 6258 RVA: 0x000BC810 File Offset: 0x000BAA10
		public TimeStampTokenInfo(TstInfo tstInfo)
		{
			this.tstInfo = tstInfo;
			try
			{
				this.genTime = tstInfo.GenTime.ToDateTime();
			}
			catch (Exception ex)
			{
				throw new TspException("unable to parse genTime field: " + ex.Message);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001873 RID: 6259 RVA: 0x000BC864 File Offset: 0x000BAA64
		public bool IsOrdered
		{
			get
			{
				return this.tstInfo.Ordering.IsTrue;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001874 RID: 6260 RVA: 0x000BC876 File Offset: 0x000BAA76
		public Accuracy Accuracy
		{
			get
			{
				return this.tstInfo.Accuracy;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001875 RID: 6261 RVA: 0x000BC883 File Offset: 0x000BAA83
		public DateTime GenTime
		{
			get
			{
				return this.genTime;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001876 RID: 6262 RVA: 0x000BC88B File Offset: 0x000BAA8B
		public GenTimeAccuracy GenTimeAccuracy
		{
			get
			{
				if (this.Accuracy != null)
				{
					return new GenTimeAccuracy(this.Accuracy);
				}
				return null;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x000BC8A2 File Offset: 0x000BAAA2
		public string Policy
		{
			get
			{
				return this.tstInfo.Policy.Id;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001878 RID: 6264 RVA: 0x000BC8B4 File Offset: 0x000BAAB4
		public BigInteger SerialNumber
		{
			get
			{
				return this.tstInfo.SerialNumber.Value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06001879 RID: 6265 RVA: 0x000BC8C6 File Offset: 0x000BAAC6
		public GeneralName Tsa
		{
			get
			{
				return this.tstInfo.Tsa;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600187A RID: 6266 RVA: 0x000BC8D3 File Offset: 0x000BAAD3
		public BigInteger Nonce
		{
			get
			{
				if (this.tstInfo.Nonce != null)
				{
					return this.tstInfo.Nonce.Value;
				}
				return null;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x0600187B RID: 6267 RVA: 0x000BC8F4 File Offset: 0x000BAAF4
		public AlgorithmIdentifier HashAlgorithm
		{
			get
			{
				return this.tstInfo.MessageImprint.HashAlgorithm;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x0600187C RID: 6268 RVA: 0x000BC906 File Offset: 0x000BAB06
		public string MessageImprintAlgOid
		{
			get
			{
				return this.tstInfo.MessageImprint.HashAlgorithm.Algorithm.Id;
			}
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x000BC922 File Offset: 0x000BAB22
		public byte[] GetMessageImprintDigest()
		{
			return this.tstInfo.MessageImprint.GetHashedMessage();
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x000BC934 File Offset: 0x000BAB34
		public byte[] GetEncoded()
		{
			return this.tstInfo.GetEncoded();
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600187F RID: 6271 RVA: 0x000BC941 File Offset: 0x000BAB41
		public TstInfo TstInfo
		{
			get
			{
				return this.tstInfo;
			}
		}

		// Token: 0x0400171A RID: 5914
		private TstInfo tstInfo;

		// Token: 0x0400171B RID: 5915
		private DateTime genTime;
	}
}
