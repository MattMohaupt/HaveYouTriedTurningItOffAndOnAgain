using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// adding namespaces
using Unity.Netcode;
using Unity.Multiplayer.Center.Common.Analytics;
using System.Threading.Tasks;
using System;
// because we are using the NetworkBehaviour class
// NewtorkBehaviour class is a part of the Unity.Netcode namespace
// extension of MonoBehaviour that has functions related to multiplayer
public class PlayerMovement : NetworkBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 200f;
    public float mouseSensitivity = 3f;
    // create a list of colors
    public List<Color> colors = new List<Color>();
    
    [SerializeField] private float pickupForce = 150f;

    [SerializeField] private Transform pickupPoint; // Empty object at the pickup area
    [SerializeField] private float pickupRadius = 0.1f; // Radius for detection area
    [SerializeField] private LayerMask carriableLayer; // Layer for carriable objects

    // For tracking the held object
    private GameObject heldObject;
    // Position to hold the object
    [SerializeField] private Transform holdPoint;
    
    // getting the reference to the prefab
    [SerializeField] private GameObject spawnedPrefab;
    // save the instantiated prefab
    private GameObject instantiatedPrefab;

    public GameObject fixedpc;

    public GameObject cannon;
    public GameObject bullet;

    //checking to make sure if a user is interacting with an item they are locked in place
    public bool canMove = true;

    // reference to the camera audio listener
    [SerializeField] private AudioListener audioListener;
    // reference to the camera
    [SerializeField] private Camera playerCamera;

    private float rotationX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        carriableLayer = LayerMask.GetMask("Carriable");
    }
    // Update is called once per frame
    void Update()
    {
        // check if the player is the owner of the object
        // makes sure the script is only executed on the owners 
        // not on the other prefabs 
        if (!IsOwner) return;

        Vector3 moveDirection = new Vector3(0, 0, 0);

        if(canMove){
            if (Input.GetKey(KeyCode.W))
            {
                moveDirection.z = +1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                moveDirection.z = -1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                moveDirection.x = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveDirection.x = +1f;
            }
            Vector3 worldMoveDirection = transform.TransformDirection(moveDirection.normalized);
            transform.position += worldMoveDirection * speed * Time.deltaTime;
        }
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(0, mouseX, 0);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -30f, 60f);
        playerCamera.transform.localEulerAngles = new Vector3(rotationX, 0, 0);

        // if I is pressed spawn the object 
        // if J is pressed destroy the object
        // if (Input.GetKeyDown(KeyCode.I))
        // {
        //     //instantiate the object
        //     instantiatedPrefab = Instantiate(spawnedPrefab);
        //     // spawn it on the scene
        //     instantiatedPrefab.GetComponent<NetworkObject>().Spawn(true);
        // }

        // if (Input.GetKeyDown(KeyCode.J))
        // {
        //     //despawn the object
        //     instantiatedPrefab.GetComponent<NetworkObject>().Despawn(true);
        //     // destroy the object
        //     Destroy(instantiatedPrefab);
        // }

        // if (Input.GetButtonDown("Fire1"))
        // {
        //     // call the BulletSpawningServerRpc method
        //     // as client can not spawn objects
        //     BulletSpawningServerRpc(cannon.transform.position, cannon.transform.rotation);
        // }

        // will check to see if there is an object the player can pick up
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                DropObject();
            }
        }

        // will check to see if there is an object the player can interact with
        if (Input.GetKeyDown(KeyCode.F)){
            if (heldObject == null){
                TryInteraction();
            }
            else{
                TryInteraction(heldObject);
            }
        }

        // Vector3 rayOrigin = playerCamera.transform.position;
        // Vector3 rayDirection = playerCamera.transform.forward;
        
        // Debug.DrawRay(rayOrigin, rayDirection * 10f, Color.red);
        
        // if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, 10f, carriableLayer))
        // {
        //     Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
        // }

    }

    // this method is called when the object is spawned
    // we will change the color of the objects
    public override void OnNetworkSpawn()
    {
        GetComponent<MeshRenderer>().material.color = colors[(int)OwnerClientId];

        // check if the player is the owner of the object
        if (!IsOwner) return;
        // if the player is the owner of the object
        // enable the camera and the audio listener
        audioListener.enabled = true;
        playerCamera.enabled = true;
    }

    // need to add the [ServerRPC] attribute
    [ServerRpc]
    // method name must end with ServerRPC
    private void BulletSpawningServerRpc(Vector3 position, Quaternion rotation)
    {
        // call the BulletSpawningClientRpc method to locally create the bullet on all clients
        BulletSpawningClientRpc(position, rotation);
    }

    [ClientRpc]
    private void BulletSpawningClientRpc(Vector3 position, Quaternion rotation)
    {
        GameObject newBullet = Instantiate(bullet, position, rotation);
        newBullet.GetComponent<Rigidbody>().linearVelocity += Vector3.up * 2;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1500);
        // newBullet.GetComponent<NetworkObject>().Spawn(true);
    }

    private void TryPickupObject() {
        // Collider[] colliders = Physics.OverlapSphere(pickupPoint.position, pickupRadius);

        // if (colliders.Length > 0)
        // {
        //     GameObject targetObject = colliders[0].gameObject;

        //     if (targetObject.CompareTag("carriable"))
        //     {
        //         heldObject = targetObject;

        //         Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        //         rb.useGravity = false;
        //         rb.isKinematic = true;

        //         heldObject.transform.SetParent(holdPoint);
        //         heldObject.transform.localPosition = Vector3.zero;
        //         heldObject.transform.localRotation = Quaternion.identity;
        //     }
        // }

        Collider[] colliders = Physics.OverlapSphere(pickupPoint.position, pickupRadius);

        if (colliders.Length > 0)
        {
            GameObject targetObject = null;

            foreach (Collider col in colliders)
            {
                Debug.Log("Detected object for pickup: " + col.gameObject.name);
                if (col.gameObject.CompareTag("carriable"))
                {
                    targetObject = col.gameObject;
                    break; // Stop at the first carriable item
                }
            }

            if (targetObject != null)
            {
                heldObject = targetObject;

                Rigidbody rb = heldObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }

                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;

                Debug.Log("Picked up: " + heldObject.name);
            }
            else
            {
                Debug.Log("No carriable object detected.");
            }
        }
        else
        {
            Debug.Log("No objects found in pickup radius.");
        }
    }

    private void DropObject() {
        if (heldObject != null)
        {
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = false;

            heldObject.transform.SetParent(null);
            rb.AddForce(transform.forward * 2f, ForceMode.Impulse);

            heldObject = null;
        }
    }

    private async Task TryInteraction(GameObject heldobject = null)
    {
        Debug.Log("--- STARTING INTERACTION CHECK ---");
        Debug.Log($"Held object: {(heldObject ? heldObject.name : "null")}");
        
        Collider[] colliders = Physics.OverlapSphere(pickupPoint.position, pickupRadius);
        Debug.Log($"Found {colliders.Length} colliders in radius");

        foreach (Collider col in colliders)
        {
            Debug.Log($"Checking: {col.gameObject.name} (Layer: {LayerMask.LayerToName(col.gameObject.layer)})");
            
            Computer computer = col.GetComponent<Computer>();
            if (computer != null)
            {
                Debug.Log($"Found Computer component! State: {computer.currentState}, Type: {computer.issueType}");
                
                if (heldObject != null)
                {
                    Debug.Log($"Held item: {heldObject.name}");
                    bool correctCombo = (computer.issueType == 0 && heldObject.name.Contains("Tools")) || 
                                    (computer.issueType == 1 && heldObject.name.Contains("Battery"));
                    
                    Debug.Log($"Combo check: {correctCombo}");
                    
                    if (correctCombo)
                    {
                        Debug.Log("Fixing computer...");
                        canMove = false;
                        await Task.Delay(2000);
                        
                        computer.FixComputer();
                        // Destroy(heldObject);
                        // heldObject = null;
                        DropObject();
                        canMove = true;
                        return;
                    }
                }
                else
                {
                    Debug.Log("No item held (need tool/battery)");
                }
            }
        }
        Debug.Log("--- END INTERACTION CHECK ---");
    }
}