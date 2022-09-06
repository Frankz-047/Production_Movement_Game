using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPoisonComponent : MonoBehaviour
{
    //overalReaction To poision
    internal bool b_isAttachedToDandelion = false;
    public bool b_isPoisonous = false;

    public float f_PoisonDuration;
    public Material m_originalMaterial;
    public Material m_poisonMaterial;
    private float f_PosiontimeLeft;
    // For Damagin Player

    private PlayerHealth p_playerInContact;
    private bool b_canDamagePlayer = true;

    // Start is called before the first frame update

    void Start()
    {
    
        f_PosiontimeLeft = f_PoisonDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (b_isAttachedToDandelion)
        {
            GetComponent<Renderer>().material = m_poisonMaterial;
        }
        else
        {
            f_PosiontimeLeft -= Time.deltaTime;
            if (f_PosiontimeLeft <= 0)
            {
                b_isPoisonous = false; ;
                GetComponent<Renderer>().material = m_originalMaterial;
            }
        }
      
        if(p_playerInContact != null && b_canDamagePlayer && b_isPoisonous)
        {
            p_playerInContact.Hit();
            StartCoroutine(DamagePlayer());
        }
        // deal damage 

    }


    private IEnumerator DamagePlayer()
    {
        b_canDamagePlayer = false;
        //wait 3 seconds before the droplet falls to next object
        yield return new WaitForSeconds(2);

        b_canDamagePlayer = true;


    }
    
    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.GetComponent<PlayerHealth>())
        {
            p_playerInContact = collision.gameObject.GetComponent<PlayerHealth>();
        }

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerHealth>())
        {
            p_playerInContact = null;
        }
    }
    public void setPoisous()
    {
        m_originalMaterial = GetComponent<Renderer>().material;
        b_isPoisonous = true;
        f_PosiontimeLeft = f_PoisonDuration;
        GetComponent<Renderer>().material = m_poisonMaterial;
    }
}
