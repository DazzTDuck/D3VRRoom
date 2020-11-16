using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;


public class SnapSystem : MonoBehaviour
{
    public Transform snapPlace;
    public Transform ObjectToSnap;
    public float minDistanceToSnap = 0.05f;
    public Material snapMaterial;
    [Space]
    public UnityEvent OnSnapped;

    Material originalMaterial;
    MeshRenderer meshRenderer;
    MeshCollider meshCollider;

    Hand rightHand;
    Hand leftHand;
    
    bool hasSnapped = false;
    bool isInRange = false;
    bool isHoldingObjectRight = false;
    bool isHoldingObjectLeft = false;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterial = meshRenderer.material;
        SnapPlaceShow(false, false);

        //to get the hands
        foreach (var hand in FindObjectsOfType<Hand>())
        {
            if (hand.handType == Valve.VR.SteamVR_Input_Sources.RightHand)
                rightHand = hand;
            else
                leftHand = hand;
        }
    }

    void SnapPlaceShow(bool canShow, bool hasCollision)
    {        
        meshRenderer.enabled = canShow;
        meshCollider.enabled = hasCollision;
    }
    void ChangeMaterial(Material newMaterial)
    {
        meshRenderer.material = newMaterial;
    }

    void ShowSnapPosistion()
    {
        if (!hasSnapped)
        {
            if (isHoldingObjectRight || isHoldingObjectLeft)
            {
                SnapPlaceShow(true, false);
                ChangeMaterial(snapMaterial);
            }
            else
            {
                SnapPlaceShow(false, false);
                ChangeMaterial(originalMaterial);
            }
        }      
    }  
    
    void CheckHandsForObject()
    {
        if (!hasSnapped)
        {
            if (rightHand.ObjectIsAttached(ObjectToSnap.gameObject))
            {
                isHoldingObjectRight = true;
            }
            else if (leftHand.ObjectIsAttached(ObjectToSnap.gameObject))
            {
                isHoldingObjectLeft = true;
            }
            else
            {
                isHoldingObjectRight = false;
                isHoldingObjectLeft = false;
            }
                
        }
        
    }
    void SnapObject()
    {
        if (isHoldingObjectRight && !hasSnapped || isHoldingObjectLeft && !hasSnapped)
        {
            if(Vector3.Distance(snapPlace.position, ObjectToSnap.position) < minDistanceToSnap)
            {
                hasSnapped = true;

                if (isHoldingObjectRight)
                    rightHand.DetachObject(ObjectToSnap.gameObject);
                else if (isHoldingObjectLeft)
                    leftHand.DetachObject(ObjectToSnap.gameObject);

                ChangeMaterial(originalMaterial);
                SnapPlaceShow(true, true);

                Destroy(ObjectToSnap.gameObject);
                ObjectToSnap = null;

                OnSnapped.Invoke();
            }
        }
    }

    private void Update()
    {
        CheckHandsForObject();
        ShowSnapPosistion();
        SnapObject();
    }
}
