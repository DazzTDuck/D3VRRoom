using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class FuzeBoxPuzzle : MonoBehaviour
{
    public GameObject replacementFuze;
    public GameObject newFuze;
    public float minDistanceToPlaceFuze = 0.05f;

    public UnityEvent onFuzeReplaced;

    //private
    float distance;
    bool placed;
    bool brokenFuzeRemoved;
    bool newFuzeCloseEnough;

    public void BrokenFuzeRemoved()
    {
        brokenFuzeRemoved = true;
        Debug.Log("Broken Fuze Removed");
    }
    public void ReferenceNewFuze(GameObject fuze)
    {
        newFuze = fuze;
    }

    public void CheckDistanceToPlaceNewFuze()
    {
        if (brokenFuzeRemoved && !placed)
        {
            if (replacementFuze && newFuze)
                distance = Vector3.Distance(replacementFuze.transform.position, newFuze.transform.position);

            if (distance < minDistanceToPlaceFuze)
                newFuzeCloseEnough = true;
        }      
    }
    public void PlaceNewFuze()
    {
        if (newFuzeCloseEnough && !placed)
        {
            placed = true;
            replacementFuze.SetActive(true);
            Destroy(newFuze, 0.2f);

            onFuzeReplaced.Invoke();

            Debug.Log("Fuze Replaced");
        }
    }
    public void HandHoverUpdate(Hand hand)
    {
        if (placed)
        {
            hand.DetachObject(newFuze);
            Debug.Log("Fuze Detached from hand");
        }
    }

    private void Update()
    {
        CheckDistanceToPlaceNewFuze();
        PlaceNewFuze();
    }
}
