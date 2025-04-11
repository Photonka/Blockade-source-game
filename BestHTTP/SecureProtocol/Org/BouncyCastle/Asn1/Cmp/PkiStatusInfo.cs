using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007B0 RID: 1968
	public class PkiStatusInfo : Asn1Encodable
	{
		// Token: 0x06004671 RID: 18033 RVA: 0x00195E42 File Offset: 0x00194042
		public static PkiStatusInfo GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return PkiStatusInfo.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06004672 RID: 18034 RVA: 0x00195E50 File Offset: 0x00194050
		public static PkiStatusInfo GetInstance(object obj)
		{
			if (obj is PkiStatusInfo)
			{
				return (PkiStatusInfo)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PkiStatusInfo((Asn1Sequence)obj);
			}
			throw new ArgumentException("Unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004673 RID: 18035 RVA: 0x00195E90 File Offset: 0x00194090
		public PkiStatusInfo(Asn1Sequence seq)
		{
			this.status = DerInteger.GetInstance(seq[0]);
			this.statusString = null;
			this.failInfo = null;
			if (seq.Count > 2)
			{
				this.statusString = PkiFreeText.GetInstance(seq[1]);
				this.failInfo = DerBitString.GetInstance(seq[2]);
				return;
			}
			if (seq.Count > 1)
			{
				object obj = seq[1];
				if (obj is DerBitString)
				{
					this.failInfo = DerBitString.GetInstance(obj);
					return;
				}
				this.statusString = PkiFreeText.GetInstance(obj);
			}
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x00195F23 File Offset: 0x00194123
		public PkiStatusInfo(int status)
		{
			this.status = new DerInteger(status);
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x00195F37 File Offset: 0x00194137
		public PkiStatusInfo(int status, PkiFreeText statusString)
		{
			this.status = new DerInteger(status);
			this.statusString = statusString;
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x00195F52 File Offset: 0x00194152
		public PkiStatusInfo(int status, PkiFreeText statusString, PkiFailureInfo failInfo)
		{
			this.status = new DerInteger(status);
			this.statusString = statusString;
			this.failInfo = failInfo;
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06004677 RID: 18039 RVA: 0x00195F74 File Offset: 0x00194174
		public BigInteger Status
		{
			get
			{
				return this.status.Value;
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06004678 RID: 18040 RVA: 0x00195F81 File Offset: 0x00194181
		public PkiFreeText StatusString
		{
			get
			{
				return this.statusString;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06004679 RID: 18041 RVA: 0x00195F89 File Offset: 0x00194189
		public DerBitString FailInfo
		{
			get
			{
				return this.failInfo;
			}
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x00195F94 File Offset: 0x00194194
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.status
			});
			if (this.statusString != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.statusString
				});
			}
			if (this.failInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.failInfo
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002D11 RID: 11537
		private DerInteger status;

		// Token: 0x04002D12 RID: 11538
		private PkiFreeText statusString;

		// Token: 0x04002D13 RID: 11539
		private DerBitString failInfo;
	}
}
