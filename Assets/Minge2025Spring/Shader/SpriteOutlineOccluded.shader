Shader "My Shaders/OccludedFill_Fixed"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [Header(Occlusion Settings)]
        _OccludedColor ("Occluded Color", Color) = (1, 0, 0, 0.5) // 隠れた時の色（デフォルトは半透明の赤）
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

            // ステンシル設定：
            // 隠れているピクセルには目印(1)を付け、見えているピクセルはリセット(0)する
            Stencil
            {
                Ref 1
                Comp Always
                Pass Replace // Zテストに成功した場合(隠れている場合)、ステンシル値を1にする
                ZFail Zero   // ★★★ 修正箇所 ★★★ Zテストに失敗した場合(見えている場合)、ステンシル値を0にリセットする
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _OccludedColor;

            v2f vert(appdata IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed alpha = tex2D(_MainTex, IN.texcoord).a;
                fixed4 final_color = _OccludedColor;
                final_color.a *= alpha;
                clip(final_color.a - 0.01);
                return final_color;
            }
            ENDCG
        }

        // ===================================================================
        // パス 1: 見えている時のスプライト本体を描画。ただしステンシルの目印がない部分のみ
        // ===================================================================
        Pass
        {
            Cull Off
            Lighting Off
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            ZTest LEqual // ★手前にある（見えている）時のみ描画

            // ステンシル設定：目印(1)が付いていないピクセル（隠れていない部分）のみ描画する
            Stencil
            {
                Ref 1
                Comp NotEqual // ステンシル値が1でなければ描画
            }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
                fixed4 color = tex2D(_MainTex, IN.texcoord) * IN.color;
                clip(color.a - 0.01);
                return color;
            }
            ENDCG
        }
    }
    Fallback "Transparent/VertexLit"
}