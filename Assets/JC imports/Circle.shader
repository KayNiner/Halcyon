Shader "Unlit/Circle"
{
    Properties
    {
        //_Color("Color", Color) = (1,1,1,1)
        _Width("Width", float) = 1
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
        Blend SrcAlpha One
        ColorMask RGB
        Cull Off Lighting Off ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
        
            float _Width;
            //fixed4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;  
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float dis = length(float2(i.uv.x - 0.5, i.uv.y - 0.5));
                float lower = 0.5 - _Width;
                if (dis > 0.5 || dis < lower) discard;
                float a = min(saturate((0.5 - dis) / fwidth(dis)), saturate((dis - lower) / fwidth(dis)));

                return fixed4(i.color.rgb, i.color.a * a);
            }
            ENDCG
        }
    }
}
