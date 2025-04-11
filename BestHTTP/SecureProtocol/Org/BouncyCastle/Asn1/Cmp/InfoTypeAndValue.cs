using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp
{
	// Token: 0x020007A2 RID: 1954
	public class InfoTypeAndValue : Asn1Encodable
	{
		// Token: 0x060045FE RID: 17918 RVA: 0x00194CD5 File Offset: 0x00192ED5
		private InfoTypeAndValue(Asn1Sequence seq)
		{
			this.infoType = DerObjectIdentifier.GetInstance(seq[0]);
			if (seq.Count > 1)
			{
				this.infoValue = seq[1];
			}
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x00194D05 File Offset: 0x00192F05
		public static InfoTypeAndValue GetInstance(object obj)
		{
			if (obj is InfoTypeAndValue)
			{
				return (InfoTypeAndValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new InfoTypeAndValue((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x00194D44 File Offset: 0x00192F44
		public InfoTypeAndValue(DerObjectIdentifier infoType)
		{
			this.infoType = infoType;
			this.infoValue = null;
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x00194D5A File Offset: 0x00192F5A
		public InfoTypeAndValue(DerObjectIdentifier infoType, Asn1Encodable optionalValue)
		{
			this.infoType = infoType;
			this.infoValue = optionalValue;
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06004602 RID: 17922 RVA: 0x00194D70 File Offset: 0x00192F70
		public virtual DerObjectIdentifier InfoType
		{
			get
			{
				return this.infoType;
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06004603 RID: 17923 RVA: 0x00194D78 File Offset: 0x00192F78
		public virtual Asn1Encodable InfoValue
		{
			get
			{
				return this.infoValue;
			}
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x00194D80 File Offset: 0x00192F80
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.infoType
			});
			if (this.infoValue != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.infoValue
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002C9B RID: 11419
		private readonly DerObjectIdentifier infoType;

		// Token: 0x04002C9C RID: 11420
		private readonly Asn1Encodable infoValue;
	}
}
