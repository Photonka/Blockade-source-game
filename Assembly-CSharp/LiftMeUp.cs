using System;
using UnityEngine;

// Token: 0x02000091 RID: 145
public class LiftMeUp : MonoBehaviour
{
	// Token: 0x06000444 RID: 1092 RVA: 0x00056C6F File Offset: 0x00054E6F
	private void Awake()
	{
		this.lift = false;
		this.lift_sound = Resources.Load<AudioClip>("Sound/elevator_sound_full");
		this.lift_wait = Resources.Load<AudioClip>("Sound/wait_music");
		this.audio_lift = base.gameObject.AddComponent<AudioSource>();
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x00056CAC File Offset: 0x00054EAC
	public void PlaySound()
	{
		if (this.audio_lift.isPlaying)
		{
			this.BreakSound();
		}
		this.timer = Time.time + 10f;
		this.audio_lift.clip = this.lift_sound;
		this.audio_lift.Play();
		this.lift = true;
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x00056D00 File Offset: 0x00054F00
	public void PlayWait()
	{
		if (this.audio_lift.isPlaying)
		{
			return;
		}
		this.timer = Time.time + 90f;
		this.audio_lift.clip = this.lift_wait;
		this.audio_lift.Play();
		this.lift = true;
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x00056D4F File Offset: 0x00054F4F
	private void Update()
	{
		if (this.lift)
		{
			if (this.timer < Time.time)
			{
				this.BreakSound();
				return;
			}
		}
		else if (this.audio_lift.isPlaying)
		{
			this.BreakSound();
		}
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00056D80 File Offset: 0x00054F80
	public void BreakSound()
	{
		this.audio_lift.Stop();
		this.lift = false;
	}

	// Token: 0x040008CC RID: 2252
	private float timer;

	// Token: 0x040008CD RID: 2253
	private bool lift;

	// Token: 0x040008CE RID: 2254
	private AudioSource audio_lift;

	// Token: 0x040008CF RID: 2255
	private AudioClip lift_sound;

	// Token: 0x040008D0 RID: 2256
	private AudioClip lift_wait;
}
