using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x020006BB RID: 1723
	public class QCStatement : Asn1Encodable
	{
		// Token: 0x06003FDA RID: 16346 RVA: 0x0017F7DC File Offset: 0x0017D9DC
		public static QCStatement GetInstance(object obj)
		{
			if (obj == null || obj is QCStatement)
			{
				return (QCStatement)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new QCStatement(Asn1Sequence.GetInstance(obj));
			}
			throw new ArgumentException("unknown object in GetInstance: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003FDB RID: 16347 RVA: 0x0017F829 File Offset: 0x0017DA29
		private QCStatement(Asn1Sequence seq)
		{
			this.qcStatementId = DerObjectIdentifier.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.qcStatementInfo = seq[1];
			}
		}

		// Token: 0x06003FDC RID: 16348 RVA: 0x0017F859 File Offset: 0x0017DA59
		public QCStatement(DerObjectIdentifier qcStatementId)
		{
			this.qcStatementId = qcStatementId;
		}

		// Token: 0x06003FDD RID: 16349 RVA: 0x0017F868 File Offset: 0x0017DA68
		public QCStatement(DerObjectIdentifier qcStatementId, Asn1Encodable qcStatementInfo)
		{
			this.qcStatementId = qcStatementId;
			this.qcStatementInfo = qcStatementInfo;
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06003FDE RID: 16350 RVA: 0x0017F87E File Offset: 0x0017DA7E
		public DerObjectIdentifier StatementId
		{
			get
			{
				return this.qcStatementId;
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06003FDF RID: 16351 RVA: 0x0017F886 File Offset: 0x0017DA86
		public Asn1Encodable StatementInfo
		{
			get
			{
				return this.qcStatementInfo;
			}
		}

		// Token: 0x06003FE0 RID: 16352 RVA: 0x0017F890 File Offset: 0x0017DA90
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.qcStatementId
			});
			if (this.qcStatementInfo != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.qcStatementInfo
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040027B2 RID: 10162
		private readonly DerObjectIdentifier qcStatementId;

		// Token: 0x040027B3 RID: 10163
		private readonly Asn1Encodable qcStatementInfo;
	}
}
