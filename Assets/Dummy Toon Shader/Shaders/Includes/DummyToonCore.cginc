#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "AutoLight.cginc"
#include "UnityPBSLighting.cginc"

float _Cutoff;

#if defined(_ALPHATOCOVERAGE_ON)
	float _AlphaToCoverageCutoff;
#endif

#if defined(_NORMALMAP)
	sampler2D _BumpMap;
	float4 _BumpMap_ST;
	float _BumpScale;
#endif

#if defined(_DETAILNORMAL_UV0) || defined(_DETAILNORMAL_UV1)
	#define DETAILNORMALMAP

	sampler2D _DetailNormalMap;
	float4 _DetailNormalMap_ST;
	float _DetailNormalMapScale;
	sampler2D _DetailMask;
#endif

sampler2D _Ramp;
float4 _Ramp_TexelSize;
float _ToonContrast;
float _ToonRampOffset;
float3 _StaticToonLight;

float _Intensity;
float _Saturation;

float _DirectLightBoost;
float _IndirectLightBoost;

float _IndirectLightDirMergeMin;
float _IndirectLightDirMergeMax;

float3 _LightDirectionNudge;

#if defined(_RAMPMASK_ON)
	sampler2D _RampMaskTex;

	sampler2D _RampR;
	float4 _RampR_TexelSize;
	float _ToonContrastR;
	float _ToonRampOffsetR;
	float _IntensityR;
	float _SaturationR;
	
	sampler2D _RampG;
	float4 _RampG_TexelSize;
	float _ToonContrastG;
	float _ToonRampOffsetG;
	float _IntensityG;
	float _SaturationG;
	
	sampler2D _RampB;
	float4 _RampB_TexelSize;
	float _ToonContrastB;
	float _ToonRampOffsetB;
	float _IntensityB;
	float _SaturationB;
#endif

#if defined(_ADDITIVERAMP_FORWARDADD_ONLY) || defined(_ADDITIVERAMP_ALWAYS)
	sampler2D _AdditiveRamp;
	float4 _AdditiveRamp_TexelSize;
#endif

#if defined(_METALLICGLOSSMAP)
	sampler2D _MetallicGlossMap;
#endif

float _Metallic;
float _Glossiness;

#if defined(_SPECGLOSSMAP)
	// _SpecColor is already defined somewhere
	//float4 _SpecColor;
	sampler2D _SpecGlossMap;
#endif

#if defined(_EMISSION)
	sampler2D _EmissionMap;
	float4 _EmissionColor;

	float _EmissionMapIsTint;
	float _EmissionPremultiply;
#endif

#if defined(_MATCAP_ON)
	float _MatCapMode;

	sampler2D _MatCap;
	float _MatCapStrength;

	float4 _MatCapColor;
	#if defined(_MATCAPTINTTEX_ON)
		sampler2D _MatCapTintTex;
	#endif
	float _MatCapOrigin;
#endif

#if defined(_RIMLIGHT_ADD) || defined(_RIMLIGHT_MIX)
	sampler2D _RimTex;
	float4 _RimLightColor;
	float _RimLightMode;
	float _RimWidth;
	float _RimInvert;
#endif

#ifdef OUTLINE_PASS
	uniform sampler2D _OutlineTex;
	uniform float4 _OutlineTex_ST;
	uniform float _OutlineWidth;
	uniform float4 _OutlineColor;

	uniform float _ScreenSpaceMinDist;
	uniform float _ScreenSpaceMaxDist;
#else
	sampler2D _MainTex;
	float4 _Color;
#endif

#if defined(_VERTEXOFFSET_ON)
	float3 _VertexOffsetPos;
	float3 _VertexOffsetRot;
	float3 _VertexOffsetScale;
	float3 _VertexOffsetPosWorld;
#endif

#if defined(_OCCLUSION_ON)
	sampler2D _OcclusionMap;
	float _OcclusionStrength;
#endif

// Most textures reuse the tiling and offset values of the main texture, so this should always be available
float4 _MainTex_ST;

struct appdata
{
	float4 vertex : POSITION;
	float3 normal : NORMAL;
	#if defined(_NORMALMAP) || defined(DETAILNORMALMAP)
		float4 tangent : TANGENT;
	#endif
	float2 uv : TEXCOORD0;
	#if defined(_DETAILNORMAL_UV1)
		float2 uv1 : TEXCOORD1;
	#endif
};

struct v2f
{
	#if defined(_DETAILNORMAL_UV1)
		float4 uv : TEXCOORD0; // Pack UV0 and UV1 into the same interpolator, if UV1 exists
	#else
		float2 uv : TEXCOORD0;
	#endif
	float4 pos : SV_POSITION;
	float3 normalDir : TEXCOORD1;
	#if defined(_NORMALMAP) || defined(DETAILNORMALMAP)
		float3 tangentDir : TEXCOORD2;
		float3 bitangentDir : TEXCOORD3;
	#endif
	float4 worldPos : TEXCOORD4;
	SHADOW_COORDS(5)
	UNITY_FOG_COORDS(6)
	#ifndef LIMITED_INTERPOLATORS
		float4 objWorldPos : TEXCOORD7;
	#endif
};

#if defined(_DEBUG_ON)
	#include "DummyToonDebug.cginc"
#endif

#include "DummyToonLighting.cginc"
#include "DummyToonRamping.cginc"

#if defined(_METALLICGLOSSMAP) || defined(_SPECGLOSSMAP)
	#include "DummyToonMetallicSpecular.cginc"
#endif

#if defined(_SPECULAR_ON)
	#include "DummyToonSpecular.cginc"
#endif

#if defined(_MATCAP_ON)
	#include "DummyToonMatcap.cginc"
#endif

#if defined(_RIMLIGHT_ADD) || defined(_RIMLIGHT_MIX)
	#include "DummyToonRimlight.cginc"
#endif

#if defined(_VERTEXOFFSET_ON)
	#include "DummyToonVertexOffset.cginc"
#endif

#if defined(_HUESHIFT_ON)
	#include "DummyToonHueShift.cginc"
#endif

float3 NormalDirection(v2f i)
{
	float3 normalDir = normalize(i.normalDir);
	
	// Perturb normals with a normal map
	#if defined(_NORMALMAP) && defined(DETAILNORMALMAP) // Both normal and detail normal
		float3x3 tangentTransform = float3x3(i.tangentDir, i.bitangentDir, i.normalDir);

		// Get detail mask
		float4 detailMaskTex = tex2D(_DetailMask, i.uv.xy);
		
		float3 bumpTex = UnpackScaleNormal(tex2D(_BumpMap, i.uv.xy), _BumpScale);
		
		// Choose the correct UV map set
		#if defined(_DETAILNORMAL_UV0)
			// Sample the detail normal using UV0, and re-apply the tiling. This may result in stacked tiling if the main texture is also transformed.
			float3 detailBumpTex = UnpackScaleNormal(tex2D(_DetailNormalMap,TRANSFORM_TEX(i.uv.xy, _DetailNormalMap)), _DetailNormalMapScale * detailMaskTex.r);
		#else
			// Sample the detail normal with UV1
			float3 detailBumpTex = UnpackScaleNormal(tex2D(_DetailNormalMap, i.uv.zw), _DetailNormalMapScale * detailMaskTex.r);
		#endif
		
		float3 normalLocal = BlendNormals(bumpTex, detailBumpTex);
		normalDir = normalize(mul(normalLocal, tangentTransform));  
	#elif defined(_NORMALMAP) // Only normal
		float3x3 tangentTransform = float3x3(i.tangentDir, i.bitangentDir, i.normalDir);
		float3 bumpTex = UnpackScaleNormal(tex2D(_BumpMap, i.uv.xy), _BumpScale);
		float3 normalLocal = bumpTex.rgb;
		normalDir = normalize(mul(normalLocal, tangentTransform));  
	#elif defined(DETAILNORMALMAP) // Only detail normal
		float3x3 tangentTransform = float3x3(i.tangentDir, i.bitangentDir, i.normalDir);

		// Get detail mask
		float4 detailMaskTex = tex2D(_DetailMask, i.uv.xy);
		
		// Choose the correct UV map set
		#if defined(_DETAILNORMAL_UV0)
			// Sample the detail normal, and re-apply the tiling. This may result in stacked tiling if the main texture is also transformed.
			float3 bumpTex = UnpackScaleNormal(tex2D(_DetailNormalMap,TRANSFORM_TEX(i.uv.xy, _DetailNormalMap)), _DetailNormalMapScale * detailMaskTex.r);
		#else
			float3 bumpTex = UnpackScaleNormal(tex2D(_DetailNormalMap, i.uv.zw), _DetailNormalMapScale * detailMaskTex.r);
		#endif
		
		float3 normalLocal = bumpTex.rgb;
		normalDir = normalize(mul(normalLocal, tangentTransform));  
	#endif
	
	return normalDir;
}

v2f vert (appdata v)
{
	// If vertex offset is enabled, apply that first
	#if defined(_VERTEXOFFSET_ON)
		VertexOffset(v);
	#endif

	v2f o;

	#if defined(_DEBUG_ON)
		if(_DebugEnabled == 1)
		{
			v.vertex = DebugUVs(v.uv, v.vertex);
		}
	#endif

	o.pos = UnityObjectToClipPos(v.vertex);
	// If UV1 is used, pack UV0 and UV1 into a single float4
	#if defined(_DETAILNORMAL_UV1)
		float2 uv0 = TRANSFORM_TEX(v.uv, _MainTex);
		float2 uv1 = TRANSFORM_TEX(v.uv1, _DetailNormalMap);
		o.uv = float4(uv0, uv1);
	#else
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
	#endif
	o.normalDir = UnityObjectToWorldNormal(v.normal);

	// If we're not using normal maps or detail normal maps, we don't need the tangent or bitangent at all, saving us 2 interpolators.
	#if defined(_NORMALMAP) || defined(DETAILNORMALMAP)
		o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
		o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
	#endif

	o.worldPos = mul(unity_ObjectToWorld, v.vertex);

	#ifndef LIMITED_INTERPOLATORS
		o.objWorldPos = mul(unity_ObjectToWorld, float4(0,0,0,1));
	#endif
	
	TRANSFER_SHADOW(o);

	UNITY_TRANSFER_FOG(o, o.pos);
	
	return o;
}

// Use SV_IsFrontFace semantic in shader model 5.0, this is not available in 2.0 so is not used
#ifdef NO_ISFRONTFACE
float4 frag (v2f i) : SV_Target
#else
float4 frag (v2f i, bool facing : SV_IsFrontFace) : SV_Target
#endif
{
	float3 viewDir = normalize(_WorldSpaceCameraPos.xyz - i.worldPos.xyz);

	// Sample main texture
	#ifdef OUTLINE_PASS
		float4 mainTex = tex2D(_OutlineTex, i.uv.xy);
		mainTex *= _OutlineColor;
	#else
		float4 mainTex = tex2D(_MainTex, i.uv.xy);
		mainTex *= _Color;
	#endif
	
	// Alpha to coverage
	#if defined(_ALPHATOCOVERAGE_ON) && !defined(NO_DERIVATIVES)
		mainTex.a = (mainTex.a - _AlphaToCoverageCutoff) / max(fwidth(mainTex.a), 0.00001) + 0.5;
	#endif
	
	// Cutout
	#if defined(_ALPHATEST_ON)
		clip(mainTex.a - _Cutoff);
	#endif
	
	// Hue shift
	#if defined(_HUESHIFT_ON)
		ApplyHueShift(i.uv.xy, mainTex.rgb);
	#endif

	// Get all vars related to toon ramping
	float IntensityVar;
	float SaturationVar;
	float ToonContrastVar;
	float ToonRampOffsetVar;
	float4 ToonRampMaskColor;
	GetToonVars(i.uv.xy, IntensityVar, SaturationVar, ToonContrastVar, ToonRampOffsetVar, ToonRampMaskColor);
	
	// Obtain albedo from main texture and multiply by intensity
	float3 albedo = mainTex.rgb * IntensityVar;

	// Get the difference to subtract later.
	// This essentially fades out the albedo without fading out other effects that rely on albedo (such as matcap or metallics)
	#if defined(_ALPHAPREMULTIPLY_ON)
		float3 premultiplyDifference = albedo - (albedo * mainTex.a);	
	#endif
	
	// Apply saturation modifier
	float lum = Luminance(albedo);
	albedo = lerp(float3(lum, lum, lum), albedo, SaturationVar);

	// Get normal direction from vertex normals (and normal maps if applicable)
	float3 normalDir = NormalDirection(i);

	// If this is a backface, reverse the normal direction
	#ifndef NO_ISFRONTFACE
		float faceSign = facing ? 1 : -1;
		normalDir *= faceSign;
	#endif
	
	// Matcap
	#if defined(_MATCAP_ON)
		// Matcap origin
		// If 0, viewdir to surface is used
		// If 1, viewdir to object center is used
		// NOTE: if interpolators are limited (shader model 2.0), objWorldPos is not available, so the surface viewdir is used.
		#ifdef LIMITED_INTERPOLATORS
			float3 matcapViewDir = viewDir;
		#else
			float3 matcapViewDir;
			if (_MatCapOrigin == 1)
			{
				matcapViewDir = normalize(_WorldSpaceCameraPos.xyz - i.objWorldPos.xyz);
			}
			else
			{
				matcapViewDir = viewDir;
			}
		#endif
		Matcap(matcapViewDir, normalDir, i.uv.xy, albedo);
	#endif
	
	// Rimlight
	#if defined(_RIMLIGHT_ADD) || defined(_RIMLIGHT_MIX)
		Rimlight(i.uv.xy, viewDir, normalDir, albedo);
	#endif

	#if defined(_ALPHAPREMULTIPLY_ON)
		float3 originalAlbedo = albedo;
		albedo -= premultiplyDifference;
	#endif
	
	// Lighting

	// If enabled, sample AO map in ForwardBase to handle AO
	#if defined(UNITY_PASS_FORWARDBASE) && defined(_OCCLUSION_ON)
		float4 occlusionTex = tex2D(_OcclusionMap, i.uv.xy);
		float occlusionStrength = (1 - occlusionTex.r) * _OcclusionStrength;
	#else
		float occlusionStrength = 0;
	#endif
	
	// Get light attenuation
	UNITY_LIGHT_ATTENUATION(attenuation, i, i.worldPos.xyz);
	
	float3 lightDirection;
	float3 lightColor;

	float3 specularColor = float3(0,0,0);
	
	// Fill the finalcolor with indirect light data (SH and vertex lights)
	float3 finalColor = IndirectToonLighting(albedo, normalDir, i.worldPos.xyz, occlusionStrength);
	
	#ifdef UNITY_PASS_FORWARDBASE
		// Run the lighting function with non-realtime data first       
		GetBaseLightData(lightDirection, lightColor);
		
		// If the ambient light direction is too close to the actual realtime directional light direction (happens with mixed lights),
		// the direction will be smoothly merged.
		// This makes the lighting look better with sharp toon ramps.
		#if !defined(_OVERRIDEWORLDLIGHTDIR_ON)
			if(any(_WorldSpaceLightPos0))
			{
				SmoothBaseLightData(lightDirection);
			}
		#endif

		// Since this is actually a "fake" environmental light, dim it based on the AO strength.
		#if defined(_OCCLUSION_ON)
			finalColor += ToonLightingBase(albedo, normalDir, lightDirection, lightColor, ToonRampMaskColor, ToonContrastVar, ToonRampOffsetVar) * _IndirectLightBoost * (1 - occlusionStrength);
		#else
			finalColor += ToonLightingBase(albedo, normalDir, lightDirection, lightColor, ToonRampMaskColor, ToonContrastVar, ToonRampOffsetVar) * _IndirectLightBoost;
		#endif
		// Apply specular highlights for the base light
		#if defined(_SPECULAR_ON)
			specularColor += Specular(albedo, lightDirection, lightColor, normalDir, viewDir, 1, i.uv.xy, occlusionStrength) * _SpecularIndirectBoost;
		#endif
	#endif

	// Fill lightDirection and lightColor with current light data
	GetLightData(i.worldPos.xyz, lightDirection, lightColor);
	
	// Apply current light
	// If the current light is black, it will have no effect. Skip it to save on calculations and texture samples.
	#ifdef UNITY_PASS_FORWARDBASE
		UNITY_BRANCH
		if(any(_LightColor0.rgb))
		{
			finalColor += ToonLighting(albedo, normalDir, lightDirection, lightColor, ToonRampMaskColor, ToonContrastVar, ToonRampOffsetVar) * attenuation * _DirectLightBoost;
			// Apply specular highlights again for the realtime light
			#if defined(_SPECULAR_ON)
				specularColor += Specular(albedo, lightDirection, lightColor, normalDir, viewDir, attenuation, i.uv.xy, 0);
			#endif
		}
	#else
		finalColor += ToonLighting(albedo, normalDir, lightDirection, lightColor, ToonRampMaskColor, ToonContrastVar, ToonRampOffsetVar) * attenuation * _DirectLightBoost;
		#if defined(_SPECULAR_ON)
			specularColor += Specular(albedo, lightDirection, lightColor, normalDir, viewDir, attenuation, i.uv.xy, 0);
		#endif
	#endif
	
	// Apply metallic
	#if defined(_METALLICGLOSSMAP) || defined(_SPECGLOSSMAP)
		#if defined(_ALPHAPREMULTIPLY_ON)
			MetallicSpecularGloss(i.worldPos.xyz, i.uv.xy, normalDir, originalAlbedo, finalColor);
		#else
			MetallicSpecularGloss(i.worldPos.xyz, i.uv.xy, normalDir, albedo, finalColor);
		#endif
	#endif

	// After the metallic, apply the specular highlights
	#if defined(_SPECULAR_ON)
		finalColor += specularColor;
	#endif
	
	// Apply emission
	#if defined(UNITY_PASS_FORWARDBASE) && defined(_EMISSION)
		float4 emissive = tex2D(_EmissionMap, i.uv.xy);
		emissive *= _EmissionColor;
		
		UNITY_BRANCH
		if(_EmissionMapIsTint == 1)
		{
			emissive.rgb *= mainTex.rgb;
		}
		
		// When using premultiplied transparency, optionally premultiply the emission color too.
		#if defined(_ALPHAPREMULTIPLY_ON)
			if(_EmissionPremultiply == 1)
			{
				finalColor += emissive.rgb * mainTex.a;
			}
			else
			{
				finalColor += emissive.rgb;
			}
		#else
			finalColor += emissive.rgb;
		#endif
	#endif
	
	#if defined(_ALPHABLEND_ON) || defined(_ALPHATOCOVERAGE_ON) || defined(_ALPHAPREMULTIPLY_ON)
		float finalAlpha = mainTex.a;
	#else
		float finalAlpha = 1;
	#endif

	float4 finalOutput = float4(finalColor, finalAlpha);

	UNITY_APPLY_FOG(i.fogCoord, finalOutput);

	#if defined(_DEBUG_ON)
		if(_DebugEnabled == 1 && _DebugNormals == 1)
		{
			float3 normalColor = DebugNormalColor(normalDir);
			return float4(normalColor, 1);
		}
	#endif

	return finalOutput;
}