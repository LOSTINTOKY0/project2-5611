/*using UnityEngine;
using System.Collections.Generic;
public class quadScript : MonoBehaviour
{




vn = v; //start with old vels.
//(new velocity buffer)
//Update vels. before pos.
for(int i  = 0; i<(nx-1); i++){
  for(int j = 0; j<ny; j++){
    e = p[i+1,j] - p[i,j];
    l = np.sqrt(e.dot(e));
    e = e/l; //normalize
    v1 = e.dot(v[i,j]);
    v2 = e.dot(v[i+1,j]);
    f = -ks*(l0-l)-kd*(v1-v2);
    vn[i,j] += f*e*dt;
    vn[i+1,j] -= f*e*dt;
  }
} //horiz.


for(i = 0; i<nx; i++)){
  for(j = 0; j<ny-1; j++){
    e = p[i,j+1] - p[i,j];
    l = np.sqrt(e.dot(e));
    e = e/l; //normalize
    v1 = e.dot(v[i,j]);
    v2 = e.dot(v[i,j+1]);
    f = -ks*(l0-l)-kd*(v1-v2);
    vn[i,j] += f*e*dt;
    vn[i,j+1] -= f*e*dt;
    vn += [0,-.1,0,]; //gravity
    vn[0,:] = 0 ;     //fix top row
    v = vn;//update vel.
    p += v*dt; //update pos
  }
}  //vertical





for(i = 0; i<nx; i++){
  for( j = 0; j<ny; j++){
    d = SpherePos.distTo(p[i,j]);
    if( d < sphereR+.09){
      n = -1*(SpherePos - p[i,j]); //sphere normal
      n.normalize(); n = [n[0],n[1],n[2]];
      bounce = np.multiply(np.dot(v[i,j],n),n);
      v[i,j] -= 1.5*bounce;
      p[i,j] += np.multiply(.1 + sphereR - d, n); //move out

    }

  }

}
}

*/
