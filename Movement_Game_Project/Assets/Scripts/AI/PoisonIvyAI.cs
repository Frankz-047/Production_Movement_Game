using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonIvyAI : BaseAI
{

    public WallPoisonComponent ObjectInContact;
    public GameObject go_DropletObject;
    public float f_TimerBeforeDrop;
    public float f_yOffset;

    private bool b_canFire = true;
    //for testing later to change into funcionts ******
    public bool isAlive = true;
    //********
    private float f_yLower;
    private Vector3 v3_BottomCoordinate;

    // Start is called before the first frame update
    void Start()
    {
        //get bottom position of object in contact with the AI 
        float f_yHalfExtents = ObjectInContact.GetComponent<BoxCollider>().bounds.extents.y;
        
        f_yLower = (ObjectInContact.transform.position.y - f_yHalfExtents) - f_yOffset;
        v3_BottomCoordinate = new Vector3(ObjectInContact.transform.position.x, f_yLower, ObjectInContact.transform.position.z);
        Debug.Log(f_yLower);
    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive)
        {
            if (ObjectInContact != null && b_canFire)
            {
                ObjectInContact.b_isAttachedToDandelion = true;
                GameObject droplet = Instantiate(go_DropletObject, v3_BottomCoordinate, ObjectInContact.transform.rotation);
                droplet.transform.SetParent(this.transform);
                StartCoroutine(DropPoison());
            }
        }
        else
        {
            ObjectInContact.b_isAttachedToDandelion = false;
        }
        
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(v3_BottomCoordinate, 1);
    }
    private IEnumerator DropPoison()
    {
        b_canFire = false;
        //wait 3 seconds before the droplet falls to next object
        yield return new WaitForSeconds(6);

        b_canFire = true ;
        
        
    }
}
