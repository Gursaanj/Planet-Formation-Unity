using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace {
    Mesh mesh;
    int resolution;
    Vector3 localUp;
    Vector3 axisA;
    Vector3 axisB;

    public TerrainFace(Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        // axisA is an orientated version of localUp

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);

        // axisB is the perpendicular component between the vertices localUp and axisA
        
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConsructMesh()
    {
        // Number of vertices that will make up the triangles for the resolution of each TerrainFace

        Vector3[] vertices = new Vector3[resolution * resolution];
        // Number of trinagles that will form with the decision on resolution (r-1)^2 * 2(triangles per shape) *3(vertices per triangle)

        Vector3[] normals = new Vector3[resolution * resolution];

        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 2* 3];
        // indexing the triangles generation count

        int triIndex = 0;

        // Decide where the vertex should be on the face, normalized from 0 to 1 (weighted accumulation)
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                // Index where we are on the for loop, where for each iteration over y we do a whole row of x

                int i = x + (y * resolution);
                Vector2 percent = new Vector2(x, y) / (resolution - 1);

                // Need to move from the centre of the cube to the appropriate corner on a face by adjusting with axisA and axisB

                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                //To pop the face of cube onto the sphere
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = pointOnUnitSphere;
                normals[i] = vertices[i].normalized;

                // time to generate the triangles according to the index, in a clockwise format, so that the mesh would orient the right way
                // the triangles per square will be (i, i+res+1, i+res) and (i, i+1, i+res+1)

                // Making sure to stop counting trinagles at the edges

                if (x != resolution -1 && y != resolution -1)
                {
                    // First triangle
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + resolution + 1;
                    triangles[triIndex + 2] = i + resolution;
                    // Second triangle
                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + resolution + 1;

                    triIndex += 6;
                }
            }
        }

        // To make sure that the number of triangles and vertices match up and errors come about

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        //mesh.RecalculateNormals();
        mesh.normals = normals;
    }
}
