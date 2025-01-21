using UnityEngine;

public class Board : MonoBehaviour
{
    [Header("Input Settings: ")]
    [SerializeField] private LayerMask boxesLayerMask;
    [SerializeField] private float touchRadius;

    [Header("Mark Sprites: ")]
    [SerializeField] private Sprite spriteX;
    [SerializeField] private Sprite spriteO;

    public Mark[] marks;

    private Camera cam;
    private Mark currentMark;

    private void Start ()
    {
        cam = Camera.main;

        currentMark = Mark.X;

        marks = new Mark[9];
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp (0))
        {
            Vector2 touchPosition = cam.ScreenToWorldPoint (Input.mousePosition);

            Collider2D hit = Physics2D.OverlapCircle (touchPosition, touchRadius, boxesLayerMask);

            if (hit)
            {
                HitBox(hit.GetComponent <Box> ());
            }
        }
    }

    private void HitBox (Box box)
    {
        if(!box.isMarked)
        {
            marks [box.index] = currentMark;

            box.SetAsMarked (GetSprite(), currentMark);

            if (CheckIfWin())
            {
                Debug.Log (currentMark.ToString () + " Wins!");
                return;
            };

            SwitchPlayer();
        }
    }

    private bool CheckIfWin ()
    {
        return
        AreBoxesMatched (0,1,2) || AreBoxesMatched (3,4,5) || AreBoxesMatched (6,7,8) ||
        AreBoxesMatched (0,3,6) || AreBoxesMatched (1,4,7) || AreBoxesMatched (2,5,8) ||
        AreBoxesMatched (0,4,8) || AreBoxesMatched (2,4,6);
    }

    private bool AreBoxesMatched (int a, int b, int c)
    {
        Mark m = currentMark;
        return (marks[a] == m && marks[b] == m && marks[c] == m);
    }

    private void SwitchPlayer ()
    {
        currentMark = (currentMark == Mark.X) ? Mark.O : Mark.X;
    }

    private Sprite GetSprite ()
    {
        return (currentMark == Mark.X) ? spriteX : spriteO;
    }
}
