using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueprintPlace : MonoBehaviour
{

    RaycastHit hit;
    Vector3 movePoint;
    public GameObject prefab;
    List<Transform> children = new();

    private bool isPlaceable;
    private bool isOverlap = false;

    BuyableObject buying;

    public List<string> tagName = new List<string>();


    void Start()
    {
        tagName.Add("place_obj");

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f))
        {
            transform.position = hit.point;
        }

        foreach (Transform child in transform)
        {
            children.Add(child);
            if (child.childCount > 0)
            {
                children.Add(child.GetChild(0));
            }
        }

        buying = gameObject.GetComponent<BuyableObject>();
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, 1, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider != null && (!tagName.Contains(hit.collider.tag) || isOverlap || !buying.CanBuy()))
            {
                isPlaceable = false;
                foreach (Transform child in children)
                {
                    child.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 0.35f);
                }
            }
            else
            {
                isPlaceable = true;
                foreach (Transform child in children)
                {
                    child.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.35f);
                }
            }

            transform.position = hit.point;
        }

        if (Input.GetMouseButtonDown(0) && isPlaceable)
        {
            if (!buying) buying = gameObject.GetComponent<BuyableObject>();
            if (buying.BuyAsset())
            {
                Instantiate(prefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.transform.tag == "Turret" && other.isTrigger == false)
        {
            isOverlap = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.transform.tag == "Turret" && other.isTrigger == false)
        {
            isOverlap = false;
        }
    }
}
