using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSimplification : MonoBehaviour
{
    public float simplificationThreshold = 0.1f; // ���Ƽ򻯳̶ȵ���ֵ
   

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.mesh != null)
        {
            Mesh mesh = meshFilter.mesh;
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;

            int[] simplifiedTriangles;
            Vector3[] simplifiedVertices;

            SimplifyMesh(vertices, triangles, out simplifiedVertices, out simplifiedTriangles);



            mesh.Clear();
            mesh.vertices = simplifiedVertices;
            mesh.triangles = simplifiedTriangles;
            mesh.RecalculateNormals();

            // ����򻯺�Ķ�����
            int originalVertexCount = vertices.Length;
            int simplifiedVertexCount = simplifiedVertices.Length;
            Debug.Log("Origin Vertices number: " + originalVertexCount);
            Debug.Log("Simplified Vertex number: " + simplifiedVertexCount);


        }
    }

    void SimplifyMesh(Vector3[] vertices, int[] triangles, out Vector3[] simplifiedVertices, out int[] simplifiedTriangles)
    {
        // ʹ��RDP�㷨���������
        // ����ֻ�򻯶��㣬���ı���������˽ṹ

        int[] pointIndexMap = new int[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
            pointIndexMap[i] = i;

        Simplify(vertices, 0, vertices.Length - 1, pointIndexMap, simplificationThreshold);

        // �����򻯺�Ķ��������������
        simplifiedVertices = new Vector3[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
            simplifiedVertices[i] = vertices[pointIndexMap[i]];

        simplifiedTriangles = triangles;
    }

    void Simplify(Vector3[] vertices, int start, int end, int[] pointIndexMap, float threshold)
    {
        if (start < end)
        {
            int index = -1;
            float maxDistance = 0f;

            // �ҵ�������Զ�ĵ㣬��Ϊ��ǰ�εĶ˵�
            for (int i = start + 1; i < end; i++)
            {
                float distance = GetPerpendicularDistance(vertices[i], vertices[start], vertices[end]);
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    index = i;
                }
            }

            if (maxDistance > threshold)
            {
                // ����ǰ�εĶ˵����򻯺�����飬�������ݹ��
                pointIndexMap[index] = start;
                Simplify(vertices, start, index, pointIndexMap, threshold);
                Simplify(vertices, index, end, pointIndexMap, threshold);
            }
        }
    }

    float GetPerpendicularDistance(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        // ����㵽ֱ�ߵĴ�ֱ����
        Vector3 lineDirection = lineEnd - lineStart;
        float lineLengthSquared = Vector3.Dot(lineDirection, lineDirection);
        float t = Mathf.Clamp01(Vector3.Dot(point - lineStart, lineDirection) / lineLengthSquared);
        Vector3 projection = lineStart + t * lineDirection;
        return Vector3.Distance(point, projection);
    }
}
