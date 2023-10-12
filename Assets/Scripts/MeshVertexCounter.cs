using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshVertexCounter : MonoBehaviour
{
    void Start()
    {
        // 获取网格过滤器组件
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            // 获取网格
            Mesh mesh = meshFilter.sharedMesh;

            if (mesh != null)
            {
                // 获取顶点数量
                int vertexCount = mesh.vertexCount;

                // 输出顶点数量
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
