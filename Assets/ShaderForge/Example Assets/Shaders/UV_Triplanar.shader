// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32253,y:32069,varname:node_4013,prsc:2|diff-7984-OUT;n:type:ShaderForge.SFN_Color,id:1304,x:31858,y:31931,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1304,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:7984,x:32062,y:32069,varname:node_7984,prsc:2|A-1304-RGB,B-7513-OUT;n:type:ShaderForge.SFN_Code,id:7513,x:31317,y:32095,varname:node_7513,prsc:2,code:ZgBsAG8AYQB0ADMAIABwAG8AaQBuAHQATwBuAFUAbgBpAHQAUwBwAGgAZQByAGUAIAA9ACAAZgBsAG8AYQB0ADMAKAAxACwAMAAsADAAKQA7AA0ACgBmAGwAbwBhAHQAIABwAGkAIAA9ACAAMwAuADEANAAxADUAOQAyADYANQAzADUAOAA5ADcAOQAzADIAMwA4ADQANgAyADYANAAzADMAOAAzADIANwA5ADUAOwANAAoADQAKAC8ALwBnAGUAdAAgAHQAaABlACAAdgBlAHIAaQBjAGEAbAAgAHAAbwBzAGkAdABpAG8AbgANAAoAZgBsAG8AYQB0ACAAdgBlAHIAdABpAGMAYQBsAFQAaABlAHQAYQAgAD0AIAAuADUAIAAqACAAcABpACAAKgAgACgALQAxACAAKwAgACgAVQBWAC4AeQAgACoAIAAyACkAIAApADsADQAKAHAAbwBpAG4AdABPAG4AVQBuAGkAdABTAHAAaABlAHIAZQAuAHoAIAA9ACAAcwBpAG4AKAB2AGUAcgB0AGkAYwBhAGwAVABoAGUAdABhACkAOwANAAoACgAvAC8AZwBlAHQAIAB0AGgAZQAgAHIAYQBkAGkAdQBzACAAZgBvAHIAIAB0AGgAZQAgAGgAbwByAGkAegBvAG4AdABhAGwAIABjAHIAbwBzAHMAIABzAGUAYwB0AGkAbwBuAAoAZgBsAG8AYQB0ACAAeAB6AFIAYQBkAGkAdQBzACAAPQAgAGEAYgBzACgAYwBvAHMAKAB2AGUAcgB0AGkAYwBhAGwAVABoAGUAdABhACkAKQA7AAoACgAvAC8AZwBlAHQAIAB0AGgAZQAgAHAAbwBzAGkAdABpAG8AbgAgAGEAcgBvAHUAbgBkACAAYQAgAHQAaABlACAAYwBpAHIAYwBsAGUAIABjAHIAbwBzAHMAIABzAGUAYwB0AGkAbwBuAAoAZgBsAG8AYQB0ACAAaABvAHIAaQB6AG8AbgB0AGEAbABUAGgAZQB0AGEAIAA9ACAAVQBWAC4AeAAgACoAIAAyACAAKgAgAHAAaQA7AA0ACgBwAG8AaQBuAHQATwBuAFUAbgBpAHQAUwBwAGgAZQByAGUALgB5ACAAPQAgAGMAbwBzACgAaABvAHIAaQB6AG8AbgB0AGEAbABUAGgAZQB0AGEAKQAgACoAIAB4AHoAUgBhAGQAaQB1AHMAOwANAAoAcABvAGkAbgB0AE8AbgBVAG4AaQB0AFMAcABoAGUAcgBlAC4AeAAgAD0AIABzAGkAbgAoAGgAbwByAGkAegBvAG4AdABhAGwAVABoAGUAdABhACkAIAAqACAAeAB6AFIAYQBkAGkAdQBzADsADQAKAA0ACgAvAC8AZgBlAGUAbAAgAGYAcgBlAGUAIAB0AG8AIAByAGUAbQBvAHYAZQAgAHQAaABlACAAZABlAHQAYQBpAGwAIABzAGUAdAB0AGkAbgBnACAAaQBmACAAZABlAHMAaQByAGUAZAAgAGYAbwByACAAcABlAHIAZgBvAHIAbQBhAG4AYwBlACAAKABwAG8AdwAgAGkAcwAgAGUAeABwAGUAbgBzAGkAdgBlACkADQAKAC8ALwBmAGwAbwBhAHQAMwAgAGwAbwBjAGEAbABOAG8AcgBtAGEAbAAgAD0AIABhAGIAcwAoAHAAbwBpAG4AdABPAG4AVQBuAGkAdABTAHAAaABlAHIAZQAuAHgAeQB6ACkAOwANAAoADQAKAGYAbABvAGEAdAAzACAAbABvAGMAYQBsAE4AbwByAG0AYQBsACAAPQAgAHAAbwB3ACgAYQBiAHMAKABwAG8AaQBuAHQATwBuAFUAbgBpAHQAUwBwAGgAZQByAGUALgB4AHkAegApACwARABlAHQAYQBpAGwAKQA7AA0ADQAKAA0ACgBmAGwAbwBhAHQAMwAgAGwAbwBjAGEAbABQAG8AcwBpAHQAaQBvAG4AIAA9ACAAcABvAGkAbgB0AE8AbgBVAG4AaQB0AFMAcABoAGUAcgBlAC4AcgBnAGIALwBTAGMAYQBsAGUAOwANAAoAZgBsAG8AYQB0ADQAIAB0AGUAeAAxACAAPQAgAHQAZQB4ADIARAAoAFQAZQB4ACwAbABvAGMAYQBsAFAAbwBzAGkAdABpAG8AbgAuAHIAZwApADsADQAKAGYAbABvAGEAdAA0ACAAdABlAHgAMgAgAD0AIAB0AGUAeAAyAEQAKABUAGUAeAAsAGwAbwBjAGEAbABQAG8AcwBpAHQAaQBvAG4ALgByAGIAKQA7AA0ACgBmAGwAbwBhAHQANAAgAHQAZQB4ADMAIAA9ACAAdABlAHgAMgBEACgAVABlAHgALABsAG8AYwBhAGwAUABvAHMAaQB0AGkAbwBuAC4AZwBiACkAOwANAAoACgByAGUAdAB1AHIAbgAgACgAbABvAGMAYQBsAE4AbwByAG0AYQBsAC4AYgAqAHQAZQB4ADEALgByAGcAYgApACAAKwAgACgAbABvAGMAYQBsAE4AbwByAG0AYQBsAC4AZwAqAHQAZQB4ADIALgByAGcAYgApACAAKwAgACgAbABvAGMAYQBsAE4AbwByAG0AYQBsAC4AcgAqAHQAZQB4ADMALgByAGcAYgApADsA,output:2,fname:UV_TPM,width:641,height:392,input:12,input:1,input:0,input:0,input_1_label:Tex,input_2_label:UV,input_3_label:Detail,input_4_label:Scale|A-6058-TEX,B-3459-UVOUT,C-6000-OUT,D-4238-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:6058,x:30980,y:32102,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_6058,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:3459,x:30980,y:32254,varname:node_3459,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:6000,x:30987,y:32430,ptovrint:False,ptlb:Detail,ptin:_Detail,varname:node_6000,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:4238,x:30987,y:32529,ptovrint:False,ptlb:Scale,ptin:_Scale,varname:node_4238,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Blend,id:9262,x:32612,y:33029,varname:node_9262,prsc:2,blmd:10,clmp:True;proporder:1304-6058-6000-4238;pass:END;sub:END;*/

Shader "TheMasonX/Triplanar Mapping/UV Space" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _Texture ("Texture", 2D) = "white" {}
        _Detail ("Detail", Float ) = 2
        _Scale ("Scale", Float ) = 2
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _LightColor0;
            float3 UV_TPM( sampler2D Tex , float2 UV , float Detail , float Scale ){
            float3 pointOnUnitSphere = float3(1,0,0);
            float pi = 3.1415926535897932384626433832795;
            
            //get the verical position
            float verticalTheta = .5 * pi * (-1 + (UV.y * 2) );
            pointOnUnitSphere.z = sin(verticalTheta);
            
            //get the radius for the horizontal cross section
            float xzRadius = abs(cos(verticalTheta));
            
            //get the position around a the circle cross section
            float horizontalTheta = UV.x * 2 * pi;
            pointOnUnitSphere.y = cos(horizontalTheta) * xzRadius;
            pointOnUnitSphere.x = sin(horizontalTheta) * xzRadius;
            
            //feel free to remove the detail setting if desired for performance (pow is expensive)
            //float3 localNormal = abs(pointOnUnitSphere.xyz);
            
            float3 localNormal = pow(abs(pointOnUnitSphere.xyz),Detail);
            
            float3 localPosition = pointOnUnitSphere.rgb/Scale;
            float4 tex1 = tex2D(Tex,localPosition.rg);
            float4 tex2 = tex2D(Tex,localPosition.rb);
            float4 tex3 = tex2D(Tex,localPosition.gb);
            
            return (localNormal.b*tex1.rgb) + (localNormal.g*tex2.rgb) + (localNormal.r*tex3.rgb);
            }
            
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Detail)
                UNITY_DEFINE_INSTANCED_PROP( float, _Scale)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float _Detail_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Detail );
                float _Scale_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Scale );
                float3 diffuseColor = (_Color_var.rgb*UV_TPM( _Texture , i.uv0 , _Detail_var , _Scale_var ));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform float4 _LightColor0;
            float3 UV_TPM( sampler2D Tex , float2 UV , float Detail , float Scale ){
            float3 pointOnUnitSphere = float3(1,0,0);
            float pi = 3.1415926535897932384626433832795;
            
            //get the verical position
            float verticalTheta = .5 * pi * (-1 + (UV.y * 2) );
            pointOnUnitSphere.z = sin(verticalTheta);
            
            //get the radius for the horizontal cross section
            float xzRadius = abs(cos(verticalTheta));
            
            //get the position around a the circle cross section
            float horizontalTheta = UV.x * 2 * pi;
            pointOnUnitSphere.y = cos(horizontalTheta) * xzRadius;
            pointOnUnitSphere.x = sin(horizontalTheta) * xzRadius;
            
            //feel free to remove the detail setting if desired for performance (pow is expensive)
            //float3 localNormal = abs(pointOnUnitSphere.xyz);
            
            float3 localNormal = pow(abs(pointOnUnitSphere.xyz),Detail);
            
            float3 localPosition = pointOnUnitSphere.rgb/Scale;
            float4 tex1 = tex2D(Tex,localPosition.rg);
            float4 tex2 = tex2D(Tex,localPosition.rb);
            float4 tex3 = tex2D(Tex,localPosition.gb);
            
            return (localNormal.b*tex1.rgb) + (localNormal.g*tex2.rgb) + (localNormal.r*tex3.rgb);
            }
            
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _Color)
                UNITY_DEFINE_INSTANCED_PROP( float, _Detail)
                UNITY_DEFINE_INSTANCED_PROP( float, _Scale)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 _Color_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Color );
                float _Detail_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Detail );
                float _Scale_var = UNITY_ACCESS_INSTANCED_PROP( Props, _Scale );
                float3 diffuseColor = (_Color_var.rgb*UV_TPM( _Texture , i.uv0 , _Detail_var , _Scale_var ));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
