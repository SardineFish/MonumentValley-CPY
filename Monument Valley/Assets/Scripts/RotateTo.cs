using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTo : MonoBehaviour
{
    public class RotateOptions
    {
        public GameObject GameObject;
        public Vector3 TargetRotation;
        public float Time;
        public float StartTime;
        public float RunTime;
        public Func<float, float> TimeFunction;
        public RotateOptions(GameObject gameObj, Vector3 target, float time)
        {
            this.GameObject = gameObj;
            this.TargetRotation = target;
            this.Time = time;
            this.TimeFunction = (x) =>
            {
                return x;
            };
        }
        public RotateOptions(GameObject gameObj, Vector3 target, float time, Func<float, float> timeFunction)
        {
            this.GameObject = gameObj;
            this.TargetRotation = target;
            this.Time = time;
            this.TimeFunction = timeFunction;
        }
        public void Rotate(float dt)
        {
            var x = (RunTime + dt) / Time;
            x = TimeFunction(x);
            var lastX = RunTime / Time;
            lastX = TimeFunction(lastX);
            if (x >= 1)
            {
                x = 1;
                var dx = (x - lastX) / (1 - lastX);
                var dv = (TargetRotation - GameObject.transform.rotation.eulerAngles) * dx;
                GameObject.transform.Rotate(dv);
                if (OnFinished != null)
                    OnFinished.Invoke(this, new EventArgs());
            }
            else
            {
                var dx = (x - lastX) / (1 - lastX);
                var dv = (TargetRotation - GameObject.transform.rotation.eulerAngles) * dx;
                GameObject.transform.Rotate(dv);
            }

            RunTime += dt;
        }
        public event EventHandler OnFinished;
    }
    public static Dictionary<GameObject, RotateOptions> RotateList = new Dictionary<GameObject, RotateOptions>();
    public static void Start(RotateOptions options)
    {
        RotateList[options.GameObject] = options;
        options.OnFinished += (sender, e) =>
        {
            RotateTo.RotateList.Remove((sender as RotateTo.RotateOptions).GameObject);
        };
    }
    public static void Stop(GameObject gameObj)
    {
        if (RotateList.ContainsKey(gameObj))
            RotateList.Remove(gameObj);
    }
    // Use this for initialization
    void Start()
    {
        //MoveList = new Dictionary<GameObject, MoveOptions>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in RotateList)
        {
            if (!item.Key)
            {
                RotateList.Remove(item.Key);
            }
            item.Value.Rotate(Time.deltaTime);
        }
    }
}
