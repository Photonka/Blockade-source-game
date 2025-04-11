using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B4 RID: 180
public class vp_Component : MonoBehaviour
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060005CF RID: 1487 RVA: 0x0006ACB1 File Offset: 0x00068EB1
	public vp_StateManager StateManager
	{
		get
		{
			return this.m_StateManager;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0006ACB9 File Offset: 0x00068EB9
	public vp_State DefaultState
	{
		get
		{
			return this.m_DefaultState;
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0006ACC1 File Offset: 0x00068EC1
	public float Delta
	{
		get
		{
			return Time.deltaTime * 60f;
		}
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0006ACCE File Offset: 0x00068ECE
	public float SDelta
	{
		get
		{
			return Time.smoothDeltaTime * 60f;
		}
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0006ACDB File Offset: 0x00068EDB
	public Transform Transform
	{
		get
		{
			return this.m_Transform;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0006ACE3 File Offset: 0x00068EE3
	public Transform Parent
	{
		get
		{
			return this.m_Parent;
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0006ACEB File Offset: 0x00068EEB
	public Transform Root
	{
		get
		{
			return this.m_Root;
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0006ACF3 File Offset: 0x00068EF3
	public AudioSource Audio
	{
		get
		{
			return this.m_Audio;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0006ACFB File Offset: 0x00068EFB
	// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0006AD20 File Offset: 0x00068F20
	public bool Rendering
	{
		get
		{
			return this.Renderers.Count > 0 && this.Renderers[0].enabled;
		}
		set
		{
			foreach (Renderer renderer in this.Renderers)
			{
				if (!(renderer == null))
				{
					renderer.enabled = value;
				}
			}
		}
	}

	// Token: 0x060005D9 RID: 1497 RVA: 0x0006AD7C File Offset: 0x00068F7C
	protected virtual void Awake()
	{
		this.m_Transform = base.transform;
		this.m_Parent = base.transform.parent;
		this.m_Root = base.transform.root;
		this.m_Audio = base.GetComponent<AudioSource>();
		this.EventHandler = (vp_EventHandler)this.m_Transform.root.GetComponentInChildren(typeof(vp_EventHandler));
		this.CacheChildren();
		this.CacheSiblings();
		this.CacheFamily();
		this.CacheRenderers();
		this.CacheAudioSources();
		this.m_StateManager = new vp_StateManager(this, this.States);
		this.StateManager.SetState("Default", base.enabled);
	}

	// Token: 0x060005DA RID: 1498 RVA: 0x0006AE2E File Offset: 0x0006902E
	protected virtual void Start()
	{
		this.ResetState();
	}

	// Token: 0x060005DB RID: 1499 RVA: 0x00002B75 File Offset: 0x00000D75
	protected virtual void Init()
	{
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0006AE36 File Offset: 0x00069036
	protected virtual void OnEnable()
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Register(this);
		}
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0006AE52 File Offset: 0x00069052
	protected virtual void OnDisable()
	{
		if (this.EventHandler != null)
		{
			this.EventHandler.Unregister(this);
		}
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x0006AE6E File Offset: 0x0006906E
	protected virtual void Update()
	{
		if (!this.m_Initialized)
		{
			this.Init();
			this.m_Initialized = true;
		}
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x00002B75 File Offset: 0x00000D75
	protected virtual void FixedUpdate()
	{
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x00002B75 File Offset: 0x00000D75
	protected virtual void LateUpdate()
	{
	}

	// Token: 0x060005E1 RID: 1505 RVA: 0x0006AE88 File Offset: 0x00069088
	public void SetState(string state, bool enabled = true, bool recursive = false, bool includeDisabled = false)
	{
		this.m_StateManager.SetState(state, enabled);
		if (recursive)
		{
			foreach (vp_Component vp_Component in this.Children)
			{
				if (includeDisabled || (vp_Utility.IsActive(vp_Component.gameObject) && vp_Component.enabled))
				{
					vp_Component.SetState(state, enabled, true, includeDisabled);
				}
			}
		}
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0006AF08 File Offset: 0x00069108
	public void ActivateGameObject(bool setActive = true)
	{
		if (setActive)
		{
			this.Activate();
			using (List<vp_Component>.Enumerator enumerator = this.Siblings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					vp_Component vp_Component = enumerator.Current;
					vp_Component.Activate();
				}
				return;
			}
		}
		this.DeactivateWhenSilent();
		foreach (vp_Component vp_Component2 in this.Siblings)
		{
			vp_Component2.DeactivateWhenSilent();
		}
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0006AFA8 File Offset: 0x000691A8
	public void ResetState()
	{
		this.m_StateManager.Reset();
		this.Refresh();
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0006AFBB File Offset: 0x000691BB
	public bool StateEnabled(string stateName)
	{
		return this.m_StateManager.IsEnabled(stateName);
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0006AFCC File Offset: 0x000691CC
	public void RefreshDefaultState()
	{
		vp_State vp_State = null;
		if (this.States.Count == 0)
		{
			vp_State = new vp_State(base.GetType().Name, "Default", null, null);
			this.States.Add(vp_State);
		}
		else
		{
			for (int i = this.States.Count - 1; i > -1; i--)
			{
				if (this.States[i].Name == "Default")
				{
					vp_State = this.States[i];
					this.States.Remove(vp_State);
					this.States.Add(vp_State);
				}
			}
			if (vp_State == null)
			{
				vp_State = new vp_State(base.GetType().Name, "Default", null, null);
				this.States.Add(vp_State);
			}
		}
		if (vp_State.Preset == null || vp_State.Preset.ComponentType == null)
		{
			vp_State.Preset = new vp_ComponentPreset();
		}
		if (vp_State.TextAsset == null)
		{
			vp_State.Preset.InitFromComponent(this);
		}
		vp_State.Enabled = true;
		this.m_DefaultState = vp_State;
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0006B0E1 File Offset: 0x000692E1
	public void ApplyPreset(vp_ComponentPreset preset)
	{
		vp_ComponentPreset.Apply(this, preset);
		this.RefreshDefaultState();
		this.Refresh();
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0006B0F7 File Offset: 0x000692F7
	public vp_ComponentPreset Load(string path)
	{
		vp_ComponentPreset result = vp_ComponentPreset.LoadFromResources(this, path);
		this.RefreshDefaultState();
		this.Refresh();
		return result;
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x0006B10C File Offset: 0x0006930C
	public vp_ComponentPreset Load(TextAsset asset)
	{
		vp_ComponentPreset result = vp_ComponentPreset.LoadFromTextAsset(this, asset);
		this.RefreshDefaultState();
		this.Refresh();
		return result;
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x0006B124 File Offset: 0x00069324
	public void CacheChildren()
	{
		this.Children.Clear();
		foreach (vp_Component vp_Component in base.GetComponentsInChildren<vp_Component>(true))
		{
			if (vp_Component.transform.parent == base.transform)
			{
				this.Children.Add(vp_Component);
			}
		}
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x0006B17C File Offset: 0x0006937C
	public void CacheSiblings()
	{
		this.Siblings.Clear();
		foreach (vp_Component vp_Component in base.GetComponents<vp_Component>())
		{
			if (vp_Component != this)
			{
				this.Siblings.Add(vp_Component);
			}
		}
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x0006B1C4 File Offset: 0x000693C4
	public void CacheFamily()
	{
		this.Family.Clear();
		foreach (vp_Component vp_Component in base.transform.root.GetComponentsInChildren<vp_Component>(true))
		{
			if (vp_Component != this)
			{
				this.Family.Add(vp_Component);
			}
		}
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x0006B218 File Offset: 0x00069418
	public void CacheRenderers()
	{
		this.Renderers.Clear();
		foreach (Renderer item in base.GetComponentsInChildren<Renderer>(true))
		{
			this.Renderers.Add(item);
		}
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x0006B258 File Offset: 0x00069458
	public void CacheAudioSources()
	{
		this.AudioSources.Clear();
		foreach (AudioSource item in base.GetComponentsInChildren<AudioSource>(true))
		{
			this.AudioSources.Add(item);
		}
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x0006B296 File Offset: 0x00069496
	public virtual void Activate()
	{
		this.m_DeactivationTimer.Cancel();
		vp_Utility.Activate(base.gameObject, true);
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x0006B2AF File Offset: 0x000694AF
	public virtual void Deactivate()
	{
		vp_Utility.Activate(base.gameObject, false);
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x0006B2C0 File Offset: 0x000694C0
	public void DeactivateWhenSilent()
	{
		if (this == null)
		{
			return;
		}
		if (vp_Utility.IsActive(base.gameObject))
		{
			foreach (AudioSource audioSource in this.AudioSources)
			{
				if (audioSource.isPlaying && !audioSource.loop)
				{
					this.Rendering = false;
					vp_Timer.In(0.1f, delegate()
					{
						this.DeactivateWhenSilent();
					}, this.m_DeactivationTimer);
					return;
				}
			}
		}
		this.Deactivate();
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x00002B75 File Offset: 0x00000D75
	public virtual void Refresh()
	{
	}

	// Token: 0x04000B71 RID: 2929
	public bool Persist;

	// Token: 0x04000B72 RID: 2930
	protected vp_StateManager m_StateManager;

	// Token: 0x04000B73 RID: 2931
	public vp_EventHandler EventHandler;

	// Token: 0x04000B74 RID: 2932
	[NonSerialized]
	protected vp_State m_DefaultState;

	// Token: 0x04000B75 RID: 2933
	protected bool m_Initialized;

	// Token: 0x04000B76 RID: 2934
	protected Transform m_Transform;

	// Token: 0x04000B77 RID: 2935
	protected Transform m_Parent;

	// Token: 0x04000B78 RID: 2936
	protected Transform m_Root;

	// Token: 0x04000B79 RID: 2937
	public List<vp_State> States = new List<vp_State>();

	// Token: 0x04000B7A RID: 2938
	public List<vp_Component> Children = new List<vp_Component>();

	// Token: 0x04000B7B RID: 2939
	public List<vp_Component> Siblings = new List<vp_Component>();

	// Token: 0x04000B7C RID: 2940
	public List<vp_Component> Family = new List<vp_Component>();

	// Token: 0x04000B7D RID: 2941
	public List<Renderer> Renderers = new List<Renderer>();

	// Token: 0x04000B7E RID: 2942
	public List<AudioSource> AudioSources = new List<AudioSource>();

	// Token: 0x04000B7F RID: 2943
	protected AudioSource m_Audio;

	// Token: 0x04000B80 RID: 2944
	protected vp_Timer.Handle m_DeactivationTimer = new vp_Timer.Handle();
}
