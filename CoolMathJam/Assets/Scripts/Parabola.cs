using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola : MonoBehaviour
{
    //object that moves along parabola.
    public Transform someObject; 

    //transforms that mark the start and end
    public Transform Start;
    public Transform End;
    //desired parabola height
    public float h;

    void Update()
    {
        if (Start && End && someObject)
        {
            //Shows how to animate something following a parabola
            float objectT = Time.time % 1; //completes the parabola trip in one second
            someObject.position = SampleParabola(Start.position, End.position, h, objectT);
        }
    }


    void OnDrawGizmos()
    {
        //Draw the height in the viewport, so i can make a better gif :]
        //Handles.BeginGUI();
        //GUI.skin.box.fontSize = 16;
        //GUI.Box(new Rect(10, 10, 100, 25), h + "");
        //Handles.EndGUI();

        //Draw the parabola by sample a few times
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Start.position, End.position);
        float count = 20;
        Vector3 lastP = Start.position;
        for (float i = 0; i < count + 1; i++)
        {
            Vector3 p = SampleParabola(Start.position, End.position, h, i / count);
            Gizmos.color = i % 2 == 0 ? Color.blue : Color.green;
            Gizmos.DrawLine(lastP, p);
            lastP = p;
        }
    }

    #region Parabola sampling function
    /// <summary>
    /// Get position from a parabola defined by start and end, height, and time
    /// </summary>
    /// <param name='start'>
    /// The start point of the parabola
    /// </param>
    /// <param name='end'>
    /// The end point of the parabola
    /// </param>
    /// <param name='height'>
    /// The height of the parabola at its maximum
    /// </param>
    /// <param name='t'>
    /// Normalized time (0->1)
    /// </param>S
    public static Vector3 SampleParabola(Vector3 start, Vector3 end, float height, float t)
    {
        float parabolicT = t * 2 - 1;
        Vector3 travelDirection = end - start;
        Vector3 levelDirection = end - new Vector3(start.x, end.y, start.z);
        Vector3 right = Vector3.Cross(travelDirection, levelDirection);
        Vector3 up = Vector3.Cross(right, levelDirection);
        if (end.y > start.y) up = -up;
        Vector3 result = start + t * travelDirection;
        result += ((-parabolicT * parabolicT + 1) * height) * up.normalized;
        return result;
    }
    #endregion
}
