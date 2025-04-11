using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.X509.Store
{
	// Token: 0x02000246 RID: 582
	public sealed class X509StoreFactory
	{
		// Token: 0x060015A3 RID: 5539 RVA: 0x00023EF4 File Offset: 0x000220F4
		private X509StoreFactory()
		{
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x000B12D8 File Offset: 0x000AF4D8
		public static IX509Store Create(string type, IX509StoreParameters parameters)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			string[] array = Platform.ToUpperInvariant(type).Split(new char[]
			{
				'/'
			});
			if (array.Length < 2)
			{
				throw new ArgumentException("type");
			}
			if (array[1] != "COLLECTION")
			{
				throw new NoSuchStoreException("X.509 store type '" + type + "' not available.");
			}
			ICollection collection = ((X509CollectionStoreParameters)parameters).GetCollection();
			string a = array[0];
			if (!(a == "ATTRIBUTECERTIFICATE"))
			{
				if (!(a == "CERTIFICATE"))
				{
					if (!(a == "CERTIFICATEPAIR"))
					{
						if (!(a == "CRL"))
						{
							throw new NoSuchStoreException("X.509 store type '" + type + "' not available.");
						}
						X509StoreFactory.checkCorrectType(collection, typeof(X509Crl));
					}
					else
					{
						X509StoreFactory.checkCorrectType(collection, typeof(X509CertificatePair));
					}
				}
				else
				{
					X509StoreFactory.checkCorrectType(collection, typeof(X509Certificate));
				}
			}
			else
			{
				X509StoreFactory.checkCorrectType(collection, typeof(IX509AttributeCertificate));
			}
			return new X509CollectionStore(collection);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x000B13E8 File Offset: 0x000AF5E8
		private static void checkCorrectType(ICollection coll, Type t)
		{
			foreach (object o in coll)
			{
				if (!t.IsInstanceOfType(o))
				{
					throw new InvalidCastException("Can't cast object to type: " + t.FullName);
				}
			}
		}
	}
}
