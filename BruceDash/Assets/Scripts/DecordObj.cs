using UnityEngine;

public class DecordObj : MovingObject
{
    private BackgroundDecor backgroundDecor;

    public void Init(BackgroundDecor backgroundDecor)
    {
        this.backgroundDecor = backgroundDecor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EndMapTrigger endMap))
        {
            backgroundDecor.ReturnDecor(this);
        }
    }
}
