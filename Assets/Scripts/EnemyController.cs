using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent enemy;

    [SerializeField]
    Transform player;
    private Animator _animator;

    [SerializeField]
    float timer = 5f;

    [SerializeField]
    float bulletTime;

    [SerializeField]
    GameObject enemyBullet;

    [SerializeField]
    Transform bulletSpawnPoint;

    [SerializeField]
    float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(player.position);

        _animator.SetBool("Move", true);

        ShootAtPlayer();
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0)
            return;

        _animator.Play("KayKit Animated Character|Throw");

        bulletTime = timer;
        GameObject bulletObj =
            Instantiate(
                enemyBullet,
                bulletSpawnPoint.transform.position,
                bulletSpawnPoint.transform.rotation
            ) as GameObject;

        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        bulletRig.AddForce(bulletRig.transform.forward * bulletSpeed);
        Destroy(bulletObj, 3f);
    }
}
