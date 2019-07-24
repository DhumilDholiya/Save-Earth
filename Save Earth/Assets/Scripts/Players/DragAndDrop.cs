using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public bool isMoveAllowed { get; private set; }

    Collider2D col;
    float touchRadius = 1.2f;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if(touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapCircle(touchPos,touchRadius);
                if(col == touchedCollider)
                {
                    isMoveAllowed = true;
                }
            }
            if(touch.phase == TouchPhase.Moved)
            {
                if(isMoveAllowed)
                {
                    transform.position = new Vector2(touchPos.x,touchPos.y);
                }
            }
            if(touch.phase == TouchPhase.Ended)
            {
                isMoveAllowed = false;
            }
        }
    }
}
