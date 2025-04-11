using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X9;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Encoders;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Anssi
{
	// Token: 0x020007BD RID: 1981
	public class AnssiNamedCurves
	{
		// Token: 0x060046BA RID: 18106 RVA: 0x00096BA2 File Offset: 0x00094DA2
		private static ECCurve ConfigureCurve(ECCurve curve)
		{
			return curve;
		}

		// Token: 0x060046BB RID: 18107 RVA: 0x0011807A File Offset: 0x0011627A
		private static BigInteger FromHex(string hex)
		{
			return new BigInteger(1, Hex.Decode(hex));
		}

		// Token: 0x060046BC RID: 18108 RVA: 0x00196E64 File Offset: 0x00195064
		private static void DefineCurve(string name, DerObjectIdentifier oid, X9ECParametersHolder holder)
		{
			AnssiNamedCurves.objIds.Add(Platform.ToUpperInvariant(name), oid);
			AnssiNamedCurves.names.Add(oid, name);
			AnssiNamedCurves.curves.Add(oid, holder);
		}

		// Token: 0x060046BD RID: 18109 RVA: 0x00196E8F File Offset: 0x0019508F
		static AnssiNamedCurves()
		{
			AnssiNamedCurves.DefineCurve("FRP256v1", AnssiObjectIdentifiers.FRP256v1, AnssiNamedCurves.Frp256v1Holder.Instance);
		}

		// Token: 0x060046BE RID: 18110 RVA: 0x00196EC4 File Offset: 0x001950C4
		public static X9ECParameters GetByName(string name)
		{
			DerObjectIdentifier oid = AnssiNamedCurves.GetOid(name);
			if (oid != null)
			{
				return AnssiNamedCurves.GetByOid(oid);
			}
			return null;
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x00196EE4 File Offset: 0x001950E4
		public static X9ECParameters GetByOid(DerObjectIdentifier oid)
		{
			X9ECParametersHolder x9ECParametersHolder = (X9ECParametersHolder)AnssiNamedCurves.curves[oid];
			if (x9ECParametersHolder != null)
			{
				return x9ECParametersHolder.Parameters;
			}
			return null;
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x00196F0D File Offset: 0x0019510D
		public static DerObjectIdentifier GetOid(string name)
		{
			return (DerObjectIdentifier)AnssiNamedCurves.objIds[Platform.ToUpperInvariant(name)];
		}

		// Token: 0x060046C1 RID: 18113 RVA: 0x00196F24 File Offset: 0x00195124
		public static string GetName(DerObjectIdentifier oid)
		{
			return (string)AnssiNamedCurves.names[oid];
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060046C2 RID: 18114 RVA: 0x00196F36 File Offset: 0x00195136
		public static IEnumerable Names
		{
			get
			{
				return new EnumerableProxy(AnssiNamedCurves.names.Values);
			}
		}

		// Token: 0x04002D63 RID: 11619
		private static readonly IDictionary objIds = Platform.CreateHashtable();

		// Token: 0x04002D64 RID: 11620
		private static readonly IDictionary curves = Platform.CreateHashtable();

		// Token: 0x04002D65 RID: 11621
		private static readonly IDictionary names = Platform.CreateHashtable();

		// Token: 0x020009B3 RID: 2483
		internal class Frp256v1Holder : X9ECParametersHolder
		{
			// Token: 0x06004FEF RID: 20463 RVA: 0x001B58C5 File Offset: 0x001B3AC5
			private Frp256v1Holder()
			{
			}

			// Token: 0x06004FF0 RID: 20464 RVA: 0x001B9F18 File Offset: 0x001B8118
			protected override X9ECParameters CreateParameters()
			{
				BigInteger q = AnssiNamedCurves.FromHex("F1FD178C0B3AD58F10126DE8CE42435B3961ADBCABC8CA6DE8FCF353D86E9C03");
				BigInteger a = AnssiNamedCurves.FromHex("F1FD178C0B3AD58F10126DE8CE42435B3961ADBCABC8CA6DE8FCF353D86E9C00");
				BigInteger b = AnssiNamedCurves.FromHex("EE353FCA5428A9300D4ABA754A44C00FDFEC0C9AE4B1A1803075ED967B7BB73F");
				byte[] seed = null;
				BigInteger bigInteger = AnssiNamedCurves.FromHex("F1FD178C0B3AD58F10126DE8CE42435B53DC67E140D2BF941FFDD459C6D655E1");
				BigInteger one = BigInteger.One;
				ECCurve eccurve = AnssiNamedCurves.ConfigureCurve(new FpCurve(q, a, b, bigInteger, one));
				X9ECPoint g = new X9ECPoint(eccurve, Hex.Decode("04B6B3D4C356C139EB31183D4749D423958C27D2DCAF98B70164C97A2DD98F5CFF6142E0F7C8B204911F9271F0F3ECEF8C2701C307E8E4C9E183115A1554062CFB"));
				return new X9ECParameters(eccurve, g, bigInteger, one, seed);
			}

			// Token: 0x0400366B RID: 13931
			internal static readonly X9ECParametersHolder Instance = new AnssiNamedCurves.Frp256v1Holder();
		}
	}
}
