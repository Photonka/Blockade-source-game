using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x0200068B RID: 1675
	public class IssuerSerial : Asn1Encodable
	{
		// Token: 0x06003E52 RID: 15954 RVA: 0x00179BD0 File Offset: 0x00177DD0
		public static IssuerSerial GetInstance(object obj)
		{
			if (obj == null || obj is IssuerSerial)
			{
				return (IssuerSerial)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new IssuerSerial((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x00179C1D File Offset: 0x00177E1D
		public static IssuerSerial GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return IssuerSerial.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x00179C2C File Offset: 0x00177E2C
		private IssuerSerial(Asn1Sequence seq)
		{
			if (seq.Count != 2 && seq.Count != 3)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			this.issuer = GeneralNames.GetInstance(seq[0]);
			this.serial = DerInteger.GetInstance(seq[1]);
			if (seq.Count == 3)
			{
				this.issuerUid = DerBitString.GetInstance(seq[2]);
			}
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00179CAB File Offset: 0x00177EAB
		public IssuerSerial(GeneralNames issuer, DerInteger serial)
		{
			this.issuer = issuer;
			this.serial = serial;
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06003E56 RID: 15958 RVA: 0x00179CC1 File Offset: 0x00177EC1
		public GeneralNames Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06003E57 RID: 15959 RVA: 0x00179CC9 File Offset: 0x00177EC9
		public DerInteger Serial
		{
			get
			{
				return this.serial;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06003E58 RID: 15960 RVA: 0x00179CD1 File Offset: 0x00177ED1
		public DerBitString IssuerUid
		{
			get
			{
				return this.issuerUid;
			}
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x00179CDC File Offset: 0x00177EDC
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(new Asn1Encodable[]
			{
				this.issuer,
				this.serial
			});
			if (this.issuerUid != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.issuerUid
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x04002695 RID: 9877
		internal readonly GeneralNames issuer;

		// Token: 0x04002696 RID: 9878
		internal readonly DerInteger serial;

		// Token: 0x04002697 RID: 9879
		internal readonly DerBitString issuerUid;
	}
}
