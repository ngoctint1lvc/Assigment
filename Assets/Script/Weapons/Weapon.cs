﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public class Weapon : MonoBehaviour {
        // Thời gian giữa các lần tấn công
        public float msDelay = 100;
        float nextTurn;

        public Transform muzzle;

        public NormalBullet nbullet;
        public SphrapnelBullet sbullet;
        
        public float muzzleVelocity = 35;

        delegate void Offensive();
        Offensive att;
        void Start() {
            if (this.tag == "Gun1") att = Shoot1;
            if (this.tag == "Gun2") att = Shoot2;
            if (this.tag == "Gun3") att = Shoot3;
        }

        public void Attack() {
            att();
        }

        void Shoot1() {
            if (Time.time > nextTurn) {
                nextTurn = Time.time + msDelay / 1000;
                NormalBullet newProjectile = Instantiate(nbullet, muzzle.position, muzzle.rotation) as NormalBullet;
                newProjectile.SetSpeed(muzzleVelocity);
            }
        }
        void Shoot2() {
            if (Time.time > nextTurn) {
                nextTurn = Time.time + msDelay / 1000;
                // Đạn có thể bị chệch hướng
                Quaternion bulletDirection;
                bulletDirection.x = muzzle.rotation.x;
                bulletDirection.y = muzzle.rotation.y;
                bulletDirection.z = muzzle.rotation.z;
                bulletDirection.w = muzzle.rotation.w + Random.Range(-0.25f, 0.25f);
                
                NormalBullet newProjectile = Instantiate(nbullet, muzzle.position, bulletDirection) as NormalBullet;
                newProjectile.SetSpeed(muzzleVelocity);
            }
        }
        void Shoot3() {
            if (Time.time > nextTurn) {
                nextTurn = Time.time + msDelay / 1000;
                SphrapnelBullet newProjectile = Instantiate(sbullet, muzzle.position, muzzle.rotation) as SphrapnelBullet;
                newProjectile.SetSpeed(muzzleVelocity);
            }
        }
    } 
}