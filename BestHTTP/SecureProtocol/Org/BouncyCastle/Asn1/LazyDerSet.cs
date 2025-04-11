using System;
using System.Collections;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200065B RID: 1627
	internal class LazyDerSet : DerSet
	{
		// Token: 0x06003CE7 RID: 15591 RVA: 0x001751AC File Offset: 0x001733AC
		internal LazyDerSet(byte[] encoded)
		{
			this.encoded = encoded;
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x001751BC File Offset: 0x001733BC
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

		// Token: 0x170007CF RID: 1999
		public override Asn1Encodable this[int index]
		{
			get
			{
				this.Parse();
				return base[index];
			}
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x0017522F File Offset: 0x0017342F
		public override IEnumerator GetEnumerator()
		{
			this.Parse();
			return base.GetEnumerator();
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x0017523D File Offset: 0x0017343D
		public override int Count
		{
			get
			{
				this.Parse();
				return base.Count;
			}
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x0017524C File Offset: 0x0017344C
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
					derOut.WriteEncoded(49, this.encoded);
				}
			}
		}

		// Token: 0x040025CE RID: 9678
		private byte[] encoded;
	}
}
