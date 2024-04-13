using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVertexCounter : MonoBehaviour
{
    void Start()
    {
        // ��ȡ������������
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            // ��ȡ����
            Mesh mesh = meshFilter.sharedMesh;

            if (mesh != null)
            {
                // ��ȡ��������
                int vertexCount = mesh.vertexCount;

                // �����������
                Debug.Log("Vertex count: " + vertexCount);
            }
            else
            {
                Debug.LogWarning("No mesh found on MeshFilter component.");
            }
        }
        else
        {
            Debug.LogWarning("No MeshFilter component found on this GameObject.");
        }
    }
}
