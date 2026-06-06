using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public enum BonusType
    {
        None,
        Life,
        SpeedBoost,
        Invincibility
    }

    [SerializeField] private int scoreValue = 1;
    [SerializeField] private BonusType bonusType;
    [SerializeField] private int lifeBonus = 1;
    [SerializeField] private float speedMultiplier = 1.4f;
    [SerializeField] private float bonusDuration = 4f;

    public int ScoreValue => scoreValue;

    public void Configure(int score, BonusType bonus, int life, float speed, float duration)
    {
        scoreValue = score;
        bonusType = bonus;
        lifeBonus = life;
        speedMultiplier = speed;
        bonusDuration = duration;
    }

    public void ApplyBonus(GameObject player)
    {
        if (player == null)
        {
            return;
        }

        switch (bonusType)
        {
            case BonusType.Life:
                HeroLife heroLife = player.GetComponent<HeroLife>();
                if (heroLife != null)
                {
                    heroLife.AddLife(lifeBonus);
                }
                break;

            case BonusType.SpeedBoost:
                PlayerMove playerMove = player.GetComponent<PlayerMove>();
                if (playerMove != null)
                {
                    playerMove.ApplySpeedBoost(speedMultiplier, bonusDuration);
                }
                break;

            case BonusType.Invincibility:
                HeroLife invincibleHero = player.GetComponent<HeroLife>();
                if (invincibleHero != null)
                {
                    invincibleHero.GrantInvincibility(bonusDuration);
                }
                break;
        }
    }
}
