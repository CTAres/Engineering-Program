using UnityEngine;

public class Box : MonoBehaviour
{
    public int index;
    public Mark mark;
    public bool isMarked;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        index = transform.GetSiblingIndex();
        mark = Mark.None;
        isMarked = false;
    }

    public void SetAsMarked(Sprite sprite, Mark mark)
    {
        isMarked = true;
        this.mark = mark;

        spriteRenderer.sprite = sprite;

        //Collider gets disabled so that it cant be overwriten
        GetComponent<CircleCollider2D>().enabled = false;  
    }

    public void ResetBox(Sprite empty)
    {
        isMarked = false;
        this.mark = Mark.None;

        spriteRenderer.sprite = empty;
        
        //Collider gets enabled so that it can be used again
        GetComponent<CircleCollider2D>().enabled = true;  
    }
}
