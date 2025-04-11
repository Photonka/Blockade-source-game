using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200065A RID: 1626
	internal class LazyDerSequence : DerSequence
	{
		// Token: 0x06003CE1 RID: 15585 RVA: 0x001750B7 File Offset: 0x001732B7
		internal LazyDerSequence(byte[] encoded)
		{
			this.encoded = encoded;
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x001750C8 File Offset: 0x001732C8
		private void Parse()
		{
			lock (this)
			{
				if (this.encoded != null)
				{
					Asn1InputStream asn1InputStream = new LazyAsn1InputStream(this.encoded);
					Asn1Object obj;
					while ((obj = asn1InputStream.ReadObject()) != null)
					{
						base.AddObject(obj);
					}
					this.encoded = null;
				}
			}
		}

		// Token: 0x170007CD RID: 1997
		public override Asn1Encodable this[int index]
		{
			get
			{
				this.Parse();
				return base[index];
			}
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x0017513B File Offset: 0x0017333B
		public override IEnumerator GetEnumerator()
		{
			this.Parse();
			return base.GetEnumerator();
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x00175149 File Offset: 0x00173349
		public override int Count
		{
			get
			{
				this.Parse();
				return base.Count;
			}
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x00175158 File Offset: 0x00173358
		internal override void Encode(DerOutputStream derOut)
		{
			lock (this)
			{
				if (this.encoded == null)
				{
					base.Encode(derOut);
				}
				else
				{
					derOut.WriteEncoded(48, this.encoded);
				}
			}
		}

		// Token: 0x040025CD RID: 9677
		private byte[] encoded;
	}
}
