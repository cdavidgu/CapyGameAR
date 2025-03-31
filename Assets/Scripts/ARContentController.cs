using UnityEngine;
using System.Collections;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARContentController : MonoBehaviour
{
    [SerializeField] GameObject CapybaraCharacter;
    Animator capybaraAnimator;
    Vector3 capyInitPos;
    public float spawnTime = 1.6f;
    public GameObject spawnOrigin;
    float deltaPos = 8f;
    float stepMove = 3.3f;


    void Awake()
    {
        capybaraAnimator = CapybaraCharacter.transform.GetChild(0).GetComponent<Animator>();
        capyInitPos = CapybaraCharacter.transform.localPosition;
    }


    public void InitARContent()
    {

        Debug.Log("InitARContent........................%%%%%%%%%%%%%%%%%%%%%");
        this.gameObject.SetActive(true);
        CapybaraCharacter.SetActive(true);
        CapybaraCharacter.transform.localPosition = capyInitPos;
        ObstaculePool.SharedInstance.ResetPool();
    }

    public void StartGame()
    {
        StartCoroutine(DelayedStart());
    }

    public void StopRunning()
    {
        capybaraAnimator.SetBool("gameRunning", false);
    }

    public void MoveLeft()
    {
        // Debug.Log("MoveRight");
        float actualXPos = CapybaraCharacter.transform.localPosition.x;
        float targetXPos = actualXPos + stepMove;
        targetXPos = Mathf.Min(targetXPos, stepMove);
        while (actualXPos <= targetXPos)
        {
            actualXPos = CapybaraCharacter.transform.localPosition.x + deltaPos * Time.deltaTime;
            CapybaraCharacter.transform.localPosition = new Vector3(actualXPos, CapybaraCharacter.transform.localPosition.y, CapybaraCharacter.transform.localPosition.z);
        }
    }

    public void MoveRight()
    {
        // Debug.Log("MoveLeft");
        float actualXPos = CapybaraCharacter.transform.localPosition.x;
        float targetXPos = actualXPos - stepMove;
        targetXPos = Mathf.Max(targetXPos, -stepMove);
        while (actualXPos >= targetXPos)
        {
            actualXPos = CapybaraCharacter.transform.localPosition.x - deltaPos * Time.deltaTime;
            CapybaraCharacter.transform.localPosition = new Vector3(actualXPos, CapybaraCharacter.transform.localPosition.y, CapybaraCharacter.transform.localPosition.z);
        }
    }

    IEnumerator SpawnTimer()
    {
        float maxTime = 3f;
        float totalTime = 0;
        while (totalTime < maxTime)
        {
            yield return null;
            totalTime += Time.deltaTime;
        }
        // Debug.Log("Spawning Started");
        StartCoroutine(SpawnObstacule());
    }

    IEnumerator SpawnObstacule()
    {
        while (AppController.SharedInstance.isRunning)
        {
            yield return new WaitForSeconds(spawnTime);
            // Debug.Log(" ObjectSpawned");
            GameObject obstaculeObj = ObstaculePool.SharedInstance.GetPooledObject();
            obstaculeObj.transform.SetParent(this.transform);
            obstaculeObj.transform.localPosition = Vector3.zero;

            if (obstaculeObj != null)
            {
                float randomNum = UnityEngine.Random.value;
                float xPosition;
                if (randomNum < 0.33f)
                {
                    xPosition = spawnOrigin.transform.localPosition.x - 3.0f;
                }
                else if (randomNum < 0.67f)
                {
                    xPosition = spawnOrigin.transform.localPosition.x;
                }
                else
                {
                    xPosition = spawnOrigin.transform.localPosition.x + 3.0f;
                }
                obstaculeObj.transform.localRotation = spawnOrigin.transform.localRotation;
                obstaculeObj.SetActive(true);
                obstaculeObj.transform.localPosition = new Vector3(xPosition, spawnOrigin.transform.localPosition.y, spawnOrigin.transform.localPosition.z);
            }
        }
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(1.45f); // Wait for 1.45 seconds
        AppController.SharedInstance.isRunning = true;
        capybaraAnimator.SetBool("gameRunning", true);
        StartCoroutine(SpawnTimer());
    }
}
