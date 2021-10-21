using UnityEngine;
using System.Collections.Generic;
public class ClothSim : MonoBehaviour
{





  
//public gameObject m;// mesh;
  public float width = 1;
  public float height = 1;
  public const int nSlices = 10;
  public const int nStacks = 10;
  int stringsY = nSlices;
  int stringsX = nStacks;
  int numNodes = nStacks;
  Vector3 gravity = new Vector3(0,400,0);
  Vector3 stringTop = new Vector3(20,50,30);

  //need to be initialized in start but no time rn
  float restLen;// = width/nSlices;
  //float restLenY;// = height/nStacks;
  float mass = 1.0f; //
  float k = 2500;
  float kv = 50;
  float friction = -.005f;

  //need to put positions of nodes into list by down-across rather than across-down
  Vector3[] pos = new Vector3[nSlices*nStacks*2];
  Vector3[] vel = new Vector3[nSlices*nStacks*2];
  Vector3[] acc = new Vector3[nSlices*nStacks*2];

    List<Vector3> newVertices = new List<Vector3>();
    List<Vector2> newUV= new List<Vector2>();
    List<int> newTriangles= new List<int>();
     //BoneWeight[] weights; //= new List<BoneWeight>();

   public void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
      ready(mesh);
      Debug.Log(newVertices.Count);
      setupMesh(mesh);
    }

public void Update(){
   //gravity gets reset each time
  for(int j = 0; j< nSlices*2; j++){
    for(int i = 0; i < nStacks; i++){
        //Debug.Log(j*nSlices + i);
        acc[j*nSlices + i] = gravity;
      //Debug.Log(j*nSlices + i);

    }
  }


  //Compute (damped) Hooke's law for each spring
    for (int j = 0; j < nSlices; j++){
  for (int i = 0; i < nStacks-1; i++){
    Vector3 diff = pos[nSlices*j + i+1]- pos[j*nSlices + i];
    float stringF = -k*(diff.magnitude - restLen);
    diff.Normalize();
    Vector3 stringDir = diff;
    float projVbot = Vector3.Dot(vel[nSlices*j + i], stringDir);
    float projVtop = Vector3.Dot(vel[nSlices*j +i+1], stringDir);
    float dampF = -kv*(projVtop - projVbot);
    Vector3 force = stringDir*(stringF+dampF);
    acc[j*nSlices +i] = acc[j*nSlices +i]+(force*((-1.0f/mass)));
    acc[j*nSlices +i+1] = acc[j*nSlices +i+1]+(((1.0f/mass))*force);
    vel[j*nSlices +i] = vel[j*nSlices +i]+(vel[j*nSlices +i]*(friction));
    vel[j*nSlices +i+1]= vel[j*nSlices +i+1]+(vel[j*nSlices +i+1]*(friction));
  }
    }
  //Eulerian integration
    for (int j = 0; j < nSlices; j++){
  for (int i = 1; i < nStacks; i++){
    vel[j*nSlices +i] = vel[j*nSlices +i]+(acc[j*nSlices +i]*(Time.deltaTime));
    pos[j*nSlices +i] = pos[j*nSlices +i]+(vel[j*nSlices +i]*(Time.deltaTime));
  }
    }
    updateMesh(GetComponent<MeshFilter>().mesh);
    }


void ready(Mesh mesh){
  for(float x = -width; x<width - width/nSlices; x+=(2.0f*width/nSlices)){
      for(float y = -height; y < (height); y+=(height/nStacks)){

          // float y2 = y+(2.0f*height/nStacks);
           float x2 = x+(2.0f*(width/nSlices));

           //starts at 0,0 goes to 1,1 for image
               float imgX = -(width - x)/(2.0f*width);
               float imgX2 = -(width - x2)/(2.0f*width);
               float imgY = (height/2.0f - y)/height;



       //first 2 triangle points
           newVertices.Add( new Vector3(x,y,0));//1
           newVertices.Add(new Vector3(x2,y,0));//2
      //add normals
        //   normals.Add(Vector3(0,0,1).ToUnit());
        //   normals.Add(Vector3(0,0,1).ToUnit());
       //first 2 triangle image points
           newUV.Add(new Vector2(imgX,imgY));
           newUV.Add(new Vector2(imgX2,imgY));


           //now triangle indices
           if(y!=-height){
           int vert = newVertices.Count-1;
               newTriangles.Add(vert);
               newTriangles.Add( vert-1);
               newTriangles.Add(vert-2);
               // newTriangles for the second triangle, note some are reused
               newTriangles.Add(vert-1);
               newTriangles.Add(vert-3);
               newTriangles.Add(vert-2);

           }
  }}
}



void setupMesh(Mesh mesh){
  mesh.vertices = newVertices.ToArray();
    mesh.uv = newUV.ToArray();
    mesh.triangles = newTriangles.ToArray();
    Debug.Log(mesh.vertices.Length + " " + nSlices*nStacks*2);

    /*weights = new BoneWeight[newVertices.Count];
    for (int i = 0; i<newVertices.Count; i++) {
      weights[i].boneIndex0 = 0;

      weights[i].weight0  = 0;

    }
    mesh.boneWeights = weights; */
}

void updateMesh(Mesh mesh){
  mesh.vertices = pos;
}

//how to setupMesh in 3D?
//original code has nodes downwards,
// x1, y1, then next would be x1 + width, y2
//width*x + y
 //transfer proecsisng code to here!!!






//processing code



/*

//Draw the scene: one sphere per mass, one line connecting each pair
boolean paused = true;
void draw() {
  background(255,255,255);

  //update(1/(20*frameRate))
  if (!paused) {
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));
update(1/(20*frameRate));




};

    for (int j = 0; j < strings; j++){
  for (int i = 0; i < numNodes-1; i++){

    pushMatrix();
    line(pos.get(j)[i].x,pos.get(j)[i].y, pos.get(j)[i+1].z,pos.get(j)[i+1].x,pos.get(j)[i+1].y,pos.get(j)[i+1].z);
    translate(pos.get(j)[i+1].x,pos.get(j)[i+1].y,pos.get(j)[i+1].z);
    lights();
    fill(230,90,230);
    sphere(radius);
    popMatrix();
  }
    }

  }



*/


}
