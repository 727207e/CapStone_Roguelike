Shader "Unlit/WavePlane"
{
    Properties
    {
        //이미지에 들어갈 색상, 부드러운정도, 메탈
        _Color("Color", Color) = (1,1,1,1)
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
        //메인 이미지 관리
        _MainTex ("Texture", 2D) = "white" {}

        //물결 무늬 진동 속도
        _Speed("Speed",Range(0,100)) = 1

        //각 축의 진동속도
        _FreqX("FreqX", Range(0,2)) = 1
        _FreqY("FreqY", Range(0,2)) = 1


        //파동의 진폭
        _Amplitude("Amplitude", Range(0,10)) = 1


        //텍스쳐 이동시키는 프로퍼티
        _ScrollX("ScrollX", Range(-1, 1)) = 1
        _ScrollY("ScrollY", Range(-1, 1)) = 1


        //텍스쳐 값의 합을 나누는 값으로 밝기 조절
        _FakeEmission("FakeEmission", Range(0.5, 2)) = 2

    }

    SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work

                #include "UnityCG.cginc"


                fixed4 _Color;
        //메인물결 텍스쳐
        sampler2D _MainTex;
        float4 _MainTex_ST;

        //진동 운동을 조절
        float _Speed;
        float _FreqX;
        float _FreqY;
        float _Amplitude;

        //흐르는 텍스쳐 효과
        float _ScrollX;
        float _ScrollY;
        float _FakeEmission;
        struct appdata
        {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct v2f
        {
            float2 uv : TEXCOORD0;

            float4 vertex : SV_POSITION;
            fixed4 diff : COLOR0; //diffuse lighting color
        };


        v2f vert(appdata v)
        {
            v2f o;

            //1.버텍스의 높이를 움직여 물결 무늬 제작
            float t = _Time * _Speed;
            float waveHeight = (sin(t + (v.vertex.x) * _FreqX) + sin(t + (v.vertex.z) * _FreqY)) / 2 * _Amplitude;
            v.vertex.y = v.vertex.y + waveHeight;

            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            return o;
        }

        fixed4 frag(v2f i) : SV_Target
        {
            //텍스처를 이동시킬 갑시을 시간에 따라 바꾸어준다
            _ScrollX *= _ScrollX * _Time.y;
            _ScrollY *= _ScrollY * _Time.y;

            //기본 프리셋 텍스처 적용 코드
            float2 newUV = i.uv + float2(_ScrollX, _ScrollY);
            fixed4 col = tex2D(_MainTex, newUV);

            fixed4 c = col * _Color;

            return c / _FakeEmission;
        }
        ENDCG
    }
    }
}
