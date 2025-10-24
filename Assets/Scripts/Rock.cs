using UnityEngine;

public class Rock : MonoBehaviour
{
    private bool isActive = true;

    // Cached variables
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }
    
    public void ToggleState()
    {
        isActive = !isActive;
        var color = _spriteRenderer.color;
        _spriteRenderer.color = new Color(color.r, color.g, color.b, isActive ? 1f : 0.5f);
        if (!isActive)
        {
            Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("Player"),
                LayerMask.NameToLayer("Rock"),
                true
            );
        }
        else
        {
            Physics2D.IgnoreLayerCollision(
                LayerMask.NameToLayer("Player"),
                LayerMask.NameToLayer("Rock"),
                false
            );
        }

    }
}