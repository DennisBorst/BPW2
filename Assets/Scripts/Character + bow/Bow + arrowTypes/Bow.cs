using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{
    [Header("Bow")]
    public Transform bowModel;
    private Vector3 bowOriginalPos, bowOriginalRot;
    public Transform bowZoomTransform;

    [Space]

    [Header("Force")]
    [SerializeField] private float forceDuration;
    public float maxForceDuration;

    [Space]

    [Header("Arrow")]
    public GameObject arrowPrefab;
    public Transform arrowSpawnOrigin;
    public Transform arrowModel;
    private Vector3 arrowOriginalPos;

    [Space]

    [Header("Parameters")]
    public Vector3 arrowImpulse;
    public float shootWait;
    public bool canShoot;
    public bool shootRest = false;
    public bool isAiming = false;

    [Space]

    public float zoomInDuration;
    public float zoomOutDuration;

    private float camOriginalFov;
    public float camZoomFov;
    private Vector3 camOriginalPos;
    public Vector3 camZoomOffset;

    [Space]

    [Header("Canvas")]
    public RectTransform reticle;
    public CanvasGroup reticleCanvas;
    public Image centerCircle;
    private Vector2 originalImage;

    private void Start()
    {
        camOriginalPos = Camera.main.transform.localPosition;
        camOriginalFov = Camera.main.fieldOfView;
        bowOriginalPos = bowModel.transform.localPosition;
        bowOriginalRot = bowModel.transform.localEulerAngles;
        arrowOriginalPos = arrowModel.transform.localPosition;

        originalImage = reticle.sizeDelta;
        ShowReticle(false, 0);
    }

    // Update is called once per frame
    void Update()
    {
        InputMouse();
    }

    public void InputMouse()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (!shootRest && !isAiming)
            {
                canShoot = false;
                isAiming = true;

                StopCoroutine(PrepareSequence());
                StartCoroutine(PrepareSequence());

                ShowReticle(true, zoomInDuration / 2);

                Transform bow = bowZoomTransform;
                arrowModel.transform.localPosition = arrowOriginalPos;
                arrowModel.DOLocalMoveZ(arrowModel.transform.localPosition.z - .10f, zoomInDuration * 2f);
                CameraZoom(camZoomFov, camOriginalPos + camZoomOffset, bow.localPosition, bow.localEulerAngles, zoomInDuration, true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (!shootRest && isAiming)
            {
                StopAllCoroutines();
                float percentage = forceDuration / maxForceDuration;
                StartCoroutine(ShootSequence(percentage));
            }
        }
    }

    public void CameraZoom(float fov, Vector3 camPos, Vector3 bowPos, Vector3 bowRot, float duration, bool zoom)
    {
        Camera.main.transform.DOComplete();
        Camera.main.DOFieldOfView(fov, duration);
        Camera.main.transform.DOLocalMove(camPos, duration);
        bowModel.transform.DOLocalRotate(bowRot, duration).SetEase(Ease.OutBack);
        bowModel.transform.DOLocalMove(bowPos, duration).OnComplete(() => ShowArrow(zoom));
    }

    public void ShowArrow(bool state)
    {
        bowModel.GetChild(0).gameObject.SetActive(state);
    }

    public IEnumerator PrepareSequence()
    {
        canShoot = true;

        while (forceDuration <= maxForceDuration)
        {
            forceDuration += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        forceDuration = maxForceDuration;
    }

    public IEnumerator ShootSequence(float percentage)
    {
        yield return new WaitUntil(() => canShoot == true);

        shootRest = true;

        isAiming = false;
        canShoot = false;

        ShowReticle(false, zoomOutDuration);

        CameraZoom(camOriginalFov, camOriginalPos, bowOriginalPos, bowOriginalRot, zoomOutDuration, true);
        arrowModel.transform.localPosition = arrowOriginalPos;
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnOrigin.position, bowModel.rotation);
        arrow.GetComponent<Rigidbody>().AddForce((transform.forward * arrowImpulse.z + transform.up * arrowImpulse.y) * percentage, ForceMode.Impulse);
        ShowArrow(false);
        forceDuration = 0;

        yield return new WaitForSeconds(shootWait);
        shootRest = false;
    }

    public void ShowReticle(bool state, float duration)
    {
        float num = state ? 1 : 0;
        reticleCanvas.DOFade(num, duration);
        Vector2 size = state ? originalImage / 2 : originalImage;
        reticle.DOComplete();
        reticle.DOSizeDelta(size, duration * 4);

        if (state)
        {
            centerCircle.DOFade(1, .5f).SetDelay(duration * 3);
        }
        else
        {
            centerCircle.DOFade(0, duration);
        }
    }

    public virtual void PrepareParticles()
    {

    }

    public virtual void ShootParticles()
    {

    }
}
