Shader "Mobile/VertexWithBump" {
    Properties{
        _Shininess("Shininess", Range(0.03, 1)) = 0.078125
        _BumpMap("Normalmap", 2D) = "bump" {}
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 250

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd


        sampler2D _BumpMap;
        half _Shininess;

        struct Input {
            float4 color : COLOR;
            float2 uv_BumpMap;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 color = IN.color;
            float4 Tex2D1 = tex2D(_BumpMap,IN.uv_BumpMap.xy);

            o.Albedo = color.rgb;
            o.Gloss = color.a;
            o.Alpha = color.a;
            o.Specular = _Shininess;
            o.Normal = UnpackNormal(Tex2D1);

        }
        ENDCG
    }

        FallBack "Mobile/Bumped Specular"
}