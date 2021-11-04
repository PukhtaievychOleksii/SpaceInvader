using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public interface ICanTakeDamage{
        public void TakeDamage(float damage);
    }

    public interface ICanGiveDamage
    {
        public float GiveDamage(); 
    }


