using UnityEngine;

public static class MeshFactory
{
  public static Mesh GenerateVisionConeMesh(float visionAngle, float maxDistance, int segments)
  {
    Mesh mesh = new Mesh();

    // Define the vertices of the cone
    Vector3[] vertices = new Vector3[segments + 2]; // One vertex for the center + base circle vertices

    // Center point, which will be the origin of the cone
    vertices[0] = Vector3.zero;

    // Angle increment between segments
    float angleIncrement = visionAngle / segments;
    float currentAngle = -visionAngle / 2;

    // Generate vertices around the base of the cone
    for (int i = 1; i <= segments + 1; i++)
    {
      float radian = Mathf.Deg2Rad * currentAngle;
      float x = Mathf.Sin(radian) * maxDistance;
      float z = Mathf.Cos(radian) * maxDistance;

      vertices[i] = new Vector3(x, 0, z); // Setting the point on the boundary
      currentAngle += angleIncrement;
    }

    // Define the triangles
    int[] triangles = new int[segments * 3]; // Each triangle has 3 vertices

    for (int i = 0; i < segments; i++)
    {
      triangles[i * 3] = 0; // Always the center point
      triangles[i * 3 + 1] = i + 1; // Current base vertex
      triangles[i * 3 + 2] = i + 2; // Next base vertex
    }

    // Fix the last triangle to close the cone
    triangles[(segments - 1) * 3 + 2] = 1;

    // Assign vertices and triangles to the mesh
    mesh.vertices = vertices;
    mesh.triangles = triangles;

    Vector3[] normals = new Vector3[vertices.Length];
    for (int i = 0; i < normals.Length; i++)
    {
      normals[i] = Vector3.up; // Normals facing upwards, as the cone is flat on the ground
    }

    mesh.normals = normals;

    return mesh;
  }

}