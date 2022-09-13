using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : Card
{
    public override bool Play()
    {
        if (!this.GetComponent<PlayerMovement>().GetCanJump())
        {
            Rigidbody rb = this.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(transform.up * this.GetComponent<PlayerMovement>().jumpForce, ForceMode.Impulse);
            return true;
        }
        return false;
    }
    public override void AddToObj(GameObject obj) 
    {
        obj.AddComponent<DoubleJump>();
    }
}
