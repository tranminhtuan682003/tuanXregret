public class HeroHealth
{
    public float currentHealth { get; private set; }
    public float maxHealth { get; private set; }

    public HeroHealth(float maxHealth, float initialHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = initialHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}
