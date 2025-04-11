using System;
using System.Collections;

namespace PlatformSupport.Collections.Specialized
{
	// Token: 0x02000166 RID: 358
	public class NotifyCollectionChangedEventArgs : EventArgs
	{
		// Token: 0x06000C8E RID: 3214 RVA: 0x00091D16 File Offset: 0x0008FF16
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
		{
			if (action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException("action");
			}
			this.InitializeAdd(action, null, -1);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00091D44 File Offset: 0x0008FF44
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException("action");
			}
			if (action != NotifyCollectionChangedAction.Reset)
			{
				this.InitializeAddOrRemove(action, new object[]
				{
					changedItem
				}, -1);
				return;
			}
			if (changedItem != null)
			{
				throw new ArgumentException("action");
			}
			this.InitializeAdd(action, null, -1);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00091DAC File Offset: 0x0008FFAC
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException("action");
			}
			if (action != NotifyCollectionChangedAction.Reset)
			{
				this.InitializeAddOrRemove(action, new object[]
				{
					changedItem
				}, index);
				return;
			}
			if (changedItem != null)
			{
				throw new ArgumentException("action");
			}
			if (index != -1)
			{
				throw new ArgumentException("action");
			}
			this.InitializeAdd(action, null, -1);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x00091E20 File Offset: 0x00090020
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException("action");
			}
			if (action == NotifyCollectionChangedAction.Reset)
			{
				if (changedItems != null)
				{
					throw new ArgumentException("action");
				}
				this.InitializeAdd(action, null, -1);
				return;
			}
			else
			{
				if (changedItems == null)
				{
					throw new ArgumentNullException("changedItems");
				}
				this.InitializeAddOrRemove(action, changedItems, -1);
				return;
			}
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00091E8C File Offset: 0x0009008C
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
		{
			if (action != NotifyCollectionChangedAction.Add && action != NotifyCollectionChangedAction.Remove && action != NotifyCollectionChangedAction.Reset)
			{
				throw new ArgumentException("action");
			}
			if (action == NotifyCollectionChangedAction.Reset)
			{
				if (changedItems != null)
				{
					throw new ArgumentException("action");
				}
				if (startingIndex != -1)
				{
					throw new ArgumentException("action");
				}
				this.InitializeAdd(action, null, -1);
				return;
			}
			else
			{
				if (changedItems == null)
				{
					throw new ArgumentNullException("changedItems");
				}
				if (startingIndex < -1)
				{
					throw new ArgumentException("startingIndex");
				}
				this.InitializeAddOrRemove(action, changedItems, startingIndex);
				return;
			}
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00091F14 File Offset: 0x00090114
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException("action");
			}
			this.InitializeMoveOrReplace(action, new object[]
			{
				newItem
			}, new object[]
			{
				oldItem
			}, -1, -1);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x00091F64 File Offset: 0x00090164
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException("action");
			}
			this.InitializeMoveOrReplace(action, new object[]
			{
				newItem
			}, new object[]
			{
				oldItem
			}, index, index);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00091FB4 File Offset: 0x000901B4
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException("action");
			}
			if (newItems == null)
			{
				throw new ArgumentNullException("newItems");
			}
			if (oldItems == null)
			{
				throw new ArgumentNullException("oldItems");
			}
			this.InitializeMoveOrReplace(action, newItems, oldItems, -1, -1);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0009200C File Offset: 0x0009020C
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
		{
			if (action != NotifyCollectionChangedAction.Replace)
			{
				throw new ArgumentException("action");
			}
			if (newItems == null)
			{
				throw new ArgumentNullException("newItems");
			}
			if (oldItems == null)
			{
				throw new ArgumentNullException("oldItems");
			}
			this.InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x00092068 File Offset: 0x00090268
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index, int oldIndex)
		{
			if (action != NotifyCollectionChangedAction.Move)
			{
				throw new ArgumentException("action");
			}
			if (index < 0)
			{
				throw new ArgumentException("index");
			}
			object[] array = new object[]
			{
				changedItem
			};
			this.InitializeMoveOrReplace(action, array, array, index, oldIndex);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000920BE File Offset: 0x000902BE
		public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int index, int oldIndex)
		{
			if (action != NotifyCollectionChangedAction.Move)
			{
				throw new ArgumentException("action");
			}
			if (index < 0)
			{
				throw new ArgumentException("index");
			}
			this.InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x00092100 File Offset: 0x00090300
		internal NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int newIndex, int oldIndex)
		{
			this._action = action;
			this._newItems = ((newItems == null) ? null : new ReadOnlyList(newItems));
			this._oldItems = ((oldItems == null) ? null : new ReadOnlyList(oldItems));
			this._newStartingIndex = newIndex;
			this._oldStartingIndex = oldIndex;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0009215C File Offset: 0x0009035C
		private void InitializeAddOrRemove(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
		{
			if (action == NotifyCollectionChangedAction.Add)
			{
				this.InitializeAdd(action, changedItems, startingIndex);
				return;
			}
			if (action == NotifyCollectionChangedAction.Remove)
			{
				this.InitializeRemove(action, changedItems, startingIndex);
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00092178 File Offset: 0x00090378
		private void InitializeAdd(NotifyCollectionChangedAction action, IList newItems, int newStartingIndex)
		{
			this._action = action;
			this._newItems = ((newItems == null) ? null : new ReadOnlyList(newItems));
			this._newStartingIndex = newStartingIndex;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0009219A File Offset: 0x0009039A
		private void InitializeRemove(NotifyCollectionChangedAction action, IList oldItems, int oldStartingIndex)
		{
			this._action = action;
			this._oldItems = ((oldItems == null) ? null : new ReadOnlyList(oldItems));
			this._oldStartingIndex = oldStartingIndex;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000921BC File Offset: 0x000903BC
		private void InitializeMoveOrReplace(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex, int oldStartingIndex)
		{
			this.InitializeAdd(action, newItems, startingIndex);
			this.InitializeRemove(action, oldItems, oldStartingIndex);
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x000921D2 File Offset: 0x000903D2
		public NotifyCollectionChangedAction Action
		{
			get
			{
				return this._action;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x000921DA File Offset: 0x000903DA
		public IList NewItems
		{
			get
			{
				return this._newItems;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x000921E2 File Offset: 0x000903E2
		public IList OldItems
		{
			get
			{
				return this._oldItems;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000CA1 RID: 3233 RVA: 0x000921EA File Offset: 0x000903EA
		public int NewStartingIndex
		{
			get
			{
				return this._newStartingIndex;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000921F2 File Offset: 0x000903F2
		public int OldStartingIndex
		{
			get
			{
				return this._oldStartingIndex;
			}
		}

		// Token: 0x0400116B RID: 4459
		private NotifyCollectionChangedAction _action;

		// Token: 0x0400116C RID: 4460
		private IList _newItems;

		// Token: 0x0400116D RID: 4461
		private IList _oldItems;

		// Token: 0x0400116E RID: 4462
		private int _newStartingIndex = -1;

		// Token: 0x0400116F RID: 4463
		private int _oldStartingIndex = -1;
	}
}
