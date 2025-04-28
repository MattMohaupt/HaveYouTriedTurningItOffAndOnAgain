//using UnityEditor.PackageManager;
using UnityEngine;

public class AnimateNPCs : MonoBehaviour
{
    private Animator anim;
    private Vector3 previousPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        previousPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, previousPosition) > 0.01f)  
        {
            // NPC is moving
            anim.SetBool("npcWalk", true);
        }
        else
        {
            // NPC is not moving
            anim.SetBool("npcWalk", false);
        }

        // Update the previous position to the current position for the next frame
        previousPosition = transform.position;
    }
}
