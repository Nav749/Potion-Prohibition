using System.Collections;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public bool isSpeaking = false;
    public bool isOrdering = false;

    [SerializeField] private Animator Animator;

    public bool canStart = true;

    [SerializeField] private AudioClip sfx;
    private AudioSource sfxSource;

    [Header("Babbles")]
    [SerializeField] private AudioClip[] dialogueBabbleClips;
    [SerializeField] private AudioSource audioSourceBabble;
    [SerializeField] private bool stopAudioSource;
    [Range(-3, 3)]
    [SerializeField] private float minPitch = 0.5f;
    [Range(-3, 3)]
    [SerializeField] private float maxPitch = 3f;
    [SerializeField] private int frequency = 2;
    private int counter;

    public TextMeshProUGUI textComponet;
    public string[] linesIntro1;
    [HideInInspector] public string[] linesIntro;
    public string[] linesIntro2;
    public string[] linesIntro3;
    [SerializeField] string[] linesPasstime;
    public string[] linesRight;
    public string[] linesWrong;
    [SerializeField] string[] linesEnd;
    [SerializeField] float textSpeed;
    [SerializeField] SpriteRenderer potion;
    [SerializeField] TMP_Text text;
    private int textIndex;
    [HideInInspector] public string[] lines;
    private Sprite potionImage;
    private bool commmenedOrder = false;
    [SerializeField] Timesmet history;

    private SpriteRenderer characterRenderer;
    [SerializeField] Sprite Idle;
    [SerializeField] Sprite Correct;
    [SerializeField] Sprite Wrong;
    [SerializeField] Sprite Back;
    [SerializeField] Sprite Revealed;
    [SerializeField] Sprite revealedRight;
    [SerializeField] Sprite revealedWrong;
    public bool isChildren;
    private bool drank = false;
    private bool hasBegun = true;

    private void Start()
    {
        sfxSource = GetComponent<AudioSource>();
        sfxSource.clip = sfx;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
        lines = IntToLine();
        text.text = string.Empty;
        linesRight = linesRight.Concat(linesEnd).ToArray();
        linesWrong = linesWrong.Concat(linesEnd).ToArray();
        characterRenderer = this.GetComponent<SpriteRenderer>();
        characterRenderer.sprite = Idle;
    }

    public void ResetSprite()
    {
        if (history.GetInt() > 0)
        characterRenderer.sprite = Idle;
    }

    private void Update()
    {
        Vector3 target = (GameManager.Instance.PlayerGO.transform.position - transform.position).normalized;
        Quaternion lookingRotation = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookingRotation, 85);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        GameManager.Instance.currentlines = lineToInt();

        if (GameManager.Instance.checkDone)
        {
            lines = GameManager.Instance.correctOrder ? linesRight : linesWrong;
            characterRenderer.sprite = GameManager.Instance.correctOrder ? Correct : Wrong;
            if (isChildren && !drank && history.GetInt() < 2)
            {
                characterRenderer.sprite = Back;
            }
            if (!isSpeaking && hasBegun)
            {
                StartDialogue();
                hasBegun = false;
            }
        }

        if (!commmenedOrder) CommenceOrder();

        if (lines == linesPasstime)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
            sfxSource.Play();
        }
        else gameObject.transform.GetChild(1).gameObject.SetActive(false);

        if (isSpeaking)
        {
            Animator.SetBool("IsTalking", true);
            if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.speakable)
            {
                if (textComponet.text == lines[textIndex])
                {
                    if(isChildren && GameManager.Instance.checkDone)
                    {
                        drank = true;
                        characterRenderer.sprite = GameManager.Instance.correctOrder ? Correct : Wrong;
                    }
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    textComponet.text = lines[textIndex];
                }
            }
        }
        else
        {
            Animator.SetBool("IsTalking", false);
        }

        GameManager.Instance.orderTime = lines == linesPasstime ? true : false;
    }

    public void StartDialogue()
    {
        if(lines != linesRight || lines != linesWrong) drank = false;
        if(isChildren && history.GetInt() > 1)
        {
            Correct = revealedRight;
            Wrong = revealedWrong;
        }
        textComponet.text = string.Empty;
        textIndex = 0;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        counter = 0;
        canStart = false;
        foreach (char c in lines[textIndex].ToCharArray())
        {
            textComponet.text += c;
            counter++;
            if (stopAudioSource)
            {
                audioSourceBabble.Stop();
            }
            if (counter == frequency)
            {
                int randomIndex = Random.Range(0, dialogueBabbleClips.Length);
                AudioClip soundClip = dialogueBabbleClips[randomIndex];
                audioSourceBabble.pitch = Random.Range(minPitch, maxPitch);
                audioSourceBabble.PlayOneShot(soundClip);
                counter = 0;
            }
            yield return new WaitForSeconds(textSpeed);
        }
        canStart = true;
    }

    void NextLine()
    {
        if (textIndex < lines.Length - 1)
        {
            if (isChildren && history.GetInt() > 1 && textIndex > 2) characterRenderer.sprite = Revealed;
            textIndex++;
            textComponet.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            canStart = true;
            isSpeaking = false;
            GameManager.Instance.PlayerGO.GetComponent<playerMovement>().setMoveLock(false);
            GameManager.Instance.orderTime = true;
            if (lines == linesIntro)
            {
                lines = linesPasstime;
            }
            else if (lines != linesIntro1 && lines != linesPasstime)
            {
                if (history.GetInt() != 2)
                {
                    history.SetInt(history.GetInt() + 1);
                }
                GameManager.Instance.UpdateQuota();
                Debug.Log(GameManager.Instance.currentOrderQuota + "/ " + GameManager.Instance.orderQuota);
                this.transform.GetComponentInParent<CustomerPool>().ActivateCustomer();
                hasBegun = true;
                commmenedOrder = false;
            }
        }
    }

    void CommenceOrder()
    {
        potionImage = GameManager.Instance.currentOrder.getImage();
        potion.sprite = potionImage;
        potion.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        text.text = GameManager.Instance.currentOrder.BottomText();
        commmenedOrder = true;
    }

    int lineToInt()
    {
        if (lines == linesPasstime) return 1;
        else return 0;
    }

    string[] IntToLine()
    {
        if (GameManager.Instance.currentlines == 1) return linesPasstime;
        else
        {
            return LinesIntro();
        }
    }

    public string[] LinesIntro()
    {
        if (history.GetInt() == 0) linesIntro = linesIntro1;
        else if (history.GetInt() == 1) linesIntro = linesIntro2;
        else linesIntro = linesIntro3;

        return linesIntro;
    }

}
