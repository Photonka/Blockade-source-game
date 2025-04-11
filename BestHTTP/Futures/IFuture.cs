using System;

namespace BestHTTP.Futures
{
	// Token: 0x020007CE RID: 1998
	public interface IFuture<T>
	{
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600475F RID: 18271
		FutureState state { get; }

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06004760 RID: 18272
		T value { get; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06004761 RID: 18273
		Exception error { get; }

		// Token: 0x06004762 RID: 18274
		IFuture<T> OnItem(FutureValueCallback<T> callback);

		// Token: 0x06004763 RID: 18275
		IFuture<T> OnSuccess(FutureValueCallback<T> callback);

		// Token: 0x06004764 RID: 18276
		IFuture<T> OnError(FutureErrorCallback callback);

		// Token: 0x06004765 RID: 18277
		IFuture<T> OnComplete(FutureCallback<T> callback);
	}
}
