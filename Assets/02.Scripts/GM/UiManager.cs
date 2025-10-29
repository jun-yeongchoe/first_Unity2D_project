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

//        //���̺� ��ư �̺�Ʈ
//        if(saveBtn != null)
//        {
//            saveBtn.onClick.AddListener(OnClickSave);
//        }
//        if(cancelBtn != null) cancelBtn.onClick.AddListener(Hide);
//    }

//    // ����
//    void OnClickSave()
//    {
//        if (!string.IsNullOrEmpty(checkPointID)) 
//        {
//            //���� �Ŵ����� ��û����.
//            if (GameManager.instance != null) 
//            {
//                GameManager.instance.SaveCheckPoint(checkPointID);
//                //�޽��� ���
//                StartCoroutine(ShowToastCo("complete"));

//            }
//        }
//        Hide();
//    }
//    //�г� ������
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

//    //�г� ����
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
//        //���
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
