using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Sec;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Nist
{
	// Token: 0x02000703 RID: 1795
	public sealed class NistNamedCurves
	{
		// Token: 0x060041D3 RID: 16851 RVA: 0x00023EF4 File Offset: 0x000220F4
		private NistNamedCurves()
		{
		}

		// Token: 0x060041D4 RID: 16852 RVA: 0x00186D07 File Offset: 0x00184F07
		private static void DefineCurveAlias(string name, DerObjectIdentifier oid)
		{
			NistNamedCurves.objIds.Add(Platform.ToUpperInvariant(name), oid);
			NistNamedCurves.names.Add(oid, name);
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x00186D28 File Offset: 0x00184F28
		static NistNamedCurves()
		{
			NistNamedCurves.DefineCurveAlias("B-163", SecObjectIdentifiers.SecT163r2);
			NistNamedCurves.DefineCurveAlias("B-233", SecObjectIdentifiers.SecT233r1);
			NistNamedCurves.DefineCurveAlias("B-283", SecObjectIdentifiers.SecT283r1);
			NistNamedCurves.DefineCurveAlias("B-409", SecObjectIdentifiers.SecT409r1);
			NistNamedCurves.DefineCurveAlias("B-571", SecObjectIdentifiers.SecT571r1);
			NistNamedCurves.DefineCurveAlias("K-163", SecObjectIdentifiers.SecT163k1);
			NistNamedCurves.DefineCurveAlias("K-233", SecObjectIdentifiers.SecT233k1);
			NistNamedCurves.DefineCurveAlias("K-283", SecObjectIdentifiers.SecT283k1);
			NistNamedCurves.DefineCurveAlias("K-409", SecObjectIdentifiers.SecT409k1);
			NistNamedCurves.DefineCurveAlias("K-571", SecObjectIdentifiers.SecT571k1);
			NistNamedCurves.DefineCurveAlias("P-192", SecObjectIdentifiers.SecP192r1);
			NistNamedCurves.DefineCurveAlias("P-224", SecObjectIdentifiers.SecP224r1);
			NistNamedCurves.DefineCurveAlias("P-256", SecObjectIdentifiers.SecP256r1);
			NistNamedCurves.DefineCurveAlias("P-384", SecObjectIdentifiers.SecP384r1);
			NistNamedCurves.DefineCurveAlias("P-521", SecObjectIdentifiers.SecP521r1);
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x00186E2C File Offset: 0x0018502C
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier oid = NistNamedCurves.GetOid(name);
			if (oid != null)
			{
				return NistNamedCurves.GetByOid(oid);
			}
			return null;
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x00186E4B File Offset: 0x0018504B
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			return SecNamedCurves.GetByOid(oid);
		}

		// Token: 0x060041D8 RID: 16856 RVA: 0x00186E53 File Offset: 0x00185053
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)NistNamedCurves.objIds[Platform.ToUpperInvariant(name)];
		}

		// Token: 0x060041D9 RID: 16857 RVA: 0x00186E6A File Offset: 0x0018506A
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)NistNamedCurves.names[oid];
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060041DA RID: 16858 RVA: 0x00186E7C File Offset: 0x0018507C
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(NistNamedCurves.names.Values);
			}
		}

		// Token: 0x040029A8 RID: 10664
		private static readonly IDictionary objIds = Platform.CreateHashtable();

		// Token: 0x040029A9 RID: 10665
		private static readonly IDictionary names = Platform.CreateHashtable();
	}
}
