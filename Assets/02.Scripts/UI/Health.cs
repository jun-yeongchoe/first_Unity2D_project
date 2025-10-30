using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour 
{ 
    
    public static void HpDown(RectTransform front, int curHp, int maxHp) 
    {
        float r = Mathf.Clamp01((float)curHp / maxHp);
        var s = front.localScale;
        s.x = r;
        front.localScale = s;

        var c = front.GetComponentInChildren<Image>();
        if (r < 0.3f) c.color = Color.red;
        else if (r < 0.7f) c.color = Color.yellow;
        else c.color = Color.green;
    } 
}