using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006A2 RID: 1698
	public class TbsCertificateList : Asn1Encodable
	{
		// Token: 0x06003EF3 RID: 16115 RVA: 0x0017B9A8 File Offset: 0x00179BA8
		public static TbsCertificateList GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return TbsCertificateList.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x0017B9B8 File Offset: 0x00179BB8
		public static TbsCertificateList GetInstance(object obj)
		{
			TbsCertificateList tbsCertificateList = obj as TbsCertificateList;
			if (obj == null || tbsCertificateList != null)
			{
				return tbsCertificateList;
			}
			if (obj is Asn1Sequence)
			{
				return new TbsCertificateList((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06003EF5 RID: 16117 RVA: 0x0017BA04 File Offset: 0x00179C04
		internal TbsCertificateList(Asn1Sequence seq)
		{
			if (seq.Count < 3 || seq.Count > 7)
			{
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
			int num = 0;
			this.seq = seq;
			if (seq[num] is DerInteger)
			{
				this.version = DerInteger.GetInstance(seq[num++]);
			}
			else
			{
				this.version = new DerInteger(0);
			}
			this.signature = AlgorithmIdentifier.GetInstance(seq[num++]);
			this.issuer = X509Name.GetInstance(seq[num++]);
			this.thisUpdate = Time.GetInstance(seq[num++]);
			if (num < seq.Count && (seq[num] is DerUtcTime || seq[num] is DerGeneralizedTime || seq[num] is Time))
			{
				this.nextUpdate = Time.GetInstance(seq[num++]);
			}
			if (num < seq.Count && !(seq[num] is DerTaggedObject))
			{
				this.revokedCertificates = Asn1Sequence.GetInstance(seq[num++]);
			}
			if (num < seq.Count && seq[num] is DerTaggedObject)
			{
				this.crlExtensions = X509Extensions.GetInstance(seq[num]);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06003EF6 RID: 16118 RVA: 0x0017BB60 File Offset: 0x00179D60
		public int Version
		{
			get
			{
				return this.version.Value.IntValue + 1;
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06003EF7 RID: 16119 RVA: 0x0017BB74 File Offset: 0x00179D74
		public DerInteger VersionNumber
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06003EF8 RID: 16120 RVA: 0x0017BB7C File Offset: 0x00179D7C
		public AlgorithmIdentifier Signature
		{
			get
			{
				return this.signature;
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06003EF9 RID: 16121 RVA: 0x0017BB84 File Offset: 0x00179D84
		public X509Name Issuer
		{
			get
			{
				return this.issuer;
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x0017BB8C File Offset: 0x00179D8C
		public Time ThisUpdate
		{
			get
			{
				return this.thisUpdate;
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06003EFB RID: 16123 RVA: 0x0017BB94 File Offset: 0x00179D94
		public Time NextUpdate
		{
			get
			{
				return this.nextUpdate;
			}
		}

		// Token: 0x06003EFC RID: 16124 RVA: 0x0017BB9C File Offset: 0x00179D9C
		public CrlEntry[] GetRevokedCertificates()
		{
			if (this.revokedCertificates == null)
			{
				return new CrlEntry[0];
			}
			CrlEntry[] array = new CrlEntry[this.revokedCertificates.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new CrlEntry(Asn1Sequence.GetInstance(this.revokedCertificates[i]));
			}
			return array;
		}

		// Token: 0x06003EFD RID: 16125 RVA: 0x0017BBF1 File Offset: 0x00179DF1
		public IEnumerable GetRevokedCertificateEnumeration()
		{
			if (this.revokedCertificates == null)
			{
				return EmptyEnumerable.Instance;
			}
			return new TbsCertificateList.RevokedCertificatesEnumeration(this.revokedCertificates);
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x0017BC0C File Offset: 0x00179E0C
		public X509Extensions Extensions
		{
			get
			{
				return this.crlExtensions;
			}
		}

		// Token: 0x06003EFF RID: 16127 RVA: 0x0017BC14 File Offset: 0x00179E14
		public override Asn1Object ToAsn1Object()
		{
			return this.seq;
		}

		// Token: 0x040026EF RID: 9967
		internal Asn1Sequence seq;

		// Token: 0x040026F0 RID: 9968
		internal DerInteger version;

		// Token: 0x040026F1 RID: 9969
		internal AlgorithmIdentifier signature;

		// Token: 0x040026F2 RID: 9970
		internal X509Name issuer;

		// Token: 0x040026F3 RID: 9971
		internal Time thisUpdate;

		// Token: 0x040026F4 RID: 9972
		internal Time nextUpdate;

		// Token: 0x040026F5 RID: 9973
		internal Asn1Sequence revokedCertificates;

		// Token: 0x040026F6 RID: 9974
		internal X509Extensions crlExtensions;

		// Token: 0x0200097F RID: 2431
		private class RevokedCertificatesEnumeration : IEnumerable
		{
			// Token: 0x06004F5A RID: 20314 RVA: 0x001B8540 File Offset: 0x001B6740
			internal RevokedCertificatesEnumeration(IEnumerable en)
			{
				this.en = en;
			}

			// Token: 0x06004F5B RID: 20315 RVA: 0x001B854F File Offset: 0x001B674F
			public IEnumerator GetEnumerator()
			{
				return new TbsCertificateList.RevokedCertificatesEnumeration.RevokedCertificatesEnumerator(this.en.GetEnumerator());
			}

			// Token: 0x040035FB RID: 13819
			private readonly IEnumerable en;

			// Token: 0x020009EC RID: 2540
			private class RevokedCertificatesEnumerator : IEnumerator
			{
				// Token: 0x0600501A RID: 20506 RVA: 0x001BA448 File Offset: 0x001B8648
				internal RevokedCertificatesEnumerator(IEnumerator e)
				{
					this.e = e;
				}

				// Token: 0x0600501B RID: 20507 RVA: 0x001BA457 File Offset: 0x001B8657
				public bool MoveNext()
				{
					return this.e.MoveNext();
				}

				// Token: 0x0600501C RID: 20508 RVA: 0x001BA464 File Offset: 0x001B8664
				public void Reset()
				{
					this.e.Reset();
				}

				// Token: 0x17000C53 RID: 3155
				// (get) Token: 0x0600501D RID: 20509 RVA: 0x001BA471 File Offset: 0x001B8671
				public object Current
				{
					get
					{
						return new CrlEntry(Asn1Sequence.GetInstance(this.e.Current));
					}
				}

				// Token: 0x040036AD RID: 13997
				private readonly IEnumerator e;
			}
		}
	}
}
