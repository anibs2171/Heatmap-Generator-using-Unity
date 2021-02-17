Shader "Custom/Test 1"
{
    Properties
    {
       [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)

        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255

        _ColorMask("Color Mask", Float) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
    }


    SubShader
    {
        Tags { "Queue" = "Transparent"
                "IgnoreProjector" = "True"
                "RenderType" = "Transparent"
                "PreviewType" = "Plane"
                "CanUseSpriteAtlas" = "True"
    
        }
        
        Stencil
        {
            Ref[_Stencil]
            Comp[_StencilComp]
            Pass[_StencilOp]
            ReadMask[_StencilReadMask]
            WriteMask[_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest[unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask[_ColorMask]

        Pass
        {
            Name "Default"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            #pragma multi_compile_local _ UNITY_UI_CLIP_RECT
            #pragma multi_compile_local _ UNITY_UI_ALPHACLIP

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord  : TEXCOORD0;
                float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.worldPosition = v.vertex;
                o.vertex = UnityObjectToClipPos(o.worldPosition);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

         

            fixed4 frag(v2f i) : SV_Target
            {


                float Points[18] =  {0.75, 0.75, 0.75, 0.75, 0.25, 0.25, 0.35, 0.35, 0.85, 0.85, 0.75, 0.75, 0.75, 0.25, 0.75, 0.25, 0.75, 0.25};
                
                
                half4 blu = half4(0, 0.0, 0.3, 0.5);
                half4 gre = half4(0, 0.35, 0.2, 0.5);
                half4 yel = half4(0, -0.1, 0.01, 0.5);
               
                // sample the texture
                float _Radius = 0.5;
                //fixed4 col = tex2D(_MainTex, i.worldPosition);
                float y = 1 - i.texcoord.y;
                float x = i.texcoord.x;
                half4 col = half4(0, 0, 0, 0);
                //float2 pos[] = { float2(0.1, 0.5), float2(0.1, 0.37) };
                for (int k = 0; k < 17; k+=2)
                {
                    float dis = distance(float2(x, y), float2(Points[k], Points[k + 1]));

                    col = col + blu * smoothstep(_Radius * 0.5  , _Radius * 0.1, dis) + gre * smoothstep(_Radius* 0.25, _Radius * 0.05, dis) + yel * smoothstep(_Radius * 0.25, _Radius * 0.01, dis);

                }

                //return color;
                return col;
            }
            ENDCG
        }
    }
}

/*

These are the points:
Bottom left->(0.25, 0.75)
Bottom right->(0.75, 0.75), (0.85, 0.85)
Top left->(0.25, 0.25), (0.25, 0.25), (0.35, 0.35)
Top right->(0.75, 0.25), (0.75, 0.25), (0.75, 0.25)

*/





