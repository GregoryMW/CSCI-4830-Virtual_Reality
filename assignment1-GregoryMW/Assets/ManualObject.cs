using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualObject : MonoBehaviour
{
    // Mesh Creation
    public Material myMaterial;
    Mesh mesh;
    new MeshRenderer renderer;
    MeshFilter filter;

    // Rotation
    public Transform manualObj;
    public float rotation;
    public float rotateSpeed;
    // This will create a 3d pyramid
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        Vector3[] verts = new Vector3[5];
        verts[0] = new Vector3(0, 0, 0);
        verts[1] = new Vector3(1, 0, 0);
        verts[2] = new Vector3(0, 0, 1);
        verts[3] = new Vector3(1, 0, 1);
        verts[4] = new Vector3((float)0.5, 1, (float)0.5);

        int[] inds = new int[]
        {
            2, 1, 0,
            2, 3, 1,
            0, 1, 4,
            0, 4, 2,
            2, 4, 3,
            1, 3, 4
        };
        mesh.vertices = verts;
        mesh.SetIndices(inds, MeshTopology.Triangles, 0);

        renderer = gameObject.AddComponent<MeshRenderer>();
        renderer.material = myMaterial;
        filter = gameObject.AddComponent<MeshFilter>();
        filter.mesh = mesh;

        rotateSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        rotation += rotateSpeed * Time.deltaTime;
        manualObj.rotation = Quaternion.Euler(0, rotation, 0);
    }
}
