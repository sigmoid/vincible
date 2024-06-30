using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public Vector3 VanishingPoint;

    public Gradient ColorGradient;

    public GameObject SpriteTemplate;

    public int MinSprites = 5;
    public int MaxSprites = 20;

    public int NumSprites;

    public float LayerOffset;

    private List<GameObject> _sprites;

    // Start is called before the first frame update
    void Start()
    {
        NumSprites = Random.Range(MinSprites, MaxSprites);
        _sprites = new List<GameObject>();
        CreateSprites();
    }

    void CreateSprites()
    {
        for (int i = 0; i < NumSprites; i++)
        {
            var sprite = GameObject.Instantiate(SpriteTemplate, this.transform);

            var spriteComponent = sprite.GetComponent<SpriteRenderer>();

            spriteComponent.color = ColorGradient.Evaluate(1.0f - ((float)i / (float)NumSprites));

            spriteComponent.sortingOrder = i;

            _sprites.Add(sprite);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpritePositions();
    }

    void UpdateSpritePositions()
    {

        var basePosition = this.transform.position;

        var vanishDir = VanishingPoint - basePosition;
        vanishDir.Normalize();

        for (int i = 0; i < NumSprites; i++)
        {
            var sprite = _sprites[i];
            sprite.transform.position = basePosition - vanishDir * i * LayerOffset;
        }
    }
}
