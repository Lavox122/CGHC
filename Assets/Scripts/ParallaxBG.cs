using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxFactor = new Vector2(0.5f, 0.3f);
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float spriteWidthInUnits;

    private const int REPEAT_BACKGROUND_TIMES = 3;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        spriteRenderer.size = new Vector2(spriteRenderer.size.x * REPEAT_BACKGROUND_TIMES, spriteRenderer.size.y);

        float textureWidth = spriteRenderer.sprite.texture.width;
        float pixelPerUnit = spriteRenderer.sprite.pixelsPerUnit;

        spriteWidthInUnits = textureWidth / pixelPerUnit;
    }

    //Update is called once per frame
    private void LateUpdate()
    {
        Vector3 currentCameraPosition = cameraTransform.position;
        Vector3 cameraMovementThisFrame = currentCameraPosition - previousCameraPosition;
        transform.position -= new Vector3(cameraMovementThisFrame.x * parallaxFactor.x, cameraMovementThisFrame.y * parallaxFactor.y, 0);

        previousCameraPosition = currentCameraPosition;

        if (cameraTransform.position.x - transform.position.x >= spriteWidthInUnits)
        {
            transform.position = new Vector3(cameraTransform.position.x, transform.position.y, transform.position.z);
        }
    }
}
