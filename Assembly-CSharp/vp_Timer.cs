using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C9 RID: 201
public class vp_Timer : MonoBehaviour
{
	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060006B7 RID: 1719 RVA: 0x000703E8 File Offset: 0x0006E5E8
	public bool WasAddedCorrectly
	{
		get
		{
			return Application.isPlaying && !(base.gameObject != vp_Timer.m_MainObject);
		}
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x00070408 File Offset: 0x0006E608
	private void Awake()
	{
		if (!this.WasAddedCorrectly)
		{
			Object.Destroy(this);
			return;
		}
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x0007041C File Offset: 0x0006E61C
	private void Update()
	{
		vp_Timer.m_EventBatch = 0;
		while (vp_Timer.m_Active.Count > 0 && vp_Timer.m_EventBatch < vp_Timer.MaxEventsPerFrame)
		{
			if (vp_Timer.m_EventIterator < 0)
			{
				vp_Timer.m_EventIterator = vp_Timer.m_Active.Count - 1;
				return;
			}
			if (vp_Timer.m_EventIterator > vp_Timer.m_Active.Count - 1)
			{
				vp_Timer.m_EventIterator = vp_Timer.m_Active.Count - 1;
			}
			if (Time.time >= vp_Timer.m_Active[vp_Timer.m_EventIterator].DueTime || vp_Timer.m_Active[vp_Timer.m_EventIterator].Id == 0)
			{
				vp_Timer.m_Active[vp_Timer.m_EventIterator].Execute();
			}
			else if (vp_Timer.m_Active[vp_Timer.m_EventIterator].Paused)
			{
				vp_Timer.m_Active[vp_Timer.m_EventIterator].DueTime += Time.deltaTime;
			}
			else
			{
				vp_Timer.m_Active[vp_Timer.m_EventIterator].LifeTime += Time.deltaTime;
			}
			vp_Timer.m_EventIterator--;
			vp_Timer.m_EventBatch++;
		}
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00070545 File Offset: 0x0006E745
	public static void In(float delay, vp_Timer.Callback callback, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, callback, null, null, timerHandle, 1, -1f);
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00070557 File Offset: 0x0006E757
	public static void In(float delay, vp_Timer.Callback callback, int iterations, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, callback, null, null, timerHandle, iterations, -1f);
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00070569 File Offset: 0x0006E769
	public static void In(float delay, vp_Timer.Callback callback, int iterations, float interval, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, callback, null, null, timerHandle, iterations, interval);
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x00070578 File Offset: 0x0006E778
	public static void In(float delay, vp_Timer.ArgCallback callback, object arguments, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, null, callback, arguments, timerHandle, 1, -1f);
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0007058A File Offset: 0x0006E78A
	public static void In(float delay, vp_Timer.ArgCallback callback, object arguments, int iterations, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, null, callback, arguments, timerHandle, iterations, -1f);
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0007059D File Offset: 0x0006E79D
	public static void In(float delay, vp_Timer.ArgCallback callback, object arguments, int iterations, float interval, vp_Timer.Handle timerHandle = null)
	{
		vp_Timer.Schedule(delay, null, callback, arguments, timerHandle, iterations, interval);
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x000705AD File Offset: 0x0006E7AD
	public static void Start(vp_Timer.Handle timerHandle)
	{
		vp_Timer.Schedule(315360000f, delegate
		{
		}, null, null, timerHandle, 1, -1f);
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x000705E4 File Offset: 0x0006E7E4
	private static void Schedule(float time, vp_Timer.Callback func, vp_Timer.ArgCallback argFunc, object args, vp_Timer.Handle timerHandle, int iterations, float interval)
	{
		if (func == null && argFunc == null)
		{
			Debug.LogError("Error: (vp_Timer) Aborted event because function is null.");
			return;
		}
		if (vp_Timer.m_MainObject == null)
		{
			vp_Timer.m_MainObject = new GameObject("Timers");
			vp_Timer.m_MainObject.AddComponent<vp_Timer>();
			Object.DontDestroyOnLoad(vp_Timer.m_MainObject);
		}
		time = Mathf.Max(0f, time);
		iterations = Mathf.Max(0, iterations);
		interval = ((interval == -1f) ? time : Mathf.Max(0f, interval));
		vp_Timer.m_NewEvent = null;
		if (vp_Timer.m_Pool.Count > 0)
		{
			vp_Timer.m_NewEvent = vp_Timer.m_Pool[0];
			vp_Timer.m_Pool.Remove(vp_Timer.m_NewEvent);
		}
		else
		{
			vp_Timer.m_NewEvent = new vp_Timer.Event();
		}
		vp_Timer.m_EventCount++;
		vp_Timer.m_NewEvent.Id = vp_Timer.m_EventCount;
		if (func != null)
		{
			vp_Timer.m_NewEvent.Function = func;
		}
		else if (argFunc != null)
		{
			vp_Timer.m_NewEvent.ArgFunction = argFunc;
			vp_Timer.m_NewEvent.Arguments = args;
		}
		vp_Timer.m_NewEvent.StartTime = Time.time;
		vp_Timer.m_NewEvent.DueTime = Time.time + time;
		vp_Timer.m_NewEvent.Iterations = iterations;
		vp_Timer.m_NewEvent.Interval = interval;
		vp_Timer.m_NewEvent.LifeTime = 0f;
		vp_Timer.m_NewEvent.Paused = false;
		vp_Timer.m_Active.Add(vp_Timer.m_NewEvent);
		if (timerHandle != null)
		{
			if (timerHandle.Active)
			{
				timerHandle.Cancel();
			}
			timerHandle.Id = vp_Timer.m_NewEvent.Id;
		}
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0007076D File Offset: 0x0006E96D
	private static void Cancel(vp_Timer.Handle handle)
	{
		if (handle == null)
		{
			return;
		}
		if (handle.Active)
		{
			handle.Id = 0;
			return;
		}
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00070784 File Offset: 0x0006E984
	public static void CancelAll()
	{
		for (int i = vp_Timer.m_Active.Count - 1; i > -1; i--)
		{
			vp_Timer.m_Active[i].Id = 0;
		}
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x000707BC File Offset: 0x0006E9BC
	public static void CancelAll(string methodName)
	{
		for (int i = vp_Timer.m_Active.Count - 1; i > -1; i--)
		{
			if (vp_Timer.m_Active[i].MethodName == methodName)
			{
				vp_Timer.m_Active[i].Id = 0;
			}
		}
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00070809 File Offset: 0x0006EA09
	public static void DestroyAll()
	{
		vp_Timer.m_Active.Clear();
		vp_Timer.m_Pool.Clear();
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x00070820 File Offset: 0x0006EA20
	private void OnLevelWasLoaded()
	{
		for (int i = vp_Timer.m_Active.Count - 1; i > -1; i--)
		{
			if (vp_Timer.m_Active[i].CancelOnLoad)
			{
				vp_Timer.m_Active[i].Id = 0;
			}
		}
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00070868 File Offset: 0x0006EA68
	public static vp_Timer.Stats EditorGetStats()
	{
		vp_Timer.Stats result;
		result.Created = vp_Timer.m_Active.Count + vp_Timer.m_Pool.Count;
		result.Inactive = vp_Timer.m_Pool.Count;
		result.Active = vp_Timer.m_Active.Count;
		return result;
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x000708B4 File Offset: 0x0006EAB4
	public static string EditorGetMethodInfo(int eventIndex)
	{
		if (eventIndex < 0 || eventIndex > vp_Timer.m_Active.Count - 1)
		{
			return "Argument out of range.";
		}
		return vp_Timer.m_Active[eventIndex].MethodInfo;
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x000708DF File Offset: 0x0006EADF
	public static int EditorGetMethodId(int eventIndex)
	{
		if (eventIndex < 0 || eventIndex > vp_Timer.m_Active.Count - 1)
		{
			return 0;
		}
		return vp_Timer.m_Active[eventIndex].Id;
	}

	// Token: 0x04000BE3 RID: 3043
	private static GameObject m_MainObject = null;

	// Token: 0x04000BE4 RID: 3044
	private static List<vp_Timer.Event> m_Active = new List<vp_Timer.Event>();

	// Token: 0x04000BE5 RID: 3045
	private static List<vp_Timer.Event> m_Pool = new List<vp_Timer.Event>();

	// Token: 0x04000BE6 RID: 3046
	private static vp_Timer.Event m_NewEvent = null;

	// Token: 0x04000BE7 RID: 3047
	private static int m_EventCount = 0;

	// Token: 0x04000BE8 RID: 3048
	private static int m_EventBatch = 0;

	// Token: 0x04000BE9 RID: 3049
	private static int m_EventIterator = 0;

	// Token: 0x04000BEA RID: 3050
	public static int MaxEventsPerFrame = 500;

	// Token: 0x02000893 RID: 2195
	// (Invoke) Token: 0x06004C61 RID: 19553
	public delegate void Callback();

	// Token: 0x02000894 RID: 2196
	// (Invoke) Token: 0x06004C65 RID: 19557
	public delegate void ArgCallback(object args);

	// Token: 0x02000895 RID: 2197
	public struct Stats
	{
		// Token: 0x0400329B RID: 12955
		public int Created;

		// Token: 0x0400329C RID: 12956
		public int Inactive;

		// Token: 0x0400329D RID: 12957
		public int Active;
	}

	// Token: 0x02000896 RID: 2198
	private class Event
	{
		// Token: 0x06004C68 RID: 19560 RVA: 0x001AE2B0 File Offset: 0x001AC4B0
		public void Execute()
		{
			if (this.Id == 0 || this.DueTime == 0f)
			{
				this.Recycle();
				return;
			}
			if (this.Function != null)
			{
				this.Function();
			}
			else
			{
				if (this.ArgFunction == null)
				{
					this.Error("Aborted event because function is null.");
					this.Recycle();
					return;
				}
				this.ArgFunction(this.Arguments);
			}
			if (this.Iterations > 0)
			{
				this.Iterations--;
				if (this.Iterations < 1)
				{
					this.Recycle();
					return;
				}
			}
			this.DueTime = Time.time + this.Interval;
		}

		// Token: 0x06004C69 RID: 19561 RVA: 0x001AE354 File Offset: 0x001AC554
		private void Recycle()
		{
			this.Id = 0;
			this.DueTime = 0f;
			this.StartTime = 0f;
			this.CancelOnLoad = true;
			this.Function = null;
			this.ArgFunction = null;
			this.Arguments = null;
			if (vp_Timer.m_Active.Remove(this))
			{
				vp_Timer.m_Pool.Add(this);
			}
		}

		// Token: 0x06004C6A RID: 19562 RVA: 0x001AE3B2 File Offset: 0x001AC5B2
		private void Destroy()
		{
			vp_Timer.m_Active.Remove(this);
			vp_Timer.m_Pool.Remove(this);
		}

		// Token: 0x06004C6B RID: 19563 RVA: 0x001AE3CC File Offset: 0x001AC5CC
		private void Error(string message)
		{
			Debug.LogError(string.Concat(new object[]
			{
				"Error: (",
				this,
				") ",
				message
			}));
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004C6C RID: 19564 RVA: 0x001AE3F8 File Offset: 0x001AC5F8
		public string MethodName
		{
			get
			{
				if (this.Function != null)
				{
					if (this.Function.Method != null)
					{
						if (this.Function.Method.Name[0] == '<')
						{
							return "delegate";
						}
						return this.Function.Method.Name;
					}
				}
				else if (this.ArgFunction != null && this.ArgFunction.Method != null)
				{
					if (this.ArgFunction.Method.Name[0] == '<')
					{
						return "delegate";
					}
					return this.ArgFunction.Method.Name;
				}
				return null;
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004C6D RID: 19565 RVA: 0x001AE4A0 File Offset: 0x001AC6A0
		public string MethodInfo
		{
			get
			{
				string text = this.MethodName;
				if (!string.IsNullOrEmpty(text))
				{
					text += "(";
					if (this.Arguments != null)
					{
						if (this.Arguments.GetType().IsArray)
						{
							object[] array = (object[])this.Arguments;
							foreach (object obj in array)
							{
								text += obj.ToString();
								if (Array.IndexOf<object>(array, obj) < array.Length - 1)
								{
									text += ", ";
								}
							}
						}
						else
						{
							text += this.Arguments;
						}
					}
					text += ")";
				}
				else
				{
					text = "(function = null)";
				}
				return text;
			}
		}

		// Token: 0x0400329E RID: 12958
		public int Id;

		// Token: 0x0400329F RID: 12959
		public vp_Timer.Callback Function;

		// Token: 0x040032A0 RID: 12960
		public vp_Timer.ArgCallback ArgFunction;

		// Token: 0x040032A1 RID: 12961
		public object Arguments;

		// Token: 0x040032A2 RID: 12962
		public int Iterations = 1;

		// Token: 0x040032A3 RID: 12963
		public float Interval = -1f;

		// Token: 0x040032A4 RID: 12964
		public float DueTime;

		// Token: 0x040032A5 RID: 12965
		public float StartTime;

		// Token: 0x040032A6 RID: 12966
		public float LifeTime;

		// Token: 0x040032A7 RID: 12967
		public bool Paused;

		// Token: 0x040032A8 RID: 12968
		public bool CancelOnLoad = true;
	}

	// Token: 0x02000897 RID: 2199
	public class Handle
	{
		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004C6F RID: 19567 RVA: 0x001AE574 File Offset: 0x001AC774
		// (set) Token: 0x06004C70 RID: 19568 RVA: 0x001AE58B File Offset: 0x001AC78B
		public bool Paused
		{
			get
			{
				return this.Active && this.m_Event.Paused;
			}
			set
			{
				if (this.Active)
				{
					this.m_Event.Paused = value;
				}
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06004C71 RID: 19569 RVA: 0x001AE5A1 File Offset: 0x001AC7A1
		public float TimeOfInitiation
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.StartTime;
				}
				return 0f;
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06004C72 RID: 19570 RVA: 0x001AE5BC File Offset: 0x001AC7BC
		public float TimeOfFirstIteration
		{
			get
			{
				if (this.Active)
				{
					return this.m_FirstDueTime;
				}
				return 0f;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06004C73 RID: 19571 RVA: 0x001AE5D2 File Offset: 0x001AC7D2
		public float TimeOfNextIteration
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.DueTime;
				}
				return 0f;
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004C74 RID: 19572 RVA: 0x001AE5ED File Offset: 0x001AC7ED
		public float TimeOfLastIteration
		{
			get
			{
				if (this.Active)
				{
					return Time.time + this.DurationLeft;
				}
				return 0f;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004C75 RID: 19573 RVA: 0x001AE609 File Offset: 0x001AC809
		public float Delay
		{
			get
			{
				return Mathf.Round((this.m_FirstDueTime - this.TimeOfInitiation) * 1000f) / 1000f;
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004C76 RID: 19574 RVA: 0x001AE629 File Offset: 0x001AC829
		public float Interval
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.Interval;
				}
				return 0f;
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06004C77 RID: 19575 RVA: 0x001AE644 File Offset: 0x001AC844
		public float TimeUntilNextIteration
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.DueTime - Time.time;
				}
				return 0f;
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06004C78 RID: 19576 RVA: 0x001AE665 File Offset: 0x001AC865
		public float DurationLeft
		{
			get
			{
				if (this.Active)
				{
					return this.TimeUntilNextIteration + (float)(this.m_Event.Iterations - 1) * this.m_Event.Interval;
				}
				return 0f;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06004C79 RID: 19577 RVA: 0x001AE696 File Offset: 0x001AC896
		public float DurationTotal
		{
			get
			{
				if (this.Active)
				{
					return this.Delay + (float)this.m_StartIterations * ((this.m_StartIterations > 1) ? this.Interval : 0f);
				}
				return 0f;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06004C7A RID: 19578 RVA: 0x001AE6CB File Offset: 0x001AC8CB
		public float Duration
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.LifeTime;
				}
				return 0f;
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06004C7B RID: 19579 RVA: 0x001AE6E6 File Offset: 0x001AC8E6
		public int IterationsTotal
		{
			get
			{
				return this.m_StartIterations;
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06004C7C RID: 19580 RVA: 0x001AE6EE File Offset: 0x001AC8EE
		public int IterationsLeft
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.Iterations;
				}
				return 0;
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06004C7D RID: 19581 RVA: 0x001AE705 File Offset: 0x001AC905
		// (set) Token: 0x06004C7E RID: 19582 RVA: 0x001AE710 File Offset: 0x001AC910
		public int Id
		{
			get
			{
				return this.m_Id;
			}
			set
			{
				this.m_Id = value;
				if (this.m_Id == 0)
				{
					this.m_Event.DueTime = 0f;
					return;
				}
				this.m_Event = null;
				for (int i = vp_Timer.m_Active.Count - 1; i > -1; i--)
				{
					if (vp_Timer.m_Active[i].Id == this.m_Id)
					{
						this.m_Event = vp_Timer.m_Active[i];
						break;
					}
				}
				if (this.m_Event == null)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"Error: (",
						this,
						") Failed to assign event with Id '",
						this.m_Id,
						"'."
					}));
				}
				this.m_StartIterations = this.m_Event.Iterations;
				this.m_FirstDueTime = this.m_Event.DueTime;
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x06004C7F RID: 19583 RVA: 0x001AE7EA File Offset: 0x001AC9EA
		public bool Active
		{
			get
			{
				return this.m_Event != null && this.Id != 0 && this.m_Event.Id != 0 && this.m_Event.Id == this.Id;
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004C80 RID: 19584 RVA: 0x001AE81E File Offset: 0x001ACA1E
		public string MethodName
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.MethodName;
				}
				return "";
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06004C81 RID: 19585 RVA: 0x001AE839 File Offset: 0x001ACA39
		public string MethodInfo
		{
			get
			{
				if (this.Active)
				{
					return this.m_Event.MethodInfo;
				}
				return "";
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06004C82 RID: 19586 RVA: 0x001AE854 File Offset: 0x001ACA54
		// (set) Token: 0x06004C83 RID: 19587 RVA: 0x001AE86B File Offset: 0x001ACA6B
		public bool CancelOnLoad
		{
			get
			{
				return !this.Active || this.m_Event.CancelOnLoad;
			}
			set
			{
				if (this.Active)
				{
					this.m_Event.CancelOnLoad = value;
					return;
				}
				Debug.LogWarning("Warning: (" + this + ") Tried to set CancelOnLoad on inactive timer handle.");
			}
		}

		// Token: 0x06004C84 RID: 19588 RVA: 0x001AE897 File Offset: 0x001ACA97
		public void Cancel()
		{
			vp_Timer.Cancel(this);
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x001AE89F File Offset: 0x001ACA9F
		public void Execute()
		{
			this.m_Event.DueTime = Time.time;
		}

		// Token: 0x040032A9 RID: 12969
		private vp_Timer.Event m_Event;

		// Token: 0x040032AA RID: 12970
		private int m_Id;

		// Token: 0x040032AB RID: 12971
		private int m_StartIterations = 1;

		// Token: 0x040032AC RID: 12972
		private float m_FirstDueTime;
	}
}
