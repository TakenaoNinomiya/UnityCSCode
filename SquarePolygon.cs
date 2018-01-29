using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePolygon : MonoBehaviour
{
    public Vector3[] verticesPosition;
    [SerializeField] float height;
    [SerializeField] float width;
    [SerializeField] int squareMeshCount;
    public Vector3[] VerticesPosition
    {
        get { return verticesPosition; }
        set { verticesPosition = value; MeshCreate(squareMeshCount); }
    }
    public float Height
    {
        get { return height; }
        set { height = value; MeshCreate(squareMeshCount); }
    }
    public float Width
    {
        get { return width; }
        set { width = value; MeshCreate(squareMeshCount); }
    }
    void Start()
    {
        if (squareMeshCount <= 0) { Debug.Log("1以上を指定して下さい"); return; }
        //SetPosition();

    }
    private void OnValidate()
    {
        MeshCreate(squareMeshCount);
    }
    public void SetPosition()
    {
        verticesPosition = new Vector3[(squareMeshCount - 1) * 2 + 4];
        for (int i = 0; i < verticesPosition.Length; i++)
        {
            if (i == 0) { verticesPosition[i] = Vector3.zero; continue; }
            int caseSwitch = i % 4;
            switch (caseSwitch)
            {
                case 0:
                    verticesPosition[i] = verticesPosition[i - 2] + new Vector3(0, height, 0);
                    break;
                case 1:
                    verticesPosition[i] = verticesPosition[i - 1] + new Vector3(width, 0, 0);
                    break;
                case 2:
                    verticesPosition[i] = verticesPosition[i - 2] + new Vector3(0, height, 0);
                    break;
                case 3:
                    verticesPosition[i] = verticesPosition[i - 1] + new Vector3(width, 0, 0);
                    break;
            }
        }
        MeshCreate(squareMeshCount);
    }
    public void SetRingPosition(int count,float radius,float width)
    {

        verticesPosition = new Vector3[(count - 1) * 2 + 4];
        float[] x = new float[(count * 2) + 2];
        float[] y = new float[(count * 2) + 2];
        float[] v = new float[(count * 2) + 2];
        float[] w = new float[(count * 2) + 2];
        float coef = 360f / count;
        for (int i = 0; i < (count * 2) + 2; i++)
        {
            x[i] = Mathf.Cos(((i * coef) * Mathf.Deg2Rad)) * radius;
            y[i] = Mathf.Sin(((i * coef) * Mathf.Deg2Rad)) * radius;

            v[i] = Mathf.Cos((i * coef) * Mathf.Deg2Rad) * width;
            w[i] = Mathf.Sin((i * coef) * Mathf.Deg2Rad) * width;
        }
        for (int i = 0; i < verticesPosition.Length; i++)
        {
            int caseSwitch = i % 2;
            switch (caseSwitch)
            {
                case 0:
                    verticesPosition[i] = new Vector3(x[i], y[i], 0);
                    break;
                case 1:
                    verticesPosition[i] = new Vector3(x[i] - v[i], y[i] - w[i], 0);
                    break;
            }
        }
        MeshCreate(count);
    }
    void MeshCreate(int count)
    {
        if (verticesPosition == null || verticesPosition.Length <= 0) return;
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh mesh = mf.mesh = new Mesh();
        int[] triangles = new int[count * 2 * 3];
        int index = 0;

        index = 0;

        for (int i = 0; i < count; i++)
        {
            triangles[index++] = (i * 2) + 0;
            triangles[index++] = (i * 2) + 1;
            triangles[index++] = (i * 2) + 3;


            triangles[index++] = (i * 2) + 0;
            triangles[index++] = (i * 2) + 3;
            triangles[index++] = (i * 2) + 2;

        }
        mesh.vertices = verticesPosition;
        mesh.triangles = triangles;

    }

}
