using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private GameObject AlienPrefab;
    [SerializeField]
    private List<GameObject> AsteoidPrefabsBySize;
    void Awake()
    {
        DataHolder.SetFactory(this);
        DataHolder.SetNumbeOfAsteroidTypes(AsteoidPrefabsBySize.Count);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Bullet AddBullet(Vector3 bulletPosition,Vector3 lookAtTarget)
    {
        Bullet bullet = Instantiate(BulletPrefab, bulletPosition, Quaternion.identity).GetComponentInChildren<Bullet>();
        bullet.transform.up = lookAtTarget - bullet.transform.position;
        return bullet;

    }

    public Asteroid AddAsteroid(Vector3 postion,int sizeIndex)
    {
        Asteroid asteroid = Instantiate(AsteoidPrefabsBySize[sizeIndex], postion, Quaternion.identity).GetComponent<Asteroid>();
        asteroid.sizeIndex = sizeIndex;
        asteroid.SetRigidbody();
        DataHolder.Game.Asteroids.Add(asteroid);
        asteroid.AddRandomMoveForce();
        return asteroid;
    }

    public void AddPlayer(string NickName,int startScore)
    {
        Player player = new Player(NickName, startScore);
        bool AlreadyExist = false;
        for(int i = 0;i < DataHolder.Game.Players.Count; i++)
        {
            if (player.NickName == DataHolder.Game.Players[i].NickName) AlreadyExist = true;
        }
        if (!AlreadyExist) DataHolder.Game.Players.Add(player);
        
    }

    public void AddNewAlien(Vector3 position, int hardLevel, bool intelligent)
    {
        Alien Alien = Instantiate(AlienPrefab, position, Quaternion.identity).GetComponent<Alien>();
        Alien.shotDispertion = Alien.DefaultShotDispercion / hardLevel;
        Alien.timeBetweenShots = Alien.DefaultTimeBetweenShots / hardLevel;
        DataHolder.Game.Aliens.Add(Alien);
    }
}
