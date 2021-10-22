using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
public class ClothSim : MonoBehaviour
{

  public float width = 1; //width of cloth
  public float height = 1; //height of cloth
  public const int nSlices = 10; //number of verts in x direction
  public const int nStacks = 10; //number of verts in y direction
  int stringsY = nSlices;
  int stringsX = nStacks;
  int numNodes = nStacks;
  Vector3 gravity = new Vector3(0,0,0);
  Vector3 stringTop = new Vector3(20,50,30); // not used atm

  //need to be initialized in start but no time rn
  float restLen = .2f;// = width/nSlices;
  //float restLenY;// = height/nStacks;
  float mass = 1.0f; //
  float k = 2500;
  float kv = 50;
  float friction = -.005f;
  float ks = 2500; //TRY-IT: How does changing k affect resting length of the rope?
float kd = 50;

  //need to put positions of nodes into list by down-across rather than across-down
  Vector3[] pos = new Vector3[nSlices*nStacks*2];
  Vector3[] vel = new Vector3[nSlices*nStacks*2];
  Vector3[] newVel = new Vector3[nSlices*nStacks*2];
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
  for(int j = 0; j< nSlices; j++){
    for(int i = 0; i <= nStacks; i++){
        //Debug.Log(j*nSlices + i);
        acc[j*nSlices + i] = gravity;

      //Debug.Log(j*nSlices + i);

    }
  }


  //Compute (damped) Hooke's law for each spring
    for (int j = 0; j < nSlices; j++){
  for (int i = 0; i <=  nStacks-1; i++){
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
  for (int i = 1; i <= nStacks; i++){
    vel[j*nSlices +i] = vel[j*nSlices +i]+(acc[j*nSlices +i]*(Time.deltaTime));
    pos[j*nSlices +i] = pos[j*nSlices +i]+(vel[j*nSlices +i]*(Time.deltaTime));
    //Debug.Log("delta time is " +Time.deltaTime);

  //  Debug.Log("new pos is " + pos[j*nSlices +i]+(vel[j*nSlices +i]*(Time.deltaTime)/2.0f));
  }
    }
    //updateMesh(GetComponent<MeshFilter>().mesh);
    }


void ready(Mesh mesh){
  int a = 0;
  for(float x = 0; x< width; x+=(width/(float)nSlices)){
      for(float y = 0; y <= height ; y+=(height/(float)nStacks)){
       Debug.Log(x + " "+ y);
           float x2 = x+(2.0f*(width/nSlices));

           //starts at 0,0 goes to 1,1 for image
               float imgX = -(width - x)/(2.0f*width);
               float imgX2 = -(width - x2)/(2.0f*width);
               float imgY = (height/2.0f - y)/height;



       //first 2 triangle points
           newVertices.Add( new Vector3(x,y,0));//1
           newVertices.Add(new Vector3(x2,y,0));//2
           //Debug.Log("a is "+ a + " pos length is"+ pos.Length);
           Debug.Log("vertices size is "+newVertices.Count);
           pos[a] = new Vector3(x,y,0);
           a++;
           pos[a] = new Vector3(x2,y,0);
           a++;
           newUV.Add(new Vector2(imgX,imgY));
           newUV.Add(new Vector2(imgX2,imgY));


           //now triangle indices
           if(y!=0){
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
    Debug.Log(mesh.vertices.Length + " " + nSlices*nStacks);
}

void updateMesh(Mesh mesh){


  newVel = vel; //start with old vels.
  //(new velocity buffer)
  //Update vels. before pos.
  for(int i  = 0; i<(nSlices-1); i++){
    for(int j = 0; j<nStacks; j++){

      Vector3 e = pos[(i+1)*nSlices + j] - pos[i*nSlices+j];
      float l = (float)Math.Sqrt(Vector3.Dot(e,e));
      e = e/l; //normalize
    float  v1 = Vector3.Dot(e,vel[i*nSlices*j]);
      float v2 = Vector3.Dot(e,vel[(i+1)*nSlices+j]);
      float f = -ks*(l - restLen)-kd*(v1-v2);
      newVel[i*nSlices+j] += f*e*Time.deltaTime;
      newVel[(i+1)*nSlices+j] -= f*e*Time.deltaTime;
}
}//vert
for(int i  = 0; i<(nSlices); i++){
  for(int j = 0; j<(nStacks-1); j++){
    Vector3 e = pos[i*nSlices+j+1] - pos[i*nSlices+j+j];
    float l = (float)Math.Sqrt(Vector3.Dot(e,e));
      e = e/l; //normalize
    float v1 = Vector3.Dot(e,vel[i*nSlices+j]);
    float v2 = Vector3.Dot(e,vel[i*nSlices+j+1]);
  float  f = -ks*(l-restLen)-kd*(v1-v2);
    newVel[i*nSlices+j] += f*e*Time.deltaTime;
    newVel[i*nSlices+j+1] -= f*e*Time.deltaTime;




for(int k= 0; k<newVel.Length; k++){
  if(k<=nSlices){
    newVel[k] = new Vector3(0,0,0);
  }else if (k>nSlices){
      newVel[k] = new Vector3(0,-0.1f,0);
    }     //fix top row
  vel[k] = newVel[k];//update vel.
  pos[k] += vel[k]*Time.deltaTime;
}
     //update pos
  }
}  //horizontal


    mesh.vertices = pos;

}
}
