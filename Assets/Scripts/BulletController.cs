using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : BeatListener
{
    public GameObject bulletPrefab;

    public override void Start()
    {
        base.Start();
    }

    public override void BeatAction()
    {
        var bullet = Instantiate(bulletPrefab);
        var rigidBody = bullet.GetComponent<Rigidbody2D>();
        var transform = bullet.GetComponent<Transform>();

        transform.position = this.transform.position;
        var player = GameObject.Find("Player");
        var dir = player.transform.position - this.transform.position;
        dir.Normalize();
        //rigidBody.velocity = dir * 4;
    }
}
