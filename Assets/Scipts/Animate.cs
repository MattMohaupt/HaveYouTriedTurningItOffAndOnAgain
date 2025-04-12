using UnityEngine;

public class Animate : MonoBehaviour {
    private Animator anim;

    void Start() {
        // Get an instance of the Animator component attached to the character.
        anim = GetComponent<Animator>();
    }

    void Update() {
        bool isWalking = anim.GetBool("Walk");
        bool isMoving = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W);

        if (!isWalking && isMoving) {
            // Set the bool value to true for the parameter Walk.
            anim.SetBool("Walk", true);
        }

        if (isWalking && !isMoving) {
            // Set bool value to false to stop walking animation
            anim.SetBool("Walk", false);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            // Set the trigger value to True for the parameter PickUp.
            anim.SetTrigger("PickUp");
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            // Set trigger value to True for parameter Use
            anim.SetTrigger("Use");
        }
    }
}