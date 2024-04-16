using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;


namespace UnityStandardAssets._2D
{

    [CustomEditor(typeof(PlatformerCharacter2D))]
    [CanEditMultipleObjects]
    public class PlatformerCharacter2D_Editor : Editor
    {
        bool showInput = true;
        bool showRigidbody = true;
        bool showMovement = true;
        bool showJump = true;
        bool showSlime = false;

        //PlatformerCharacter2D script;
        SerializedProperty m_AirControl_editor;
        SerializedProperty m_SustainedJump_editor;
        SerializedProperty m_MidAirJump_editor;
        SerializedProperty m_MidAirJumpCount_editor;
        SerializedProperty m_WhatIsGround_editor;
        SerializedProperty m_ApplyMoveForce_editor;
        SerializedProperty m_MaxSpeed_editor;
        SerializedProperty m_Accel_editor;
        SerializedProperty m_AirAccel_editor;
        SerializedProperty m_CrouchSpeed_editor;
        SerializedProperty m_JumpForce_editor;
        SerializedProperty m_JumpSustainedForce_editor;
        SerializedProperty m_JumpSustainedDuration_editor;
        SerializedProperty m_AltMidAirJumpForce_editor;
        SerializedProperty m_JumpForces_editor;
        SerializedProperty m_AltMidAirSustainedJumpForce_editor;
        SerializedProperty m_JumpSustainedForces_editor;
        SerializedProperty m_AltMidAirSustainedJumpDuration_editor;
        SerializedProperty m_JumpSustainedDurations_editor;
        SerializedProperty m_SlimeTemporary_editor;
        SerializedProperty m_SlimeTempDuration_editor;
        SerializedProperty m_SlimeJumpScale_editor;
        SerializedProperty m_SlimeJumpSusForceScale_editor;
        SerializedProperty m_SlimeJumpSusDurationScale_editor;
        SerializedProperty m_SlimeJump2Scale_editor;
        SerializedProperty m_SlimeJump2SusForceScale_editor;
        SerializedProperty m_SlimeJump2SusDurationScale_editor;
        SerializedProperty m_SlimeMassScale_editor;
        SerializedProperty m_SlimeDragScale_editor;
        SerializedProperty m_SlimeAngularDragScale_editor;
        SerializedProperty m_SlimeTint_editor;
        SerializedProperty m_LimitSustainedJumpSpeed_editor;
        SerializedProperty m_MaxSustainedJumpSpeed_editor;
        SerializedProperty m_AirMaxSpeed_editor;

        SerializedProperty m_SlimeSpeedScale_editor;

        // DASH test
        SerializedProperty m_dashDuration_editor;
        SerializedProperty m_dashCoolDown_editor;
        SerializedProperty m_dashMaxSpeedMultiplier_editor;
        SerializedProperty m_dashFade_editor;
        SerializedProperty m_canDash_editor;

        void OnEnable()
        {
            //script = serializedObject.targetObject as PlatformerCharacter2D;

            if(EditorPrefs.HasKey("ShowInput"))
            {
                showInput = EditorPrefs.GetBool("ShowInput");
            }

            if (EditorPrefs.HasKey("ShowRigidbody"))
            {
                showRigidbody = EditorPrefs.GetBool("ShowRigidbody");
            }

            if (EditorPrefs.HasKey("ShowMovement"))
            {
                showMovement = EditorPrefs.GetBool("ShowMovement");
            }

            if (EditorPrefs.HasKey("ShowJump"))
            {
                showJump = EditorPrefs.GetBool("ShowJump");
            }

            if (EditorPrefs.HasKey("ShowSlime"))
            {
                showSlime = EditorPrefs.GetBool("ShowSlime");
            }

            m_AirControl_editor = serializedObject.FindProperty("m_AirControl");

            m_SustainedJump_editor = serializedObject.FindProperty("m_SustainedJump");

            m_MidAirJump_editor = serializedObject.FindProperty("m_MidAirJump");

            m_MidAirJumpCount_editor = serializedObject.FindProperty("m_MidAirJumpCount");

            m_WhatIsGround_editor = serializedObject.FindProperty("m_WhatIsGround");

            m_ApplyMoveForce_editor = serializedObject.FindProperty("m_ApplyMoveForce");

            m_MaxSpeed_editor = serializedObject.FindProperty("m_MaxSpeed");

            m_Accel_editor = serializedObject.FindProperty("m_Accel");

            m_AirAccel_editor = serializedObject.FindProperty("m_AirAccel");

            m_CrouchSpeed_editor = serializedObject.FindProperty("m_CrouchSpeed");

            m_JumpForce_editor = serializedObject.FindProperty("m_JumpForce");

            m_JumpSustainedForce_editor = serializedObject.FindProperty("m_JumpSustainedForce");

            m_JumpSustainedDuration_editor = serializedObject.FindProperty("m_JumpSustainedDuration");

            m_AltMidAirJumpForce_editor = serializedObject.FindProperty("m_AltMidAirJumpForce");

            m_JumpForces_editor = serializedObject.FindProperty("m_JumpForces");

            m_AltMidAirSustainedJumpForce_editor = serializedObject.FindProperty("m_AltMidAirSustainedJumpForce");

            m_JumpSustainedForces_editor = serializedObject.FindProperty("m_JumpSustainedForces");

            m_AltMidAirSustainedJumpDuration_editor = serializedObject.FindProperty("m_AltMidAirSustainedJumpDuration");

            m_JumpSustainedDurations_editor = serializedObject.FindProperty("m_JumpSustainedDurations");

            m_SlimeTemporary_editor = serializedObject.FindProperty("m_SlimeTemporary");

            m_SlimeTempDuration_editor = serializedObject.FindProperty("m_SlimeTempDuration");

            m_SlimeSpeedScale_editor = serializedObject.FindProperty("m_SlimeSpeedScale");

            m_SlimeJumpScale_editor = serializedObject.FindProperty("m_SlimeJumpScale");
           
            m_SlimeJumpSusForceScale_editor = serializedObject.FindProperty("m_SlimeJumpSusForceScale");
            
            m_SlimeJumpSusDurationScale_editor = serializedObject.FindProperty("m_SlimeJumpSusDurationScale");

            m_SlimeJump2Scale_editor = serializedObject.FindProperty("m_SlimeJump2Scale");

            m_SlimeJump2SusForceScale_editor = serializedObject.FindProperty("m_SlimeJump2SusForceScale");

            m_SlimeJump2SusDurationScale_editor = serializedObject.FindProperty("m_SlimeJump2SusDurationScale");

            m_SlimeMassScale_editor = serializedObject.FindProperty("m_SlimeMassScale");

            m_SlimeDragScale_editor = serializedObject.FindProperty("m_SlimeDragScale");

            m_SlimeAngularDragScale_editor = serializedObject.FindProperty("m_SlimeAngularDragScale");

            m_SlimeTint_editor = serializedObject.FindProperty("m_SlimeTint");

            m_LimitSustainedJumpSpeed_editor = serializedObject.FindProperty("m_LimitSustainedJumpSpeed");

            m_MaxSustainedJumpSpeed_editor = serializedObject.FindProperty("m_MaxSustainedJumpSpeed");

            m_AirMaxSpeed_editor = serializedObject.FindProperty("m_AirMaxSpeed");

            // DASH test
            m_dashDuration_editor = serializedObject.FindProperty("m_dashDuration");
            m_dashCoolDown_editor = serializedObject.FindProperty("m_dashCoolDown");
            m_dashMaxSpeedMultiplier_editor = serializedObject.FindProperty("m_dashMaxSpeedMultiplier");
            m_dashFade_editor = serializedObject.FindProperty("m_dashFade");
            m_canDash_editor = serializedObject.FindProperty("m_canDash");

            //Debug.Log(serializedObject.FindProperty("m_AutoRun").boolValue);
        }

        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector(); // display all other non-HideInInspector fields

            serializedObject.Update();

            EditorGUILayout.Space();

            showInput = EditorGUILayout.Foldout(showInput, "Control modes", true);
            if (showInput)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.HelpBox("The control modes are used to adjust the kind of control the player has with the character. Enabling continued sustained jumping or air jumping will show additional controls in the Jump settings foldout.", MessageType.Info);
                //EditorGUILayout.LabelField("Input settings", EditorStyles.boldLabel);
                
                EditorGUILayout.LabelField("Player control modifiers", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(m_AirControl_editor, new GUIContent("Enable Air Control", "Option so a player can change the horizontal (x axis) value while in the air."));

                EditorGUILayout.PropertyField(m_SustainedJump_editor, new GUIContent("Enable Sustained Jump", "Option so a player can hold jump to add more force while jumping."));

                EditorGUILayout.PropertyField(m_MidAirJump_editor, new GUIContent("Enable Air Jump", "Option so the player can jump again while in the air."));

                EditorGUILayout.PropertyField(m_canDash_editor, new GUIContent("Enable Dash", "Option so the player can gain a horizontal movement dash while on the ground or in mid air"));

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            showRigidbody = EditorGUILayout.Foldout(showRigidbody, "Rigidbody 2D settings", true);
            if (showRigidbody)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.HelpBox("These notes refer to the attached Rigidbody 2D component on the 2DCharacter.", MessageType.Info);

                EditorGUILayout.LabelField("Body Type", "The player should be set to Dynamic.");

                EditorGUILayout.LabelField("Material", "Type of Physics Material used on the collider.");
                EditorGUILayout.HelpBox("A Physics Material 2D is used to adjust the friction and bounce that occurs between 2D physics objects when they collide. Typically this would remain as None. Instead use the environment to change the way the player feels (e.g. sticky vs icy platforms).", MessageType.Info);

                EditorGUILayout.LabelField("Simulated", "This should be set to True.");

                EditorGUILayout.LabelField("Use Auto Mass", "This should be set to False.");

                EditorGUILayout.LabelField("Mass", "Determines the inertia of our player");
                EditorGUILayout.HelpBox("The Mass will affect the player's resistance to change in acceleration, as well as how the player interacts with other physics objects (e.g. large mass player colliding with low mass objects or vice versa).", MessageType.Info);
                EditorGUILayout.HelpBox("Changing the Mass of the player will affect other character 2D settings.", MessageType.Warning);

                EditorGUILayout.LabelField("Linear Drag", "How quickly the player will stop moving.");
                EditorGUILayout.HelpBox("Linear Drag will affect how much ground and air resistance effects the player.", MessageType.Info);
                EditorGUILayout.HelpBox("Changing the Linear Drag of the player will affect other character 2D settings.", MessageType.Warning);

                EditorGUILayout.LabelField("Angular Drag", "How quickly the player will stop rotating.");
                EditorGUILayout.HelpBox("No rotation is applied to the player, therefore Angular Drag will have little to no effect.", MessageType.Info);

                EditorGUILayout.LabelField("Gravity Scale", "How much gravity is applied.");
                EditorGUILayout.HelpBox("Gravity Scale allows you to change the amount of gravity felt by an individual rigidbody 2D without having to change the global gravity value.", MessageType.Info);
                EditorGUILayout.HelpBox("Changing the Gravity Scale of the player will affect other rigidbody 2D and character 2D settings.", MessageType.Warning);

                EditorGUILayout.LabelField("Collision Detection", "The player should be set to Continuous.");

                EditorGUILayout.LabelField("Sleeping Mode", "The player should be set to Start Awake.");

                EditorGUILayout.LabelField("Interpolate", "The player should be set to Interpolate.");

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            showMovement = EditorGUILayout.Foldout(showMovement, "Movement settings", true);
            if (showMovement)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.HelpBox("Movement settings are used adjust the player’s horizontal (x axis) movement. Swapping between Instantaneous or Additive modes will show / hide controls in the movement modifiers foldout.", MessageType.Info);

                EditorGUILayout.HelpBox("Your Rigidbody 2D settings will also affect these values. The amount of Mass or Linear Drag the player has will affect the amount of force needed to get the player moving.", MessageType.Warning);

                EditorGUILayout.LabelField("Movement modifiers", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(m_ApplyMoveForce_editor, new GUIContent("Apply Move Force", "How should force be applied to the horizontal (x axis): Instantaneous applies max speed on input; Additive applies acceleration on input until max speed is reached."));

                if (m_ApplyMoveForce_editor.intValue == 1)
                {
                    EditorGUILayout.PropertyField(m_Accel_editor, new GUIContent("Acceleration", "Amount of force added to the player every fixed frame in the horizontal (x axis)."));
                    EditorGUILayout.PropertyField(m_AirAccel_editor, new GUIContent("Air Acceleration", "Amount of force added to the player while in the air every fixed frame in the horizontal (x axis)."));
                }
                
                EditorGUILayout.PropertyField(m_MaxSpeed_editor, new GUIContent("Max Speed", "The fastest the player can travel in the horizontal (x axis)."));
                EditorGUILayout.PropertyField(m_AirMaxSpeed_editor, new GUIContent("Air Max Speed", "The fastest the player can travel in the horizontal (x axis) while in the air."));
                EditorGUILayout.PropertyField(m_CrouchSpeed_editor, new GUIContent("Crouch Speed", "Amount of maxSpeed applied to crouching movement. A value of 1 is equal to 100% of MaxSpeed."));

                if (m_canDash_editor.boolValue)
                {
                    EditorGUILayout.LabelField("Dash modifiers", EditorStyles.boldLabel);
                    EditorGUILayout.PropertyField(m_dashDuration_editor, new GUIContent("Dash Duration", "How long in seconds that the dash speed multiplier lasts."));
                    EditorGUILayout.PropertyField(m_dashMaxSpeedMultiplier_editor, new GUIContent("Dash Max Speed Multiplier", "The dash will start with this multiple of the player's top speed"));
                    EditorGUILayout.PropertyField(m_dashCoolDown_editor, new GUIContent("Dash Cooldown", "How long the dash is inactive before becoming available to use again"));
                    EditorGUILayout.PropertyField(m_dashFade_editor, new GUIContent("Dash Fade", "The amount the dash multiplier fades off over time."));
                }

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            showJump = EditorGUILayout.Foldout(showJump, "Jump settings", true);
            if (showJump)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.HelpBox("Jump settings are used to adjust the player's vertical (y axis) movement.", MessageType.Info);

                EditorGUILayout.HelpBox("Your Rigidbody 2D settings will also affect these values. The amount of Mass, Linear Drag, and Gravity Scale the player has will affect the amount of force needed to get the player into the air.", MessageType.Warning);

                EditorGUILayout.LabelField("Grounded Jump modifier", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(m_JumpForce_editor, new GUIContent("Grounded Jump Force", "Amount of force added when the player jumps."));

                if (m_SustainedJump_editor.boolValue)
                {
                    EditorGUILayout.LabelField("Continued Jump modifiers", EditorStyles.boldLabel);

                    EditorGUILayout.PropertyField(m_JumpSustainedForce_editor, new GUIContent("Cont'd Jump Force", "Amount of force added to the player every fixed frame in the vertical (y axis) while a player holds jump."));
                    EditorGUILayout.PropertyField(m_JumpSustainedDuration_editor, new GUIContent("Cont'd Jump Dur", "Amount of time in seconds (deltaTime) the sustained jump force is added to the player every fixed frame."));

                    EditorGUILayout.PropertyField(m_LimitSustainedJumpSpeed_editor);

                    if (m_LimitSustainedJumpSpeed_editor.boolValue)
                    {
                        EditorGUILayout.PropertyField(m_MaxSustainedJumpSpeed_editor);
                    }

                }

                if (m_MidAirJump_editor.boolValue)
                {
                    EditorGUILayout.LabelField("Air Jump modifiers", EditorStyles.boldLabel);

                    EditorGUILayout.PropertyField(m_MidAirJumpCount_editor, new GUIContent("Air Jump Count", "Amount of mid air jumps the player can perform."));

                    EditorGUILayout.PropertyField(m_AltMidAirJumpForce_editor, new GUIContent("Alt Air Jump Force", "Option to use an alternative mid air jump force while mid air jumping."));

                    if (m_AltMidAirJumpForce_editor.boolValue)
                    {
                        for (int i = 0; i < m_JumpForces_editor.arraySize; i++)
                        {
                            EditorGUILayout.PropertyField(m_JumpForces_editor.GetArrayElementAtIndex(i), new GUIContent("Air Jump " + (i + 1) + " Force", "Amount of force added when the player jumps while in the air."));
                        }
                    }

                    EditorGUILayout.PropertyField(m_AltMidAirSustainedJumpForce_editor, new GUIContent("Alt Air Cont'd Jump Force", "Option to use an alternative sustained jump force to mid air jump."));

                    if (m_AltMidAirSustainedJumpForce_editor.boolValue)
                    {
                        for (int i = 0; i < m_JumpSustainedForces_editor.arraySize; i++)
                        {
                            EditorGUILayout.PropertyField(m_JumpSustainedForces_editor.GetArrayElementAtIndex(i), new GUIContent("Air Jump " + (i + 1) + " Cont'd Force", "Amount of force added to the player air jump every fixed frame in the vertical (y axis) while a player holds jump."));
                        }
                    }

                    EditorGUILayout.PropertyField(m_AltMidAirSustainedJumpDuration_editor, new GUIContent("Alt Air Cont'd Jump Dur", "Option to use an alternative sustained jump duration while mid air jumping."));

                    if (m_AltMidAirSustainedJumpDuration_editor.boolValue)
                    {
                        for (int i = 0; i < m_JumpSustainedDurations_editor.arraySize; i++)
                        {
                            EditorGUILayout.PropertyField(m_JumpSustainedDurations_editor.GetArrayElementAtIndex(i), new GUIContent("Air Jump " + (i + 1) + " Cont'd Dur", "Amount of time in seconds (deltaTime) the sustained mid air jump force is added to the player every fixed frame."));
                        }
                    }
                }
                
                EditorGUILayout.LabelField("What is Ground modifier", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(m_WhatIsGround_editor, new GUIContent("What is Ground", "A mask that determines which object layers are considered as ground to the player."));

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            showSlime = EditorGUILayout.Foldout(showSlime, "Slime settings", true);
            if (showSlime)
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.HelpBox("Slime allows the character move, jump, air jump, and rigidbody settings to be modified during gameplay. " +
                                        "While it’s called slimed, think of this as a state modifier that can be set to temporary (in seconds) or permanent until removed, for one or all of the below elements of the player feel.", MessageType.Info);

                EditorGUILayout.HelpBox("Your Rigidbody 2D settings will also affect these values. The amount of Mass, Linear Drag, and Gravity Scale the player has will affect how drastic the slime effect feels.", MessageType.Warning);

                EditorGUILayout.LabelField("Slimed Move modifiers", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(m_SlimeSpeedScale_editor, new GUIContent("Slime Speed Scale", "Scaling factor on move force."));

                EditorGUILayout.PropertyField(m_SlimeTemporary_editor, new GUIContent("Slime Temporary", "Sets the slime effect to last for a set duration at full intensity."));

                EditorGUILayout.PropertyField(m_SlimeTempDuration_editor, new GUIContent("Slime Temporary Dur", "Sets the duration in seconds of the temporary slimed effect."));

                EditorGUILayout.PropertyField(m_SlimeTint_editor, new GUIContent("Slime Tint Colour", "Sets the colour of the player while under the slimed effect."));

                EditorGUILayout.LabelField("Slimed Jump modifiers", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(m_SlimeJumpScale_editor, new GUIContent("Slime Jump Scale", "Scaling factor on jump Y force."));
                
                EditorGUILayout.PropertyField(m_SlimeJumpSusForceScale_editor, new GUIContent("Slime Jump Cont'd Force Scale", "Scaling factor on jump sustained force."));
                
                EditorGUILayout.PropertyField(m_SlimeJumpSusDurationScale_editor, new GUIContent("Slime Jump Cont'd Dur Scale", "Scaling factor on jump sustained force duration."));

                EditorGUILayout.LabelField("Slimed Air Jump modifiers", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(m_SlimeJump2Scale_editor, new GUIContent("Slime Air Jump Scale", "Scaling factor on air jump Y Force."));

                EditorGUILayout.PropertyField(m_SlimeJump2SusForceScale_editor, new GUIContent("Slime Air Jump Cont'd Force Scale", "Scaling factor on air jump sustained force."));

                EditorGUILayout.PropertyField(m_SlimeJump2SusDurationScale_editor, new GUIContent("Slime Air Jump Cont'd Dur Scale", "Scaling factor on air jump sustained force duration."));

                EditorGUILayout.LabelField("Slimed Rigidbody 2D modifiers", EditorStyles.boldLabel);

                EditorGUILayout.PropertyField(m_SlimeMassScale_editor, new GUIContent("Slime Mass Scale", "Scaling factor on character mass."));
                
                EditorGUILayout.PropertyField(m_SlimeDragScale_editor, new GUIContent("Slime Drag Scale", "Scaling factor on character's linear drag."));

                EditorGUILayout.PropertyField(m_SlimeAngularDragScale_editor, new GUIContent("Slime Angular Drag Scale", "Scaling factor on character's angular drag."));

                EditorGUILayout.HelpBox("‘Slimed’ method should be called from another script (e.g. TriggerStay) using SendMessage. The player is set to a 'slimed' state, which changes the player move / platforming settings to the slime scaled values.", MessageType.Info);

                EditorGUILayout.HelpBox("If using a timed slime effect, the ‘SlimeExit’ method should be called from from a TriggerExit using SendMessage to set the slime effect counting down.", MessageType.Info);

                EditorGUILayout.HelpBox("‘WashSlime’ method should be called from another script (e.g. TriggerExit) using SendMessage. The player’s slime state is set to false, returning the player move / platforming settings back to their default values.", MessageType.Info);

                EditorGUI.indentLevel--;
            }
            
            serializedObject.ApplyModifiedProperties();
        }


        public void OnInspectorUpdate()
        {
            this.Repaint();
        }


        public void OnDestroy()
        {
            EditorPrefs.SetBool("ShowInput", showInput);
            EditorPrefs.SetBool("ShowRigidbody", showRigidbody);
            EditorPrefs.SetBool("ShowMovement", showMovement);
            EditorPrefs.SetBool("ShowJump", showJump);
            EditorPrefs.SetBool("ShowSlime", showSlime);

        }
    }
}
