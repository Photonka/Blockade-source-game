using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class MeshBuilder
{
	// Token: 0x06000912 RID: 2322 RVA: 0x00080BE4 File Offset: 0x0007EDE4
	public void AddVertices(Vector3[] vertices, Vector3 offset)
	{
		foreach (Vector3 vector in vertices)
		{
			this.vertices.Add(vector + offset);
		}
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x00080C1B File Offset: 0x0007EE1B
	public void AddNormals(Vector3[] normals)
	{
		this.normals.AddRange(normals);
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x00080C2C File Offset: 0x0007EE2C
	public void AddColor(Color color)
	{
		float num = color.a;
		if (num < 0.1f)
		{
			num = 0.5f;
		}
		this.colors.Add(new Color(num, num, num, 1f));
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x00080C68 File Offset: 0x0007EE68
	public void AddFaceColor(Color color)
	{
		this.colors.Add(new Color(color.a, color.a, color.a, 1f));
		this.colors.Add(new Color(color.a, color.a, color.a, 1f));
		this.colors.Add(new Color(color.a, color.a, color.a, 1f));
		this.colors.Add(new Color(color.a, color.a, color.a, 1f));
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x00080D14 File Offset: 0x0007EF14
	public void AddColors(Color color, int count)
	{
		for (int i = 0; i < count; i++)
		{
			float a = color.a;
			this.colors.Add(new Color(a, a, a, 1f));
		}
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x00080D4C File Offset: 0x0007EF4C
	public void AddTexCoords(Vector2[] uv)
	{
		this.uv.AddRange(uv);
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00080D5C File Offset: 0x0007EF5C
	public void AddFaceIndices(int materialIndex)
	{
		int count = this.vertices.Count;
		List<int> list = this.GetIndices(materialIndex);
		list.Add(count + 2);
		list.Add(count + 1);
		list.Add(count);
		list.Add(count + 3);
		list.Add(count + 2);
		list.Add(count);
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00080DB0 File Offset: 0x0007EFB0
	public void AddIndices(int materialIndex, int[] indices)
	{
		int count = this.vertices.Count;
		List<int> list = this.GetIndices(materialIndex);
		foreach (int num in indices)
		{
			list.Add(num + count);
		}
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00080DF0 File Offset: 0x0007EFF0
	public List<int> GetIndices(int index)
	{
		if (index >= this.indices.Length)
		{
			int num = this.indices.Length;
			Array.Resize<List<int>>(ref this.indices, index + 1);
			for (int i = num; i < this.indices.Length; i++)
			{
				this.indices[i] = new List<int>();
			}
		}
		return this.indices[index];
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00080E45 File Offset: 0x0007F045
	public List<Color> GetColors()
	{
		return this.colors;
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00080E50 File Offset: 0x0007F050
	public void Clear()
	{
		this.vertices.Clear();
		this.uv.Clear();
		this.normals.Clear();
		this.colors.Clear();
		List<int>[] array = this.indices;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Clear();
		}
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00080EA8 File Offset: 0x0007F0A8
	public Mesh ToMesh(Mesh mesh)
	{
		if (this.vertices.Count == 0)
		{
			Object.Destroy(mesh);
			return null;
		}
		if (mesh == null)
		{
			mesh = new Mesh();
		}
		mesh.Clear();
		mesh.vertices = this.vertices.ToArray();
		mesh.colors = this.colors.ToArray();
		mesh.normals = this.normals.ToArray();
		mesh.uv = this.uv.ToArray();
		mesh.subMeshCount = this.indices.Length;
		for (int i = 0; i < this.indices.Length; i++)
		{
			mesh.SetTriangles(this.indices[i].ToArray(), i);
		}
		return mesh;
	}

	// Token: 0x04000E72 RID: 3698
	private List<Vector3> vertices = new List<Vector3>();

	// Token: 0x04000E73 RID: 3699
	private List<Vector2> uv = new List<Vector2>();

	// Token: 0x04000E74 RID: 3700
	private List<Vector3> normals = new List<Vector3>();

	// Token: 0x04000E75 RID: 3701
	private List<Color> colors = new List<Color>();

	// Token: 0x04000E76 RID: 3702
	private List<int>[] indices = new List<int>[0];
}
