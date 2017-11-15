using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObj {

    Target GetMain();
    void SetMain(Target main);
    void update();
    GameObject GetGameObject();
    void SetParent(Transform parent);
}
