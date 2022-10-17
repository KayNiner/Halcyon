Shader "Custom/GradientSkyboxShader"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (1, 1, 1, 1)
        _MidColor ("Mid Color", Color) = (1, 1, 1, 1)
        _Amount ("Color Amount", Range (-10,10)) = 1
    }

    SubShader//thinkkk this is right.
    {
        Tags { "RenderType"="Background" "Queue"="Background" }
        Pass
        {
            ZWrite Off
            Cull Off
            Fog { Mode Off }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }

    CGINCLUDE
    #include "UnityCG.cginc"

    struct appdata
    {
        float4 position : POSITION;
        float3 texcoord : TEXCOORD0;
    };
    
    struct v2f
    {
        float4 position : SV_POSITION;
        float3 texcoord : TEXCOORD0;
    };
    
    half4 _TopColor;
    half4 _MidColor;
    half _Amount;
    
    v2f vert (appdata v)
    {
        v2f o;
        o.position = UnityObjectToClipPos (v.position);
        o.texcoord = v.texcoord;
        return o;
    }
    
    fixed4 frag (v2f i) : COLOR
    {
        half d = dot (normalize (i.texcoord), half4(0, 1, 0, 0)) * _Amount;
        return lerp (_MidColor, _TopColor, pow (d, 1));
    }
    ENDCG
}