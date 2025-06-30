Shader "Custom/ExternalEdgeFade"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (1,1,1,1)
        _FadeSize ("Fade Size", Range(0.01, 1.0)) = 0.2
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off

        Pass
        {
            Name "EdgeFadePass"
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            float4 _BaseColor;
            float _FadeSize;

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR; // Usamos .a como indicador de borde
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float edgeFactor : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.edgeFactor = v.color.a;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // Suavizado: si el borde vale 1, se desvanece con _FadeSize
                float alpha = saturate(1.0 - i.edgeFactor * _FadeSize);
                return float4(_BaseColor.rgb, _BaseColor.a * alpha);
            }
            ENDHLSL
        }
    }
}
