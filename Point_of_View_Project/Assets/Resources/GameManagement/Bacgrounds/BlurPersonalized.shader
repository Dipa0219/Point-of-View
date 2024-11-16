Shader "Custom/UIBlur"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _BlurSize;

            v2f vert (appdata_t v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                fixed4 col = 0;
                int samples = 10; // Number of samples for blur
                float totalWeight = 0;

                for (int x = -samples; x <= samples; x++) {
                    for (int y = -samples; y <= samples; y++) {
                        float weight = exp(-0.5 * (x*x + y*y) / (_BlurSize*_BlurSize));
                        totalWeight += weight;
                        col += tex2D(_MainTex, uv + float2(x, y) * _BlurSize) * weight;
                    }
                }

                return col / totalWeight;
            }
            ENDCG
        }
    }
}
