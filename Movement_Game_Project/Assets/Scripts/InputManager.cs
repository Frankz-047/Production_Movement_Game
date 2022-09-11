using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerCam Camera;
    [SerializeField] DeckManager Deck;
    [SerializeField] PlayerMovement Body;
    [SerializeField] GrapplingHook Grapple;
    [SerializeField] PlayerWeaponShoot Gun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime;
        Camera.MoveCam(mouseX,mouseY);

        //WASD Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Body.Walk(horizontal,vertical);

        if (Input.GetButtonDown("Fire1"))
        {
<<<<<<< Updated upstream
            Debug.Log("Jumpeee");
=======
>>>>>>> Stashed changes
            Gun.Shoot();
        }
        else if (Input.GetButtonDown("Reload"))
        {
<<<<<<< Updated upstream
            Debug.Log("Jumpeee");
=======
>>>>>>> Stashed changes
            Gun.Reload();
        }
        if (Input.GetButtonDown("Hook"))
        {
<<<<<<< Updated upstream
            Debug.Log("Jumpeee");
=======
>>>>>>> Stashed changes
            Grapple.ShootHook();
        }
        if(Input.GetButtonDown("PlayCard"))
        {
<<<<<<< Updated upstream
            Debug.Log("Jumpeee");
=======
>>>>>>> Stashed changes
            Deck.PlayCard();
        }
        if(Input.GetButtonDown("Jump"))
        {
<<<<<<< Updated upstream
            Debug.Log("Jumpeee");
=======
>>>>>>> Stashed changes
            Body.Jump();
        }
    }
}
