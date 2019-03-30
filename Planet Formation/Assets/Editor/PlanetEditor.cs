using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor {

    // Use "Using UnityEditor" and : Editor class to create custom inspector device and customEditor

    Planet planet;
    Editor shapeEditor;
    Editor colourEditor;

    // foldout bool values stored in Planet script

    // To work the editor, ask what ref does

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                planet.GeneratePlanet();  // voids the on validate method in Planet Script, making it all go throught the editor
            }
        }

        if (GUILayout.Button("Generate Planet"))
        {
            planet.GeneratePlanet();
        }
        DrawSettingsEditor(planet.shapeSettings, planet.onShapeSettingsUpdated, ref planet.shapeSettingFoldout, ref shapeEditor);
        DrawSettingsEditor(planet.colourSettings, planet.OnColourSettingsUpdated, ref planet.colourSettingFoldout, ref colourEditor);
    }

    //Create editor
    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref bool foldout, ref Editor editor)
    {
        // To work in real time effect to the Unity scene
        if (settings != null)
        {
            foldout = EditorGUILayout.InspectorTitlebar(foldout, settings); // true means that it is alwasys folded out (can't fold)
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                //draw on editor if folded out
                if (foldout)
                {
                    CreateCachedEditor(settings, null, ref editor); // cacehd editor is using preeisting editors so that a new editor does not have to be made a new setting is changed 
                    editor.OnInspectorGUI();
                }

                if (check.changed)
                {
                    if (onSettingsUpdated != null)
                    {
                        onSettingsUpdated();
                    }
                }
            }
        }
    }


    // calls function as soon as it is available to be used
    private void OnEnable()
    {
        // ask someone how this works??
        planet = (Planet)target;
    }
}
