using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;
using System;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 200f;
    public float mouseSensitivity = 3f;

    public AudioSource startComputerFix;
    public AudioSource completeComputerFix;
    public AudioSource wrongItem;

    public AudioSource fixingGenerator;

    public List<Color> colors = new List<Color>();

    [SerializeField] private float pickupForce = 150f;
    [SerializeField] private Transform pickupPoint;
    [SerializeField] private float pickupRadius = 0.1f;
    [SerializeField] private LayerMask carriableLayer;

    private GameObject heldObject;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private GameObject batteryPoint;
    [SerializeField] private GameObject toolsPoint;
    [SerializeField] private GameObject spawnedPrefab;
    private GameObject instantiatedPrefab;

    public GameObject fixedpc;
    public GameObject cannon;
    public GameObject bullet;

    public bool canMove = true;

    [SerializeField] private AudioListener audioListener;
    [SerializeField] private Camera playerCamera;

    private float rotationX = 0f;

    // LIGHTING REFERENCE
    [SerializeField] private LightingMan lightManager;  // Reference to the lighting script

    private void Start()
    {
        //need to manually set light manager because unity dumb and stupid and wont let me drag and drop it for some reason
        //i hate unity man, this was the only fix that worked. I hate this class, it makes me mad at how mismanaged it is
        //if anyone reads this I hated cs 426 at UIC and I hope they take the student feedback and improve the class
        if (lightManager == null)
        {
            lightManager = FindObjectOfType<LightingMan>();
            if (lightManager == null) 
                Debug.LogError("Could not find Lighting Manager component in scene!");
        }
        carriableLayer = LayerMask.GetMask("Carriable");
    }

    private void Update()
    {
        if (!IsOwner) return;

        Vector3 moveDirection = Vector3.zero;

        if (canMove)
        {
            if (Input.GetKey(KeyCode.W)) moveDirection.z = +1f;
            if (Input.GetKey(KeyCode.S)) moveDirection.z = -1f;
            if (Input.GetKey(KeyCode.A)) moveDirection.x = -1f;
            if (Input.GetKey(KeyCode.D)) moveDirection.x = +1f;

            Vector3 worldMoveDirection = transform.TransformDirection(moveDirection.normalized);
            transform.position += worldMoveDirection * speed * Time.deltaTime;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(0, mouseX, 0);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -30f, 60f);
        playerCamera.transform.localEulerAngles = new Vector3(rotationX, 0, 0);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null) TryPickupObject();
            else DropObject();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (heldObject == null) TryInteraction();
            else TryInteraction(heldObject);
        }
    }

    public override void OnNetworkSpawn()
    {
        GetComponent<MeshRenderer>().material.color = colors[(int)OwnerClientId];

        if (!IsOwner) return;

        audioListener.enabled = true;
        playerCamera.enabled = true;
    }

    [ServerRpc]
    private void BulletSpawningServerRpc(Vector3 position, Quaternion rotation)
    {
        BulletSpawningClientRpc(position, rotation);
    }

    [ClientRpc]
    private void BulletSpawningClientRpc(Vector3 position, Quaternion rotation)
    {
        GameObject newBullet = Instantiate(bullet, position, rotation);
        newBullet.GetComponent<Rigidbody>().linearVelocity += Vector3.up * 2;
        newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1500);
    }

    private void TryPickupObject()
    {
        Collider[] colliders = Physics.OverlapSphere(pickupPoint.position, pickupRadius);

        if (colliders.Length > 0)
        {
            GameObject targetObject = null;

            foreach (Collider col in colliders)
            {
                if (col.gameObject.CompareTag("carriable"))
                {
                    targetObject = col.gameObject;
                    break;
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
                    heldObject.GetComponent<Collider>().enabled = false;
                }

                heldObject.transform.SetParent(holdPoint);
                heldObject.transform.localPosition = Vector3.zero;
                heldObject.transform.localRotation = Quaternion.identity;
            }
        }
    }

    private void DropObject()
    {   
        if (heldObject == null) {
            return;
        }

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        heldObject.GetComponent<Collider>().enabled = true;

        heldObject.transform.SetParent(null);
        rb.AddForce(transform.forward * 2f, ForceMode.Impulse);

        // if (fixedDrop && respwanPoint != null)
        // {
        //     heldObject.transform.position = respawnPoint.position;
        //     heldObject.transform.rotation = respawnPoint.rotation;
        // }

        heldObject = null;

    }

    private async Task TryInteraction(GameObject heldobject = null)
    {
        Collider[] colliders = Physics.OverlapSphere(pickupPoint.position, pickupRadius);

        foreach (Collider col in colliders)
        {
            // Handle computer fixing
            Computer computer = col.GetComponent<Computer>();
            if (computer != null)
            {
                if (heldObject != null)
                {
                    bool correctCombo = (computer.issueType == 0 && heldObject.name.Contains("Tools")) ||
                                        (computer.issueType == 1 && heldObject.name.Contains("Battery"));

                    if (correctCombo && !lightManager.lightsAreOff)
                    {
                        startComputerFix.Play();
                        canMove = false;
                        await Task.Delay(2000);

                        computer.FixComputer();
                        startComputerFix.Stop();
                        completeComputerFix.Play();
                        GameObject tool = heldObject;
                        DropObject();
                        if (computer.issueType == 0) {
                            tool.transform.position = toolsPoint.transform.position;
                            tool.transform.rotation = toolsPoint.transform.rotation;
                        } else {
                            tool.transform.position = batteryPoint.transform.position;
                            tool.transform.rotation = batteryPoint.transform.rotation;
                        }

                        canMove = true;
                        return;
                    }
                    else
                    {
                        wrongItem.Play();
                    }
                }
            }

            // Handle light generator interaction
            if (col.gameObject.name.Contains("Light"))
            {
                Debug.Log("Interacted with Light Generator");

                if (lightManager != null && lightManager.directionalLight != null)
                {
                    if (lightManager.lightsAreOff)
                    {
                        canMove = false;
                        fixingGenerator.Play();
                        await Task.Delay(2000);
                        Debug.Log("Lights are OFF. Turning ON!");
                        lightManager.TurnLightsOn();
                        canMove = true;
                    }
                    else
                    {
                        Debug.Log("Lights are already ON.");
                    }
                }
            }
        }
    }
}
