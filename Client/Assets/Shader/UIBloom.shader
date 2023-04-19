Shader "Custom/UIBloom" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _BloomIntensity("Bloom Intensity", Range(0, 10)) = 2
        _BloomThreshold("Bloom Threshold", Range(0, 1)) = 0.5
        _BloomSoftness("Bloom Softness", Range(0, 1)) = 0.5
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "TransparentUI"}
            LOD 100

            Pass {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                float _BloomIntensity;
                float _BloomThreshold;
                float _BloomSoftness;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    fixed4 bloom = 0;
                    float threshold = _BloomThreshold;
                    float softness = _BloomSoftness;

                    // Calculate bloom
                    if (col.r > threshold) {
                        bloom = (col.r - threshold) / (1 - threshold);
                        bloom = pow(bloom, 1.0 / (softness + 0.0001));
                        bloom *= _BloomIntensity;
                    }

                    return col + bloom;
                }
                ENDCG
            }
        }
            FallBack "UI/Default"
}