using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Anssi;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.CryptoPro;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.GM;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Sec;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.TeleTrust;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Crypto.Parameters;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9
{
	// Token: 0x02000661 RID: 1633
	public class ECNamedCurveTable
	{
		// Token: 0x06003D0A RID: 15626 RVA: 0x00175738 File Offset: 0x00173938
		public static X9ECParameters GetByName(string name)
		{
			X9ECParameters x9ECParameters = X962NamedCurves.GetByName(name);
			if (x9ECParameters == null)
			{
				x9ECParameters = SecNamedCurves.GetByName(name);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = NistNamedCurves.GetByName(name);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = TeleTrusTNamedCurves.GetByName(name);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = AnssiNamedCurves.GetByName(name);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = ECNamedCurveTable.FromDomainParameters(ECGost3410NamedCurves.GetByName(name));
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = GMNamedCurves.GetByName(name);
			}
			return x9ECParameters;
		}

		// Token: 0x06003D0B RID: 15627 RVA: 0x00175790 File Offset: 0x00173990
		public static string GetName(DerObjectIdentifier oid)
		{
			string name = X962NamedCurves.GetName(oid);
			if (name == null)
			{
				name = SecNamedCurves.GetName(oid);
			}
			if (name == null)
			{
				name = NistNamedCurves.GetName(oid);
			}
			if (name == null)
			{
				name = TeleTrusTNamedCurves.GetName(oid);
			}
			if (name == null)
			{
				name = AnssiNamedCurves.GetName(oid);
			}
			if (name == null)
			{
				name = ECGost3410NamedCurves.GetName(oid);
			}
			if (name == null)
			{
				name = GMNamedCurves.GetName(oid);
			}
			return name;
		}

		// Token: 0x06003D0C RID: 15628 RVA: 0x001757E4 File Offset: 0x001739E4
		public static DerObjectIdentifier GetOid(string name)
		{
			DerObjectIdentifier oid = X962NamedCurves.GetOid(name);
			if (oid == null)
			{
				oid = SecNamedCurves.GetOid(name);
			}
			if (oid == null)
			{
				oid = NistNamedCurves.GetOid(name);
			}
			if (oid == null)
			{
				oid = TeleTrusTNamedCurves.GetOid(name);
			}
			if (oid == null)
			{
				oid = AnssiNamedCurves.GetOid(name);
			}
			if (oid == null)
			{
				oid = ECGost3410NamedCurves.GetOid(name);
			}
			if (oid == null)
			{
				oid = GMNamedCurves.GetOid(name);
			}
			return oid;
		}

		// Token: 0x06003D0D RID: 15629 RVA: 0x00175838 File Offset: 0x00173A38
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParameters x9ECParameters = X962NamedCurves.GetByOid(oid);
			if (x9ECParameters == null)
			{
				x9ECParameters = SecNamedCurves.GetByOid(oid);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = TeleTrusTNamedCurves.GetByOid(oid);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = AnssiNamedCurves.GetByOid(oid);
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = ECNamedCurveTable.FromDomainParameters(ECGost3410NamedCurves.GetByOid(oid));
			}
			if (x9ECParameters == null)
			{
				x9ECParameters = GMNamedCurves.GetByOid(oid);
			}
			return x9ECParameters;
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x00175884 File Offset: 0x00173A84
		public static IEnumerable Names
		{
			get
			{
				IList list = Platform.CreateArrayList();
				CollectionUtilities.AddRange(list, X962NamedCurves.Names);
				CollectionUtilities.AddRange(list, SecNamedCurves.Names);
				CollectionUtilities.AddRange(list, NistNamedCurves.Names);
				CollectionUtilities.AddRange(list, TeleTrusTNamedCurves.Names);
				CollectionUtilities.AddRange(list, AnssiNamedCurves.Names);
				CollectionUtilities.AddRange(list, ECGost3410NamedCurves.Names);
				CollectionUtilities.AddRange(list, GMNamedCurves.Names);
				return list;
			}
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x001758E3 File Offset: 0x00173AE3
		private static X9ECParameters FromDomainParameters(ECDomainParameters dp)
		{
			if (dp != null)
			{
				return new X9ECParameters(dp.Curve, dp.G, dp.N, dp.H, dp.GetSeed());
			}
			return null;
		}
	}
}
