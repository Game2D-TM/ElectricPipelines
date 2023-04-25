using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Threading;
using Assets.Scripts;
using UnityEngine.EventSystems;
using TMPro;

public class SceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    Snap[] snaps;
    Snap[] preventSnaps;
    public static int restartCount = 2;
    public bool nextStage { get; set; } = true;
    Timer timer;
    public Text restartText;
    public Text winScoreText;
    public Text[] preventPointText;
    public Text[] examplePointText;
    public Text[] preventExamplePointText;

    GameObject nextStageObj;
    GameObject ScoreBoard;
    public Text ScoreBoardText;
    public int nextScenePoint { get; set; } = 0;
    public int CurrentElectric { get; set; } = 0;
    public static readonly int DEFAULT_PREVENTELEC_POINT = -50;
    public static int PREVENTELEC_POINT_DIF = 10;

    public bool Running = true;

    void Start()
    {
        nextStageObj = GameObject.Find("Next");
        timer = GameObject.Find("Timer").GetComponent<Timer>();
        ScoreBoard = GameObject.Find("score");
        if (ScoreBoard != null)
        {
            ScoreBoard.SetActive(false);
        }
        restartText.text = "Live Left: " + restartCount;
        PREVENTELEC_POINT_DIF = Utils.RandomeIntEvenNumber(10, 80);
        LoadSnap();
        LoadPreventSnap();
        LoadDragTargerPoint();
        LoadPreventPipePoint();
        LoadPreventPointText();
        winScoreText.text = $"Win Score: {nextScenePoint}";
        Debug.Log($"Win Score: {nextScenePoint}");
    }

    // Update is called once per frame
    void Update()
    {
        Running = true;
        nextStage = true;
        bool isSnap = true;
        CurrentElectric = 0;
        if (snaps != null && snaps.Length > 0)
        {
            foreach (Snap snap in snaps)
            {
                if (!snap.IsSnapped)
                {
                    nextStage = false;
                    isSnap = false;
                    break;
                }
                else
                {
                    CurrentElectric += snap.target.ElectricCount;
                }
            }
        }
        bool preventSnap = false;
        if (preventSnaps != null && preventSnaps.Length > 0)
        {
            foreach (Snap snap in preventSnaps)
            {
                if (snap.IsSnapped)
                {
                    preventSnap = true;
                    CurrentElectric += snap.ElectricCount + snap.target.ElectricCount;
                    //Debug.Log($"{snap.target.Name} : {snap.target.ElectricCount}" +
                    //    $"| {snap.Name}: {snap.ElectricCount}");
                }
            }
        }
        Debug.Log("Current Electric Point: " + CurrentElectric);
        if (CurrentElectric != nextScenePoint)
        {
            nextStage = false;
            if (isSnap && preventSnap)
            {
                CurrentElectric = 0;
                Running = false;
            }
        }
        if (nextStage)
        {
            timer.Stop();
            SpriteRenderer spriteRenderer = GameObject.Find("bg").GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "game object";
            spriteRenderer.sortingOrder = 15;
            ScoreBoardText.text = CurrentElectric.ToString();
            ScoreBoard.SetActive(true);
        }
    }

    void LoadSnap()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Snap");
        if (go != null && go.Length > 0)
        {
            snaps = new Snap[go.Length];
            int index = 0;
            for (int i = go.Length - 1; i >= 0; i--)
            {
                Snap snap = go[i].GetComponent<Snap>();
                if (snap == null)
                {
                    Debug.LogError("Snap is null");
                    continue;
                }
                snaps[index] = snap;
                index++;
            }
        }
    }
    void LoadPreventSnap()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("PreventSnap");
        if (go != null && go.Length > 0)
        {
            preventSnaps = new Snap[go.Length];
            int index = 0;
            for (int i = go.Length - 1; i >= 0; i--)
            {
                Snap snap = go[i].GetComponent<Snap>();
                if (snap == null)
                {
                    Debug.LogError("Snap is null");
                    continue;
                }
                preventSnaps[index] = snap;
                index++;
            }
        }
    }
    bool isRestart = false;
    public void RestartAnimation()
    {
        if (!isRestart)
        {
            Thread.Sleep(1000);
            if (restartCount > 0)
            {
                restartCount--;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                restartText.text = "Live Left: " + restartCount;
                isRestart = true;
            }
            else
            {
                restartCount = 2;
                SceneManager.LoadScene("GameOver");
                var audio = GameObject.Find("Audio Source");
                if (audio != null)
                {
                    var compo = audio.GetComponent<DontDestroyAudio>();
                    if (compo != null)
                    {
                        compo.CanDestroy = true;
                    }
                }
                Destroy(this);
            }
        }
        else isRestart = false;
    }

    public void Restart()
    {
        if (restartCount > 0)
        {
            restartCount--;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            restartText.text = "Live Left: " + restartCount;
            isRestart = true;
        }
        else
        {
            restartCount = 2;
            SceneManager.LoadScene("GameOver");
            GameObject.Find("Audio Source").GetComponent<DontDestroyAudio>().CanDestroy = true;
            Destroy(this);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        restartCount = 2;
    }

    public void LoadNextStage()
    {
        if (nextStage)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void BackScene()
    {
        Scene curScene = SceneManager.GetActiveScene();
        if (curScene.name.Equals("LevelChoose"))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene("LevelChoose");
        }
    }
    public void ChooseLevel()
    {
        var obj = EventSystem.current.currentSelectedGameObject;
        var name = obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        string level = name.text.Split(" ")[1];
        SceneManager.LoadScene($"Level_{level}");
    }


    public void LoadDragTargerPoint()
    {
        GameObject[] dragPipesGos = GameObject.FindGameObjectsWithTag("DragPipe");
        if (dragPipesGos != null && dragPipesGos.Length > 0)
        {
            foreach (GameObject dragPipeGo in dragPipesGos)
            {
                DragTarget dragTarget = dragPipeGo.transform.GetComponent<DragTarget>();
                nextScenePoint += dragTarget.ElectricCount;
                Debug.Log($"{dragTarget.Name}: " + dragTarget.ElectricCount);
                if (examplePointText != null && examplePointText.Length > 0)
                {
                    foreach (var exampleText in examplePointText)
                    {
                        if (dragPipeGo.name.ToLower().Contains(exampleText.name.ToLower()))
                        {
                            exampleText.text = $": {dragTarget.ElectricCount}v";
                            break;
                        }
                    }
                }
            }
        }
    }

    public void LoadPreventPipePoint()
    {
        GameObject[] preventDragPipesGos = GameObject.FindGameObjectsWithTag("PreventElectric");
        if (preventDragPipesGos != null && preventDragPipesGos.Length > 0)
        {
            foreach (GameObject dragPipeGo in preventDragPipesGos)
            {
                nextScenePoint += DEFAULT_PREVENTELEC_POINT;
                Debug.Log("prevent " + dragPipeGo.name + ": -50");
                foreach (var exampleText in preventExamplePointText)
                {
                    if (dragPipeGo.name.ToLower().Contains(exampleText.name.ToLower()))
                    {
                        exampleText.text = $": {DEFAULT_PREVENTELEC_POINT}v";
                        break;
                    }
                }
            }
        }
    }

    public void LoadPreventPointText()
    {
        if (preventSnaps != null && preventSnaps.Length > 0)
        {
            int indexSnap = Random.RandomRange(1, preventSnaps.Length + 1);
            nextScenePoint += -(PREVENTELEC_POINT_DIF * indexSnap);
            foreach (Snap snap in preventSnaps)
            {
                int i = int.Parse(snap.name.Split("_")[1]);
                int preventSnapPoint = -(PREVENTELEC_POINT_DIF * (i));
                snap.ElectricCount = preventSnapPoint;
                Debug.Log($"prevent Snap {i}: {snap.ElectricCount}");
                preventPointText[i - 1].text = preventSnapPoint + "v";
            }
        }
    }
}

