using UnityEngine;

public class Human
{
    public float TimeUntilDeath { get; private set; }
    public bool IsDead { get; private set; }
    public bool ShouldBeVisible { get; private set; }

    private GameObject visualObject;

    public Human(float lifetime)
    {
        TimeUntilDeath = lifetime;
        IsDead = false;
        ShouldBeVisible = false;
        visualObject = null;
    }

    public void SetVisible(GameObject prefab, Vector3 position, Transform parent = null)
    {
        if (visualObject != null) return;

        visualObject = Object.Instantiate(prefab, position, Quaternion.identity, parent);
        ShouldBeVisible = true;
    }

    public void Update(float deltaTime)
    {
        if (IsDead) return;

        TimeUntilDeath -= deltaTime;
        if (TimeUntilDeath <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;
        if (visualObject != null)
        {
            Object.Destroy(visualObject);
        }
    }
}