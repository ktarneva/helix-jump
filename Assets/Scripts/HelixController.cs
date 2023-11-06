using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPos;
    private Vector3 startRotaiting;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject HelixLvlPrefab;

    public List<StageController> allStages = new List<StageController>();
    private float helixDistance;
    private List<GameObject> spawnedLvls = new List<GameObject>();
    private void Awake()
    {
        startRotaiting = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y -(goalTransform.localPosition.y +0.1f);
        LoadStage(0);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 curTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
            {
                lastTapPos = curTapPos; ;
            }

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * delta);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }
    }
    public void  LoadStage(int stageNum)
    {
        StageController stage = allStages[Mathf.Clamp(stageNum, 0, allStages.Count - 1)];
        if (stage == null) 
        {
            Debug.LogError("No Stage" + stageNum + "found! Are all stages loaded in the List");
            return;
        }
        //Change the color of the background
        Camera.main.backgroundColor = allStages[stageNum].stageBackgroundColor;

        //Change the color of the ball
        FindObjectOfType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNum].stageBallColor;

        //Reset the rotation of the Helix
        transform.localEulerAngles = startRotaiting;

        //destroy old lvls
        foreach (GameObject gameObject in spawnedLvls)
        {
            Destroy(gameObject);
        }

        //generate new lvl
        float lvlDistance = helixDistance / stage.lvl.Count;
        float spawnPosY = topTransform.localPosition.y;
        for (int i = 0; i < stage.lvl.Count; i++)
        {
            spawnPosY -= lvlDistance;

            //creates the lvl in scene
            GameObject level = Instantiate(HelixLvlPrefab, transform);
            Debug.Log("Levels Spawned!");
            level.transform.localPosition = new Vector3(0,spawnPosY,0);
            spawnedLvls.Add(level);

            //creating disabledParts
            int partsToDisable = 12 - stage.lvl[i].partCount;
            List<GameObject> disabledParts = new List<GameObject>();

            while (disabledParts.Count <partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;
                if (!disabledParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disabledParts.Add(randomPart);
                }
            }

            
            //generating the selected color for each part
            List<GameObject> leftParts = new List<GameObject>();

            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNum].stageLvlPartColor;
                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }
            //creating Death Parts
            List<GameObject> deathParts = new List<GameObject>();

            while (deathParts.Count < stage.lvl[i].deathPartCount)
            {
                GameObject randomDeathPart = leftParts[(Random.Range(0, leftParts.Count))];

                if (!deathParts.Contains(randomDeathPart))
                {
                    randomDeathPart.gameObject.AddComponent<DeathTrigger>();
                    deathParts.Add(randomDeathPart);
                }
            }
        }
    }


}
