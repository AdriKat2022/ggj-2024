using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Time")]
    [SerializeField]
    private bool bypassTimescale;
    [SerializeField]
    private bool activateOnAwake = true;
    [SerializeField]
    private bool isActive;

    [Header("Wave Rotation")]
    [SerializeField]
    private bool enableRotation;
    [SerializeField]
    private float rotationDepth, rotationSpeed;

    private float _time;


    [Header("Grow on hover")]
    [SerializeField]
    private bool enableHoverScale;
    [SerializeField]
    private float hoverDepth, hoverSpeed;

    private Vector3 scaleOnHover;

    [Header("Click animation")]
    [SerializeField]
    private bool enableClickAnimation;
    [SerializeField]
    private float clickDepth, clickSpeed;

    [SerializeField]
    private bool replaceClickByKey;
    [SerializeField]
    private KeyCode overridingKey;

    private Vector3 scaleOnClick;


    [Header("Sound")]
    [SerializeField]
    private bool playSoundOnHover;
    [SerializeField]
    private AudioClip soundOnHover;
    [SerializeField]
    private bool playSoundOnClick;
    [SerializeField]
    private AudioClip soundOnClick;
    [Space]
    [SerializeField]
    private bool onUpClickInstead;
    [SerializeField]
    private bool useFunctionInstead;



    private bool pointerHover;
    private bool isMouseOverFirstFrame;
    private bool firstPress;

    private Vector3 baseScale;


#if UNITY_EDITOR

    private void OnValidate()
    {
        scaleOnHover = Vector3.one * (1 + hoverDepth);
        scaleOnClick = Vector3.one * (1 - clickDepth);
    }

#endif


    private void OnEnable()
    {
        baseScale = transform.localScale;
        _time = 0;
        firstPress = false;

        if (bypassTimescale && !isActive)
        {
            isActive = true;
            StartCoroutine(BypassTimescale());
        }
    }

    private void OnDisable()
    {
        transform.localScale = baseScale;
        pointerHover = false;
        isActive = false;
    }

    private void Start()
    {
        baseScale = transform.localScale;

        isActive = activateOnAwake;


        scaleOnHover = baseScale * (1 + hoverDepth);
        scaleOnClick = baseScale * (1 - clickDepth);
        _time = 0;

        pointerHover = false;
        isMouseOverFirstFrame = false;
    }


    private IEnumerator BypassTimescale()
    {
        while (isActive)
        {
            if (enableRotation)
                HandleWaveMotion();

            isMouseOverFirstFrame &= pointerHover;


            if (enableHoverScale)
                HandleGrowth(Time.unscaledDeltaTime);

            if (playSoundOnClick && pointerHover && Input.GetMouseButtonUp(0) && !useFunctionInstead && (Input.GetMouseButtonUp(0) && onUpClickInstead || Input.GetMouseButtonDown(0) && !onUpClickInstead))
                PlayButtonSound();
            if (playSoundOnHover)
            {
                if (pointerHover && !isMouseOverFirstFrame)
                {
                    SoundManager.Instance.PlaySound(soundOnHover);
                    isMouseOverFirstFrame = true;
                }
            }
            _time += Time.unscaledDeltaTime;

            yield return null;
        }
    }

    public void PlayButtonSound() => SoundManager.Instance.PlaySound(soundOnClick);

    private void Update()
    {
        if (bypassTimescale && isActive)
            return;

        
        HandleWaveMotion();

        isMouseOverFirstFrame &= pointerHover;

        HandleGrowth(Time.deltaTime);

        if (playSoundOnClick && pointerHover && !useFunctionInstead && (Input.GetMouseButtonUp(0) && onUpClickInstead || Input.GetMouseButtonDown(0) && !onUpClickInstead))
            PlayButtonSound();

        if (playSoundOnHover)
        {
            if (pointerHover && !isMouseOverFirstFrame)
            {
                SoundManager.Instance.PlaySound(soundOnHover);
                isMouseOverFirstFrame = true;
            }
        }

        if (replaceClickByKey)
        {
            pointerHover = Input.GetKey(overridingKey);
        }

        _time += Time.deltaTime;
    }

    private void HandleGrowth(float dTime)
    {
        if (Input.GetKeyDown(overridingKey))
            firstPress = true;

        if ((Input.GetMouseButton(0) && !replaceClickByKey) || (replaceClickByKey && Input.GetKey(overridingKey) && firstPress))
            transform.localScale = Vector3.Lerp(transform.localScale, enableClickAnimation ? scaleOnClick : baseScale, dTime * clickSpeed);
        else if(pointerHover)
            transform.localScale = Vector3.Lerp(transform.localScale, enableHoverScale ? scaleOnHover : baseScale, dTime * hoverSpeed);
        else
            transform.localScale = Vector3.Lerp(transform.localScale, baseScale, dTime * hoverSpeed);

    }

    private void HandleWaveMotion()
    {
        if (!enableRotation)
            return;

        float rotationAngle = Mathf.Cos(_time * rotationSpeed) * rotationDepth;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationAngle);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (replaceClickByKey)
            return;

        pointerHover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (replaceClickByKey)
            return;

        pointerHover = false;
    }
}
