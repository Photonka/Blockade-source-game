using System;
using System.Collections.Generic;
using System.Threading;

namespace BestHTTP.Futures
{
	// Token: 0x020007D2 RID: 2002
	public class Future<T> : IFuture<T>
	{
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06004772 RID: 18290 RVA: 0x00198AD2 File Offset: 0x00196CD2
		public FutureState state
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06004773 RID: 18291 RVA: 0x00198ADC File Offset: 0x00196CDC
		public T value
		{
			get
			{
				if (this._state != FutureState.Success && this._state != FutureState.Processing)
				{
					throw new InvalidOperationException("value is not available unless state is Success or Processing.");
				}
				return this._value;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06004774 RID: 18292 RVA: 0x00198B05 File Offset: 0x00196D05
		public Exception error
		{
			get
			{
				if (this._state != FutureState.Error)
				{
					throw new InvalidOperationException("error is not available unless state is Error.");
				}
				return this._error;
			}
		}

		// Token: 0x06004775 RID: 18293 RVA: 0x00198B23 File Offset: 0x00196D23
		public Future()
		{
			this._state = FutureState.Pending;
		}

		// Token: 0x06004776 RID: 18294 RVA: 0x00198B60 File Offset: 0x00196D60
		public IFuture<T> OnItem(FutureValueCallback<T> callback)
		{
			if (this._state < FutureState.Success && !this._itemCallbacks.Contains(callback))
			{
				this._itemCallbacks.Add(callback);
			}
			return this;
		}

		// Token: 0x06004777 RID: 18295 RVA: 0x00198B88 File Offset: 0x00196D88
		public IFuture<T> OnSuccess(FutureValueCallback<T> callback)
		{
			if (this._state == FutureState.Success)
			{
				callback(this.value);
			}
			else if (this._state != FutureState.Error && !this._successCallbacks.Contains(callback))
			{
				this._successCallbacks.Add(callback);
			}
			return this;
		}

		// Token: 0x06004778 RID: 18296 RVA: 0x00198BD4 File Offset: 0x00196DD4
		public IFuture<T> OnError(FutureErrorCallback callback)
		{
			if (this._state == FutureState.Error)
			{
				callback(this.error);
			}
			else if (this._state != FutureState.Success && !this._errorCallbacks.Contains(callback))
			{
				this._errorCallbacks.Add(callback);
			}
			return this;
		}

		// Token: 0x06004779 RID: 18297 RVA: 0x00198C20 File Offset: 0x00196E20
		public IFuture<T> OnComplete(FutureCallback<T> callback)
		{
			if (this._state == FutureState.Success || this._state == FutureState.Error)
			{
				callback(this);
			}
			else if (!this._complationCallbacks.Contains(callback))
			{
				this._complationCallbacks.Add(callback);
			}
			return this;
		}

		// Token: 0x0600477A RID: 18298 RVA: 0x00198C5C File Offset: 0x00196E5C
		public IFuture<T> Process(Func<T> func)
		{
			if (this._state != FutureState.Pending)
			{
				throw new InvalidOperationException("Cannot process a future that isn't in the Pending state.");
			}
			this.BeginProcess(default(T));
			this._processFunc = func;
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.ThreadFunc));
			return this;
		}

		// Token: 0x0600477B RID: 18299 RVA: 0x00198CA8 File Offset: 0x00196EA8
		private void ThreadFunc(object param)
		{
			try
			{
				this.AssignImpl(this._processFunc());
			}
			catch (Exception error)
			{
				this.FailImpl(error);
			}
			finally
			{
				this._processFunc = null;
			}
		}

		// Token: 0x0600477C RID: 18300 RVA: 0x00198CF8 File Offset: 0x00196EF8
		public void Assign(T value)
		{
			if (this._state != FutureState.Pending && this._state != FutureState.Processing)
			{
				throw new InvalidOperationException("Cannot assign a value to a future that isn't in the Pending or Processing state.");
			}
			this.AssignImpl(value);
		}

		// Token: 0x0600477D RID: 18301 RVA: 0x00198D21 File Offset: 0x00196F21
		public void BeginProcess(T initialItem = default(T))
		{
			this._state = FutureState.Processing;
			this._value = initialItem;
		}

		// Token: 0x0600477E RID: 18302 RVA: 0x00198D34 File Offset: 0x00196F34
		public void AssignItem(T value)
		{
			this._value = value;
			this._error = null;
			foreach (FutureValueCallback<T> futureValueCallback in this._itemCallbacks)
			{
				futureValueCallback(this.value);
			}
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x00198D98 File Offset: 0x00196F98
		public void Fail(Exception error)
		{
			if (this._state != FutureState.Pending && this._state != FutureState.Processing)
			{
				throw new InvalidOperationException("Cannot fail future that isn't in the Pending or Processing state.");
			}
			this.FailImpl(error);
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x00198DC1 File Offset: 0x00196FC1
		private void AssignImpl(T value)
		{
			this._value = value;
			this._error = null;
			this._state = FutureState.Success;
			this.FlushSuccessCallbacks();
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x00198DE0 File Offset: 0x00196FE0
		private void FailImpl(Exception error)
		{
			this._value = default(T);
			this._error = error;
			this._state = FutureState.Error;
			this.FlushErrorCallbacks();
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x00198E04 File Offset: 0x00197004
		private void FlushSuccessCallbacks()
		{
			foreach (FutureValueCallback<T> futureValueCallback in this._successCallbacks)
			{
				futureValueCallback(this.value);
			}
			this.FlushComplationCallbacks();
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x00198E60 File Offset: 0x00197060
		private void FlushErrorCallbacks()
		{
			foreach (FutureErrorCallback futureErrorCallback in this._errorCallbacks)
			{
				futureErrorCallback(this.error);
			}
			this.FlushComplationCallbacks();
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x00198EBC File Offset: 0x001970BC
		private void FlushComplationCallbacks()
		{
			foreach (FutureCallback<T> futureCallback in this._complationCallbacks)
			{
				futureCallback(this);
			}
			this.ClearCallbacks();
		}

		// Token: 0x06004785 RID: 18309 RVA: 0x00198F14 File Offset: 0x00197114
		private void ClearCallbacks()
		{
			this._itemCallbacks.Clear();
			this._successCallbacks.Clear();
			this._errorCallbacks.Clear();
			this._complationCallbacks.Clear();
		}

		// Token: 0x04002DAB RID: 11691
		private volatile FutureState _state;

		// Token: 0x04002DAC RID: 11692
		private T _value;

		// Token: 0x04002DAD RID: 11693
		private Exception _error;

		// Token: 0x04002DAE RID: 11694
		private Func<T> _processFunc;

		// Token: 0x04002DAF RID: 11695
		private readonly List<FutureValueCallback<T>> _itemCallbacks = new List<FutureValueCallback<T>>();

		// Token: 0x04002DB0 RID: 11696
		private readonly List<FutureValueCallback<T>> _successCallbacks = new List<FutureValueCallback<T>>();

		// Token: 0x04002DB1 RID: 11697
		private readonly List<FutureErrorCallback> _errorCallbacks = new List<FutureErrorCallback>();

		// Token: 0x04002DB2 RID: 11698
		private readonly List<FutureCallback<T>> _complationCallbacks = new List<FutureCallback<T>>();
	}
}
