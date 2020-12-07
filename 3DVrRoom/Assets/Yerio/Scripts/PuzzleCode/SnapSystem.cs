using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;


public class SnapSystem : MonoBehaviour
{
    public Transform snapPlace;
    public Transform ObjectToSnap;
    public float minDistanceToSnap = 0.05f;
    [SerializeField] bool showSnapPlaceWhenHolding = false;
    public Material snapMaterial;
    [Space]
    public UnityEvent OnSnapped;

    [Header("For Ballerina Trophy")]
    [SerializeField] bool ballerina;
    [SerializeField] MeshRenderer baseMeshRenderer;
    [SerializeField] MeshRenderer bossMeshRenderer;
    [SerializeField] SkinnedMeshRenderer SkinnedMeshRenderer;
    [SerializeField] Material baseMaterial;
    [SerializeField] Material bossMaterial;

    Material originalMaterial;
    MeshRenderer meshRenderer;
    Collider Collider;

    Hand rightHand;
    Hand leftHand;
    
    bool hasSnapped = false;
    bool isHoldingObjectRight = false;
    bool isHoldingObjectLeft = false;

    private void Awake()
    {
        Collider = GetComponent<Collider>();

        if (!ballerina)
        {
            meshRenderer = GetComponent<MeshRenderer>();
            originalMaterial = meshRenderer.material;
        }

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
            Collider.enabled = hasCollision;
            return;
        }

        if(meshRenderer)
        meshRenderer.enabled = canShow;

        Collider.enabled = hasCollision;   
    }
    void ChangeMaterial(Material newMaterial)
    {
        if (ballerina)
        {
            baseMeshRenderer.material = newMaterial;
            bossMeshRenderer.material = newMaterial;
            SkinnedMeshRenderer.material = newMaterial;
            return;
        }

        meshRenderer.material = newMaterial;
    }

    void ChangeBackBallerina()
    {
        baseMeshRenderer.material = baseMaterial;
        bossMeshRenderer.material = bossMaterial;
        SkinnedMeshRenderer.material = bossMaterial;
    }

    void ShowSnapPosistion()
    {
        if (!hasSnapped && showSnapPlaceWhenHolding)
        {
            if (isHoldingObjectRight || isHoldingObjectLeft)
            {
                SnapPlaceShow(true, false);
                ChangeMaterial(snapMaterial);
            }
            else
            {
                SnapPlaceShow(false, false);

                if (!ballerina)
                    ChangeMaterial(originalMaterial);
                else
                    ChangeBackBallerina();
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

                if (!ballerina)
                    ChangeMaterial(originalMaterial);
                else
                    ChangeBackBallerina();

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
