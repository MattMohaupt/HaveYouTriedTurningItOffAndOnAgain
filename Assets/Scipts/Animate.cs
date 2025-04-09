using UnityEngine;

public class Animate : MonoBehaviour {
    private Animator anim;

    void Start() {
        // Get an instance of the Animator component attached to the character.
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W)) {
            // Set the trigger value to True for the parameter Walk.
            anim.SetTrigger("Walk");
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            // Set the trigger value to True for the parameter PickUp.
            anim.SetTrigger("PickUp");
        }
    }
}