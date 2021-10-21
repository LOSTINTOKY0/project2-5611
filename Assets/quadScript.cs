/*using UnityEngine;

public class QuadCreator : MonoBehaviour
{
  public float width = 1;
  public float height = 1;
  public const int nSlices = 100;
  public const int nStacks = 100;

  public void Start()
  {
      MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
      meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

      MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

      Mesh mesh = new Mesh();

      Vector3[] vertices = new Vector3[nStacks*nSlices]
      {
        for(float x = -width; x<width; x+=(2.0*width/nslices)){
            for(float y = -height/2.0; y < (height/2.0)+height/nstacks; y+=(height/nstacks)){
                 float y2 = y+(height/nstacks);
                 float x2 = x+(2.0*(width/nslices));


             //first 2 triangle points
                 vertices[nSlices*x + y].push_back(Point3(x,y,0));//1
                 vertices[nSlices*x + y]push_back(Point3(x2,y,0));//2
       }
     }
      };
      mesh.vertices = vertices;

      int[] tris = new int[nStacks*nSlices]
      {
          // lower left triangle
          0, 2, 1,
          // upper right triangle
          2, 3, 1
      };
      mesh.triangles = tris;

      Vector3[] normals = new Vector3[nStacks*nSlices]
      {
          -Vector3.forward,
          -Vector3.forward,
          -Vector3.forward,
          -Vector3.forward
      };
      mesh.normals = normals;

      Vector2[] uv = new Vector2[nStacks*nSlices]
      {
          new Vector2(0, 0),
          new Vector2(1, 0),
          new Vector2(0, 1),
          new Vector2(1, 1)
      };
      mesh.uv = uv;

      meshFilter.mesh = mesh;
  }
}
*/
