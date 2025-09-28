using UnityEngine;

public class Blaster : MonoBehaviour
{


    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] Transform _muzzle;
    [SerializeField]
    [Range(0f, 5f)] float _coolDownTime = 0.25f;


    bool CanFire
    {
        get
        {
            _cooldown -= Time.deltaTime;
            return _cooldown <= 0f;

        }
    }


    float _cooldown;


    // Update is called once per frame
    void Update()
    {
        if(CanFire && Input.GetMouseButton(0))
        {
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        _cooldown = _coolDownTime;
        Instantiate(_projectilePrefab, _muzzle.position, transform.rotation);
    }
}
