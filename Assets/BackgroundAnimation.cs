using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    public SpriteRenderer backgroundImage;
    public SpriteRenderer bigStars;
    public SpriteRenderer smallStars;
    private float closeSpeed = 0.24f;
    private float farSpeed = 1.24f;
    private float backgroundSpeed = 1f;

    private float repeatWidthBackground;
    private float repeatWidthCloseStars;
    private float repeatWidthFarStars;
    private Vector3 startPos = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        
        repeatWidthBackground = -backgroundImage.bounds.size.x;
        repeatWidthCloseStars = -bigStars.bounds.size.x;
        repeatWidthFarStars = -smallStars.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackground(backgroundImage, backgroundSpeed, repeatWidthBackground);
        MoveBackground(bigStars, farSpeed, repeatWidthCloseStars);
        MoveBackground(smallStars, closeSpeed, repeatWidthFarStars);
    }

    private void MoveBackground(SpriteRenderer image, float speed, float width)
    {
        image.transform.Translate(Vector3.left * Time.deltaTime * speed);
        if(image.transform.position.x <= width)
        {
            image.transform.position = startPos;
        }
    }
}
