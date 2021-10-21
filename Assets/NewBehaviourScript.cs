using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quadScript : MonoBehaviour
{
/*
//  Mesh mesh,
  Mesh m;
  int verts = 100;
  Vector3[] vertices = new Vector3[verts];
    // Start is called before the first frame update

    */
    void Start()
    {
    /*   //m = GetComponent<MeshFilter>().sharedMesh;
      for(int i = 0; i < verts/2; i++){
          for(int j = 0; j < verts/2; j++){
        vertices[i] += new Vector3(i,j,0);


      }
      }
      m.vertices = vertices;
      GetComponent<MeshFilter>().sharedMesh = m;

      */
    }

    // Update is called once per frame
    void Update()
    {

    }
  }

/*


  using UnityEngine;

    public class MeshCreator : MonoBehaviour
    {
        public float width = 1;
        public float height = 1;
        public int nSlices = 100;
        public int nStacks = 100;
        public void Start()
        {
            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));

            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[nSlices*nStacks];
              int[] tris = new int[verts];

           for(float x = -width; x<width; x+=(2.0*width/nslices)){
               for(float y = -height/2.0; y < (height/2.0)+height/nstacks; y+=(height/nstacks)){
                    float y2 = y+(height/nstacks);
                    float x2 = x+(2.0*(width/nslices));

                //starts at 0,0 goes to 1,1
                    float imgX = -(width - x)/(2.0*width);
                    float imgX2 = -(widtg - x2)/(2.0*width);
                    float imgY = (height/2.0 - y)/height;
                //first 2 triangle points
                    vertices.push_back(Point3(x,y,0));//1
                    vertices.push_back(Point3(x2,y,0));//2
                //first 2 triangle normals
                    normals.push_back(Vector3(0,0,1).ToUnit());
                    normals.push_back(Vector3(0,0,1).ToUnit());
                //first 2 triangle image points
                    texcoords.push_back(Point2(imgX,imgY));
                    texcoords.push_back(Point2(imgX2,imgY));
                //add vertices
                if(y!=-width/2.0){
                int vert = vertices.size()-1;
                    indices.push_back(vert);
                    indices.push_back(vert-1);
                    indices.push_back(vert-2);
                    // indices for the second triangle, note some are reused
                    indices.push_back(vert-1);
                    indices.push_back(vert-3);
                    indices.push_back(vert-2);

                }
                }
            }
              for(int i = 0; i < verts/2; i++){
                  for(int j = 0; j < verts/2; j++){
                vertices[i] += new Vector3(width/(verts/2 - i),height/(verts/2 - i),0);
                vertices[i+1] += new Vector3(width/(verts/2 - i),height/(verts/2 - i),0);
                vertices[i+2] += new Vector3(width/(verts/2 - i),height/(verts/2 - i),0);




              }
            }
                new Vector3(0, 0, 0),
                new Vector3(width, 0, 0),
                new Vector3(0, height, 0),
                new Vector3(width, height, 0)

            mesh.vertices = vertices;

            int[] tris = new int[6]
            {
                // lower left triangle
                0, 2, 1,
                // upper right triangle
                2, 3, 1
            };
            mesh.triangles = tris;

            Vector3[] normals = new Vector3[4]
            {
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward
            };
            mesh.normals = normals;

            Vector2[] uv = new Vector2[4]
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
