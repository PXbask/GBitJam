Shader "Custom/Blur" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _BlurRadius("Blur Radius", Range(0, 10)) = 1.0
    }

    SubShader{
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

            v2f vert(appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                float2 texCoord = i.uv;
                float2 offset = float2(_BlurRadius, _BlurRadius);
                fixed4 blurColor = tex2D(_MainTex, texCoord + offset);
                blurColor += tex2D(_MainTex, texCoord - offset);
                blurColor *= 0.5;
                return blurColor;
            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}