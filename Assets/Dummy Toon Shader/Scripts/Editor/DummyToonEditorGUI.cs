using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Rokk.DummyToon.Editor
{
    public class DummyToonEditorGUI : DummyToonEditorBase
    {
        // Main
        protected MaterialProperty mainTex = null;
        protected MaterialProperty color = null;
        protected MaterialProperty bumpMap = null;
        protected MaterialProperty bumpScale = null;
        protected MaterialProperty cutoff = null;
        protected MaterialProperty emissionMap = null;
        protected MaterialProperty emissionColor = null;
        protected MaterialProperty emissionIsTint = null;
        protected MaterialProperty emissionPremultiplyEnabled = null;
        protected MaterialProperty cullMode = null;
        protected MaterialProperty renderMode = null;
        protected MaterialProperty zWrite = null;
        protected MaterialProperty cutoutEnabled = null;

        // AO
        protected MaterialProperty occlusionMap = null;
        protected MaterialProperty occlusionStrength = null;

        // Toon lighting
        protected MaterialProperty ramp = null;
        protected MaterialProperty toonContrast = null;
        protected MaterialProperty toonRampOffset = null;
        protected MaterialProperty staticToonLight = null;
        protected MaterialProperty intensity = null;
        protected MaterialProperty saturation = null;
        protected MaterialProperty directLightBoost = null;
        protected MaterialProperty indirectLightBoost = null;
        protected MaterialProperty rampTintingEnabled = null;
        protected MaterialProperty indirectLightDirMergeMin = null;
        protected MaterialProperty indirectLightDirMergeMax = null;
        protected MaterialProperty rampAntiAliasingEnabled = null;
        protected MaterialProperty overrideWorldLightDirection = null;
        protected MaterialProperty additiveRampMode = null;
        protected MaterialProperty additiveRamp = null;
        protected MaterialProperty lightDirectionNudge = null;

        // Metallic and specular (old)
        protected MaterialProperty metallicMode = null;
        protected MaterialProperty metallicMapTex = null;
        protected MaterialProperty metallic = null;
        protected MaterialProperty smoothness = null;
        protected MaterialProperty metalSpecularTex = null;
        protected MaterialProperty metalSpecularColor = null;

        // New specular
        protected MaterialProperty specularEnabled = null;
        protected MaterialProperty specularMode = null;
        protected MaterialProperty specularMap = null;
        protected MaterialProperty specularColor = null;
        protected MaterialProperty specularToonyEnabled = null;
        protected MaterialProperty specularToonyCutoff = null;
        protected MaterialProperty specularIndirectLightBoost = null;

        // Ramp masking
        protected MaterialProperty rampMaskingEnabled = null;
        protected MaterialProperty rampMaskTex = null;
        protected MaterialProperty rampR = null;
        protected MaterialProperty toonContrastR = null;
        protected MaterialProperty toonRampOffsetR = null;
        protected MaterialProperty intensityR = null;
        protected MaterialProperty saturationR = null;
        protected MaterialProperty rampG = null;
        protected MaterialProperty toonContrastG = null;
        protected MaterialProperty toonRampOffsetG = null;
        protected MaterialProperty intensityG = null;
        protected MaterialProperty saturationG = null;
        protected MaterialProperty rampB = null;
        protected MaterialProperty toonContrastB = null;
        protected MaterialProperty toonRampOffsetB = null;
        protected MaterialProperty intensityB = null;
        protected MaterialProperty saturationB = null;

        // Rimlight
        protected MaterialProperty rimLightMode = null;
        protected MaterialProperty rimLightColor = null;
        protected MaterialProperty rimTex = null;
        protected MaterialProperty rimWidth = null;
        protected MaterialProperty rimInvert = null;

        // Matcap
        protected MaterialProperty matCapTex = null;
        protected MaterialProperty matCapMode = null;
        protected MaterialProperty matCapStrength = null;
        protected MaterialProperty matCapTintTex = null;
        protected MaterialProperty matCapColor = null;
        protected MaterialProperty matCapOrigin = null;

        // Outlines
        protected MaterialProperty outlineWidth = null;
        protected MaterialProperty outlineColor = null;
        protected MaterialProperty outlineTex = null;
        protected MaterialProperty outlineScreenspaceEnabled = null;
        protected MaterialProperty outlineScreenspaceMinDist = null;
        protected MaterialProperty outlineScreenspaceMaxDist = null;
        protected MaterialProperty outlineStencilComp = null;
        protected MaterialProperty outlineAlphaWidthEnabled = null;

        // Alpha To Coverage
        protected MaterialProperty alphaToCoverageEnabled = null;
        protected MaterialProperty alphaToCoverageCutoff = null;

        // Detail normal
        protected MaterialProperty detailNormalTex = null;
        protected MaterialProperty detailNormalScale = null;
        protected MaterialProperty detailNormalUvMap = null;
        protected MaterialProperty detailMaskTex = null;

        // Vertex Offset
        protected MaterialProperty vertexOffsetEnabled = null;
        protected MaterialProperty vertexOffsetPos = null;
        protected MaterialProperty vertexOffsetRot = null;
        protected MaterialProperty vertexOffsetScale = null;
        protected MaterialProperty vertexOffsetPosWorld = null;

        // Stencils
        protected MaterialProperty stencilRef = null;
        protected MaterialProperty stencilPassOp = null;
        protected MaterialProperty stencilFailOp = null;
        protected MaterialProperty stencilZFailOp = null;
        protected MaterialProperty stencilCompareFunction = null;

        // ZTest
        protected MaterialProperty zTest = null;

        // Hue Shift
        protected MaterialProperty hueShiftEnabled = null;
        protected MaterialProperty hueShiftAmount = null;
        protected MaterialProperty hueShiftMaskTex = null;
        protected MaterialProperty hueShiftAmountOverTime = null;

        // Debug properties
        /// <summary>
        /// Controls whether the debug section is enabled at all.
        /// </summary>
        protected MaterialProperty debugOptionsEnabled = null;

        /// <summary>
        /// Controls whether debug is *currently* enabled or not. Allows toggling the feature on/off at runtime without having to adjust the keywords.
        /// </summary>
        protected MaterialProperty debugEnabled = null;
        protected MaterialProperty debugMinLightBrightness = null;
        protected MaterialProperty debugMaxLightBrightness = null;
        protected MaterialProperty debugNormalsEnabled = null;
        protected MaterialProperty debugUvsEnabled = null;
        protected MaterialProperty debugUvsOffset = null;

        // Internal properties
        protected MaterialProperty srcBlend = null;
        protected MaterialProperty dstBlend = null;
        protected MaterialProperty outlineStencilWriteAction;

        // Keeps track of which sections are opened and closed.
        private bool mainExpanded = true;
        private bool toonExpanded = true;
        private bool outlinesExpanded = true;
        private bool metallicExpanded = false;
        private bool specularExpanded = false;
        private bool matcapExpanded = false;
        private bool rimlightExpanded = false;
        private bool vertexOffsetExpanded = false;
        private bool hueShiftExpanded = false;
        private bool debugExpanded = false;
        private bool miscExpanded = false;
#if SHADEROPTIMIZER_INSTALLED
        private bool shaderOptimizerExpanded = false;
#endif

        private bool emissionTintHelpExpanded = false;
        private bool emissionPremultiplyHelpExpanded = false;
        private bool renderModeHelpExpanded = false;
        private bool rampTintHelpExpanded = false;
        private bool rampMaskHelpExpanded = false;
        private bool indirectLightMergeHelpExpanded = false;
        private bool rampAntiAliasingHelpExpanded = false;
        private bool additiveRampHelpExpanded = false;
        private bool matCapOriginHelpExpanded = false;
        private bool vertexOffsetHelpExpanded = false;
        private bool specularModeHelpExpanded = false;
        private bool lightDirectionNudgeHelpExpanded = false;

        private bool shouldRegenerateKeywords = true;

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            base.OnGUI(materialEditor, properties);

            shouldRegenerateKeywords = true;

            // Show a disclaimer if the material is locked
            if (IsMaterialLocked())
            {
                EditorGUILayout.HelpBox("Material is locked, property changes might not go through. Unlock the material under \"Shader Optimizer\" to edit it.", MessageType.Info);
            }

            // Draw the main shader property groups
            DrawShaderProperties();

            // Always draw the misc and shader optimizer sections last
            DrawMisc();

#if SHADEROPTIMIZER_INSTALLED
            if (CanMaterialBeLocked())
            {
                DrawShaderOptimizer();
            }
#endif

            Divider();

            DrawVersion();

            // Setup shader keywords
            SetupKeywords();

            if (HasOutlines())
            {
                SetupOutlineStencilValues();
            }
        }

        /// <summary>
        /// Draw the main shader property groups. Can be overridden to add new sections more easily.
        /// If overridden, the base implementation should be called first.
        /// </summary>
        protected virtual void DrawShaderProperties()
        {
            DrawMain();
            if (HasOutlines())
            {
                DrawOutlines();
            }
            DrawToonLighting();
            DrawMetallic();
            DrawSpecular();
            DrawMatcap();
            DrawRimlight();

            if (HasVertexOffset())
            {
                DrawVertexOffset();
            }

            DrawHueShift();

            DrawDebug();
        }

        // Called when user switches to this shader
        public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
        {
            base.AssignNewShaderToMaterial(material, oldShader, newShader);

            SetupBlendModes(material);

            // Fix invalid properties carried over from Noenoe
            if (oldShader.name.ToUpperInvariant().Contains("NOENOE"))
            {
                material.SetFloat("_Intensity", 1.3f);
                material.SetFloat("_Saturation", 1f);
                material.SetFloat("_ToonContrast", 0.5f);
            }
        }

        private void DrawMain()
        {
            mainExpanded = Section("Main", mainExpanded);
            if (!mainExpanded)
            {
                return;
            }

            editor.TexturePropertySingleLine(new GUIContent("Main Texture", "The main albedo texture to use."), mainTex, color);

            editor.TexturePropertySingleLine(new GUIContent("Normal Map"), bumpMap, bumpScale);

            editor.TexturePropertySingleLine(new GUIContent("Emission Map"), emissionMap, emissionColor);

            EditorGUI.indentLevel += 2;
            ShaderPropertyWithHelp(
                emissionIsTint,
                new GUIContent("Tinted Emission", "If enabled, the emission is maintex * emission rather than just emission."),
                ref emissionTintHelpExpanded,
                "If enabled, the emission color is determined by main texture * emission. Useful for emission maps that are just solid colors.\r\n\r\n" +
                "If disabled, the emission map is used as-is."
            );

            if (renderMode.floatValue == 3)
            {
                ShaderPropertyWithHelp(
                    emissionPremultiplyEnabled,
                    new GUIContent("Premultiply Emission", "If enabled, the emission color will be premultiplied by the alpha."),
                    ref emissionPremultiplyHelpExpanded,
                    "If enabled, the emission color will be multiplied by the alpha. It will fade out as the alpha decreases.\r\n\r\n" +
                    "If disabled, the emission will not fade out when alpha decreases."
                );
            }
            EditorGUI.indentLevel -= 2;

            editor.ShaderProperty(cullMode, new GUIContent("Sidedness", "Which sides of the mesh should be rendered."));

            EditorGUI.BeginChangeCheck();

            ShaderPropertyWithHelp(
                renderMode,
                new GUIContent("Render Mode", "Change the rendering mode of the material."),
                ref renderModeHelpExpanded,
                "Sets the transparency mode.\n\n" +
                "Opaque: No transparency. Default.\n\n" +
                "Fade: Traditional alpha blended transparency. Best used for things like blush effects, ghosts and holograms.\n\n" +
                "Transparent: More physically plausible transparency. Special effects such as metallics and specular highlights are not affected by alpha. Best used for things like glass and other physical surfaces."
            );

            if (EditorGUI.EndChangeCheck())
            {
                SetupBlendModes();
            }

            EditorGUI.BeginChangeCheck();
            editor.ShaderProperty(cutoutEnabled, new GUIContent("Cutout", "Whether to use alpha-based cutout (alpha testing)."));
            if (EditorGUI.EndChangeCheck())
            {
                SetupBlendModes();
            }

            if (cutoutEnabled.floatValue == 1)
            {
                EditorGUI.indentLevel += 2;
                editor.RangeProperty(cutoff, "Alpha Cutoff");
                EditorGUI.indentLevel -= 2;
            }

            editor.ShaderProperty(zWrite, new GUIContent("ZWrite", "Whether the shader should write to the depth buffer or not."));

            if (renderMode.floatValue == 0 && zWrite.floatValue == 0)
            {
                EditorGUILayout.HelpBox("ZWrite is disabled on a non-transparent rendering mode. This is likely not intentional.", MessageType.Warning);
            }

            editor.TexturePropertySingleLine(new GUIContent("Occlusion Map", "The Ambient Occlusion map for this material. The red channel of the AO map determines how much indirect light is contributed."), occlusionMap);
            if(occlusionMap.textureValue != null)
            {
                EditorGUI.indentLevel += 2;
                editor.RangeProperty(occlusionStrength, "Occlusion Strength");
                EditorGUI.indentLevel -= 2;
                if (occlusionStrength.floatValue == 0)
                {
                    EditorGUILayout.HelpBox("Occlusion Strength is 0. Consider setting the Occlusion Map texture to None.", MessageType.Warning);
                }
            }

            editor.TextureScaleOffsetProperty(mainTex);
        }

        private void DrawToonLighting()
        {
            toonExpanded = Section("Toon Lighting", toonExpanded);
            if (!toonExpanded)
            {
                return;
            }

            ToonRampProperty(new GUIContent("Toon Ramp"), ramp);

            editor.RangeProperty(toonContrast, "Toon Contrast");
            editor.RangeProperty(toonRampOffset, "Toon Ramp Offset");
            editor.RangeProperty(intensity, "Intensity");
            editor.RangeProperty(saturation, "Saturation");
            editor.RangeProperty(directLightBoost, "Direct Lighting Boost");
            editor.RangeProperty(indirectLightBoost, "Indirect Lighting Boost");

            EditorGUILayout.Space();

            // Light direction merge properties and help box
            LabelWithHelp(
                new GUIContent("Indirect Light Direction Merge"),
                ref indirectLightMergeHelpExpanded,
                "If the dominant light direction of the ambient lighting is too close to the direction of the most important realtime directional light, their light directions will be merged together. " +
                "This helps prevent the creation of two different light directions on the same Mixed directional light."
            );

            EditorGUI.indentLevel += 2;
            editor.RangeProperty(indirectLightDirMergeMin, "Minimum Merge Threshold");
            editor.RangeProperty(indirectLightDirMergeMax, "Maximum Merge Threshold");
            EditorGUI.indentLevel -= 2;

            EditorGUILayout.Space();

            // Draw the ramp AA toggle and a help box button horizontally
            ShaderPropertyWithHelp(
                rampAntiAliasingEnabled,
                new GUIContent("Ramp Anti-Aliasing", "Enable ramp anti-aliasing, which can eliminate jagged edges on sharper toon ramps."),
                ref rampAntiAliasingHelpExpanded,
                "Enables anti-aliasing on toon ramp textures, which can help eliminate jagged edges on sharper toon ramps. The filter mode of the ramp texture must not be set to Point."
            );

            // Draw the ramp tinting toggle and a help box button horizontally
            ShaderPropertyWithHelp(
                rampTintingEnabled,
                new GUIContent("Ramp Tinting", "Enable the Ramp Tinting feature, which tints shadows to the surface color."),
                ref rampTintHelpExpanded,
                "Toon ramp tinting is an experimental feature that tints the darker side of the toon ramp towards the surface color. This can help make colors look less washed out on the dark side of the ramp. Works best with greyscale ramps, not recommended with colored ramps."
            );

            Vector3Property(staticToonLight, new GUIContent("Fallback light direction", "The direction the fake light should be coming from if no light or ambient lighting was detected at all."), true);
            editor.ShaderProperty(overrideWorldLightDirection, new GUIContent("Always use fallback", "Whether the fallback light direction should *always* be used."));

            // Draw the ramp masking toggle and a help box button horizontally
            ShaderPropertyWithHelp(
                rampMaskingEnabled,
                new GUIContent("Ramp Masking", "Enable the Ramp Masking feature, allowing you to use RGB masks to define up to 4 toon ramps on the same material."),
                ref rampMaskHelpExpanded,
                "Toon Ramp Masking allows up to 4 different toon ramps and toon values to be used on the same material. The color of the mask texture defines whether the Red, Green, Blue or Default values are used in that particular area.\n\n" +
                "Color an area red on the mask texture to use the R values, green for G values, and so on. Black means use the default values above."
            );

            if (rampMaskingEnabled.floatValue == 1)
            {
                DrawRampMasking();
            }

            // Draw the additive ramping toggle and a help box button horizontally
            ShaderPropertyWithHelp(
                additiveRampMode,
                new GUIContent("Additive Ramp Mode", "Which additive ramp mode to use."),
                ref additiveRampHelpExpanded,
                "- None: Use the regular toon ramps.\r\n" +
                "- Additive Only: Use the additive toon ramp for all realtime lights, except the most important realtime directional light.\r\n" +
                "- Always: Always use the additive toon ramp for all realtime lights.\r\n\r\n" +
                "The \"Additive Only\" mode better preserves toon ramp colors under mixed lights, but \"Always\" mode is more consistent."
            );

            if (additiveRampMode.floatValue != 0)
            {
                editor.TexturePropertySingleLine(new GUIContent("Additive Ramp", "The toon ramp texture to use for realtime lights."), additiveRamp);
            }

            // Light direction nudge
            Vector3PropertyWithHelp(
                lightDirectionNudge,
                new GUIContent("Light Direction Nudge"),
                ref lightDirectionNudgeHelpExpanded,
                "Nudges the light into a particular direction. Useful on toony models where lights don't look good when coming from above.\n\n" +
                "For instance, lowering the Y value will make the light come from more horizontal directions instead."
            );
        }

        private void DrawRampMasking()
        {
            editor.TexturePropertySingleLine(new GUIContent("Ramp Mask Tex    ", "A mask texture that dictates which toon ramp goes where (black, red, green, blue)."), rampMaskTex);
            EditorGUILayout.Space();

            //Red ramp
            ToonRampProperty(new GUIContent("Toon Ramp (R)", "The toon ramp texture to use on the red parts of the mask."), rampR);

            EditorGUI.indentLevel += 2;
            editor.RangeProperty(toonContrastR, "Toon Contrast (R)");
            editor.RangeProperty(toonRampOffsetR, "Toon Ramp Offset (R)");
            editor.RangeProperty(intensityR, "Intensity (R)");
            editor.RangeProperty(saturationR, "Saturation (R)");
            EditorGUI.indentLevel -= 2;

            EditorGUILayout.Space();

            //Green ramp
            ToonRampProperty(new GUIContent("Toon Ramp (G)", "The toon ramp texture to use on the green parts of the mask."), rampG);

            EditorGUI.indentLevel += 2;
            editor.RangeProperty(toonContrastG, "Toon Contrast (G)");
            editor.RangeProperty(toonRampOffsetG, "Toon Ramp Offset (G)");
            editor.RangeProperty(intensityG, "Intensity (G)");
            editor.RangeProperty(saturationG, "Saturation (G)");
            EditorGUI.indentLevel -= 2;

            EditorGUILayout.Space();

            //Blue ramp
            ToonRampProperty(new GUIContent("Toon Ramp (B)", "The toon ramp texture to use on the blue parts of the mask."), rampB);

            EditorGUI.indentLevel += 2;
            editor.RangeProperty(toonContrastB, "Toon Contrast (B)");
            editor.RangeProperty(toonRampOffsetB, "Toon Ramp Offset (B)");
            editor.RangeProperty(intensityB, "Intensity (B)");
            editor.RangeProperty(saturationB, "Saturation (B)");
            EditorGUI.indentLevel -= 2;

            EditorGUILayout.Space();
        }

        private void DrawOutlines()
        {
            outlinesExpanded = Section("Outlines", outlinesExpanded);
            if (!outlinesExpanded)
            {
                return;
            }

            editor.FloatProperty(outlineWidth, "Outline Width");
            editor.TexturePropertySingleLine(new GUIContent("Outline Texture", "The main texture (RGBA) and color tint used for the outlines. Alpha determines outline width."), outlineTex, outlineColor);
            editor.ShaderProperty(outlineAlphaWidthEnabled, new GUIContent("Alpha affects width", "Whether the outline texture's alpha should affect the outline width in that area."));
            editor.ShaderProperty(outlineScreenspaceEnabled, new GUIContent("Screenspace outlines", "Whether the outlines should be screenspace (always equally large, no matter the distance)"));

            if (outlineScreenspaceEnabled.floatValue == 1)
            {
                // Draw multi-slider for min/max distance
                float minDist = outlineScreenspaceMinDist.floatValue;
                float maxDist = outlineScreenspaceMaxDist.floatValue;
                EditorGUI.BeginChangeCheck();
                EditorGUILayout.MinMaxSlider("Size Limits (Min / Max)", ref minDist, ref maxDist, 0f, 10f);
                if (EditorGUI.EndChangeCheck())
                {
                    outlineScreenspaceMinDist.floatValue = minDist;
                    outlineScreenspaceMaxDist.floatValue = maxDist;
                }

                editor.FloatProperty(outlineScreenspaceMinDist, "Min Size");
                editor.FloatProperty(outlineScreenspaceMaxDist, "Max Size");
            }

            editor.ShaderProperty(outlineStencilComp, new GUIContent("Outline Mode", "Outer Only will only render the outlines on the outer edges of the model."));

            //Display a warning if the user attempts to use outlines to create double-sidedness.
            if (
                outlineWidth.floatValue == 0
                && outlineTex.textureValue != null && mainTex.textureValue != null
                && outlineTex.textureValue.GetInstanceID() == mainTex.textureValue.GetInstanceID())
            {
                EditorGUILayout.HelpBox("Outlines should not be used to make your model double-sided. Use the \"Sidedness\" property under Main instead.", MessageType.Warning);
            }
        }

        private void DrawMetallic()
        {
            metallicExpanded = Section("Metallic", metallicExpanded);
            if (!metallicExpanded)
            {
                return;
            }

            editor.ShaderProperty(metallicMode, new GUIContent("Metallic Mode", "Set the metallic mode for this material (None, Metallic workflow, Specular workflow)"));

            //Draw either the metallic or specular controls
            if (metallicMode.floatValue == 1)
            {
                DrawMetallicWorkflow();
            }
            else if (metallicMode.floatValue == 2)
            {
                DrawSpecularWorkflow();
            }
            else
            {
                //Draw greyed out metallic
                EditorGUI.BeginDisabledGroup(true);
                DrawMetallicWorkflow();
                EditorGUI.EndDisabledGroup();
            }
        }

        private void DrawMetallicWorkflow()
        {
            editor.TexturePropertySingleLine(new GUIContent("Metallic Map", "Defines Metallic (R) and Smoothness (A). Lower smoothness blurs reflections."), metallicMapTex);
            editor.RangeProperty(metallic, "Metallic");
            editor.RangeProperty(smoothness, "Smoothness");
        }

        private void DrawSpecularWorkflow()
        {
            editor.TexturePropertyWithHDRColor(new GUIContent("Specular Map", "Defines Specular color (RGB) and Smoothness (A). Lower smoothness blurs reflections."), metalSpecularTex, metalSpecularColor, true);
            editor.RangeProperty(smoothness, "Smoothness");
        }

        private void DrawSpecular()
        {
            specularExpanded = Section("Specular Highlights", specularExpanded);
            if (!specularExpanded)
            {
                return;
            }

            editor.ShaderProperty(specularEnabled, new GUIContent("Specular Enabled", "Enables the Specular Highlights feature."));

            EditorGUI.BeginDisabledGroup(specularEnabled.floatValue == 0);
            ShaderPropertyWithHelp(specularMode, new GUIContent(
                "Specular Mode", "Defines the specular mode that should be used."),
                ref specularModeHelpExpanded,
                "Defines the type of specular to use. Blinn-Phong shows up from wider angles than Blinn, and is slightly more realistic.\n\n" +
                "Blinn-Phong View is \"fake\" specular that is visible from any direction, similar to a matcap."
            );

            editor.TexturePropertySingleLine(new GUIContent("Specular Map", "The specular map texture to use. RGB defines color, A defines smoothness."), specularMap, specularColor);

            editor.ShaderProperty(specularToonyEnabled, new GUIContent("Toony Specular", "Enables toony specular, which looks sharper."));
            if(specularToonyEnabled.floatValue == 1)
            {
                EditorGUI.indentLevel += 2;
                editor.ShaderProperty(specularToonyCutoff, new GUIContent("Specular Cutoff", "Specularity below this value is cut off and replaced with no specular. Any specularity above this value is replaced with full specular instead, giving a toonier look."));
                EditorGUI.indentLevel -= 2;
            }

            editor.ShaderProperty(specularIndirectLightBoost, new GUIContent("Specular Indirect Light Boost", "Increases or decreases the intensity of specular highlights from the environment. Set to 0 to only receive specular highlights from realtime lights."));

            EditorGUI.EndDisabledGroup();
        }

        private void DrawMatcap()
        {
            matcapExpanded = Section("Matcap", matcapExpanded);
            if (!matcapExpanded)
            {
                return;
            }

            editor.ShaderProperty(matCapMode, new GUIContent("Matcap Mode"));

            EditorGUI.BeginDisabledGroup(matCapMode.floatValue == 0);
            editor.TexturePropertySingleLine(new GUIContent("Matcap Texture"), matCapTex);

            editor.ShaderProperty(matCapStrength, new GUIContent("Matcap Strength"));

            if (matCapMode.floatValue != 0 && matCapStrength.floatValue == 0)
            {
                EditorGUILayout.HelpBox("Matcap strength is zero, consider turning Matcap mode off for performance.", MessageType.Warning);
            }

            editor.TexturePropertySingleLine(new GUIContent("Matcap Tint", "Tint the matcap to the specified color (RGB). Alpha controls strength."), matCapTintTex, matCapColor);

            ShaderPropertyWithHelp(matCapOrigin,
                new GUIContent("Matcap origin"),
                ref matCapOriginHelpExpanded,
                "Determines the origin point for the matcap. Changing this to \"Object\" might help with artifacts when viewing from directly above/below, but will shift the matcap if the object origin moves. " +
                "Surface is generally better for character models, while Object is often better for objects and static props.\r\n\r\n" +
                "Default: Surface");

            EditorGUI.EndDisabledGroup();
        }

        private void DrawRimlight()
        {
            rimlightExpanded = Section("Rimlight", rimlightExpanded);
            if (!rimlightExpanded)
            {
                return;
            }

            editor.ShaderProperty(rimLightMode, new GUIContent("Rimlight Mode"));
            EditorGUI.BeginDisabledGroup(rimLightMode.floatValue == 0);

            editor.TexturePropertySingleLine(new GUIContent("Rimlight Texture", "The rimlight texture. RGB controls color, Alpha controls strength."), rimTex, rimLightColor);
            editor.RangeProperty(rimWidth, "Rimlight Width");

            editor.ShaderProperty(rimInvert, new GUIContent("Invert Rimlight"));

            if (rimLightMode.floatValue != 0 && rimLightColor.colorValue.a == 0)
            {
                EditorGUILayout.HelpBox("Rimlight alpha is set to 0. Consider turning rimlights off for performance.", MessageType.Warning);
            }

            if (rimLightMode.floatValue == 1 && rimWidth.floatValue == 0)
            {
                EditorGUILayout.HelpBox("Rimlight is additive but width is set to 0. Consider turning rimlights off for performance.", MessageType.Warning);
            }

            EditorGUI.EndDisabledGroup();
        }

        private void DrawVertexOffset()
        {
            vertexOffsetExpanded = Section("Vertex Offset", vertexOffsetExpanded);
            if(!vertexOffsetExpanded)
            {
                return;
            }

            ShaderPropertyWithHelp(
                vertexOffsetEnabled,
                new GUIContent("Vertex Offset"),
                ref vertexOffsetHelpExpanded,
                "Enables the Vertex Offset feature, allowing you to visually displace the model, without changing the actual position of the object."
            );

            EditorGUI.BeginDisabledGroup(vertexOffsetEnabled.floatValue == 0);

            Vector3Property(vertexOffsetPos, new GUIContent("Local Position Offset", "How much to translate the mesh by in object space."), true);
            Vector3Property(vertexOffsetRot, new GUIContent("Rotation", "How much to rotate the mesh by in object space."), true);
            Vector3Property(vertexOffsetScale, new GUIContent("Scale", "How much to scale the mesh by in object space."), true);
            Vector3Property(vertexOffsetPosWorld, new GUIContent("World Position Offset", "How much to translate the mesh by in world space."), true);

            EditorGUI.EndDisabledGroup();
        }

        private void DrawHueShift()
        {
            hueShiftExpanded = Section("Hue Shift", hueShiftExpanded);
            if (!hueShiftExpanded)
            {
                return;
            }

            editor.ShaderProperty(hueShiftEnabled, new GUIContent("Enable Hue Shift", "Enables hue shift, which shifts the color of affected areas."));

            EditorGUI.BeginDisabledGroup(hueShiftEnabled.floatValue == 0);

            editor.RangeProperty(hueShiftAmount, "Hue Shift Amount");
            editor.TexturePropertySingleLine(new GUIContent("Hue Shift Mask", "Defines a mask texture for the hue shift. White areas are fully hue shifted, black areas are not hue shifted at all."), hueShiftMaskTex);
            editor.FloatProperty(hueShiftAmountOverTime, "Hue Shift Amount Over Time");

            EditorGUI.EndDisabledGroup();
        }

        private void DrawMisc()
        {
            miscExpanded = Section("Misc", miscExpanded);
            if (!miscExpanded)
            {
                return;
            }

            editor.TexturePropertySingleLine(new GUIContent("Detail Normal Map", "An additional detail normal map."), detailNormalTex, detailNormalScale);
            editor.TextureScaleOffsetProperty(detailNormalTex);
            editor.ShaderProperty(detailNormalUvMap, new GUIContent("UV Map", "Which UV Map to use for the detail normals."));
            editor.TexturePropertySingleLine(new GUIContent("Detail Mask", "A mask texture that defines how strong the normal map should be (R)."), detailMaskTex);

            EditorGUILayout.Space();

            editor.ShaderProperty(alphaToCoverageEnabled, new GUIContent("Alpha To Coverage", "Whether to enable the Alpha To Coverage feature, also known as anti-aliased cutout."));

            if (alphaToCoverageEnabled.floatValue == 1)
            {
                EditorGUI.indentLevel += 2;
                editor.RangeProperty(alphaToCoverageCutoff, "Alpha Cutoff");
                EditorGUI.indentLevel -= 2;
                EditorGUILayout.HelpBox("When using Alpha To Coverage, ensure that the main texture has \"Mip Maps Preserve Coverage\" enabled in the import settings.", MessageType.Info);
            }

            if (HasStencils())
            {
                EditorGUILayout.Space();

                EditorGUILayout.LabelField(new GUIContent("Stencil Options"), EditorStyles.boldLabel);
                editor.ShaderProperty(stencilRef, new GUIContent("Stencil Ref Value", "The stencil reference value to use for comparisons."));
                editor.ShaderProperty(stencilPassOp, new GUIContent("Stencil Pass Op", "What to do with the reference value if the compare function is true."));
                editor.ShaderProperty(stencilFailOp, new GUIContent("Stencil Fail Op", "What to do with the reference value if the compare function is false."));
                editor.ShaderProperty(stencilZFailOp, new GUIContent("Stencil ZFail Op", "What to do with the reference value if the stencil test passes, but the Z test fails."));
                editor.ShaderProperty(stencilCompareFunction, new GUIContent("Stencil Compare Function", "The comparison function to use for stencil operations."));

                EditorGUILayout.Space();
            }

            if (HasZTest())
            {
                editor.ShaderProperty(zTest, new GUIContent("ZTest", "Depth testing function. LEqual by default, do not touch unless you know what you're doing!"));
            }

            editor.RenderQueueField();
        }

        private void DrawDebug()
        {
            debugExpanded = Section("Debug", debugExpanded);
            if (!debugExpanded)
            {
                return;
            }

            editor.ShaderProperty(debugOptionsEnabled, new GUIContent("Allow Debug Values", "If enabled, allows you to use the below debug values, such as putting a limit on the min/max brightness of lights."));

            EditorGUI.BeginDisabledGroup(debugOptionsEnabled.floatValue == 0);

            editor.ShaderProperty(debugEnabled, new GUIContent("Enable Debug Values", "Enables the below debug options (only if Allow Debug Values is enabled)."));
            editor.ShaderProperty(debugMinLightBrightness, new GUIContent("Min Light Brightness", "If debug is enabled and a light is darker than this, it will be brightened to this value."));
            editor.ShaderProperty(debugMaxLightBrightness, new GUIContent("Max Light Brightness", "If debug is enabled and a light is brighter than this, it will be darkened to this value."));
            
            editor.ShaderProperty(debugNormalsEnabled, new GUIContent("Show Normals", "Shows the surface normals as colors."));
            editor.ShaderProperty(debugUvsEnabled, new GUIContent("Visualize UV's", "Visualize the model's UV maps."));
            Vector3Property(debugUvsOffset, new GUIContent("UV Offset", "How much to offset the mesh by after visualizing the UV's."));

            EditorGUI.EndDisabledGroup();
        }

#if SHADEROPTIMIZER_INSTALLED
        private void DrawShaderOptimizer()
        {
            shaderOptimizerExpanded = Section("Shader Optimizer", shaderOptimizerExpanded);
            if (!shaderOptimizerExpanded)
            {
                return;
            }

            if (IsMaterialLocked())
            {
                if (GUILayout.Button("Unlock"))
                {
                    UnlockMaterial();
                }
                EditorGUILayout.HelpBox("Material is locked. Hit the \"Unlock\" button to unlock it and allow edits again.", MessageType.Info);
            }
            else
            {
                string propertiesToAnimate = material.GetTag("propertiesToAnimate", false, "");

                if (GUILayout.Button("Lock Material"))
                {
                    // For this event, skip the keyword stuff
                    shouldRegenerateKeywords = false;

                    // Lock all materials individually, respecting the "properties to animate" box
                    foreach(var mat in this.materials)
                    {
                        string propertiesToAnimateForThisMaterial = mat.GetTag("propertiesToAnimate", false, "");
                        if (propertiesToAnimateForThisMaterial.Trim().Length > 0)
                        {
                            LockMaterial(mat, propertiesToAnimateForThisMaterial.Trim().Split(';').ToList());
                        }
                        else
                        {
                            LockMaterial(mat);
                        }
                    }
                }

                // Check if the text field should be shown as a "mixed" value, this happens if materials have different values for the properties to animate field.
                var showMixed = false;
                foreach(var mat in this.materials)
                {
                    string propertiesToAnimateForThisMaterial = mat.GetTag("propertiesToAnimate", false, "");
                    if(propertiesToAnimate != propertiesToAnimateForThisMaterial)
                    {
                        showMixed = true;
                        break;
                    }
                }

                EditorGUILayout.HelpBox("Locking your material optimizes it, but also prevents properties from being changed or animated afterwards.\r\n\r\nTo make a property editable, type its internal name into the text field below. Multiple semicolon-separated (;) values are possible.", MessageType.Info);
                EditorGUILayout.LabelField(new GUIContent("Animatable properties"), EditorStyles.boldLabel);
                EditorGUI.BeginChangeCheck();

                EditorGUI.showMixedValue = showMixed;
                propertiesToAnimate = EditorGUILayout.TextField(propertiesToAnimate);
                EditorGUI.showMixedValue = false;

                if(EditorGUI.EndChangeCheck())
                {
                    foreach (var mat in this.materials)
                    {
                        mat.SetOverrideTag("propertiesToAnimate", propertiesToAnimate);
                    }
                }
            }
        }
#endif

        protected override void FindProperties(MaterialProperty[] props)
        {
            base.FindProperties(props);

            // Main
            mainTex = FindProperty("_MainTex");
            color = FindProperty("_Color");
            bumpMap = FindProperty("_BumpMap");
            bumpScale = FindProperty("_BumpScale");
            cutoff = FindProperty("_Cutoff");
            emissionMap = FindProperty("_EmissionMap");
            emissionColor = FindProperty("_EmissionColor");
            emissionIsTint = FindProperty("_EmissionMapIsTint");
            emissionPremultiplyEnabled = FindProperty("_EmissionPremultiply");
            cullMode = FindProperty("_Cull");
            cutoutEnabled = FindProperty("_CutoutEnabled");

            // AO
            occlusionMap = FindProperty("_OcclusionMap");
            occlusionStrength = FindProperty("_OcclusionStrength");

            // Toon lighting
            ramp = FindProperty("_Ramp");
            toonContrast = FindProperty("_ToonContrast");
            toonRampOffset = FindProperty("_ToonRampOffset");
            staticToonLight = FindProperty("_StaticToonLight");
            intensity = FindProperty("_Intensity");
            saturation = FindProperty("_Saturation");
            directLightBoost = FindProperty("_DirectLightBoost");
            indirectLightBoost = FindProperty("_IndirectLightBoost");
            rampTintingEnabled = FindProperty("_RampTinting");
            indirectLightDirMergeMin = FindProperty("_IndirectLightDirMergeMin");
            indirectLightDirMergeMax = FindProperty("_IndirectLightDirMergeMax");
            rampAntiAliasingEnabled = FindProperty("_RampAntiAliasingEnabled");
            overrideWorldLightDirection = FindProperty("_OverrideWorldLightDir");
            additiveRampMode = FindProperty("_AdditiveRampMode");
            additiveRamp = FindProperty("_AdditiveRamp");
            lightDirectionNudge = FindProperty("_LightDirectionNudge");

            // Metallic and specular (old)
            metallicMode = FindProperty("_MetallicMode");
            metallicMapTex = FindProperty("_MetallicGlossMap");
            metallic = FindProperty("_Metallic");
            smoothness = FindProperty("_Glossiness");
            metalSpecularTex = FindProperty("_SpecGlossMap");
            metalSpecularColor = FindProperty("_SpecColor");

            // New specular
            specularEnabled = FindProperty("_SpecularEnabled");
            specularMode = FindProperty("_SpecularMode");
            specularMap = FindProperty("_SpecMap");
            specularColor = FindProperty("_SpecularColor");
            specularToonyEnabled = FindProperty("_SpecularToonyEnabled");
            specularToonyCutoff = FindProperty("_SpecularToonyCutoff");
            specularIndirectLightBoost = FindProperty("_SpecularIndirectBoost");

            // Ramp masking
            rampMaskingEnabled = FindProperty("_RampMaskEnabled");
            rampMaskTex = FindProperty("_RampMaskTex");
            rampR = FindProperty("_RampR");
            toonContrastR = FindProperty("_ToonContrastR");
            toonRampOffsetR = FindProperty("_ToonRampOffsetR");
            intensityR = FindProperty("_IntensityR");
            saturationR = FindProperty("_SaturationR");
            rampG = FindProperty("_RampG");
            toonContrastG = FindProperty("_ToonContrastG");
            toonRampOffsetG = FindProperty("_ToonRampOffsetG");
            intensityG = FindProperty("_IntensityG");
            saturationG = FindProperty("_SaturationG");
            rampB = FindProperty("_RampB");
            toonContrastB = FindProperty("_ToonContrastB");
            toonRampOffsetB = FindProperty("_ToonRampOffsetB");
            intensityB = FindProperty("_IntensityB");
            saturationB = FindProperty("_SaturationB");

            // Rimlight
            rimLightMode = FindProperty("_RimLightMode");
            rimLightColor = FindProperty("_RimLightColor");
            rimTex = FindProperty("_RimTex");
            rimWidth = FindProperty("_RimWidth");
            rimInvert = FindProperty("_RimInvert");

            // Matcap
            matCapTex = FindProperty("_MatCap");
            matCapMode = FindProperty("_MatCapMode");
            matCapStrength = FindProperty("_MatCapStrength");
            matCapTintTex = FindProperty("_MatCapTintTex");
            matCapColor = FindProperty("_MatCapColor");
            matCapOrigin = FindProperty("_MatCapOrigin");

            // Outlines
            outlineWidth = FindProperty("_OutlineWidth", false);
            outlineColor = FindProperty("_OutlineColor", false);
            outlineTex = FindProperty("_OutlineTex", false);
            outlineStencilComp = FindProperty("_OutlineStencilComp", false);
            outlineAlphaWidthEnabled = FindProperty("_OutlineAlphaWidthEnabled", false);
            outlineScreenspaceEnabled = FindProperty("_ScreenSpaceOutline", false);
            outlineScreenspaceMinDist = FindProperty("_ScreenSpaceMinDist", false);
            outlineScreenspaceMaxDist = FindProperty("_ScreenSpaceMaxDist", false);

            // A2C
            alphaToCoverageEnabled = FindProperty("_AlphaToCoverage");
            alphaToCoverageCutoff = FindProperty("_AlphaToCoverageCutoff");

            // Detail normal
            detailNormalTex = FindProperty("_DetailNormalMap");
            detailNormalScale = FindProperty("_DetailNormalMapScale");
            detailNormalUvMap = FindProperty("_UVSec");
            detailMaskTex = FindProperty("_DetailMask");

            // Stencils
            stencilRef = FindProperty("_StencilRef", false);
            stencilPassOp = FindProperty("_StencilPassOp", false);
            stencilFailOp = FindProperty("_StencilFailOp", false);
            stencilZFailOp = FindProperty("_StencilZFailOp", false);
            stencilCompareFunction = FindProperty("_StencilCompareFunction", false);

            // ZTest
            zTest = FindProperty("_ZTest", false);

            // Vertex offset
            vertexOffsetEnabled = FindProperty("_VertexOffsetEnabled");
            vertexOffsetPos = FindProperty("_VertexOffsetPos");
            vertexOffsetRot = FindProperty("_VertexOffsetRot");
            vertexOffsetScale = FindProperty("_VertexOffsetScale");
            vertexOffsetPosWorld = FindProperty("_VertexOffsetPosWorld");

            // Hue shift
            hueShiftEnabled = FindProperty("_HueShiftEnabled");
            hueShiftAmount = FindProperty("_HueShiftAmount");
            hueShiftMaskTex = FindProperty("_HueShiftMask");
            hueShiftAmountOverTime = FindProperty("_HueShiftAmountOverTime");

            debugOptionsEnabled = FindProperty("_DebugOptionsEnabled");
            debugEnabled = FindProperty("_DebugEnabled");
            debugMinLightBrightness = FindProperty("_DebugMinLightBrightness");
            debugMaxLightBrightness = FindProperty("_DebugMaxLightBrightness");
            debugNormalsEnabled = FindProperty("_DebugNormals");
            debugUvsEnabled = FindProperty("_DebugUVs");
            debugUvsOffset = FindProperty("_DebugUVsOffset");

            // Internal properties
            renderMode = FindProperty("_Mode");
            srcBlend = FindProperty("_SrcBlend");
            dstBlend = FindProperty("_DstBlend");
            zWrite = FindProperty("_ZWrite");
            outlineStencilWriteAction = FindProperty("_StencilWriteAction", false);
        }

        private bool HasOutlines()
        {
            return outlineWidth != null;
        }

        private bool HasStencils()
        {
            return stencilPassOp != null;
        }

        private bool HasZTest()
        {
            return zTest != null;
        }

        private bool HasVertexOffset()
        {
            return vertexOffsetPos != null;
        }

        protected virtual void SetupKeywords()
        {
            if(!shouldRegenerateKeywords)
            {
                return;
            }

            foreach (var mat in this.materials)
            {
                // Delete all keywords first
                mat.shaderKeywords = new string[] { };

                // Don't add new keywords on locked in shaders
                if (IsMaterialLocked(mat))
                {
                    continue;
                }

                // Transparency keyword
                if (mat.GetFloat("_Mode") == 2)
                {
                    mat.EnableKeyword("_ALPHABLEND_ON");
                }
                else if (mat.GetFloat("_Mode") == 3)
                {
                    mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                }

                // Add normal map keyword if used.
                if (mat.GetTexture("_BumpMap") != null)
                {
                    mat.EnableKeyword("_NORMALMAP");
                }

                // Add cutout keyword if used
                if (mat.GetFloat("_CutoutEnabled") == 1)
                {
                    mat.EnableKeyword("_ALPHATEST_ON");
                }

                // Add ramp tinting keyword
                if (mat.GetFloat("_RampTinting") == 1)
                {
                    mat.EnableKeyword("_RAMPTINT_ON");
                }

                // Add ramp masking keyword
                if (mat.GetFloat("_RampMaskEnabled") == 1)
                {
                    mat.EnableKeyword("_RAMPMASK_ON");
                }

                // Ramp anti-aliasing keyword
                if (mat.GetFloat("_RampAntiAliasingEnabled") == 1)
                {
                    mat.EnableKeyword("_RAMPANTIALIASING_ON");
                }

                // Override world light dir keyword
                if (mat.GetFloat("_OverrideWorldLightDir") == 1)
                {
                    mat.EnableKeyword("_OVERRIDEWORLDLIGHTDIR_ON");
                }

                // Additive Ramping keyword
                if (mat.GetFloat("_AdditiveRampMode") == 1)
                {
                    mat.EnableKeyword("_ADDITIVERAMP_FORWARDADD_ONLY");
                }
                if (mat.GetFloat("_AdditiveRampMode") == 2)
                {
                    mat.EnableKeyword("_ADDITIVERAMP_ALWAYS");
                }

                // Add Metallic or Specular keyword if used.
                if (mat.GetFloat("_MetallicMode") == 1)
                {
                    mat.EnableKeyword("_METALLICGLOSSMAP");
                }
                if (mat.GetFloat("_MetallicMode") == 2)
                {
                    mat.EnableKeyword("_SPECGLOSSMAP");
                }

                // Specular highlights keyword
                if(mat.GetFloat("_SpecularEnabled") == 1)
                {
                    mat.EnableKeyword("_SPECULAR_ON");
                }

                // Emission keyword
                // Emission is automatically enabled when the emission tint is set to anything other than black. Alpha is ignored for the comparison.
                Color emissionCol = mat.GetColor("_EmissionColor");
                if (new Color(emissionCol.r, emissionCol.g, emissionCol.b, 1) != Color.black)
                {
                    mat.EnableKeyword("_EMISSION");
                }

                // Matcap keyword
                if (mat.GetFloat("_MatCapMode") != 0)
                {
                    mat.EnableKeyword("_MATCAP_ON");
                }

                // Matcap tint texture keyword
                if (matCapTintTex.textureValue != null && mat.GetFloat("_MatCapMode") != 0)
                {
                    mat.EnableKeyword("_MATCAPTINTTEX_ON");
                }

                // Outline alpha width keyword
                if (HasOutlines() && mat.GetFloat("_OutlineAlphaWidthEnabled") == 1)
                {
                    mat.EnableKeyword("_OUTLINE_ALPHA_WIDTH_ON");
                }

                // Screenspace outline keyword
                if (HasOutlines() && mat.GetFloat("_ScreenSpaceOutline") == 1)
                {
                    mat.EnableKeyword("_OUTLINE_SCREENSPACE");
                }

                // Rimlight keyword
                if (mat.GetFloat("_RimLightMode") == 1)
                {
                    mat.EnableKeyword("_RIMLIGHT_ADD");
                }
                else if (mat.GetFloat("_RimLightMode") == 2)
                {
                    mat.EnableKeyword("_RIMLIGHT_MIX");
                }

                // A2C keyword
                if (mat.GetFloat("_AlphaToCoverage") == 1)
                {
                    mat.EnableKeyword("_ALPHATOCOVERAGE_ON");
                }

                // Detail normals keywords
                if (mat.GetTexture("_DetailNormalMap") != null)
                {
                    if (mat.GetFloat("_UVSec") == 0)
                    {
                        mat.EnableKeyword("_DETAILNORMAL_UV0");
                    }
                    else
                    {
                        mat.EnableKeyword("_DETAILNORMAL_UV1");
                    }
                }

                // Vertex offset
                if (mat.GetFloat("_VertexOffsetEnabled") == 1)
                {
                    mat.EnableKeyword("_VERTEXOFFSET_ON");
                }

                // Hue shift
                if (mat.GetFloat("_HueShiftEnabled") == 1)
                {
                    mat.EnableKeyword("_HUESHIFT_ON");
                }

                // AO
                if(mat.GetTexture("_OcclusionMap") != null)
                {
                    mat.EnableKeyword("_OCCLUSION_ON");
                }

                // Debug options
                if(mat.GetFloat("_DebugOptionsEnabled") == 1)
                {
                    mat.EnableKeyword("_DEBUG_ON");
                }
            }
        }

        /// <summary>
        /// Sets up material blend modes and blending/alphatest keywords, render queues and override tags.
        /// By default, the currently selected materials are used.
        /// This overload is preferred, but cannot be used with AssignNewShaderToMaterial().
        /// </summary>
        private void SetupBlendModes()
        {
            foreach (var mat in this.materials)
            {
                SetupBlendModes(mat);
            }
        }

        /// <summary>
        /// Sets up material blend modes and blending/alphatest keywords, render queues and override tags.
        /// </summary>
        /// <param name="material">The material to set up.</param>
        private void SetupBlendModes(Material material)
        {
            // Check if the render type is fade/transparent or not
            if (material.GetInt("_Mode") == 2)
            {
                // Set up fade blend modes
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                // Enable alpha blend keyword
                material.EnableKeyword("_ALPHABLEND_ON");

                // Set queue and rendertype
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                material.SetOverrideTag("RenderType", "Transparent");
            }
            else if (material.GetInt("_Mode") == 3)
            {
                // Set up transparent blend modes
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

                // Enable alpha premultiply keyword
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");

                // Set queue and rendertype
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                material.SetOverrideTag("RenderType", "Transparent");
            }
            else
            {
                // Set up opaque blend modes (no blending)
                material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);

                // Disable alpha blend/premultiply keywords
                material.DisableKeyword("_ALPHABLEND_ON");
                material.DisableKeyword("_ALPHAPREMULTIPLY_ON");

                // If Cutout is enabled, set the queue to AlphaTest and change the rendertype
                // Otherwise, set the queue and rendertype back to default.
                if (material.GetInt("_CutoutEnabled") == 1)
                {
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                    material.SetOverrideTag("RenderType", "TransparentCutout");
                }
                else
                {
                    material.renderQueue = -1;
                    material.SetOverrideTag("RenderType", "");
                }
            }
        }

        private void SetupOutlineStencilValues()
        {
            // Check if outer only outline mode is enabled
            if (outlineStencilComp.floatValue == (int)UnityEngine.Rendering.CompareFunction.NotEqual)
            {
                // If so, tell ForwardBase pass to also write to the stencil buffer
                this.outlineStencilWriteAction.floatValue = (int)UnityEngine.Rendering.StencilOp.Replace;
            }
            else
            {
                // Otherwise, tell the ForwardBase pass to *not* write stencils.
                this.outlineStencilWriteAction.floatValue = (int)UnityEngine.Rendering.StencilOp.Keep;
            }
        }

        protected override void ToonRampProperty(GUIContent guiContent, MaterialProperty rampProperty)
        {
            base.ToonRampProperty(guiContent, rampProperty);

            // Display warning if ramp anti-aliasing is enabled but the toon ramp filter mode is Point.
            if (rampAntiAliasingEnabled.floatValue == 1 && TextureIsPointFiltered(rampProperty))
            {
                EditorGUILayout.HelpBox("Ramp anti-aliasing is enabled, but the filtering mode of this ramp texture is set to Point. This feature only works with Bilinear/Trilinear.", MessageType.Warning);
            }
        }
    }
}
