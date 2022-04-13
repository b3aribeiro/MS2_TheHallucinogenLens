Shader "Hidden/Testing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _TintColor("Tint Color", Color) = (1,1,1,1)
        _Transparency("Transparency", Range(0.0,1)) = 0.5
    }

    SubShader
    {
        // No culling or depth
        //Cull Off ZWrite Off ZTest Always
        Tags {"Queue" = "Transparent" "RenderType"="Transparent"}
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4    _MainTex_ST;
            float _Transparency;
            float _TintColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                //o.uv = v.uv;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_TARGET
            {
                fixed4 col = tex2D(_MainTex, i.uv) + _TintColor;
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                col.r = 0.5;
                col.a = _Transparency;

                 fixed4 returnCol = fixed4(2, col.g, col.b, col.a);

                return col;
            }
            ENDCG
        }
    }
}
