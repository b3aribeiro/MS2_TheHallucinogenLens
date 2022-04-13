using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grab : MonoBehaviour
{
    private float launchForce = 20; //how moch force to apply to a throw
    private float raycastDist = 50; //how far away can an object be grabbed

    public Image reticle; //the dot where you aim
    public Transform holdPoint; // where the players hand would be
    public Transform camTrans; // reference to the camera's transform
    private bool reticleTarget = false; // this check is used to track if the reticle is over a valid target
    public LayerMask grabbableLayerMask; // What layer can be grabbed
    private int grabbableLayer; //The grabbableLayer as an int
    private int ignorePlayerLayer; //This layer does not collide with the player
    private int originalLayer; //Here we can save the original layer the object was on that was picked up
    private Transform heldObject = null; // The held object's transform if a object is held
    private Rigidbody heldRigidbody = null; // The held object's Rigidbody

    private void Start()
    {
        //This gets the layer numbers for IgnorePlayer and grabbable so we dont have to track it ourselves
        ignorePlayerLayer = LayerMask.NameToLayer("IgnorePlayer");
        grabbableLayer = LayerMask.NameToLayer("Grabbable");

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //On mouse click, check for an object to pick up. If an object is already held throw it.
            if (heldObject == null)
            {
                CheckForPickUp();
            }
            else
            {
                LaunchObject();
            }
        }
    }

    void CheckForPickUp()
    {
        //Cast a ray from the camera position in the direction the camera is facing for a distance of raycastDist.
        //Return a collision with an object on a grabbable layer if one is detected.
        RaycastHit hit;
        if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, raycastDist, grabbableLayerMask))
        {
            StartCoroutine(PickUpObject(hit.transform)); //pass in the transform of the collider that was hit
        }
    }

    IEnumerator PickUpObject(Transform _transform)
    {
        heldObject = _transform;
        originalLayer = heldObject.gameObject.layer; // save the original layer
        heldObject.gameObject.layer = ignorePlayerLayer; // ignore player layer - keeps held objects from hitting the players collider
        heldRigidbody = heldObject.GetComponent<Rigidbody>();
        heldRigidbody.isKinematic = true; //ignore gravity and other physics while held

        float t = 0;
        while (t < .4f)
        {
            //lerp the position of the object to the held position for .4 sec
            heldRigidbody.position = Vector3.Lerp(heldRigidbody.position, holdPoint.position, t);
            t += Time.deltaTime;
            yield return null;
        }
        SnapToHand(); //When it is close snap it into place
    }

    void SnapToHand()
    {
        heldObject.position = holdPoint.position;
        heldObject.parent = holdPoint; // make it a child of the hold position so it inharits the position and rotation
    }

    void LaunchObject()
    {
        StopAllCoroutines(); //if the grab coroutine is still running, stop it and skip to the end
        SnapToHand();

        heldRigidbody.isKinematic = false; //regular physics like gravity is active again
        heldObject.position = camTrans.position; //jump to the center camera position so it is more lined up with the retical
        heldRigidbody.AddForce(camTrans.forward * launchForce, ForceMode.VelocityChange);  //throw in the direction the camera is facing
        //ForceMode.VelocityChange means add an instant velocity, and the same for any object regardless of mass

        heldObject.parent = null; //remove it as a child and set it back on the root level of the hierarchy
        StartCoroutine(LetGo());
    }

    IEnumerator LetGo()
    {
        yield return new WaitForSeconds(.1f);
        heldObject.gameObject.layer = originalLayer; //Reset the physics layer
        heldObject = null; //remove the reference to the object 
    }


    private void FixedUpdate()
    {
        RaycastHit hit; //Cast a ray check if its blocked
        if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, raycastDist) && hit.collider.gameObject.layer == grabbableLayer)
        {
            //If the retical is not already red change its color
            if (!reticleTarget)
            {
                reticle.color = Color.red;
                reticleTarget = true; //This bool keeps the color from updatiing if there is no change
            }
        }
        else if (reticleTarget) //if no target is hit and the reticle is active then change it back to white
        {
            reticle.color = Color.white;
            reticleTarget = false;
        }
    }
}