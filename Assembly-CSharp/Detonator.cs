using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
[AddComponentMenu("Detonator/Detonator")]
public class Detonator : MonoBehaviour
{
	// Token: 0x06000A69 RID: 2665 RVA: 0x00087E10 File Offset: 0x00086010
	private void Awake()
	{
		this.FillDefaultMaterials();
		this.components = base.GetComponents(typeof(DetonatorComponent));
		foreach (DetonatorComponent detonatorComponent in this.components)
		{
			if (detonatorComponent is DetonatorFireball)
			{
				this._fireball = (detonatorComponent as DetonatorFireball);
			}
			if (detonatorComponent is DetonatorSparks)
			{
				this._sparks = (detonatorComponent as DetonatorSparks);
			}
			if (detonatorComponent is DetonatorShockwave)
			{
				this._shockwave = (detonatorComponent as DetonatorShockwave);
			}
			if (detonatorComponent is DetonatorSmoke)
			{
				this._smoke = (detonatorComponent as DetonatorSmoke);
			}
			if (detonatorComponent is DetonatorGlow)
			{
				this._glow = (detonatorComponent as DetonatorGlow);
			}
			if (detonatorComponent is DetonatorLight)
			{
				this._light = (detonatorComponent as DetonatorLight);
			}
			if (detonatorComponent is DetonatorForce)
			{
				this._force = (detonatorComponent as DetonatorForce);
			}
			if (detonatorComponent is DetonatorHeatwave)
			{
				this._heatwave = (detonatorComponent as DetonatorHeatwave);
			}
		}
		if (!this._fireball && this.autoCreateFireball)
		{
			this._fireball = base.gameObject.AddComponent<DetonatorFireball>();
			this._fireball.Reset();
		}
		if (!this._smoke && this.autoCreateSmoke)
		{
			this._smoke = base.gameObject.AddComponent<DetonatorSmoke>();
			this._smoke.Reset();
		}
		if (!this._sparks && this.autoCreateSparks)
		{
			this._sparks = base.gameObject.AddComponent<DetonatorSparks>();
			this._sparks.Reset();
		}
		if (!this._shockwave && this.autoCreateShockwave)
		{
			this._shockwave = base.gameObject.AddComponent<DetonatorShockwave>();
			this._shockwave.Reset();
		}
		if (!this._glow && this.autoCreateGlow)
		{
			this._glow = base.gameObject.AddComponent<DetonatorGlow>();
			this._glow.Reset();
		}
		if (!this._light && this.autoCreateLight)
		{
			this._light = base.gameObject.AddComponent<DetonatorLight>();
			this._light.Reset();
		}
		if (!this._force && this.autoCreateForce)
		{
			this._force = base.gameObject.AddComponent<DetonatorForce>();
			this._force.Reset();
		}
		if (!this._heatwave && this.autoCreateHeatwave && SystemInfo.supportsImageEffects)
		{
			this._heatwave = base.gameObject.AddComponent<DetonatorHeatwave>();
			this._heatwave.Reset();
		}
		this.components = base.GetComponents(typeof(DetonatorComponent));
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x000880A4 File Offset: 0x000862A4
	private void FillDefaultMaterials()
	{
		if (!this.fireballAMaterial)
		{
			this.fireballAMaterial = Detonator.DefaultFireballAMaterial();
		}
		if (!this.fireballBMaterial)
		{
			this.fireballBMaterial = Detonator.DefaultFireballBMaterial();
		}
		if (!this.smokeAMaterial)
		{
			this.smokeAMaterial = Detonator.DefaultSmokeAMaterial();
		}
		if (!this.smokeBMaterial)
		{
			this.smokeBMaterial = Detonator.DefaultSmokeBMaterial();
		}
		if (!this.shockwaveMaterial)
		{
			this.shockwaveMaterial = Detonator.DefaultShockwaveMaterial();
		}
		if (!this.sparksMaterial)
		{
			this.sparksMaterial = Detonator.DefaultSparksMaterial();
		}
		if (!this.glowMaterial)
		{
			this.glowMaterial = Detonator.DefaultGlowMaterial();
		}
		if (!this.heatwaveMaterial)
		{
			this.heatwaveMaterial = Detonator.DefaultHeatwaveMaterial();
		}
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x00088171 File Offset: 0x00086371
	private void Start()
	{
		if (this.explodeOnStart)
		{
			this.UpdateComponents();
			this.Explode();
		}
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00088187 File Offset: 0x00086387
	private void Update()
	{
		if (this.destroyTime > 0f && this._lastExplosionTime + this.destroyTime <= Time.time)
		{
			Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000A6D RID: 2669 RVA: 0x000881B8 File Offset: 0x000863B8
	private void UpdateComponents()
	{
		if (this._firstComponentUpdate)
		{
			foreach (DetonatorComponent detonatorComponent in this.components)
			{
				detonatorComponent.Init();
				detonatorComponent.SetStartValues();
			}
			this._firstComponentUpdate = false;
		}
		if (!this._firstComponentUpdate)
		{
			float num = this.size / Detonator._baseSize;
			Vector3 vector;
			vector..ctor(this.direction.x * num, this.direction.y * num, this.direction.z * num);
			float timeScale = this.duration / Detonator._baseDuration;
			foreach (DetonatorComponent detonatorComponent2 in this.components)
			{
				if (detonatorComponent2.detonatorControlled)
				{
					detonatorComponent2.size = detonatorComponent2.startSize * num;
					detonatorComponent2.timeScale = timeScale;
					detonatorComponent2.detail = detonatorComponent2.startDetail * this.detail;
					detonatorComponent2.force = new Vector3(detonatorComponent2.startForce.x * num + vector.x, detonatorComponent2.startForce.y * num + vector.y, detonatorComponent2.startForce.z * num + vector.z);
					detonatorComponent2.velocity = new Vector3(detonatorComponent2.startVelocity.x * num + vector.x, detonatorComponent2.startVelocity.y * num + vector.y, detonatorComponent2.startVelocity.z * num + vector.z);
					detonatorComponent2.color = Color.Lerp(detonatorComponent2.startColor, this.color, this.color.a);
				}
			}
		}
	}

	// Token: 0x06000A6E RID: 2670 RVA: 0x0008836C File Offset: 0x0008656C
	public void Explode()
	{
		this._lastExplosionTime = Time.time;
		foreach (DetonatorComponent detonatorComponent in this.components)
		{
			this.UpdateComponents();
			detonatorComponent.Explode();
		}
	}

	// Token: 0x06000A6F RID: 2671 RVA: 0x000883AC File Offset: 0x000865AC
	public void Reset()
	{
		this.size = 10f;
		this.color = Detonator._baseColor;
		this.duration = Detonator._baseDuration;
		this.FillDefaultMaterials();
	}

	// Token: 0x06000A70 RID: 2672 RVA: 0x000883D8 File Offset: 0x000865D8
	public static Material DefaultFireballAMaterial()
	{
		if (Detonator.defaultFireballAMaterial != null)
		{
			return Detonator.defaultFireballAMaterial;
		}
		Detonator.defaultFireballAMaterial = new Material(Shader.Find("Particles/Additive"));
		Detonator.defaultFireballAMaterial.name = "FireballA-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Fireball") as Texture2D;
		Detonator.defaultFireballAMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultFireballAMaterial.mainTexture = mainTexture;
		Detonator.defaultFireballAMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		return Detonator.defaultFireballAMaterial;
	}

	// Token: 0x06000A71 RID: 2673 RVA: 0x00088468 File Offset: 0x00086668
	public static Material DefaultFireballBMaterial()
	{
		if (Detonator.defaultFireballBMaterial != null)
		{
			return Detonator.defaultFireballBMaterial;
		}
		Detonator.defaultFireballBMaterial = new Material(Shader.Find("Particles/Additive"));
		Detonator.defaultFireballBMaterial.name = "FireballB-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Fireball") as Texture2D;
		Detonator.defaultFireballBMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultFireballBMaterial.mainTexture = mainTexture;
		Detonator.defaultFireballBMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		Detonator.defaultFireballBMaterial.mainTextureOffset = new Vector2(0.5f, 0f);
		return Detonator.defaultFireballBMaterial;
	}

	// Token: 0x06000A72 RID: 2674 RVA: 0x00088514 File Offset: 0x00086714
	public static Material DefaultSmokeAMaterial()
	{
		if (Detonator.defaultSmokeAMaterial != null)
		{
			return Detonator.defaultSmokeAMaterial;
		}
		Detonator.defaultSmokeAMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		Detonator.defaultSmokeAMaterial.name = "SmokeA-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Smoke") as Texture2D;
		Detonator.defaultSmokeAMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultSmokeAMaterial.mainTexture = mainTexture;
		Detonator.defaultSmokeAMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		return Detonator.defaultSmokeAMaterial;
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x000885A4 File Offset: 0x000867A4
	public static Material DefaultSmokeBMaterial()
	{
		if (Detonator.defaultSmokeBMaterial != null)
		{
			return Detonator.defaultSmokeBMaterial;
		}
		Detonator.defaultSmokeBMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
		Detonator.defaultSmokeBMaterial.name = "SmokeB-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Smoke") as Texture2D;
		Detonator.defaultSmokeBMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultSmokeBMaterial.mainTexture = mainTexture;
		Detonator.defaultSmokeBMaterial.mainTextureScale = new Vector2(0.5f, 1f);
		Detonator.defaultSmokeBMaterial.mainTextureOffset = new Vector2(0.5f, 0f);
		return Detonator.defaultSmokeBMaterial;
	}

	// Token: 0x06000A74 RID: 2676 RVA: 0x00088650 File Offset: 0x00086850
	public static Material DefaultSparksMaterial()
	{
		if (Detonator.defaultSparksMaterial != null)
		{
			return Detonator.defaultSparksMaterial;
		}
		Detonator.defaultSparksMaterial = new Material(Shader.Find("Particles/Additive"));
		Detonator.defaultSparksMaterial.name = "Sparks-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/GlowDot") as Texture2D;
		Detonator.defaultSparksMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultSparksMaterial.mainTexture = mainTexture;
		return Detonator.defaultSparksMaterial;
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x000886C8 File Offset: 0x000868C8
	public static Material DefaultShockwaveMaterial()
	{
		if (Detonator.defaultShockwaveMaterial != null)
		{
			return Detonator.defaultShockwaveMaterial;
		}
		Detonator.defaultShockwaveMaterial = new Material(Shader.Find("Particles/Additive"));
		Detonator.defaultShockwaveMaterial.name = "Shockwave-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Shockwave") as Texture2D;
		Detonator.defaultShockwaveMaterial.SetColor("_TintColor", new Color(0.1f, 0.1f, 0.1f, 1f));
		Detonator.defaultShockwaveMaterial.mainTexture = mainTexture;
		return Detonator.defaultShockwaveMaterial;
	}

	// Token: 0x06000A76 RID: 2678 RVA: 0x00088754 File Offset: 0x00086954
	public static Material DefaultGlowMaterial()
	{
		if (Detonator.defaultGlowMaterial != null)
		{
			return Detonator.defaultGlowMaterial;
		}
		Detonator.defaultGlowMaterial = new Material(Shader.Find("Particles/Additive"));
		Detonator.defaultGlowMaterial.name = "Glow-Default";
		Texture2D mainTexture = Resources.Load("Detonator/Textures/Glow") as Texture2D;
		Detonator.defaultGlowMaterial.SetColor("_TintColor", Color.white);
		Detonator.defaultGlowMaterial.mainTexture = mainTexture;
		return Detonator.defaultGlowMaterial;
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x000887CC File Offset: 0x000869CC
	public static Material DefaultHeatwaveMaterial()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			return null;
		}
		if (Detonator.defaultHeatwaveMaterial != null)
		{
			return Detonator.defaultHeatwaveMaterial;
		}
		Detonator.defaultHeatwaveMaterial = new Material(Shader.Find("HeatDistort"));
		Detonator.defaultHeatwaveMaterial.name = "Heatwave-Default";
		Texture2D texture2D = Resources.Load("Detonator/Textures/Heatwave") as Texture2D;
		Detonator.defaultHeatwaveMaterial.SetTexture("_BumpMap", texture2D);
		return Detonator.defaultHeatwaveMaterial;
	}

	// Token: 0x04000FBD RID: 4029
	private static float _baseSize = 30f;

	// Token: 0x04000FBE RID: 4030
	private static Color _baseColor = new Color(1f, 0.423f, 0f, 0.5f);

	// Token: 0x04000FBF RID: 4031
	private static float _baseDuration = 3f;

	// Token: 0x04000FC0 RID: 4032
	public float size = 10f;

	// Token: 0x04000FC1 RID: 4033
	public Color color = Detonator._baseColor;

	// Token: 0x04000FC2 RID: 4034
	public bool explodeOnStart = true;

	// Token: 0x04000FC3 RID: 4035
	public float duration = Detonator._baseDuration;

	// Token: 0x04000FC4 RID: 4036
	public float detail = 1f;

	// Token: 0x04000FC5 RID: 4037
	public float upwardsBias;

	// Token: 0x04000FC6 RID: 4038
	public float destroyTime = 7f;

	// Token: 0x04000FC7 RID: 4039
	public bool useWorldSpace = true;

	// Token: 0x04000FC8 RID: 4040
	public Vector3 direction = Vector3.zero;

	// Token: 0x04000FC9 RID: 4041
	public Material fireballAMaterial;

	// Token: 0x04000FCA RID: 4042
	public Material fireballBMaterial;

	// Token: 0x04000FCB RID: 4043
	public Material smokeAMaterial;

	// Token: 0x04000FCC RID: 4044
	public Material smokeBMaterial;

	// Token: 0x04000FCD RID: 4045
	public Material shockwaveMaterial;

	// Token: 0x04000FCE RID: 4046
	public Material sparksMaterial;

	// Token: 0x04000FCF RID: 4047
	public Material glowMaterial;

	// Token: 0x04000FD0 RID: 4048
	public Material heatwaveMaterial;

	// Token: 0x04000FD1 RID: 4049
	private Component[] components;

	// Token: 0x04000FD2 RID: 4050
	private DetonatorFireball _fireball;

	// Token: 0x04000FD3 RID: 4051
	private DetonatorSparks _sparks;

	// Token: 0x04000FD4 RID: 4052
	private DetonatorShockwave _shockwave;

	// Token: 0x04000FD5 RID: 4053
	private DetonatorSmoke _smoke;

	// Token: 0x04000FD6 RID: 4054
	private DetonatorGlow _glow;

	// Token: 0x04000FD7 RID: 4055
	private DetonatorLight _light;

	// Token: 0x04000FD8 RID: 4056
	private DetonatorForce _force;

	// Token: 0x04000FD9 RID: 4057
	private DetonatorHeatwave _heatwave;

	// Token: 0x04000FDA RID: 4058
	public bool autoCreateFireball = true;

	// Token: 0x04000FDB RID: 4059
	public bool autoCreateSparks = true;

	// Token: 0x04000FDC RID: 4060
	public bool autoCreateShockwave = true;

	// Token: 0x04000FDD RID: 4061
	public bool autoCreateSmoke = true;

	// Token: 0x04000FDE RID: 4062
	public bool autoCreateGlow = true;

	// Token: 0x04000FDF RID: 4063
	public bool autoCreateLight = true;

	// Token: 0x04000FE0 RID: 4064
	public bool autoCreateForce = true;

	// Token: 0x04000FE1 RID: 4065
	public bool autoCreateHeatwave;

	// Token: 0x04000FE2 RID: 4066
	private float _lastExplosionTime = 1000f;

	// Token: 0x04000FE3 RID: 4067
	private bool _firstComponentUpdate = true;

	// Token: 0x04000FE4 RID: 4068
	private Component[] _subDetonators;

	// Token: 0x04000FE5 RID: 4069
	public static Material defaultFireballAMaterial;

	// Token: 0x04000FE6 RID: 4070
	public static Material defaultFireballBMaterial;

	// Token: 0x04000FE7 RID: 4071
	public static Material defaultSmokeAMaterial;

	// Token: 0x04000FE8 RID: 4072
	public static Material defaultSmokeBMaterial;

	// Token: 0x04000FE9 RID: 4073
	public static Material defaultShockwaveMaterial;

	// Token: 0x04000FEA RID: 4074
	public static Material defaultSparksMaterial;

	// Token: 0x04000FEB RID: 4075
	public static Material defaultGlowMaterial;

	// Token: 0x04000FEC RID: 4076
	public static Material defaultHeatwaveMaterial;
}
