using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    //Parçaların listesi
    private List<Transform> destructibles = new List<Transform>();
     
	public void Destruct(Vector3 velocity)
    {
        foreach(Transform t in transform)
        {
           
            if(t != transform)
            {
                

                if (t.gameObject.GetComponent(typeof(MeshRenderer)))
                {
                   
                    //parçaları listeye ekle
                    
                    destructibles.Add(t);
                }
            }
        }

        foreach(Transform t in destructibles)
        {
            //Parçaladık
            InsertComponents(t);
            t.GetComponent<Rigidbody>().AddForce(velocity*10);
            Destroy(t.gameObject,3f);
        }
    }

    //Parçalamak için gerekli componentleri ekle
    private void InsertComponents(Transform t)
    {
        t.gameObject.AddComponent<Rigidbody>();
        MeshCollider collider = t.gameObject.AddComponent<MeshCollider>();
        collider.convex = true;
        t.gameObject.transform.SetParent(null);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player") && !transform.CompareTag("Player"))
        {
            Destruct(Vector3.zero);
        } 
    }

   
}
