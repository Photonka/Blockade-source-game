using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x02000685 RID: 1669
	public class ExtendedKeyUsage : Asn1Encodable
	{
		// Token: 0x06003E14 RID: 15892 RVA: 0x00178A6D File Offset: 0x00176C6D
		public static ExtendedKeyUsage GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ExtendedKeyUsage.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003E15 RID: 15893 RVA: 0x00178A7C File Offset: 0x00176C7C
		public static ExtendedKeyUsage GetInstance(object obj)
		{
			if (obj is ExtendedKeyUsage)
			{
				return (ExtendedKeyUsage)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ExtendedKeyUsage((Asn1Sequence)obj);
			}
			if (obj is X509Extension)
			{
				return ExtendedKeyUsage.GetInstance(X509Extension.ConvertValueToObject((X509Extension)obj));
			}
			throw new ArgumentException("Invalid ExtendedKeyUsage: " + Platform.GetTypeName(obj));
		}

		// Token: 0x06003E16 RID: 15894 RVA: 0x00178ADC File Offset: 0x00176CDC
		private ExtendedKeyUsage(Asn1Sequence seq)
		{
			this.seq = seq;
			foreach (object obj in seq)
			{
				if (!(obj is DerObjectIdentifier))
				{
					throw new ArgumentException("Only DerObjectIdentifier instances allowed in ExtendedKeyUsage.");
				}
				this.usageTable[obj] = obj;
			}
		}

		// Token: 0x06003E17 RID: 15895 RVA: 0x00178B5C File Offset: 0x00176D5C
		public ExtendedKeyUsage(params KeyPurposeID[] usages)
		{
			this.seq = new DerSequence(usages);
			foreach (KeyPurposeID keyPurposeID in usages)
			{
				this.usageTable[keyPurposeID] = keyPurposeID;
			}
		}

		// Token: 0x06003E18 RID: 15896 RVA: 0x00178BA9 File Offset: 0x00176DA9
		[Obsolete]
		public ExtendedKeyUsage(ArrayList usages) : this(usages)
		{
		}

		// Token: 0x06003E19 RID: 15897 RVA: 0x00178BB4 File Offset: 0x00176DB4
		public ExtendedKeyUsage(IEnumerable usages)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in usages)
			{
				Asn1Encodable instance = DerObjectIdentifier.GetInstance(obj);
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					instance
				});
				this.usageTable[instance] = instance;
			}
			this.seq = new DerSequence(asn1EncodableVector);
		}

		// Token: 0x06003E1A RID: 15898 RVA: 0x00178C48 File Offset: 0x00176E48
		public bool HasKeyPurposeId(KeyPurposeID keyPurposeId)
		{
			return this.usageTable.Contains(keyPurposeId);
		}

		// Token: 0x06003E1B RID: 15899 RVA: 0x00178C56 File Offset: 0x00176E56
		[Obsolete("Use 'GetAllUsages'")]
		public ArrayList GetUsages()
		{
			return new ArrayList(this.usageTable.Values);
		}

		// Token: 0x06003E1C RID: 15900 RVA: 0x00178C68 File Offset: 0x00176E68
		public IList GetAllUsages()
		{
			return Platform.CreateArrayList(this.usageTable.Values);
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06003E1D RID: 15901 RVA: 0x00178C7A File Offset: 0x00176E7A
		public int Count
		{
			get
			{
				return this.usageTable.Count;
			}
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x00178C87 File Offset: 0x00176E87
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x0400267A RID: 9850
		internal readonly IDictionary usageTable = Platform.CreateHashtable();

		// Token: 0x0400267B RID: 9851
		internal readonly Asn1Sequence seq;
	}
}
