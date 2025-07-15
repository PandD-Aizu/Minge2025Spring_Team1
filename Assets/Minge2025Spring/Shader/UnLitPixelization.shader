Shader "My Shaders/UnLitPixelization"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _PixelSize("Pixel Size", float) = 100.0
    }
    
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        
        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            # include "UnityCG.cginc"

            /// 変数宣言
            Texture2D _MainTex;           // メインテクスチャ
            SamplerState sampler_MainTex; // サンプラーステート
            float4 _MainTex_ST;           // テクスチャのST変換（スケールとオフセット）
            float _PixelSize;             // ピクセルサイズ

            /// 構造体
            // 頂点属性とバリエーションを定義
            struct Attributes
            {
                float4 positionObjectSpace : POSITION;  // 頂点位置（オブジェクト空間）
                float2 uv                  : TEXCOORD0; // UV座標
            };

            // バリエーション構造体
            struct Varyings
            {
                float4 positionClipSpace   : SV_POSITION; // クリップ空間位置
                float2 uv                  : TEXCOORD0;   // UV座標
            };

            /// <summary>
            /// 頂点シェーダー
            /// <summary>
            /// <param name="In">入力属性</param>
            /// <returns>出力バリエーション</returns>
            Varyings vert (Attributes In)
            {
                Varyings Out;
                Out.positionClipSpace = UnityObjectToClipPos(In.positionObjectSpace.xyz);
                Out.uv = In.uv * _MainTex_ST.xy + _MainTex_ST.zw;

                return Out;
            }

            /// <summary>
            /// フラグメントシェーダー
            /// <summary>
            /// <param name="In">入力バリエーション</param>
            /// <retuns>出力カラー</returns>
            half4 frag (Varyings In) : SV_Target
            {
                float2 snappedUV = (floor(In.uv * _PixelSize) + 0.5) / _PixelSize;

                half4 col = _MainTex.Sample(sampler_MainTex, snappedUV);

                return col;
            }

            ENDHLSL
        }
    }
}
