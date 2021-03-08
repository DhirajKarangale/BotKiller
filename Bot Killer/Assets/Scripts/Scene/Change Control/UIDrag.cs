using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIDrag : MonoBehaviour,IDragHandler,IPointerDownHandler
{
    public Vector2 deafultSize;
    public Vector2 deafultPosition;
    private Vector2 currPosition;
    private Vector3 newPosition;
    RectTransform rectTransform;
    private ButtonCustomizer buttonCustomizer;
    int currentScene;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        buttonCustomizer = GetComponentInParent<ButtonCustomizer>();
        SetSizeAndOpicity(PlayerPrefs.GetFloat(transform.name + "size", rectTransform.sizeDelta.x / deafultSize.x),PlayerPrefs.GetFloat(transform.name + "opcity",1f));
        rectTransform.anchoredPosition = new Vector2(PlayerPrefs.GetFloat(transform.name + "x",deafultPosition.x), PlayerPrefs.GetFloat(transform.name + "y",deafultPosition.y));
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        if(currentScene == 3)
        {
        currPosition = RectTransformUtility.WorldToScreenPoint(new Camera(),transform.position);
        currPosition.x = Mathf.Clamp(currPosition.x,rectTransform.sizeDelta.x/2f,Screen.width-rectTransform.sizeDelta.x/2f);
        currPosition.y = Mathf.Clamp(currPosition.y,rectTransform.sizeDelta.y/2f,Screen.height-rectTransform.sizeDelta.y/2f);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform,currPosition,new Camera(),out newPosition);
        transform.position = newPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       if(currentScene == 3)
       {
        buttonCustomizer.selectButton = this;
        buttonCustomizer.SetButtonData(rectTransform.sizeDelta.x/deafultSize.x,GetComponent<CanvasGroup>().alpha);
       }
    }

    public void OnDrag(PointerEventData eventData)
    {
       if(currentScene == 3)
       {
         transform.position = Input.mousePosition;
       }
    }

    public void SetSizeAndOpicity(float size,float opicit)
   {
    rectTransform.sizeDelta = deafultSize * size;
    foreach (var g in GetComponentsInChildren<CanvasGroup>())
    {
        g.alpha = opicit;
    }
   }

   public void SaveData()
   {
       PlayerPrefs.SetFloat(transform.name + "size",rectTransform.sizeDelta.x/deafultSize.x);
       PlayerPrefs.SetFloat(transform.name + "opcity",GetComponent<CanvasGroup>().alpha);
       PlayerPrefs.SetFloat(transform.name + "x",rectTransform.anchoredPosition.x);
       PlayerPrefs.SetFloat(transform.name + "y",rectTransform.anchoredPosition.y);
   }

   public void ResetUI()
   {
       SetSizeAndOpicity(1f,1f);
       rectTransform.anchoredPosition = deafultPosition;
   }
}
