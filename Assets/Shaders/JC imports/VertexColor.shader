Shader "Mobile/VertexColor" {
    Properties{}
        SubShader{
            Tags { "RenderType" = "Opaque" }
            LOD 250

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd

        struct Input {
            float4 color : COLOR;
        };

        void surf(Input IN, inout SurfaceOutput o) {
            fixed4 color = IN.color;

            o.Albedo = color.rgb;
            o.Gloss = color.a;
            o.Alpha = color.a;
        }
        ENDCG
    }

        FallBack "Mobile/Bumped Specular"
}