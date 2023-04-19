Shader "Custom/Blur" {
    Properties{
        _BlurRadius("Blur Radius", Range(0.0, 10.0)) = 1.0
        _MainTex("Texture", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
    }

        SubShader{
            Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}

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
                float _BlurRadius;
                float4 _Color;

                v2f vert(appdata v) {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target {
                    fixed4 col = tex2D(_MainTex, i.uv);
                    float blurSize = _BlurRadius * 0.1;
                    float4 sum = float4(0.0f, 0.0f, 0.0f, 0.0f);

                    sum += tex2D(_MainTex, i.uv + float2(-blurSize, -blurSize));
                    sum += tex2D(_MainTex, i.uv + float2(-blurSize, blurSize));
                    sum += tex2D(_MainTex, i.uv + float2(blurSize, -blurSize));
                    sum += tex2D(_MainTex, i.uv + float2(blurSize, blurSize));

                    col = (col + sum) / 5.0;
                    col = col *  _Color;

                    return col;
                }
                ENDCG
            }
    }
        FallBack "UI/Default"
}