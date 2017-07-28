using System;
using UnityEngine;
using ConstructiveSolidGeometry;

public class CSG_ops
{
    public static void CSG_calculations(GameObject first_selected, GameObject second_selected, GameObject new_obj, int control)
    {
        CSG A = CSG.fromMesh(first_selected.GetComponent<MeshFilter>().mesh, first_selected.transform);
        CSG B = CSG.fromMesh(second_selected.GetComponent<MeshFilter>().mesh, second_selected.transform);

        CSG result;
        if(control == 0) result = A.intersect(B);
        else if(control == 1) result = A.union(B);
        else if (control == 2) result = A.subtract(B);
        else if (control == 3) result = B.subtract(A);
        else
        {
            CSG temp1 = A.subtract(B);
            CSG temp2 = B.subtract(A);
            result = temp1.union(temp2);
        }
        //memory leak
        if (result != null) new_obj.GetComponent<MeshFilter>().mesh = result.toMesh();
        //problems with the pivot
        return;
    }
}