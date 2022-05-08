using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public bool cooldown;
    public int picked = 0;
    private float speed = 2f;
    private float waitTime = 3f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {  
        
        if(other.gameObject.CompareTag("Player") && cooldown == false){
            Destroy(this.gameObject);
            picked++;
            cooldown = true;
            StartCoroutine(CDCounter(waitTime));
        }
    }

    private IEnumerator CDCounter(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            cooldown = false;
        }
    }
    

}
