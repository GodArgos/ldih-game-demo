Shader "Custom/PsychedelicDistortion2D"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistortStrength ("Distortion Strength", Float) = 0.05
        _DistortSpeed ("Distortion Speed", Float) = 2
        _Frequency ("Distortion Frequency", Float) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _DistortStrength;
            float _DistortSpeed;
            float _Frequency;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                float t = _Time.y * _DistortSpeed;

                // Ondas senoidales para distorsión psicodélica
                uv.x += sin(uv.y * _Frequency + t) * _DistortStrength;
                uv.y += cos(uv.x * _Frequency + t) * _DistortStrength;

                fixed4 col = tex2D(_MainTex, uv);
                return col;
            }
            ENDCG
        }
    }
}
