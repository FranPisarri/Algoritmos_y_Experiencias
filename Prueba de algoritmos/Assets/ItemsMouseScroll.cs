using UnityEngine;
using UnityEngine.UI;

public class ItemsMouseScroll : MonoBehaviour
{
    [SerializeField] Image[] item_bar;
    [SerializeField] Color selected_color;
    [SerializeField] Color unselected_color;
    int actual_item = 0;

    private void Start()
    {
        item_bar[0].color = selected_color;
        InputManager.Instance.MouseScroll += ScrollMenu;
    }


    void ScrollMenu(Vector2 dir)
    {
        item_bar[actual_item].color = unselected_color;
        actual_item -= (int)dir.y;
        if (actual_item < 0) actual_item = item_bar.Length - 1;
        if (actual_item >= item_bar.Length) actual_item = 0;
        item_bar[actual_item].color = selected_color;
    }
}
