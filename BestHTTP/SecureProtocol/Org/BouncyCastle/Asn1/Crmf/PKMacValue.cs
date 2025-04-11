using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Cmp;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Crmf
{
	// Token: 0x0200075D RID: 1885
	public class PKMacValue : Asn1Encodable
	{
		// Token: 0x06004404 RID: 17412 RVA: 0x0018F376 File Offset: 0x0018D576
		private PKMacValue(Asn1Sequence seq)
		{
			this.algID = AlgorithmIdentifier.GetInstance(seq[0]);
			this.macValue = DerBitString.GetInstance(seq[1]);
		}

		// Token: 0x06004405 RID: 17413 RVA: 0x0018F3A2 File Offset: 0x0018D5A2
		public static PKMacValue GetInstance(object obj)
		{
			if (obj is PKMacValue)
			{
				return (PKMacValue)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new PKMacValue((Asn1Sequence)obj);
			}
			throw new ArgumentException("Invalid object: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004406 RID: 17414 RVA: 0x0018F3E1 File Offset: 0x0018D5E1
		public static PKMacValue GetInstance(Asn1TaggedObject obj, bool isExplicit)
		{
			return PKMacValue.GetInstance(Asn1Sequence.GetInstance(obj, isExplicit));
		}

		// Token: 0x06004407 RID: 17415 RVA: 0x0018F3EF File Offset: 0x0018D5EF
		public PKMacValue(PbmParameter pbmParams, DerBitString macValue) : this(new AlgorithmIdentifier(CmpObjectIdentifiers.passwordBasedMac, pbmParams), macValue)
		{
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x0018F403 File Offset: 0x0018D603
		public PKMacValue(AlgorithmIdentifier algID, DerBitString macValue)
		{
			this.algID = algID;
			this.macValue = macValue;
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06004409 RID: 17417 RVA: 0x0018F419 File Offset: 0x0018D619
		public virtual AlgorithmIdentifier AlgID
		{
			get
			{
				return this.algID;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600440A RID: 17418 RVA: 0x0018F421 File Offset: 0x0018D621
		public virtual DerBitString MacValue
		{
			get
			{
				return this.macValue;
			}
		}

		// Token: 0x0600440B RID: 17419 RVA: 0x0018F429 File Offset: 0x0018D629
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.algID,
				this.macValue
			});
		}

		// Token: 0x04002BA6 RID: 11174
		private readonly AlgorithmIdentifier algID;

		// Token: 0x04002BA7 RID: 11175
		private readonly DerBitString macValue;
	}
}
