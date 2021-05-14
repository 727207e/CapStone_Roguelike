Shader "Unlit/TVNoiseShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _NoiseTex("Noise Texture", 2D) = "grey" {}
        _StPt("Dissolve Start Point",Vector) = (0,0,0,0)
        _DissolveRange("Dissolve Range", Range(0,5)) = 0.0
        _DissolveExp("Dissolve Exponent", Range(0,5)) = 0.0
        _DistortionStrength("Distortion Strength", Range(-1,1)) = 0.0
        _BlurStrength("Blur Strength", Range(0,30)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _NoiseTex;
            float4 _MainTex_ST;
            float4 _NoiseTex_ST;
            half4 _StPt;
            half _DissolveRange;
            half _DissolveExp;
            half _DistortionStrength;
            half _BlurStrength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 DissolvePoint = _StPt.xy;

                half NoiseValue = tex2D(_NoiseTex, i.uv);

                half DissolveFactor = saturate(distance(i.uv, _StPt.xy) + (1 - _DissolveRange));
                DissolveFactor = pow(DissolveFactor, _DissolveExp);
                DissolveFactor = 1 - (DissolveFactor);

                float4 NewUV = float4(float2(i.uv - NoiseValue * DissolveFactor * _DistortionStrength), 0, DissolveFactor * _BlurStrength);
                half4 col = tex2Dlod(_MainTex, NewUV) * (1 - DissolveFactor);

                return col;
            }
            ENDCG
        }
    }
}
