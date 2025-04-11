using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Abc;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Math.EC.Multiplier
{
	// Token: 0x02000335 RID: 821
	public class WTauNafMultiplier : AbstractECMultiplier
	{
		// Token: 0x06002011 RID: 8209 RVA: 0x000F193C File Offset: 0x000EFB3C
		protected override ECPoint MultiplyPositive(ECPoint point, BigInteger k)
		{
			if (!(point is AbstractF2mPoint))
			{
				throw new ArgumentException("Only AbstractF2mPoint can be used in WTauNafMultiplier");
			}
			AbstractF2mPoint abstractF2mPoint = (AbstractF2mPoint)point;
			AbstractF2mCurve abstractF2mCurve = (AbstractF2mCurve)abstractF2mPoint.Curve;
			int fieldSize = abstractF2mCurve.FieldSize;
			sbyte b = (sbyte)abstractF2mCurve.A.ToBigInteger().IntValue;
			sbyte mu = Tnaf.GetMu((int)b);
			BigInteger[] si = abstractF2mCurve.GetSi();
			ZTauElement lambda = Tnaf.PartModReduction(k, fieldSize, b, si, mu, 10);
			return this.MultiplyWTnaf(abstractF2mPoint, lambda, b, mu);
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000F19B0 File Offset: 0x000EFBB0
		private AbstractF2mPoint MultiplyWTnaf(AbstractF2mPoint p, ZTauElement lambda, sbyte a, sbyte mu)
		{
			ZTauElement[] alpha = (a == 0) ? Tnaf.Alpha0 : Tnaf.Alpha1;
			BigInteger tw = Tnaf.GetTw(mu, 4);
			sbyte[] u = Tnaf.TauAdicWNaf(mu, lambda, 4, BigInteger.ValueOf(16L), tw, alpha);
			return WTauNafMultiplier.MultiplyFromWTnaf(p, u);
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000F19F4 File Offset: 0x000EFBF4
		private static AbstractF2mPoint MultiplyFromWTnaf(AbstractF2mPoint p, sbyte[] u)
		{
			AbstractF2mCurve abstractF2mCurve = (AbstractF2mCurve)p.Curve;
			sbyte a = (sbyte)abstractF2mCurve.A.ToBigInteger().IntValue;
			WTauNafMultiplier.WTauNafCallback callback = new WTauNafMultiplier.WTauNafCallback(p, a);
			AbstractF2mPoint[] preComp = ((WTauNafPreCompInfo)abstractF2mCurve.Precompute(p, WTauNafMultiplier.PRECOMP_NAME, callback)).PreComp;
			AbstractF2mPoint[] array = new AbstractF2mPoint[preComp.Length];
			for (int i = 0; i < preComp.Length; i++)
			{
				array[i] = (AbstractF2mPoint)preComp[i].Negate();
			}
			AbstractF2mPoint abstractF2mPoint = (AbstractF2mPoint)p.Curve.Infinity;
			int num = 0;
			for (int j = u.Length - 1; j >= 0; j--)
			{
				num++;
				int num2 = (int)u[j];
				if (num2 != 0)
				{
					abstractF2mPoint = abstractF2mPoint.TauPow(num);
					num = 0;
					ECPoint b = (num2 > 0) ? preComp[num2 >> 1] : array[-num2 >> 1];
					abstractF2mPoint = (AbstractF2mPoint)abstractF2mPoint.Add(b);
				}
			}
			if (num > 0)
			{
				abstractF2mPoint = abstractF2mPoint.TauPow(num);
			}
			return abstractF2mPoint;
		}

		// Token: 0x040018B1 RID: 6321
		internal static readonly string PRECOMP_NAME = "bc_wtnaf";

		// Token: 0x020008F5 RID: 2293
		private class WTauNafCallback : IPreCompCallback
		{
			// Token: 0x06004DA2 RID: 19874 RVA: 0x001B1722 File Offset: 0x001AF922
			internal WTauNafCallback(AbstractF2mPoint p, sbyte a)
			{
				this.m_p = p;
				this.m_a = a;
			}

			// Token: 0x06004DA3 RID: 19875 RVA: 0x001B1738 File Offset: 0x001AF938
			public PreCompInfo Precompute(PreCompInfo existing)
			{
				if (existing is WTauNafPreCompInfo)
				{
					return existing;
				}
				return new WTauNafPreCompInfo
				{
					PreComp = Tnaf.GetPreComp(this.m_p, this.m_a)
				};
			}

			// Token: 0x04003458 RID: 13400
			private readonly AbstractF2mPoint m_p;

			// Token: 0x04003459 RID: 13401
			private readonly sbyte m_a;
		}
	}
}
