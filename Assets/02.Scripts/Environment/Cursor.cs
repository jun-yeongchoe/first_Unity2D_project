using UnityEngine;


public class Cursor : MonoBehaviour
{
    [SerializeField] Texture2D cursorImg;
    [SerializeField] Texture2D isEnemyOn;
    [SerializeField] LayerMask enemy;
   
    private void Update()
    {
        var p = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var tex = Physics2D.OverlapPoint(p, enemy) ? isEnemyOn : cursorImg;
        var hot = new Vector2(tex.width * 0.5f, tex.height * 0.5f);

        UnityEngine.Cursor.SetCursor(tex, hot, CursorMode.ForceSoftware);
    }

}
