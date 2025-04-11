using System;
using UnityEngine;

// Token: 0x020000D0 RID: 208
public class vp_FPPlayerEventHandler : vp_StateEventHandler
{
	// Token: 0x06000773 RID: 1907 RVA: 0x000757F8 File Offset: 0x000739F8
	protected override void Awake()
	{
		base.Awake();
		base.BindStateToActivity(this.Walk);
		base.BindStateToActivity(this.Jump);
		base.BindStateToActivity(this.Crouch);
		base.BindStateToActivity(this.Zoom);
		base.BindStateToActivity(this.Reload);
		base.BindStateToActivity(this.Dead);
		base.BindStateToActivityOnStart(this.Attack);
		this.SetWeapon.AutoDuration = 1f;
		this.Reload.AutoDuration = 1f;
		this.Zoom.MinDuration = 0.2f;
		this.Crouch.MinDuration = 0.5f;
		this.Jump.MinPause = 0f;
		this.SetWeapon.MinPause = 0.2f;
	}

	// Token: 0x04000C94 RID: 3220
	public vp_Message<float> HUDDamageFlash;

	// Token: 0x04000C95 RID: 3221
	public vp_Message<string> HUDText;

	// Token: 0x04000C96 RID: 3222
	public vp_Value<Vector2> InputMoveVector;

	// Token: 0x04000C97 RID: 3223
	public vp_Value<bool> AllowGameplayInput;

	// Token: 0x04000C98 RID: 3224
	public vp_Value<float> Health;

	// Token: 0x04000C99 RID: 3225
	public vp_Value<Vector3> Position;

	// Token: 0x04000C9A RID: 3226
	public vp_Value<Vector2> Rotation;

	// Token: 0x04000C9B RID: 3227
	public vp_Value<Vector3> Forward;

	// Token: 0x04000C9C RID: 3228
	public vp_Activity Dead;

	// Token: 0x04000C9D RID: 3229
	public vp_Activity Walk;

	// Token: 0x04000C9E RID: 3230
	public vp_Activity Jump;

	// Token: 0x04000C9F RID: 3231
	public vp_Activity Crouch;

	// Token: 0x04000CA0 RID: 3232
	public vp_Activity Zoom;

	// Token: 0x04000CA1 RID: 3233
	public vp_Activity Attack;

	// Token: 0x04000CA2 RID: 3234
	public vp_Activity Reload;

	// Token: 0x04000CA3 RID: 3235
	public vp_Activity<int> SetWeapon;

	// Token: 0x04000CA4 RID: 3236
	public vp_Activity<Vector3> Earthquake;

	// Token: 0x04000CA5 RID: 3237
	public vp_Attempt SetPrevWeapon;

	// Token: 0x04000CA6 RID: 3238
	public vp_Attempt SetNextWeapon;

	// Token: 0x04000CA7 RID: 3239
	public vp_Attempt<string> SetWeaponByName;

	// Token: 0x04000CA8 RID: 3240
	public vp_Value<int> CurrentWeaponID;

	// Token: 0x04000CA9 RID: 3241
	public vp_Value<string> CurrentWeaponName;

	// Token: 0x04000CAA RID: 3242
	public vp_Value<bool> CurrentWeaponWielded;

	// Token: 0x04000CAB RID: 3243
	public vp_Value<float> CurrentWeaponReloadDuration;

	// Token: 0x04000CAC RID: 3244
	public vp_Value<string> CurrentWeaponClipType;

	// Token: 0x04000CAD RID: 3245
	public vp_Message<string, int> GetItemCount;

	// Token: 0x04000CAE RID: 3246
	public vp_Attempt<object> AddItem;

	// Token: 0x04000CAF RID: 3247
	public vp_Attempt<object> RemoveItem;

	// Token: 0x04000CB0 RID: 3248
	public vp_Attempt<object> AddAmmo;

	// Token: 0x04000CB1 RID: 3249
	public vp_Attempt DepleteAmmo;

	// Token: 0x04000CB2 RID: 3250
	public vp_Attempt RemoveClip;

	// Token: 0x04000CB3 RID: 3251
	public vp_Value<int> CurrentWeaponAmmoCount;

	// Token: 0x04000CB4 RID: 3252
	public vp_Value<int> CurrentWeaponClipCount;

	// Token: 0x04000CB5 RID: 3253
	public vp_Message<float> FallImpact;

	// Token: 0x04000CB6 RID: 3254
	public vp_Message<float> HeadImpact;

	// Token: 0x04000CB7 RID: 3255
	public vp_Message<Vector3> ForceImpact;

	// Token: 0x04000CB8 RID: 3256
	public vp_Message<float> GroundStomp;

	// Token: 0x04000CB9 RID: 3257
	public vp_Message<float> BombShake;

	// Token: 0x04000CBA RID: 3258
	public vp_Value<Vector3> EarthQuakeForce;

	// Token: 0x04000CBB RID: 3259
	public vp_Message Stop;

	// Token: 0x04000CBC RID: 3260
	public vp_Value<Transform> Platform;

	// Token: 0x04000CBD RID: 3261
	public vp_Value<bool> Pause;
}
