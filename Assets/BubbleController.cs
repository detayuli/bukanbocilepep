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
    [SerializeField] float bubbleSpeed = 0.01f;
    [SerializeField] int blowChance = 80;
    private Coroutine explodeCoroutine;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int PlayerControlled;
    [SerializeField] KeyCode blow, kocok;
    [SerializeField] Animator animator;
    [SerializeField] GameObject body;
    [SerializeField] Material idle, blowing, kocoking;

    AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isBubbleThere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(blow))
        {
            if (explodeCoroutine == null)
            {
                explodeCoroutine = StartCoroutine(ExplodeRepeatedly());
            }
        }
        else if (Input.GetKeyUp(blow))
        {
            body.GetComponent<Renderer>().material = idle;
            if (explodeCoroutine != null)
            {
                StopCoroutine(explodeCoroutine);
                explodeCoroutine = null;
            }
        }

        if (Input.GetKey(blow))
        {
            Blow();
        }

        if (Input.GetKeyDown(kocok))
        {
            animator.Play("kocok");
            animator.SetInteger("Post", 0);
            body.GetComponent<Renderer>().material = kocoking;
            if (isBubbleThere && state == BubbleState.Idle)
            {
                if (blowChance < 100 && blowChance > 25)
                    blowChance -= 5;
            }
            else
            {
                if (!isBubbleThere)
                {
                    instantiatedBubble = Instantiate(bubble, spawnPoint.position, Quaternion.identity, transform);
                    isBubbleThere = true;
                }
                else if (isBubbleThere && state == BubbleState.Blowing && instantiatedBubble.transform.localScale.x > 1f)
                {
                    ReleaseBubble();
                }
            }
        }
    }

    private void ReleaseBubble()
    {
        //make the bubble floating in the air like a bubble
        instantiatedBubble.GetComponent<Rigidbody>().useGravity = false;
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
        animator.SetInteger("Post", 1);
        body.GetComponent<Renderer>().material = blowing;
        state = BubbleState.Blowing;
        instantiatedBubble.transform.localScale += new Vector3(bubbleSpeed, bubbleSpeed, bubbleSpeed) * Time.deltaTime;
    }

    private void Explode()
    {
        state = BubbleState.Idle;
        if (instantiatedBubble.transform.localScale.x < 1f)
            return;
        var chance = Random.Range(0, 100);
        if (chance < blowChance)
        {
            //destroy the instantiated bubble
            Destroy(instantiatedBubble);
            audioManager.PlaySFX(audioManager.meledak);
            isBubbleThere = false;
        }
        Debug.Log("Explode Invoked " + chance.ToString());
    }
}