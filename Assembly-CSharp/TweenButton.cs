using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AF RID: 175
public class TweenButton
{
	// Token: 0x06000584 RID: 1412 RVA: 0x00069694 File Offset: 0x00067894
	public TweenButton(GUIGS _guigs, Vector2 _myOffset, OFFSET_TYPE _offset_type, Texture2D _primary, Texture2D _background, Texture2D _secondary, Texture2D _selected, AudioClip _hover_melody, AudioClip _press_melody, AudioSource _AS, float _hover_melody_prepause, float _tween_scale = 10f, int _rotate_speed = 10, float _org_width = 0f, float _org_height = 0f, float _force_angle = 0f)
	{
		this.myOffset = _myOffset;
		this.offset_type = _offset_type;
		this.primary = _primary;
		this.background = _background;
		this.secondary = _secondary;
		this.selected = _selected;
		this.tween_scale = _tween_scale;
		this.rotateSpeed = _rotate_speed;
		if (_org_width > 0f)
		{
			this.orgWidth = _org_width;
			this.myWidth = _org_width;
		}
		else
		{
			this.orgWidth = (float)this.primary.width;
			this.myWidth = (float)this.primary.width;
		}
		if (_org_height > 0f)
		{
			this.orgHeight = _org_height;
			this.myHeight = _org_height;
		}
		else
		{
			this.orgHeight = (float)this.primary.height;
			this.myHeight = (float)this.primary.height;
		}
		this.myGUIGS = _guigs;
		this.myAngle = _force_angle;
		this.hover_melody = _hover_melody;
		this.press_melody = _press_melody;
		this.hover_melody_prepause = _hover_melody_prepause;
		this.AS = _AS;
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x000697B8 File Offset: 0x000679B8
	public bool DrawButton(Vector2 mpos)
	{
		if (this.myGUIGS != GM.currGUIState && this.myGUIGS != GUIGS.NULL)
		{
			return false;
		}
		if (!this.draw)
		{
			return false;
		}
		this.Tween(this.buttonRect.Contains(mpos));
		Color color = GUI.color;
		Matrix4x4 matrix = GUI.matrix;
		GUIUtility.RotateAroundPivot(this.myAngle, this.buttonRect.center);
		GUI.color = new Color(color.r, color.g, color.b, this.a);
		if (this.background != null)
		{
			GUI.DrawTexture(this.buttonRect, this.background);
		}
		GUI.color = new Color(color.r, color.g, color.b, 1f);
		Color color2 = GUI.color;
		if (this.primary != null)
		{
			GUI.DrawTexture(this.buttonRect, this.primary);
		}
		GUI.matrix = matrix;
		GUI.color = Color.white;
		if (this.secondary != null)
		{
			GUI.DrawTexture(this.buttonRect, this.secondary);
		}
		if (this.isSelected && this.selected != null)
		{
			GUI.DrawTexture(this.buttonRect, this.selected);
		}
		GUI.color = color2;
		GUI.color = color;
		if (GUI.Button(this.buttonRect, "", GUIManager.gs_style1))
		{
			if (this.press_melody != null)
			{
				if (this.AS == null)
				{
					this.AS = Object.FindObjectOfType<MainCamera>().GetComponent<AudioSource>();
				}
				if (this.AS != null)
				{
					this.AS.PlayOneShot(this.press_melody);
				}
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00069968 File Offset: 0x00067B68
	private void Tween(bool hover)
	{
		if (this.tt.Count != 0)
		{
			foreach (TWEEN_TYPE tween_TYPE in this.tt)
			{
				if (tween_TYPE == TWEEN_TYPE.BIGGER)
				{
					this.BiggerTween(hover);
				}
				if (this.offset_type == OFFSET_TYPE.IS_CENTER)
				{
					this.buttonRect = new Rect(this.myOffset.x - this.myWidth / 2f, this.myOffset.y - this.myHeight / 2f, this.myWidth, this.myHeight);
				}
				else if (this.offset_type == OFFSET_TYPE.FROM_CENTER)
				{
					Vector2 vector = GUIUtility.ScreenToGUIPoint(new Vector2((float)(Screen.width / 2) + this.myOffset.x - this.myWidth / 2f, (float)(Screen.height / 2) + this.myOffset.y - this.myHeight / 2f));
					this.buttonRect = new Rect(vector.x, vector.y, this.myWidth, this.myHeight);
				}
				else if (this.offset_type == OFFSET_TYPE.SCREEN_OFFSET)
				{
					this.buttonRect = new Rect((float)Screen.width / 2f + this.myOffset.x - this.myWidth / 2f, this.myOffset.y - this.myHeight / 2f, this.myWidth, this.myHeight);
				}
				if (tween_TYPE == TWEEN_TYPE.ROTATE)
				{
					this.RotateTween(hover);
				}
				if (tween_TYPE == TWEEN_TYPE.GLOW)
				{
					this.GlowTween(hover);
				}
			}
			if (hover)
			{
				if (!this.played)
				{
					if (this.hover_melody == null)
					{
						return;
					}
					if (this.AS == null)
					{
						this.AS = Object.FindObjectOfType<MainCamera>().GetComponent<AudioSource>();
					}
					if (this.AS != null)
					{
						this.AS.PlayOneShot(this.hover_melody);
					}
					this.played = true;
					return;
				}
			}
			else
			{
				this.played = false;
			}
			return;
		}
		if (this.offset_type == OFFSET_TYPE.IS_CENTER)
		{
			this.buttonRect = new Rect(this.myOffset.x - this.myWidth / 2f, this.myOffset.y - this.myHeight / 2f, this.myWidth, this.myHeight);
			return;
		}
		if (this.offset_type == OFFSET_TYPE.FROM_CENTER)
		{
			Vector2 vector2 = GUIUtility.ScreenToGUIPoint(new Vector2((float)(Screen.width / 2) + this.myOffset.x - this.myWidth / 2f, (float)(Screen.height / 2) + this.myOffset.y - this.myHeight / 2f));
			this.buttonRect = new Rect(vector2.x, vector2.y, this.myWidth, this.myHeight);
			return;
		}
		if (this.offset_type == OFFSET_TYPE.SCREEN_OFFSET)
		{
			this.buttonRect = new Rect((float)Screen.width / 2f + this.myOffset.x - this.myWidth / 2f, this.myOffset.y - this.myHeight / 2f, this.myWidth, this.myHeight);
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00069CB0 File Offset: 0x00067EB0
	private void BiggerTween(bool hover)
	{
		if (hover || this.forceHover)
		{
			this.myWidth = Mathf.Lerp(this.myWidth, this.orgWidth * (1f + this.tween_scale / 100f), Time.deltaTime * 10f);
			this.myHeight = Mathf.Lerp(this.myHeight, this.orgHeight * (1f + this.tween_scale / 100f), Time.deltaTime * 10f);
			return;
		}
		this.myWidth = Mathf.Lerp(this.myWidth, this.orgWidth, Time.deltaTime * 10f);
		this.myHeight = Mathf.Lerp(this.myHeight, this.orgHeight, Time.deltaTime * 10f);
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00069D77 File Offset: 0x00067F77
	private void RotateTween(bool hover)
	{
		this.myAngle += Time.deltaTime * (float)this.rotateSpeed;
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00069D94 File Offset: 0x00067F94
	private void GlowTween(bool hover)
	{
		this.a += Time.deltaTime * (float)this.coeff;
		if (this.a >= 1f || this.a <= 0f)
		{
			this.coeff *= -1;
		}
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00069DE4 File Offset: 0x00067FE4
	public void DropSize()
	{
		this.myWidth = 0f;
		this.myHeight = 0f;
		if (this.hover_melody_prepause > 0f)
		{
			this.mute_time = Time.time + this.hover_melody_prepause;
			this.draw = false;
			return;
		}
		this.draw = true;
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00069E38 File Offset: 0x00068038
	public void GO()
	{
		if (this.mute_time > Time.time)
		{
			return;
		}
		if (this.AS == null)
		{
			this.AS = Object.FindObjectOfType<MainCamera>().GetComponent<AudioSource>();
		}
		if (this.AS != null)
		{
			this.AS.PlayOneShot(this.hover_melody);
		}
		this.draw = true;
	}

	// Token: 0x04000B4E RID: 2894
	private GUIGS myGUIGS;

	// Token: 0x04000B4F RID: 2895
	private Vector2 myOffset;

	// Token: 0x04000B50 RID: 2896
	public List<TWEEN_TYPE> tt = new List<TWEEN_TYPE>();

	// Token: 0x04000B51 RID: 2897
	private float tween_scale;

	// Token: 0x04000B52 RID: 2898
	private Texture2D background;

	// Token: 0x04000B53 RID: 2899
	private Texture2D primary;

	// Token: 0x04000B54 RID: 2900
	private Texture2D secondary;

	// Token: 0x04000B55 RID: 2901
	private Texture2D selected;

	// Token: 0x04000B56 RID: 2902
	public bool isSelected;

	// Token: 0x04000B57 RID: 2903
	private AudioClip hover_melody;

	// Token: 0x04000B58 RID: 2904
	private AudioClip press_melody;

	// Token: 0x04000B59 RID: 2905
	private AudioSource AS;

	// Token: 0x04000B5A RID: 2906
	private float hover_melody_prepause;

	// Token: 0x04000B5B RID: 2907
	private float myWidth;

	// Token: 0x04000B5C RID: 2908
	private float myHeight;

	// Token: 0x04000B5D RID: 2909
	private float orgWidth;

	// Token: 0x04000B5E RID: 2910
	private float orgHeight;

	// Token: 0x04000B5F RID: 2911
	private float myAngle;

	// Token: 0x04000B60 RID: 2912
	private int rotateSpeed;

	// Token: 0x04000B61 RID: 2913
	private float a = 1f;

	// Token: 0x04000B62 RID: 2914
	public bool draw = true;

	// Token: 0x04000B63 RID: 2915
	private Rect buttonRect;

	// Token: 0x04000B64 RID: 2916
	private OFFSET_TYPE offset_type;

	// Token: 0x04000B65 RID: 2917
	private float mute_time;

	// Token: 0x04000B66 RID: 2918
	private bool played;

	// Token: 0x04000B67 RID: 2919
	public bool forceHover;

	// Token: 0x04000B68 RID: 2920
	private int coeff = -1;
}
