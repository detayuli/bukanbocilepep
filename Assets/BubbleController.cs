using System.Collections;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    GameObject instantiatedBubble;
    [SerializeField] bool isBubbleThere = true;
    [SerializeField] float bubbleSpeed = 0.1f;
    [SerializeField] int blowChance = 80;
    private Coroutine explodeCoroutine;
    [SerializeField] Transform spawnPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBubbleThere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            Blow();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (explodeCoroutine == null)
            {
                explodeCoroutine = StartCoroutine(ExplodeRepeatedly());
            }
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            if (explodeCoroutine != null)
            {
                StopCoroutine(explodeCoroutine);
                explodeCoroutine = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isBubbleThere)
            {
                if (blowChance < 100 && blowChance > 25)
                    blowChance -= 5;
            }
            else
            {
                //Instantiate a new bubble as childern of thsi script gameobject
                instantiatedBubble = Instantiate(bubble, spawnPoint.position, Quaternion.identity, transform);
                isBubbleThere = true;
            }
        }
    }

    private IEnumerator ExplodeRepeatedly()
    {
        while (true)
        {
            Explode();
            yield return new WaitForSeconds(1f);
        }
    }

    private void Blow()
    {
        instantiatedBubble.transform.localScale += new Vector3(bubbleSpeed, bubbleSpeed, bubbleSpeed);
    }

    private void Explode()
    {
        var chance = Random.Range(0, 100);
        if (chance < blowChance)
        {
            //destroy the instantiated bubble
            Destroy(instantiatedBubble);
            isBubbleThere = false;
        }
        Debug.Log("Explode Invoked " + chance.ToString());
    }
}