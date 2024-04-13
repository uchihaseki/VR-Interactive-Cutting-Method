using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
//using static Oculus.Interaction.Grabbable;
//using static Oculus.Interaction.GrabInteractable;



public class SliceObject : MonoBehaviour
{
    //public Transform planeDebug;
    //public GameObject target;

    public Transform startSlicePoint;
    public Transform endSlicePoint;
   

    public VelocityEstimator velocityEstimator;
    public LayerMask sliceableLayer;


    public Material crossSectionMaterial;
    public float cutForce = 2000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //  void Update()
    //  {
    //      if(Keyboard.current.spaceKey.wasPressedThisFrame)
    //      {
    //          Slice(target);
    //      }
    //  }


    // start & end point -> point 1 2 3 4 5... Then do RDP to reduce points -> loop to do slice 



    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if(hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if(hull != null)
        {
            //GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            //SetupSlicedComponent(upperHull);
            //
            //GameObject loverHull = hull.CreateLowerHull(target, crossSectionMaterial);
            //SetupSlicedComponent(loverHull);
            //
            //Destroy(target);

            GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
            SetupSlicedComponent(upperHull);
            upperHull.layer = target.layer;


            GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
            SetupSlicedComponent(lowerHull);
            lowerHull.layer = target.layer;

            Destroy(target);
        }


    }


    //Need to add smth to do the interaction -------- grab
    public void SetupSlicedComponent(GameObject slicedObject)
    {
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, slicedObject.transform.position, 1);
    }

}
