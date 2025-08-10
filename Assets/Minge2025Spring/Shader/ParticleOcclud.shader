Shader "My Shaders/OccludedParticle_Flipbook"
{
    Properties
    {
        _MainTex ("Particle Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        [Header(Occlusion Settings)]
        _OccludedColor ("Occluded Color", Color) = (1, 0, 0, 0.5) // 隠れた時の色

        [Header(Texture Sheet Animation)]
        [Toggle(_)] _TexSheetHint ("Enable in Particle System's module", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "DisableBatching"="True" // バッチングを無効にしないとステンシルが正しく機能しない場合がある
        }

        // ===================================================================
        // パス 0: 隠れた部分を描画し、ステンシルを更新する
        // ===================================================================
        Pass
        {
            Cull Off
            Lighting Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest Greater // ★奥にある（隠れている）時のみ描画

            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace
                ZFail Zero
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_particles

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                fixed4 color : COLOR;
            };

            sampler2D _MainTex;
            fixed4 _OccludedColor;

            v2f vert(appdata IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // Texture Sheet Animationが有効な場合、IN.texcoordはUnityによって自動的に更新される
                fixed texAlpha = tex2D(_MainTex, IN.texcoord.xy).a;
                fixed4 final_color = _OccludedColor;
                final_color.a *= texAlpha * IN.color.a;

                clip(final_color.a - 0.01);
                return final_color;
            }
            ENDCG
        }

        // ===================================================================
        // パス 1: 見えている時のパーティクル本体を描画。ただしステンシルの目印がない部分のみ
        // ===================================================================
        Pass
        {
            Cull Off
            Lighting Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual // ★手前にある（見えている）時のみ描画

            Stencil
            {
                Ref 1
                Comp NotEqual
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_particles
            #pragma multi_compile _ PIXELSNAP_ON

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _Color;

            v2f vert(appdata IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap(OUT.vertex);
                #endif
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                // Texture Sheet Animationが有効な場合、IN.texcoordはUnityによって自動的に更新される
                fixed4 color = tex2D(_MainTex, IN.texcoord.xy) * IN.color;
                clip(color.a - 0.01);
                return color;
            }
            ENDCG
        }
    }
    Fallback "Transparent/VertexLit"
}