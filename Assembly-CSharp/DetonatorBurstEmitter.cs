using System;
using UnityEngine;

// Token: 0x0200012C RID: 300
public class DetonatorBurstEmitter : DetonatorComponent
{
	// Token: 0x06000A7A RID: 2682 RVA: 0x0008891A File Offset: 0x00086B1A
	public override void Init()
	{
		MonoBehaviour.print("UNUSED");
	}

	// Token: 0x06000A7B RID: 2683 RVA: 0x00088926 File Offset: 0x00086B26
	public void Awake()
	{
		if (this.explodeOnAwake)
		{
			this.Explode();
		}
	}

	// Token: 0x06000A7C RID: 2684 RVA: 0x00088938 File Offset: 0x00086B38
	private void Update()
	{
		if (this.exponentialGrowth)
		{
			float num = Time.time - this._emitTime;
			float num2 = this.SizeFunction(num - DetonatorBurstEmitter.epsilon);
			float num3 = (this.SizeFunction(num) / num2 - 1f) / DetonatorBurstEmitter.epsilon;
		}
		if (this._delayedExplosionStarted)
		{
			this._explodeDelay -= Time.deltaTime;
			if (this._explodeDelay <= 0f)
			{
				this.Explode();
			}
		}
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x000889AC File Offset: 0x00086BAC
	private float SizeFunction(float elapsedTime)
	{
		float num = 1f - 1f / (1f + elapsedTime * this.speed);
		return this.initFraction + (1f - this.initFraction) * num;
	}

	// Token: 0x06000A7E RID: 2686 RVA: 0x000889E9 File Offset: 0x00086BE9
	public void Reset()
	{
		this.size = this._baseSize;
		this.color = this._baseColor;
		this.damping = this._baseDamping;
	}

	// Token: 0x06000A7F RID: 2687 RVA: 0x00088A10 File Offset: 0x00086C10
	public override void Explode()
	{
		if (this.on)
		{
			this._scaledDuration = this.timeScale * this.duration;
			this._scaledDurationVariation = this.timeScale * this.durationVariation;
			this._scaledStartRadius = this.size * this.startRadius;
			if (!this._delayedExplosionStarted)
			{
				this._explodeDelay = this.explodeDelayMin + Random.value * (this.explodeDelayMax - this.explodeDelayMin);
			}
			if (this._explodeDelay <= 0f)
			{
				Color[] array = new Color[5];
				if (this.useExplicitColorAnimation)
				{
					array[0] = this.colorAnimation[0];
					array[1] = this.colorAnimation[1];
					array[2] = this.colorAnimation[2];
					array[3] = this.colorAnimation[3];
					array[4] = this.colorAnimation[4];
				}
				else
				{
					array[0] = new Color(this.color.r, this.color.g, this.color.b, this.color.a * 0.7f);
					array[1] = new Color(this.color.r, this.color.g, this.color.b, this.color.a * 1f);
					array[2] = new Color(this.color.r, this.color.g, this.color.b, this.color.a * 0.5f);
					array[3] = new Color(this.color.r, this.color.g, this.color.b, this.color.a * 0.3f);
					array[4] = new Color(this.color.r, this.color.g, this.color.b, this.color.a * 0f);
				}
				this._tmpCount = this.count * this.detail;
				if (this._tmpCount < 1f)
				{
					this._tmpCount = 1f;
				}
				int num = 1;
				while ((float)num <= this._tmpCount)
				{
					this._tmpPos = Vector3.Scale(Random.insideUnitSphere, new Vector3(this._scaledStartRadius, this._scaledStartRadius, this._scaledStartRadius));
					this._tmpPos = this._thisPos + this._tmpPos;
					this._tmpDir = Vector3.Scale(Random.insideUnitSphere, new Vector3(this.velocity.x, this.velocity.y, this.velocity.z));
					this._tmpDir.y = this._tmpDir.y + 2f * (Mathf.Abs(this._tmpDir.y) * this.upwardsBias);
					if (this.randomRotation)
					{
						this._randomizedRotation = Random.Range(-1f, 1f);
						this._tmpAngularVelocity = Random.Range(-1f, 1f) * this.angularVelocity;
					}
					else
					{
						this._randomizedRotation = 0f;
						this._tmpAngularVelocity = this.angularVelocity;
					}
					this._tmpDir = Vector3.Scale(this._tmpDir, new Vector3(this.size, this.size, this.size));
					this._tmpParticleSize = this.size * (this.particleSize + Random.value * this.sizeVariation);
					this._tmpDuration = this._scaledDuration + Random.value * this._scaledDurationVariation;
					num++;
				}
				this._emitTime = Time.time;
				this._delayedExplosionStarted = false;
				this._explodeDelay = 0f;
				return;
			}
			this._delayedExplosionStarted = true;
		}
	}

	// Token: 0x04000FED RID: 4077
	private float _baseDamping = 0.1300004f;

	// Token: 0x04000FEE RID: 4078
	private float _baseSize = 1f;

	// Token: 0x04000FEF RID: 4079
	private Color _baseColor = Color.white;

	// Token: 0x04000FF0 RID: 4080
	public float damping = 1f;

	// Token: 0x04000FF1 RID: 4081
	public float startRadius = 1f;

	// Token: 0x04000FF2 RID: 4082
	public float maxScreenSize = 2f;

	// Token: 0x04000FF3 RID: 4083
	public bool explodeOnAwake;

	// Token: 0x04000FF4 RID: 4084
	public bool oneShot = true;

	// Token: 0x04000FF5 RID: 4085
	public float sizeVariation;

	// Token: 0x04000FF6 RID: 4086
	public float particleSize = 1f;

	// Token: 0x04000FF7 RID: 4087
	public float count = 1f;

	// Token: 0x04000FF8 RID: 4088
	public float sizeGrow = 20f;

	// Token: 0x04000FF9 RID: 4089
	public bool exponentialGrowth = true;

	// Token: 0x04000FFA RID: 4090
	public float durationVariation;

	// Token: 0x04000FFB RID: 4091
	public bool useWorldSpace = true;

	// Token: 0x04000FFC RID: 4092
	public float upwardsBias;

	// Token: 0x04000FFD RID: 4093
	public float angularVelocity = 20f;

	// Token: 0x04000FFE RID: 4094
	public bool randomRotation = true;

	// Token: 0x04000FFF RID: 4095
	public bool useExplicitColorAnimation;

	// Token: 0x04001000 RID: 4096
	public Color[] colorAnimation = new Color[5];

	// Token: 0x04001001 RID: 4097
	private bool _delayedExplosionStarted;

	// Token: 0x04001002 RID: 4098
	private float _explodeDelay;

	// Token: 0x04001003 RID: 4099
	public Material material;

	// Token: 0x04001004 RID: 4100
	private float _emitTime;

	// Token: 0x04001005 RID: 4101
	private float speed = 3f;

	// Token: 0x04001006 RID: 4102
	private float initFraction = 0.1f;

	// Token: 0x04001007 RID: 4103
	private static float epsilon = 0.01f;

	// Token: 0x04001008 RID: 4104
	private float _tmpParticleSize;

	// Token: 0x04001009 RID: 4105
	private Vector3 _tmpPos;

	// Token: 0x0400100A RID: 4106
	private Vector3 _tmpDir;

	// Token: 0x0400100B RID: 4107
	private Vector3 _thisPos;

	// Token: 0x0400100C RID: 4108
	private float _tmpDuration;

	// Token: 0x0400100D RID: 4109
	private float _tmpCount;

	// Token: 0x0400100E RID: 4110
	private float _scaledDuration;

	// Token: 0x0400100F RID: 4111
	private float _scaledDurationVariation;

	// Token: 0x04001010 RID: 4112
	private float _scaledStartRadius;

	// Token: 0x04001011 RID: 4113
	private float _scaledColor;

	// Token: 0x04001012 RID: 4114
	private float _randomizedRotation;

	// Token: 0x04001013 RID: 4115
	private float _tmpAngularVelocity;
}
