using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {
    //design patterns: sourcemaking.com
    //singleton pattern (design pattern)
    private static Singleton instance = null; //new Singleton();

    public static Singleton Instance {
    get {
        //Besta ik al?
        if(instance == null) {
            //Nee? maak mezelf aan
            instance = new Singleton();
        }
        //return mezelf
        return instance;
        }
    }


    //Observer pattern (delegate)
    public delegate void DamageEvent(int amount);

    public class EnemyManager {
        public EnemyManager() {
            Enemy myEnemy = new Enemy();
            myEnemy.onDeath += SomeoneGotHit;
        }

        private void SomeoneGotHit(int thisMuch) {
            //Enemy e = sender as Enemy();
            //myEnemy.onDeath -= 
        }
    }

    public class Enemy {
        public DamageEvent onDeath;

        public Enemy() {
            if(onDeath != null) {
                onDeath(99);
            } 
        }
    }
}
