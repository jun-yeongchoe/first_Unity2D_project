//using System.Collections;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;

//public class UiManager : MonoBehaviour
//{
//    public static UiManager Instance { get; private set; }

//    [Header("UI")]
//    [SerializeField] private GameObject panel;
//    [SerializeField] private TextMeshProUGUI message;
//    [SerializeField] private Button saveBtn;
//    [SerializeField] private Button cancelBtn;

//    [Header("Toast")]
//    [SerializeField] private CanvasGroup toast;
//    [SerializeField] private TextMeshProUGUI toastText;
//    [SerializeField] private float toastDuration = 1.5f;

//    private string checkPointID;

//    private void Awake()
//    {
//        if(Instance != null)
//        {
//            Destroy(Instance);
//            return;
//        }
//        Instance = this;
//        DontDestroyOnLoad(gameObject);
//    }

//    private void Start()
//    {
//        if (panel != null)
//        {
//            panel.SetActive(false);
//        }

//        //세이브 버튼 이벤트
//        if(saveBtn != null)
//        {
//            saveBtn.onClick.AddListener(OnClickSave);
//        }
//        if(cancelBtn != null) cancelBtn.onClick.AddListener(Hide);
//    }

//    // 저장
//    void OnClickSave()
//    {
//        if (!string.IsNullOrEmpty(checkPointID)) 
//        {
//            //게임 매니저에 요청하자.
//            if (GameManager.instance != null) 
//            {
//                GameManager.instance.SaveCheckPoint(checkPointID);
//                //메시지 출력
//                StartCoroutine(ShowToastCo("complete"));

//            }
//        }
//        Hide();
//    }
//    //패널 보여줌
//    public void Show(string msg)
//    {
//        checkPointID = msg;
//        if (message != null) {
//            message.text = "save?";
//        }
//        if (panel != null) {
//            panel.SetActive(true);
//        }
//    }

//    //패널 숨김
//    public void Hide()
//    {
//        checkPointID = null;
//        if(panel != null) panel.SetActive(false);
//    }

//    IEnumerator ShowToastCo(string msg)
//    {
//        if (toastText != null) 
//        {   
//            toastText.text = msg;
//        }
//        if (toast == null) yield break;

//        float time = 0.0f;
//        //Fade In
//        while(time < 0.2f)
//        {
//            time += Time.deltaTime;
//            toast.alpha = Mathf.Lerp(0.0f, 1.0f, time/0.2f);
//            yield return null;
//        }
//        //대기
//        yield return new WaitForSeconds(toastDuration);
//        //Fade out
//        time = 0.0f;
//        while(time < 0.25f)
//        {
//            time += Time.deltaTime;
//            toast.alpha = Mathf.Lerp(1.0f, 0.0f, time / 0.25f);
//            yield return null;
//        }
//    }
//}
