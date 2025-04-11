using System;

namespace BestHTTP.Extensions
{
	// Token: 0x020007D4 RID: 2004
	public sealed class CircularBuffer<T>
	{
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060047A5 RID: 18341 RVA: 0x001996EA File Offset: 0x001978EA
		// (set) Token: 0x060047A6 RID: 18342 RVA: 0x001996F2 File Offset: 0x001978F2
		public int Capacity { get; private set; }

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060047A7 RID: 18343 RVA: 0x001996FB File Offset: 0x001978FB
		// (set) Token: 0x060047A8 RID: 18344 RVA: 0x00199703 File Offset: 0x00197903
		public int Count { get; private set; }

		// Token: 0x17000AA1 RID: 2721
		public T this[int idx]
		{
			get
			{
				int num = (this.startIdx + idx) % this.Capacity;
				return this.buffer[num];
			}
			set
			{
				int num = (this.startIdx + idx) % this.Capacity;
				this.buffer[num] = value;
			}
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x00199762 File Offset: 0x00197962
		public CircularBuffer(int capacity)
		{
			this.Capacity = capacity;
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x00199774 File Offset: 0x00197974
		public void Add(T element)
		{
			if (this.buffer == null)
			{
				this.buffer = new T[this.Capacity];
			}
			this.buffer[this.endIdx] = element;
			this.endIdx = (this.endIdx + 1) % this.Capacity;
			if (this.endIdx == this.startIdx)
			{
				this.startIdx = (this.startIdx + 1) % this.Capacity;
			}
			this.Count = Math.Min(this.Count + 1, this.Capacity);
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x00199800 File Offset: 0x00197A00
		public void Clear()
		{
			this.startIdx = (this.endIdx = 0);
		}

		// Token: 0x04002DBF RID: 11711
		private T[] buffer;

		// Token: 0x04002DC0 RID: 11712
		private int startIdx;

		// Token: 0x04002DC1 RID: 11713
		private int endIdx;
	}
}
