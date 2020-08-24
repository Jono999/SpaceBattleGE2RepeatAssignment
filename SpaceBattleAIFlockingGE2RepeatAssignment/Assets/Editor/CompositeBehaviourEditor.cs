using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CompositeBehaviour))]
public class CompositeBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CompositeBehaviour cb = (CompositeBehaviour) target;

        //Rect r = EditorGUILayout.BeginHorizontal();
        //r.height = EditorGUIUtility.singleLineHeight;

        //check for behaviours
        if (cb.behaviours == null || cb.behaviours.Length == 0)
        {
            EditorGUILayout.HelpBox("No Behaviours in Array.", MessageType.Warning);
            // EditorGUILayout.EndHorizontal();
            //EditorGUILayout.BeginHorizontal();
            //r.height = EditorGUIUtility.singleLineHeight;
        }
        else
        {

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Number", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));

            EditorGUILayout.LabelField("Behaviours", GUILayout.MinWidth(60f));

            EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));

            EditorGUILayout.EndHorizontal();



            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < cb.behaviours.Length; i++)

            {

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));

                cb.behaviours[i] = (FlockBehaviour) EditorGUILayout.ObjectField(cb.behaviours[i], typeof(FlockBehaviour),
                    false, GUILayout.MinWidth(60f));

                cb.weights[i] =
                    EditorGUILayout.FloatField(cb.weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));

                EditorGUILayout.EndHorizontal();

            }

            if (EditorGUI.EndChangeCheck())

            {

                EditorUtility.SetDirty(cb);

            }

        }



        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Behaviour"))

        {

            AddBehavior(cb);

            EditorUtility.SetDirty(cb);

        }

        EditorGUILayout.EndHorizontal();



        if (cb.behaviours != null && cb.behaviours.Length > 0)

        {

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Remove Behaviour"))

            {

                RemoveBehavior(cb);

                EditorUtility.SetDirty(cb);

            }

            EditorGUILayout.EndHorizontal();

        }

    }



    private void AddBehavior(CompositeBehaviour cb)

    {

        int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;

        FlockBehaviour[] newBehaviors = new FlockBehaviour[oldCount + 1];

        float[] newWeights = new float[oldCount + 1];

        for (int i = 0; i < oldCount; i++)

        {

            newBehaviors[i] = cb.behaviours[i];

            newWeights[i] = cb.weights[i];

        }

        newWeights[oldCount] = 1f;

        cb.behaviours = newBehaviors;

        cb.weights = newWeights;

    }

    private void RemoveBehavior(CompositeBehaviour cb)

    {

        int oldCount = cb.behaviours.Length;
        if (oldCount == 1)
        {
            cb.behaviours = null;
            cb.weights = null;
            return;
        }

        else

        {

            FlockBehaviour[] newBehaviors = new FlockBehaviour[oldCount - 1];

            float[] newWeights = new float[oldCount - 1];

            for (int i = 0; i < oldCount - 1; i++)

            {

                newBehaviors[i] = cb.behaviours[i];

                newWeights[i] = cb.weights[i];

            }

            cb.behaviours = newBehaviors;

            cb.weights = newWeights;

        }

    }
}

//void RemoveBehaviour(CompositeBehaviour cb)
//{
//int oldCount = cb.behaviours.Length;
//    if (oldCount == 1)
//{
//    cb.behaviours = null;
//    cb.weights = null;
//    return;
//}
//FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount - 1];
//float[] newWeights = new float[oldCount - 1];
//    for (int i = 0; i < oldCount - 1; i++)
//{
//    newBehaviours[i] = cb.behaviours[i];
//    newWeights[i] = cb.weights[i];
//}
//cb.behaviours = newBehaviours;
//cb.weights = newWeights;
//}

/* else
 {
     //r.x = 30f;
    // r.width = EditorGUIUtility.currentViewWidth - 95f;
    // EditorGUI.LabelField(r, "Behaviours");
    // r.x = EditorGUIUtility.currentViewWidth - 65f;
    // r.width = 60f;
    // EditorGUI.LabelField(r, "Weights");
    // r.y += EditorGUIUtility.singleLineHeight * 1.2f;
    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField("Number", );

     EditorGUI.BeginChangeCheck();
     for (int i = 0; i < cb.behaviours.Length; i++)
     {
         r.x = 5f;
         r.width = 20f;
         EditorGUI.LabelField(r, i.ToString());
         r.x = 30f;
         r.width = EditorGUIUtility.currentViewWidth - 95f;
         cb.behaviours[i] =
             (FlockBehaviour) EditorGUI.ObjectField(r, cb.behaviours[i], typeof(FlockBehaviour), false);
         r.x = EditorGUIUtility.currentViewWidth - 65f;
         r.width = 60f;
         cb.weights[i] = EditorGUI.FloatField(r, cb.weights[i]);
         r.y += EditorGUIUtility.singleLineHeight * 1.1f;
     }

     if (EditorGUI.EndChangeCheck())
     {
         EditorUtility.SetDirty(cb);
     }
 }

 EditorGUILayout.EndHorizontal();
     r.x = 5f;
     r.width = EditorGUIUtility.currentViewWidth - 10f;
     r.y += EditorGUIUtility.singleLineHeight * 0.5f;

     if (GUI.Button(r, "Add Behaviour"))
     {
         //add behaviour#
         AddBehaviour(cb);
         EditorUtility.SetDirty(cb);
     }

     r.y += EditorGUIUtility.singleLineHeight * 1.5f;
     if (cb.behaviours != null && cb.behaviours.Length > 0)
     {
         if (GUI.Button(r, "Remove Behaviour"))
         {
             //remove behaviour
             RemoveBehaviour(cb);
             EditorUtility.SetDirty(cb);
         }
     }
}

void AddBehaviour(CompositeBehaviour cb)
{
 int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
 FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount + 1];
 float[] newWeights = new float[oldCount + 1];
 for (int i = 0; i < oldCount; i++)
 {
     newBehaviours[i] = cb.behaviours[i];
     newWeights[i] = cb.weights[i];
 }

 newWeights[oldCount] = 1f;
 cb.behaviours = newBehaviours;
 cb.weights = newWeights;
}

void RemoveBehaviour(CompositeBehaviour cb)
{
 int oldCount = cb.behaviours.Length;
 if (oldCount == 1)
 {
     cb.behaviours = null;
     cb.weights = null;
     return;
 }
 FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount - 1];
 float[] newWeights = new float[oldCount - 1];
 for (int i = 0; i < oldCount - 1; i++)
 {
     newBehaviours[i] = cb.behaviours[i];
     newWeights[i] = cb.weights[i];
 }
 cb.behaviours = newBehaviours;
 cb.weights = newWeights;
}
}*/
