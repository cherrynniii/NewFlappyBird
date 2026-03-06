using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    // 메인 카메라
    private Camera cam;

    // 카메라 원래 위치
    private Vector3 cameraOriginPos;

    // 카메라 흔들기 강도 (기본값)
    [SerializeField, Range(0.01f, 0.1f)]
    private float originShakeRange = 0.05f;

    // 카메라 흔들기 시간 (기본값)
    [SerializeField, Range(0.1f, 1f)]
    private float originShakeDuration = 0.5f;

    // 흔드는 도중 몇 프레임마다 원위치로 잠깐 되돌릴지
    [SerializeField]
    private int originShakeInitSpacing = 5;

    // 실행 중인 코루틴 핸들
    private Coroutine startCameraShakeCoroutine;
    private Coroutine endCameraShakeCoroutine;

    void Start()
    {
        cam = Camera.main;

        // 메인 카메라가 없으면 방어
        if (cam == null)
        {
            Debug.LogError("CameraManager: Camera.main 이 null 입니다. 메인 카메라에 'MainCamera' 태그가 있는지 확인하세요.");
            enabled = false;
            return;
        }

        cameraOriginPos = cam.transform.localPosition;
    }

    // 카메라 흔들기 시작
    // 매개변수를 안 넣으면 인스펙터 기본값으로 동작
    public void ShakeCamera(float shakeRange = 0f, float duration = 0f)
    {
        Debug.Log("카메라 흔들기 호출");

        StopPrevCameraShakeCoroutines();

        float range = (shakeRange == 0f) ? originShakeRange : shakeRange;
        float time = (duration == 0f) ? originShakeDuration : duration;

        startCameraShakeCoroutine = StartCoroutine(StartShake(range));
        endCameraShakeCoroutine = StartCoroutine(StopShake(time));
    }

    // 흔들기 코루틴
    private IEnumerator StartShake(float shakeRange)
    {
        int shakeInitSpacing = originShakeInitSpacing;

        while (true)
        {
            shakeInitSpacing--;

            float cameraPosX = Random.value * shakeRange * 2f - shakeRange;
            float cameraPosY = Random.value * shakeRange * 2f - shakeRange;

            Vector3 cameraPos = cam.transform.position;
            cameraPos.x += cameraPosX;
            cameraPos.y += cameraPosY;
            cam.transform.position = cameraPos;

            if (shakeInitSpacing < 0)
            {
                shakeInitSpacing = originShakeInitSpacing;
                cam.transform.localPosition = cameraOriginPos;
            }

            yield return null;
        }
    }

    // duration 후 흔들기 종료
    private IEnumerator StopShake(float duration)
    {
        yield return new WaitForSeconds(duration);

        cam.transform.localPosition = cameraOriginPos;

        StopPrevCameraShakeCoroutines();
    }

    // 이전 흔들기 코루틴 중지
    private void StopPrevCameraShakeCoroutines()
    {
        if (startCameraShakeCoroutine != null)
        {
            StopCoroutine(startCameraShakeCoroutine);
            startCameraShakeCoroutine = null;
        }

        if (endCameraShakeCoroutine != null)
        {
            StopCoroutine(endCameraShakeCoroutine);
            endCameraShakeCoroutine = null;
        }
    }
}