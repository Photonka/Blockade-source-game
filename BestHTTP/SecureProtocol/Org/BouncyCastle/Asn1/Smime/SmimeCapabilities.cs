using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Pkcs;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Smime
{
	// Token: 0x020006CB RID: 1739
	public class SmimeCapabilities : Asn1Encodable
	{
		// Token: 0x0600404B RID: 16459 RVA: 0x00181BF8 File Offset: 0x0017FDF8
		public static SmimeCapabilities GetInstance(object obj)
		{
			if (obj == null || obj is SmimeCapabilities)
			{
				return (SmimeCapabilities)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new SmimeCapabilities((Asn1Sequence)obj);
			}
			if (obj is AttributeX509)
			{
				return new SmimeCapabilities((Asn1Sequence)((AttributeX509)obj).AttrValues[0]);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x0600404C RID: 16460 RVA: 0x00181C69 File Offset: 0x0017FE69
		public SmimeCapabilities(Asn1Sequence seq)
		{
			this.capabilities = seq;
		}

		// Token: 0x0600404D RID: 16461 RVA: 0x00181C78 File Offset: 0x0017FE78
		[Obsolete("Use 'GetCapabilitiesForOid' instead")]
		public ArrayList GetCapabilities(DerObjectIdentifier capability)
		{
			ArrayList arrayList = new ArrayList();
			this.DoGetCapabilitiesForOid(capability, arrayList);
			return arrayList;
		}

		// Token: 0x0600404E RID: 16462 RVA: 0x00181C94 File Offset: 0x0017FE94
		public IList GetCapabilitiesForOid(DerObjectIdentifier capability)
		{
			IList list = Platform.CreateArrayList();
			this.DoGetCapabilitiesForOid(capability, list);
			return list;
		}

		// Token: 0x0600404F RID: 16463 RVA: 0x00181CB0 File Offset: 0x0017FEB0
		private void DoGetCapabilitiesForOid(DerObjectIdentifier capability, IList list)
		{
			if (capability == null)
			{
				using (IEnumerator enumerator = this.capabilities.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						SmimeCapability instance = SmimeCapability.GetInstance(obj);
						list.Add(instance);
					}
					return;
				}
			}
			foreach (object obj2 in this.capabilities)
			{
				SmimeCapability instance2 = SmimeCapability.GetInstance(obj2);
				if (capability.Equals(instance2.CapabilityID))
				{
					list.Add(instance2);
				}
			}
		}

		// Token: 0x06004050 RID: 16464 RVA: 0x00181D64 File Offset: 0x0017FF64
		public override Asn1Object ToAsn1Object()
		{
			return this.capabilities;
		}

		// Token: 0x04002824 RID: 10276
		public static readonly DerObjectIdentifier PreferSignedData = PkcsObjectIdentifiers.PreferSignedData;

		// Token: 0x04002825 RID: 10277
		public static readonly DerObjectIdentifier CannotDecryptAny = PkcsObjectIdentifiers.CannotDecryptAny;

		// Token: 0x04002826 RID: 10278
		public static readonly DerObjectIdentifier SmimeCapabilitesVersions = PkcsObjectIdentifiers.SmimeCapabilitiesVersions;

		// Token: 0x04002827 RID: 10279
		public static readonly DerObjectIdentifier Aes256Cbc = NistObjectIdentifiers.IdAes256Cbc;

		// Token: 0x04002828 RID: 10280
		public static readonly DerObjectIdentifier Aes192Cbc = NistObjectIdentifiers.IdAes192Cbc;

		// Token: 0x04002829 RID: 10281
		public static readonly DerObjectIdentifier Aes128Cbc = NistObjectIdentifiers.IdAes128Cbc;

		// Token: 0x0400282A RID: 10282
		public static readonly DerObjectIdentifier IdeaCbc = new DerObjectIdentifier("1.3.6.1.4.1.188.7.1.1.2");

		// Token: 0x0400282B RID: 10283
		public static readonly DerObjectIdentifier Cast5Cbc = new DerObjectIdentifier("1.2.840.113533.7.66.10");

		// Token: 0x0400282C RID: 10284
		public static readonly DerObjectIdentifier DesCbc = new DerObjectIdentifier("1.3.14.3.2.7");

		// Token: 0x0400282D RID: 10285
		public static readonly DerObjectIdentifier DesEde3Cbc = PkcsObjectIdentifiers.DesEde3Cbc;

		// Token: 0x0400282E RID: 10286
		public static readonly DerObjectIdentifier RC2Cbc = PkcsObjectIdentifiers.RC2Cbc;

		// Token: 0x0400282F RID: 10287
		private Asn1Sequence capabilities;
	}
}
