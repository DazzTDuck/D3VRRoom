using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;


public class SnapSystem : MonoBehaviour
{
    public Transform snapPlace;
    public Transform ObjectToSnap;
    [SerializeField] MeshRenderer[] meshRenderers;
    [SerializeField] Collider[] Colliders;
    public float minDistanceToSnap = 0.05f;
    [Space]
    public UnityEvent OnSnapped;

    [Header("For Ballerina Trophy")]
    [SerializeField] bool ballerina;
    [SerializeField] MeshRenderer baseMeshRenderer;
    [SerializeField] MeshRenderer bossMeshRenderer;
    [SerializeField] SkinnedMeshRenderer SkinnedMeshRenderer;

    Hand rightHand;
    Hand leftHand;
    
    bool hasSnapped = false;
    bool isHoldingObjectRight = false;
    bool isHoldingObjectLeft = false;

    private void Awake()
    {
        //to get the hands
        foreach (var hand in FindObjectsOfType<Hand>())
        {
            if (hand.handType == Valve.VR.SteamVR_Input_Sources.RightHand)
                rightHand = hand;
            else
                leftHand = hand;
        }
    }

    void Start()
    {
        SnapPlaceShow(false, false);
    }

    public void SnapPlaceShow(bool canShow, bool hasCollision)
    {
        if (ballerina)
        {
            baseMeshRenderer.enabled = canShow;
            bossMeshRenderer.enabled = canShow;
            SkinnedMeshRenderer.enabled = canShow;
            foreach (var coll in Colliders)
            {
                coll.enabled = hasCollision;
            }
            return;
        }

        if(meshRenderers.Length > 0)
        {
            foreach (var mesh in meshRenderers)
            {
                mesh.enabled = canShow;
            }
        }

        foreach (var coll in Colliders)
        {
            coll.enabled = hasCollision;
        }
    }

    void ShowSnapPosistion()
    {
        if (!hasSnapped)
        {
            if (isHoldingObjectRight || isHoldingObjectLeft)
            {
                SnapPlaceShow(true, false);
            }
            else
            {
                SnapPlaceShow(false, false);
            }
        }      
    }  
    
    void CheckHandsForObject()
    {
        if (!hasSnapped && rightHand && leftHand)
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

               // SnapPlaceShow(true, true);

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
