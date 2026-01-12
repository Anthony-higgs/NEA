using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Targeting")]
    public float detectionRange = 10f;
    public LayerMask enemyLayer;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    public float bulletSpeed = 10f;

    private float nextFireTime;
    private Transform currentTarget;

    void Update()
    {
        FindTarget();
        Aim();
        Shoot();
    }
    // followed a tutorial quite closely to help with all the unity maths stuff
    void FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            transform.position,
            detectionRange,
            enemyLayer
        );

        float closestDist = Mathf.Infinity;
        currentTarget = null;

        foreach (Collider2D hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                currentTarget = hit.transform;
            }
        }
    }
    Vector2 PredictTargetPosition()
    {
        if (currentTarget == null) return Vector2.zero;

        Rigidbody2D enemyRb = currentTarget.GetComponent<Rigidbody2D>();
        if (enemyRb == null)
            return currentTarget.position;

        Vector2 enemyPos = currentTarget.position;
        Vector2 enemyVel = enemyRb.velocity;

        float distance = Vector2.Distance(transform.position, enemyPos);
        float timeToHit = distance / bulletSpeed;

        return enemyPos + enemyVel * timeToHit;
    }
    void Aim()
    {
        if (currentTarget == null) return;

        Vector2 predictedPos = PredictTargetPosition();
        Vector2 direction = predictedPos - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Debug.Log("aiming");
    }
    void Shoot()
    {
        Debug.Log("Bullet shot");
        if (currentTarget == null) return;
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            transform.rotation
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.Init(transform.up, bulletSpeed);
    }


}
