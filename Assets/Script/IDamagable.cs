using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame {
    public interface IDamageable {
        void TakeHit(float dame, Vector3 hitPoint, Vector3 hitDirection);
        void TakeHit(float dame);
    }
}
