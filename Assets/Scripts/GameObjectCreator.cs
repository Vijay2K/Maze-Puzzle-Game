using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectCreator : MonoBehaviour
{
    public static void Create(int x, int z, float scale, PrimitiveType type, bool hasTrialRenderer, Color color)
    {
        GameObject obj = GameObject.CreatePrimitive(type);
        GameObject endTxt = Resources.Load<GameObject>("Prefabs/end_text");

        Vector3 pos = new Vector3(x, 0, z);
        obj.transform.position = pos;
        obj.transform.localScale = Vector3.one * scale;
        obj.GetComponent<Renderer>().material.color = color;
        obj.GetComponent<BoxCollider>().size = Vector3.one * (scale + 1.5f);

        if(hasTrialRenderer)
        {
            Mover mover = obj.AddComponent<Mover>();
            mover.speed = 20;
            TrailRenderer trailRend = obj.AddComponent<TrailRenderer>();
            trailRend.material = new Material(Shader.Find("Sprites/Default"));

            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(scale, scale);
            curve.AddKey(scale, scale);

            trailRend.widthCurve = curve;

            float alpha = 1f;

            Gradient gradient = new Gradient();
            GradientColorKey[] colorKey =
            {
                new GradientColorKey(color, 0f),
                new GradientColorKey(color, 1f)
            };

            GradientAlphaKey[] alphaKey =
            {
                new GradientAlphaKey(alpha, 0.75f),
                new GradientAlphaKey(alpha, 1.0f)
            };

            gradient.SetKeys(colorKey, alphaKey);
            trailRend.colorGradient = gradient;
        } else
        {
            GameObject end = Instantiate(endTxt);
            end.transform.position = obj.transform.position + Vector3.up * 1.5f;
        }
    }
}
