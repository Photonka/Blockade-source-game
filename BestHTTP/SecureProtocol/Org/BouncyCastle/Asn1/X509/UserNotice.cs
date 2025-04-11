using System;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020006A4 RID: 1700
	public class UserNotice : Asn1Encodable
	{
		// Token: 0x06003F08 RID: 16136 RVA: 0x0017BDDC File Offset: 0x00179FDC
		public UserNotice(NoticeReference noticeRef, DisplayText explicitText)
		{
			this.noticeRef = noticeRef;
			this.explicitText = explicitText;
		}

		// Token: 0x06003F09 RID: 16137 RVA: 0x0017BDF2 File Offset: 0x00179FF2
		public UserNotice(NoticeReference noticeRef, string str) : this(noticeRef, new DisplayText(str))
		{
		}

		// Token: 0x06003F0A RID: 16138 RVA: 0x0017BE04 File Offset: 0x0017A004
		[Obsolete("Use GetInstance() instead")]
		public UserNotice(Asn1Sequence seq)
		{
			if (seq.Count == 2)
			{
				this.noticeRef = NoticeReference.GetInstance(seq[0]);
				this.explicitText = DisplayText.GetInstance(seq[1]);
				return;
			}
			if (seq.Count == 1)
			{
				if (seq[0].ToAsn1Object() is Asn1Sequence)
				{
					this.noticeRef = NoticeReference.GetInstance(seq[0]);
					this.explicitText = null;
					return;
				}
				this.noticeRef = null;
				this.explicitText = DisplayText.GetInstance(seq[0]);
				return;
			}
			else
			{
				if (seq.Count == 0)
				{
					this.noticeRef = null;
					this.explicitText = null;
					return;
				}
				throw new ArgumentException("Bad sequence size: " + seq.Count);
			}
		}

		// Token: 0x06003F0B RID: 16139 RVA: 0x0017BEC6 File Offset: 0x0017A0C6
		public static UserNotice GetInstance(object obj)
		{
			if (obj is UserNotice)
			{
				return (UserNotice)obj;
			}
			if (obj == null)
			{
				return null;
			}
			return new UserNotice(Asn1Sequence.GetInstance(obj));
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06003F0C RID: 16140 RVA: 0x0017BEE7 File Offset: 0x0017A0E7
		public virtual NoticeReference NoticeRef
		{
			get
			{
				return this.noticeRef;
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x0017BEEF File Offset: 0x0017A0EF
		public virtual DisplayText ExplicitText
		{
			get
			{
				return this.explicitText;
			}
		}

		// Token: 0x06003F0E RID: 16142 RVA: 0x0017BEF8 File Offset: 0x0017A0F8
		public override Asn1Object ToAsn1Object()
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			if (this.noticeRef != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.noticeRef
				});
			}
			if (this.explicitText != null)
			{
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					this.explicitText
				});
			}
			return new DerSequence(asn1EncodableVector);
		}

		// Token: 0x040026F8 RID: 9976
		private readonly NoticeReference noticeRef;

		// Token: 0x040026F9 RID: 9977
		private readonly DisplayText explicitText;
	}
}
