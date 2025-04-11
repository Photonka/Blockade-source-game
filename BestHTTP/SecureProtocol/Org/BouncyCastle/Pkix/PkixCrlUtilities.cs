using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509;
using BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Pkix
{
	// Token: 0x020002A7 RID: 679
	public class PkixCrlUtilities
	{
		// Token: 0x060018F4 RID: 6388 RVA: 0x000BFB14 File Offset: 0x000BDD14
		public virtual ISet FindCrls(X509CrlStoreSelector crlselect, PkixParameters paramsPkix, DateTime currentDate)
		{
			ISet set = new HashSet();
			try
			{
				set.AddAll(this.FindCrls(crlselect, paramsPkix.GetAdditionalStores()));
				set.AddAll(this.FindCrls(crlselect, paramsPkix.GetStores()));
			}
			catch (Exception innerException)
			{
				throw new Exception("Exception obtaining complete CRLs.", innerException);
			}
			ISet set2 = new HashSet();
			DateTime value = currentDate;
			if (paramsPkix.Date != null)
			{
				value = paramsPkix.Date.Value;
			}
			foreach (object obj in set)
			{
				X509Crl x509Crl = (X509Crl)obj;
				if (x509Crl.NextUpdate.Value.CompareTo(value) > 0)
				{
					X509Certificate certificateChecking = crlselect.CertificateChecking;
					if (certificateChecking != null)
					{
						if (x509Crl.ThisUpdate.CompareTo(certificateChecking.NotAfter) < 0)
						{
							set2.Add(x509Crl);
						}
					}
					else
					{
						set2.Add(x509Crl);
					}
				}
			}
			return set2;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000BFC20 File Offset: 0x000BDE20
		public virtual ISet FindCrls(X509CrlStoreSelector crlselect, PkixParameters paramsPkix)
		{
			ISet set = new HashSet();
			try
			{
				set.AddAll(this.FindCrls(crlselect, paramsPkix.GetStores()));
			}
			catch (Exception innerException)
			{
				throw new Exception("Exception obtaining complete CRLs.", innerException);
			}
			return set;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000BFC68 File Offset: 0x000BDE68
		private ICollection FindCrls(X509CrlStoreSelector crlSelect, IList crlStores)
		{
			ISet set = new HashSet();
			Exception ex = null;
			bool flag = false;
			foreach (object obj in crlStores)
			{
				IX509Store ix509Store = (IX509Store)obj;
				try
				{
					set.AddAll(ix509Store.GetMatches(crlSelect));
					flag = true;
				}
				catch (X509StoreException innerException)
				{
					ex = new Exception("Exception searching in X.509 CRL store.", innerException);
				}
			}
			if (!flag && ex != null)
			{
				throw ex;
			}
			return set;
		}
	}
}
