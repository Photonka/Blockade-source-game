using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x02000701 RID: 1793
	public class TbsRequest : Asn1Encodable
	{
		// Token: 0x060041C7 RID: 16839 RVA: 0x00186A6E File Offset: 0x00184C6E
		public static TbsRequest GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return TbsRequest.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x00186A7C File Offset: 0x00184C7C
		public static TbsRequest GetInstance(object obj)
		{
			if (obj == null || obj is TbsRequest)
			{
				return (TbsRequest)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new TbsRequest((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x00186AC9 File Offset: 0x00184CC9
		public TbsRequest(GeneralName requestorName, Asn1Sequence requestList, X509Extensions requestExtensions)
		{
			this.version = TbsRequest.V1;
			this.requestorName = requestorName;
			this.requestList = requestList;
			this.requestExtensions = requestExtensions;
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x00186AF4 File Offset: 0x00184CF4
		private TbsRequest(Asn1Sequence seq)
		{
			int num = 0;
			Asn1Encodable asn1Encodable = seq[0];
			if (asn1Encodable is Asn1TaggedObject)
			{
				Asn1TaggedObject asn1TaggedObject = (Asn1TaggedObject)asn1Encodable;
				if (asn1TaggedObject.TagNo == 0)
				{
					this.versionSet = true;
					this.version = DerInteger.GetInstance(asn1TaggedObject, true);
					num++;
				}
				else
				{
					this.version = TbsRequest.V1;
				}
			}
			else
			{
				this.version = TbsRequest.V1;
			}
			if (seq[num] is Asn1TaggedObject)
			{
				this.requestorName = GeneralName.GetInstance((Asn1TaggedObject)seq[num++], true);
			}
			this.requestList = (Asn1Sequence)seq[num++];
			if (seq.Count == num + 1)
			{
				this.requestExtensions = X509Extensions.GetInstance((Asn1TaggedObject)seq[num], true);
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x060041CB RID: 16843 RVA: 0x00186BBD File Offset: 0x00184DBD
		public DerInteger Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x060041CC RID: 16844 RVA: 0x00186BC5 File Offset: 0x00184DC5
		public GeneralName RequestorName
		{
			get
			{
				return this.requestorName;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x060041CD RID: 16845 RVA: 0x00186BCD File Offset: 0x00184DCD
		public Asn1Sequence RequestList
		{
			get
			{
				return this.requestList;
			}
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x00186BD5 File Offset: 0x00184DD5
		public X509Extensions RequestExtensions
		{
			get
			{
				return this.requestExtensions;
			}
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x00186BE0 File Offset: 0x00184DE0
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (!this.version.Equals(TbsRequest.V1) || this.versionSet)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 0, this.version)
				});
			}
			if (this.requestorName != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 1, this.requestorName)
				});
			}
			asn1EncodableVector.Add(new Asn1Encodable[]
			{
				this.requestList
			});
			if (this.requestExtensions != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					new DerTaggedObject(true, 2, this.requestExtensions)
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x0400299C RID: 10652
		private static readonly DerInteger V1 = new DerInteger(0);

		// Token: 0x0400299D RID: 10653
		private readonly DerInteger version;

		// Token: 0x0400299E RID: 10654
		private readonly GeneralName requestorName;

		// Token: 0x0400299F RID: 10655
		private readonly Asn1Sequence requestList;

		// Token: 0x040029A0 RID: 10656
		private readonly X509Extensions requestExtensions;

		// Token: 0x040029A1 RID: 10657
		private bool versionSet;
	}
}
