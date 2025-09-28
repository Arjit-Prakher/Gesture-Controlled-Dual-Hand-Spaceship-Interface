using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    [Range(5000f, 25000f)]
    float launchForce = 10000f;
    [SerializeField]
    [Range(10, 1000)] int _damage = 100;
    [SerializeField]
    [Range(2f, 10f)] float _range = 5f;


    bool OutOfFuel
    {
        get
        {
            _duration -= Time.deltaTime;
            return _duration <= 0f;
        }
    }

    Rigidbody _rigidbody;
    float _duration;



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rigidbody.AddForce(launchForce * transform.forward);
        _duration = _range;

    }

    private void Update()
    {
        if (OutOfFuel) Destroy(gameObject);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log($"Projectile collided with {collision.collider.name}");
    //}

}
