using System;
using System.Collections;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Utilities;

namespace BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1
{
	// Token: 0x0200060D RID: 1549
	public class Asn1EncodableVector : IEnumerable
	{
		// Token: 0x06003AD6 RID: 15062 RVA: 0x0016FA30 File Offset: 0x0016DC30
		public static Asn1EncodableVector FromEnumerable(IEnumerable e)
		{
			Asn1EncodableVector asn1EncodableVector = new Asn1EncodableVector(Array.Empty<Asn1Encodable>());
			foreach (object obj in e)
			{
				Asn1Encodable asn1Encodable = (Asn1Encodable)obj;
				asn1EncodableVector.Add(new Asn1Encodable[]
				{
					asn1Encodable
				});
			}
			return asn1EncodableVector;
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x0016FA9C File Offset: 0x0016DC9C
		public Asn1EncodableVector(params Asn1Encodable[] v)
		{
			this.Add(v);
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x0016FAB8 File Offset: 0x0016DCB8
		public void Add(params Asn1Encodable[] objs)
		{
			foreach (Asn1Encodable value in objs)
			{
				this.v.Add(value);
			}
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x0016FAE8 File Offset: 0x0016DCE8
		public void AddOptional(params Asn1Encodable[] objs)
		{
			if (objs != null)
			{
				foreach (Asn1Encodable asn1Encodable in objs)
				{
					if (asn1Encodable != null)
					{
						this.v.Add(asn1Encodable);
					}
				}
			}
		}

		// Token: 0x170007A9 RID: 1961
		public Asn1Encodable this[int index]
		{
			get
			{
				return (Asn1Encodable)this.v[index];
			}
		}

		// Token: 0x06003ADB RID: 15067 RVA: 0x0016FB2F File Offset: 0x0016DD2F
		[Obsolete("Use 'object[index]' syntax instead")]
		public Asn1Encodable Get(int index)
		{
			return this[index];
		}

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06003ADC RID: 15068 RVA: 0x0016FB38 File Offset: 0x0016DD38
		[Obsolete("Use 'Count' property instead")]
		public int Size
		{
			get
			{
				return this.v.Count;
			}
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06003ADD RID: 15069 RVA: 0x0016FB38 File Offset: 0x0016DD38
		public int Count
		{
			get
			{
				return this.v.Count;
			}
		}

		// Token: 0x06003ADE RID: 15070 RVA: 0x0016FB45 File Offset: 0x0016DD45
		public IEnumerator GetEnumerator()
		{
			return this.v.GetEnumerator();
		}

		// Token: 0x0400255B RID: 9563
		private IList v = Platform.CreateArrayList();
	}
}
