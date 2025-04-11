using System;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x020006FB RID: 1787
	public class ResponseBytes : Asn1Encodable
	{
		// Token: 0x06004190 RID: 16784 RVA: 0x00186179 File Offset: 0x00184379
		public static ResponseBytes GetInstance(Asn1TaggedObject obj, bool explicitly)
		{
			return ResponseBytes.GetInstance(Asn1Sequence.GetInstance(obj, explicitly));
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x00186188 File Offset: 0x00184388
		public static ResponseBytes GetInstance(object obj)
		{
			if (obj == null || obj is ResponseBytes)
			{
				return (ResponseBytes)obj;
			}
			if (obj is Asn1Sequence)
			{
				return new ResponseBytes((Asn1Sequence)obj);
			}
			throw new ArgumentException("unknown object in factory: " + Platform.GetTypeName(obj), "obj");
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x001861D5 File Offset: 0x001843D5
		public ResponseBytes(DerObjectIdentifier responseType, Asn1OctetString response)
		{
			if (responseType == null)
			{
				throw new ArgumentNullException("responseType");
			}
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			this.responseType = responseType;
			this.response = response;
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x00186208 File Offset: 0x00184408
		private ResponseBytes(Asn1Sequence seq)
		{
			if (seq.Count != 2)
			{
				throw new ArgumentException("Wrong number of elements in sequence", "seq");
			}
			this.responseType = DerObjectIdentifier.GetInstance(seq[0]);
			this.response = Asn1OctetString.GetInstance(seq[1]);
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06004194 RID: 16788 RVA: 0x00186258 File Offset: 0x00184458
		public DerObjectIdentifier ResponseType
		{
			get
			{
				return this.responseType;
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x00186260 File Offset: 0x00184460
		public Asn1OctetString Response
		{
			get
			{
				return this.response;
			}
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x00186268 File Offset: 0x00184468
		public override Asn1Object ToAsn1Object()
		{
			return new DerSequence(new Asn1Encodable[]
			{
				this.responseType,
				this.response
			});
		}

		// Token: 0x04002987 RID: 10631
		private readonly DerObjectIdentifier responseType;

		// Token: 0x04002988 RID: 10632
		private readonly Asn1OctetString response;
	}
}
