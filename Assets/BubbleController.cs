using System.Collections;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEditorInternal;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    enum BubbleState
    {
        Idle,
        Blowing
    }
    BubbleState state;
    [SerializeField] GameObject bubble;
    GameObject instantiatedBubble;
    [SerializeField] bool isBubbleThere = true;
    [SerializeField] float bubbleSpeed = 0.1f;
    [SerializeField] int blowChance = 80;
    private Coroutine explodeCoroutine;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int PlayerControlled;
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
            if (isBubbleThere && state == BubbleState.Idle)
            {
                if (blowChance < 100 && blowChance > 25)
                    blowChance -= 5;
            }
            else
            {
                if(!isBubbleThere){
                instantiatedBubble = Instantiate(bubble, spawnPoint.position, Quaternion.identity, transform);
                isBubbleThere = true;
                } else if (isBubbleThere && state == BubbleState.Blowing)
                {
                    ReleaseBubble();
                }
            }
        }
    }

    private void ReleaseBubble()
    {
        //make the bubble floating in the air like a bubble
        instantiatedBubble.GetComponent<Rigidbody>().useGravity = true;
        instantiatedBubble.GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
        GameManager.instance.AddScore(PlayerControlled, Mathf.RoundToInt(instantiatedBubble.transform.localScale.x * 10));
        isBubbleThere = false;
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
        state = BubbleState.Blowing;
        instantiatedBubble.transform.localScale += new Vector3(bubbleSpeed, bubbleSpeed, bubbleSpeed);
    }

    private void Explode()
    {
        state = BubbleState.Idle;
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